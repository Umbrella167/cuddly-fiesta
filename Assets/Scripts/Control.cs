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
    public float rotationSpeed = 255f; // Adjust this value to control rotation speed
    public float power = 0;
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
        if (Input.GetMouseButton(1))
        {
            packet.ctrl = true;
        }
        // Rotation towards targetPos
        
        power = (robot.transform.position - targetObj.transform.position).magnitude;
        Debug.Log(power);
        float[] localVelocities = GlobalToLocalVelocity(globalVx, globalVy);
        packet.velX = localVelocities[0];
        packet.velY = localVelocities[1];
        packet.velR = RotateTowardsTarget();
        Debug.Log(packet.velR);
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
    }

    public float[] GlobalToLocalVelocity(float global_vx, float global_vy)
    {
        // 1. ��ȡ�����˵�ȫ����ת
        float theta_w = robot.transform.eulerAngles[1] * Mathf.Deg2Rad; // Convert to radians
        float local_vx = global_vx * Mathf.Cos(theta_w) + global_vy * Mathf.Sin(theta_w);
        float local_vy = -global_vx * Mathf.Sin(theta_w) + global_vy * Mathf.Cos(theta_w);
        // 6. ���ؾֲ��ٶ�����
        return new float[] { local_vx, local_vy };
    }

    float RotateTowardsTarget()
    {
        if (targetObj == null || robot == null) return 0;

        Vector3 toTarget = robot.transform.position - targetObj.transform.position;
        toTarget.y = 0; // ���Դ�ֱ����

        // ���������������Ŀ�������ǰ��/���󷽣�
        if (toTarget.sqrMagnitude < 0.001f) return 0;

        // ��׼�����򲢻�ȡ��ǰ����
        Vector3 robotForward = robot.transform.forward;
        toTarget.Normalize();
        robotForward.Normalize();

        // ��������ŵĽǶȲ��Χ[-180, 180]��
        float angleDiff = Vector3.SignedAngle(robotForward, toTarget, Vector3.up);

        // ��̬������ת�ٶȣ��Ƕ�Խ��ת��Խ�죩
        float dynamicSpeed = Mathf.Lerp(
            0.5f * rotationSpeed,
            rotationSpeed,
            Mathf.Clamp01(Mathf.Abs(angleDiff) / 20f) // 45���ڿ�ʼ����
        );

        // Ӧ����ת��ʹ�����·����
        Quaternion targetRot = Quaternion.LookRotation(toTarget);
        robot.transform.rotation = Quaternion.RotateTowards(
            robot.transform.rotation,
            targetRot,
            dynamicSpeed * Time.deltaTime
        );

        // ���¼��㵱ǰ�ǶȲ�����µ���ת״̬��
        angleDiff = Vector3.SignedAngle(robot.transform.forward, toTarget, Vector3.up);

        // �������������΢С������
        if (Mathf.Abs(angleDiff) < 0.5f) return 0;

        // �����ٶȣ�������ı������ƣ�
        float velR = Mathf.Clamp(
            angleDiff * 5f, // ����ϵ���ɸ�����Ҫ����
            -255f,
            255f
        );

        return velR;
    }
}