using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Param : MonoBehaviour
{
    static public string GAME_MODE;
    static public string GAME_CONNECT_MODE;
    // vision
    static public string MCAST_GRP = "224.5.23.2"; // �Ӿ����յ�ַ
    static public int MCAST_PORT_SIM = 10020; // �Ӿ����ն˿� ����10020��ʵ��10005
    static public int MCAST_PORT_REAL = 10006; // �Ӿ����ն˿� ����10020��ʵ��10005

    static public float SCALE_COORDINATE = 0.01f;
    // control

    static public int MAX_POWER = 255;

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
    static public float OUT_OF_SIGHT_Y = -10f; // Y���꣬��ʾ�����˲�����Ұ��
    static public float CAMERA_SLOW_DISTANCE = 3f; // ����� �����ƶ��ľ���
    static public float DRIBBLE_BALL_DISTANCE = 1.8f;
}
