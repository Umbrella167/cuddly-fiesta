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

// ����ע��Ľӿڣ�����һ��Send����
public interface ISender
{
    void Send(byte[] message);
}

public class ConnectHandler : MonoBehaviour
{
    // ����ע���ʵ��
    private ISender _sender;

    // ͨ�����캯��ע������
    public ConnectHandler(ISender sender)
    {
        _sender = sender;
    }

    // ������������������ע�����Send����
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
            Debug.Log($"������ {message.Length} �ֽڵ�����");
        }
        catch (Exception ex)
        {
            Debug.Log($"��������ʧ��: {ex.Message}");
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
            Debug.Log($"������ {message.Length} �ֽڵ�����");
        }
        catch (Exception ex)
        {
            Debug.Log($"��������ʧ��: {ex.Message}");
        }
    }
}