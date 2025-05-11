using UnityEngine;

public class RuntimeLineRenderer : MonoBehaviour
{
    public Material lineMaterial; // 使用默认材质或自定义材质
    private LineRenderer lineRenderer;

    void Start()
    {
        // 创建LineRenderer组件
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
    }

    // 动态更新起点和终点（例如在Update中）
    public void UpdatePoints(Vector3 start, Vector3 end)
    {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
    public void UpdateExtendedLine(Vector3 start, Vector3 originalEnd, float totalLength)
    {
        
        Vector3 p2 = start;
        Vector3 direction = originalEnd - start;

        Vector3 unitDirection = direction;
        p2 = start + unitDirection * totalLength;
        p2.y = start.y;
        UpdatePoints(start, p2); 
    }

    public void UpdateExtendedLineWithAngle(Vector3 start, float yRotationAngle, float totalLength)
    {
        // 将绕Y轴的旋转角度转换为方向向量（XZ平面）
        Vector3 direction = Quaternion.Euler(0, yRotationAngle, 0) * Vector3.back;

        // 计算终点位置
        Vector3 endPoint = start + direction * totalLength;

        // 保持终点Y轴与起点一致（可选，根据需求决定是否保留）
        endPoint.y = start.y;

        // 更新线段渲染
        UpdatePoints(start, endPoint);
    }

}