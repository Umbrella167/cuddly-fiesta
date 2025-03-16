using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    // Start is called before the first frame update
    static public GameObject selfRobot = null;
    static public GameObject ball = null;

    static public GameObject[] player = new GameObject[Param.MAX_PLAYER];
    static public GameObject[] enemy = new GameObject[Param.MAX_PLAYER];

    static public GameObject mouseObj = null;
    void Awake()
    {
        string enemy_team = Connect_Gate.team == Param.BLUE ? Param.BLUE : Param.YELLOW;
        ball = GameObject.Find("Ball");
        selfRobot = GameObject.Find(Connect_Gate.team + "_robot" + Connect_Gate.robotID.ToString());
        mouseObj = GameObject.Find("target");
        for (int i = 0; i < Param.MAX_PLAYER; i++)
        {
            player[i] =  GameObject.Find(Connect_Gate.team + "_robot" + i.ToString());
            enemy[i] = GameObject.Find(enemy_team + "_robot" + i.ToString());
        }
    }

    static public bool isValid(int num,string enemy_or_player = "player")
    {
        
        if (enemy_or_player == Param.ENEMY)
        {
            return (enemy[num].transform.position[1] == Param.OUT_OF_SIGHT_Y)?true:false;
        }
        else
        {
            return (player[num].transform.position[1] == Param.OUT_OF_SIGHT_Y) ? true : false;
        }

    }
    static public bool isEnemy(string team)
    {
        return Connect_Gate.team != team;

    }

}
