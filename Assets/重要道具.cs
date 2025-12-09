using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.LookDev;
using static 小地图显示;



public partial  class 重要道具 : MonoBehaviour
{
    //[Tooltip("拾取之后不刷新 等存档 保存状态")]
    //public bool 获取后销毁 = true;
    [Tooltip("拾取之后完成保存进度")]
    public bool 获取后保存 =false  ;
    [Space]
    [SerializeField]
    UnityEvent 被触发的事件;
    public string Anim_Name;
     
    Animator A;
    SpriteRenderer sp;
    BoxCollider2D bc;
    public GameObject 表面特效;
    [SerializeField ]
     GameObject 表面特效_;
    Player3.玩家能力 N => Player3.I.N_;
    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        A = GetComponent<Animator>();
      bc = GetComponent<BoxCollider2D>();
        if (bc==null )
        {
            bc = gameObject.AddComponent<BoxCollider2D>();
            bc.isTrigger = true;
        }
 
 
        if (GetComponent<生命周期管理>() == null)
        {
            销毁触发 +=()=> { 
                Dead();
            } ;
        }
      
       
    }
    private void Start()
    {

        if (Anim_Name ==""&& Anim_Name ==null)
            Anim_Name = gameObject.name;

        if (A==null)
        {
            Debug.LogError(gameObject .name );
        }
        else if (A.HasState(0, Animator.StringToHash(Anim_Name)))
        {
            A.Play(Anim_Name);
        }

        if (表面特效!=null)
        {
            if (表面特效_ != null) Destroy(表面特效_);
             表面特效_ = Instantiate(表面特效,transform );
            表面特效_.transform.localPosition = Vector2.zero;
            var a = 表面特效_.GetComponent<SpriteRenderer>();
    var an = 表面特效_.GetComponent<Animator >();
            an.Play("表面");
            sp.Copy_SpriteRenderto(a, 1);
        }
    }
    public void 加灵魂碎片( )
    {
        Player3.I.玩家数值.灵魂碎片++;
    }
    public void  激活地图(Transform s)
    {
        var a = s.GetComponent<相机框>();
        Vector2Int vi= new Vector2Int(gameObject.scene.buildIndex, a.编号);

        //Initialize_Mono.I.重制触发?.Invoke(gameObject.scene.buildIndex,GetComponent<监控激活碰撞框>().所属相机编号);
        小地图数据.I_.add(vi);  ///添加之后刷新  小地图状态但是不全显示
        消息.I.Come_on_Meesge("下个目标出现，按 M开启地图查看\n(双击M可以打开缩小大地图)");

        Initialize_Mono.I.小地图刷新?.Invoke();
    }
    public void 加钱(int    value)
    {
        Player3.I.玩家数值.钱 += value;
        if (value>40)
        {
            消息.I.Come_on_Meesge("获得花瓣："+value);
        }
        //Debug.LogError(Player3.I.玩家数值.钱);
        Player3.I.玩家数值.保存();
    }
    public void 开启格挡()
    {
        N.格挡=true;
        消息.I.Come_on_Meesge("你获得了新的能力");
    }
    public void 开启圆劈()
    {

        N.圆劈 = true;
        消息.I.Come_on_Meesge("你获得了新的能力");
    }
    public void 开启时缓()
    {
  
        N.时缓 = true;
        消息.I.Come_on_Meesge("你获得了新的能力");
    }
    public void 开启地图()
    {
        N.地图道具解锁  = true;
    }
    public void 开启爬墙()
    {
       N. 墙冲浪 = true;
        消息.I.Come_on_Meesge("你获得了新的能力");
    }
    public void 开启冲刺()
    {
        N.Dash = true;
        消息.I.Come_on_Meesge("你获得了新的能力");
    }
    public void 加攻击(float value)
    {
        Player3.I.当前hp += value;
    }
    public void 加血(float value)
    {
        Player3.I.当前hp += value;
    }
    public void 时缓冲()
    {
        N.时缓 = true;
        消息.I.Come_on_Meesge("你获得了新的能力");
    }
    public void 开启悬浮 ()
    {
        N.悬浮= true;
        消息.I.Come_on_Meesge("你获得了新的能力");
    }
    public void 开启空中Dash ()
    {
        N.空中Dash = true;
        消息.I.Come_on_Meesge("你获得了新的能力");
    }
    public void 开启下落攻击()
    {
        N.下落攻击  = true;
        消息.I.Come_on_Meesge("你获得了新的能力");
    }
    public void 开启上升攻击( )
    {
        N.上升攻击=true;
        消息.I.Come_on_Meesge("你获得了新的能力");
    }
  
    public void 开启Dash加速()
    {
        N.Dash加速 = true;
        消息.I.Come_on_Meesge("你获得了新的能力,滑铲后移动速度加快");
    }
    //public void Speed_Leave(  )
    //{
    //    脉冲.I.File(transform .position);

    //    Player3.I.SetSpeed(t);
    //    Player3.Public_Const_Speed = 0.1f;
    //}
    public void 加最大血(float value)
    {
        Player3.I.hpMax  += value;
 
            消息.I.Come_on_Meesge("血量最大上升：" + value);
 
        Player3.I.玩家数值.保存();
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject .CompareTag(Initialize.Player) )
    //    {
    //        if (Player3.I != null)
    //        {
    //             被触发的事件?.Invoke();
    //            销毁触发?.Invoke();
    //            if (获取后保存)
    //            {
    //                Player3.SaveAll();
    //            }

    //        }
    //    }
    //}
    Int不重复 IIIIIIIB = new Int不重复();
    private void OnTriggerEnter2D(Collider2D collision)
    {
 
        ///不重复
        if (!IIIIIIIB.Add(Time.frameCount)) return;
        if (collision.CompareTag(Initialize .Player) )
        {
            if (Player3 .I!=null)
            {
                     被触发的事件?.Invoke();  
                Event_M.I.Invoke(Event_M.刷新提示机关);

                销毁触发?.Invoke();
                if (获取后保存)
                {
                    Player3.SaveAll();
                } 
 
            } 
        }
    } 
}
public partial class 重要道具 : I_Dead, I_ReturnPool, I_deadTo_Re
{


    public Action 销毁触发 { get; set; }

    public Bounds 盒子 => bc.bounds;
     
    public bool Dead()
    {
        特效_pool_2.I.GetPool(transform.position, T_N.特效小Get).同步玩家 = true;
        if (sp == null ||bc==null)
        {
            Debug.LogError(gameObject);
        }

        Initialize_Mono.I.Waite(
            ()=> {
                if (表面特效_ != null)
                {
                    Destroy(表面特效_);
                }

            });

        sp.enabled = false;
        bc.enabled = false;

        if (GetComponent<生命周期管理>() == null)
        {
            if (Pool_Key_name==null || Pool_Key_name == "")
            { 
            }
            else
            {
                Debug.LogError("回归");
                Surp_Pool.I.ReturnPool(gameObject);
            }  
        }
        return true;
    }
 
    public string Pool_Key_name { get; set; }
    public void 重制()
    { 
        sp.enabled = true ;
        bc.enabled = true;
        Start();
    }
     
}

 
