using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class skydash : State_Base
{
    float 原重力;

    bool 开始了;


    public override bool 能力激活的
    {

        get
        {
            能力激活的_显示 = Player.N_.空中Dash;
            return Player.N_.Dash;
        }
        set
        {
            Player.N_.空中Dash = value;
            能力激活的_显示 = value;
        }
    }
    public override bool 可以切换嘛()
    {
        if (Player.skydash.冷却好了)
        {
            return true;
        }
        else
        {
            Player.闪光();
            return false;
        }

    }
    public override void EnterState()
    {
        Debug.LogError("空空空");
        Player.一半(true );
        A.Playanim(A_N.skydash_0_);
        Player.Velocity = new Vector2(Player.Velocity.x ,3f);
    }
    
    public override void FixedState()
    {
        if (!开始了) return;

        Player.Dash_(Player.LocalScaleX_Int, Player.skydash); 
        if (Player.skydash.冲刺持续时间_<0.1f)
        {
            残影.I.开启残影(false);
        }
        if (!Player.skydash.冲刺显示)
        {
            f.To_State(E_State.sky );
        } 
    }
    public override void StateStart()
    {
        原重力 = Player.GravityScale;
    }
    public override void ExitState()
    {
        //Player.NB_Dash_Time = 0;
        if (Player.skydash .冲刺持续时间_ >= 0)
        {
            Player.DASH数据重制(Player.skydash);
        }
        Player.一半(false);
        开始了 = false;
        Player.GravityScale = 原重力;
        //Player.Velocity = new Vector2(Player.LocalScaleX_Int *Player.玩家数值 .常态速度 ,0f);
        Player.方向更新();
        残影.I.开启残影(false); 
    }

    public override void UpdateState()
    { 
        if (Player.Atk)
        {

            Player.Atk = false;
            Player.输入DASH数据(Player.skydash);
            开始了 = true;
            Player.GravityScale = 0; 
            残影.I.开启残影(true);
        } 
    }
}
public class dash : State_Base
{
    public override bool 能力激活的
    { 
        get
        {
            能力激活的_显示 = Player.N_.Dash;
            return Player.N_.Dash;
        }
        set
        {
            Player.N_.Dash = value;
            能力激活的_显示 = value;
        }
    }

    public override bool 可以切换嘛()
    {
        if (Player.dundash.冷却好了)
        {
            return true;
        }
        else
        {
            Player.闪光();
            return false ;
        }

    }
    public override void EnterState()
    {
        Debug.LogWarning(  Player.dundash.冲刺持续时间);
        Player.NB_Dash_Time = 0.2f;
        Player.HPROCK = true;
        A.Playanim(A_N.dundash_0_);
        Player.输入DASH数据(Player.dundash);
        Player.进入一半();
        残影.I.开启残影(true, Player.dundash.冲刺持续时间-0.1f);
    }
    Vector2 Last;
    
    public override void FixedState()
    {
 
        if (Last!=(Vector2)Player.transform .position )
        {
            Last = Player.transform.position; 
        }
        else if (Player.头空_)
        {
            Player.强行退出DASH = true;
        }
        Player.保持Dash = !Player.头空_;
        if (!Player.前空_ && !Player.头空_)
        {
            Player.保持Dash = true;
       Player.Flip();
        } 

        Player.Dash_(Player.LocalScaleX_Int, Player.dundash);
         

        if (Player.dundash.冲刺持续时间_ < 0.1f)
        {
            残影.I.开启残影(false);
        }
        if (!Player.dundash.冲刺显示    )
        { 
            if (Player .Ground )
            {//在地上
               if (IP.按键检测_按住(IP.下))
            {//按住蹲
                f.To_State(E_State.dun);
            }
               else
            {

                if (真实正负0 == 0)//方向正零负无效，因为，DASH期间输入无效                      Debug.LogError(IP.方向正零负);
                    {
                    f.To_State(E_State.idle);
                }
                else
                {
                    f.To_State(E_State.run);
                }
               }  
            }
            else
            {
                f.To_State(E_State.sky);
            }
        }
        else
        {
            //Debug.LogError(Player.Velocity);
        }
    }
    int 真实正负0
    {
        get
        {
            if (Input.GetKey(Player_input.I.左))
            {
                return -1;
            }
            else if (Input.GetKey(Player_input.I.右))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
    public override void ExitState()
    {
        Player.NB_Dash_Time =0f;
        if (Player.dundash.冲刺持续时间_>=0)
        {
            Player.DASH数据重制(Player.dundash);
        }
        Player.HPROCK = false;
        Player.保持Dash = false;
        Player.强行退出DASH = false;

        残影.I.开启残影(false);
        Player.退出一半();
    }

    public override void 离开地面()
    {
        Player.强行退出DASH = true;
 //       Player.dun.冲刺持续时间 = 0f;
 //Player.dun.冲刺显示 = false;
    }

}
