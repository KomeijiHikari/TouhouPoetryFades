using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class 简单重生平台 : MonoBehaviour, I_Revive,I_Dead ,I_Speed_Is
{
    [SerializeField]
    SpriteRenderer sr;
    BoxCollider2D bc;
    //[SerializeField] 
 
 
    //[SerializeField]
    private float re_Time=2.8f;
    public Bounds 盒子 => sr.bounds;
    //public Bounds 盒子 => new Bounds (transform.position, sr.size);

    public bool Re { get =>true; set { } }
    public float Re_Time { get => re_Time; set => re_Time = value; }
    public Action 销毁触发 { get  ; set   ; }
    public bool 是 { get { return 是1; }  set {
            if (Deb)
            {
                Debug.LogError(value);
            }
             
            是1 = value; } }

    public bool Deb;
    public float Live_Time_ { get; set; } = 0;
    public float Live_Time_Max1 { get  => Initialize_Mono.I.重生平台存活时间; }
    public float Speed_Lv { get => speed_Lv; set => speed_Lv = value; }

    [DisplayOnly]
    [SerializeField]
    private bool 是1;

    [DisplayOnly]
    [SerializeField]
    public float 进度;
     

    I_Speed_Is I;
    [SerializeField]
    private float speed_Lv;

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag(Initialize.Player))
    //        是 = false;
    //}
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.LogError(collision+"      AAAAAAAAAAA");
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
                Live_Time_ -= Time.fixedDeltaTime* ((I_Speed_Is)this).固定等级差;
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
            是 = true; 
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
