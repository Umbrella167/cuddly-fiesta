using UnityEngine;

// 请确保 Vision.cs 和 GotoPos.cs 脚本存在于你的项目中

public static class GetBall // Make the class static
{
    // 定义拦截参数为公共静态常量或变量，方便调整
    public const float MIN_BALL_SPEED_FOR_INTERCEPTION = 0.1f; // 球速最低阈值，用于避免处理静止或慢速球
    // 用于判断球是否大致朝向机器人移动的阈值
    // 计算 ballVelocity 和 (robotPosition - ballPosition) 的点积
    // 如果点积 > 0，表示球速方向在从球到机器人的向量方向上有分量，即大致朝向机器人。
    public const float DOT_PRODUCT_THRESHOLD = 0f; // 点积阈值

    /// <summary>
    /// 计算机器人为了处理球所需的原始速度指令。
    /// 如果球速满足阈值并且大致朝向机器人移动，计算前往投影点（截球点）的速度；
    /// 否则，计算前往球当前位置的速度。
    /// </summary>
    /// <param name="robot">当前机器人的 GameObject。</param>
    /// <param name="ball">球的 GameObject。</param>
    /// <returns>一个表示前往目标点的原始速度指令的 Vector3。
    /// 这个 Vector3 的分量结构与 GotoPos.robot2pos 的返回值相同：
    /// X 分量对应期望的世界 X 轴速度，Y 分量对应期望的世界 Z 轴速度的负值，Z 分量未使用（为 0）。
    /// 返回 Vector3.zero 如果对象无效。</returns>
    static public Vector3 calculateInterceptionVelocity(GameObject robot, GameObject ball)
    {
        // 确保机器人和球对象有效
        if (robot == null || ball == null)
        {
            // Debug.LogWarning("GetBall.calculateInterceptionVelocity: Robot or Ball GameObject is null.");
            return Vector3.zero; // 如果对象无效，返回零速度
        }

        // 获取球的 VelocityTracker 组件来获取速度
        Vision.VelocityTracker ballVelTracker = ball.GetComponent<Vision.VelocityTracker>();

        // 确保球有 VelocityTracker 组件
        if (ballVelTracker == null)
        {
            Debug.LogError("GetBall.calculateInterceptionVelocity: Ball does not have a VelocityTracker component!");
            return Vector3.zero; // 如果没有速度组件，返回零速度
        }

        Vector3 ballVelocity = ballVelTracker.velocity;
        Vector3 robotPosition = robot.transform.position;
        Vector3 ballPosition = ball.transform.position;

        float ballSpeed = ballVelocity.magnitude;

        // 计算从球到机器人的向量
        Vector3 ballToRobot = robotPosition - ballPosition;
        float dotProduct = Vector3.Dot(ballVelocity, ballToRobot);

        // 定义目标位置变量
        Vector3 targetPosition;

        // --- 判断是否满足截球条件 ---
        // 条件：1. 球速大于阈值 2. 球大致朝向机器人移动 3. 机器人不与球重叠 (避免投影计算问题)
        bool canIntercept = ballSpeed > MIN_BALL_SPEED_FOR_INTERCEPTION &&
                            dotProduct > DOT_PRODUCT_THRESHOLD &&
                            ballToRobot.sqrMagnitude > 0.001f; // 使用小的平方距离阈值

        if (canIntercept)
        {

            Vector3 PB = robotPosition - ballPosition; // 向量：球 -> 机器人
            float ballVelSqrMag = ballVelocity.sqrMagnitude; // 球速的平方

            if (ballVelSqrMag > 0.001f) // 使用一个小的容差值
            {
                float t = Vector3.Dot(PB, ballVelocity) / ballVelSqrMag;

                targetPosition = ballPosition + t * ballVelocity;

            }
            else
            {
                targetPosition = ballPosition;
            }
        }
        else
        {
            targetPosition = ballPosition;
        }

        Vector3 requiredRawVelocity = GotoPos.robot2pos(robot, targetPosition);
        return requiredRawVelocity;
    }

}