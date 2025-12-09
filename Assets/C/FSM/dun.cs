using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dun : State_Base
{
    bool 不退出一半;
    public override void EnterState()
    {


        Player.进入一半();
        Player.Velocity = Vector2.zero;
        switch (f.I_State_L.state)
        {
            case E_State.dash:
                A.Playanim(A_N.dun_dundash);
                break;
            default:
                A.Playanim(A_N.dun_0_);
                break;
        }
 


    }
    
    public override void ExitState(E_State e)
    {
        if (!不退出一半)
        {
            Player.退出一半();
        }
        else
        {
            不退出一半 = false;
        }
    }



    public override void 按下(KeyCode obj)
    {
        if (obj == IP.k.冲刺)
        {
            if (Player.dundash.冷却好了)
            {
                f.To_State(E_State.dash);
            }
            else
            {
                Player.闪光();
            }
        }
        if (obj == IP.k.攻击)
        {
            不退出一半 = true; 
            f.To_State(E_State.dunatk);
        }

    }

    public override void 松开(KeyCode obj)
    {
        if (obj==IP.k.下)
        { 
            f.To_State(E_State.idle);
        }
    }

    public override void 离开地面()
    {
 
        f.To_State(E_State.sky);
    }
}
