using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using UnityEngine.Analytics;
using static ConnectHandler;
using System.IO;
using System;
using UnityEditor.PackageManager;
using System.Runtime.InteropServices.ComTypes;
using System.IO.Ports;
using static packet;

// 依赖注入的接口，定义一个Send方法
public interface ISender
{
    void Send(byte[] message);
}

public class ConnectHandler : MonoBehaviour
{
    // 依赖注入的实例
    private ISender _sender;

    // 通过构造函数注入依赖
    public ConnectHandler(ISender sender)
    {
        _sender = sender;
    }

    // 公共方法，调用依赖注入类的Send方法
    public void Send(byte[] message)
    {
        if (_sender != null)
        {
            _sender.Send(message);
        }
        else
        {
            Debug.LogError("Sender is null.  Make sure it's assigned.");
        }
    }
}

public class SocketHandler : ISender
{
    private TcpClient _client;
    //private NetworkStream _stream;
    public SocketHandler(TcpClient client)
    {
        _client = client;
        //_stream = client.GetStream();
    }

    public void Send(byte[] message)
    {
        try
        {
            //_stream.Write(message, 0, message.Length);
            //_stream.Flush();
            _client.GetStream().Write(message, 0, message.Length);
            Debug.Log($"发送了 {message.Length} 字节的数据");
        }
        catch (Exception ex)
        {
            Debug.Log($"发送数据失败: {ex.Message}");
        }
    }
}

public class SerialHandler : ISender
{
    private SerialPort _ser;
    public SerialHandler(SerialPort ser)
    {
        _ser = ser;
    }

    public void Send(byte[] message)
    {
        try
        {
            _ser.Write(message, 0, Constants.TRANSMIT_PACKET_SIZE);
            _ser.BaseStream.Flush();
            Debug.Log($"发送了 {message.Length} 字节的数据");
        }
        catch (Exception ex)
        {
            Debug.Log($"发送数据失败: {ex.Message}");
        }
    }
}