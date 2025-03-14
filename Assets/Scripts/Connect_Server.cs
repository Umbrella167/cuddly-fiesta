using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net.Sockets;
using TMPro;
using System;
using static packet;


public class Connect_Server : MonoBehaviour
{
    public TMP_Dropdown TeamDropdown;
    public TMP_InputField RobotId_Input, Frequency_Input;
    public TextMeshProUGUI SocketStatusText;
    public TMP_InputField IP_Input, Port_Input;
    public Button Connect_Socket_Button;

    static public string ipAddress = "127.0.0.1"; // Default IP
    static public int port = 114514; // Default Port
    static public int robotID = 0;
    static public string team = "Blue";
    static public RadioPacket packet = null;
    private TcpClient client;
    private NetworkStream stream;

    void Start()
    {
        Connect_Socket_Button.onClick.AddListener(ButtonOnClickEvent);
        //GameObject.Find("Connect_Socket_Button").GetComponent<Button>().onClick.AddListener(ButtonOnClickEvent);
    }

    // 尝试打开Socket连接并返回成功状态
    private bool OpenSocket(string ipAddress, int port)
    {
        try
        {
            if (client != null && client.Connected)
            {
                client.Close();
            }

            client = new TcpClient();
            client.Connect(ipAddress, port);

            if (client.Connected)
            {
                stream = client.GetStream();
                UpdateStatus($"成功连接 {ipAddress}:{port}");
                return true;
            }
        }
        catch (Exception ex)
        {
            UpdateStatus($"连接失败: {ex.Message}");
        }
        return false;
    }

    public void ButtonOnClickEvent()
    {
        Connect_Gate.team = TeamDropdown.options[TeamDropdown.value].text;

        // 验证输入
        if (!int.TryParse(RobotId_Input.text, out Connect_Gate.robotID))
        {
            UpdateStatus("无效的机器人ID");
            return;
        }

        if (!int.TryParse(Port_Input.text, out port))
        {
            UpdateStatus("无效的端口值");
            return;
        }

        if (!int.TryParse(Frequency_Input.text, out Connect_Gate.frequency))
        {
            UpdateStatus("无效的频率值");
            return;
        }

        ipAddress = IP_Input.text;
        if (string.IsNullOrEmpty(ipAddress))
        {
            UpdateStatus("请输入有效IP地址");
            return;
        }

        // 尝试打开Socket
        if (!OpenSocket(ipAddress, port))
        {
            return;
        }
        Debug.Log($"测试测试: {Connect_Gate.frequency}");
        Connect_Gate.packet = new RadioPacket(Connect_Gate.frequency);
        Connect_Gate.isender = new SocketHandler(client);
        SceneManager.LoadScene("World");

    }

    private void UpdateStatus(string message)
    {
        Debug.Log(message);
        //SocketStatusText.text = $"{message}";
    }

    //void OnDestroy()
    //{
    //    if (client != null && client.Connected)
    //    {
    //        client.Close();
    //        UpdateStatus("Socket已关闭");
    //    }
    //}
}