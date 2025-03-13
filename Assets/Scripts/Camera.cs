using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    GameObject robot = null;
    // Start is called before the first frame update
    void Start()
    {
        robot = GameObject.Find(Connect.team + "_robot" + Connect.robotID.ToString());
        //robot = GameObject.Find("yellow_robot8");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = robot.transform.position;
        transform.position = Vector3.Lerp(transform.position, playerPos + Vector3.back * 10 + Vector3.up * 5.5f - Vector3.left * 3.8f, 10f * Time.deltaTime);
    }
}
