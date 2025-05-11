using UnityEngine;

// ��ȷ�� Vision.cs �� GotoPos.cs �ű������������Ŀ��

public static class GetBall // Make the class static
{
    // �������ز���Ϊ������̬������������������
    public const float MIN_BALL_SPEED_FOR_INTERCEPTION = 0.1f; // ���������ֵ�����ڱ��⴦��ֹ��������
    // �����ж����Ƿ���³���������ƶ�����ֵ
    // ���� ballVelocity �� (robotPosition - ballPosition) �ĵ��
    // ������ > 0����ʾ���ٷ����ڴ��򵽻����˵������������з����������³�������ˡ�
    public const float DOT_PRODUCT_THRESHOLD = 0f; // �����ֵ

    /// <summary>
    /// ���������Ϊ�˴����������ԭʼ�ٶ�ָ�
    /// �������������ֵ���Ҵ��³���������ƶ�������ǰ��ͶӰ�㣨����㣩���ٶȣ�
    /// ���򣬼���ǰ����ǰλ�õ��ٶȡ�
    /// </summary>
    /// <param name="robot">��ǰ�����˵� GameObject��</param>
    /// <param name="ball">��� GameObject��</param>
    /// <returns>һ����ʾǰ��Ŀ����ԭʼ�ٶ�ָ��� Vector3��
    /// ��� Vector3 �ķ����ṹ�� GotoPos.robot2pos �ķ���ֵ��ͬ��
    /// X ������Ӧ���������� X ���ٶȣ�Y ������Ӧ���������� Z ���ٶȵĸ�ֵ��Z ����δʹ�ã�Ϊ 0����
    /// ���� Vector3.zero ���������Ч��</returns>
    static public Vector3 calculateInterceptionVelocity(GameObject robot, GameObject ball)
    {
        // ȷ�������˺��������Ч
        if (robot == null || ball == null)
        {
            // Debug.LogWarning("GetBall.calculateInterceptionVelocity: Robot or Ball GameObject is null.");
            return Vector3.zero; // ���������Ч���������ٶ�
        }

        // ��ȡ��� VelocityTracker �������ȡ�ٶ�
        Vision.VelocityTracker ballVelTracker = ball.GetComponent<Vision.VelocityTracker>();

        // ȷ������ VelocityTracker ���
        if (ballVelTracker == null)
        {
            Debug.LogError("GetBall.calculateInterceptionVelocity: Ball does not have a VelocityTracker component!");
            return Vector3.zero; // ���û���ٶ�������������ٶ�
        }

        Vector3 ballVelocity = ballVelTracker.velocity;
        Vector3 robotPosition = robot.transform.position;
        Vector3 ballPosition = ball.transform.position;

        float ballSpeed = ballVelocity.magnitude;

        // ������򵽻����˵�����
        Vector3 ballToRobot = robotPosition - ballPosition;
        float dotProduct = Vector3.Dot(ballVelocity, ballToRobot);

        // ����Ŀ��λ�ñ���
        Vector3 targetPosition;

        // --- �ж��Ƿ������������ ---
        // ������1. ���ٴ�����ֵ 2. ����³���������ƶ� 3. �����˲������ص� (����ͶӰ��������)
        bool canIntercept = ballSpeed > MIN_BALL_SPEED_FOR_INTERCEPTION &&
                            dotProduct > DOT_PRODUCT_THRESHOLD &&
                            ballToRobot.sqrMagnitude > 0.001f; // ʹ��С��ƽ��������ֵ

        if (canIntercept)
        {

            Vector3 PB = robotPosition - ballPosition; // �������� -> ������
            float ballVelSqrMag = ballVelocity.sqrMagnitude; // ���ٵ�ƽ��

            if (ballVelSqrMag > 0.001f) // ʹ��һ��С���ݲ�ֵ
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