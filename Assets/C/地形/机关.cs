using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Cinemachine;
using System;

public class 感应: MonoBehaviour
{
 protected   Animator an;
    protected  Collider2D bc;

    public float 玩家检测范围;

    [SerializeField]
    private bool 玩家进入了检测范围1;

    protected  virtual bool 玩家进入了检测范围 { get => 玩家进入了检测范围1; set => 玩家进入了检测范围1 = value; }

    private void Awake()
    {
        an = GetComponent<Animator>();
        if (GetComponent< Collider2D>() != null)
        {
            bc = GetComponent< Collider2D>();
        }
        else
        {
            bc = gameObject.AddComponent< Collider2D>();
        }

        bc.isTrigger = true;
    }

 protected   virtual  void Update()
    {
        if (玩家检测范围 !=0)
        {
            var a = transform.localScale.x;
            //var s=   Physics2D.RaycastAll(transform.position, new Vector3(a, 0, 0), 1 << LayerMask.NameToLayer("Player") );
            //   玩家进入了检测范围 = s == null;
            玩家进入了检测范围 = Physics2D.Raycast(transform.position, new Vector3(a, 0, 0), 玩家检测范围 , 1 << LayerMask.NameToLayer("Player"));
            Debug.DrawRay(transform.position, new Vector3(a* 玩家检测范围, 0, 0)  , Color.red);
        }
    }
    int EFc;
    int Fc;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject ==Player3 .I.gameObject && Fc != Time.frameCount)
        { 
                Fc = Time.frameCount;
     
            if (玩家检测范围 != 0) return; 
            玩家进入了检测范围 = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject ==  Player3.I.gameObject && EFc != Time.frameCount)
        {
            EFc = Time.frameCount;
            if (玩家检测范围 != 0) return;
            玩家进入了检测范围 = false;
        }
    }
}
interface I_Inter_action
{
    public void Inter_action();
}

public class 交互_Base: 感应, I_Inter_action, I_Dead
{
    [Tooltip("符合这个方向或者是空就触发")]
    [DisableOnPlay]
    [SerializeField]
    E_方向 Ctrtten;
    public Bounds 盒子 => default;
    [SerializeField]
    UnityEvent 被触发的事件;

    [SerializeField]
    UnityEvent Enter;

    [SerializeField]
    UnityEvent Exite;

    [SerializeField]
    UnityEvent Stay;

    protected override bool 玩家进入了检测范围
    {
        get { return base.玩家进入了检测范围; }
        set
        {
            if (base.玩家进入了检测范围 != value)
            {
                if (value)
                {
                    /// 触发器打开
                    /// 相同方向    或者      方向为空
                    bool B =
                        (transform.你在我的哪里(Player3.I.transform).is_四方向比较(Ctrtten.方向To_v2())
                        || Ctrtten == E_方向.Null);
                    if (B)
                    { 
                        Enter?.Invoke();
                    } 
                }
                else
                {
                    Exite?.Invoke();
                }
                base.玩家进入了检测范围 = value;
            }
        }
    }
    public void Inter_action()
    {
        //Debug.LogError("c void Inter_action()c void Inter_action()");
        //被玩家外部调用
        if (!玩家进入了检测范围) return;
        if (被触发的事件!=null) 被触发的事件?.Invoke();

    }

    /// <summary>
    /// 一次性机关
    /// </summary>
    [SerializeField] [DisplayOnly] protected   bool 不在检测玩家嘛;

    public GameObject 标识;
    protected override      void Update()
    {
        base.Update();
        if (!不在检测玩家嘛)
        {
            if (标识 != null) 标识.SetActive(玩家进入了检测范围);
        }
        if (玩家进入了检测范围)
        {
            if (Stay != null)
            {
                Stay.Invoke();
            }
        }

        if (玩家进入了检测范围 &&
            Player_input.I.按键检测_按下(Player_input.I.交互))
        {
            Inter_action();
        }
    }

    public Action 销毁触发 { get; set; }



    public bool Dead()
    {
        玩家进入了检测范围 = false;
        bc.enabled = false;
        return true;
    }
}

public partial class 机关 : 交互_Base
{

    //public void 
    public void 弹性(GameObject   a)
      {
        Initialize_Mono.I.Waite(
            () => {
                StartCoroutine(a.transform.Q弹(0.8f, 0.5f, true, true, null, false));
            },
            0.05f
            );
     }
    public void 大特效(GameObject a)
    {
        var B = 特效_pool_2.I.GetPool(a.transform.position  ,T_N.特效大爆炸);
    }
    public void 销毁触发_()
    {
        销毁触发?.Invoke();
    }

    public void  不在检测玩家( )
    {
        不在检测玩家嘛 = true;
        标识.SetActive(false);
    }
  
    public  void U_Event_()
    {
        //摄像机.I.FOV_缩放();
    }
    private void Start()
    {
        if (标识!=null)
        {
            标识.SetActive(false);
        }
    }

    public void 相机激活 (CinemachineVirtualCameraBase vcam )
    {
        var brain = 摄像机.I.Brain;
 
        cv = vcam;
            if (brain == null || vcam == null)
                return;
        if (brain.ActiveVirtualCamera != (ICinemachineCamera)vcam)
            //brain.SoloCamera = vcam.;
                vcam.MoveToTopOfPrioritySubqueue(); 
    }
    CinemachineVirtualCameraBase cv;

    public void 传送至(Transform T)
    {
        Player3.I.transform.position = T.position; 
    }
    public void 原地提示(string s)
    { 
        var a= GetComponentsInChildren<适应文字>(true);
        if (a!=null&&a.Length>0)
        {
            适应 = a[0];
        } 
        if (适应==null)
        {
            Debug.LogError("子物体没有适应文字组件");
        }
        else
        {
            Debug.LogError("设置了");
            适应.开关(true);
            适应.SetText(s);
        }
    }
 
  public  适应文字 适应;

    public void 跟随提示(string  s)
    { 
        适应 = Player3.I.适应文字;
        适应.开关  (true);
        适应.SetText(s);
    }
    public void 强提示a(GameObject OBJ)
    {
 
        主UI.I.强提示.开(OBJ); 
    }
    public void 关闭提示()
    {
        适应.SetText(null);
        适应.开关(false );
        适应 = null;
    }
    public void Boss杀手(bool B)
    {
        Player3.I.玩家数值.Boss杀手 = B;
    }
    public void 弹簧()
    {
        an.Play("弹簧");
    }
    public void 弹反蓄力教学(bool b)
    {
        Player3.I.N_.弹反蓄力教学模式 = b;
    }
    public void 教学(bool b)
    { 
        Player3.I.N_.教学模式 =b;
    }
    public void 相机还原()
    {
 
           摄像机.I.c.MoveToTopOfPrioritySubqueue();
    }
    public void 玩家加加加加加加速Y(float y)
    {
        if (Player3.I.Velocity.y < y)
        {
            Debug.LogError("触发触发触发触发");
            Player3.I.Velocity = new Vector2(Player3.I.Velocity.x,   y);
        }
        else
        {
            Debug.LogError("触发触发触发触发"+ Player3.I.Velocity.y);
        }

    }
    public void  玩家加速Y(float y)
    { 
            Player3.I.Velocity = new Vector2(Player3.I.Velocity.x, y); 
    }

}
