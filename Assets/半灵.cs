using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 半灵 : MonoBehaviour
{
    public static 半灵 I;
    [SerializeField]
    控制粒子 子弹发射;
    [SerializeField] 单片段 idle;
    [SerializeField] 单片段 Atk;
    [SerializeField] 亚拉动画 A;

    public void 打到目标()
    {
 
        生物.被扣血(35,Player3.I.gameObject ,0);
    }
    private void Awake()
    {
        if (I != null && I != this) Destroy(this);
        else I = this;
        A=GetComponent<亚拉动画 >();
        A.动画结束 += 动画结束;
    }

    private void 动画结束(string obj)
    {
        if (obj == null) return;
        if (obj == Atk.name)
        {
            A.播放(idle);
        }   
    }
    [SerializeField]
    Enemy_base 生物;
    public void 初始化(Enemy_base E,bool b=false)
    {
        if (b)
        {
            生物 = E;
            子弹发射.飞向的target = E.transform;
            子弹发射.co = E.co;
            子弹发射.enabled = true;
            子弹发射.初始化();
        }
        else
        {
            生物 = null;
            子弹发射.飞向的target = null;
            子弹发射.co = null;
            子弹发射.enabled = false; 
        } 
    }
    int TIMEF;
    public void  发射()
    { 
        if (TIMEF!=Time .frameCount)
        {
            TIMEF = Time.frameCount; 
            A.播放(Atk);
            子弹发射.pa.Play();
        }
        else
        { 
            Initialize_Mono.I.Waite((() => 发射()),0.01f); 
        }

    }
}
