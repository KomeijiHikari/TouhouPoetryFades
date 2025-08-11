using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class 单方面通过 : I_Dead, I_Revive, I_Speed_Change
{
    public GameObject 对象 { get => gameObject; }
    public 上海玩家 上海; 
    public Bounds 盒子 { get => sp.bounds; }
    public Action 销毁触发 { get; set; }

    [SerializeField] private bool re;
    [SerializeField] private float re_Time;
    private float speed_Lv=1;

    public bool Re { get => re; set => re = value; }
    public float Re_Time { get => re_Time; set => re_Time = value; }

    public Action 变速触发 { get; set; }

    public I_Speed_Change I_S => this;

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

public partial class 单方面通过 : MonoBehaviour
{

    BoxCollider2D bc;
    SpriteRenderer sp;
    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }

    [SerializeField]
    E_方向 被破坏方向;
    private void Start()
    {
        if (上海 != null)
            Initialize_Mono.I.Waite(() => {
                上海.transform.SetParent(transform);

            });



    }
    void 延迟销毁()
    {
        //Player3.I.LastV_Velocity();
        //销毁触发?.Invoke();

        if (销毁延迟时间 == 0)
        { 
            销毁触发?.Invoke();
        }
        else
        {
            Initialize_Mono.I.Waite(
            () => { 销毁触发?.Invoke(); },
                I_S.固定等级差 * 销毁延迟时间
                );
        }

    }
    No_Re R = new No_Re();
    public bool bug;
    private void OnCollisionEnter2D(Collision2D co )
    {
        if (bug)  Debug.LogError("  (cAAAAAAAAAAAAAAAAAAAAAAAAAAAAA向));");
        if (co .gameObject .layer ==Initialize .L_Player )
        {
            if (bug) Debug.LogError(" BBBBBBBBBBBBBBBBBBBBB));");
            if (!R.Note_Re()) return;
    
            var b =
                Initialize.Vector2Int比较
                (co.contacts[0].normal.normalized , -Initialize.方向To_v2(被破坏方向),bug );

            if (bug) Debug.LogError(Initialize.方向To_v2(被破坏方向) + "" + co.contacts[0].normal);
            if (b)
            {
                Debug.LogError("  (co.contacts[0].normal, -Initialize.方向To_v2(被破坏方向));");
                延迟销毁();
            }
            else
            {
                Debug.LogError("  NONONONONO");
            }
        }

    }
    [SerializeField]
    float 销毁延迟时间 = 0;
    //private void OnTriggerEnter2D(Collider2D c )
    //{ 
    //    if (c  .gameObject .layer==Initialize .L_Player)
    //    {
    //        var b=Initialize.Is_方向(-Player3 .I.Velocity, 被破坏方向); 
    //        if (b)
    //        {
    //            延迟销毁();
    //        }
    //        else
    //        {
    //            Player3.I.gameObject.transform.position = Player3.I.SafeWay;
    //            Player3.I.Velocity = Vector2.zero;
    //            Player3.I.  被扣血(atkvalue, this.gameObject);
    //        } 
    //    }
    //    else
    //    {
    //        c.gameObject.transform.position = Player3.I.SafeWay; 
    //        c.GetComponent<I_生命>()?.被扣血(atkvalue, this.gameObject);
    //    }
    //}
 void 开关(bool b=false )
    {
        if (!b)
        {
            Player3.I.LastV_Velocity();
      
            sp.enabled = false;
            bc.enabled = false;  
        }
        else
        {
     var a=       GetComponent<平台动画效果 >();
            sp.enabled = true;
            bc.enabled = true;
            //if (a!=null)
            //{
            //    sp.enabled = true;
            //    a.Q弹结束 += () => { bc.enabled = true; };
            //}
            //else
            //{
            //    sp.enabled = true;
            //    bc.enabled = true;
            //}

        }
        if (上海!=null)
        {
            上海.开关(b);
        }
    }    
}
