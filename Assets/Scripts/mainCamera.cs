using UnityEngine;

public class mainCamera : MonoBehaviour
{
    public LayerMask ignoreLayer;
    public GameObject MouseImage;
    public GameObject PowerRageBoundary;
    public GameObject BallDirFlag;
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
        UnityEngine.Cursor.visible = false; // 隐藏鼠标
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 使用 LayerMask 忽略特定图层
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreLayer))
        {
            MouseImage.transform.position = new Vector3(hit.point.x, 0.06f, hit.point.z);
        }
        Vector3 playerPos = robot.transform.position;
        BallDirFlag.transform.position = robot.transform.position - (Vector3.up * 0.5f);
        BallDirFlag.transform.LookAt(Vision.ball.transform);
        BallDirFlag.transform.eulerAngles = new Vector3(90, BallDirFlag.transform.eulerAngles.y + 90, 0);



        if (Vector3.Distance(Vision.ball.transform.position, playerPos) > Param.DRIBBLE_BALL_DISTANCE) 
        {
            PowerRageBoundary.transform.position = robot.transform.position;
        }
        if (Input.GetKey(KeyCode.KeypadPlus) || Input.GetKey(KeyCode.Equals)) 
        {
            transform.position += Vector3.up * 0.1f;
        }
        if (Input.GetKey(KeyCode.KeypadMinus) || Input.GetKey(KeyCode.Minus))
        {
            transform.position -= Vector3.up * 0.1f;
        }

        float speed = Vector3.Distance(transform.position, playerPos) > Param.CAMERA_SLOW_DISTANCE ? 5f * Time.deltaTime : 1.2f * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, new Vector3(playerPos.x, transform.position.y, playerPos.z), speed);

    }
}
