using UnityEngine;

public class mainCamera : MonoBehaviour
{
    public LayerMask ignoreLayer;
    public GameObject MouseImage;
    public GameObject PowerRageBoundary;

    public Camera cam;
    GameObject robot;
    // Start is called before the first frame update
    void Start()
    {
        robot = GameObject.Find(Connect_Gate.team + "_robot" + Connect_Gate.robotID.ToString());
        //robot = GameObject.Find("yellow_robot8");

    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false; // 隐藏鼠标
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 使用 LayerMask 忽略特定图层
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreLayer))
        {
            MouseImage.transform.position = new Vector3(hit.point.x, 0.06f, hit.point.z);
        }
        Vector3 playerPos = robot.transform.position;

        if (Vector3.Distance(Vision.ball.transform.position, playerPos) > 0.95) 
        {
            PowerRageBoundary.transform.position = robot.transform.position;
        }

        float speed = Vector3.Distance(transform.position, playerPos) > Param.CAMERA_SLOW_DISTANCE ? 5f * Time.deltaTime : 1.2f * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, new Vector3(playerPos.x, transform.position.y, playerPos.z), speed);

    }
}
