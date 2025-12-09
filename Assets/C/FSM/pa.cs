using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pa : State_Base
{
    float 原先重力;
    public override bool 可以切换嘛()
    {
        return false; 
        if (Player.玩家数值.Boss杀手) return false;
        
        //Debug.LogError("AAAAAAAAAAAAAAAAAAAA");
        //Debug.LogError(Player.脚底发射(1));
        //Player.脚底中间.DraClirl(0.1f,Color .green,89);
        //((Vector2)Player.站立box.bounds.center).DraClirl(0.1f, Color.red, 30);
        if (Time.time -  ExiteTime<1f||
            Player.脚底发射(2)!=Vector2 .zero  
            )
        {
            return false;
        }
        else
        {
            return true ;
        }
        
    }
    public override void ExitState(E_State e)
    {
        Player.GravityScale = 原先重力;
    }
    public override void EnterState()
    {
        Player.Velocity = Vector2.zero;
        原先重力 = Player.GravityScale;
        Player.GravityScale = 0;

        Vector3 差 = Player.悬挂.手的位置- Player.悬挂.Poin;
        Player.transform.position -= 差;
  
        A.Playanim(A_N.pa_0_);
    }
 
    public override void UpdateState()
    {
        Player.Velocity = Vector2.zero;

        if (Player.End)
        {
            Player.transform.position = f.改变后的.transform.position;

            f.To_State(E_State.idle);
            //if (IP.方向正零负 ==0)
            //{
            //    f.To_State(E_State.idle);
            //}
            //else
            //{
            //    f.To_State(E_State.run);
            //}
 
        }




        if (EnterTime>0.1f )
        {
            if (IP.按键检测_按住(IP.k.上))
            {
                A.Playanim(A_N.pa_to_);
            }
            if (IP.按键检测_按住(IP.k.下))
            {
                f.To_State(E_State.sky);
            }
        }
    
    }
    public override void 方向改变(bool obj)
    {
        Player_input.假装相反方向键();
    }
}
