using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using System;
using UnityEngine.Rendering.Universal;
using Cinemachine;
using System.Linq;
using Sirenix.OdinInspector;
using static 生命周期管理;

[Serializable ]
public struct   Value
{  
    [SerializeField] bool  Bool; 
    [SerializeField ]int  Int ;
    [SerializeField] float Float;
    [SerializeField] string String;
    [SerializeField] Vector2 v2;


    [SerializeField] bool bD;
    static string sD= "-999";
    static Vector2 vD =new Vector2 (99,99);
    static  float   fD = -99.90000000000000000f;
    static int iD= -999;
    public static Value Set(object v)
    {
        Value O = new Value();
        if (v is int) O.Int = Convert.ToInt32(v); else O.Int = iD; 
        if (v is float) O.Float = Convert.ToSingle(v); else O.Float = fD;

        if (v is bool) {
            O.bD = true;
            O.Bool = Convert.ToBoolean(v);
        } 
        if (v is string)  O.String = Convert.ToString(v); else  O.String = sD;
        if (v is Vector2) O.v2 = O.v2 = (Vector2)v; else O.v2 = vD;
        return O;
    }
    public  object Get()
    {
        if (bD) return Bool;
        if (Int != iD) return Int ;
        if (Float != fD) return Float;
        if (String != sD) return String;
        if (v2 != vD) return v2; 
        return null;
    }
}
 interface I_Save
{
    string Name { get; }
    void 保存();
    void 读取();
}
public partial class Player3 : BiologyBase
{
    public 控制粒子 子弹发射;
    public 圆斩跳  圆斩判定; 
    public new  Bounds Bounds
    {
        get
        {
            if (co == null)
            {
                return new Bounds();
            } 

            return 站立box .bounds;
        }
    }
    public bool 地面调试; 
    public static void  SaveAll()
    {
        Event_M.I.Invoke(Event_M.场景保存触发, Player3.I.gameObject);
        Player3.I.N_.保存();
        Player3.I.玩家数值.保存();
        DeadPla.I.保存();
        Save_D.Save();
    }
 
    public  Vector2 屏幕上的坐标
    {
        get
        {
            var a = 摄像机.I.Camera_Bounds; 
            return 摄像机.to_屏幕坐标(a, Player3.I.transform.position);
        }
    }
    new public Transform transform
    {
        get => base.transform;
    } 
    [DisplayOnly]
    public float 变速时间;
    float 真实时间;
    float 最后速度;
    float Speed_fixDeltaTime
    {
        get =>Time.fixedDeltaTime/Public_Const_Speed ;
    }
    [DisplayOnly]
    public float 游戏内的时钟;
    [DisplayOnly]
   [SerializeField]
  float 底层speed;

    [SerializeField] [DisplayOnly]
    float Public_Const_SpeedASD;
    [SerializeField]
    bool 调试模式;

    public bool 持续提升速度;
    public float  提升速度;

    public float 主动设置;
    static float Public_Const_Speed_ = 1;
    public Action Public_Speed_;
    public static float Public_Const_Speed
    {
        get
        {
            return Public_Const_Speed_;
        }
        set
        {
            if (Public_Const_Speed_ != value)
            {
                Public_Const_Speed_ = value; 
                Player3.I.Public_Speed_?.Invoke();
                //Debug.LogError("变速，由" + Public_Const_Speed_ + "变成" + value);
            }
        }
    }

    public 功能数值Base 玩家数值;
    [SerializeField]
    [DisplayOnly]
    玩家能力 显示;

    [Serializable]
    public class 玩家能力 : I_Save
    {
 public void 全解锁()
        {
            地图道具解锁 = true;

            空中Dash = true;
            Dash = true;
          爬墙 = true;
          下落攻击 = true;
          上升攻击 = true;
          悬浮 = true;
          格挡 = true;
          时缓 = true;
        }
   public  string  Name { get => "玩家能力数据";}
      public    void 保存( )
        {
 
            if (Player3.I.N_==null)
            {
                Player3.I.N_ = new 玩家能力();
            }
            string s = JsonUtility.ToJson(Player3.I.N_);
            Save_D.Add(Name, s);   

 
        }
        public  void   读取()
        {
            弹反蓄力教学模式 = false;
            教学模式 = false;

            if (Save_D.存档字典_.ContainsKey(Name))
            {
                Player3.I.N_ = Save_D.Load_Value_D<玩家能力>(Name,true ); 
            }
            else
            { 
                保存();
            }
        }
        public bool 弹反蓄力教学模式 { get; set; }
        public bool 教学模式 { get; set; }
        [SerializeField] public bool 地图道具解锁;
         
        [SerializeField] public  bool 空中Dash ;
        [SerializeField] public  bool Dash;
        [SerializeField] public  bool 爬墙 ;
        [SerializeField] public  bool 下落攻击 ;
        [SerializeField] public  bool 上升攻击 ;
        [SerializeField] public  bool 悬浮 ;
        [SerializeField] public  bool 格挡 ;
        [SerializeField] public  bool 时缓 ; 
 
    }
 
    public 判定框Base 判定框 { get; set; }
    public 玩家受伤效果 受伤 { get; set; }
    public static Player3 I { get; private set; }

    public DASH dundash { get; set; } = new DASH(0.2f, 40f, 10f, 1f, E_dash.下铲);
    public DASH skydash { get; set; } = new DASH(0.3f, 30f, 10, 1f, E_dash.空中);
    [NonSerialized]
    public AniContr_4 _4;
    private FSM F;
    public E_State State { get { return F.I_State_C.state; } }

    [DisplayOnly]
    public Vector2 监控;


    public float Wall_Way_Y;
    public float LastWall { get; set; }
    [DisplayOnly]
    public bool ladder;
    /// <summary>
    /// 前方是空的
    /// </summary>
    [SerializeField] [DisplayOnly] bool 顶死1;

    public float NB_Dash_Time { get; set; }
    public bool 顶死
    {
        get => 顶死1;
        set
        {
            if (顶死1 != value)
            {
 
                //Debug.LogError("变便便");
                顶死1 = value;
                顶到墙了?.Invoke(value);
            }
        }
    }
    public AnimationCurve AC;
    public AnimationCurve AC2;



    public   BoxCollider2D po;
    //void 播放特效(string s)
    //{
    //    特效_pool.I.GetPool(gameObject, s);
    //}
    void ASAD(DASH2 dASH)
    {
        StartCoroutine(开起来(dASH));
        dASH.恢复 -= ASAD;
    }
    IEnumerator 开起来(DASH2 dASH)
    {
        yield return new WaitForSeconds(dASH.冲刺冷却时间);
        dASH.冷却好了 = true;
    }

  Vector2 Last_Velocity;

    public Action 按下跳跃 { get; set; }
    public Action<KeyCode> 按下 { get; set; }
    public Action<KeyCode> 松开 { get; set; }
    public Action<KeyCode> 按住 { get; set; }
    public Action<bool> 顶到墙了 { get; set; }
    public Action<bool> 方向按住 { get; set; }
    public Action<bool> 方向改变_Action { get; set; }

    public 适应文字 适应文字;

  public 悬挂检测 悬挂 { get;private set; }

    public override void Flip()
    {
        Player_input.假装相反方向键(); 
        base.Flip();
    }
 
 public void 缓慢反向力(float 最低点,float 倍率=1)
    {
        float 百分比 =MathF .Abs (Velocity.x) / 玩家数值.常态速度 ;

        百分比 = Mathf.Clamp(百分比, 最低点, 1f);
       AddForce( LocalScaleX_Set * Vector2.right *  玩家数值.水平相反力 * 百分比);
        //Debug.LogError(百分比 + "  力度   " + LocalScaleX_Set * Vector2.right * 玩家数值.水平相反力* 倍率 * 百分比);
    }
    protected override void Awake()
    {  
        base.Awake();


        if (I != null && I != this)
        {
            Destroy(this);
        }
        else
        {
            I = this;
        }
        _4 = GetComponent<AniContr_4>();
        悬挂 = GetComponentInChildren<悬挂检测>();
        //朝向 = 1; 
        受伤 = GetComponent<玩家受伤效果>();
        判定框 = GetComponentInChildren<判定框Base>();
        po = GetComponent<BoxCollider2D>();
        F = GetComponent<FSM>();

        if (Player_input.I!=null)
        {
            Player_input.I.KeyDown += 按下_;
            Player_input.I.KeyUp += 松开_;
            Player_input.I.KeyState += 按住_;
            Player_input.I.方向变动 += 方向_;
        }
        else
        {
            Initialize_Mono.I.Waite(()=> {
                Player_input.I.KeyDown += 按下_;
                Player_input.I.KeyUp += 松开_;
                Player_input.I.KeyState += 按住_;
                Player_input.I.方向变动 += 方向_;
            });
        }



        原始Offset = po.offset;
        原始Size = po.size;

        Player_Father = transform.parent;

        Player3.I.玩家数值.读取();
        Player3.I.N_.读取();
    }


 
    [DisplayOnly]
    public float ladderX;
    public bool 碰到Ground;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == Initialize.L_Ladder.value)
        {
            ladder = true;
            ladderX = collision.transform.position.x;
        }
        if (collision.gameObject.layer == Initialize.L_Ground.value)
        {
            碰到Ground = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Initialize.L_Ladder.value)
        {
            ladder = true;
        }
        if (collision.gameObject.layer == Initialize.L_Ground.value)
        {
            碰到Ground = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.layer == Initialize.L_Ladder.value)
        {
            ladder = false;
        }
        if (collision.gameObject.layer == Initialize.L_Ground.value)
        {
            碰到Ground = false;
        }
    }
    public void 闪光()
    {
        Light2D light2D = GetComponentInChildren<Light2D>();
        if (light2D != null)
        {
            StartCoroutine(开闪一下(light2D));
        }
        else
        {
            Debug.LogError("灯光为空但是被调用，这是个空引用");
        }

    }

    IEnumerator 开闪一下(Light2D light2D)
    {
        light2D.enabled = true;
        yield return new WaitForSeconds(0.03f);
        light2D.enabled = false;
    }


    private void 按下_(KeyCode obj)
    { 
        按下?.Invoke(obj);
        if (obj == Player_input.I.跳跃)
        {
            按下跳跃?.Invoke();
        }
    }
    private void 松开_(KeyCode obj)
    {
        松开?.Invoke(obj);
    }
    private void 按住_(KeyCode obj)
    {
        按住?.Invoke(obj);

        if (obj == Player_input.I.左)
        {
            方向按住?.Invoke(false);
        }
        else if (obj == Player_input.I.右)
        {
            方向按住?.Invoke(true);
        }
    }
    public void 方向更新()
    {

        if (LocalScaleX_Int != Player_input.I.方向正负)
        {
            LocalScaleX_Int = Player_input.I.方向正负;
            Debug.Log("方向更新触发");
        } 

    }
    public void 方向改变(bool b)
    {
        transform.localScale = new Vector2((b ? 1 : -1), transform.localScale.y);
    }


    private void 方向_(int obj)
    {
        if (obj == -1)
        {
            方向改变_Action?.Invoke(false);
        }
        else if (obj == 1)
        {
            方向改变_Action?.Invoke(true);
        }
        Velocity = new Vector2(0, Velocity.y);

    }


    public float 距离 = 0.5f;

    public void To_SafeWay()
    {
 
        Initialize_Mono.I.Waite(() => {
            Player3.I.transform.position = SafeWay_;
            Player3.I.Velocity = Vector2.zero;
        },0.2f);


        主UI.I.遮罩动画();
    }

    private  Vector2 SafeWay_;

    [DisplayOnly]
    [SerializeField ]
    private Move_P 脚下1;
    public Move_P 脚下 { get => 脚下1; set => 脚下1 = value; }

   void FixedUpdate()
    {
        游戏内的时钟 += Time.fixedDeltaTime/Public_Const_Speed;
     
        if (持续提升速度)
        {
            Public_Const_Speed += 提升速度*Time .fixedDeltaTime;
        }

        if (Time.time - 真实时间>1.5f)
        {
            var 差值 = Player3.Public_Const_Speed - 底层speed;
            int 正负号 = Initialize.返回正负号(差值);
            var 绝对值 = MathF.Abs(差值);
            if (变速时间 != 0 && 绝对值 >= 0.02f)
            {
                ///上一秒经过的时间
                var b = (游戏内的时钟 - 变速时间-(1.5f/最后速度));
                var a = 绝对值 * b * b;

                Player3.Public_Const_Speed -= 正负号 * a;
                //Debug.LogError("游戏内的时钟:\n" + 游戏内的时钟 + " 变速时间:\n" + 变速时间 
                //    + "Speed:\n" + Public_Const_Speed 
                //         + "经过的:\n" + b
                //   + "减去:\n" + 正负号 * a); 
                if (绝对值 <= 0.02f)
                {
                    Player3.Public_Const_Speed = 底层speed;
                }
            }
        }


        if (备用地面 == 备用地面_Laset)
        {
            备用地面检测21 = false;
            //没有 接触   
        }
        else
        {
            备用地面检测21 = true;
            备用地面_Laset = 备用地面;
            // 有 接触   
        }

        var chaox = -(Player3.I.Bounds.center - Vector3 .zero).normalized;
        Debug.DrawRay(Player3.I.Bounds.center, chaox * 10f, Color.blue); 
    }
    public void 录入安全地点(bool 长=false )
    {
        float 距离 = 0.1f;
        if (长) 距离 = 3f;
        var a = Physics2D.Raycast(new Vector2(Bounds.center.x, Bounds.min.y), Vector2.down, 距离, 1 << Initialize.L_Ground);
        if (a.collider != null)
        {
            if (a.collider.gameObject.CompareTag(Initialize .Ground ))
            {
                SafeWay_ = transform.position;
            } 
        }
    }
    //float IS;
    [SerializeField]
    Transform 中检测;
    [SerializeField]
    Transform 下检测;
    [SerializeField]
    Transform 上检测;
    //[SerializeField]
    //Transform 上检测;
    public bool 顶上;
    public bool 上;
    public bool 中;
    public bool 下;
    public enum E_wall
    {
        OOOO,
        OIOO,
        IIII,
        OIII
    }
    public E_wall e_wall;

    List<Collider2D> 敌人碰撞_ = new List<Collider2D>();
    void 冲刺伤害()
    {
        if (NB_Dash_Time != 0)
        {
            NB_Dash_Time -= Time.deltaTime;
             
            var c = Physics2D.BoxCast(
                脚底中间,
                Bounds.size,
                0f,
                Vector2.up,
                1f,
                1 << Initialize.L_Enemy_hit_collision
                ).collider;
            //var c = Physics2D.OverlapCircle(Bounds.center, 10f, 1 << Initialize.L_Enemy_collision);
            if (c != null)
            {
                if (!敌人碰撞_.Contains(c))
                {
                    敌人碰撞_.Add(c);
                    var a = c.GetComponent<Enemy_base>();
                    if (a == null)
                    {
                        a = c.gameObject.transform.parent.GetComponent<Enemy_base>();
                    }
                    if (a != null)
                    {
                        var st = Initialize.Get_CutternAnimName(a.an);
                        if (st == Enemy_base.atk)
                        {
                            Initialize_Mono.I.时缓(0.4f, 0.25f);
                             伤害(a);

                        }
                    }
                }
            }
        }
        else
        {
            if (敌人碰撞_ != null)
            {
                敌人碰撞_.Clear();
            }
        }
    }
    public static bool Contains(int layer, LayerMask layerMask)
    {
        return (layerMask & 1 << layer) > 0;
    }
    int 备用地面;
    int 备用地面_Laset;
    private void OnCollisionStay2D(Collision2D co)
    {
        //if (co.gameObject.layer ==站立检测层. )
        {
            var 方向正确 = Initialize.Vector2Int比较(co.contacts[0].normal, Vector2.up);

            if (Contains(co.gameObject.layer, 碰撞检测层)
                &&方向正确
                && co.contacts[0].collider==po
                )
            {
                备用地面++;
                if (备用地面 > 1000)
                {
                    备用地面 = 0;
                }
            }

        }
    }
    //public BoxCollider2D 前档板;
    protected void Update()
    {
        
        {
            if (Player_input.I.按键检测_按下(Player_input.I.变速))
            {
                if (主 == null)
                {
                    Debug.LogError("主为空");
                    return;
                }
                I_假死 a;
                if (辅 != null)
                {
                    a = 主;

                    主 = 辅;
                    辅 = a;
                    Public_Const_Speed = 主.对象.GetComponent<I_Speed_Change>().Speed_Lv;
                }
                else
                {
                    辅 = 主;

                    Public_Const_Speed = 1;
                }


            }
        }
      

        if (Ground && !HPROCK && (FSM.f.I_State_C.state == E_State.run || FSM.f.I_State_C.state == E_State.idle))
        {
            录入安全地点();  
        }
        前后和头(1f, 0.1f);
        显示 = N_;
               Public_Const_SpeedASD = Public_Const_Speed;
        if (调试模式)
        {
           N_.全解锁();
            调试模式 = false;
            Public_Const_Speed = 主动设置;
        }
        if (速度调试 )
        {
            Public_Const_Speed = 主动设置;
            速度调试 = false;
        }

        冲刺伤害();


        //var AASD = Physics2D.OverlapCircle(Bounds.center, 10f, 1 << Initialize.L_Enemy_collision);
        //if (AASD !=null)     AASD.gameObject.transform.position = Vector3.zero;


        //if (Input.GetKeyDown(KeyCode.T ))
        //{
        //    if (Ground )
        //    {
        //        曲线(40, 80, 0.2f,AC);
        //    } 
        //}
        //顶上 = 悬挂.遮挡;
        下 = Physics2D.OverlapCircle(下检测.position, 0.1f, 碰撞检测层) != null;
        上 = Physics2D.OverlapCircle(上检测.position, 0.1f, 碰撞检测层) != null;
        中 = Physics2D.OverlapCircle(中检测.position, 0.1f, 碰撞检测层) != null; if (true)
            if (上 && 中 && 下)
            {
                e_wall = E_wall.IIII;
            }
            else
            {
                e_wall = E_wall.OOOO;
            }
        //else if (!顶上 && 上 && !中 && !下)
        //{
        //    e_wall = E_wall.OIOO;
        //}
        //else
        //{

        //    e_wall = E_wall.OOOO;
        //}
        //else if (顶上 && 上 && 中 && !下
        //    || 顶上 && 上 && !中 && !下)
        //{
        //    e_wall = E_wall.IIIO;

        //}
        //else if (!顶上 && 上 && 中 && 下
        //    || (!顶上 &&! 上 && 中 && 下))
        //{
        //    e_wall = E_wall.OIII;
        //}
        顶死 = e_wall == E_wall.IIII /*|| e_wall == E_wall.OIOO*/;

         Last_Velocity = Velocity;
        监控 = Velocity;
    }
    public void LastV_Velocity()
    {
        Velocity =  Last_Velocity;
    }
    public void asdasd()
    {

    }
    public void 水平起步加力(KeyCode obj)
    {
        if (obj == Player_input.I.左 ||
          obj == Player_input.I.右)
        {
            AddForce(new Vector2(Player_input.I.方向正零负 * 玩家数值.起步速度, 0));
        }

    }

    public void 地面水平速度限制if_uppdate()
    {
        if (Math.Abs(Velocity.x) >= 玩家数值.常态速度)
        {
            Velocity = new Vector2(玩家数值.常态速度 * Player_input.I.方向正零负, Velocity.y);
        }
    }
    public void 朝向update()
    {
        transform.localScale = new Vector2(Player_input.I.方向正负, transform.localScale.y);
    }

    public void 减速()
    {

    }
 



    /// <summary>
    /// 现在STAy中调用  只能当接触地面， 侧面和脚底一起接触会失效
    ///  enter  和Exite   不能用  因为MOVE——P   或者失去碰撞会不调用Exite 
    /// </summary>
    public bool 备用地面检测21
    {
        get => 备用地面检测2; set
        {
            备用地面检测2 = value;
        }
    }
    [SerializeField]
    [DisplayOnly]
    bool 备用地面检测2;
    public void 竖直限制()
    {
        if (Velocity.y < -玩家数值.最大下落速度)
        {
            Velocity = new Vector2(Velocity.x, -玩家数值.最大下落速度);
        }
    }
    public void 水平限制(float a)
    {
        if (MathF.Abs(Velocity.x) > 玩家数值.常态速度 * a)
        {
            Velocity = new Vector2(玩家数值.常态速度 * a * Player_input.I.方向正负, Velocity.y);
        }
    }

    public GameObject 互动物品 { get => 互动物品1; set => 互动物品1 = value; }
    [SerializeField]
    [DisplayOnly]
    private GameObject 互动物品1;
    /// <summary>
    /// XY是坐标     TIME是多久内到达
    /// </summary>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    /// <param name="sumTime"></param>
    public void 曲线(float X, float Y, float sumTime, AnimationCurve a)
    {
        if (开启) return;
        Debug.LogError("AA void 曲线(float X, float Y, float sumTime, AnimationCurve a void 曲线(float X, float Y, float sumTime, AnimationCurve aAAA");
        StartCoroutine(asdasd(X, Y, sumTime, a));
    }
    bool 开启;
    IEnumerator asdasd(float 距离X, float 距离Y, float sumTime, AnimationCurve a)
    {
        开启 = true;
        float time = 0;
        float 结果X;
        float 结果Y;
        Velocity = Vector2.zero;
        while (time < sumTime)
        {
            time += Time.deltaTime;
            float X = time / sumTime;


            结果Y = a.Evaluate(X) * 距离Y;
            结果X = X * 距离X;
            Velocity = new Vector2(LocalScaleX_Int * 结果X, 结果Y);
            yield return null;
        }
        //Velocity = Vector2 .zero;
        End = true;
        开启 = false;
        yield break;
    }

    public void 水平限制()
    {
        if (MathF.Abs(Velocity.x) > 玩家数值.常态速度)
        {
            Velocity = new Vector2(玩家数值.常态速度 * Mathf.Sign(Velocity.x), Velocity.y);
        }
    }
 
    /// <summary>
    ///   都是正面设置
    /// </summary>
    /// <param name="E"></param   >
    /// <param name="E_距离"></param>
    /// <param name="E_矢量"></param>
    /// <param name="E_位置"></param>
    /// <param name="M_poX"></param>
    /// <param name="M_poY"></param>
    /// <param name="M_forceX"></param>
    /// <param name="M_forceY"></param>
public     void 反作用力(Enemy_base E, float E_距离, Vector2 E_矢量, Vector2 E_位置,
       Vector2 M_po,
       Vector2 M_Force)
    {
        if (E != null)
     {
        if ((E.Bounds.center -  Bounds.center).magnitude < E_距离) 
        {
        E.p.SafeVelocity = new Vector2( LocalScaleX_Set * E_矢量.x, E_矢量.y); 
        E.transform.position += new Vector3( LocalScaleX_Set * E_位置.x, E_位置.x);
        } 
    } 
         
        var 悬空 =  悬空检测();
        var a = Physics2D.Raycast(new Vector2(反面脚底.x, Bounds.center.y), 反向, 碰撞检测层).collider == null;
 
 
        ///互作用力  
        if (!a)
        {///判断后面是不是空的 
            if (!悬空)
            { 
                 transform.position += new Vector3( LocalScaleX_Set *  M_po.x, M_po.y);
                 Velocity = new Vector2( LocalScaleX_Set * M_Force.x, M_Force.y);
            }
        }
    }
    /// <summary>
    /// 玩家无法操作
    /// </summary>
    /// <param name="当前dASH"></param>
    public void 输入DASH数据(DASH 当前dASH)
    {
        if (!当前dASH.冷却好了) return;
        当前dASH.冷却好了 = false;
        //Debug.LogError(当前dASH.冷却好了+"AAAAAAAAAAAAAAA");
        当前dASH.冲刺显示 = true;
        当前dASH.发力显示 = true;
        Player_input.I.输入开关 = false;
        当前dASH.冲刺持续时间_ = 当前dASH.冲刺持续时间;

        //StartCoroutine(进入某冲刺模式(当前dASH));

    }

    public void DASH数据重制(DASH 当前dASH)
    {
        强行退出DASH = false;
        当前dASH.冲刺显示 = false;
        Player_input.I.输入开关 = true;
        //当前dASH.冷却好了 = true;
    }
    IEnumerator 某冲刺结束(DASH 当前dASH)
    {
        强行退出DASH = false;
        if (当前dASH.冲刺显示 == true)
        {
            残影.I.开启残影(false);
            //Player.I.冲刺表示 = false;
            当前dASH.冲刺显示 = false;
            //冲刺动画结束?.Invoke();
            //if (空中DASH行为)
            //{
            //    空中DASH行为 = false;
            //}

            Player_input.I.输入开关 = true;
        }
        yield return new WaitForSeconds(当前dASH.冲刺冷却时间);
        当前dASH.冷却好了 = true;
    }
    public Vector2 脚底发射箱()
    {

        var b =
            Physics2D.BoxCast(
                 脚底中间,
                 new Vector2(Bounds.size.x - 0.1f, 0.1f),
                 0,
                 Vector2.down,
                 30f,
                 碰撞检测层
                );
        if (b.collider == null)
        {
            Debug.LogError("返回为0");
            return Vector2.zero;
        }
        return b.point;
    }
    public Vector2 脚底发射(float 距离)
    {
 
        Vector2 a = 脚底中间;
        var b = Physics2D.Raycast(a, Vector2.down, 距离, 碰撞检测层);
        if (b.collider == null)
        {
            return Vector2.zero;
        }

        if (b.point == Vector2.zero) Debug.LogError("返回为0");
        return b.point;
    }
    /// <summary>
    /// 检测30的距离,返回地面点
    /// </summary>
    /// <returns></returns>
    public Vector2 脚底发射()
    {
        Vector2 a = 脚底中间;
        var b = Physics2D.Raycast(a, Vector2.down, 30f, 碰撞检测层);
        if (b.collider == null || b.collider.gameObject.CompareTag(Initialize.MovePlatform))
        {
            Debug.LogError("返回为0");
            return Vector2.zero;

        }

        if (b.point == Vector2.zero) Debug.LogError("返回为0");
        return b.point;
    }
    public bool 保持Dash { get; set; }
    public bool 强行退出DASH { get; set; }
    public Action 受伤了 { get; internal set; }
    public override Action 被打 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }





    /// <summary>
    /// Fix调用
    /// </summary>
    /// <param name="朝向"></param>
    /// <param name="aSH"></param>

    public void Dash_(int 朝向, DASH aSH)
    {
        ///当dash_过程被打断，状态机内部不会引用这个,


        if (!aSH.冲刺显示) return;
        if (!aSH.发力显示) return;

        if (!保持Dash) aSH.冲刺持续时间_ -= Time.fixedDeltaTime;

        if (强行退出DASH) { aSH.冲刺持续时间_ = 0; }

        if (aSH.冲刺持续时间_ <= 0)
        {
            aSH.冲刺持续时间_ = 0;
            StartCoroutine(某冲刺结束(aSH));
        }
        else
        {
            if (aSH.冲刺持续时间_ / aSH.冲刺持续时间 < 1 / 4 && aSH.冲刺持续时间 > 0.2f)
            {
                float X = Mathf.Lerp(Velocity.x, 0, 0.5f);
                Velocity = new Vector2(X, 0);
            }
            else
            {
                float DashSpeed = (朝向 * aSH.冲刺速度 * aSH.冲刺持续时间_ / aSH.冲刺持续时间) + (朝向 * aSH.基础冲刺速度);
                float X = Mathf.Lerp(Velocity.x, DashSpeed, 0.5f);
                Velocity = new Vector2(X, 0);
            }

        }


    }
    //float 蹲后OffY = -1.55f;
 
    [DisableOnPlay]
   public BoxCollider2D  站立box ;

    [SerializeField ][DisableOnPlay]
    float 蹲后SizeY = 1.9f;
    Vector2 原始Offset;
    Vector2 原始Size;
    float 离地距离;
    public void 退出一半()
    {

        站立box.enabled = true;
        return;
        一半(false);
        transform.position = new Vector2(transform.position.x, transform.position.y + 离地距离);
    }
    public void 一半(bool b)
    {
        if (b)
        {
            po.size = new Vector2(po.size.x, 蹲后SizeY);
            var a = (原始Size.y - 蹲后SizeY) / 2;
            po.offset = new Vector2(po.offset.x, po.offset.y- a);
        }
        else
        {
            po.offset = 原始Offset;
            po.size = 原始Size;
        }

    }
    public void 进入一半()
    {
        站立box.enabled =false ;
        return;
        一半(true);
        var a = po;
        Vector2 最低点 = Vector2.zero;
        Vector2 碰撞点 = Vector2.zero;
        for (int i = 0; ; i++)
        {
            if (i>1000)
            {
                Debug.LogError("离谱离谱离谱");
                break;
            }
            float C = i * 0.3f;
            最低点 = new Vector2(a.bounds.min.x + C, a.bounds.min.y);
            碰撞点 = Physics2D.Raycast(最低点, Vector2.down, 1f, 碰撞检测层).point;//没碰到会返回为零
            if (碰撞点 != Vector2.zero) break;
        } 
        离地距离 = (最低点 - 碰撞点).y;
        //Debug.LogError(离地距离);
        transform.position = new Vector2(transform.position.x, transform.position.y - 离地距离);
    }

    public bool 头顶没有挤压()
    {
        if (!头空_)
        {
            return
       Physics2D.BoxCast(
      new Vector2(co.bounds.center.x, co.bounds.max.y),
       new Vector2(co.bounds.size.x - 0.5f, 0.01f),
       0f,
       Vector2.up,
       0.05f,
       碰撞检测层
       )
       .collider == null;
        }
        else
        {
            return true;
        }
    }
    public void 跳跃触发(Vector2 v)
    {
        I.Velocity = v;
    }
    public void 跳跃触发()
    {
        I.Velocity = new Vector2(I.Velocity.x, I.玩家数值.跳跃瞬间速度);
    }

    protected override void 离开地面_()
    {
     if(地面调试 )   Debug.Log("离开地面");

        离开地面事件?.Invoke();
    }

    public float 跳跃欲输入时间 = 0.1f;
    public bool 空中攻击过了;
    public bool 上升攻击过了;
    public bool 圆形攻击过了;
    protected override void 接触地面_()
    {
        圆形攻击过了 = false;
           上升攻击过了 = false;
        空中攻击过了 = false;
        玩家数值.跳跃剩余跃次数 = 玩家数值.最大跳跃次数;
        if (地面调试) Debug.Log("接触地面");
        接触地面事件?.Invoke();

        if (Player_input.I.D_I[Player_input.I.跳跃].down_State < 0.1)
        {
            按下跳跃?.Invoke();
        }
    }
    [SerializeField] LayerMask 碰撞检测层_;
    public override    LayerMask 碰撞检测层
    {
        get
        {
            return 碰撞检测层_;
            //return 1 << Initialize.L_Ground | 1 << Initialize.L_M_Ground | 1 << Initialize.L_Box_Ground
            //     | 1 << Initialize.L_Only_Ground;
        }
    }

    public bool BigHit=false ;

    int 不一致次数;


    protected override void 前后和头(float 距离, float DI横)
    {
  

        ///卡在边缘砖头
        ///移动平台的is triiger
        ///
        ///  备用地面的夹角情况 
        ///   悬在墙上
        float DD = Ground ? 0 : 0.3f;
         
        var DIs =
                         Physics2D.BoxCastAll(
              new Vector2(po.bounds.center.x, po.bounds.min.y),
               new Vector2(po.bounds.size.x - DD, 0.1f),
               0f,
               Vector2.down,
                0.2f+po.edgeRadius,
              碰撞检测层
               ) ;

        Collider2D DI=null;
        foreach (var item in DIs)
        {
            if (item.collider!=null)
            {
                if (item.collider.isTrigger == false )
                {
                    bool BB = Initialize.Get_碰撞(Initialize .L_Player ,item.collider .gameObject .layer);
                    if (BB)
                    {//该层被忽略了
                        break;
                    }
 
                    DI= item.collider;
                    break; 
                }
            }
        }
        if (地面调试) if (DI == null) Debug.LogError( "      备用：" + 备用地面检测21);
        if (地面调试)      if (DI != null)     Debug.LogError(DI+"      Trriger:"+DI .isTrigger+"      备用："+ 备用地面检测21);
        if (DI != null && DI.isTrigger == false)
        {
            Ground = true; 
        }
        else
        {  
            bool bB = false;
            if (Velocity.y == 0 && 备用地面检测21 && e_wall == E_wall.OOOO)
            {
                bB = true;
            }

            Ground = bB; 
        }
        //if (备用地面检测21 != Ground)
        //{
        //    不一致次数++;
        //    if (不一致次数 > 5)
        //    {
        //        Debug.LogError("不一致                     " + "备用地面检测21" + 备用地面检测21 + "           Ground" + Ground + "         " + 不一致次数);
        //    }
        //}
        //else
        //{
        //    不一致次数 = 0;
        //}
      ((Vector2)po.bounds.max).DraClirl();
        var tou =
        Physics2D.BoxCast(
       new Vector2(po.bounds.center.x, po.bounds.max.y),
        new Vector2(po.bounds.size.x - 0.5f, 1),
        0f,
        Vector2.up,
         距离,
   1 << Initialize.L_Ground | 1 << Initialize.L_M_Ground
   )
        .collider;
        if (tou == null)
        {
            头空_ = true;
        }
        else
        {
            头空_ = tou.isTrigger;
        }

        Collider2D A = Physics2D.BoxCast(
        new Vector2(po.bounds.min.x, po.bounds.center.y),
        new Vector2(0.001f, po.bounds.size.y - 0.4f),
        0f,
      Vector2.left,
   0.05f,
     碰撞检测层
        )
        .collider;
        var a = A == null|| A.isTrigger  ;

        Collider2D B = Physics2D.BoxCast(
 new Vector2(po.bounds.max.x, po.bounds.center.y),
 new Vector2(0.001f, po.bounds.size.y - 0.4f),
 0f,
       Vector2.right,
 0.05f,
 碰撞检测层
 )
 .collider;
        var b =B== null ||  B.isTrigger;

        switch (transform.localScale.x)
        {
            case -1:
                前空_ = a;
                后空_ = b;
                break;
            case 1:
                前空_ = b;
                后空_ = a;
                break;
        }
    }
}

public partial class Player3 : I_生命, I_攻击
{ 
    public void   伤害(I_生命 e,float value=0)
    {
        if (value == 0) value = atkvalue;
        e.被扣血(value,Player3.I.gameObject,0);
    }
    public bool is原Parent
    {
        get
        {
            return Player3.I.transform.parent == Player3.I.Player_Father;
        }
    }
    Transform Player_Father;
    public void ChangeFather(Transform father = null)
    { 
        if (father == null)
        {
            Debug.Log ("还原");
            if (transform .parent .gameObject .activeInHierarchy ==false )
            {
                Debug.Log ("父物体关闭，离谱了");
                GameObject a = new GameObject();
                a.transform.SetParent(transform .parent );
                gameObject.transform.SetParent(a.transform );
                transform.SetParent(Player_Father);
            }
            else
            {
                transform.SetParent(Player_Father);
            }

        }
        else
        {
            Debug.Log ("夫对象改变");

            transform.SetParent(father);
        }
    }
    protected override bool 灵魂
    {
        get
        {
            Player_input.I.输入开关 = 灵魂1;
            return 灵魂1;
        }
        set
        {
            Player_input.I.输入开关 = value;
            灵魂1 = value;
        }
    }
    public override Action 生命归零 { get; set; }
    [SerializeField]
 

    public override float 当前hp
    {
        get { return 玩家数值.当前Hp; }
        set
        {
            if (value < 玩家数值.当前Hp)
            {
                if (!HPROCK)
                {
                    玩家数值.当前Hp = value;
                }
            }
            else if (value > 玩家数值.当前Hp)
            {
                玩家数值.当前Hp = value;
            }
        }
    }
    public override float hpMax { get => 玩家数值.Max_Hp; set => 玩家数值.Max_Hp = value; }
    [SerializeField]
    bool HPROCK_;
    public override bool HPROCK
    {
        get => HPROCK_; set
        {
            HPROCK_ = value;
        }
    }
    public override float atkvalue { get => 玩家数值.Atk; set => 玩家数值.Atk = value; }


    public override void 扣最大上限(float i)
    {

    }
    void 变速特效(float f)
    {
        if (MathF.Abs(Player3.Public_Const_Speed - f) > 1)
        {
            脉冲.I.File(Player3.I.transform.position);
            Initialize_Mono.I.时缓(0.1f, 0.8f);
        }
        else
        {
            脉冲.I.File(Player3.I.transform.position, 0.01f);
            Initialize_Mono.I.时缓(0.1f, 0.2f);
        }
    }
    I_假死 辅;
    I_假死 主;
    I_假死 当前;
    void 切换(I_Speed_Change I)
    {
        if (当前 != null)
        {
            当前.假死了(false);
        }
        ///假死
        var a = I.对象.GetComponent<I_假死>();
        if (a != null) a.假死了(true);
        主 = a;
        当前 = a;
    }
    No_Re RR=new No_Re ();

    public void 同速_(I_Speed_Change I)
    {
        if (!RR.Note_Re()) return;

        ///切换相关
        //var f = I.Current_Speed_LV;
        //if (f == 0)
        //{
        //    f = I.Speed_Lv;
        //}  

        //切换(I);
        //if (I.Speed_Lv== I.Current_Speed_LV)
        //{
        //    底层speed
        //}

        var f = I.Current_Speed_LV;
        if (Player3.Public_Const_Speed == f)
        {
            Debug.Log(Player3.Public_Const_Speed + "      等级一致      " + f);
            return;
        }

        变速特效(f);

        Player3.Public_Const_Speed = f; 
            底层speed = I.Speed_Lv; 

        I.变速触发?.Invoke();
        变速时间 = 游戏内的时钟;
        最后速度 = f;
       真实时间 = Time.time;
    }

    public enum 防御   //最开始的小兵大量消耗格挡条  第二个消耗小  让各党，拿了第二个之后i第二个简单第一个难了
    {
        Null,
        可以防御,
        开始防御,

        防御反击,

        反击攻击,
    }
    public 防御 防御状态;

    public 玩家能力 N_;

    [DisplayOnly]
    public Fly_Ground Fly;


    public Func<  GameObject, bool> Hit_FuncFSM;

    No_Re RRR = new No_Re();

    public Vector2  受伤Force; 
    public bool 硬抗;

    Int不重复 IIIIIIIB = new Int不重复();
    /// <summary>
    /// 999 秒杀
    /// </summary>
    /// <param name="i"></param>
    /// <param name="obj"></param>
    public override void 被扣血(float i, GameObject obj, int SKey=0)
    { ///不能反弹碰撞伤害   
        if (SKey == 0) SKey = Initialize.Get_随机Int();
        if (!IIIIIIIB.Add(SKey)) return;
        if (i!=999)
        {
            if (HPROCK) return;
        }

        bool 受伤伤 = true;
 
        if (Hit_FuncFSM!=null&& i != 999)
        {
 
            受伤伤 = Hit_FuncFSM.Invoke(obj);
        } 
        if (受伤伤)
        {  
            受伤.EnterHit(i,0, obj,硬抗);
             
            当前hp -= i;
            if (!硬抗) 受伤了?.Invoke(); 
        }
        硬抗 = false;
        受伤Force = Vector2.zero;
    }

    public override void 扣攻击(float i)
    {

    }
 [DisplayOnly]
    [SerializeField ]
    private bool atk;
    /// <summary>
    /// 动画事件
    /// </summary>
    public bool Atk { get => atk; set => atk = value; }
    /// <summary>
    /// 动画事件
    /// </summary>
    public bool End { get; set; }
    public void ATK()
    {
        Atk = true;
    }
    public void END()
    {
        End = true;
    }
}