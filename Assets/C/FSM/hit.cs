using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit : State_Base
{
    float 原重力;
    bool Dead;
    bool BigHit => Player.BigHit;
    public override void EnterState()
    {
  
        Player.加速(false);
        Initialize.Set_碰撞(Initialize .L_Player , Initialize.L_Air_wall,false);
        if (Player.当前hp <=0)
        {
            A.Playanim(A_N.dead);
            Player.站立box.isTrigger = true ;
            Player.po.isTrigger = true;
            Dead = true;
            Player.Velocity = Vector2.zero;
            原重力 = Player.GravityScale;
            Player.GravityScale = 0; 
        }
        else
        {
            if (BigHit)
            {
                Player.受伤.stiffnessTime_ = Player.受伤.stiffnessTime大硬直;
                A.Playanim(A_N.bighit);
                //A.Playanim(A_N.run_0_);
            }
            else
            {
                A.Playanim(A_N.hit);
            }

        }
    }

    public override    void ExitState(E_State e )
    {
        Initialize.Set_碰撞(Initialize.L_Player, Initialize.L_Air_wall,true);
        Initialize_Mono.I.Waite(() => {
            Initialize.Set_碰撞(Initialize.L_Player, Initialize.L_Air_wall, true);
            },0.1f);
        if (Dead)
        {
        Player3.I.生命归零?.Invoke();
            Debug.LogError("        Player.生命归零?.Invoke();        Player.生命归零?.Invoke();");
        Dead = false;
        Player.Velocity = Vector2.zero;
            Player.开启灵魂();
            Player3.I.ChangeFather();
            yalaAudil.I.EffectsPlay("ReLive", 0);
            

            this.Player.站立box.isTrigger =false ;
            Player.po.isTrigger =false ;

            Player.当前hp = Player.hpMax;

            Player.GravityScale = 原重力;
        }
        Player.BigHit = false;
    } 
    public override void Hit()
    {
        //Debug.LogError("死了又死");
    }
    
    public override void UpdateState()
    {
        if (A.AnimSpeed==0)
        {//谜一样的bug 动画速度会归0
            A.AnimSpeed = 1;
        }
            if (Dead)
        {
            Player.Velocity = Vector2.zero;
            Player.备用地面检测21 = false;
            if (Player.End)
            {
                Player.End = false;
                if (Player.Ground)
                {
                    f.To_State(E_State.idle);
                }
                else
                {
                    f.To_State(E_State.sky);
                }
            }
        }
        else if (BigHit)
        {
            if (Player.受伤.stiffnessTime_ < 0)
            {
                if (Player.Ground)
                {
                    f.To_State(E_State.idle);
                }
                else
                {
                    f.To_State(E_State.sky);
                }
            }
            else
            {
                if (Player.Ground&& EnterTime > 0.1f)
                {
                    A.Playanim(A_N .hitwaite);
                }
            }
        }
        else
        {
            if (Player.受伤.stiffnessTime_ < 0)
            {
                if (Player.Ground)
                {
                    f.To_State(E_State.idle);
                }
                else
                {
                    f.To_State(E_State.sky);
                }
            }  
            else if (Player.Ground&& EnterTime >0.1f)
            {
                Player.Velocity = Player.Velocity / 2f;
                    A.Playanim(A_N.idle_0_); 
            }
        } 
    }
}
