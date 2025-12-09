using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class run : State_Base 
{
    public override void AweakStatebase()
    {
        base.AweakStatebase();
        原先速度 = Player.玩家数值.常态速度;
        加速度 = 原先速度 * 1.5f;
    }
    public override void EnterState()
    {
        //Player.脚下 = null;
        var sc = Player.transform.localScale;
        Player.transform.localScale = new Vector2(Mathf .Sign (sc.x),1);
        //if (f.I_State_L.state == E_State.dun)
        //{
        //    A.Playanim("run_idle_to0");
        //}
        switch (f.I_State_L.state)
        {
      
            case E_State.idle:
                {
                    Player.Velocity = Player.Velocity = Vector2.zero;
                    if (
             (Player.transform.localScale.x == 1 && Input.GetKey(IP.k.左)
             ||
             Player.transform.localScale.x == -1 && Input.GetKey(IP.k .右)
             ) && A.翻转开关
            
              )
                {
                    Debug.Log(Initialize._Color("翻转调用" + Player.transform.localScale.x + Input.GetKey(IP.k.左) + Input.GetKey(IP.k.右) + A.翻转开关, Color.green));
                    A.Playanim(A_N.run_chang);
                }
                else if (A.当前anim.name  == A_N.idle_jump_to0)
                { //  想要打断静止的蹲起
                  //A.Playanim("run_idle_to0");
                    var a = A.GetAnim(A_N.run_idle_to0);
                    a.speed = 1.3f;
                    A.NextAnim(a, 0.7f, 1f);   //现在的赶快播完，否则有可能在播期间   会变idle                                 播完了才能切

                    //A.时间界限 = 0.7f;
                    //A. Next = A_N.run_idle_to0;
                }
                else
                {
                    //Player .方向更新 ()
                    A.Playanim(A_N.run_idle_to0); 
                }
        }
        break; 
            case E_State.sky:
            case E_State.skyatk:
            case E_State.downatk:
                //Player.Velocity = Player.Velocity = Vector2.zero;
                yalaAudil.I.EffectsPlay("DallGround", 0);
                A.Playanim(A_N.run_jump_to0 );
                break;
            case E_State.atk:

                A.Playanim(A_N.run_idle_to0, 0.25f);
                Player.Velocity = (new Vector2 (Player .LocalScaleX_Int*Player.玩家数值.常态速度 ,0)); 
                break; 
            case E_State.dash:

 
                A.Playanim(A_N.run_dundash);
                Player.Velocity = (new Vector2(Player.LocalScaleX_Int * Player.玩家数值.常态速度     , 0));

                Player.加速(true);
                break;
            case E_State.counter:
            case E_State.gedang:
                A.Playanim(A_N.run_idle_to0, 0.25f);
                Player.Velocity = (new Vector2(Player.LocalScaleX_Int * Player.玩家数值.常态速度, 0));
                break;
            //case E_State.pa:
            //    Player.Velocity = (new Vector2(Player.LocalScaleX_Int * Player.玩家数值.常态速度, 0));
            //    A.Playanim(A_N.run_jump_to0, 0.5f);
            //    break;

        } 
    }
    float 加速度时间 = 3;
    float 加速度 { get; set; }
    float 原先速度 { get; set; }

    public override void FixedState()
    {
        if (false)
            if (Player.Player_Father_False != null)
        {
     
                Player.transform.localPosition += new Vector3(IP.方向正零负 * Player.玩家数值.常态速度, 0, 0) * Time.fixedDeltaTime;

            if (Player.脚下 != null && Player.脚下.移动方式 == Move_P.方式.水平)
            {
                Debug.LogError("水平移动平台上");
                if (Player.脚下.方向 == Player.LocalScaleX_Set)
                {
                    Player.Changef(Player3.I.Player_Father);///设置之后正方向罚站
                    //Player.Velocity = new Vector2(IP.方向正零负 * Player.玩家数值.常态速度, Player.Velocity.y);
                    Player.transform.localPosition += new Vector3(IP.方向正零负 * Player.玩家数值.常态速度, 0, 0) * Time.fixedDeltaTime;
                }
                else if (Player.脚下.方向 == -Player.LocalScaleX_Set)
                {


                    //Player.Velocity = new Vector2(IP.方向正零负 * Player.玩家数值.常态速度, Player.Velocity.y);
                    Player.transform.position += new Vector3(IP.方向正零负 * Player.玩家数值.常态速度, 0, 0) * Time.fixedDeltaTime;
                }

            }
        }
        else
        {
            Player.Velocity = new Vector2(IP.方向正零负 * Player.玩家数值.常态速度, Player.Velocity.y);
        }
        Player.Velocity = new Vector2(IP.方向正零负 * Player.玩家数值.常态速度, Player.Velocity.y);

        //Debug.LogError( "速度" + Player.玩家数值.常态速度);
 
        //玩家水平运动设置空物体    静止和竖直 是父物体
        //if (Player.Player_Father_False != null)
        //{
        //    Player.Changef(Player3.I.Player_Father);
        //} 

        //Player.Velocity = new Vector2(IP.方向正零负 * Player.玩家数值.常态速度, Player.Velocity.y);
        //Player. AddForce(new Vector2(IP.方向正零负 * Player.玩家数值.起步速度, 0));
        if (  EnterTime>0.1f  )
        {
            Player.水平限制();
        }

        //if (Player.脚下 != null && Player.脚下.移动方式 == Move_P.方式.水平)
        //{
            //var a = -Player.脚下.帧移动距离 * Player.脚下.方向;
            //if (Player.脚下.方向 == Player.LocalScaleX_Set)
            //{
            //    Player.transform.position -= new Vector3(a * 3, 0, 0);
            //}
            //else if (Player.脚下.方向 == -Player.LocalScaleX_Set)
            //{
            //    Player.transform.position += new Vector3(a, 0, 0);
            //}

        //}
    }
    public override void ExitState(E_State e)
    {
        if (Player.Player_Father_False != null)
            Player.Changef(Player3.I.Player_Father_False);

        //Player.   脚下 = null;
        加速(false);
    }
    public override void 按下跳跃()
    {
        if (!Player.头顶没有挤压()) return;
        Player.玩家数值.跳跃剩余跃次数--;
 

        Player.跳跃触发();
        f.To_State(E_State.sky);
    }
    public override void 离开地面()
    {
        f.To_State(E_State.sky);
    }
    void 加速(bool b)
    {
        return; 
        Debug.LogError("加速");
        if (b)
        {
     
            Player.玩家数值.常态速度 = 加速度;
            残影.I.开启残影(true);

        }
        else
        {
            Player.玩家数值.常态速度 = 原先速度;
            残影.I.开启残影(false);  
        }

    }

    public override void 松开(KeyCode obj)
    {
        if (obj == IP.k.攻击)
        {
            if (Time.frameCount - f.Getstate(E_State.atk).ExiteFramet >atk. FrametColod)
            {
                f.To_State(E_State.atk); return;
            }
        }
 


    }
    public override void UpdateState()
    {
        if (EnterTime>加速度时间)
        {
            加速(false);
        }
        //if (!IP.Get_key(IP.冲刺).被按下了吗)
        //{
        //    加速(false);
        //}

        if (IP.方向正零负==0)
        {
            if (IP.方向正零负_非零计时器>1f)
            {
                //A.Playanim("run_0_");
                Player.Velocity = new Vector2(8f * IP.方向正负, Player.Velocity.y);
            }
            //Debug.LogError("ASDASDASDADS"+ IP.方向正零负_非零计时器);
            f.To_State(E_State.idle);
        }
 

    }

    public override void 方向改变(bool obj)
    {
 

            if (A.当前anim .name== A_N.run_0_)
        {
            //进入跑步循环动画后才会触发，否则idle反方向就会播放这个
            A.Playanim(A_N.run_chang);
        }

        Player.方向改变 (obj);
        Player.Velocity = Vector2.zero;
    }
    public override void 按下(KeyCode obj)
    {
        if (obj == IP.k.格挡)
        {
            f.To_State(E_State.gedang); return;
        }
        if (obj == IP.k.冲刺)
        { 
            
                f.To_State(E_State. dash); 
        }
       else if (obj == IP.k.下)
        {
            f.To_State(E_State.dun);
        }
        else if (obj == IP.k.攻击)
        { 
            //f.To_State(E_State.atk);
        }
        //else if (obj == IP.格挡)
        //{
        //    f.To_State(E_State.counter );
        //} 
    }
}