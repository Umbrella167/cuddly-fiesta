using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geometry : MonoBehaviour
{




    public static bool LinesIntersect(Vector2 line1Start, Vector2 line1End, Vector2 line2Start, Vector2 line2End)
    {
        // 计算方向向量
        Vector2 dir1 = line1End - line1Start;
        Vector2 dir2 = line2End - line2Start;

        // 计算叉乘
        float cross = dir1.x * dir2.y - dir1.y * dir2.x;

        // 如果叉乘为0，说明直线平行
        if (Mathf.Approximately(cross, 0f))
            return false;

        // 计算参数t和u
        Vector2 v = line2Start - line1Start;
        float t = (v.x * dir2.y - v.y * dir2.x) / cross;
        float u = (v.x * dir1.y - v.y * dir1.x) / cross;

        // 判断交点是否在两条线段上
        return t >= 0 && t <= 1 && u >= 0 && u <= 1;
    }

    public static Vector2 GetProjectionPoint2D(Vector2 point, Vector2 lineStart, Vector2 lineEnd)
    {
        // 计算直线方向向量
        Vector2 lineDirection = lineEnd - lineStart;

        // 计算点到线起点的向量
        Vector2 pointToLineStart = point - lineStart;

        // 计算投影长度
        float projectionLength = Vector2.Dot(pointToLineStart, lineDirection) / lineDirection.sqrMagnitude;

        // 计算投影点
        Vector2 projectionPoint = lineStart + projectionLength * lineDirection;

        return projectionPoint;
    }
}

public static class Vector3Extensions
{
    // 转换为2D向量（保留X和Y）
    public static Vector2 ToVector2(this Vector3 vector)
    {
        return new Vector2(vector.x, vector.y);
    }

    // 可选：指定保留哪个平面
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

    // 枚举定义
    public enum Plane
    {
        XY,
        XZ,
        YZ
    }
}
