using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
[CreateAssetMenu(fileName = "弹幕子弹数据",menuName = "能量子弹数据")]
public class Bullet数据 : ScriptableObject
{
    [Range(0, 1 )]
    public float 追踪玩家=0;
    public bool 自身旋转 = false;

    [Header("子弹初始配置")] 
    public float 生命周期=3;

    public float LinearVelocity=1;
    public float Acceleration=1;
    public float AngularVelocity=1;
    public float AngularAcceleration=1;


    public float Max速度 = int.MaxValue;
    [Header("发射器初始配置")]

    ///awake后  第一发默认发射角度
    public float InitRotation;

    ///awake后  初始加速度
    public float SenderAngularVelocity = 1;
    public float 发射器Max角速度 = int.MaxValue;
    [Tooltip("发射角的旋转速度")]
    public float SenderAcceleration=  1;

    [Tooltip("一组几个子弹")]
    public int Count=1;
    [Tooltip("一组子弹间隔角度")]
    public int LineAnle=30;

    

    public float 发射间隔 = 0.5f;
    /// <summary>
    /// 为0表示一直发射
    /// </summary>
    public int 波次 = 0;
    /// <summary>
    /// 为0表示一直发射
    /// </summary>
    public float 发射时间 = 0;
    [Header("预制体")]
    public GameObject pre;
}
