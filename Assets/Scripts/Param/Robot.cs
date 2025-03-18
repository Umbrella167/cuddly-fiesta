using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct Robot
{
    public int id;
    public Vector3 position;
    public float dir;
    public string team;

    public static readonly Vector3 DefaultPosition = new UnityEngine.Vector3(0, -10, 10);


    // 可选：构造函数，方便初始化
    public Robot(float dir, string team, int id = default, Vector3 position = default)
    {
        this.id = id == default?-1: id;
        this.position = position == default ? DefaultPosition : position;
        this.dir = dir;
        this.team = team;
    }

    public override string ToString()
    {
        return $"Robot ID: {id}, Position: {position}, Direction: {dir}";
    }
}
