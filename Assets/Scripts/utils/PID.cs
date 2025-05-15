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
        _integral = Mathf.Clamp(_integral + error * deltaTime, -100, 100);
        if(Mathf.Abs(error) < 0.5f)
        {
            _integral = 0;
        }
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

