using System;
using UnityEngine;

public class GotoPos : MonoBehaviour
{
    static float kp_x = 12f;
    static float ki_x = 0.8f;
    static float kd_x = 0f;

    static float kp_y = 12f;
    static float ki_y = 0.8f;
    static float kd_y = 0f;

    static float min_vel = -180f;
    static float max_vel = 180f;
    static PIDController controller_x = new PIDController(kp_x, ki_x, kd_x, min_vel, max_vel)
    {
        DeadZone = 1f,        // 调整死区大小
        DeadZoneOutput = 1,
        MinAbsOutput = 30f  // 调整接近目标点时的最小速度

    };

    static PIDController controller_y = new PIDController(kp_y, ki_y, kd_y, min_vel, max_vel)
    {
        DeadZone = 1f,        // 调整死区大小
        DeadZoneOutput = 1,
        MinAbsOutput = 30f  // 调整接近目标点时的最小速度
    };


    public static Vector3 robot2pos(GameObject robot, Vector3 targetPos,string gamemode = "Simulate")
    {
        Vector3 vel = new Vector3(0, 0, 0);
        Vector3 robot_pos = robot.transform.position;
        float controlOutputX = controller_x.Update(robot_pos.x, targetPos.x, Time.deltaTime);
        float controlOutputY = controller_y.Update(robot_pos.z, targetPos.z, Time.deltaTime);

        if (gamemode == Param.SIMULATE)
        {
            vel.x = controlOutputX;
            vel.y = -controlOutputY;
        }
        else
        {
            vel.x = -controlOutputY;
            vel.y = -controlOutputX;
        }


        //Debug.Log("X: " + (targetPos.x - robot_pos.x).ToString() + "    out X: " + (vel.x).ToString() +"    Y: " + (targetPos.z - robot_pos.z).ToString() + "    out Y: " + (vel.y).ToString());
        return vel;
    }


    private void Start()
    {

    }


}
