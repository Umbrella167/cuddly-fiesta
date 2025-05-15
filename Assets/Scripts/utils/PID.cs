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
        _integral = Mathf.Clamp(_integral + error * deltaTime, -100, 100);
        if(Mathf.Abs(error) < 0.5f)
        {
            _integral = 0;
        }
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

