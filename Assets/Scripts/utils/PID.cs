using UnityEngine;
public class PIDRotation
{
    public float P = 0.5f;   // ������������
    public float I = 0.0f;
    public float D = 0.1f;   // ����΢����������

    private float _integral;
    private float _previousError;

    public float Compute(float error, float deltaTime)
    {
        // ������
        float proportional = P * error;

        // ��������޷���ֹ���ֱ��ͣ�
        _integral = Mathf.Clamp(_integral + error * deltaTime, -1, 1);
        float integral = I * _integral;

        // ΢���ʹ�����仯�ʣ�
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
    public float Kp = 10f;  // ����ϵ��
    public float Ki = 0.1f; // ����ϵ��
    public float Kd = 0.05f; // ΢��ϵ��

    private float previousError = 0f;
    private float integralError = 0f;
    private const float MAX_INTEGRAL = 50f; // �������޷�

    public float Calculate(float currentVelocity, float targetVelocity, float deltaTime)
    {
        // �������
        float error = targetVelocity - currentVelocity;

        // ������
        integralError += error * deltaTime;
        // �������޷�
        integralError = Mathf.Clamp(integralError, -MAX_INTEGRAL, MAX_INTEGRAL);

        // ΢����
        float derivativeError = (error - previousError) / deltaTime;

        // PID���
        float output = Kp * error + Ki * integralError + Kd * derivativeError;

        // ����ǰһ�����
        previousError = error;

        return output;
    }

    // ����PID������
    public void Reset()
    {
        previousError = 0f;
        integralError = 0f;
    }
}
