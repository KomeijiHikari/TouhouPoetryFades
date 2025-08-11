using System;
using System.Collections;
using System.Collections.Generic;
using Trisibo;
using UnityEngine;

public  partial  class 传送点 : MonoBehaviour
{
    public bool 可被销毁;
    public float 编号;
    BoxCollider2D bc;
    SpriteRenderer sp;
    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D >();
    }
    public   Vector3 传送点坐标 { get { 
            玩家走到了外面 = true;
            return transform.position; 
        } }
 

 
    void Start()
    {
        编号= transform.Get_摄像框编号();
    }

    public bool 玩家走到了外面;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") return;
        玩家走到了外面 = false;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.tag != "Player") return; 
        if (!玩家走到了外面)
        {
            玩家走到了外面 =true;
            传送导点.I.最后点 = transform.position;
            Player3.I.录入安全地点(true );
            if (可被销毁)
            {
                销毁触发?.Invoke();
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
