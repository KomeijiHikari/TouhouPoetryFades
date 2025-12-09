using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counter : State_Base
{
    enum E_蓄力状态
    {
        None,
        进入蓄力,
        蓄力中,
        蓄力好了,
        蓄力过头,
        玩泥巴去,
        蓄力OK
    } 
 
 
    public Enemy_base  E;
    public Fly_Ground Fly;




    public override void 方向改变(bool obj) { }
    public void 提示开关(bool b)
    {
        if (Fly == null) return;
        按键方向.I.gameObject.SetActive(b);
    }

    E_蓄力状态 蓄力状态_;
    /// <summary>
    /// 期间时间尺度不为1  因此要真实时间
    /// </summary>
    float Tiimme => Time.unscaledTime;
    float tim;
    void Out()
    {
        if (Player.Ground)
        {
            if (IP.按键检测_按住(IP.k.格挡))
            {
                f.To_State(E_State.gedang );
            }
            else if (IP.方向正零负!=0)
            {
                f.To_State(E_State.run);
            }
            else if (IP.方向正零负 == 0)
            {
                f.To_State(E_State.idle);
            }
          
        }
        else
        {
            f.To_State(E_State.sky);
        }

    }
    void 成功失败(bool b)
    {
        if (IP.按键检测_松开(IP.k.攻击))
        {


            f.特效(f.蓄力, false);
            f.特效(f.Pool, false);
            if (b)
            {
                蓄力状态_ = E_蓄力状态.蓄力OK;
            }
            else
            {
                蓄力状态_ = E_蓄力状态.None;
            }
            释放自我(false);
        }
    }
    E_方向 最后方向;
    void 反弹投射物()
    {
        if (Fly != null)
        {
            Debug.LogError("投射      投射"._Color(Color.blue));
            var 方向 = Fly.方向;


            if (按键方向.I.最后 != null) 方向 = 按键方向.I.最后.v;

            Fly.sp.闪光(0.3f, true);
            var a = Fly.transform.Q弹(1, 0.25f, false, true, null, false);
            Fly.StartCoroutine(a);

            Fly.暂停 = false;
            Fly.销毁 = false;

            Fly.Currrtten = Fly.L2;
            Fly.方向 = 方向;

            Initialize.时间恢复();
        }
    }
    void 释放自我(bool 进入)
    {
        if (进入)
        {
            Initialize_Mono.I.时缓不动 = true;
            A.AnimSpeed = 0;
        }
        else
        {
            Initialize_Mono.I.时缓不动 = false;
            A.AnimSpeed = 1;
        }
    }

 
    public override void ExitState(E_State e)
    {
 
        if (Fly != null) Fly.暂停 = false; 
      f.  教学模式(false);

        Player.防御状态 = Player3.防御.Null;
        最后方向 = E_方向.Null;
        蓄力状态_ = E_蓄力状态.None;
        提示开关(false);

        tim = 0;
        f.特效(f.蓄力, false);
        f.特效(f.Pool, false);

        Fly = null;
        E = null;
        A.AnimSpeed = 1;
        摄像机.I.FOV_还原(1);
        Initialize.时间恢复();
        Player.真实动画 = false; 
        Player.HPROCK = false;
        Player.方向更新();
    }
    public override void EnterState()
    {
 
        var a = (gedang)f.Getstate(E_State.gedang) ;
        if (a != null) Fly = a.Fly;
        if (a != null) E = a.E;
        if (Fly != null) Fly.暂停 = true;

 
        if (E != null) Player.伤害(E); 
 
 

        if (Fly != null)
        {
            Initialize_Mono.I.时缓(0.1f, 2f);
            Fly.反作用力(15);
        }
        else
        {
            Initialize_Mono.I.时缓(0.2f, 0.9f);
        }
        Player.防御状态 = Player3.防御.防御反击;
        f.教学模式();
        Player.HPROCK = true;
        A.Playanim(A_N.counter_0_);
        摄像机.I.FOV_缓动至(摄像机.I.当前场景默认FOV * 0.95f, 0.3f);
    }
    public override void UpdateState()
    {
        switch (蓄力状态_)
        {
            case E_蓄力状态.进入蓄力:
                if (Tiimme - tim > 0.5f && tim != 0)
                {
                    Debug.LogError("进入进入进入");
                    蓄力状态_ = E_蓄力状态.蓄力中;

                    f.特效(f.蓄力, true);

                    if (Player.N_.时缓)
                    {
                        f.真实特效(T_N.特效蓄力触发);
                    } 
                } 
                成功失败(false);
                break;
            case E_蓄力状态.蓄力中:
                if (Tiimme - tim > 1f && tim != 0)
                {
                    f.特效(f.蓄力, false);
                    f.特效(f.Pool, true);

                    if (Player.N_.时缓)
                    {
                        f.真实特效(T_N.特效蓄力结束);
                    } 
                    蓄力状态_ = E_蓄力状态.蓄力好了;
                }
                成功失败(false);
                break;
            case E_蓄力状态.蓄力好了:
                if (Tiimme - tim > 2f && tim != 0)
                {
                    f.特效(f.Pool, false);
                    蓄力状态_ = E_蓄力状态.蓄力过头;
                }
                成功失败(true);
                break;
            case E_蓄力状态.蓄力过头:
                if (Tiimme - tim > 3f && tim != 0)
                {
                    蓄力状态_ = E_蓄力状态.玩泥巴去;
                }
                成功失败(false);
                f.特效(f.Pool, false);
                break;
            case E_蓄力状态.玩泥巴去:
                f.To_State(E_State.idle);
                break;
        }

        switch (Player.防御状态)
        { 
            case Player3.防御.防御反击: 
                if (A.当前进度 > 0.99f)
                {
                    f.To_State(E_State.idle);
                }
                break; 
            case Player3.防御.反击攻击:
                if (Player.Atk)
                {
                    Player.Atk = false;
                    提示开关(false);
                    Initialize.时间恢复();

                    if (Fly != null) 反弹投射物();

                    if (蓄力状态_ == E_蓄力状态.蓄力OK && Player.N_.时缓)
                    {
                        if (E != null) Player.同速_(E);
                        else if (Fly != null) Player.同速_(Fly);
                    }


                    摄像机.I.FOV_缓动至(摄像机.I.当前场景默认FOV * 0.9f, 0.3f);
                    if (E != null) Player.反作用力(E, 3, Vector2.left * 4, Vector2.left, Vector2.left * 0.5f, Vector2.left * 1.5f);

                    if (E != null) Player.伤害(E); 
                    if (E != null) E.韧性(-100f);
                }
                if (A.当前进度 > 0.99f)
                {
                    Out();
                } 
                break; 
        }
    }
    public override void 按下(KeyCode obj)
    {
        if (obj==IP.k.攻击)
        {
            switch (Player.防御状态)
            {

                case Player3.防御.防御反击:
                    Player.防御状态 = Player3.防御.反击攻击;

                    f.教学模式();
                    提示开关(true);

                    A.Playanim(A_N.counter_Atk);
                    tim = Tiimme;
                    Player.真实动画 = true;
                    蓄力状态_ = E_蓄力状态.进入蓄力;

                    释放自我(true);
                    break;
                //case Player3.防御.反击攻击:
                //    break;
                default:
                    break;
            }
        }
    }
}
