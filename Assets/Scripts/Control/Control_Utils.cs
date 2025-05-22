using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Control_Utils : MonoBehaviour
{

    static public float RotateTowardsTarget(GameObject robot, UnityEngine.Vector3 target_pos,PIDRotation pid ,string game_mode,float maxRotationOutput = 500)
    {
        Vector3 toTarget = game_mode == Param.SIMULATE ? - (robot.transform.position - target_pos): robot.transform.position - target_pos; 
        toTarget.y = 0;
        if (toTarget.sqrMagnitude < 0.001f) return 0;
        toTarget.Normalize();

        Vector3 robotForward = game_mode == Param.SIMULATE?robot.transform.rotation * Vector3.back: robot.transform.forward;
        robotForward.Normalize();

        float angleDiff = Vector3.SignedAngle(robotForward, toTarget, Vector3.up);
        angleDiff = game_mode == Param.SIMULATE ?-1* angleDiff: angleDiff;
        float rotationOutput = pid.Compute(angleDiff, Time.deltaTime);

        rotationOutput = Mathf.Clamp(rotationOutput, -maxRotationOutput, maxRotationOutput);

        robotForward = game_mode == Param.SIMULATE ? robot.transform.rotation * Vector3.back : robot.transform.forward;
        robotForward.Normalize();
        angleDiff = Vector3.SignedAngle(robotForward, toTarget, Vector3.up);
        if (Mathf.Abs(angleDiff) < 0.5f) return 0;
        return rotationOutput;
    }


    static public float[] GlobalToLocalVelocity(GameObject robot, float global_vx, float global_vy,bool is_use = true,bool is_real = true)
    {

        if (!is_use) return new float[] { global_vx, global_vy };
        float local_vx = global_vx;
        float local_vy = global_vy;
        // 1. ��ȡ�����˵�ȫ����ת
        float theta_w = robot.transform.eulerAngles[1] * Mathf.Deg2Rad; // Convert to radians
        if (is_real)
        {
            local_vx = global_vx * Mathf.Cos(theta_w) + global_vy * Mathf.Sin(theta_w);
            local_vy = -global_vx * Mathf.Sin(theta_w) + global_vy * Mathf.Cos(theta_w);
        }
        else
        {
            local_vx = global_vx * Mathf.Cos(-theta_w) + global_vy * Mathf.Sin(-theta_w);
            local_vy = -global_vx * Mathf.Sin(-theta_w) + global_vy * Mathf.Cos(-theta_w);
        }

            // 6. ���ؾֲ��ٶ�����
       return new float[] { local_vx, local_vy };
    }
    static public float[] GlobalToLocalVelocityNew(GameObject robot, float global_vx, float global_vy, bool is_use = true)
    {
        if (robot == null)
        {
            Debug.LogError("GlobalToLocalVelocityNew: Robot GameObject is null. Returning global velocities directly.");
            return new float[] { global_vx, global_vy };
        }

        // ��ȡ�����˵�ȫ����ת (Y����ת)
        float theta_w = robot.transform.eulerAngles.y * Mathf.Deg2Rad; // Convert to radians

        // ��ת����Ӧ�����ٶ�����
        float local_vx = global_vx * Mathf.Cos(theta_w) + global_vy * Mathf.Sin(theta_w);
        float local_vy = -global_vx * Mathf.Sin(theta_w) + global_vy * Mathf.Cos(theta_w);

        // ���ؾֲ��ٶ�����
        return new float[] { local_vx, local_vy };
    }

    static public float PowerSet(float dist, float rate = 0.018f, float min = 3, float max = 165)
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