using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEditor.Sprites;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static packet;
public class control : MonoBehaviour
{
    GameObject robot = null;
    int control_robot_id = Connect.robotID;
    int control_frequency = Connect.frequency;
    public RadioPacket packet = Connect.packet;
    public float globalVx = 0;
    public float globalVy = 0;
    public float globalVr = 0;
    // Start is called before the first frame update
    void Start()
    {
        System.Threading.Thread.Sleep(1000);
        robot = GameObject.Find(Connect.team + "_robot" + Connect.robotID.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        resetPacket();
        if (Input.GetKey(KeyCode.S)) 
        {
            globalVx = 50;
        }
        if (Input.GetKey(KeyCode.W))
        {
            globalVx = -50;
        }
        if (Input.GetKey(KeyCode.D))
        {
            globalVy = -50;
        }
        if (Input.GetKey(KeyCode.A))
        {
            globalVy = 50;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            packet.velR = 50;
        }
        if (Input.GetKey(KeyCode.E))
        {
            packet.velR = -50;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            globalVx = globalVx != 0 ? Math.Sign(globalVx) * 255 : 0;
            globalVy = globalVy != 0 ? Math.Sign(globalVy) * 255 : 0;

        }

        float[] localVelocities = GlobalToLocalVelocity(globalVx, globalVy);
        packet.velX = localVelocities[0];
        packet.velY = localVelocities[1];
        Debug.Log($"{packet.velX},{packet.velY}");
        packet.Encode();
        Connect.ser.Write(packet.transmitPacket, 0, Constants.TRANSMIT_PACKET_SIZE);
        Connect.ser.BaseStream.Flush();
        System.Threading.Thread.Sleep(1);


    }
    public void resetPacket()
    {
        packet.robotID = control_robot_id;
        packet.frequency = control_frequency;
        packet.velR = 0;
        packet.velX = 0;
        packet.velY = 0;
        globalVx = 0;
        globalVy = 0;
    }
    public float[] GlobalToLocalVelocity(float global_vx, float global_vy)
    {
        // 1. 获取机器人的全局旋转
        float theta_w = robot.transform.eulerAngles[1] / Mathf.Rad2Deg;
        Debug.Log(theta_w);
        float local_vx = global_vx * Mathf.Cos(theta_w) + global_vy * Mathf.Sin(theta_w);
        float local_vy = -global_vx * Mathf.Sin(theta_w) + global_vy * Mathf.Cos(theta_w);
        // 6. 返回局部速度数组
        return new float[] { local_vx, local_vy };
        //return new float[] { global_vx, global_vy };

    }

}
