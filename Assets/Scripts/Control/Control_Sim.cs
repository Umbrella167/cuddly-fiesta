﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Packet;
using static UnityEngine.GraphicsBuffer;

public class Control_Sim : MonoBehaviour
{
    public GameObject PowerRageBoundary;
    static public float targetVx = 0;
    static public float targetVy = 0;
    static public float acceleration = 80f;   // 加速度（单位/秒²）
    static public float deceleration = 400f;   // 减速度（单位/秒²）

    static int control_robot_id = Connect_Gate.robotID;
    static int control_frequency = Connect_Gate.frequency;
    static string control_team = Connect_Gate.team;
    static public GameObject targetObj;
    static public GameObject nearMouseObj;
    static public float selfVx = 0;
    static public float selfVy = 0;
    static public float selfVr = 0;
    static public float selfPower = 0;
    static public float maxRotationOutput = 500f; // 最大旋转输出值
    static public PIDRotation pid = new PIDRotation();
    static public RadioPacket[] packet = new RadioPacket[16];
    RuntimeLineRenderer line;


    
    public void Awake()
    {
        if (Param.GAME_MODE != Param.SIMULATE) return;

        for (int i = 0; i < packet.Length; i++)
        {
            packet[i] = new RadioPacket(control_frequency); // 或者使用不同的参数，根据你的需求
            packet[i].robotID = i;
        }
        pid.P = 7.85f;
        pid.I = 0.03f;
        pid.D = 0.5f;
        System.Threading.Thread.Sleep(100);
        targetObj = Vision.mouseObj;
        line = gameObject.AddComponent<RuntimeLineRenderer>();

    }

    // Update is called once per frame
    public void Update()
    {
        if (Param.GAME_MODE != Param.SIMULATE) return;
        resetPacket();
        ProcessInput();
        autoShoot();
        auto_unlock_ball();

        float deltaTime = Time.deltaTime;
        UpdateVelocity(ref selfVx, targetVx, deltaTime);
        UpdateVelocity(ref selfVy, targetVy, deltaTime);

        line.UpdateExtendedLineWithAngle(Vision.selfRobot.transform.position + Vector3.up * 0.01f, Vision.selfRobot.transform.eulerAngles.y, 20f);
        packet[control_robot_id].velR = Control_Utils.RotateTowardsTarget(Vision.selfRobot, targetObj.transform.position, pid,Param.SIMULATE);
        packet[control_robot_id].velX = selfVx;
        packet[control_robot_id].velY = selfVy;

    }

    public void auto_unlock_ball() 
    {
        if (Vector3.Distance(Vision.ball.transform.position, Vision.selfRobot.transform.position) <= Param.DRIBBLE_BALL_DISTANCE) 
        {
            targetObj = Vision.mouseObj;
        }

    }

    
    private void UpdateVelocity(ref float current, float target, float deltaTime)
    {
        if (Mathf.Approximately(target, 0))
        {
            // 当没有输入时使用减速度
            current = Mathf.MoveTowards(current, 0, deceleration * deltaTime);
        }
        else
        {
            // 根据目标方向使用加速度
            float accelerateDirection = Mathf.Sign(target);
            if (Mathf.Approximately(current, 0) || Mathf.Sign(current) == accelerateDirection)
            {
                // 同方向加速
                current = Mathf.MoveTowards(current, target, acceleration * deltaTime);
            }
            else
            {
                // 反向时先快速减速
                current = Mathf.MoveTowards(current, 0, deceleration * 2 * deltaTime);
            }
        }
    }
    public void autoShoot() 
    {
        float distance = Vector3.Distance(Vision.ball.transform.position, PowerRageBoundary.transform.position);
        if (distance > 8.2)
        {
            packet[control_robot_id].shootMode = false;
            packet[control_robot_id].shootPowerLevel = Control_Utils.PowerSet(2);
            packet[control_robot_id].shoot = true;
        }

    }
    public void ProcessInput()
    {
        // 初始化目标速度为0
        targetVx = 0;
        targetVy = 0;

        // 键盘输入处理
        if (Input.GetKey(KeyCode.D)) targetVx = Param.NROMAL_SPEED;
        if (Input.GetKey(KeyCode.A)) targetVx = -Param.NROMAL_SPEED;
        if (Input.GetKey(KeyCode.W)) targetVy = -Param.NROMAL_SPEED;
        if (Input.GetKey(KeyCode.S)) targetVy = Param.NROMAL_SPEED;

        // 速度模式切换
        if (Input.GetKey(KeyCode.LeftShift))
        {
            targetVx = targetVx != 0 ? Mathf.Sign(targetVx) * Param.MAX_SPEED : 0;
            targetVy = targetVy != 0 ? Mathf.Sign(targetVy) * Param.MAX_SPEED : 0;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            targetVx = targetVx != 0 ? Mathf.Sign(targetVx) * Param.SLOW_SPEED : 0;
            targetVy = targetVy != 0 ? Mathf.Sign(targetVy) * Param.SLOW_SPEED : 0;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            targetVy = -1 * targetVy;
            packet[control_robot_id].useGlobleVel = false;
        
        }
            
        if (Input.GetMouseButton(1))
        {
            packet[control_robot_id].ctrl = true;
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButton(0))
        {

            packet[control_robot_id].shootMode = true;
            packet[control_robot_id].shootPowerLevel = Control_Utils.PowerSet((targetObj.transform.position - Vision.selfRobot.transform.position).magnitude);
            packet[control_robot_id].shoot = true;
        }
        else if (Input.GetMouseButton(0)) 
        {
            packet[control_robot_id].shootMode = false;
            packet[control_robot_id].shootPowerLevel = Control_Utils.PowerSet((targetObj.transform.position - Vision.selfRobot.transform.position).magnitude);
            packet[control_robot_id].shoot = true;

        }
        if (Input.GetMouseButton(2)) 
        {
            nearMouseObj = Vision.FindNearestObjectInRange(Vision.mouseObj.transform.position, 2f);
            nearMouseObj = nearMouseObj == null ? Vision.mouseObj : nearMouseObj;
            targetObj = nearMouseObj;
        }

        packet[control_robot_id].velX = selfVx;
        packet[control_robot_id].velY = selfVy;
        

    }
    public void resetPacket()
    {
        packet[control_robot_id].resetPacket(control_robot_id, control_frequency);

        //selfVx = 0;
        //selfVy = 0;
    }
}
