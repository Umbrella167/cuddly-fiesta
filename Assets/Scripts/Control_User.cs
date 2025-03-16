using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using static Packet;

public class Control_User : MonoBehaviour
{

    int control_robot_id = Connect_Gate.robotID;
    int control_frequency = Connect_Gate.frequency;
    string control_team = Connect_Gate.team;
    public GameObject targetObj;
    public float selfVx = 0;
    public float selfVy = 0;
    public float selfVr = 0;
    public float selfPower = 0;
    public bool dribble = false;
    public float dribblePower = 3.0f;
    public bool shoot = false;
    public float shootPower = 0f;
    public float maxRotationOutput = 500f; // 最大旋转输出值
    public PIDRotation pid = new PIDRotation();




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProcessInput()
    {

    }


}
