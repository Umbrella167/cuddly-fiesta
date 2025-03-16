using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Control_Utils : MonoBehaviour
{

    static public float RotateTowardsTarget(GameObject robot, UnityEngine.Vector3 target_pos,PIDRotation pid ,float maxRotationOutput = 500)
    {


        Vector3 toTarget = -(robot.transform.position - target_pos); 
        toTarget.y = 0;
        if (toTarget.sqrMagnitude < 0.001f) return 0;
        toTarget.Normalize();
        Vector3 robotForward = robot.transform.rotation * Vector3.back;

        robotForward.Normalize();

        float angleDiff = Vector3.SignedAngle(robotForward, toTarget, Vector3.up);
        angleDiff = -1* angleDiff;
        float rotationOutput = pid.Compute(angleDiff, Time.deltaTime);

        rotationOutput = Mathf.Clamp(rotationOutput, -maxRotationOutput, maxRotationOutput);

        robotForward = robot.transform.forward;
        robotForward.Normalize();
        angleDiff = Vector3.SignedAngle(robotForward, toTarget, Vector3.up);
        if (Mathf.Abs(angleDiff) < 0.5f) return 0;

        return rotationOutput;
    }


    static public float[] GlobalToLocalVelocity(GameObject robot, float global_vx, float global_vy)
    {
        // 1. ��ȡ�����˵�ȫ����ת
        float theta_w = robot.transform.eulerAngles[1] * Mathf.Deg2Rad; // Convert to radians
        float local_vx = global_vx * Mathf.Cos(theta_w) + global_vy * Mathf.Sin(theta_w);
        float local_vy = -global_vx * Mathf.Sin(theta_w) + global_vy * Mathf.Cos(theta_w);
        // 6. ���ؾֲ��ٶ�����
        return new float[] { local_vx, local_vy };
    }

    static public float PowerSet(float dist, float rate = 0.05f, float min = 5, float max = 150)
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

}