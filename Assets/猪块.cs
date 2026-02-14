using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 猪块 : MonoBehaviour, I_Dead, I_Revive, I_Speed_Is
{

    BoxCollider2D bc;
    SpriteRenderer sp;
    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    } 
    private void Start()
    {
        if (上海 != null)
            Initialize_Mono.I.Waite(() => {
                上海.transform.SetParent(transform);

            });

        var a = GetComponent<生命周期管理>();
        a. 重生时不等待玩家 = true;
        a.重生伤害 = false;
        Player3.I.圆斩对象 += asd;
    }

    private void asd(int obj)
    {
        if (obj == gameObject.GetInstanceID())
        {
 
                销毁触发?.Invoke();
        }

    } 
    No_Re R = new No_Re();
    public bool bug; 
    void 开关(bool b = false)
    {
        if (!b)
        {
            Player3.I.LastV_Velocity();

            sp.enabled = false;
            bc.enabled = false;
        }
        else
        {
            var a = GetComponent<平台动画效果>();
            sp.enabled = true;
            bc.enabled = true; 

        }
        if (上海 != null)
        {
            上海.开关(b);
        }
    }
    public GameObject 对象 { get => gameObject; }
    public 上海玩家 上海;
    public Bounds 盒子 { get => sp.bounds; }
    public Action 销毁触发 { get; set; }

    [SerializeField] private bool re;
    private float re_Time = 0.7f;
    [SerializeField] private float speed_Lv = 1;

    public bool Re { get => re; set => re = value; }
    public float Re_Time { get => re_Time / speed_Lv; set => re_Time = value; }

    public Action 变速触发 { get; set; }


    public float Current_Speed_LV => Speed_Lv;

    public float Speed_Lv { get => speed_Lv; set => speed_Lv = value; }

    public bool Dead()
    {
        开关();
        return true;
    }

    public bool 重制()
    {
        开关(true);
        return true;
    }
}
