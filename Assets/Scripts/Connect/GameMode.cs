using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public UdpNetwork network;
    void Start()
    {
        network = GetComponent<UdpNetwork>();
        if (Connect_Gate.GAME_MODE == "Serial")
        {
            
            network.networkMode = UdpNetwork.Mode.Server;
            // ��ӿ����ü��
            if (network == null)
            {
                Debug.LogError("UdpNetwork component not found!");
                return;
            }
            network.OnMessageReceived += (message, ip) =>
            {
                int num = ReverseRealNum(message[1], message[2]);
                control.packet[num].transmitPacket = message;
                Debug.Log($"{num}");
            };
        }
        else if (Connect_Gate.GAME_MODE == "Client") 
        {
            UdpNetwork network = GetComponent<UdpNetwork>();
            network.networkMode = UdpNetwork.Mode.Client;
            network.serverIP = Connect_Gate.IP;
            network.serverPort = Connect_Gate.Port; 

            if (network == null)
            {
                Debug.LogError("UdpNetwork component not found!");
                return;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Connect_Gate.GAME_MODE == "Serial") 
        {
            control.send_all_packet_server();
        }
        else if (Connect_Gate.GAME_MODE == "Client")
        {
            network.Send(control.packet[Connect_Gate.robotID].transmitPacket);
            System.Threading.Thread.Sleep(3);
        }
    }

    public static int ReverseRealNum(byte txbuff1, byte txbuff2)
    {
        // ��� TXBuff[1] �Ƿ�����λ
        for (int bitPos = 0; bitPos < 8; bitPos++)
        {
            int mask = 0x01 << bitPos;
            if ((txbuff1 & mask) != 0)
            {
                // ������λ����� TXBuff[2] �Ƿ�Ϊ 0
                if (txbuff2 != 0)
                {
                    return 0; // ��ͻ���޷�Ψһȷ��
                }
                return 8 + bitPos; // real_num = 8 + λλ��
            }
        }

        // ��� TXBuff[2] �Ƿ�����λ
        for (int bitPos = 0; bitPos < 8; bitPos++)
        {
            int mask = 0x01 << bitPos;
            if ((txbuff2 & mask) != 0)
            {
                // ������λ����� TXBuff[1] �Ƿ�Ϊ 0
                if (txbuff1 != 0)
                {
                    return 0; // ��ͻ���޷�Ψһȷ��
                }
                return bitPos; // real_num = λλ��
            }
        }

        return 0;
    }
}
