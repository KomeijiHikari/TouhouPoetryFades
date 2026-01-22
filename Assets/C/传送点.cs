using System;
using System.Collections;
using System.Collections.Generic;
using Trisibo;
using UnityEngine;

public  partial  class 传送点 : MonoBehaviour
{
    public bool 是否回血=true;
    public bool 可被销毁;
    public float 编号;
    BoxCollider2D bc;
    SpriteRenderer sp;
    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D >();

        //玩家走到了外面 = true;

    }
    //public   Vector3 传送点坐标 { get { 
    //        玩家走到了外面 = true;
    //        return transform.position; 
    //    } }
 

 
    void Start()
    {
        编号= transform.Get_摄像框编号();

        ///有存档的情况下
        //Initialize_Mono.I.Waite(() =>
        //{
        //    玩家走到了外面 = false;
        //}, 0.8f);

        Player3.I.生命归零 += () => {

            Ttime=Time.time;
        };
    }
    float Ttime;
    private void Update()
    {
        if (玩家走到了外面)
        {
            if (Player_input.I.按键检测_按下(Player_input.I.k.交互))
            { 
                主UI.I.加点展开(); 
            } 
        } 
    }
    public bool 玩家走到了外面;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(Initialize.Player)) return;
        if (玩家走到了外面)
        {
        玩家走到了外面 = false;

            Player3.I.适应文字.开关(false);
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(Initialize.Player)) return;
        if (!玩家走到了外面)
        {
            if (Time.time< Ttime+1)     return;
     
            if (是否回血)
            { 
                Player3.I.当前hp = Player3.I.hpMax;
            }
            玩家走到了外面 =true;
            传送导点.I.最后点 = transform.position;
            Player3.I.录入安全地点(true );
            Player3.I.安全地点(true);
            if (可被销毁)
            { 
                销毁触发?.Invoke();
            }
            else
            {
                Player3.I.适应文字.开关(true);
                Player3.I.适应文字.SetText("E 打开商店");
            }
            Player3.SaveAll();
        }
    }
 
}

public partial class 传送点 : I_Dead
{
    public Action 销毁触发 { get  ; set  ; }

    public Bounds 盒子 =>bc .bounds;

    public bool Dead()
    {
        bc.enabled = false;
        sp.enabled = false;
        return true;
    }
}
