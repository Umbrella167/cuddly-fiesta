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
    public TMP_Dropdown PortDropdown; // 更改为下拉菜单
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

    // 扫描可用串口并填充下拉菜单
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

    // 尝试打开串口并返回成功状态
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
                WriteTimeout = 500 // 添加写入超时设置
            };

            ser.Open();
            if (ser.IsOpen)
            {
                UpdateStatus($"成功连接 {portName}");
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

        if (!int.TryParse(Frequency_Input.text, out Connect_Gate.frequency))
        {
            UpdateStatus("无效的频率值");
            return;
        }

        // 获取选择的端口
        string selectedPort = PortDropdown.options[PortDropdown.value].text;
        if (string.IsNullOrEmpty(selectedPort))
        {
            UpdateStatus("请选择有效端口");
            return;
        }

        // 尝试打开端口
        if (!OpenPort(selectedPort))
        {

            return;

        }
        // 准备并发送数据
        Connect_Gate.packet = new RadioPacket(Connect_Gate.frequency);
        ser.Write(Connect_Gate.packet.start_packet1, 0, Connect_Gate.packet.start_packet1.Length);
        ser.BaseStream.Flush();
        UpdateStatus($"数据已发送至 {selectedPort}");

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
        //    UpdateStatus("串口已关闭");
        //}
    }
}