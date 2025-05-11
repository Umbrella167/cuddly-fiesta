using UnityEngine;

public class RuntimeLineRenderer : MonoBehaviour
{
    public Material lineMaterial; // ʹ��Ĭ�ϲ��ʻ��Զ������
    private LineRenderer lineRenderer;

    void Start()
    {
        // ����LineRenderer���
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
    }

    // ��̬���������յ㣨������Update�У�
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
        // ����Y�����ת�Ƕ�ת��Ϊ����������XZƽ�棩
        Vector3 direction = Quaternion.Euler(0, yRotationAngle, 0) * Vector3.back;

        // �����յ�λ��
        Vector3 endPoint = start + direction * totalLength;

        // �����յ�Y�������һ�£���ѡ��������������Ƿ�����
        endPoint.y = start.y;

        // �����߶���Ⱦ
        UpdatePoints(start, endPoint);
    }

}