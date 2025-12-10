using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using Unity.VisualScripting;



public partial class    被打消失
{
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    sp.color = Color.red;
    //    Initialize_Mono.I.Waite(() => { 
    //        sp.color = Color.white;
    //    },0.1f);
   
    //}
    private void Update()
    {
        if (主动出发)
        {
            主动出发 = false;
               Trriger?.Invoke();
        }
    }
    public bool 主动出发;

    BoxCollider2D bc;
    SpriteRenderer sp;
 
  public   UnityEvent Trriger;

    [DisableOnPlay]
    [SerializeField]
   E_方向 当前方向;

    [DisableOnPlay]
    [SerializeField]
    Vector2 Way;
    private void Awake()
    {
        if (Poin!=null)     PoinPos = Poin.position;
        sp = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D >();

        Re_Time =    re_Time/ Seed_lv_计算;
    }
 public Bounds 盒子 { get => sp.bounds; }

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
 
        当前hp = hpMax;
 
        粒子 = GetComponentInChildren<粒子系统>();
        bc = GetComponent<BoxCollider2D >();
    }
    粒子系统 粒子;
 

 public void 被爆炸物触发()
    {
        if (当前方向 == E_方向.Null)
        {
            销毁触发?.Invoke();
        }
    }
    [SerializeField ]
    LayerMask 免疫;
    void 扣血效果(float i, GameObject obj)
    {
        if (Initialize.Layer_is(obj.layer, 免疫)) return;

        float 反方向 = Initialize.返回和对方相反方向的标准力(gameObject, obj).x;
        sp.闪光(0.051f); ;

        transform.DOShakePosition(0.1f, new Vector2(0.4f, 0), 30, 45f, false);
        当前hp -= i;
        if (粒子 != null)
        {
            粒子.数量 = i / 2;
 
            粒子.喷射方向 = new Vector2(反方向, 0);
            粒子?.Play();
        }
    }

    void 开关(bool b = false)
    {
        if (!b)
        {
            sp.enabled = false;
            bc.enabled = false;

        }
        else
        { 
                sp.enabled = true;
                bc.enabled = true; 
        }
    } 
}

public partial class 被打消失 : MonoBehaviour, I_生命, I_Dead, I_Revive
{




    public float hpMax_;
    public float hpMax { get {
            if (hpMax_==0)
            {
                hpMax_ = 100;
            }
            return hpMax_;
        } set => hpMax_ = value; }
    public Action 生命归零 { get; set; }
    public bool HPROCK { get; set; }





    public float 当前hp_;


    public float 当前hp
    {
        get
        {
            return 当前hp_;
        }
        set
        { 
            当前hp_ = value;
            if (当前hp_ <= 0)
            {
                销毁触发?.Invoke();
            }
        }
    }
    [SerializeField] bool Deb;

    float TT;

    public Transform Poin;
    Vector3 PoinPos;
    public void 被扣血(float i, GameObject obj, int SKey)
    {
        if (Time.time-TT> 最小收击时间间隔)
        {
            TT = Time.time;
            var a = false;
            a = Initialize.is_Boun判断(盒子, 当前方向.方向To_v2(), obj.transform.position);
            //if (Poin==null)
            //{ 
            //    a = 当前方向.方向To_v2().is_四方向比较(transform.你在我的哪里(obj.transform));
            //}
            //else
            //{
            //    ///冰块很长玩家在下面用圆劈 但是在冰块右下方 符合右方标准 所以 让“自己”位置偏移到最右侧 
            //    var zzz = PoinPos.你在我的哪里(obj.transform,true);
            //    var www = 当前方向.方向To_v2();
            //    Debug.LogError(zzz + "              "+ www);
            //  a = 当前方向.方向To_v2().is_四方向比较(PoinPos.你在我的哪里(obj.transform));
            //}

            if (a) 扣血效果(i, obj);
        }

    }
    public float 最小收击时间间隔 = 0.1f;
    public void 扣最大上限(float i)
    {
    }
    public Action 销毁触发 { get; set; }

    [SerializeField ]   private bool re;
    [SerializeField] private float re_Time;
    public bool Re { get => re; set => re = value; }
    public float Re_Time { get => re_Time; set => re_Time = value; }

    public  float    Seed_lv_计算=1;
    public bool Dead()
    {
        开关();

        if (Trriger!=null)   Trriger?.Invoke();
 
        return true;
    }

    public bool 重制()
    {

        当前hp = hpMax; 
        开关(true );
        return true;
    }
}