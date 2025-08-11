using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "生物数据", menuName = "ScriptableObject/生物数据", order = 0)]
public class 生物数据 : ScriptableObject 
{
    public string Name;

    public float 移动速度;
    public float[] 移动速度们;

    public Vector2  爆发力;
    public Vector2 爆发力2;

 

    public float hpMax1;

    public float atkvalue1;
    public float[] atkvalues ;

    public bool 会不会扣血;
    public bool 会不会跳跃;
    public Action 生命归零 { get; set; }

    public float 韧性;
    public float hpMax { get => hpMax1; set => hpMax1 = value; }
    public float atkvalue { get => atkvalue1; set => atkvalue1 = value; } 
}
