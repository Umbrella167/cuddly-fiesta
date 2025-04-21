using UnityEngine;
public class PIDRotation
{
    public float P = 0.5f;   // 调整比例增益
    public float I = 0.0f;
    public float D = 0.1f;   // 增加微分项抑制震荡

    private float _integral;
    private float _previousError;

    public float Compute(float error, float deltaTime)
    {
        // 比例项
        float proportional = P * error;

        // 积分项（带限幅防止积分饱和）
        _integral = Mathf.Clamp(_integral + error * deltaTime, -1, 1);
        float integral = I * _integral;

        // 微分项（使用误差变化率）
        float derivative = D * ((error - _previousError) / deltaTime);
        _previousError = error;

        return proportional + integral + derivative;
    }

    public void Reset()
    {
        _integral = 0;
        _previousError = 0;
    }
}


public class VelocityPIDController
{
    public float Kp = 10f;  // 比例系数
    public float Ki = 0.1f; // 积分系数
    public float Kd = 0.05f; // 微分系数

    private float previousError = 0f;
    private float integralError = 0f;
    private const float MAX_INTEGRAL = 50f; // 积分项限幅

    public float Calculate(float currentVelocity, float targetVelocity, float deltaTime)
    {
        // 计算误差
        float error = targetVelocity - currentVelocity;

        // 积分项
        integralError += error * deltaTime;
        // 积分项限幅
        integralError = Mathf.Clamp(integralError, -MAX_INTEGRAL, MAX_INTEGRAL);

        // 微分项
        float derivativeError = (error - previousError) / deltaTime;

        // PID输出
        float output = Kp * error + Ki * integralError + Kd * derivativeError;

        // 更新前一次误差
        previousError = error;

        return output;
    }

    // 重置PID控制器
    public void Reset()
    {
        previousError = 0f;
        integralError = 0f;
    }
}
