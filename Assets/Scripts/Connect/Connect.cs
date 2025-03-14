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
    public TMP_Dropdown TeamDropdown;
    public TMP_InputField RobotId_Input, Frequency_Input;
    public TextMeshProUGUI SerialStatusText;
    public TMP_Dropdown PortDropdown; // ����Ϊ�����˵�
    public Button Connect_Button;



    public int baudRate = 115200;
    public Parity parity = Parity.None;
    public int dataBits = 8;
    public StopBits stopBits = StopBits.One;

    static public SerialPort ser = null;
    
    void Start()
    {
        PopulatePortDropdown();
        Connect_Button.onClick.AddListener(ButtonOnClickEvent);
        //GameObject.Find("Connect_Button").GetComponent<Button>().onClick.AddListener(ButtonOnClickEvent);

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
        Connect_Gate.team = TeamDropdown.options[TeamDropdown.value].text;
        // ��֤����
        if (!int.TryParse(RobotId_Input.text, out Connect_Gate.robotID))
        {
            UpdateStatus("��Ч�Ļ�����ID");
            return;
        }

        if (!int.TryParse(Frequency_Input.text, out Connect_Gate.frequency))
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
        Connect_Gate.packet = new RadioPacket(Connect_Gate.frequency);
        ser.Write(Connect_Gate.packet.start_packet1, 0, Connect_Gate.packet.start_packet1.Length);
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
        ser.Write(Connect_Gate.packet.start_packet2, 0, Connect_Gate.packet.start_packet2.Length);
        ser.BaseStream.Flush();

        SceneManager.LoadScene("World");

    }

    private void UpdateStatus(string message)
    {
        Debug.Log(message);
        SerialStatusText.text = $"{message}";
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