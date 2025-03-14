using System;
using System.Linq;
using UnityEngine;
using static packet;

public class control : MonoBehaviour
{
    GameObject robot = null;
    int control_robot_id = Connect_Gate.robotID;
    int control_frequency = Connect_Gate.frequency;
    string control_team = Connect_Gate.team;
    public GameObject targetObj;
    //static public RadioPacket packet = Connect_Gate.packet;
    static public RadioPacket[] packet = new RadioPacket[16];

    public float selfVx = 0;
    public float selfVy = 0;
    public float selfVr = 0;
    public float selfPower = 0;


    // Start is called before the first frame update


    public float maxRotationOutput = 500f; // �����ת���ֵ
    public PIDRotation pid = new PIDRotation();


    void Start()
    {
        for (int i = 0; i < packet.Length; i++)
        {
            packet[i] = new RadioPacket(control_frequency); // ����ʹ�ò�ͬ�Ĳ����������������
            packet[i].robotID = i;
        }

        pid.P = 3f;
        pid.I = 0.001f;
        pid.D = 0f;
        System.Threading.Thread.Sleep(1000);
        robot = GameObject.Find(control_team + "_robot" + control_robot_id.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        resetPacket();

        // Movement controls
        if (Input.GetKey(KeyCode.S))
        {
            selfVx = 50;
        }
        if (Input.GetKey(KeyCode.W))
        {
            selfVx = -50;
        }
        if (Input.GetKey(KeyCode.D))
        {
            selfVy = -50;
        }
        if (Input.GetKey(KeyCode.A))
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
        // Rotation towards targetPos
        
        if (Input.GetMouseButton(0))
        {
            packet[control_robot_id].shootPowerLevel = PowerSet((targetObj.transform.position - robot.transform.position).magnitude);
            packet[control_robot_id].shoot = true;
        }
        float[] localVelocities = GlobalToLocalVelocity(selfVx, selfVy);
        packet[control_robot_id].velX = localVelocities[0];
        packet[control_robot_id].velY = localVelocities[1];
        packet[control_robot_id].velR = RotateTowardsTarget();
        packet[control_robot_id].Encode();
        
    }

    public void resetPacket()
    {
        packet[control_robot_id].robotID = control_robot_id;
        packet[control_robot_id].frequency = control_frequency;
        packet[control_robot_id].velR = 0;
        packet[control_robot_id].velX = 0;
        packet[control_robot_id].velY = 0;
        selfVx = 0;
        selfVy = 0;
        packet[control_robot_id].ctrl = false;
        packet[control_robot_id].shoot = false;
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

        Vector3 toTarget = robot.transform.position - targetObj.transform.position; // ע�⣺Ŀ�� - ������
        toTarget.y = 0;

        if (toTarget.sqrMagnitude < 0.001f) return 0;

        toTarget.Normalize();
        Vector3 robotForward = robot.transform.forward;
        robotForward.Normalize();

        // ��������ŵĽǶȲ��Χ[-180, 180]��
        float angleDiff = Vector3.SignedAngle(robotForward, toTarget, Vector3.up);

        // ʹ��PID������������ת�ٶ�
        float rotationOutput = pid.Compute(angleDiff, Time.deltaTime);

        // ������ת���
        rotationOutput = Mathf.Clamp(rotationOutput, -maxRotationOutput, maxRotationOutput);

        // Ӧ����ת
        robot.transform.Rotate(Vector3.up, rotationOutput * Time.deltaTime);

        // ���¼��㵱ǰ�ǶȲ���������жϣ�
        robotForward = robot.transform.forward;
        robotForward.Normalize();
        angleDiff = Vector3.SignedAngle(robotForward, toTarget, Vector3.up);

        // �������������΢С������
        if (Mathf.Abs(angleDiff) < 0.5f) return 0;

        return rotationOutput;
    }

    public float PowerSet(float dist, float rate = 0.05f, float min = 5, float max = 150)
    {

        // ��������������Ч��
        if (dist <= 0 || min >= max || rate <= 0)
        {
            return min; // ���߷���һ������ֵ������ NaN
        }

        // ����������ӣ�ȷ������� [0, 1] ��Χ��
        float proportion = 1.0f - (float)Math.Exp(-dist * rate); // ʹ��ָ��������ȷ�������� 0 �� 1 ֮��
        proportion = Math.Max(0.0f, Math.Min(1.0f, proportion)); // ���Ʊ����� [0, 1] ��Χ��

        // ʹ�ñ�����������Сֵ�����ֵ֮����в�ֵ
        return min + proportion * (max - min);
    }


    static public void send_all_packet_server()
    {

        for (int i = 0; i < packet.Length; i++)
        {
            bool areEqual = packet[i].transmitPacket.SequenceEqual(new byte[Constants.TRANSMIT_PACKET_SIZE]);
            if (!areEqual)
            {
                //Debug.Log($"{packet[i].transmitPacket}");
                Connect.ser.Write(packet[i].transmitPacket, 0, packet[i].transmitPacket.Length);
                Connect.ser.BaseStream.Flush();
                System.Threading.Thread.Sleep(1);
            }
        }
    }
}