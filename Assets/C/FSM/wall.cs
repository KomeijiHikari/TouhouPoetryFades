using JetBrains.Annotations;
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

    float 缓冲时间Max_长=0.3f;

    float 缓冲时间Max_短 = 0.1f;
    float 缓冲时间Max2 = 1f;
    float 第一次进来的时间_ { get; set; }
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
        var a = Player.地面检测(1 << Initialize.L_Ground | 1 << Initialize.L_M_Ground, jul);
        return a .Length>0;
    }
    public override bool 可以切换嘛()
    {

        if (距离地面很近(1.8f)) return false;
        return true;
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
      Player.  wall_进入为正面 = Player.LocalScaleX_Int ;
        Debug.LogError("   Player.  wall_进入为正面  " + Player.wall_进入为正面);
        //return;
        is_wall_surfing = false;

        Player.空中攻击过了 = false;
        Player.圆形攻击过了 = false;
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
    void 录入(KeyCode obj, bool 按下)
    {
        bool 同向 = false;
        if (obj == IP.k.左&& Player.LocalScaleX_Int == -1)
            同向 = true;
        else if (obj == IP.k.右&& Player.LocalScaleX_Int == 1) 
            同向 = true; 

        if (同向)
        {
            if (按下) 

                Last = Player.transform.localPosition;
   
            else 
                Last = Vector2.zero; 
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
        ///上一半打开 有没有  没有
        ///下一班打开有没有  没有
        ///全开有 
        ///上一半打开 有没有  有
        /// 上三有没有  没有
        /// 上一二关 三开有没有     有
        /// 后if 有没有
        if (距离地面很近(1.7f))
        {
            f.To_State(E_State.sky);//滑落，下坠 
            return;
        }
        //Debug.LogError(Player.transform.position);
        //if (false)
        if (!Player.顶死)
        {

            f.To_State(E_State.sky);//滑落，下坠 
            return;
        }
 
            if (IP.按键检测_按下(IP.k.冲刺))
        { 
            is_wall_surfing = true;
            f.To_State(E_State.wall_surfing);//滑落，下坠 
        }
        bool 同输入 = ( IP.按键检测_按住(IP.k.左) &&  IP.按键检测_按住(IP.k.右));
 if (同输入) Last = Player.transform.localPosition;
        if (IP.方向正零负== Player.transform.localScale.x
            //因为左右一起按也是返回为0
             )
        {
            if (Last!=Vector2.zero)
            {////可能大部分位置为题在这里
                Player.transform.localPosition = new Vector2(Last.x, Last.y);
         
 
            }
            Player.Velocity = Vector2 .zero;
             
        }
        else
        {
            //Debug.LogError(Player.transform.position); 
  
                Player.Velocity = new Vector2(Player.Velocity.x, Mathf.Clamp(Player.Velocity.y, -1f, float.MaxValue));
 

            //Debug.LogError(Player.transform.position);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //return;
        //Debug.LogError(Player.transform.position);
 
            if (!按下了相反)
        {

            if (Player.transform.localScale.x != IP.方向正负 
                && IP.按键检测_按下(IP.k.跳跃)
             )
            {
                //反方向键和跳跃一起按下                 
                Debug.LogError("          && IP.按键检测_按下(IP.k.跳跃)");
                登墙跳(E_方式.反向);
  
            }
            else if (Player.transform.localScale.x != IP.方向正负)
            {//第一次进来
             //相反
                按下了相反 = true;
                第一次进来的时间_ = Time.time;
                Player_input.假装相反方向键();
                Player.StartCoroutine(土狼(缓冲时间Max_长, 1));
                return;
            }
            else if (IP.按键检测_按下(IP.k.跳跃)  )
            {
                //只按了跳跃
                Debug.LogError("       键检测_按下(IP.k.跳跃) && Enter   ");
                if(IP.方向正零负!=0) 
                    登墙跳(E_方式.同向); 
             else   登墙跳(E_方式.无操作);

            }
        }
        else
        {//第二次进来
            //var a = Time.time- 第一次进来的时间_ < 缓冲时间Max;
            //if (a)
            //{//时间之内 
            //    if (IP.按键检测_按下(IP.k.跳跃))
            //    {
            //        Debug.LogError("       = Time.time- 第一次进来的时间_ < 缓冲时间Max;   ");

            //        登墙跳(E_方式.反向);

            //    }
            //}
            //else
            {//时间之外 
                Player.StartCoroutine(土狼(缓冲时间Max_长,1));
                Player.方向更新();
                f.To_State(E_State.sky);
            }
        }
    }

   enum E_方式
    {
    无操作,反向,同向
    }

   void 登墙跳(E_方式 E)
    {
        Debug.LogError("    void 登墙跳(E_方式 E)    void 登墙跳(E_方式 E)");
        switch (E)
        { 
         case E_方式.反向:
                金庸(Initialize_Mono.I.删掉最长);
                Player.方向更新();
                ///又分成 方向已经改变和方向没有改变俩情况   关键字段进入为正面
                Player.跳跃触发(new Vector2(-Player.wall_进入为正面 * 8f, Player.玩家数值.跳跃瞬间速度));
                f.To_State(E_State.sky);
                break;
        case E_方式.无操作:
                金庸(Initialize_Mono.I.删掉 );
                Player.StartCoroutine(土狼(缓冲时间Max_短, -1));
                Player.跳跃触发(new Vector2(-Player.transform.localScale.x * 8f, Player.玩家数值.跳跃瞬间速度) *3/4);
                Player.方向更新();
                f.To_State(E_State.sky);
                break;
            case E_方式.同向:
                金庸(Initialize_Mono.I.删掉最短);
                Player.StartCoroutine(土狼(缓冲时间Max_短, -1));
                Player.跳跃触发(new Vector2(-Player.transform.localScale.x * 8f, Player.玩家数值.跳跃瞬间速度)/4);
                Player.方向更新();
                f.To_State(E_State.sky);
                break; 
        }
        Debug.LogError(E);
      
    } 

    IEnumerator 土狼(float t,int i)
    {
        Debug.LogError("进入"+i +"    "+t);
        ///先反方向然后空格  1   B
        ///先空格然后方向键  -1  A
        ///A 短B长

      Player.  is土狼时间_Wall = i;
        yield return new  WaitForSeconds(t);
        Player.is土狼时间_Wall = 0;
    }
    public  void 金庸(float time)
    {
        Player3.I.记录a(time); 
    }

    public override void 接触地面()
    {
        f.To_State(E_State.sky);
        f.Getstate(E_State.sky).接触地面();
        //Initialize_Mono.I .Waite(  ()=>I.接触地面事件.Invoke());
    }

    public override void 方向改变(bool obj) {   }
     
}
