using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net.Sockets;
using System.IO;
using TMPro;
using System;
using System.Linq;
using static packet;
using System.Net;

public class Connect_Server : MonoBehaviour
{
    public TMP_InputField IP_Input; // ����Ϊ�����
    public TMP_Dropdown TeamDropdown; // ����Ϊ�����˵�
    public TMP_InputField RobotId_Input, Port_Input;
    public TextMeshProUGUI SocketStatusText;

    static public string ipAddress = "127.0.0.1"; // Default IP
    static public int port = 114514; // Default Port
    static public int robotID = 0;
    static public string team = "Blue";
    static public RadioPacket packet = null;
    private TcpClient client;
    private NetworkStream stream;

    void Start()
    {
        GameObject.Find("Connect_Socket_Button").GetComponent<Button>().onClick.AddListener(ButtonOnClickEvent);
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
        team = TeamDropdown.options[TeamDropdown.value].text;
        // ��֤����
        if (!int.TryParse(RobotId_Input.text, out robotID))
        {
            UpdateStatus("��Ч�Ļ�����ID");
            return;
        }

        if (!int.TryParse(Port_Input.text, out port))
        {
            UpdateStatus("��Ч�Ķ˿�ֵ");
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
        SceneManager.LoadScene("World");

    }

    private void UpdateStatus(string message)
    {
        Debug.Log(message);
        //SocketStatusText.text = $"{message}";
    }

    void OnDestroy()
    {
        if (client != null && client.Connected)
        {
            client.Close();
            UpdateStatus("Socket�ѹر�");
        }
    }
}