using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Param : MonoBehaviour
{
    static public string GAME_MODE;
    static public string GAME_CONNECT_MODE;

    // control
    static public float NROMAL_SPEED = 80f;
    static public float SLOW_SPEED = 40f;
    static public float MAX_SPEED = 200f;

    static public int ROBOT_Z = 0;
    static public int MAX_PLAYER = 16;
    static public string BLUE = "blue";
    static public string YELLOW = "yellow";
    static public string ENEMY = "enemy";
    static public string PLAYER = "player";
    static public string SIMULATE = "Simulate";
    static public string REAL = "Real";
    static public float OUT_OF_SIGHT_Y = -10f; // Y坐标，表示机器人不在视野内
    static public float CAMERA_SLOW_DISTANCE = 3f; // 摄像机 缓慢移动的距离

}
