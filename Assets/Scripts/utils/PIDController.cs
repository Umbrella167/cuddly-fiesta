// Assets/Scripts/utils/PIDController.cs
using UnityEngine;

public class PIDController
{
    public float Kp { get; set; }
    public float Ki { get; set; }
    public float Kd { get; set; }

    private float integral;
    private float previousError;

    private float outputMin = -float.MaxValue;
    private float outputMax = float.MaxValue;
    // ������С�������ֵ
    public float MinAbsOutput { get; set; } = 0f;
    // ����
    public float DeadZone { get; set; } = 0.1f;
    public float DeadZoneOutput { get; set; } = 0.1f;

    public PIDController(float p, float i, float d, float minOut = -float.MaxValue, float maxOut = float.MaxValue)
    {
        Kp = p;
        Ki = i;
        Kd = d;
        outputMin = minOut;
        outputMax = maxOut;
        Reset(); // Initialize integral and previousError
    }

    public void SetOutputLimits(float min, float max)
    {
        outputMin = min;
        outputMax = max;
    }

    public void Reset()
    {
        integral = 0;
        previousError = 0;
    }

    public float Update(float currentValue, float targetValue, float deltaTime, bool isAngle = false)
    {
        if (deltaTime <= 0)
        {
            return Mathf.Clamp(Kp * (isAngle ? AngleDiff(targetValue, currentValue) : targetValue - currentValue), outputMin, outputMax);
        }

        float error;
        if (isAngle)
        {
            error = AngleDiff(targetValue, currentValue);
        }
        else
        {
            error = targetValue - currentValue;
        }

        // ��������
        if (Mathf.Abs(error) <= DeadZone)
        {
            // ��������ֱ�ӷ���DeadZoneOutput
            return DeadZoneOutput;
        }

        integral += error * deltaTime;

        float derivative = (error - previousError) / deltaTime;
        previousError = error;

        float output = (Kp * error) + (Ki * integral) + (Kd * derivative);

        // Ӧ����С�������ֵ����
        if (Mathf.Abs(output) < MinAbsOutput)
        {
            // ����������ֵС����С����������������������С���
            output = error > 0 ? MinAbsOutput : -MinAbsOutput;
        }

        return Mathf.Clamp(output, outputMin, outputMax);
    }

    /// <summary>
    /// Calculates the shortest angle difference between two angles (targetAngle - currentAngle).
    /// Result is in the range [-180, 180].
    /// </summary>
    public static float AngleDiff(float targetAngleDeg, float currentAngleDeg)
    {
        float diff = targetAngleDeg - currentAngleDeg;
        while (diff > 180f) diff -= 360f;
        while (diff < -180f) diff += 360f;
        return diff;
    }
}