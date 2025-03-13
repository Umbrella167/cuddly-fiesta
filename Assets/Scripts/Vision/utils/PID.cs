public class PIDRotation
{
    public float P = 1.0f; // 比例增益
    public float I = 0.0f; // 积分增益
    public float D = 0.0f; // 微分增益

    private float _integral;
    private float _previousError;

    public float Compute(float error, float deltaTime)
    {
        // 比例项
        float proportional = P * error;

        // 积分项
        _integral += error * deltaTime;
        float integral = I * _integral;

        // 微分项
        float derivative = D * (error - _previousError) / deltaTime;
        _previousError = error;

        // 总输出
        return proportional + integral + derivative;
    }

    public void Reset()
    {
        _integral = 0;
        _previousError = 0;
    }
}
