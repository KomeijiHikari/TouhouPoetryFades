using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Cinemachine;
using System;

public class 感应: MonoBehaviour
{
    [SerializeField] protected bool Deb;
    protected   Animator an;
    protected  Collider2D bc;
    protected SpriteRenderer sp;
    public float 玩家检测范围;

    [SerializeField]
    private bool 玩家进入了检测范围1;

    protected  virtual bool 玩家进入了检测范围 { 
        get => 玩家进入了检测范围1;
        set {

            if (玩家进入了检测范围1 != value)
            { 
                if (Deb) Debug.LogError("玩家进入了检测范围1" + value + gameObject.name + transform.position);
            }
        
            玩家进入了检测范围1 = value;
        } }

    protected void Awake()
    {

        sp = GetComponent<SpriteRenderer>();
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

        StartC = sp.color;
    }
    [SerializeField]
   protected Color StartC;
    protected   virtual  void Update()
    {
        玩家进入了检测范围 = StayTrue;
        if (玩家进入了检测范围)
        {
         sp.color = Color.black;
        }
        else
        {
            sp.color = StartC;
        }
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
    //private void OnTriggerEnter2D(Collider2D collision)
    //{ 
 
    //    if (collision.gameObject ==Player3 .I.gameObject && Fc != Time.frameCount)
    //    {  
    //        Fc = Time.frameCount; 
    //        if (玩家检测范围 != 0) return;
    //        if (Deb) Debug.LogError("玩家进入了检测范围1" + gameObject.name + transform.position);
    //        if (Deb)
    //        {
    //            Debug.LogError(bc+" "+ bc.enabled);
    //        }
    //        if (bc==null||!bc.enabled) return; 
    //        玩家进入了检测范围 = true; 
    //    }
    //}
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == Player3.I.gameObject && Fc != Time.frameCount)
        {
            Fc = Time.frameCount;
            if (玩家检测范围 != 0) return;
            if (Deb) Debug.LogError("玩家进入了检测范围1" + gameObject.name + transform.position);
            if (Deb)
            {
                Debug.LogError(bc + " " + bc.enabled);
            }
            if (bc == null || !bc.enabled) return;
            StayTrue = true;
        }
    }
    bool StayTrue;
    private void LateUpdate()
    {
        StayTrue = false;
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject ==  Player3.I.gameObject && EFc != Time.frameCount&&collision==Player3.I.站立box)
    //    {
    //        EFc = Time.frameCount;
    //        if (玩家检测范围 != 0) return;
    //        玩家进入了检测范围 = false;
    //    }
    //}
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
    //public Bounds 盒子 => bc.bounds;
    public Bounds 盒子
    {
        get {
            if (sp != null) return sp.bounds;

            //Debug.LogError("交互_Base 盒子 盒子 盒子 盒子 盒子 盒子 "+bc.bounds.size);
            return new Bounds(bc.bounds.center, bc.bounds.size) ; }

    }
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
            Player_input.I.按键检测_按下(Player_input.I.k.交互))
        {
            Inter_action();
        }
    }

    public Action 销毁触发 { get; set; }

  protected  void 开关(bool b)
    {
        ///bug1  记录
        ///bouns 异常 指定为bcbouns size为0  指定为localscale size固定为1（碰撞适应原因）
        ///导致  在重叠状态打开（未能正常检测canlive） 然后因为重叠再次关闭  
        ///BUG表现为 sp没有打开但是触发事件
        ///
        ///bug2  记录 
        ///如果正常关掉bc en abled  会在当前帧再次触发一次

        if (sp != null) 
        {
            sp.enabled = b;
            Debug.LogError(" sp.enabled   sp.enabled " + sp.enabled);
        }
        

        bc.enabled = b;

        if (!b) ///关闭 的画trriger会在当前帧在触发一次 因此关闭bc.nabled
                /// 后 延时一帧再设置玩家进入了检测范围为false
        {
            StartCoroutine(Initialize.Waite(() => 玩家进入了检测范围 = false));
        }
        else
        {
            玩家进入了检测范围 = false;
        }

       

        //enabled = b;
    }

    public bool Dead()
    { 
        开关(false); 
        return true;
    }
}

public partial class 机关 : 交互_Base, I_Revive
{
   
    [SerializeField] private bool re;
    [SerializeField] private float re_Time;

    public bool Re { get => re; set => re = value; }
    public float Re_Time { get => re_Time; set => re_Time = value; }
     
    public bool 重制()
    {
        开关(true);
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
    public void X位移(Transform T)
    {
        Player3.I.transform.position = new Vector3
            (T.position.x+(3*Player3.I.LocalScaleX_Int), Player3.I.transform.position.y, Player3.I.transform.position.z);
    }
    public void Y位移(Transform T)
    {
        Player3.I.transform.position = new Vector3
             (Player3.I.transform.position.x,T.position.y, Player3.I.transform.position.z);
    }
    public void 传送至(Transform T)
    {
        Player3.I.Dash传送触发 ?.Invoke(T.gameObject.GetInstanceID());
       
        Player3.I.transform.position = T.position; 
    }
    public void  ChangeSpeed(float t)
    {
        if (Player3.Public_Const_Speed != t)
        {
            Player3.I.SetSpeed(t);
            //Player3.Public_Const_Speed = t;
            Player3.I.变速特效(t);
        } 
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
        if (适应 == null) return;
        适应.SetText(null);
        适应.开关(false );
        //适应 = null;
    }
    public void Boss杀手(bool B)
    {
        Player3.I.玩家数值.Boss杀手 = B;
    }
    public void 弹簧()
    {
        ///速度一一下通过时。Retime 为0.4f 
        ///5一下为0.4/8 依次类推
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
