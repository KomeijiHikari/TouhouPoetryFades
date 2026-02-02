using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 半灵 : MonoBehaviour
{
      flow F;
    public Transform Target { get { return F.TargetTransform; } set {
            F.TargetTransform = value;
        } }

    [SerializeField]
  public  Transform 玩家目标点;
    public static 半灵 I;
    [SerializeField]
    控制粒子 子弹发射;
    [SerializeField] 单片段 idle;
    [SerializeField] 单片段 Atk;
    [SerializeField] 亚拉动画 A;

    [SerializeField]
    SpriteRenderer sp;

    [SerializeField] ParticleSystem 半灵尾巴;

    public void Set跟随开关(bool b)
    {
        F.enabled= b;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="b"> 表示要不要设置成这个   </param>
    public void SetTarget(Transform  s ,bool b=false)
    {
        if ( b  )
        {
            Target= s;
          
        }
        else
        {
            ////不要设置成s

            if (Target==s)
            {
                //取消设置
                Target = 玩家目标点;
            }
            else
            {
                if (Target==玩家目标点)
                {

                }
                else
                {

                }
            }
 
  
        }
    }

    public bool 尾巴;
    public void 半灵开关(bool c)
    {
    var a= 半灵尾巴.emission;
        sp.enabled = c;
        a.enabled = c; 
    }
    public void 打到目标()
    { 
        生物.被扣血(35,Player3.I.gameObject ,0);
    }

    private void Update()
    {
        if (尾巴!= Player3.I.N_.半灵)
        {
            尾巴 = Player3.I.N_.半灵;
            半灵开关(尾巴);
        }
    }
    private void Awake()
    {
        if (I != null && I != this) Destroy(this);
        else I = this;
        gameObject.组件(ref sp);
        A=GetComponent<亚拉动画 >();
        A.动画结束 += 动画结束;
        F = GetComponent<flow>();
    }
    private void Start()
    {
        半灵开关(false);
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
            子弹发射.飞向的target = E.transform.transform;
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
        if (!Player3.I.N_.半灵) return;
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
