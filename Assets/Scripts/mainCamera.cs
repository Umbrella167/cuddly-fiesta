using System.Collections;
using System.Collections.Generic;
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
        robot = GameObject.Find(Connect.team + "_robot" + Connect.robotID.ToString());
        //robot = GameObject.Find("yellow_robot8");
        Instantiate(PowerRageBoundary);
        Instantiate(MouseImage);
        PowerRageBoundary = GameObject.Find("boundary(Clone)");
        MouseImage = GameObject.Find("target(Clone)");
        
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
        //Vector3 mousePosition = Input.mousePosition;
        //mousePosition.z = 30;
        //Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //worldPosition.y = 0.01f;
        //MouseImage.transform.position = worldPosition;
        //MouseImage.transform.position = new Vector3(MouseImage.transform.position.x, 0.1f, MouseImage.transform.position.z);
        PowerRageBoundary.transform.position = robot.transform.position;

        Vector3 playerPos = robot.transform.position;
        transform.position = Vector3.Lerp(transform.position, playerPos + Vector3.back * 25 + Vector3.up * 30f - Vector3.left * 3.8f, 10f * Time.deltaTime);
    }
}
