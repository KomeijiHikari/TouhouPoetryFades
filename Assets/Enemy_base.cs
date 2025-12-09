using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using DG.Tweening;
using System.Linq;
using SampleFSM;
using Sirenix.OdinInspector;
public enum E_超速等级
{
    静止,
    低速,                                                                                                                                                                                                                    
    正常,
    超速,
    半虚化,
    虚化,
    虚无,
}
public interface I_Speed_Is
{
 
    /// <summary>
    /// 每个预制体自行设置的  ，游戏内不会改变
    /// </summary> 
    public float 固定等级差
    {
        get
        {
            var f = Speed_Lv / Player3.Public_Const_Speed;
            //var  V = Math.Clamp(f, Initialize_Mono.I.Speed_Min, Initialize_Mono.I.Speed_Max);
            return f;
        }
    }

    /// <summary>
    /// 只读  等级换算玩家速度后的速度
    /// </summary>
    float Speed_Lv { get; set; }
}
public interface I_Speed_Change: I_Speed_Is
{
    /// <summary>
    ///  设定  我比主角快，那就上海主角 很快 那就消失
    ///  我比主角慢 那就接近静止  太慢 那就碰撞消失  视觉存在
    /// </summary>


    public GameObject 对象 { get  ; }
    System.Action 变速触发 { get; set; }
    E_超速等级 超速等级 { get {
            E_超速等级 e_ = E_超速等级.正常;

            if (固定等级差 < 1 / Initialize_Mono.I.阀值|| 固定等级差._is(1 / Initialize_Mono.I.阀值 )) 
                e_ = E_超速等级.低速;
            if (固定等级差 <    Initialize_Mono.I.负阀值 || 固定等级差._is(1 / Initialize_Mono.I.负阀值 ))
                e_ = E_超速等级.静止; 
            if ( 固定等级差 >= Initialize_Mono.I.阀值) 
                    e_ = E_超速等级.超速;
                    if ( 固定等级差 >= Initialize_Mono.I.阀值2) 
                        e_ = E_超速等级.半虚化;
                        if ( 固定等级差 >= Initialize_Mono.I.阀值2_5) 
                            e_ = E_超速等级.虚化;
                            if ( 固定等级差 >= Initialize_Mono.I.阀值3) 
                                e_ = E_超速等级.虚无;     
            return e_;
        } }

    bool 限制
    {
        get
        {
            //return 超速等级== E_超速等级.虚无;
            return I_S.固定等级差 < Initialize_Mono.I.负阀值 || I_S.固定等级差 > Initialize_Mono.I.阀值3;
        }
    }
     I_Speed_Change I_S { get; }

    /// <summary>
    /// 只读  当前换算玩家速度后的速度
    /// </summary>
    float Curttent_Speed { get
        {
            return Current_Speed_LV / Player3.Public_Const_Speed;
        } }
    /// <summary>
    /// 生物的话，会一直改变          死物的话不会变          变速的目标
    /// </summary>
    float Current_Speed_LV { get; }
 
}

interface I_暂停
{
    public bool 暂停 { get; set; }
}
/// <summary>
/// 攻击行为和动画由行为树编辑。，攻击实际伤害 数值发生由碰撞箱触发
/// </summary>
public partial class Enemy_base : BiologyBase, I_Speed_Change, I_暂停, I_M_Ridbody2D
{ 
    public bool 根性=false ;
    public GameObject 对象 { get => gameObject; }
    [SerializeField]
    private bool debug_;
    public bool Debug_ { get => debug_; set => debug_ = value; }
    public System.Action 变速触发 { get; set; }
    public I_Speed_Change I_S { get => (I_Speed_Change)this; }
    public enum Enemy_anim_state
    {
        Null,
        idle,
        fang,
        move,
        atk,
        hit,
        dead,
    }
    public Enemy_anim_state 当前 = Enemy_anim_state.idle;
    //public void TreeAction()
    //{
    //    v.SendEvent("Action");
    //}
    public string Event_死亡 { get; set; } = "生命归零";
    public string Event_受击 { get; set; } = "Hit";
    public string Event_打断 { get; set; } = "Break";
    /// <summary>
    /// speed LV 是一个单位内不动的
    /// speed 是 speed LV   和静态换算后的
    /// 
    /// mosp 
    /// jump sp      一秒之内移动多少   距离/时间                    当主角变快的时候   主角变快      坟墓变大，单位距离变小    
    /// 所以                 距离/主角
    /// 
    /// 执行的时候是      jump sp*jump sp  
    /// </summary>
 
    [DisplayOnly]
    [SerializeField]
    float 移动距离_;
    [DisplayOnly]
    [SerializeField]
    float 显示_Speed_;

    public float Current_Speed_LV
    {
        get { 
            if (p.Get_矢量长度()==0)
            {
                
                return Speed_Lv;
            }
            else
            {
                return p.Get_矢量长度() * Speed_Lv;
            }  
        } 
    }
    [SerializeField]
    float Speed_Lv_;
    public float Speed_Lv { get => Speed_Lv_; set => Speed_Lv_ = value; }

    [SerializeField]
    float Move_speed_;
    public float Move_speed { get => Move_speed_; set => Move_speed_ = value; }
    [DisplayOnly]
    [SerializeField]
    float Jump_speed_;
    public float Jump_speed { get => Jump_speed_; set => Jump_speed_ = value; }


    public bool 破防;

    [SerializeField]
    float 韧性__;
    public float 韧性_
    {
        get
        {
            return 韧性__;
        }
        set
        {
            韧性__ = Mathf.Clamp(value, -Max韧性, Max韧性);

        }
    }
public  float Max韧性;

    float 加速度;
    public bool 碰撞开关;

    ///// <summary>
    ///// 某种程度上是生命周期管理的  子状态
    ///// </summary>
    //   state 活着;
    //   state 超速;
    //   state 受击;
    //   /// <summary>
    //   /// 维托给生命管理周期的   
    //   /// </summary>
    //   state 死亡;
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer != Initialize.L_Player) return;
        if (碰撞不动 == Vector2.zero) return;
        transform.position = 碰撞不动;
    }
    Vector2 碰撞不动;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != Initialize.L_Player) return;
        碰撞不动 = transform.position;

    }

    private void OnCollisionStay2D(Collision2D co)
    {
        if (co.gameObject.CompareTag(Initialize.Player))
        {
            if (Player3.I.HPROCK) return;
            var a = (Player3.I.Bounds.center - Bounds.center).normalized;
            if (Player3.I.Ground)
            {
                Player3.I.Velocity = new Vector2(a.x * 5, 0);
            }
            else
            {
                Player3.I.Velocity = a * 5;
            }
        }
    }
    public void 瞬移至(Vector2 v)
    {
        transform.position = p.碰撞预测(v);
    }
    [SerializeField ]
    float 韧性增幅=0;

    [SerializeField]
    bool 回复到满;
    /// <summary>
    /// 输入正  加韧性
    /// </summary>
    /// <param name="v"></param>
    public int   韧性(float v )
    {
        ///满    加速度归零  破防为flase;
        ///大于零，每帧回韧性，砍一刀扣韧性
        ///小于零   破防为true  每帧回韧性；        v<0发射  砍一刀扣韧性    
        韧性_ += v;
        if (韧性_ == Max韧性)
        {
            回复到满 = false;
              加速度 = 0;
            破防 = false;
           return -99;
        }
        else
        {//恢复  
            if (回复到满) {
                加速度 += Time.deltaTime * (Initialize_Mono.I.敌人回耐久速度 + 韧性增幅);
            }   

            if (韧性_<0&& 韧性_+加速度 > 0)
            {
                恢复了();
                //return  1;
            }

            韧性_ += 加速度 * Time.deltaTime;

            //if (v==0) return -99;
            if (韧性_ > 0)
            {  ///大于  
                破防 = false;
                if (v < 0)
                {
                    ///没有破防造成伤害  
                    Hit(); 
                }
            }
            else
            {  ///韧性低于0了 
                 
                if (!破防)
                {
                    回复到满 = true;
                    /// 击破一瞬间 
                    if (韧性_>-160)
                    {
                        韧性_ =-160;
                    }
                    上天(); 
                } 
                ///小于
                破防 = true; 
                if (v < 0)
                { 
                    ///破防后造成伤害
                    打断();
                    return -1;
                }
            }
        }
        if (v != 0)
            Debug.LogError("奇怪，不可能"+v);

        return -99;
    }

    public void 嗝屁()
    {
        yalaAudil.I.EffectsPlay("Die", gameObject. GetInstanceID());

        co.enabled = false;
        生命归零?.Invoke();

        HPROCK = true;
        v?.SendEvent(Event_死亡);
        Initialize_Mono.I.Waite(
            () => { 销毁触发?.Invoke();
            }
            //, 0.8f
            );
    }
    public System.Action A_破防受击;
    public System.Action A_受击;
    public System.Action A_恢复;
    public System.Action A_破防;
    public virtual   void 上天()
    {
        A_破防?.Invoke();
    }
    public virtual void 恢复了()
    {
        A_恢复?.Invoke(); 
    }
    public virtual void Hit()
    {
        A_受击?.Invoke();
           v?.SendEvent(Event_受击);
    }
    public virtual void 打断()
    {
        A_破防受击?.Invoke();
        v?.SendEvent(Event_打断);
    }
    public static 生物数据 Get_生物数据(string s)
    {

        return Resources.Load("ScriptableObject/生物/" + s) as 生物数据;
    }
    [SerializeField]
    Vector2 BoomSpeed_;
    public Vector2 BoomSpeed { get => BoomSpeed_; set => BoomSpeed_ = value; }

    [SerializeField]
    bool 空气墙碰撞设置1_=true;
    /// <summary>
    ///    只读
    /// </summary>
    [SerializeField]
    [DisplayOnly]
    bool 空气墙碰撞显示_; 
    public bool 空气墙碰撞
    {
        get
        {
            var b = !Initialize.Get_碰撞(Initialize.L_Enemy, Initialize.L_Air_wall);
            return b;
        }
        set
        {

            Initialize.Set_碰撞(Initialize.L_Enemy, Initialize.L_Air_wall, !value);
        }
    }


    public static string ready { get => "ready"; }
    public static string atk { get => "atk"; }
    public static string run { get => "run"; }
    public static string idle { get => "idle"; }
    public static string dead { get => "dead"; }
    public static string hit { get => "hit"; }
    public 粒子系统 粒子系统;
    public override System.Action 生命归零 { get; set; }
    public override System.Action 被打 { get; set; }
    public override float atkvalue { get => atkvalue_; set => atkvalue_ = value; }
    public override float 当前hp { get => 当前hp_; set => 当前hp_ = value; }
    public override float hpMax { get => hpMax_; set => hpMax_ = value; }
    public override bool HPROCK { get => HPROCK_; set => HPROCK_ = value; }

    [SerializeField]
    float 重力增幅_;
    public float 重力增幅 { get => 重力增幅_; set => 重力增幅_ = value; }
    [SerializeField] float atkvalue_ = 10;
    [SerializeField] float 当前hp_ = 100;
    [SerializeField] float hpMax_ = 100;
    [SerializeField] bool HPROCK_;
    [SerializeField] bool 灵魂_ = true;


    public string Name;

    protected override bool 灵魂 { get => 灵魂_; set => 灵魂_ = value; }

    BehaviorTree v;

    public Phy p { get; set; }
    //public new Vector2 Velocity { get; set; }
    public new Vector2 Velocity { get => p.当前; set => p.Velocity = value; }
    public float 移动距离
    {
        get
        {
            var f = 0f;
            if (Velocity.x == 0)
            {
                f = Mathf.Abs(Velocity.y);
            }
            else if (Velocity.y == 0)
            {
                f = Mathf.Abs(Velocity.x);
            }
            else
            {
                f = Velocity.magnitude;
            }
            return f;
        }
    }
    [SerializeField]
    [DisplayOnly]
    float 显示读取后刷新_Curttent_Speed_;

    public void Play(string name)
    {
        an.Play(name);
    }
    public 生物数据 m;
    public bool Add;

    protected override void Awake()
    {
        if (sp == null)
            sp = GetComponentInChildren<SpriteRenderer>();
        if (an == null)
            an = GetComponentInChildren<Animator>();

        base.Awake();

    if(!Debug_ )    gameObject.AddComponent<MonoMager>();
        co = GetComponent<Collider2D>();
        p = GetComponent<Phy>();

        v = GetComponent<BehaviorTree>();

        if (粒子系统 == null)
            粒子系统 = GetComponentInChildren<粒子系统>();
        if (Speed_Lv == 0) Speed_Lv = 1;

 
        开箱();

       GravityScale = 0;
        sp.material = 材质管理.Get_Material(材质管理.Other);
        //边缘颜色更新();
        //Color .blue 

    }
    No_Re RR = new No_Re();
    bool 初始化;
    void 开箱()
    { 
        if (!RR.Note_Re()) return;  
        m = Get_生物数据(Name);
        if (m != null)
        {
            if (!初始化)
            {
                if (Add)
                {
                    Move_speed += m.移动速度; 
                    BoomSpeed += m.爆发力;
                    atkvalue += m.atkvalue;
                    hpMax += m.hpMax;
                    Max韧性 = m.韧性;
                }
                else
                {
                    Move_speed = m.移动速度;

                    BoomSpeed = m.爆发力;
                    atkvalue = m.atkvalue;
                    hpMax = m.hpMax;
                    Max韧性 = m.韧性;
                }
            }
        }
        当前hp = hpMax; 
  
        韧性__ = Max韧性;

        if (Debug_ ) 
        Debug.LogError(Max韧性+"                   AAAAAAA                                 "  +  韧性__+gameObject.name);

        初始化 = true;
    }

    [SerializeField] [DisplayOnly]
    bool 限制_;
    private void Update()
    {
  
        限制_ = I_S.限制;
        if (Debug_)
        {
            if (v != null)
            {
                var a = v.GetVariable("Target");
                if (a != null)
                {
                    var b = (Vector2)a.GetValue();
                    if (b != Vector2.zero)
                    {
                        b.DraClirl(1);
                    }
                }
            }
        }

        if (!暂停)
        {

            前后和头(0.1f, 0.1f);
        }

        韧性(0);  
        if (!is_Dead)
        {
            p.Stop = I_S.限制;
            co.enabled = !I_S.限制;
        }

        if (I_S.限制 || 暂停)
        {
            an.speed = 0;
            v?.DisableBehavior(true); ///在当前节点  停止 
        }
        else
        {
            v?.EnableBehavior(); ///恢复

            if (an!=null)   an.speed = I_S.固定等级差;
        }

 
               //显示读取后刷新_Curttent_Speed_ = I_S.Curttent_Speed;
               显示_Speed_ = I_S.固定等级差; 

        if (暂停) return;
        空气墙碰撞 = 空气墙碰撞设置1_;
        空气墙碰撞显示_ = 空气墙碰撞;
    }
    

    Int不重复 IIIIIIIB=new Int不重复 ();

    public float 被打时间;
    public override void 被扣血(float i, GameObject obj, int SKey=0)
    {

        /// 挡住无伤  没挡住  被扣血  扣血破防  或者被扣学死掉   4种情况

        if (SKey == 0) SKey = Initialize.Get_随机Int();
        ///不重复
        if (!IIIIIIIB.Add(SKey)) return;
        ///是玩家
        var BB = obj == Player3.I.gameObject; 

        ///防御情况下玩家收到反作用力
        if (HPROCK)
        {
            if (BB) Player3.I.反作用力(this, 0, Vector2.zero, Vector2.zero,
                  Vector2.left * 0.5f, Vector2.left);
            特效_pool_2.I.GetPool(gameObject, T_N.特效防御, an.GetComponent<SpriteRenderer>());
            return;
        }

        yalaAudil.I.EffectsPlay("Hit", gameObject.GetInstanceID()); 
        //共用特效
        sp.闪光(0.05f);
        ///血量计算
        if (破防)   当前hp -= i;
        //if (BB) Player3.I.反作用力(this, 3, Vector2.left, Vector2.left * 0.5f, Vector2.left * 0.3f, Vector2.left * 0.1f);
        被打时间 = Time.time;

        float 反方向 = Initialize.返回和对方相反方向的标准力(gameObject, obj).x;
        if (当前hp <= 0)
        { 

            ///特效
            粒子系统?.restore();
            粒子系统?.Play(); 

            ///死亡击飞位移
            float Y = Initialize.RandomInt(50, 100);
            float X = Initialize.RandomInt(7, 10);
            Velocity = new Vector2(反方向 * X, Y);

            ///结果
   
            嗝屁(); 
        }
        else
        {
            if (BB)
            {
                if (Debug_)
                {
                    Debug.LogError(i+ "            if (Debug_)      if (Debug_)      if (Debug_)    ");
                }
            }
            //if (SKey == 0)
            //{
            //    韧性(-i);
            //}
            //else
            //{
            //    韧性(-SKey);
            //}
             韧性(-i);
            ///血液特效
            if (粒子系统!=null)
            {
                //粒子系统.transform.SetParent(null);
                粒子系统.数量 = 3;
                粒子系统.喷射方向 = new Vector2(反方向, 0);
                粒子系统?.Play();

                 
            }
            ///刀光特效
            特效_pool_2.I.GetPool(Bounds.center, T_N.特效受击, 反方向 > 0, sp).Speed_Lv = Player3.Public_Const_Speed;

            ///位移 
            if (扣血外部力 != Vector2.zero)
            {
                p.Stop_Velo();
                p.SafeVelocity = 扣血外部力;
                扣血外部力 = Vector2.zero;
            }
            else if (!根性) p.SafeVelocity = new Vector2(反方向 * 5, 0);
          



            //震动
            DG.Tweening.Sequence s = DOTween.Sequence();
            s.Append(sp.transform.DOShakePosition(0.2f, new Vector2(0.5f, 0), 33, 45f, false, false, ShakeRandomnessMode.Harmonic));
            s.AppendCallback(() => sp.transform.localPosition = Vector2.zero);

            被打?.Invoke();

            
        }
    }

    public override LayerMask 碰撞检测层
    {
        get
        {
            if (空气墙碰撞)
            {
                return 1 << Initialize.L_Ground | 1 << Initialize.L_Air_wall| 1 << Initialize.L_M_Ground;

            }
            else
            {
                return 1 << Initialize.L_Ground | 1 << Initialize.L_M_Ground;
            }
        }
    }

    public bool 暂停 { get => 暂停1; set {
 
          暂停1 = value;
        } }

    public 地面情况 前面有坑(float 检测总距离, int 数量, float 第一根距离, float 深度)
    {
        float 单个距离 = 检测总距离 / (数量 - 1);
        List<bool> boolList = new List<bool>();

        // 遍历并发出射线  
        for (int i = 0; i < 数量; i++)
        {
            // 计算射线的水平偏移量  
            float 当前增长距离 = 第一根距离 + i * 单个距离;
            Vector2 原点 = new Vector2(正面头顶.x + (LocalScaleX_Set * 当前增长距离), 正面头顶.y);
            Vector2 方向 = Vector2.down;
            float 射线长度 = Bounds.size.y + 深度;

            // 发射射线并检测地面  
            bool isGround = Physics2D.Raycast(原点, 方向, 射线长度, 碰撞检测层).collider == null;
            boolList.Add(isGround);
#if UNITY_EDITOR
            Debug.DrawRay(原点, 方向 * 射线长度, Color.green, 0.01f);
#endif
        }


        // 检查boolList并返回地面情况  
        if (!boolList.Any()) // 空列表时没有元素，返回平地  
            return 地面情况.平地;

        if (boolList.All(b => b)) // 所有元素都是true（实际上是没有地面），返回跨不过去  
            return 地面情况.跨不过去;

        // 检查是否存在至少一个true（没有地面）后面跟着至少一个false（有地面）的情况，并返回有坑  
        if (boolList[0])
        {
            for (int i = 1; i < boolList.Count - 1; i++)
            {
                if (!boolList[i])
                {
                    return 地面情况.有坑;
                }
            }
        }
        // 如果没有找到需要跳跃的坑，则返回平地或未知情况  
        // 如果所有情况都不符合，打印错误并返回未知情况或平地  
        return 地面情况.平地; // 暂时返回平地，但实际情况可能需要进一步处理 
    }

 
    [DisableOnPlay]
    [SerializeField ]
    private bool 暂停1;

}

public partial class Enemy_base : I_Dead, I_Revive
{
 
    public Bounds 盒子 => co.bounds;

    [SerializeField]
    [DisableOnPlay]
    private bool re_ = true;
    public bool Re { get => re_; set => re_ = value; }
    public float Re_Time { get; set; } = 0;
    public System.Action 销毁触发 { get; set; }

    [SerializeField]
    Vector2 StartWay;
    private void Start()
    {
        Start朝向 = LocalScaleX_Set;
        StartWay = transform.position;

        StartWay.DraClirl(5, Color.blue, 20);

    }
    [UnityEngine.Tooltip(" 死亡时候关闭  复活时候开启的组件　")]
    [SerializeField]
    List<Component> 组件列表;

    public bool is_Dead;
    public bool Dead()
    {
        if (Debug_) Debug.LogError("  Dead() Dead() Dead() Dead() " + gameObject.name + transform.position);
        is_Dead = true;
        组件列表.集体开关(false);
        v?.DisableBehavior(true);
        return true;

    }
    float Start朝向;
 

    public void Event_重制()
    {
        var a = 重制();
    }
    

    public bool 重制()
    {
 
           if (Debug_) Debug.LogError(" 重制() 重制() 重制() 重制() 重制() ");
        if (p != null) p.Stop_Velo();
        E_重制?.Invoke();
       v?.EnableBehavior(); ///恢复

        an.Play(idle);
        HPROCK = false; //和死亡状态相对应
        is_Dead = false;
        开箱();
        组件列表.集体开关(true);
        LocalScaleX_Set = Start朝向;
        transform.position = StartWay;
        if (Debug_) ((Vector2)transform.position).DraClirl(5, Color.red, 90);

        return true;
    }
   public  System.Action E_重制;
}

public class Int不重复
{
    List<int> 列表=new List<int> ();
    /// <summary>
    ///  如果重复返回false
    /// </summary>
    /// <param name="i"></param>
    /// <param name="de"></param>
    /// <returns></returns>
    public bool Add(int i,bool de=false)
    {
    if(de)    Debug.LogError(i);
        if (!列表.Contains (i))
        {
            列表.Add(i);
            if (列表.Count >1000)
            {
                列表.Clear();
            }
            return true;
        }
        else
        {
            return false;
        } 
    } 
}
