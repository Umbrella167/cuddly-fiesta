using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;

public class UdpNetwork : MonoBehaviour
{
    public enum Mode { Server, Client }

    public Mode networkMode = Mode.Server;

    // Server settings
    public int serverPort = 16701;

    // Client settings
    public string serverIP = "172.20.64.232";
    public int clientTargetPort = 16701;

    private UdpClient udpClient;
    private IPEndPoint targetEndPoint;
    private Thread receiveThread;
    private bool isRunning;

    public event Action<byte[], string> OnMessageReceived; // (message, sourceIP)

    void Start()
    {
        serverIP = Connect_Gate.IP;
        clientTargetPort = Connect_Gate.Port;

        Loom.Initialize(); // ������г�ʼ������
        if (Connect_Gate.GAME_MODE == "Serial")
        {
            networkMode = Mode.Server;
            InitializeServer();
        }
        else if (Connect_Gate.GAME_MODE == "Client")
        {
            networkMode = Mode.Client;
            InitializeClient();
        }
        //if (networkMode == Mode.Server)
        //{
        //    InitializeServer();
        //}
        //else
        //{
        //    InitializeClient();
        //}
    }

    private void InitializeServer()
    {
        // Bind to 0.0.0.0 to listen on all interfaces
        udpClient = new UdpClient(new IPEndPoint(IPAddress.Parse("0.0.0.0"), serverPort));
        isRunning = true;
        receiveThread = new Thread(ServerReceive);
        receiveThread.Start();
    }

    private void InitializeClient()
    {
        udpClient = new UdpClient(0);
        //udpClient.EnableBroadcast = true; // �����Ҫ�㲥�ɿ���
        Debug.Log($"Sent {serverIP} bytes to {clientTargetPort}");

        targetEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), clientTargetPort);
        Debug.Log($"Sent {serverIP} bytes to {clientTargetPort}");
    }

    private void ServerReceive()
    {
        while (isRunning)
        {
            try
            {
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = udpClient.Receive(ref remoteEndPoint);
                //string message = Encoding.UTF8.GetString(data);

                // Unity���߳�ִ��
                Loom.QueueOnMainThread(() =>
                {
                    OnMessageReceived?.Invoke(data, remoteEndPoint.Address.ToString());
                });
            }
            catch (Exception e)
            {
                Debug.LogError($"Receive error: {e.Message}");
            }
        }
    }

    public void Send(byte[] message)
    {
        if (networkMode != Mode.Client)
        {
            Debug.LogWarning("Only client can send messages");
            return;
        }

        try
        {
            udpClient.Send(message, message.Length, targetEndPoint);
            //Debug.Log($"Sent {message.Length} bytes to {targetEndPoint}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Send error: {e.Message}");
        }
    }

    void OnDestroy()
    {
        isRunning = false;
        udpClient?.Close();
        receiveThread?.Abort();
    }
}

// ��Ҫ����Ŀ�����Loom.cs���ڶ��̻߳ص�
public class Loom : MonoBehaviour
{
    private static Loom _instance;

    public static void Initialize()
    {
        if (_instance == null)
        {
            _instance = new GameObject("Loom").AddComponent<Loom>();
            DontDestroyOnLoad(_instance.gameObject);
        }
    }

    public static void QueueOnMainThread(Action action)
    {
        lock (_actions)
        {
            _actions.Add(action);
        }
    }

    private static List<Action> _actions = new List<Action>();
    private List<Action> _currentActions = new List<Action>();

    void Update()
    {
        lock (_actions)
        {
            _currentActions.Clear();
            _currentActions.AddRange(_actions);
            _actions.Clear();
        }

        foreach (var action in _currentActions)
        {
            action();
        }
    }
}