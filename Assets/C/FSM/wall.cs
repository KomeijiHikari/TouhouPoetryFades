using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.LookDev;



public class wall_surfing : State_Base
{
    public override bool 能力激活的
    {
        get
        {
            能力激活的_显示 = Player.N_.墙冲浪;
            return Player.N_.墙冲浪;
        }
        set
        {
            Player.N_.墙冲浪 = value;
            能力激活的_显示 = value;
        }
    }
    public override void FixedState()
    {
        base.FixedState();
        Player.Velocity = new Vector2(0, 25); 
        if (!Player.顶死)
        {
            f.To_State(E_State.sky);
            return;
        }
        if (!Player.头空_)
        {
            Player.Velocity = new Vector2(0, 0);
            f.To_State(E_State.sky);
        }
    }
}

public class wall : State_Base
{
    bool 挂在move_P上;

    bool 按下了相反;
    int 第一次进来的时间_ { get; set; }
    //public override bool 能力激活的 { 
    //    get {
    //     能力激活的_显示 = Player.N_.爬墙; 
    //        return Player.N_.爬墙; }
    //    set { Player.N_.爬墙 = value;
    //        能力激活的_显示 = value;
    //    }
    //}

 Vector2 asd() ///需要知道X位置 以及是不是move
    {
        var c = Physics2D.Raycast
            (Player.Bounds.center, new Vector2(Player.LocalScaleX_Int, 0), 3f
            , 1 << Initialize.L_M_Ground | 1 << Initialize.L_Ground) ;
        if (c.collider==null)
        {
            Debug.LogError("离谱  碰到了但是没有碰到");
            return Vector2.zero;
        }
        if (c.collider .gameObject.layer==Initialize.L_Ground)
        {
            var a = c.collider.gameObject.GetComponent<单方面通过>(); //雪块碰到就化而不是爬上去

            if (a==null)
            {   ///不是雪块
                return new Vector2(c.point.x, 0);
            }
            if (a!=null)
            {/// 是雪块
                var b =    a.触发(false);
                if (b)
                {
                    return new Vector2(c.point.x, 0);
                    //可以爬
                }
                else
                {
                    return Vector2.zero;
                }
            }
        }
        else  
        {
            c.point.DraClirl(100,Color.green,10);
            return new Vector2(c.point.x, 1);
        } 
        return Vector2.zero;
    }
    void  addd()
    {
        var c = Physics2D.Raycast
           (Player.Bounds.center, new Vector2(Player.LocalScaleX_Int, 0), 3f
           , 1 << Initialize.L_M_Ground | 1 << Initialize.L_Ground);
    }
 bool 距离地面很近(float jul)
    {
        //return false;
        var a= Player.地面检测(jul);
      
        return a .Length>0;
    }
    public override bool 可以切换嘛()
    { 
        if (距离地面很近(1.8f)) return false; 
        var a = asd();
        if (a==Vector2.zero)
        {
            Debug.LogError("离谱  碰到了但是没有碰到");
            return false;
        }
        else
        {
            if (a.y==1)
            {
                ///是move 平台
            }
            else 
            {
                /// 不是move 平台  移动位置;

                Player.transform.position = new Vector2
                     (a.x - (Player.距离墙面的距离 * Player.LocalScaleX_Int), Player.transform.position.y);
                Debug.LogError(Player.transform.position);
            }
        }
        var x = Player.transform.position.x;
        var b = f.Wall_X._is(x,0.1f);
        Debug.LogError(b);
        return true;
        if (!b)
        {
           f.Wall_X = x;
            return true;
        }
        else
        {
             
            return false;
        }

    }
    public override void ExitState(E_State e)
    { 
        按下了相反 = false;

        if (!is_wall_surfing)
            Player3.I.ChangeFather();

    } 
    
    public override void EnterState()
    { 
        is_wall_surfing = false;
      var c=  Physics2D.Raycast(Player.Bounds.center,new Vector2(Player.LocalScaleX_Int,0),1f,1<<Initialize .L_M_Ground   ).collider;
        if (c!=null)
        {
            Debug.LogError("挂在上面");
            Debug.LogError(Player.transform.position);
            挂在move_P上 = true;
            Player .ChangeFather(c.transform);
            //c.GetComponent<Move_P>().设置父级(Player.transform);
            Debug.LogError(Player.transform.position);
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

        //Debug.LogError(Player.transform.position);
    }
    Vector2 Last { get; set; }
    void 录入(KeyCode obj, bool b)
    {
        if (obj == IP.k.左)
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
        else if (obj == IP.k.右)
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
    bool is_wall_surfing;
    public override void UpdateState()
    {

        //Debug.LogError(Player.transform.position);

        if (!Player.顶死)
        {

            f.To_State(E_State.sky);//滑落，下坠 
            return;
        }
        //Debug.LogError(Player.transform.position);
        if (IP.按键检测_按下(IP.k.冲刺))
        {

            is_wall_surfing = true;
            f.To_State(E_State.wall_surfing);//滑落，下坠 
        } 
        if (IP.方向正零负== Player.transform.localScale.x   )
        {
            if (Last!=Vector2.zero)
            {
               Player.transform.localPosition  = Last;
            }
            Player.Velocity = Vector2 .zero;
            //Debug.LogError(Player.transform.position);
        }
        else
        {
            //Debug.LogError(Player.transform.position);
 
            Player.Velocity = new Vector2(Player.Velocity.x, Mathf.Clamp(Player.Velocity.y, -1f, float.MaxValue));

            //Debug.LogError(Player.transform.position);
        }

 
        //Debug.LogError(Player.transform.position);
 
        if (!按下了相反)
        {
            if (Player.transform.localScale.x != IP.方向正负&& IP.按键检测_按下(IP.k.跳跃))
            {
                金庸();
                //一起按下                    Player.方向更新();
                Debug.LogError("情况1"); 

                Player.跳跃触发();


           
                f.To_State(E_State.sky);
            }
          else  if (Player.transform.localScale.x != IP.方向正负)
            {//第一次进来
             //相反
 
                Debug.LogError("按下了相反方向键");
                按下了相反 = true;
                第一次进来的时间_ =Time .frameCount;
                Player_input.假装相反方向键();
                 
                return;
            }
            else if (IP.按键检测_按下(IP.k.跳跃))
            {
                金庸();
                Debug.LogError("只按了跳跃");
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
                if (IP.按键检测_按下(IP.k.跳跃))
                {
                    Debug.LogError("一起按下");
                    金庸();
                    Player.方向更新();
 
                    Player.跳跃触发( );


                    f.To_State(E_State.sky); 
                }
            }
            else
            {//时间之外 
                Debug.LogError("没有按");
                Player.方向更新();
                f.To_State(E_State.sky);
            }
        }

        if (距离地面很近(1.7f))
        {
            Player.方向更新();
            f.To_State(E_State.sky);
        } 
    }

    private void 金庸()
    {
        Player3.I.记录a(); 
    }

    public override void 接触地面()
    {
        f.To_State(E_State.sky);
        f.Getstate(E_State.sky).接触地面();
        //Initialize_Mono.I .Waite(  ()=>I.接触地面事件.Invoke());
    }

    public override void 方向改变(bool obj) {   }
     
}
