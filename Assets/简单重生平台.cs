using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class 简单重生平台 : MonoBehaviour, I_Revive,I_Dead 
{
    [SerializeField]
    SpriteRenderer sr;
    BoxCollider2D bc;
    [SerializeField]
    private float Live_Time_Max=1;

    [DisplayOnly]
    [SerializeField]
    private float live_Time_;
    //[SerializeField]
    private float re_Time=1;
    public Bounds 盒子 => sr.bounds;
    //public Bounds 盒子 => new Bounds (transform.position, sr.size);

    public bool Re { get =>true; set { } }
    public float Re_Time { get => re_Time; set => re_Time = value; }
    public Action 销毁触发 { get  ; set   ; }
    public bool 是 { get { return 是1; }  set { Debug.LogError(value);
            
            是1 = value; } }

    public float Live_Time_ { get => live_Time_; set => live_Time_ = value; }
    public float Live_Time_Max1 { get => Live_Time_Max; }

    [DisplayOnly]
    [SerializeField]
    private bool 是1;

    [DisplayOnly]
    [SerializeField]
    public float 进度;
     

    I_Speed_Is I;
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag(Initialize.Player))
    //        是 = false;
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Initialize.Player))
            if (collision.transform.position.y>transform.position.y)
            {
                是 = true;
            }
    }
    private void FixedUpdate()
    {
        if (bc.enabled)
        {
            if (是)
            {
                Live_Time_ -= Time.fixedDeltaTime*(1/ Player3.Public_Const_Speed)   ;
                if (进度 > 1)
                {
                    销毁触发?.Invoke();
                }
            }
            else
            {
                Live_Time_ = Live_Time_Max1;
            }
        }
      
    }
    private void Update()
    {
        进度 = 1 - Live_Time_ / Live_Time_Max1;

    }
    public bool 重制()
    {

        sr.enabled = true;
        bc.enabled = true;
        return true;
    }
    private void Awake()
    {
        bc = GetComponent<BoxCollider2D>();

    }
    private void Start()
    {
        Player3.I.圆斩对象 += asd;

    var a=   GetComponent<生命周期管理>();
        a.重生时不等待玩家 = false;
        a.真实时间复活 = true;
        //Re_Time
    }
    void asd(int i)
    {
        if (i==gameObject.GetInstanceID())
        {
            Debug.LogError("AAAAAAAAAAAAAAAAAAAA");
            是 = true;
            Debug.LogError(是);
        }
    }
    public bool Dead()
    {
        是 = false;
        sr.enabled = false;
        bc.enabled = false ;
        return true;
    } 
}
