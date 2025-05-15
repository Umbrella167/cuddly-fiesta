using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using Google.Protobuf;
using TMPro;

public class RefereeReceiver : MonoBehaviour
{
    private Socket refereeSocket;
    public Referee referee_msg = null;
    public TMP_Text ref_msg = null;
    public GameObject ball_placement = null;
    public string MCAST_GRP = Param.REF_MCAST_GRP;
    public int MCAST_PORT = Param.REF_MCAST_PORT;

    void Start()
    {
        // ����UDP�׽���
        refereeSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        // ���õ�ַ����
        refereeSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

        // �󶨵�0.0.0.0��ַ
        EndPoint endPoint = new IPEndPoint(IPAddress.Any, MCAST_PORT);
        refereeSocket.Bind(endPoint);

        // ����ಥ��
        IPAddress multicastAddress = IPAddress.Parse(MCAST_GRP);
        MulticastOption multicastOption = new MulticastOption(multicastAddress);
        refereeSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, multicastOption);

        // ��ʼ��������
        Thread receiveThread = new Thread(ReceiveData);
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    void Update()
    {
        // ������µĲ�����Ϣ�����������ﴦ��
        if (referee_msg != null)
        {
            ProcessRefereeMessage(referee_msg);
            referee_msg = null;
        }
    }

    void ReceiveData()
    {
        try
        {
            byte[] buffer = new byte[65535]; // ���UDP���ݰ���С
            EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                int bytesReceived = refereeSocket.ReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref remoteEndPoint);

                if (bytesReceived > 0)
                {
                    byte[] data = new byte[bytesReceived];
                    Array.Copy(buffer, data, bytesReceived);
                    try
                    {
                        Referee newRefereeMsg = Referee.Parser.ParseFrom(data);

                        // ʹ������ȷ���̰߳�ȫ�ظ��� referee_msg
                        lock (this)
                        {
                            referee_msg = newRefereeMsg;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Failed to parse referee packet: {ex.Message}");
                    }
                }
            }
        }
        catch (SocketException ex)
        {
            Debug.LogError("Socket error: " + ex.Message);
        }
    }

    void ProcessRefereeMessage(Referee msg)
    {
        // ��ӡ��ǰ����
        Debug.Log($"Current Command: {msg.Command}");
        string command_str = msg.Command.ToString();
        ref_msg.text = "Ref Msg: " + command_str;

        // ����Ƿ���ָ��λ��
        if (msg.DesignatedPosition != null && (command_str == "BallPlacementYellow" || command_str == "BallPlacementBlue" ))
        {
            ball_placement.transform.position = new Vector3((float) msg.DesignatedPosition.X * Param.SCALE_COORDINATE, 0.07f, (float)msg.DesignatedPosition.Y * Param.SCALE_COORDINATE);
        }
        else 
        {
            ball_placement.transform.position = new Vector3(0, -1f, 0);
        }
    }

    void OnApplicationQuit()
    {
        // �˳�ʱ�뿪�ಥ�鲢�ͷ���Դ
        if (refereeSocket != null)
        {
            refereeSocket.Close();
            refereeSocket = null;
        }
    }
}