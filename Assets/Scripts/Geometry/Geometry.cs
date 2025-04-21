using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geometry : MonoBehaviour
{




    public static bool LinesIntersect(Vector2 line1Start, Vector2 line1End, Vector2 line2Start, Vector2 line2End)
    {
        // ���㷽������
        Vector2 dir1 = line1End - line1Start;
        Vector2 dir2 = line2End - line2Start;

        // ������
        float cross = dir1.x * dir2.y - dir1.y * dir2.x;

        // ������Ϊ0��˵��ֱ��ƽ��
        if (Mathf.Approximately(cross, 0f))
            return false;

        // �������t��u
        Vector2 v = line2Start - line1Start;
        float t = (v.x * dir2.y - v.y * dir2.x) / cross;
        float u = (v.x * dir1.y - v.y * dir1.x) / cross;

        // �жϽ����Ƿ��������߶���
        return t >= 0 && t <= 1 && u >= 0 && u <= 1;
    }

    public static Vector2 GetProjectionPoint2D(Vector2 point, Vector2 lineStart, Vector2 lineEnd)
    {
        // ����ֱ�߷�������
        Vector2 lineDirection = lineEnd - lineStart;

        // ����㵽����������
        Vector2 pointToLineStart = point - lineStart;

        // ����ͶӰ����
        float projectionLength = Vector2.Dot(pointToLineStart, lineDirection) / lineDirection.sqrMagnitude;

        // ����ͶӰ��
        Vector2 projectionPoint = lineStart + projectionLength * lineDirection;

        return projectionPoint;
    }
}

public static class Vector3Extensions
{
    // ת��Ϊ2D����������X��Y��
    public static Vector2 ToVector2(this Vector3 vector)
    {
        return new Vector2(vector.x, vector.y);
    }

    // ��ѡ��ָ�������ĸ�ƽ��
    public static Vector2 ToVector2(this Vector3 vector, Plane plane)
    {
        switch (plane)
        {
            case Plane.XY:
                return new Vector2(vector.x, vector.y);
            case Plane.XZ:
                return new Vector2(vector.x, vector.z);
            case Plane.YZ:
                return new Vector2(vector.y, vector.z);
            default:
                return new Vector2(vector.x, vector.y);
        }
    }

    // ö�ٶ���
    public enum Plane
    {
        XY,
        XZ,
        YZ
    }
}
