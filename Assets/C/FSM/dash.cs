using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class skydash : State_Base
{
    /// <summary>
    ///  冲锋过程碰到某机关 触发？？？？
    /// </summary>
    float 原重力;

    bool 开始了;


    public override bool 能力激活的
    {

        get
        {
            能力激活的_显示 = Player.N_.空中Dash;
            return Player.N_.空中Dash;
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
        圆斩 = false;
        Debug.LogError("空空空");
        Player.一半(true );
        A.Playanim(A_N.skydash_0_);
        Player.Velocity = new Vector2(Player.Velocity.x ,3f);
    }
    
    public override void FixedState()
    {
        if (!开始了) return;

        Player.Velocity = Player.Dash_(Player.LocalScaleX_Int, Player.skydash); 
        if (Player.skydash.冲刺持续时间_<0.1f)
        {
            残影.I.开启残影(false);
        }
  
        if (!Player.skydash.冲刺显示)
        {
            if (圆斩)
            {
                f.To_State(E_State.cricleatk);
                return;
            }
            f.To_State(E_State.sky );
            return;
        }

    }
    public override void StateStart()
    {
        原重力 = Player.GravityScale;
    }
    public override void ExitState(E_State e)
    {

        圆斩 = false;
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
        //var a = Player_input.I.原生正负零();
        //if (a!=0)
        //{
        //    Player.LocalScaleX_Int = a; 
        //}  
        残影.I.开启残影(false);
        Player.加速(true);
    }
    bool 圆斩; 
    public override void UpdateState()
    {
        if ( Input.GetKeyDown(IP.k.跳跃)  )
        {
            圆斩 = true;
        }
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

    /// <summary>
    ///  0.9
    /// </summary>
    float startTTTTTTTTTTT;
    /// <summary>
    ///  0.1
    /// </summary>
    float endt;
    float startt;
    public override void StateStart()
    {
        base.StateStart();
        var a = A.GetAnim(A_N.dundash_1_).time;
        var cent = A.GetAnim(A_N.dundash_0_).time;
        var b = A.GetAnim(A_N.dundash_负1_).time;
        Debug.LogError(a +"      "+b );
        //if (a+cent+b < Player.dundash.冲刺持续时间)
        //{

        //}
        Player.dundash.冲刺持续时间 += a   + b; 
        startTTTTTTTTTTT = Player.dundash.冲刺持续时间 - a;
        startt = a;
        endt =   b;
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (A.当前进度 > 0.99f)
        {
            //if (A.当前名字 == AN.)
            //{

            //}
        }
    }
  Vector2  EnterV;
    public override void EnterState()
    {
        EnterV = Vector2.zero;
        if (f.I_State_L.state==E_State.run)
        {
            EnterV = Player.Velocity;
        }
        //Debug.LogError(Player.Velocity+"AAAAAAAAAAAAAAAAAAA");
        //Debug.LogWarning(  Player.dundash.冲刺持续时间);
        Player.NB_Dash_Time = 0.2f;
        Player.HPROCK = true;
        A.Playanim(A_N.dundash_1_);
        Player.输入DASH数据(Player.dundash);
        Player.进入一半();
        残影.I.开启残影(true, Player.dundash.冲刺持续时间-0.1f);
    }
    Vector2 Last;

    enum E_阶段
    {
         开始,
         中间,
         结束
    }
    void 阶段( )
    {
        var a = Player.Dash_(Player.LocalScaleX_Int, Player.dundash);

        var 经过 =   Player.dundash.冲刺持续时间_;
        E_阶段 j = E_阶段.开始;
        if (Player.dundash.冲刺持续时间_ < startTTTTTTTTTTT) j = E_阶段.中间;
        if (Player.dundash.冲刺持续时间_ < endt) j = E_阶段.结束;

        if (Player.保持Dash)   j = E_阶段.中间; 

        Vector2 outt= Vector2.zero;
        switch (j)
        {
            case E_阶段.开始:
                 经过 = (Player.dundash.冲刺持续时间 - Player.dundash.冲刺持续时间_)
                    /    startt ;
                经过 =  Mathf.Min(经过,1);
                经过 *= 经过;
                 outt = Vector2.Lerp(EnterV, a,    经过);  
                break;
            case E_阶段.中间:
                outt=a;
                break;
            case E_阶段.结束:
                经过 = Player.dundash.冲刺持续时间_ /   endt;
                经过 =1- Mathf.Min(经过, 1);

                Vector2 tar= Vector2.zero; 
                if (IP.方向正零负_原生 != 0) tar = new Vector2(Player.LocalScaleX_Int * Player.玩家数值.常态速度, Player.Velocity.y);

                Debug.LogError(tar+"  "+ IP.方向正零负_原生);
                经过 = 1 - (1 - 经过)*(1 - 经过);
                outt = Vector2.Lerp(a, tar,   经过);

                if (A.当前名字 == A_N.dundash_负1_ && Player.保持Dash)
                    ///已经播放 并且头 那就回去播放打回
                    A.Playanim(A_N.dundash_0_);
                else if (A.当前名字 == A_N.dundash_0_ && !Player.保持Dash)
                    ///没有播放 且头 那就播放 
                    A.Playanim(A_N.dundash_负1_);
                break; 
        }

        //Debug.LogError(经过+"     "+j+"   "+outt+"    "+ IP.方向正零负_原生);
        Player.Velocity = outt; 
    }
    public override void FixedState()
    {
 
        if (Last!=(Vector2)Player.transform .position )
        {///我在移动
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
        阶段();
      //Player.Velocity=  Player.Dash_(Player.LocalScaleX_Int, Player.dundash);

        if (!Player.Ground)
        {
            f.To_State(E_State.sky);
        }


         
        if (Player.dundash.冲刺持续时间_ < 0.1f)
        {
            残影.I.开启残影(false);
        }
        if (!Player.dundash.冲刺显示    )
        { 
            if (Player .Ground )
            {//在地上
               if (IP.按键检测_按住(IP.k.下))
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
            if (Input.GetKey(Player_input.I.k.左))
            {
                return -1;
            }
            else if (Input.GetKey(Player_input.I.k.右))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
    public override void ExitState(E_State e)
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
        Player.加速(true);

        Player.退出一半();
    }

    public override void 离开地面()
    {
        Player.强行退出DASH = true;
 //       Player.dun.冲刺持续时间 = 0f;
 //Player.dun.冲刺显示 = false;
    }

}
