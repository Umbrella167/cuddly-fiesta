using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Vision; // 导入 Vision 类的静态成员，方便直接访问 Vision.player 等

public class ArtificalPotentialField : MonoBehaviour
{
    static public Vector3 APF(GameObject robot, Vector3 desiredTranslationalVel)
    {
        Vector3 totalRepulsionVelocity = Vector3.zero; // 初始化总斥力速度影响
        Vector3 robotPos = robot.transform.position;

        // 获取机器人的当前速度
        VelocityTracker robotVelTracker = robot.GetComponent<VelocityTracker>();
        Vector3 robotVel = (robotVelTracker != null) ? robotVelTracker.velocity : Vector3.zero;

        // 潜在的障碍物列表：其他玩家机器人、敌方机器人
        List<GameObject> obstacles = new List<GameObject>();

        // 参数调整
        float safetyDistance = 0.5f;  // 安全距离
        float repulsiveStrength = 10f; // 斥力强度
        float maxRepulsiveVelocity = 2f; // 最大斥力速度

        for (int i = 0; i < Param.MAX_PLAYER; i++)
        {
            // 检查是否有效（非null，并且不是机器人自身）
            if (Vision.player[i] != robot && Vision.isValid(i, "player"))
            {
                obstacles.Add(Vision.player[i]);
            }
            if (Vision.isValid(i, "enemy"))
            {
                obstacles.Add(Vision.enemy[i]);
            }
        }

        // 遍历所有潜在的障碍物
        foreach (GameObject obstacle in obstacles)
        {
            Vector3 obstaclePos = obstacle.transform.position;
            float distance = Vector3.Distance(robotPos, obstaclePos);

            // 只对距离较近的障碍物产生斥力
            if (distance < safetyDistance)
            {
                // 计算斥力方向（从障碍物指向机器人）
                Vector3 repulsiveDirection = (robotPos - obstaclePos).normalized;

                // 计算斥力大小（距离越近，斥力越大）
                float repulsiveMagnitude = repulsiveStrength * (1 / distance - 1 / safetyDistance);
                repulsiveMagnitude = Mathf.Clamp(repulsiveMagnitude, 0, maxRepulsiveVelocity);

                // 计算斥力速度
                Vector3 repulsiveVelocity = repulsiveDirection * repulsiveMagnitude;

                // 主要在垂直于期望运动方向的方向上产生斥力
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