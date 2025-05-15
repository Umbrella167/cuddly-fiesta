using UnityEngine;
// using System.Collections; // 如果未使用可以移除

public class Param : MonoBehaviour
{
    // STATIC (这些保持硬编码或不在此次JSON加载请求范围内)
    static public int BALL_Z = 0;
    static public float ROBOT_Z = 0.54f;
    static public int MAX_PLAYER = 16;
    static public string BLUE = "blue";
    static public string YELLOW = "yellow";
    static public string ENEMY = "enemy";
    static public string PLAYER = "player";
    static public string SIMULATE = "Simulate";
    static public string REAL = "Real";
    static public float OUT_OF_SIGHT_Y = -10f;
    static public float CAMERA_SLOW_DISTANCE = 3f;
    static public float DRIBBLE_BALL_DISTANCE = 1.75f;

    static public string GAME_MODE;
    static public string GAME_CONNECT_MODE;
    static public float SCALE_COORDINATE = 0.01f;
    static public int MAX_POWER = 255;

    // ////////////////////////////////////////////////////////////JSON READ////////////////////////////////////////////////////////////////////////////////////
    // vision
    static public string MCAST_GRP;
    static public int MCAST_PORT_SIM;
    static public int MCAST_PORT_REAL;

    // ref box
    static public string REF_MCAST_GRP;
    static public int REF_MCAST_PORT;

    //real control udp
    static public int CONTROL_SERVERPORT;

    /////////////////////////////////////////////////////////////////////////////REAL//////////////////////////////////////////////////////////////////////////
    // POWERSET
    static public float REAL_POWERSET_RATE_CHIP;
    static public float REAL_POWERSET_MIN_CHIP;
    static public float REAL_POWERSET_MAX_CHIP;

    static public float REAL_POWERSET_RATE_FLAT;
    static public float REAL_POWERSET_MIN_FLAT;
    static public float REAL_POWERSET_MAX_FLAT;

    // ROTATE PID
    static public float REAL_PIDROTATION_KP;
    static public float REAL_PIDROTATION_KI;
    static public float REAL_PIDROTATION_KD;

    //AUTO ACC (这些变量之前在REAL注释块下，但没有REAL_前缀)
    // ParamLoader 会将 RealParams.DRIBBLING_ACC 映射到此处的 DRIBBLING_ACC
    static public float DRIBBLING_ACC;
    static public float UNDRIBBLING_ACC;

    // CONTROL SPEED
    static public float REAL_NROMAL_SPEED; // 注意: 变量名中的 "NROMAL" 是个可能的拼写错误
    static public float REAL_SLOW_SPEED;
    static public float REAL_MAX_SPEED;

    /////////////////////////////////////////////////////////////////////////////SIM//////////////////////////////////////////////////////////////////////////
    // SIM PID控制参数
    static public float SIM_PIDROTATION_KP;
    static public float SIM_PIDROTATION_KI;
    static public float SIM_PIDROTATION_KD;

    // SIM 功率设置参数
    static public float SIM_POWERSET_RATE_CHIP;
    static public float SIM_POWERSET_MIN_CHIP;
    static public float SIM_POWERSET_MAX_CHIP;

    static public float SIM_POWERSET_RATE_FLAT;
    static public float SIM_POWERSET_MIN_FLAT;
    static public float SIM_POWERSET_MAX_FLAT;

    // 自动加速参数
    static public float SIM_DRIBBLING_ACC;
    static public float SIM_UNDRIBBLING_ACC;

    // 控制速度参数
    static public float SIM_NROMAL_SPEED; // 注意: 变量名中的 "NROMAL" 是个可能的拼写错误
    static public float SIM_SLOW_SPEED;
    static public float SIM_MAX_SPEED;
}