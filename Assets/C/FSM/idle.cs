using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaction : State_Base
{
    public override void Hit()
    { 
    }
    public override void 方向改变(bool obj)
    {
        Player_input.假装相反方向键();
    }
}

public class idle : State_Base
{

public override void StateStart()
    {
        base.StateStart();
    }

 
    public override void EnterState()
    {
  
        if (f.I_State_L == null)
        {
            //为空时 
            A.Playanim(A_N.idle_0_);
        }
        else
        {
            switch (f.I_State_L.state)
            {
                case E_State.run:
                    {
                        var name = A_N.idle_run_to0;
                        if (A.当前anim.name == A_N.run_0_)
                        {
                            //和跑步循环后打断 
                            name =A_N.idle_run_to0;
                            //Player.AddForce(new Vector2(IP.方向正负 * 50, 0));
                        }
                        else if (A.当前anim.name == A_N.run_idle_to0)
                        {
                            if (A.当前anim.进度 >= 0.3f)
                            {//起跑动画开跑的时候打断,   播放超过一定时间就播放停止
                                name = A_N.idle_run_to0 ;
                            }
                            else if(A.当前anim.进度 < 0.3f)
                            {
                                name = A_N.idle_0_ ;
 
                            }
                            else if (f!=null&&f.I_State_L != null && f.I_State_LL != null && f.I_State_LLL!=null)
                            {
                               if ( (f.I_State_LLL.state == E_State.sky
                                    && f.I_State_LL.state == E_State.idle
                                               && f.I_State_L.state == E_State.run
                                                     && Time.frameCount - f.Getstate(E_State.sky).ExiteFramet < 10)  )
                                {///sky  idle run   idle 
                                    A.NextAnim(A.GetAnim(A_N.idle_run_to0), 0.5f, 2f);
                                    A.animator.speed = 1.3f;
                                }
                            }
                            else
                            {                    //轻按一下
                                name = A_N.idle_0_ ; 
                            }
                            //if (Player.玩家数值.Boss杀手) 
                            //    Player.缓慢反向力(0.1f); 
                        }
                        else if (A.当前anim.name == A_N.run_chang&& A.当前anim.进度<0.45f)
                        {
                            name = (A_N.idle_0_);
                        }
                        else if (A.当前anim.name == A_N.run_jump_to0)
                        {
                            //A.Next = A_N.idle_run_to0;
                            A.NextAnim(A.GetAnim(A_N.idle_run_to0));
                        }
                        else if (A.当前anim.name == A_N.run_jump_to0)
                        {//延迟结束     提前切
                         // 
                         //A.Next = A_N.idle_0_;
                            A.NextAnim(A.GetAnim(A_N.idle_0_));
                        }
                        else
                        {
                            name = (A_N.idle_run_to0);
                        }
                  Player.      缓慢反向力(0.3f); 
                        A.Playanim(name);
                    }
       
                    break;
                case E_State.downatk:
                case E_State.skyatk:
                case E_State.sky:
                    if(IP.方向正零负 ==0)
                    { Player.Velocity = new Vector2(Player.Velocity.x/3,0); }
                    A.Playanim(A_N.idle_jump_to0 ); 
                    break;
                case E_State.dun:
                case E_State.atk:
                case E_State.dash:
                case E_State.hit:
                    Player.Velocity = Vector2.zero;
                        A.Playanim(A_N.idle_0_);
                    break;

                case E_State.counter:
                case E_State.gedang:
                      A.Playanim(A_N.idle_gedang_to0); 
                    break;
                case E_State.pa:
                    A.Playanim(A_N.idle_jump_to0,0.5f);
                    break;
                case E_State.upatk:
                                        A.Playanim(A_N.idle_jump_to0, 0.5f);
                    break;
            }
        }
        #region  a
        //if (f.I_State_L == null)
        //{
        //    //为空时
        //    A.Playanim("idle_0_");
        //}
        //else if (f.I_State_L.state == E_State.run)
        //{
        //    if (A.当前anim.name == "run_0_")
        //    {
        //        //和跑步循环后打断
        //        A.Playanim("idle_run_to0");
        //    }
        //    else if (A.GetAnim("run_idle_to0").进度_ >= 0.3f)
        //    {//起跑动画开跑的时候打断
        //        A.Playanim("idle_run_to0");
        //        I.rb.AddForce(new Vector2(-IP.方向正负 * I.玩家数值.起步速度 * 3, 0));
        //    }
        //    else if (A.当前anim.name == "run_chang_to0")
        //    {
        //        A.Playanim("idle_run_to0");
        //    }
        //    else if( Mathf.Abs ( I.rb.velocity.x)<3f)
        //    {
        //        //起跑动画播放小于0.3秒打断
        //        //跑步动画没播完就打断就没惯性
        //        I.rb.velocity = Vector2.zero;
        //        if (f.I_State_LL.state == E_State.sky)
        //        {
        //            A.Playanim("idle_0_");
        //            //A.Playanim("idle_run_to0");
        //        }
        //        else
        //        {
        //            A.Playanim("idle_0_");
        //        }
        //    }
        //    else
        //    {
        //        A.Playanim("idle_run_to0");
        //    }   
        //}
        #endregion
        //Initialize.Set_碰撞(Initialize.L_Enemy, Initialize.L_Player, false);

    }
    public override void 按住(KeyCode obj)
    {
 
        if (obj == IP.右 || obj == IP.左)
        {
            if (Input.GetKey(IP.左) && Input.GetKey(IP.右)) return;
            f.To_State(E_State.run);
 
        }
 
    }

    public override void ExitState()
    {
 
        //Initialize.Set_碰撞(Initialize.L_Enemy, Initialize.L_Player, true);
        Player.方向更新(); 
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
    public override void 方向改变(bool obj)   { 
        Player_input.假装相反方向键();
 
    }
    public override void 松开(KeyCode obj)
    {
        if (obj == IP.攻击)
        {
            if (Player_input .I.按键检测_按住(IP.上)  )
            {
                Debug.LogError("AAAAAAAAAAAAAAAA");
                f.To_State(E_State.upatk);
                return; 
            }

            if (Time.frameCount - f.Getstate(E_State.atk).ExiteFramet >atk.FrametColod)
            { 
                f.To_State(E_State.atk); return;
            }
        } 
    }

    public override void 按下(KeyCode obj)
    {

        if (obj == IP.格挡 )
        {
            f.To_State(E_State.gedang); return;
        }

        else if (obj == IP.冲刺)
        { 
                f.To_State(E_State.dash); return;
        }
        else if (obj== IP.下)
        {
 
            f.To_State(E_State.dun); return;
        }

    }
 

}
