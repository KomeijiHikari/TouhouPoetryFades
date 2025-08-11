using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class 发射下落物
{
    public float 投射物发射速度;
    SpriteRenderer sp;

    [Range (0,1)]
    [SerializeField ]
    float 开始间隔=0;
    [SerializeField ]
    float 间隔=1;
    float Ti;
    float 释放的时间点;

    [SerializeField]
    int 秒;
    private void Awake()
    {
        sp =GetComponent <SpriteRenderer>(); 
        释放的时间点 =  -(秒 % 间隔); 
    }
    private void Start()
    {
        预播放();
    }
    int LastC; 
    public bool De=false ;
    void 预播放()
    {
        var a = Surp_Pool.I.GetValue(Surp_Pool.下坠刺).GetComponent<Fly_Ground>();
        //if (投射物发射速度!=0)     a.Speed_Lv = 投射物发射速度;
        var 速度 = a.Speed_Lv * a.self_speed;
        var 初始差 = (开始间隔 * 间隔 * 速度);
        var 最终长度 = (秒 * 速度)- 初始差;//   11秒   速度为4  长度为  44
        var 步距 = 速度 * 间隔;//速度4 间隔2    步距  8

        int  中间数量 = (int )(最终长度 / 步距);
        for (int i = 0; i < 中间数量+1; i++)
        {
            var 点 = (最终长度 - (i * 步距)  ) * a.方向 ;
            点 = 点 + (Vector2 )transform.position;
            Surp_Pool.I.GetPool(Surp_Pool.下坠刺).transform .position= 点;
        if(De)    点.DraClirl(2, Color.red, 10);
        }
    }
 
 
 
    private void FixedUpdate()
    {
        Ti += Time.fixedDeltaTime*I_S .固定等级差;
        if (LastC!=Time.frameCount)
        {
            LastC = Time.frameCount;

        if (Ti>间隔  + 释放的时间点 )
        {
            释放的时间点 = Ti;
            发射();
        }
        }
    }

    void 发射()
    {
        var a = Surp_Pool.I.GetPool(Surp_Pool.下坠刺);
        a.transform.position = transform.position;
 

    }
}

public partial class 发射下落物 : MonoBehaviour, I_Dead, I_Revive, I_Speed_Change
{
    public GameObject 对象 { get => gameObject; }
    [SerializeField] private float speed_Lv;
    [SerializeField] private bool re;
    [SerializeField] private float re_Time;

    public bool Re { get => re; set => re = value; }
    public float Re_Time { get => re_Time; set => re_Time = value; }
    public Action 销毁触发 { get; set; }
    public Action 变速触发 { get; set; }

    public Bounds 盒子 => sp.bounds;


    public I_Speed_Change I_S => this;

    public float Current_Speed_LV => Speed_Lv;

    public float Speed_Lv { get => speed_Lv; set => speed_Lv = value; }

    public bool Dead()
    {
        return true;
    }

    public bool 重制()
    {
        return true;
    }
}
