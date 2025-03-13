using Microsoft.Win32;
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
    public GameObject targetObj;
    public RadioPacket packet = Connect.packet;
    public float globalVx = 0;
    public float globalVy = 0;
    public float globalVr = 0;
    public float power = 0;
    // Start is called before the first frame update

    public float maxRotationOutput = 500f; // 最大旋转输出值
    public PIDRotation pid = new PIDRotation();

    void Start()
    {
        pid.P = 3f;
        pid.I = 0.001f;
        pid.D = 0f;
        System.Threading.Thread.Sleep(1000);
        robot = GameObject.Find(Connect.team + "_robot" + Connect.robotID.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        resetPacket();

        // Movement controls
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
            packet.velR = 500;
        }
        if (Input.GetKey(KeyCode.E))
        {
            packet.velR = -500;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            globalVx = globalVx != 0 ? Math.Sign(globalVx) * 255 : 0;
            globalVy = globalVy != 0 ? Math.Sign(globalVy) * 255 : 0;
        }
        if (Input.GetMouseButton(1))
        {
            packet.ctrl = true;
        }
        // Rotation towards targetPos
        
        if (Input.GetMouseButton(0))
        {
            packet.shootPowerLevel = PowerSet((targetObj.transform.position - robot.transform.position).magnitude);
            packet.shoot = true;
        }
        Debug.Log(packet.shootPowerLevel);
        float[] localVelocities = GlobalToLocalVelocity(globalVx, globalVy);
        packet.velX = localVelocities[0];
        packet.velY = localVelocities[1];
        packet.velR = RotateTowardsTarget();

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
        packet.ctrl = false;
        packet.shoot = false;
    }

    public float[] GlobalToLocalVelocity(float global_vx, float global_vy)
    {
        // 1. 获取机器人的全局旋转
        float theta_w = robot.transform.eulerAngles[1] * Mathf.Deg2Rad; // Convert to radians
        float local_vx = global_vx * Mathf.Cos(theta_w) + global_vy * Mathf.Sin(theta_w);
        float local_vy = -global_vx * Mathf.Sin(theta_w) + global_vy * Mathf.Cos(theta_w);
        // 6. 返回局部速度数组
        return new float[] { local_vx, local_vy };
    }

    float RotateTowardsTarget()
    {
        if (targetObj == null || robot == null) return 0;

        Vector3 toTarget = robot.transform.position - targetObj.transform.position; // 注意：目标 - 机器人
        toTarget.y = 0;

        if (toTarget.sqrMagnitude < 0.001f) return 0;

        toTarget.Normalize();
        Vector3 robotForward = robot.transform.forward;
        robotForward.Normalize();

        // 计算带符号的角度差（范围[-180, 180]）
        float angleDiff = Vector3.SignedAngle(robotForward, toTarget, Vector3.up);

        // 使用PID控制器计算旋转速度
        float rotationOutput = pid.Compute(angleDiff, Time.deltaTime);

        // 限制旋转输出
        rotationOutput = Mathf.Clamp(rotationOutput, -maxRotationOutput, maxRotationOutput);

        // 应用旋转
        robot.transform.Rotate(Vector3.up, rotationOutput * Time.deltaTime);

        // 重新计算当前角度差（用于死区判断）
        robotForward = robot.transform.forward;
        robotForward.Normalize();
        angleDiff = Vector3.SignedAngle(robotForward, toTarget, Vector3.up);

        // 添加死区（避免微小抖动）
        if (Mathf.Abs(angleDiff) < 0.5f) return 0;

        return rotationOutput;
    }

    public float PowerSet(float dist, float rate = 0.05f, float min = 5, float max = 150)
    {

        // 检查输入参数的有效性
        if (dist <= 0 || min >= max || rate <= 0)
        {
            return min; // 或者返回一个错误值，例如 NaN
        }

        // 计算比例因子，确保结果在 [0, 1] 范围内
        float proportion = 1.0f - (float)Math.Exp(-dist * rate); // 使用指数函数，确保比例在 0 到 1 之间
        proportion = Math.Max(0.0f, Math.Min(1.0f, proportion)); // 限制比例在 [0, 1] 范围内

        // 使用比例因子在最小值和最大值之间进行插值
        return min + proportion * (max - min);
    }


}