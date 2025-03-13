public class PIDRotation
{
    public float P = 1.0f; // ��������
    public float I = 0.0f; // ��������
    public float D = 0.0f; // ΢������

    private float _integral;
    private float _previousError;

    public float Compute(float error, float deltaTime)
    {
        // ������
        float proportional = P * error;

        // ������
        _integral += error * deltaTime;
        float integral = I * _integral;

        // ΢����
        float derivative = D * (error - _previousError) / deltaTime;
        _previousError = error;

        // �����
        return proportional + integral + derivative;
    }

    public void Reset()
    {
        _integral = 0;
        _previousError = 0;
    }
}
