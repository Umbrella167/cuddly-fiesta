using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using Google.Protobuf;
using static packet;

public class MulticastReceiver : MonoBehaviour
{

    private Socket multicastSocket;
    public SSL_WrapperPacket vision_packet_recive = null;
    public SSL_WrapperPacket vision_packet = null;
    static public SSL_WrapperPacket[] vision_packet_real = new SSL_WrapperPacket[4];

    private const string MCAST_GRP = "224.5.23.2";
    private const int MCAST_PORT = 10020;
    private const float SCALE_NUM = 0.01f;
    private const float OUT_OF_SIGHT_Y = -10f; // Y坐标，表示机器人不在视野内
    private const int NUM_ROBOTS = 16; // 每个队伍的机器人数量
    private const int DISAPPEARANCE_THRESHOLD = 20; // 消失次数阈值
    public GameObject ball_obj = null;
    // 存储机器人GameObject的字典，提高查找效率
    private Dictionary<string, GameObject> blueRobots = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> yellowRobots = new Dictionary<string, GameObject>();

    // 存储机器人消失次数的字典
    private Dictionary<string, int> blueDisappearanceCounts = new Dictionary<string, int>();
    private Dictionary<string, int> yellowDisappearanceCounts = new Dictionary<string, int>();

    void Start()
    {
        // 初始化机器人字典
        InitializeRobots(blueRobots, "blue_robot");
        InitializeRobots(yellowRobots, "yellow_robot");

        // 初始化消失次数字典
        InitializeDisappearanceCounts(blueDisappearanceCounts, "blue_robot");
        InitializeDisappearanceCounts(yellowDisappearanceCounts, "yellow_robot");

        ball_obj = GameObject.Find("Ball");
        // 创建UDP套接字
        multicastSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        // 设置地址重用
        multicastSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

        // 绑定到0.0.0.0地址
        EndPoint endPoint = new IPEndPoint(IPAddress.Any, MCAST_PORT);
        multicastSocket.Bind(endPoint);

        // 加入多播组
        IPAddress multicastAddress = IPAddress.Parse(MCAST_GRP);
        MulticastOption multicastOption = new MulticastOption(multicastAddress);
        multicastSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, multicastOption);

        // 开始接收数据
        Thread receiveThread = new Thread(ReceiveData);
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    // 初始化机器人字典
    private void InitializeRobots(Dictionary<string, GameObject> robotDict, string robotPrefix)
    {
        for (int i = 0; i < NUM_ROBOTS; i++)
        {
            string obj_name = robotPrefix + i.ToString();
            GameObject robot = GameObject.Find(obj_name);
            if (robot != null)
            {
                robotDict.Add(obj_name, robot);
            }
            else
            {
                Debug.LogError($"Robot GameObject not found: {obj_name}");
            }
        }
    }

    // 初始化消失次数字典
    private void InitializeDisappearanceCounts(Dictionary<string, int> disappearanceCounts, string robotPrefix)
    {
        for (int i = 0; i < NUM_ROBOTS; i++)
        {
            string obj_name = robotPrefix + i.ToString();
            disappearanceCounts.Add(obj_name, 0);
        }
    }

    void Update()
    {
        
        for(int i = 0; i < 4; i++) 
        {
            if (vision_packet_real[i] != null) 
            {
                ProcessPacket(vision_packet_real[i]);
                vision_packet_real[i] = null;
            }
        
        }

    }

    void ReceiveData()
    {
        try
        {
            byte[] buffer = new byte[65535]; // 最大UDP数据包大小
            EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                int bytesReceived = multicastSocket.ReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref remoteEndPoint);
                // 将接收到的字节数组反序列化为SSL_WrapperPacket
                if (bytesReceived > 0)
                {
                    byte[] data = new byte[bytesReceived];
                    Array.Copy(buffer, data, bytesReceived);
                    try
                    {
                        SSL_WrapperPacket newPacket = SSL_WrapperPacket.Parser.ParseFrom(data);
                        // 使用锁来确保线程安全地更新 vision_packet
                        lock (this)
                        {
                            vision_packet = newPacket;
                            vision_packet_real[vision_packet.Detection.CameraId] = newPacket;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Failed to parse packet: {ex.Message}");
                    }
                }
            }
        }
        catch (SocketException ex)
        {
            Debug.LogError("Socket error: " + ex.Message);
        }
    }

    void ProcessPacket(SSL_WrapperPacket packet)
    {
        // 处理检测数据
        
        if (packet.Detection != null)
        {
            // 创建HashSet来快速查找当前帧中存在的机器人ID
            HashSet<uint> detectedBlueIds = new HashSet<uint>();
            HashSet<uint> detectedYellowIds = new HashSet<uint>();
            foreach (var ball in packet.Detection.Balls) 
            {
                float x = ball.X * SCALE_NUM;
                float y = ball.Y * SCALE_NUM;
                ball_obj.transform.position = new UnityEngine.Vector3(x, -0.5f, y);
            }
            // 处理蓝色机器人
            foreach (var robot_blue in packet.Detection.RobotsBlue)
            {

                uint id = robot_blue.RobotId;
                detectedBlueIds.Add(id);
                float x = robot_blue.X * SCALE_NUM;
                float y = robot_blue.Y * SCALE_NUM;
                //float dir = -robot_blue.Orientation * Mathf.Rad2Deg;
                float dir = -(robot_blue.Orientation + Mathf.PI / 2) * Mathf.Rad2Deg;
                string obj_name = "blue_robot" + id.ToString();

                if (blueRobots.ContainsKey(obj_name))
                {

                    blueRobots[obj_name].transform.position = new UnityEngine.Vector3(x, 0, y);
                    blueRobots[obj_name].transform.rotation = UnityEngine.Quaternion.Euler(0, dir, 0);
                    blueDisappearanceCounts[obj_name] = 0; // 重置消失计数器
                }
                else
                {
                    Debug.LogWarning($"Robot GameObject not found in dictionary: {obj_name}");
                }
            }

            // 处理黄色机器人
            foreach (var robot_yellow in packet.Detection.RobotsYellow)
            {
                uint id = robot_yellow.RobotId;
                detectedYellowIds.Add(id);
                float x = robot_yellow.X * SCALE_NUM;
                float y = robot_yellow.Y * SCALE_NUM;
                float dir = -(robot_yellow.Orientation + Mathf.PI / 2) * Mathf.Rad2Deg;
                string obj_name = "yellow_robot" + id.ToString();

                if (yellowRobots.ContainsKey(obj_name))
                {
                    yellowRobots[obj_name].transform.position = new UnityEngine.Vector3(x, 0, y);
                    yellowRobots[obj_name].transform.rotation = UnityEngine.Quaternion.Euler(0, dir, 0);
                    //Debug.Log($"{dir}");
                    yellowDisappearanceCounts[obj_name] = 0; // 重置消失计数器
                }
                else
                {
                    Debug.LogWarning($"Robot GameObject not found in dictionary: {obj_name}");
                }
            }

            // 处理未检测到的蓝色机器人
            for (int i = 0; i < NUM_ROBOTS; i++)
            {
                string obj_name = "blue_robot" + i.ToString();
                if (!detectedBlueIds.Contains((uint)i))
                {
                    blueDisappearanceCounts[obj_name]++; // 增加消失计数器
                    if (blueDisappearanceCounts[obj_name] > DISAPPEARANCE_THRESHOLD)
                    {
                        if (blueRobots.ContainsKey(obj_name))
                        {
                            blueRobots[obj_name].transform.position = new UnityEngine.Vector3(0, OUT_OF_SIGHT_Y, 0);
                        }
                    }
                }
            }

            // 处理未检测到的黄色机器人
            for (int i = 0; i < NUM_ROBOTS; i++)
            {
                string obj_name = "yellow_robot" + i.ToString();
                if (!detectedYellowIds.Contains((uint)i))
                {
                    yellowDisappearanceCounts[obj_name]++; // 增加消失计数器
                    if (yellowDisappearanceCounts[obj_name] > DISAPPEARANCE_THRESHOLD)
                    {
                        if (yellowRobots.ContainsKey(obj_name))
                        {
                            yellowRobots[obj_name].transform.position = new UnityEngine.Vector3(0, OUT_OF_SIGHT_Y, 0);
                        }
                    }
                }
            }
        }

        // 处理几何数据
        if (packet.Geometry != null)
        {
            //Debug.Log(packet.Geometry);
            // 在这里处理Geometry数据
        }
    }

    void OnApplicationQuit()
    {
        // 退出时离开多播组并释放资源
        if (multicastSocket != null)
        {
            multicastSocket.Close();
            multicastSocket = null;
        }
    }
}