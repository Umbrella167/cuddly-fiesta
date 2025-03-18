using System;
using System.Linq;
using UnityEngine;
using static Packet;

public class Control : MonoBehaviour
{
    int control_robot_id = Connect_Gate.robotID;
    int control_frequency = Connect_Gate.frequency;
    string control_team = Connect_Gate.team;
    string game_mode = Connect_Gate.GAME_MODE;
    public GameObject targetObj;
    static public RadioPacket[] packet = new RadioPacket[16];

    public float selfVx = 0;
    public float selfVy = 0;
    public float selfVr = 0;
    public float selfPower = 0;

    public float maxRotationOutput = 500f; // 最大旋转输出值

    void Start()
    {
        
        System.Threading.Thread.Sleep(1000);
        if (game_mode == Param.SIMULATE) 
        {
            packet = Control_Sim.packet;
        }
        else if (game_mode == Param.REAL)
        {
            packet = Control_Real.packet;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float[] localVelocities = Control_Utils.GlobalToLocalVelocity(Vision.selfRobot,packet[control_robot_id].velX, packet[control_robot_id].velY);
        packet[control_robot_id].velX = localVelocities[0];
        packet[control_robot_id].velY = localVelocities[1];
        packet[control_robot_id].Encode();
    }
    

}