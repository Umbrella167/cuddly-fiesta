using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Packet;

public class Control_Sim : MonoBehaviour
{
    static int control_robot_id = Connect_Gate.robotID;
    static int control_frequency = Connect_Gate.frequency;
    static string control_team = Connect_Gate.team;
    static public GameObject targetObj;
    static public float selfVx = 0;
    static public float selfVy = 0;
    static public float selfVr = 0;
    static public float selfPower = 0;
    static public float maxRotationOutput = 500f; // 最大旋转输出值
    static public PIDRotation pid = new PIDRotation();
    static public RadioPacket[] packet = new RadioPacket[16];
    public void Awake()
    {
        for (int i = 0; i < packet.Length; i++)
        {
            packet[i] = new RadioPacket(control_frequency); // 或者使用不同的参数，根据你的需求
            packet[i].robotID = i;
        }
        pid.P = 3.5f;
        pid.I = 0.03f;
        pid.D = 0.1f;
        System.Threading.Thread.Sleep(1000);
        targetObj = Vision.mouseObj;
    }

    // Update is called once per frame
     public void Update()
    {
        if (Param.GAME_MODE == Param.SIMULATE) 
        {
            resetPacket();
            ProcessInput();
            packet[control_robot_id].velR = Control_Utils.RotateTowardsTarget(Vision.selfRobot, targetObj.transform.position, pid);
        }


    }
    public void ProcessInput()
    {

        // Movement controls
        if (Input.GetKey(KeyCode.D))
        {
            selfVx = 50;
        }
        if (Input.GetKey(KeyCode.A))
        {
            selfVx = -50;
        }
        if (Input.GetKey(KeyCode.W))
        {
            selfVy = -50;
        }
        if (Input.GetKey(KeyCode.S))
        {
            selfVy = 50;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            selfVx = selfVx != 0 ? Math.Sign(selfVx) * 255 : 0;
            selfVy = selfVy != 0 ? Math.Sign(selfVy) * 255 : 0;
        }
        if (Input.GetMouseButton(1))
        {
            packet[control_robot_id].ctrl = true;
        }
        if (Input.GetMouseButton(0))
        {
            packet[control_robot_id].shootPowerLevel = Control_Utils.PowerSet((targetObj.transform.position - Vision.selfRobot.transform.position).magnitude);
            packet[control_robot_id].shoot = true;
        }
        packet[control_robot_id].velX = selfVx;
        packet[control_robot_id].velY = selfVy;
        

    }
    public void resetPacket()
    {
        packet[control_robot_id].resetPacket(control_robot_id, control_frequency);
        selfVx = 0;
        selfVy = 0;
    }
}
