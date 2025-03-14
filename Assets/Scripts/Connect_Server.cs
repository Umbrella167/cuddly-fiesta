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

    // ���Դ�Socket���Ӳ����سɹ�״̬
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
                UpdateStatus($"�ɹ����� {ipAddress}:{port}");
                return true;
            }
        }
        catch (Exception ex)
        {
            UpdateStatus($"����ʧ��: {ex.Message}");
        }
        return false;
    }

    public void ButtonOnClickEvent()
    {
        Connect_Gate.team = TeamDropdown.options[TeamDropdown.value].text;

        // ��֤����
        if (!int.TryParse(RobotId_Input.text, out Connect_Gate.robotID))
        {
            UpdateStatus("��Ч�Ļ�����ID");
            return;
        }

        if (!int.TryParse(Port_Input.text, out port))
        {
            UpdateStatus("��Ч�Ķ˿�ֵ");
            return;
        }

        if (!int.TryParse(Frequency_Input.text, out Connect_Gate.frequency))
        {
            UpdateStatus("��Ч��Ƶ��ֵ");
            return;
        }

        ipAddress = IP_Input.text;
        if (string.IsNullOrEmpty(ipAddress))
        {
            UpdateStatus("��������ЧIP��ַ");
            return;
        }

        // ���Դ�Socket
        if (!OpenSocket(ipAddress, port))
        {
            return;
        }
        Debug.Log($"���Բ���: {Connect_Gate.frequency}");
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
    //        UpdateStatus("Socket�ѹر�");
    //    }
    //}
}