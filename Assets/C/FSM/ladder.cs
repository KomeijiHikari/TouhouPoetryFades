using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladder : State_Base
{
    float 重力;
    float speed=8f;
    public override void AweakStatebase()
    {
        base.AweakStatebase();
        重力 = Player.GravityScale; 
    }

    public override void EnterState()
    {
        Player.Trigger = true;
        Player.transform.position = new Vector2(Player.ladderX, Player.transform.position.y);
        A.Playanim(A_N.ladder_0_);
        Player.GravityScale = 0;
    }

    public override void ExitState()
    {
        Player.Trigger =false;
        Player.GravityScale = 重力;
        Player.方向更新();
    }

    public override void FixedState()
    {
        if (!Player.ladder)
        { 
            //ladder  会误判     会提前打 否
            if (IP.竖直正负零!=0)
            {
                if (Time.time - f.Getstate(E_State.sky).ExiteTime >0.5f)
                    f.To_State(E_State.sky);
            }
 
        }
        A.AnimSpeed = IP.竖直正负零;

        Player.Velocity = new Vector2(0, IP.竖直正负零* speed);
    }

    public override void 按下跳跃()
    {
        if (Time.time - f.Getstate(E_State.sky).ExiteTime < 0.5f) return;
        if (Player.碰到Ground) return;
        A.Playanim(JUMAP_name.上去);
        Player.跳跃触发();


            f.To_State(E_State.sky);
    }

    public override void 方向改变(bool obj)
    {
        Player_input.假装相反方向键();
    }
}
