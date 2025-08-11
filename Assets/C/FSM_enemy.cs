using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
[System.Serializable]
public class State_enemy_Base : I_enemy_State
{
    [DisplayOnly ]
    public bool 显示;
    public E_enemyState state_;
public  E_enemyState state
    {
        get
        {
            return state_;
        }
        set
        {
            state_ = value;
        }
    }
    protected FSM_enemy f;
    protected Animator an;
    protected SpriteRenderer sp;
    protected Collider2D c;
    protected enemy e;


    public State_enemy_Base asdasdasd(FSM_enemy fSM_Enemy, E_enemyState e_)
    {
        f = fSM_Enemy;

        state = e_;

        an = f.gameObject.GetComponent<Animator>();
        sp = f.gameObject.GetComponent<SpriteRenderer>();
        c = f.gameObject.GetComponent<Collider2D>();
        e = f.gameObject.GetComponent<enemy>();


        if (f == null )
        {
            Debug.LogError("F初始化失败");
        }
        if (an == null)
        {
            Debug.LogError("an初始化失败");
        }
        if (sp == null)
        {
            Debug.LogError("sp初始化失败");
        }
        if (c == null)
        {
            Debug.LogError("c初始化失败");
        }

        if (e == null)
        {
            Debug.LogError("e初始化失败");
        }
        return this;
    }
     
    public void baseEnd()
    {
        e.A_.进入攻击范围 -= 进入攻击范围;
        e.被打 -= 被打;
        e.moving . 到达目标点Enter -= 到达目标点Enter;
        e.戒备.发现玩家了嘛 -= 发现玩家了嘛;
        e.时间结束 -= 时间结束;
        e.A_.攻击结束 -= 攻击结束;
        显示 =false;

    }
    public void bassStart()
    {
        e.被打 += 被打;
        e.A_.攻击结束 += 攻击结束;
        e.A_.进入攻击范围 += 进入攻击范围;
        e.戒备.发现玩家了嘛 += 发现玩家了嘛;
        e.moving.到达目标点Enter += 到达目标点Enter;
        e.时间结束 += 时间结束;
        显示 =true ;
    }


    public virtual void 被打()
    {
        f.To_State(E_enemyState.hid);
    }

    public virtual void 攻击结束()
    {
        f.To_State(E_enemyState.idle);
    }

    public virtual void 进入攻击范围()
    {

    }

    public virtual void 发现玩家了嘛(bool obj)
    {

    }

    public virtual   void  到达目标点Enter(点 obj)
    {

    }

    public virtual void aweak()
    {

    }



    public virtual void EnterState()
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

    public virtual void 时间结束(E_enemyState e_)
    {

    }
}
public class Hit_enemy : State_enemy_Base
{
    bool 时间结束了嘛;
    public override void EnterState()
    {
        时间结束了嘛 = false;
        e.开始计时(state, 0.5f);
    }

    public override void FixedState()
    {
        if (e.Ground)
        {
            if (时间结束了嘛)
            {
                f.To_State(E_enemyState.idle);
            }

        }
    }

    public override void 时间结束(E_enemyState e_)
    {
        if (e_==state)
        {
            时间结束了嘛 = true;
                if(e.Ground)
            {

                f.To_State(E_enemyState.idle);
            }
        }
    }
}
public class Idle_enemy : State_enemy_Base
{
    public bool 时间结束了嘛;
    public override void EnterState()
    {
        时间结束了嘛 = false;
        an.Play("idle");

        if (e.A_.进入攻击检测范围了吗)
        {
            e.开始计时(state, 0.5f);
            e.Velocity=(Vector2.zero);
            return;
        }

        e.开始计时(state,2);
        e.Velocity=(Vector2.zero);
    }

    public override void UpdateState()
    {
        if (时间结束了嘛)
        {
            if (e.戒备.发现玩家了吗)
            {

            } 
        }
    }

    public override void 时间结束(E_enemyState e_)
    {
        if (e_ != state) return;
        时间结束了嘛 = true;
        if (e.戒备.发现玩家了吗)
        {
            f.To_State(E_enemyState.find);
            return;
        }
        if (e.moving.目标.obj == e.gameObject)
            return;
        f.To_State(E_enemyState.patrol);
    }
}
public class Atk_enemy : State_enemy_Base
{
    public override void FixedState()
    {
        e.A_.攻击();
    }


    public override void 攻击结束()
    {
        Debug.LogWarning("");
        f.To_State(E_enemyState.idle);
    }
}
public class Find_enemy : State_enemy_Base
{
    public override void EnterState()
    {
        if (e.A_.进入攻击检测范围了吗)
        {
            f.To_State(E_enemyState.atk);
            return;
        }
        an.Play("move");
    }

    public override void FixedState()
    {
        e.追击();
    }

    public override void 发现玩家了嘛(bool obj)
    {
        if (!obj)
        {
            f.To_State(E_enemyState.idle);
        }
    }

    public override void 进入攻击范围()
    {
        f.To_State(E_enemyState.atk);
    }
}
public class Move_enemy : State_enemy_Base
{
    public override void EnterState()
    {
        an.Play("move");
        //e.开始计时(state, 5);
    }
    public override void UpdateState()
    {

            if (e.戒备.发现玩家了吗)
            {
                f.To_State(E_enemyState.find);
            }
    }
    public override void FixedState()
    {
        if (e.Ground)
        {

            e.moving.moving();
        }
    }

    public override void 到达目标点Enter(点 obj)
    {
        f.To_State(E_enemyState.idle);
    }

    public override void 时间结束(E_enemyState e_)
    {
        if (e_ != state) return;
        f.To_State(E_enemyState.idle);
    }
}
public    interface I_enemy_State
{

    public  E_enemyState state { get; set; }
    void 到达目标点Enter(点 obj);
    void aweak();
    void baseEnd();
    void FixedState();
    void 被打();
    void 发现玩家了嘛(bool obj);
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

    void 时间结束(E_enemyState e_);
    void bassStart();
}

public enum E_enemyState
{
    idle,
    sky,
    patrol,
    find,
    atk,
    hid,
}

public class FSM_enemy_base : MonoBehaviour,打印消息
{
    public bool 状态消息_;
    public bool 状态消息 { get => 状态消息_; set => 状态消息_ = value; }



    public I_enemy_State I_Enemy_State_L;
    public I_enemy_State  I_Enemy_State_C;
}
public class FSM_enemy : FSM_enemy_base
{



    [DisplayOnly ]
    public enemy e_;
    [HideInInspector] public Dictionary<E_enemyState, I_enemy_State> D_State = new Dictionary<E_enemyState, I_enemy_State>();
    [DisplayOnly]
    public List<State_enemy_Base> 所有的状态 = new List<State_enemy_Base>();

     
    private void Awake()
    {


        e_ = GetComponent<enemy>();


        初始化();
        foreach (var item in D_State)
        {
            item.Value.aweak();
            所有的状态.Add((State_enemy_Base)item.Value);
        }


    }
    private void FixedUpdate()
    {

        I_Enemy_State_C.FixedState();

    }
    void Start()
    {
        To_State(E_enemyState.idle);
    }
    private void Update()   //每帧运行一次
    {
        I_Enemy_State_C.UpdateState();
    }

    public void 初始化()
    {
        D_State.Add(E_enemyState.atk, new  Atk_enemy().asdasdasd(this, E_enemyState.atk));
        D_State.Add(E_enemyState.find, new Find_enemy().asdasdasd(this, E_enemyState.find));
        D_State.Add(E_enemyState.idle, new Idle_enemy().asdasdasd(this, E_enemyState.idle));
        D_State.Add(E_enemyState.patrol , new Move_enemy().asdasdasd(this, E_enemyState.patrol));
        D_State.Add(E_enemyState.hid, new Hit_enemy().asdasdasd(this, E_enemyState.hid));
    }
    public void To_State(E_enemyState E)
    {
 
        if (I_Enemy_State_C != null)
        {

            I_Enemy_State_C.ExitState();
            I_Enemy_State_C.baseEnd();
        }

        I_Enemy_State_L = I_Enemy_State_C;
        I_Enemy_State_C = D_State[E];
        if (状态消息)
        {
            string a = "敌人状态情况=\n" + "上一个状态：" + I_Enemy_State_L + "当前状态：" + I_Enemy_State_C;
            Initialize_Mono.I.Debug_(this.GetType(), a);
        }



        I_Enemy_State_C.bassStart();
        I_Enemy_State_C.EnterState();

    }

}
