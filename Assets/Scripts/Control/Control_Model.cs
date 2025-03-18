using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Model : MonoBehaviour
{

    public Vector3 NextPosition(GameObject robot, float vx, float vy) 
    {
        float deltaX = vx * Time.deltaTime;
        float deltaY = vy * Time.deltaTime;
        Vector3 robotPostion = robot.transform.position;
        Vector3 nextPosition = new UnityEngine.Vector3(robotPostion.x + deltaX, robotPostion.y, robotPostion.z + deltaY);
        return nextPosition;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
