using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vision : MonoBehaviour
{
    static public GameObject selfRobot = null;
    static public GameObject ball = null;
    static public GameObject[] player = new GameObject[Param.MAX_PLAYER];
    static public GameObject[] enemy = new GameObject[Param.MAX_PLAYER];

    static public GameObject mouseObj = null;

    // 用于存储上一帧的位置
    private static Vector3[] lastPlayerPositions = new Vector3[Param.MAX_PLAYER];
    private static Vector3[] lastEnemyPositions = new Vector3[Param.MAX_PLAYER];
    private static Vector3 lastBallPosition;
    private static Vector3 lastSelfRobotPosition;
    void Awake()
    {
        string enemy_team = Connect_Gate.team == Param.BLUE ? Param.BLUE : Param.YELLOW;
        ball = GameObject.Find("Ball");
        selfRobot = GameObject.Find(Connect_Gate.team + "_robot" + Connect_Gate.robotID.ToString());
        mouseObj = GameObject.Find("target");
        if (selfRobot != null)
        {
            if (!selfRobot.GetComponent<VelocityTracker>())
                selfRobot.AddComponent<VelocityTracker>();
        }
        for (int i = 0; i < Param.MAX_PLAYER; i++)
        {
            player[i] = GameObject.Find(Connect_Gate.team + "_robot" + i.ToString());
            enemy[i] = GameObject.Find(enemy_team + "_robot" + i.ToString());

            // 为每个 GameObject 添加 vel 属性
            if (player[i] != null)
            {
                if (!player[i].GetComponent<VelocityTracker>())
                    player[i].AddComponent<VelocityTracker>();
            }

            if (enemy[i] != null)
            {
                if (!enemy[i].GetComponent<VelocityTracker>())
                    enemy[i].AddComponent<VelocityTracker>();
            }
        }

        // 为球添加 vel 属性
        if (ball != null)
        {
            if (!ball.GetComponent<VelocityTracker>())
                ball.AddComponent<VelocityTracker>();
        }
    }
    void Update()
    {
        if (selfRobot != null)
        {
            VelocityTracker selfRobotVelTracker = selfRobot.GetComponent<VelocityTracker>();
            selfRobotVelTracker.UpdateVelocity(lastSelfRobotPosition);
            lastSelfRobotPosition = selfRobot.transform.position;
        }

        // 更新所有玩家机器人的速度
        for (int i = 0; i < Param.MAX_PLAYER; i++)
        {
            if (player[i] != null)
            {
                VelocityTracker playerVelTracker = player[i].GetComponent<VelocityTracker>();
                playerVelTracker.UpdateVelocity(lastPlayerPositions[i]);
                lastPlayerPositions[i] = player[i].transform.position;
            }

            if (enemy[i] != null)
            {
                VelocityTracker enemyVelTracker = enemy[i].GetComponent<VelocityTracker>();
                enemyVelTracker.UpdateVelocity(lastEnemyPositions[i]);
                lastEnemyPositions[i] = enemy[i].transform.position;
            }
        }

        // 更新球的速度
        if (ball != null)
        {
            VelocityTracker ballVelTracker = ball.GetComponent<VelocityTracker>();
            ballVelTracker.UpdateVelocity(lastBallPosition);
            lastBallPosition = ball.transform.position;
        }
    }

    static public GameObject FindNearestObjectInRange(Vector3 position, float radius)
    {
        return ball;
    }


    static public bool isValid(int num,string enemy_or_player = "player")
    {
        
        if (enemy_or_player == Param.ENEMY)
        {
            return (enemy[num].transform.position[1] == Param.OUT_OF_SIGHT_Y)?false: true;
        }
        else
        {
            return (player[num].transform.position[1] == Param.OUT_OF_SIGHT_Y) ?false : true;
        }

    }
    static public bool isEnemy(string team)
    {
        return Connect_Gate.team != team;

    }

    // 新增速度追踪组件
    public class VelocityTracker : MonoBehaviour
    {
        public Vector3 velocity { get; private set; }

        public void UpdateVelocity(Vector3 lastPosition)
        {
            velocity = (transform.position - lastPosition) / Time.deltaTime;
        }
    }

}
