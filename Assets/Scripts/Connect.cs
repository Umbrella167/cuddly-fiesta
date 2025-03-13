using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO.Ports;
using TMPro;
using System;
using System.Linq;
using static packet;


public class Connect : MonoBehaviour
{
    public TMP_Dropdown PortDropdown; // ����Ϊ�����˵�
    public TMP_Dropdown TeamDropdown; // ����Ϊ�����˵�
    public TMP_InputField RobotId_Input, Frequency_Input;
    public TextMeshProUGUI ConnectionStatusText;

    public int baudRate = 115200;
    public Parity parity = Parity.None;
    public int dataBits = 8;
    public StopBits stopBits = StopBits.One;

    static public SerialPort ser = null;
    static public int robotID = 0;
    static public int frequency = 0;
    static public string team = "Blue";
    static public RadioPacket packet = null;
    void Start()
    {
        PopulatePortDropdown();
        GameObject.Find("Connect_Button").GetComponent<Button>().onClick.AddListener(ButtonOnClickEvent);

    }

    // ɨ����ô��ڲ���������˵�
    private void PopulatePortDropdown()
    {
        PortDropdown.ClearOptions();
        string[] ports = SerialPort.GetPortNames();
        PortDropdown.AddOptions(ports.ToList());

        if (ports.Length > 0)
        {
            PortDropdown.value = 0;
            UpdateStatus($"Status: Find {ports.Length} Port");
        }
        else
        {
            UpdateStatus("Status: Not find port");
        }
    }

    // ���Դ򿪴��ڲ����سɹ�״̬
    private bool OpenPort(string portName)
    {
        try
        {
            if (ser != null && ser.IsOpen)
            {
                ser.Close();
            }

            ser = new SerialPort(portName, baudRate, parity, dataBits, stopBits)
            {
                ReadTimeout = 500,
                WriteTimeout = 500 // ���д�볬ʱ����
            };

            ser.Open();
            if (ser.IsOpen)
            {
                UpdateStatus($"�ɹ����� {portName}");
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

        if (!int.TryParse(Frequency_Input.text, out frequency))
        {
            UpdateStatus("��Ч��Ƶ��ֵ");
            return;
        }

        // ��ȡѡ��Ķ˿�
        string selectedPort = PortDropdown.options[PortDropdown.value].text;
        if (string.IsNullOrEmpty(selectedPort))
        {
            UpdateStatus("��ѡ����Ч�˿�");
            return;
        }

        // ���Դ򿪶˿�
        if (!OpenPort(selectedPort))
        {

            return;

        }
        // ׼������������

        packet = new RadioPacket(frequency);
        ser.Write(packet.start_packet1, 0, packet.start_packet1.Length);
        ser.BaseStream.Flush();
        UpdateStatus($"�����ѷ����� {selectedPort}");

        float start_time = Time.time;
        while (Time.time - start_time < 2) 
        {
            if (ser.IsOpen && ser.BytesToRead > 0)
            {
                ser.ReadExisting();
                break;
            }
            System.Threading.Thread.Sleep(10);
        }
        System.Threading.Thread.Sleep(2000);
        ser.Write(packet.start_packet2, 0, packet.start_packet2.Length);
        ser.BaseStream.Flush();

        SceneManager.LoadScene("World");

    }

    private void UpdateStatus(string message)
    {
        Debug.Log(message);
        ConnectionStatusText.text = $"״̬: {message}";
        ConnectionStatusText.color = message.StartsWith("�ɹ�") ? Color.green : Color.red;
    }

    void OnDestroy()
    {
        //if (ser != null && ser.IsOpen)
        //{
        //    ser.Close();
        //    UpdateStatus("�����ѹر�");
        //}
    }
}