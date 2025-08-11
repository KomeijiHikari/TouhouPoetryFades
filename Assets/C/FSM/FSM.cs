using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public enum E_State
{
    idle,
    run,
    sky,
    wall,
    dun,
    dash,
    atk,
    ladder,
    skyatk,
    dunatk,
    skydash,
    downatk,
    upatk, 
    hit,
    gedang,
    counter,
    cricleatk,
    pa,
    interaction
    //ATK,//近战 -> 追击/丢失/死亡
    //DIE,//死亡
    // DASH
}



/// <summary>
/// 状态接口
/// </summary>
public interface I_State
{
 
    public E_State state { get; set; }
    public bool 能力激活的 { get; set; }

    /// <summary>
    /// 如果返回true 可以切换 
    /// </summary>
    /// <returns></returns>
    public bool 可以切换嘛();
    void AweakStatebase();
    void EnterStatebase();
    void UpdateStatebase();
    void ExiteStatebase();
    void StateStart();
    void FixedState();
    /// <summary>
    /// 进入状态
    /// </summary>
    void EnterState();
    /// <summary>
    /// 离开状态
    /// </summary>
    void ExitState();
    /// <summary>
    /// 更新状态
    /// </summary>
    void UpdateState();
     

}




[DefaultExecutionOrder(1)]
//第二个方案实践，实现代码放在Player2里，其他状态引用实现代码方法
public class FSM : MonoBehaviour
{
 
  public   Transform 改变后的;
    public bool 状态消息 = true;
    public bool 变速攻击;
    public static FSM f { get; private set; }

    public I_State I_State_LLL;
    public I_State I_State_LL;
    public I_State I_State_L;
    public I_State I_State_C;

 
    [HideInInspector] public Dictionary<E_State, I_State> D_State = new Dictionary<E_State, I_State>();
    [DisplayOnly]
    public List<State_Base> 所有的状态;

    //特效模板管理 Action;
 public 特效模板管理 蓄力 { get; set; }
   public 特效模板管理 Pool { get; set; }


    public State_Base Getstate(E_State E)
    {
        for (int i = 0; i < 所有的状态.Count; i++)
        {
            if (所有的状态[i].state == E)
            {
                return 所有的状态[i];
            }
        }
        Debug.LogError("试图查找" + E + "但是返回空值");
        return null;
    }
    private void Awake()
    { 
          //a = asdasd_();
          所有的状态 = new List<State_Base>();
        if (f != null && f != this)
        {
            Destroy(this);
        }
        else
        {
            f = this;
        }

        所有状态自动注册字典();
        foreach (var item in D_State)
        {
            item.Value.AweakStatebase();
        }

        To_State(E_State.idle);
    }
    public void 真实特效(string name)
    {
        var a = 特效_pool_2.I.GetPool(gameObject, name);
        a.同步玩家 = true;
        a.an.updateMode = AnimatorUpdateMode.UnscaledTime;
    }
    bool 弹反教学 => Player3.I.N_.弹反蓄力教学模式;
    bool 教学 => Player3.I.N_.教学模式;
    适应文字 指示 => Player3.I.适应文字;
    public void 教学模式(bool b = true)
    { 
        if (!教学 && !弹反教学) return; 
        if (b)
        {
            switch (Player3.I.防御状态)
            {
                case Player3.防御.开始防御: 
                    指示.开关(true);
                    Initialize_Mono.I.时缓(0.1f, 1f);
                    指示.SetText("按  攻击键");
                    break;
                case Player3.防御.防御反击: 
                    Initialize_Mono.I.时缓(0.1f, 1f);
                    if (弹反教学)
                    {
                        指示.SetText("W+攻击键    长按 ");
                    }
                    else
                    {
                        指示.SetText("按  方向键  + 攻击键");
                    }

                    break;
                case Player3.防御.反击攻击:
                    if (弹反教学)
                    {
                        指示.SetText(" 长按 ");
                    }
                    else
                    {
                        指示.开关(false);
                    }
                    break;
            }
        }
        else
        {
            指示.开关(false);
        }

    }
    void Start_蓄力相关()
    {
        蓄力 = 特效_pool_2.I.GetPool(gameObject, T_N.特效蓄力).GetComponent<特效模板管理>();
        Pool = 特效_pool_2.I.GetPool(gameObject, T_N.特效蓄力完成).GetComponent<特效模板管理>();

        蓄力.an.updateMode = AnimatorUpdateMode.UnscaledTime;
        Pool.an.updateMode = AnimatorUpdateMode.UnscaledTime;

        Pool.transform.SetParent(transform);
        蓄力.transform.SetParent(transform);
        蓄力.代理回归 = true;
        Pool.代理回归 = true;
        蓄力.gameObject.SetActive(false);
        Pool.gameObject.SetActive(false);

        Pool.同步玩家 = true;
        蓄力.同步玩家 = true;

        Pool.Speed_Lv = Player3.Public_Const_Speed;
        蓄力.Speed_Lv = Player3.Public_Const_Speed;
    }
    E_State last;
    void Update_蓄力相关()
    {
 
        if (!Player3 .I.N_.时缓 )
        {
            return;
        }
        bool 打断=false , 完成 = false;
        switch (f.I_State_C .state)
        { 
            case E_State.hit:
                打断 = true;
                break;
            case E_State.counter:
                完成 = true;
                break; 
        }

        if (打断 || 完成)
        {
            if (last!=I_State_C .state)
            {
                last = I_State_C.state;
                特效(Pool, false);
                特效(蓄力, false);

            }

            蓄力状态 = E_蓄力状态.没蓄力; 
            return;
        }
        switch (蓄力状态)
        {
            case E_蓄力状态.没蓄力:
                if (Player_input.I.Get_key(Player_input.I.攻击).yes_State> 蓄力进入时间)
                {
                    真实特效( T_N.特效蓄力触发);

                    蓄力状态 = E_蓄力状态.开始蓄力;
                    特效(蓄力);
                } 
                break;
            case E_蓄力状态.开始蓄力:
                if (Player_input.I.按键检测_松开(Player_input.I.攻击))
                {
                    特效(Pool, false);
                    特效(蓄力, false);
                    蓄力状态 = E_蓄力状态.没蓄力;
                }
                else    if (Player_input.I.Get_key(Player_input.I.攻击).yes_State > 蓄力完成时间  )
                {
                    真实特效(T_N.特效蓄力结束);
 
                    蓄力状态 = E_蓄力状态.蓄力好了;
                    特效(Pool);
                    特效(蓄力, false);
                }
                    break;
            case E_蓄力状态.蓄力好了:
                //蓄力完成时间 = Time.time;
                if (Player_input.I.按键检测_松开(Player_input.I.攻击))
                {
                    特效(Pool, false);
                    特效(蓄力, false);
                    蓄力状态 = E_蓄力状态.没蓄力; 
                }
                else
                {
                    蓄力成功之后 = Time.time;
                }
                break; 
        } 
    }

    private void Start()
    {
 
        foreach (var item in D_State.Values)
        {
            item.StateStart();
        }
        Start_蓄力相关();
      
    }
 public    void 特效(特效模板管理 t,bool  b=true )
    {
        if (!Player3.I.N_.时缓) return;
        if (b)
        {
            t.gameObject.SetActive(true);
            if (t.A_name != null)
            {

                var F =      t.播放特效(t.A_name);
                t.transform.localPosition = F.偏移; 
            }

        }
        else
        {
 
            t.gameObject.SetActive(false );
        }

    }
    private void FixedUpdate()
    {
 
        I_State_C.FixedState();
    }
 
    public float 蓄力成功之后;

    float 蓄力进入时间 = 0.2f;
    float 蓄力完成时间 = 0.3f;
    enum E_蓄力状态
    {
        没蓄力,
        开始蓄力,
        蓄力好了
    }
    [DisplayOnly]
    [SerializeField ]
    E_蓄力状态 蓄力状态;



    private void Update()   //每帧运行一次
    {
        Update_蓄力相关();

        I_State_C.UpdateStatebase();
        I_State_C.UpdateState();
    }
 
    public void 所有状态自动注册字典()
    {
        foreach (E_State v in Enum.GetValues(typeof(E_State)))
        {
            string enumString = v.ToString();
            string className = enumString;

            // 字符串转换为类名  
            Type classType = Type.GetType(className);

            if (classType != null && typeof(I_State).IsAssignableFrom(classType))
            {
                // 使用反射创建实例  
                object instance = Activator.CreateInstance(classType);

                // 检查是否成功创建实例  
                if (instance != null)
                {
                    所有的状态.Add((State_Base)instance);
                    // 添加到字典中  
                    D_State.Add(v, (I_State)instance);
                    D_State[v].state = v;
                }
                else
                {
                    Debug.LogError("无法创建实例");
                }
            }
            else
            {
                Debug.LogError("类型为空或者没有继承接口");
            }
        }
    }
    int LastF;
    public bool To_State(E_State E)
    { 

        if (!D_State[E].可以切换嘛())
        {
            Player3.I.闪光();
            return false;
        } 
        if (!D_State[E].能力激活的)
        {
            Player3.I.闪光();
            Debug.LogWarning("下一个状态"+E+"未激活，当前状态"+ I_State_C.state);
            return  false;
        }
        if (LastF != Time.frameCount)
        {
            LastF = Time.frameCount;
        }
        else
        {
            Debug.LogError("同一帧切换状态    上个状态:    " + I_State_L + "当前状态:     " + I_State_C + "目标状态:     " + E);
        }
        ///上个状态的别留下来
        Player3.I.Atk = false;
        Player3.I.End = false;
        if (I_State_C!= null)
        {
            I_State_C.ExitState();
            I_State_C.ExiteStatebase();
        }
        I_State_LLL = I_State_LL;
        I_State_LL = I_State_L;
        I_State_L = I_State_C; 
        I_State_C = D_State[E];
        if (状态消息)
        {
            Debug.Log(   Time.time + "        状态情况=        "+ I_State_C +
                           "\n上一个状态：" + I_State_L       + 
                            "\n上上个状态：" + I_State_LL +
                          "\n上上上个状态：" + I_State_LLL
                );
        }
        if (I_State_L==Getstate(E_State.idle )&& I_State_C== Getstate(E_State.counter))
        {

        }
        else
        {
          Player3.I.  防御状态 =  Player3.防御.Null;
        } 
        I_State_C.EnterStatebase();
        I_State_C.EnterState();

        return true;
    }

}


[System.Serializable]
public class State_Base : I_State
{
    [DisplayOnly]
    [SerializeField]
    public  int ExiteFramet;
    [DisplayOnly]
    [SerializeField]
    public float  ExiteTime;
    protected  Player_input IP;
    protected Player3 Player;
    protected FSM f;
    protected AniContr_4 A;
    [DisplayOnly]
    [SerializeField]
 E_State state_;

  protected  float Float_Value;

    

  /// <summary>
  ///  进入状态后的间隔时间
  /// </summary>
    protected float EnterTime
    {
        get
        {
            return Time.time - f.Getstate(f.I_State_L.state).ExiteTime;
        }
    }
    public E_State state
    {
      
        get { return state_; }
        set { state_ = value; }
    }


    [DisplayOnly]
    [SerializeField]
protected bool 能力激活的_显示=true;
    public   virtual bool 能力激活的 { get => 能力激活的_显示;   set => 能力激活的_显示=value; }

    [DisplayOnly]
    [SerializeField]
    public bool 活动;
    public   virtual  void AweakStatebase()

    {//初始化，最初只执行一次

        f = FSM.f;
          IP = Player_input.I;
        Player = Player3.I;
        A = Player3.I._4;
    }
    public void UpdateStatebase()
    {

        //ExiteTime = Time.time;
        //ExiteFramet = Time.frameCount;
    }
    public virtual  void StateStart()
    {
    }
    public  virtual bool 可以切换嘛()
    {
        return true;
    } 
    public void ExiteStatebase()
    {
        ExiteTime = Time.time;
        ExiteFramet = Time.frameCount;
        活动 = false;
         
        Player3.I.按住 -=  按住;
        Player3.I.松开 -= 松开;
        Player3.I.按下 -=  按下;
        Player3.I.接触地面事件 -=   接触地面;
        Player3.I.离开地面事件 -=  离开地面;
        Player3.I.按下跳跃 -= 按下跳跃;
        Player3.I.方向按住 -=  方向按住;
        Player3.I.方向改变_Action -= 方向改变;
        Player3.I.顶到墙了 -= 顶到墙了;
        Player3.I.受伤了 -= Hit;
        Player3.I.Hit_FuncFSM -= Hit_Func;
    }
    public void EnterStatebase()
    {
        活动 = true; 
        Player3.I.按住 += 按住;
        Player3.I.松开 += 松开;
        Player3.I.按下 +=  按下;
        Player3.I.接触地面事件 += 接触地面;
        Player3.I.离开地面事件 += 离开地面;
        Player3.I.按下跳跃 += 按下跳跃;
        Player3.I.方向按住 +=  方向按住;
        Player3.I.方向改变_Action +=  方向改变;
        Player3.I.顶到墙了 += 顶到墙了;
        Player3.I.受伤了 += Hit;
        Player3.I.Hit_FuncFSM += Hit_Func;


    }

    public virtual bool Hit_Func(GameObject arg)
    {
        return true;
    }

    public virtual void Hit()
    { 
        f.To_State(E_State.hit);
    }
    public virtual  void EnterState()
    { 
    } 
    public virtual void ExitState()
    { 
    } 
    public virtual void FixedState()
    { 
    } 
    public virtual void UpdateState()
    {

    }
    public virtual void 顶到墙了(bool obj)
    {

    }

    public virtual void 按下(KeyCode obj)
    {

    }

    public virtual void 按住(KeyCode obj)
    {

    }

    public virtual void 松开(KeyCode obj)
    {

    }

    public virtual void 按下跳跃()
    {
    }

    public virtual void 方向按住(bool obj)
    {
    }

    public virtual void 方向改变(bool obj)
    {
 
        Player.方向改变 (obj);
    }

    public virtual void 离开地面()
    {
    }

    public virtual void 接触地面()
    {

    }
} 