using BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs;
using Cinemachine;
using Cysharp.Threading.Tasks;
using Ink.Parsed;
using ItemMager;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine; 
using UnityEngine.Rendering.Universal;
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
    public bool DashDeb;
    public Action<int> Dash传送触发;
    public 控制粒子 子弹发射;
    public 圆斩跳 圆斩判定;
    public new Bounds Bounds
    {
        get
        { 

            if (站立box.enabled)
            {
            return 站立box.bounds; 
            }
            else if(站立box.enabled)
            {
                return 蹲BOX.bounds;
            }
            else
            {
                Debug.LogError("两个都关闭");
                return new Bounds();
            }
        }
    }
    public bool 地面调试;

    public static void SaveAll()
    {
        Save_D.Add(SpeedMager.Public_Const_Speed_Name, Player3.Public_Const_Speed);
        Save_D.Add(SpeedMager.Public_Const辅助_Speed_Name, SpeedMager.I.Last副Speed1Leve);
        Event_M.I.Invoke(Event_M.场景保存触发, Player3.I.gameObject);
        if (Player3.I.加速了)
        {
            Player3.I.加速(false);
        }
        Save假tr实例(TsL_I); 
        Player3.I.冷却全部好();
        Player3.I.N_.保存();
        Player3.I.玩家数值.保存();
        DeadPla.I.保存();
        所有物品管理.I.save();
        Save_D.Save();
    }


    [Obsolete("已经被弃用了", false )]
    public Vector2 屏幕上的坐标
    {
        get
        {
            var a = 摄像机.I.Camera_Bounds;
            return 摄像机.to_屏幕坐标(a, Player3.I.transform.position);
        }
    }  
    [SerializeField]
    [DisplayOnly]
    float Public_Const_SpeedASD;


    public static float Public_Const_Speed
    {
        get { return SpeedMager.Public_Const_Speed; }
    }


    public 功能数值Base 玩家数值;
    [SerializeField]
    [DisplayOnly]
    玩家能力 显示;

    [Serializable]
    public class 玩家能力 : I_Save
    {
        public enum E_玩家能力
        {
            圆劈,
            墙冲浪,
            上升攻击,
            空中Dash,
            时缓
        }
        public void 全解锁(bool b = true)
        {
            地图道具解锁 = b;

            Dash加速 = b;
            圆劈 = b;
            空中Dash = b;
            Dash = b;
            墙冲浪 = b;
            下落攻击 = b;
            上升攻击 = b;
            悬浮 = b;
            格挡 = b;
            时缓 = b;
            速度切换 = b;
        }
        public string Name { get => "玩家能力数据"; }
        public void 保存()
        {

            if (Player3.I.N_ == null)
            {
                Player3.I.N_ = new 玩家能力();
            }
            string s = JsonUtility.ToJson(Player3.I.N_);
            Save_D.Add(Name, s);
        }
        public void 读取()
        {
            弹反蓄力教学模式 = false;
            教学模式 = false;

            if (Save_D.存档字典_.ContainsKey(Name))
            {
                Player3.I.N_ = Save_D.Load_Value_D<玩家能力>(Name, true);
            }
            else
            {
                保存();
            }
        }
        public bool 弹反蓄力教学模式 { get; set; }
        public bool 教学模式 { get; set; }
        public bool 地图道具解锁
        {
            get => 地图道具解锁1; set
            {
                Debug.LogError("改变");
                地图道具解锁1 = value;
            }
        }

        [SerializeField] private bool 地图道具解锁1;

        [SerializeField] public bool Dash;
        [SerializeField] public bool 空中Dash;
        [SerializeField] public bool 墙冲浪;
        [SerializeField] public bool 上升攻击;
        [SerializeField] public bool 悬浮;
        [SerializeField] public bool 半灵;
        [SerializeField] public bool 格挡;

        [SerializeField] public bool 时缓; 
        [SerializeField] public bool 圆劈;
        [SerializeField] public bool 速度切换;
        [SerializeField] public bool 箭矢弹反;

        [SerializeField] public bool 攻击打断;
        [SerializeField] public bool 下落攻击;
        [SerializeField] public bool Dash加速;
        [SerializeField] public bool 无限圆劈;
        [SerializeField] public bool 速度视野;
    }

    public 判定框Base 判定框 { get; set; }
    public 玩家受伤效果 受伤 { get; set; }
    public static Player3 I { get; private set; }

    public void 冷却全部好()
    {
        dundash.冷却好了 = true;
        skydash.冷却好了 = true;
    }
    public DASH dundash { get; set; } = new DASH(0.07f, 40f, 20f, 1f, E_dash.下铲);
    public DASH skydash { get; set; } = new DASH(0.3f, 30f, 10, 1f, E_dash.空中);
    [NonSerialized]
    public AniContr_4 _4;
    private FSM F;
    public E_State State { get { return F.I_State_C.state; } }

    [DisplayOnly]
    public Vector2 监控;

    public void 消弹( )
    {
  ((atk)F.Getstate(E_State.atk))  .   消弹( );
    }
    public float Wall_Way_Y;
    public float LastWall { get; set; }
    [DisplayOnly]
    public bool ladder;
    /// <summary>
    /// 前方是空的
    /// </summary>
    [SerializeField][DisplayOnly] bool 顶死1;

    /// <summary>
    /// 传送相关
    /// </summary>
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



    public BoxCollider2D 蹲BOX;
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

    public 悬挂检测 悬挂 { get; private set; }

    public override void Flip()
    {
        Player_input.假装相反方向键();
        base.Flip();
    }

    public void 缓慢反向力(float 最低点, float 倍率 = 1)
    {
        float 百分比 = MathF.Abs(Velocity.x) / 玩家数值.常态速度;

        //百分比 = Mathf.Clamp( 百分比, 最低点, 1f);
        //Debug.LogError(百分比);
        //百分比 = 1;
        AddForce(Time.fixedDeltaTime * LocalScaleX_Set * Vector2.right * 玩家数值.水平相反力 * 百分比);
        //Debug.LogError(百分比 + "  力度   " + LocalScaleX_Set * Vector2.right * 玩家数值.水平相反力* 倍率 * 百分比);
    }
    protected override void Awake()
    {
        if (I != null && I != this) Destroy(this);
        else I = this;
        base.Awake();


   
        _4 = GetComponent<AniContr_4>();
        悬挂 = GetComponentInChildren<悬挂检测>();
        //朝向 = 1; 
        受伤 = GetComponent<玩家受伤效果>();
        判定框 = GetComponentInChildren<判定框Base>();
        蹲BOX = GetComponent<BoxCollider2D>();
        F = GetComponent<FSM>();

        if (Player_input.I != null)
        {
            Player_input.I.KeyDown += 按下_;
            Player_input.I.KeyUp += 松开_;
            Player_input.I.KeyState += 按住_;
            Player_input.I.方向变动 += 方向_;
        }
        else
        {
            Initialize_Mono.I.Waite(() =>
            {
                Player_input.I.KeyDown += 按下_;
                Player_input.I.KeyUp += 松开_;
                Player_input.I.KeyState += 按住_;
                Player_input.I.方向变动 += 方向_;
            });
        }



        原始Offset = 蹲BOX.offset;
        原始Size = 蹲BOX.size;

        Player_Father = transform.parent;
        生命归零 += () => {
            Debug.LogError("AAAAAAAAAAAAAA");

            Vector2Int V = 摄像机.I.相机框Int;
            Initialize_Mono.I.重制触发?.Invoke(V.x,V.y);

            LoadAll().Forget(); 
        };

        
        Initialize_Mono.I.重制触发 += (int i, int L) =>
        {
            More_SafeWay_ = Vector2.zero;
            Initialize_Mono.I.Waite(() =>
            {
                SafeWay_ = Player3.I.transform.position;
            }, 0f);
        };

        原速度 = 玩家数值.常态速度;
        加速度 = 原速度 * 1.5f;
    }
    public async UniTask LoadAll()
    {
        Initialize_Mono.LoadPla_and_D();
        所有物品管理.I.从存档刷新();
        Player3.I.玩家数值.读取();
        Player3.I.N_.读取(); 
        SpeedMager.I.Load(); ///玩家前，SM这会儿没有
    }
    float 原速度;
    float 加速度;
    private void Start()
    {
        变速 = Player_input.I.Get_key((Player_input.I.k.变速));
        LoadAll().Forget();
        Initialize_Mono.I.重制触发 += (int i, int l) => {
            Initialize_Mono.I.Waite(() =>
            {
                安全地点(true);
            }, 0.02f);

        };

    }
    public bool 加速了;
    public void 加速(bool f)
    {
        if (Player3.I.N_.Dash加速)
        {
            if (f)
            {

                玩家数值.常态速度 = 加速度;
                加速了 = true;
                残影.I.开启残影(true);
            }
            else
            {
                玩家数值.常态速度 = 原速度;
                加速了 = false;
                残影.I.开启残影(false);
            }

        }

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
        Debug.LogError("AAAAAAAAAAAAAAA");
        Initialize.闪光(sp, 0.2f, true);
    }

    //IEnumerator 开闪一下(Light2D light2D)
    //{
    //    light2D.enabled = true;
    //    yield return new WaitForSeconds(0.03f);
    //    light2D.enabled = false;
    //}


    private void 按下_(KeyCode obj,int i )
    {
        if (i != 0) return;
        按下?.Invoke(obj);
        if (obj == Player_input.I.k.跳跃)
        {
            按下跳跃?.Invoke();
        }
    }
    private void 松开_(KeyCode obj, int i)
    {
        if (i != 0) return;
        松开?.Invoke(obj);
    }
    private void 按住_(KeyCode obj, int i)
    {
        if (i != 0) return;
        按住?.Invoke(obj);

        if (obj == Player_input.I.k.左)
        {
            方向按住?.Invoke(false);
        }
        else if (obj == Player_input.I.k.右)
        {
            方向按住?.Invoke(true);
        }
    }
    public void 方向更新()
    {

        ///方向不一样 
        if (LocalScaleX_Set != Player_input.I.方向正负)
        {
             ///方向不一样，尺寸正确了
            Debug.LogError(Player_input.I.方向正负+"        "+ LocalScaleX_Set);
            LocalScaleX_Int = Player_input.I.方向正负;
            Debug.Log("方向更新触发");
        }
        else
        {
            ///方向 一样 
            if (MathF.Abs(transform.lossyScale.x) != 1)
            {
                // 尺寸   不对对 
                LocalScaleX_Int = Player_input.I.方向正负;
            }
        }

    }
    public void 方向改变(bool b)
    {
        if (transform.lossyScale.x == transform.localScale.x)
        {
            transform.localScale = new Vector3((b ? 1 : -1), transform.localScale.y, 1);
        }
        else
        {

        transform.localScale = new Vector3((b ? -1 :  1), transform.localScale.y, 1);
        }
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
        if (is土狼时间_Wall == 0)
            if (禁止朝向 == 0)
                Velocity = new Vector2(0, Velocity.y);

    }


    public float 距离 = 0.5f;

    public void To_SafeWay()
    {
        yalaAudil.I.EffectsPlay("PlayerHit", 1);

        Initialize_Mono.I.Waite(() =>
        {
            Player3.I.transform.position = SafeWay_;
            Player3.I.Velocity = Vector2.zero;
        }, 0.2f);


        主UI.I.遮罩动画();
    }


    [SerializeField]
    [DisplayOnly]
    private Vector2 SafeWay_;
    [DisplayOnly]
    private Vector2 More_SafeWay_;
    [DisplayOnly]
    [SerializeField]
    private Move_P 脚下1;

    public int MovePTimef;
     
 public void 对齐脚下()
    {
        var hit = Physics2D.BoxCast(new Vector2(Bounds.center.x, Bounds.min.y),
new Vector2(Bounds.size.x, 0.1f), 0, Vector2.down, 1f, 1 << Initialize.L_M_Ground |Initialize.L_Ground);

        if (hit.point == Vector2.zero) return;
        if (hit.collider.gameObject.layer!=Initialize.L_M_Ground) return;
        var v2 = hit.point;

        if (v2 != Vector2.zero)
        {
            var va = Bounds.min.y - v2.y;

            if (va>0.061f)
            {
                transform.position -= new Vector3(0, va, 0);

                Debug.LogError(va + "  set脚下(Move_P s) ");
            }

        }
    }
    public void set脚下(Move_P s)
    {
        MovePTimef = Time.frameCount;
        脚下 = s;
        Ground=true;
        //float f=Physics2D.Raycast(new Vector2(Bounds.center.x, Bounds.min.y), Vector2.down, 3f, 1 << Initialize.L_M_Ground).distance;
        对齐脚下();
    }
    public Move_P 脚下 { get {
            return   脚下1; } set {
            Debug.LogError("哪里AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"+value);
            脚下1 = value;
        
        } }
    void FixedUpdate()
    { 
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

        var chaox = -(Player3.I.Bounds.center - Vector3.zero).normalized;
        Debug.DrawRay(Player3.I.Bounds.center, chaox * 10f, Color.blue);
    }
    public Action<int> 圆斩对象;
    public void 安全地点(bool b = false)
    {
        if (b)
        {
            More_SafeWay_ = transform.position;
        }
        else
        {
            if (More_SafeWay_ == Vector2.zero) More_SafeWay_ = SafeWay_;

            transform.position = More_SafeWay_;
        }

    }
    public void 录入安全地点(bool 长 = false)
    {

        float 距离 = 0.1f;
        if (长) 距离 = 3f;
        var a = Physics2D.Raycast(new Vector2(Bounds.center.x, Bounds.min.y), Vector2.down, 距离, 1 << Initialize.L_Ground);
        if (a.collider != null)
        {
            if (a.collider.gameObject.CompareTag(Initialize.Ground))
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
                && 方向正确
                && co.contacts[0].collider == 蹲BOX
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

    public static float 前档板距离 = 0.05f;

    void 半灵相关()
    {
        if (N_.半灵)
        {
 
            //if (Ground)
                if (Player_input.I.按键检测_按下(Player_input.I.k.控灵))
                { 
                    Player_input.I.State = 1;
                    摄像机.I.设置相机跟随( 半灵.I.gameObject  );
        
                }else   if ( Player_input.I.按键检测_松开(Player_input.I.k.控灵, 1))
            { 
                Player_input.I.State = 0;
                摄像机.I.设置相机跟随(焦点.I.gameObject);
     
                    半灵.I.玩家目标点.transform.localPosition = Vector3.zero;
            }

            if (Player_input.I.State == 1)
            {
                //int heng = Player_input.I.方向正零负;
                //int shu = Player_input.I.竖直正负零;
                Vector3  V = Player_input.I.输入;
                Debu.LogError("X    Y"+V);
                半灵.I.玩家目标点.transform.position+= V * Time.deltaTime * 玩家数值.常态速度*3;
            }
        }
    }
    [SerializeField]


    public float Last副Speed1
    { get { return SpeedMager.I.Last副Speed1Leve; }
        set { SpeedMager.I.Last副Speed1Leve = value; } }

    //public new  SpriteRenderer sp;

 [DisplayOnly] public   Key 变速;
    protected override  void Update()
    { 
        base.Update();
         
     if (  N_.速度切换)
        {
 
            ///短按
            if (Player_input.I.按键检测_松开(Player_input.I.k.变速))
            {
                if (Player_input.I.Now_Time_- 变速.KeytimeDown < 0.2f)
                {
                    SpeedMager.I.切换();
                }
            }
 
        }

        半灵相关();


         if(N_.速度视野)if (Player_input.I.按键检测_按下(Player_input.I.k.视野)) 切换Shader.I.isSpeed = !切换Shader.I.isSpeed;

        if (Ground && !HPROCK && (FSM.f.I_State_C.state == E_State.run || FSM.f.I_State_C.state == E_State.idle))
        {
            录入安全地点();
        }
        前后和头(1f, 0.1f);
        显示 = N_;
        Public_Const_SpeedASD = Public_Const_Speed;

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
        下 = Physics2D.OverlapCircle(下检测.position, 前档板距离, 碰撞检测层) != null;
        上 = Physics2D.OverlapCircle(上检测.position, 前档板距离, 碰撞检测层) != null;
        中 = Physics2D.OverlapCircle(中检测.position, 前档板距离, 碰撞检测层) != null;
        if (上 && 中 && 下)
        {
            e_wall = E_wall.IIII;
        }
        else
        {
            e_wall = E_wall.OOOO;
        }
        顶死 = e_wall == E_wall.IIII /*|| e_wall == E_wall.OIOO*/;

        Last_Velocity = Velocity;
        监控 = Velocity;
    }

    public Vector3 假检测(float value)
    {
        Vector3 上 = Physics2D.Raycast(上检测.position, Vector2.right * LocalScaleX_Int, value, 碰撞检测层).point;
        Vector3 中 = Physics2D.Raycast(中检测.position, Vector2.right * LocalScaleX_Int, value, 碰撞检测层).point;
        Vector3 下 = Physics2D.Raycast(下检测.position, Vector2.right * LocalScaleX_Int, value, 碰撞检测层).point;
        bool b = 上 != Vector3.zero && 上.x._is(中.x) && 中.x._is(下.x);
        //Debug.LogError("        " + b +上+中+下);
        if (b)
        {
            return 中;
        }
        else
        {
            return Vector3.zero;
        }
    }
    public float 距离墙面的距离 => MathF.Abs(中检测.transform.localScale.x + 前档板距离 * 2);
    public void LastV_Velocity()
    {
        Velocity = Last_Velocity;
    }
    public void asdasd()
    {

    }
    public void 水平起步加力(KeyCode obj)
    {
        if (obj == Player_input.I.k.左 ||
          obj == Player_input.I.k.右)
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
    public void 反作用力(Enemy_base E, float E_距离, Vector2 E_矢量, Vector2 E_位置,
           Vector2 M_po,
           Vector2 M_Force)
    {
        if (E != null)
        {
            if ((E.Bounds.center - Bounds.center).magnitude < E_距离)
            {
                E.p.SafeVelocity = new Vector2(LocalScaleX_Set * E_矢量.x, E_矢量.y);
                E.transform.position += new Vector3(LocalScaleX_Set * E_位置.x, E_位置.x);
            }
        }

        var 悬空 = 悬空检测();
        var a = Physics2D.Raycast(new Vector2(反面脚底.x, Bounds.center.y), 反向, 碰撞检测层).collider == null;


        ///互作用力  
        if (!a)
        {///判断后面是不是空的 
            if (!悬空)
            {
                transform.position += new Vector3(LocalScaleX_Set * M_po.x, M_po.y);
                Velocity = new Vector2(LocalScaleX_Set * M_Force.x, M_Force.y);
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
    public Vector2 脚底发射(float 距离,float value=0)
    {

        Vector2 a = 脚底中间+Vector2.up*value;
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

    public Vector2 Dash_(int 朝向, DASH aSH)
    {
        ///当dash_过程被打断，状态机内部不会引用这个,


        if (!aSH.冲刺显示) return Vector2.zero;
        if (!aSH.发力显示) return Vector2.zero;

        if (!保持Dash) aSH.冲刺持续时间_ -= Time.fixedDeltaTime;

        if (强行退出DASH) { aSH.冲刺持续时间_ = 0; }

        if (aSH.冲刺持续时间_ <= 0)
        {
            aSH.冲刺持续时间_ = 0;
            StartCoroutine(某冲刺结束(aSH));
        }
        else
        {
            //if (aSH.冲刺持续时间_ / aSH.冲刺持续时间 < 1 / 4 && aSH.冲刺持续时间 > 0.2f)
            //{
            //    float X = Mathf.Lerp(Velocity.x, 0, 0.5f);
            //    return    new Vector2(X, 0);
            //}
            //else
            //{
            //    float DashSpeed = (朝向 * aSH.冲刺速度 * aSH.冲刺持续时间_ / aSH.冲刺持续时间) + (朝向 * aSH.基础冲刺速度);
            //    float X = Mathf.Lerp(Velocity.x, DashSpeed, 0.5f);
            //    return   new Vector2(X, 0);
            //}
            float DashSpeed = (朝向 * aSH.冲刺速度 * aSH.冲刺持续时间_ / aSH.冲刺持续时间) + (朝向 * aSH.基础冲刺速度);
            //float X = Mathf.Lerp(Velocity.x, DashSpeed, 0.5f);
            return new Vector2(DashSpeed, 0);
        }

        return Vector2.zero;
    }
    //float 蹲后OffY = -1.55f;

    [DisableOnPlay]
    public BoxCollider2D 站立box;

    [SerializeField]
    [DisableOnPlay]
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
            蹲BOX.size = new Vector2(蹲BOX.size.x, 蹲后SizeY);
            var a = (原始Size.y - 蹲后SizeY) / 2;
            蹲BOX.offset = new Vector2(蹲BOX.offset.x, 蹲BOX.offset.y - a);
        }
        else
        {
            蹲BOX.offset = 原始Offset;
            蹲BOX.size = 原始Size;
        }

    }
    public void 进入一半()
    {
        站立box.enabled = false;
        return;
        一半(true);
        var a = 蹲BOX;
        Vector2 最低点 = Vector2.zero;
        Vector2 碰撞点 = Vector2.zero;
        for (int i = 0; ; i++)
        {
            if (i > 1000)
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
    public void 跳跃触发(Vector2 v, string s = null)
    {
        if (s != null)
        {
            Debug.LogError(s + v);
        }
        I.Velocity = v;
    }
    public void 跳跃触发()
    {
        I.Velocity = new Vector2(I.Velocity.x, I.玩家数值.跳跃瞬间速度);
    }

    protected override void 离开地面_()
    {
        if (地面调试) Debug.Log("离开地面");
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

        if (Player_input.I.D_I[Player_input.I.k.跳跃].down_State < 0.1)
        {
            按下跳跃?.Invoke();
        }
    }
    //public LayerMask 可以原批碰撞检测层_;
    [SerializeField] LayerMask 碰撞检测层_;
    public override LayerMask 碰撞检测层
    {
        get
        {
            return 碰撞检测层_;
            //return 1 << Initialize.L_Ground | 1 << Initialize.L_M_Ground | 1 << Initialize.L_Box_Ground
            //     | 1 << Initialize.L_Only_Ground;
        }
    }



    public bool BigHit = false;

    int 不一致次数;
    public void 差价()
    {

        //return;
        蹲BOX.isTrigger = false;
        float Yc = 站立box.bounds.min.y - sp.bounds.min.y - 0.1f;
        transform.position += Vector3.up * Yc;
        //站立box.size = sky.Startsize;
        //站立box.offset = sky.StartOff;
    }
public     BoxCollider2D 最低点()
    {
        BoxCollider2D B = 蹲BOX;
        if (!蹲BOX.enabled||  蹲BOX.isTrigger) B = 站立box;
        return B;
    }
    public RaycastHit2D[] 地面检测(LayerMask L, float 距离=0.2f)
    {
 
        float DD = Ground ? 0 : 0.3f;

        if (Initialize_Mono.I.MoveP_优化)
            if (Ground&&脚下!=null)
        { 
            //距离 += 脚下.帧移动距离*2+1f;
        }
        BoxCollider2D B = 最低点();
        return Physics2D.BoxCastAll(
              new Vector2(B.bounds.center.x, B.bounds.min.y),
               new Vector2(B.bounds.size.x - DD, 0.1f),
               0f,
               Vector2.down,
                距离 + B.edgeRadius,
             L
               );
    }
    protected override void 前后和头(float 距离, float DI横)
    {


        ///卡在边缘砖头
        ///移动平台的is triiger
        ///
        ///  备用地面的夹角情况 
        ///   悬在墙上

        bool LastGr = Ground;
        var DIs = 地面检测(碰撞检测层);
        bool NowGround=false;
        Collider2D DI = null;
        foreach (var item in DIs)
        {
            if (item.collider != null)
            {
                if (item.collider.isTrigger == false)
                {
                    bool BB = Initialize.Get_碰撞(Initialize.L_Player, item.collider.gameObject.layer);
                    if (BB)
                    {//该层被忽略了
                        break;
                    }

                    DI = item.collider;
                     
                    break;
                }
            }
        }
        if (地面调试) if (DI == null) Debug.LogError("      备用：" + 备用地面检测21);
        if (地面调试) if (DI != null) Debug.LogError(DI + "      Trriger:" + DI.isTrigger + "      备用：" + 备用地面检测21);
        if (DI != null && DI.isTrigger == false)
        {
            NowGround = true;
        }
        else
        {
            bool bB = false;
            if (Velocity.y == 0 && 备用地面检测21 && e_wall == E_wall.OOOO)
            {
                bB = true;
            }

            NowGround = bB;


        }
        if (Initialize_Mono.I.MoveP_优化)
        {
            if (脚下 != null)
            {
                if (!NowGround)
                    if (MovePTimef + 2 >= Time.frameCount)
                    {///一种是接触后并且父类后 自己没跟上（怀疑
                     ///一种是接触前认为Ground
                        Debug.LogError("吃" + Time.frameCount);
                        NowGround = true;
                    }
            }
            else
            {
                if (NowGround)
                {
                    if (NowGround != LastGr)
                    {
                        foreach (var item in DIs)
                        {
                            if (item.collider.CompareTag(Initialize.MovePlatform))
                            {
                                NowGround = true;
                                var aaa = item.collider.GetComponent<Move_P>();
                                if (aaa != null) set脚下(aaa);
                                break;
                            }
                        }
                    }
                }

            }
        }

        if (false)
        {
            BoxCollider2D Bx = 最低点();
            var Av = Bx.bounds.min;
            var Bv = new Vector2(Bx.bounds.max.x, Bx.bounds.min.y);
            var Cv = new Vector2(Bx.bounds.center.x, Bx.bounds.min.y);
        bool Ab=    Physics2D.CircleCast(Av,0.001f,Vector2.zero,0, 碰撞检测层).point == Vector2.zero;
            bool Cb = Physics2D.CircleCast(Cv, 0.001f, Vector2.zero, 0, 碰撞检测层).point == Vector2.zero;
            bool Bb = Physics2D.CircleCast(Bv, 0.001f, Vector2.zero, 0, 碰撞检测层).point==Vector2.zero;

            Deb(Av,Ab);
            Deb(Bv, Bb);
            Deb(Cv,  Cb);
            void Deb(Vector2 v,bool b)
            {
                Color c = Color.red;
                if(b)c=Color.white;
                v.DraClirl(0.1f, c, 2f);
            }
            if ( Cb)
            {///没东西
            //if(Bb != Ab)
                    //GroundGround = false;
            }
        }
        Ground = NowGround;
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

        //new Vector2(po.bounds.size.x - 0.5f, 1),
        ((Vector2)蹲BOX.bounds.max).DraClirl();
        var tou =
        Physics2D.BoxCast(
       new Vector2(蹲BOX.bounds.center.x, 蹲BOX.bounds.max.y),
        new Vector2(蹲BOX.bounds.size.x , 1),
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
        new Vector2(Bounds.min.x,Bounds.center.y),
        new Vector2(0.001f, Bounds.size.y - 0.4f),
        0f,
      Vector2.left,
   0.05f,
     碰撞检测层
        )
        .collider;
        var a = A == null || A.isTrigger;

        Collider2D B = Physics2D.BoxCast(
 new Vector2(Bounds.max.x, Bounds.center.y),
 new Vector2(0.001f, Bounds.size.y - 0.4f),
 0f,
       Vector2.right,
 0.05f,
 碰撞检测层
 )
 .collider;
        var b = B == null || B.isTrigger;

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
    public int 返回方向()
    {
        var a键盘输入方向 = Player_input.I.方向正零负;
        if (a键盘输入方向 == 0)
        {
            ///无输入 啥都不干
            return -禁止朝向1;
        }
        else if (禁止朝向1 == 0)
        {
            ///禁用时间过了 
            return a键盘输入方向;
        }
        else if (禁止朝向1 == a键盘输入方向)
        {
            ///想要输入禁止的  不能
            return -禁止朝向1;
        }
        else if(a键盘输入方向==-禁止朝向1)
        {//相反   那就对了，

            return a键盘输入方向;
        }else
        {
            return -禁止朝向1;
        }


    }
    public int 禁止朝向1 { get { return 禁止朝向; } private set { if (禁止朝向 != value) {
          
            }   禁止朝向 = value; } }
    int 禁止朝向 = 0;
    internal void 记录a(float t )
    {
        if (t==0)
        {
            禁止朝向1 = 0;
            transform.position.DraClirl(1, Color.yellow, 1);
        }
        else
        {
            if(禁止!=null) StopCoroutine(禁止);
            禁止朝向1 = wall_进入为正面;
            禁止 = StartCoroutine(ads(t));  
        }
    }
    Coroutine 禁止;
    IEnumerator ads(float t)
    { 
        yield return new WaitForSeconds(t);
        记录a(0);
        禁止 = null;
    }
    public float 反登time = 0.3f;
    internal int is土狼时间_Wall=0;
    internal int wall_进入为正面;
}

public partial class Player3 : I_生命, I_攻击
{
    public void 伤害(I_生命 e, float value = 0)
    {
        if (value == 0) value = atkvalue;


        e.被扣血(value, Player3.I.gameObject, 0);
    }
    public bool is原Parent
    {
        get
        {
            return Player3.I.transform.parent == Player3.I.Player_Father;
        }
    }
    public Transform Player_Father_False { get; private set; }
    public Transform Player_Father { get; private set; }

    public void Changef(Transform f)
    {
        transform.SetParent(f);

        //Vector3 po = transform.position;  
        //Initialize_Mono.I.Waite(() =>
        //{
        //    transform.position = po;
        //}, 0.01f);
        //transform.position = po; 
    }

    /// <summary>
    /// 外部调用   更改和还原
    /// </summary>
    /// <param name="father"></param>
    public void ChangeFather(Transform father = null)
    {
        if (!Initialize_Mono.I.MoveP_优化) return; 
        if (father == null)
        { 
            if (transform != null && transform.parent != null && transform.parent.gameObject != null)
                if (transform.parent.gameObject.activeInHierarchy == false)
                {
                    Debug.Log("父物体关闭，离谱了");
                    GameObject a = new GameObject();
                    a.transform.SetParent(transform.parent); ////盲猜 父物体回归对象池关闭 导致异常
                    Player_Father_False = null;

                    gameObject.transform.SetParent(a.transform);

                    Player_Father_False = null;
                    Changef(Player_Father);
                    //transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    Player_Father_False = null;
                    Changef(Player_Father);
                    //transform.rotation = Quaternion.Euler(0, 0, 0);
                }

        }
        else ///改变
        {
            Debug.Log("夫对象改变");
            Player_Father_False = father;

            Changef(father);
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
            if (false)
            {
                Debug.LogError("无敌状态改变" + value);
            }
            HPROCK_ = value;
        }
    }
    public override float atkvalue { get => 玩家数值.Atk; set => 玩家数值.Atk = value; }


    public override void 扣最大上限(float i)
    {

    }
    public void 变速特效(float f)
    {
        Debu.LogError("ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ");
        if (MathF.Abs(Player3.Public_Const_Speed - f) > 1)
        {
            脉冲.I.File(Player3.I.transform.position);
            Initialize_Mono.I.时缓(0.1f, 0.8f);
        }
        else
        {
            脉冲.I.File(Player3.I.transform.position);
            //脉冲.I.File(Player3.I.transform.position, 0.01f);
            Initialize_Mono.I.时缓(0.1f, 0.2f);
        }
    }
    I_假死 辅;
    I_假死 主;
    I_假死 当前;


    public void SetSpeed(float f)
    {
        SpeedMager.I.有变速(f,true ); 
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



    public Func<GameObject, bool> Hit_FuncFSM;

    No_Re RRR = new No_Re();

    public Vector2 受伤Force;
    public bool 硬抗;

    Int不重复 IIIIIIIB = new Int不重复();


    GameObject LastGame;
    /// <summary>
    /// 999 秒杀
    /// </summary>
    /// <param name="i"></param>
    /// <param name="obj"></param>
    public override void 被扣血(float i, GameObject obj, int SKey = 0)
    { ///不能反弹碰撞伤害    
        Debug.LogError("被扣血" + i);
        if (SKey == 0) SKey = Initialize.Get_随机Int();
        if (!IIIIIIIB.Add(SKey)) return;

        if (LastGame != obj  )
        {
            LastGame = obj;
            Initialize_Mono.I.Waite(() => { LastGame = null; },0.2f);
        }else return;

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
            Debug.LogError("被扣血  前" + 当前hp);
            //当前hp -= i;
            当前hp -= 1;
            Debug.LogError("被扣血  后" + 当前hp);
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