using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Vision; // ���� Vision ��ľ�̬��Ա������ֱ�ӷ��� Vision.player ��

public class ArtificalPotentialField : MonoBehaviour
{
    static public Vector3 APF(GameObject robot, Vector3 desiredTranslationalVel)
    {
        Vector3 totalRepulsionVelocity = Vector3.zero; // ��ʼ���ܳ����ٶ�Ӱ��
        Vector3 robotPos = robot.transform.position;

        // ��ȡ�����˵ĵ�ǰ�ٶ�
        VelocityTracker robotVelTracker = robot.GetComponent<VelocityTracker>();
        Vector3 robotVel = (robotVelTracker != null) ? robotVelTracker.velocity : Vector3.zero;

        // Ǳ�ڵ��ϰ����б�������һ����ˡ��з�������
        List<GameObject> obstacles = new List<GameObject>();

        // ��������
        float safetyDistance = 0.5f;  // ��ȫ����
        float repulsiveStrength = 10f; // ����ǿ��
        float maxRepulsiveVelocity = 2f; // �������ٶ�

        for (int i = 0; i < Param.MAX_PLAYER; i++)
        {
            // ����Ƿ���Ч����null�����Ҳ��ǻ���������
            if (Vision.player[i] != robot && Vision.isValid(i, "player"))
            {
                obstacles.Add(Vision.player[i]);
            }
            if (Vision.isValid(i, "enemy"))
            {
                obstacles.Add(Vision.enemy[i]);
            }
        }

        // ��������Ǳ�ڵ��ϰ���
        foreach (GameObject obstacle in obstacles)
        {
            Vector3 obstaclePos = obstacle.transform.position;
            float distance = Vector3.Distance(robotPos, obstaclePos);

            // ֻ�Ծ���Ͻ����ϰ����������
            if (distance < safetyDistance)
            {
                // ����������򣨴��ϰ���ָ������ˣ�
                Vector3 repulsiveDirection = (robotPos - obstaclePos).normalized;

                // ���������С������Խ��������Խ��
                float repulsiveMagnitude = repulsiveStrength * (1 / distance - 1 / safetyDistance);
                repulsiveMagnitude = Mathf.Clamp(repulsiveMagnitude, 0, maxRepulsiveVelocity);

                // ��������ٶ�
                Vector3 repulsiveVelocity = repulsiveDirection * repulsiveMagnitude;

                // ��Ҫ�ڴ�ֱ�������˶�����ķ����ϲ�������
                Vector3 desiredDir = desiredTranslationalVel.normalized;
                Vector3 perpDir = Vector3.Cross(desiredDir, Vector3.forward).normalized;
                float perpComponent = Vector3.Dot(repulsiveVelocity, perpDir);

                totalRepulsionVelocity += perpDir * perpComponent;
            }
        }
        Debug.Log(totalRepulsionVelocity);
        Vector3 finalTranslationalVelocity = desiredTranslationalVel + totalRepulsionVelocity;
        return finalTranslationalVelocity;
    }
}