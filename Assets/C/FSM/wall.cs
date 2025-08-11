using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : State_Base
{
    bool 挂在move_P上;

    bool 按下了相反;
    int 第一次进来的时间_ { get; set; }
    public override bool 能力激活的 { 
        get {
         能力激活的_显示 = Player.N_.爬墙; 
            return Player.N_.爬墙; }
        set { Player.N_.爬墙 = value;
            能力激活的_显示 = value;
        }
    }

    public override void ExitState()
    { 
        按下了相反 = false;
        Player3.I.ChangeFather();
    } 
    public override void EnterState()
    {
      var c=  Physics2D.Raycast(Player.Bounds.center,new Vector2(Player.LocalScaleX_Int,0),1f,1<<Initialize .L_M_Ground   ).collider;
        if (c!=null)
        {
            挂在move_P上 = true;
            Player .ChangeFather(c.transform);
            //c.GetComponent<Move_P>().设置父级(Player.transform);
        } 
        Player.Velocity = Vector2.zero;
        Last = Player.transform.localPosition;
        switch (Player.e_wall)
        { 
            case Player3.E_wall.OIOO:
                A.Playanim(A_N.wall_0_1);
                break;
            default:
                A.Playanim(A_N.wall_0_);
                break;
        } 


    }
    Vector2 Last { get; set; }
    void 录入(KeyCode obj, bool b)
    {
        if (obj == IP.左)
        {
            if (Player.LocalScaleX_Int == -1)
            {
                if (b)
                {
                    Last = Player.transform.localPosition;
                }
                else
                {
                    Last = Vector2.zero;
                }

            }
        }
        else if (obj == IP.右)
        {
            if (Player.LocalScaleX_Int == 1)
            {
                if (b)
                {
                    Last = Player.transform.localPosition;
                }
                else
                {
                    Last = Vector2.zero;
                }
 
            }
        } 
    }
    public override void 按下(KeyCode obj)
    {
        录入(obj, true);
    }

    public override void 松开(KeyCode obj)
    { 
        录入(obj, false);
    }
    public override void UpdateState()
    {
 
        if ( !Player.顶死) f.To_State(E_State.sky);//滑落，下坠 

        if (IP.方向正零负== Player.transform.localScale.x   )
        {
            if (Last!=Vector2.zero)
            {
               Player.transform.localPosition  = Last;
            }
            Player.Velocity = Vector2 .zero;
        }
        else
        { 
            Player.Velocity = new Vector2(Player.Velocity.x, Mathf.Clamp(Player.Velocity.y, -1f, float.MaxValue));
        }
 


        if (!按下了相反)
        {
            if (Player.transform.localScale.x != IP.方向正负&& IP.按键检测_按下(IP.跳跃))
            {
                //一起按下                    Player.方向更新();

                Player.跳跃触发();
                f.To_State(E_State.sky);
            }
          else  if (Player.transform.localScale.x != IP.方向正负)
            {//第一次进来
             //相反
                按下了相反 = true;
                第一次进来的时间_ =Time .frameCount;
                Player_input.假装相反方向键();
                 
                return;
            }
            else if (IP.按键检测_按下(IP.跳跃))
            {
                //只按了跳跃
 
                Player.跳跃触发(new Vector2(-Player.transform.localScale.x * 8f, Player.玩家数值.跳跃瞬间速度));
                Player.方向更新();
                f.To_State(E_State.sky);
            }
        }
        else
        {//第二次进来
            var a = Time.frameCount - 第一次进来的时间_<10;
            if (a)
            {//时间之内
                if (IP.按键检测_按下(IP.跳跃))
                {
                    Player.方向更新();
 
                    Player.跳跃触发( ); 
                    f.To_State(E_State.sky); 
                }
            }
            else
            {//时间之外 
                Player.方向更新();
                f.To_State(E_State.sky);
            }
        } 
      } 
    public override void 接触地面()
    {
        f.To_State(E_State.sky);
        f.Getstate(E_State.sky).接触地面();
        //Initialize_Mono.I .Waite(  ()=>I.接触地面事件.Invoke());
    }

    public override void 方向改变(bool obj) {   }
     
}
