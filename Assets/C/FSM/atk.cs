using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using 流程控制;
public static class T_N
{
    public static string 特效消弹 { get; } = "特效消弹";

    public static string 特效闪光爆炸 { get; } = "特效闪光爆炸";

    public static string 特效圆跳 { get; } = "特效圆跳";
    public static string 特效大伤害 { get; } = "特效大伤害";
    public static string 特效大爆炸 { get; } = "特效大爆炸";
    public static string 特效小Get { get; } = "特效小Get";
    public static string 特效变速 { get; } = "特效变速";
    public static string 特效砖块爆炸 { get; } = "特效砖块爆炸";
    public static string 特效蓄力触发 { get; } = "特效蓄力触发";
    public static string 特效蓄力结束 { get; } = "特效蓄力结束";
    public static string 特效蓄力完成 { get; } = "特效蓄力完成";
    public static string 特效蓄力 { get; } = "特效蓄力";
    public static string 特效受击 { get; } = "特效受击";
    public static string 特效防御 { get; } = "特效防御";
    public static string downatk_0_ { get; } = "downatk_0_";
    public static string skyatk_0_ { get; } = "skyatk_0_";
    public static string atk_1_ { get; } = "atk_1_";
    public static string atk_2_ { get; } = "atk_2_";
    public static string atk_0_ { get; } = "atk_0_";
    public static string dunatk_0_ { get; } = "dunatk_0_";
    public static string dunatk_1_ { get;  } = "dunatk_1_";
    public static string upatk_0_ { get; } = "upatk_0_";
    public static string 特效跳跃 { get; } = "特效jump";
    public static string 特效落地 { get; } = "特效落地";
    public static string 特效闪闪 { get; } = "特效闪闪";
} 
public static class A_N
{
    public static string idle_dundash { get; } = "idle_dundash";

    public static string run_dundash { get; } = "run_dundash";

    public static string dun_dundash { get; } = "dun_dundash";

    public static string dead { get; } = "dead";
    public static string hit { get; } = "hit";
    public static string bighit { get; } = "bighit";
    public static string hitwaite { get; } = "hitwaite";
    public static string pa_to_ { get; } = "pa_to_";
    public static string pa_0_ { get; } = "pa_0_";

    public static string counter_Atk { get; } = "counter_Atk";
    public static string counter_0_ { get; } = "counter_0_";

    public static string idle_gedang_to0 { get; } = "idle_gedang_to0";
    
            public static string gedang_idle_to0 { get; } = "gedang_idle_to0";
    public static string gedang_0_ { get; } = "gedang_0_";  
    public static string gedang_A_to0 { get; } = "gedang_A_to0";
    public static string downatk_2_ { get; } = "downatk_2_";
    public static string downatk_0_ { get;   } = "downatk_0_";
    public static string downatk_1_ { get; } = "downatk_1_";
    public static string skydash_0_ { get; } = "skydash_0_"; 
    public static string jump_max { get;   } = "jump_max";
    public static string jump_down { get; } = "jump_down";
    public static string wall_0_ { get; } = "wall_0_";
    public static string wall_0_1 { get; } = "wall_0_1";
    public static string ladder_0_ { get; } = "ladder_0_";
    public static string run_chang { get; } = "run_chang";
    public static string run_0_ { get; } = "run_0_";
    public static string run_idle_to0 { get; } = "run_idle_to0";
    public static string idle_run_to0 { get; } = "idle_run_to0";
    public static string idle_0_ { get; } = "idle_0_";
    public static string run_jump_to0 { get; } = "run_jump_to0";
    public static string idle_atk_to0 { get; } = "idle_atk_to0";
    public static string idle_jump_to0 { get; } = "idle_jump_to0";
    public static string atk_0_ { get; } = "atk_0_";
    public static string atk_1_ { get; } = "atk_1_";
    public static string atk_2_ { get; } = "atk_2_";
    public static string jump_ { get; } = "jump_";
    public static string dundash_0_ { get; } = "dundash_0_";
    public static string dundash_1_ { get; } = "dundash_1_";
    public static string dundash_负1_ { get; } = "dundash_-1_";
    public static string dun_0_ { get; } = "dun_0_";

    public static string skyatk_0_ { get; } = "skyatk_0_"; 
    public static string skyatk_0back_ { get; } = "skyatk_0back_";
    public static string skyatk_1_ { get; } = "skyatk_1_";

    public static string cricleatk_0_ { get; } = "cricleatk_0_";

    public static string dunatk_1_ { get; } = "dunatk_1_";
    public static string dunatk_0_ { get;} = "dunatk_0_";
    public static string upatk_0_ { get; } = "upatk_0_";
    public static string air { get; } = "air"; 
}
public abstract  class  atkBase: State_Base
{
    protected bool 蓄力攻击
    {
        get => f.变速攻击;
        set =>f. 变速攻击 = value;
    }

    protected float 碰撞框触发时间 { get; set; } = 0.1f;
    public override void ExitState(E_State e)
    {
        base.ExitState( e);
         判定框.开启判定框判定框(false);

        Player.方向更新();
        蓄力攻击 = false;
    }
    public override void EnterState()
    {
 
        if (Time.time - f.蓄力成功之后 < 0.1f)
        {
            蓄力攻击 = true;
        }
        else
        {
            //Debug.LogError("Time.time       " + Time.time + "f.蓄力成功之后          " + f.蓄力成功之后);

        }
    }
 
    protected  struct CollerValue
    {
        public Vector2 Offcet;
        public Vector2 Size;


        public CollerValue(Vector2 aOffcet, Vector2 aSize) : this()
        {
            Offcet = aOffcet;
            Size = aSize;
        }
    }
 protected   enum 阶段
    {
        ready,
        Action,
        end,
    }
    protected 阶段 动画阶段;

    protected bool 马上下一个攻击 { get; set; } 
   /// <summary>
   /// 0下是开始
   /// </summary>
    protected private int 第N下 { get; set; }
 
    protected CollerValue 当前碰撞Value;
    protected 判定框Base 判定框;
    protected Animator 攻击特效;
    protected string 当前攻击特效名字 { get; set; }

    protected CollerValue 上升攻击 { get; set; } = new(new Vector2(1.55f, -1.39f), new Vector2(3.16f, 5.52f));
    protected CollerValue 圆形攻击 { get; set; } = new(new Vector2(0.6f,-0.81f), new Vector2(6.4f,6.12f));
    protected CollerValue   空中 { get; set; } = new(new Vector2(3.42f, -0.6f), new Vector2(3.8f, 3.3f));
    protected CollerValue 普攻近 { get; set; } = new(new Vector2(2.5f,-0.7f), new Vector2(3.4f,3.1f));
    protected CollerValue 普攻远 { get; set; } = new(new Vector2(3.98f, -0.6f), new Vector2(4, 3.3f));
    protected CollerValue d蹲 { get; set; } = new(new Vector2(3.98f, -1.3f), new Vector2(5, 1.62f));
    protected CollerValue Down { get; set; } = new(new Vector2(0.44f, -1.52f), new Vector2(1.9f, 4.5f));
    public override void 方向改变(bool obj)  {  }
    protected virtual void 开启碰撞框播放特效()
    {
        if (当前攻击特效名字 == null || 当前攻击特效名字 == "")
        {
            Debug.LogError("特效名为为空");
            return;
        }
        攻击特效.Play(当前攻击特效名字);
        判定框.SetBox(当前碰撞Value.Offcet, 当前碰撞Value.Size);
        判定框.开启一会儿(碰撞框触发时间);
        当前攻击特效名字 = null;
    }
    public override void AweakStatebase()
    {
        base.AweakStatebase();
        判定框 = Player.判定框;
        攻击特效 = 判定框.GetAnimator();
    }
    public override void UpdateState()
    {
 
        if (Player.Atk)
        {
            动画阶段 = 阶段.Action;
            Player.Atk = false;

            开启碰撞框播放特效();
        }
        if (Player.End)
        {
            Player.End = false;
            if (!马上下一个攻击)
            {//没有按第二次
                单个攻击结束();
            }
            else
            {//按下了第二次
                //Debug.LogError("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                第N下++;
                马上下一个攻击 = false;
                播放相应动画();
            }
        }
 
    }
    protected bool  旋转箭失触发(Collider2D  a )
    { 
        if (a == null) return false  ; 
        if ( !a.CompareTag(Initialize.One_way))return false ; 
        var F = a.gameObject.GetComponent<Fly_Ground>();
        if (F==null) return false; 
        ///平台和箭矢的区分  
        if (!F.可以旋转) return true; 
        if (F != null)
        {

            //Debug.LogError("             AAAAAAAAAAAAA"+(x.Sign() == Player.LocalScaleX_Int)  );

            F.Currrtten =F.L2; 
            //Debug.LogError(state+"      "+f.I_State_L.state);
            switch (state)
            { 
                case E_State.upatk:
                    F.旋转触发(1);
                    return true;
                case E_State.cricleatk:
                    var x = a.transform.position.x - Player.transform.position.x;
                    bool 正面 = x.Sign() == Player.LocalScaleX_Int;
                    //F.旋转触发(-1);
                    if (正面)
                    {
                        F.旋转触发(-1);

                    }
                    //else
                    //{
                    //    F.旋转触发(1);
                    //}

                    return true;
                default:
                    F.旋转触发(0);
                    return true;
            }; 
        }
        else return false;
    } 
    protected bool 消弹()
    {
        bool a = false;
        if (判定框.所有碰撞体 != null && 判定框.所有碰撞体.Count > 0)
        { 
            for (int i = 0; i < 判定框.所有碰撞体.Count; i++)
            {
                var CC = 判定框.所有碰撞体[i]; 

                if (CC!=null)
                {
                    var A = CC.GetComponent<Bullet>();
                    if (A != null)
                    {  
                    if (A.被消弹())
                        {
                            a = true;
                            半灵.I.发射();
                        }
                 
                    }
                } 
            }
        }
        return a;
    }
    protected   virtual  bool  击中效果()
    {
        if (判定框.所有碰撞体 !=null&& 判定框.所有碰撞体.Count>0)
        {
            for (int i = 0; i < 判定框.所有碰撞体.Count; i++)
            {
                var a = 判定框.所有碰撞体[i];

                  if (旋转箭失触发(a)) 判定框.所有碰撞体[i] = null;   
            }
        }
        
        for (int i = 0; i < 判定框.敌人.Count; i++)
        {
            if (判定框.敌人[i] == null) Debug.LogError("A null" );
            if (判定框.敌人[i] == null)      continue;
           
            var e = 判定框.敌人[i];

            bool b_ = Player.LocalScaleX_Set != 1 ? true : false;

            Player.伤害(e ); 

            bool b = e.当前hp <= 0;  //    是扣血之后判断
            if (b)
            {
                //嗝屁

                Initialize_Mono.I.时缓(0.02f, 0.1f);
                Player.受伤.镜头晃动(1);

                if (摄像机.I.当前场景默认FOV!=0)
                {
                    摄像机.I.FOV_缩放并且还原(摄像机.I.当前场景默认FOV * 0.95f, 0.05f, 0.8f);
                }
          
            }
            else
            {
                //没嗝屁
                Initialize_Mono.I.时缓(0.01f, 0.08f);
            }
            判定框.敌人[i] = null;

            return true;
        }
        return   false ;
    }
    protected virtual void 播放相应动画()
    {

    }
    protected virtual void 单个攻击结束()
    {

    }
    protected void 提一下(float Y)
    { 
        Player.Velocity = new Vector2(-Player.LocalScaleX_Int *0.5f, Y);
    }
}
public class atk : atkBase
{ 
    public override void 离开地面()
    {
        base.离开地面();
        f.To_State(E_State.sky);
    }
    public static int FrametColod => 2;
    public static bool 通告 { get; set; } = false;
    public override void EnterState()
    {
        base.EnterState();
        A.Re();
        第N下 = 0;
        当前攻击特效名字 = null;
        马上下一个攻击 = false;
        Player.Velocity = Vector2.zero;



        播放相应动画();
        if (蓄力攻击 )
        {
            当前碰撞Value = 普攻近;
        }
    }

    public override void ExitState(E_State e)
    {
        base.ExitState(e);
          第N下 = 0;
        当前攻击特效名字 = null;
        马上下一个攻击 = false;
        //Player.Velocity = Vector2.zero;
        判定框.关闭再打开();
        Player.方向更新();
    }
    protected override void 播放相应动画()
    {
        if ( 蓄力攻击 )
        {
            yalaAudil.I.EffectsPlay("WaiteAtk", 0);
           
            A.Playanim(A_N.counter_Atk);
            return;
        }

        if (第N下<3)    yalaAudil.I.EffectsPlay("Atk", 0);
   
        动画阶段 = 阶段.ready;
        当前碰撞Value = 普攻近; 
        if (第N下 == 0)
        { 
            当前攻击特效名字 = T_N.atk_0_;
            A.Playanim(A_N.atk_0_);

        }
        else if (第N下 == 1)
        {
 
            当前攻击特效名字 = T_N.atk_1_;
            A.Playanim(A_N.atk_1_);
        }
        else if (第N下 == 2)
        {
     
            当前碰撞Value = 普攻远;
            当前攻击特效名字 = T_N.atk_2_;
            A.Playanim(A_N.atk_2_);
        }
        else
        {
            第N下 = 0;
            单个攻击结束();
        } 
    }
    protected override bool 击中效果()
    { 
        return base.击中效果() || 消弹();
    }
    protected override void 单个攻击结束()
    {
        if (IP.方向正零负 == 0)
        {
            f.To_State(E_State.idle);
        }
        else
        {
            f.To_State(E_State.run);
        }
    } 
    public override void 按下(KeyCode obj)
    {
        if (obj==IP.k.攻击)
        {
            if (动画阶段 == 阶段.Action)
            {
                if (IP.按键检测_按住 (IP.k.上))
                {
                    f.To_State(E_State.upatk);
                }
                else
                {
                马上下一个攻击 = true;
                }
            }
        }
    } 
    public override void UpdateState()
    {
        if (蓄力攻击  )
        {
            if (  Player.Atk)
            {
                Player.Atk = false;
                A.Playanim(A_N.counter_Atk);

                判定框.SetBox(当前碰撞Value .Offcet,当前碰撞Value .Size );
                判定框.开启一会儿(碰撞框触发时间 );

            }
            else if (A.当前进度 >0.99f&&A.当前名字 == A_N.counter_Atk)
            { 
                f.To_State(E_State.idle);
            }
            return;
        }
        base.UpdateState();
        if (IP.按键检测_按下(IP.k.跳跃))
        {
            Player.闪光();
            Player.跳跃触发();
            f.To_State(E_State.sky);
        }
        if (IP.按键检测_按下(IP.k.冲刺))
        {
            Player.闪光();
            f.To_State(E_State.dash);
        }

        击中效果();
    }


    protected override void 开启碰撞框播放特效()
    {
        base.开启碰撞框播放特效();
        通告 = 第N下 == 2; 
    }
} 
public class skyatk : atkBase
{
    protected override bool 击中效果()
    {
        return base.击中效果() || 消弹();
    }
    public override void EnterState()
    {
        base.EnterState();
        A.Re();

        第N下 = 0;
        当前攻击特效名字 = null;
        马上下一个攻击 = false;



        Player.空中攻击过了 = true;
        Player.End = false;

        if ( 蓄力攻击) 第N下 = 1; 
        播放相应动画();
    }
    public override void ExitState(E_State e)
    {
        base.ExitState(e);
        当前攻击特效名字 = null;  
        判定框.关闭再打开();
        Player.方向更新();
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (击中效果()) 提一下(7); 
    }
    public override void 接触地面()
    {
        if (IP.方向正零负 ==0)
        {
            f.To_State(E_State.idle);
        }
        else
        { 
            f.To_State(E_State.run ); 
        }
    }
    protected override void 开启碰撞框播放特效()
    {
        base.开启碰撞框播放特效();
        提一下(5);
    }
    protected override void 播放相应动画()
    {
        动画阶段 = 阶段.ready;
        当前碰撞Value = 空中;

        if (第N下<2)     yalaAudil.I.EffectsPlay("Atk", 0);

        if (第N下 ==0)
        {
            当前攻击特效名字 = T_N.skyatk_0_;
            A.Playanim(A_N.skyatk_0_);
        }
        else  if(第N下 == 1)
        {
     Debug.LogWarning("ASDASDASDASDASDASDASDASD");
            当前攻击特效名字 = T_N.skyatk_0_;
            A.Playanim(A_N.skyatk_1_);
        }

        else
        {
            第N下 = 0;
            单个攻击结束();
        } 
    }

    protected override void 单个攻击结束()
    {
        if (A.当前anim.name ==A_N.skyatk_0_ )
        {
            A.Playanim(A_N.skyatk_0back_);
        }
        f.To_State(E_State.sky);
    }
    public override void 按下(KeyCode obj)
    {
        //if (obj == IP.k.攻击)
            if (obj == IP.k.攻击)
        { 
            if (动画阶段 ==阶段.Action )
            {
                Debug.LogError("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBB");
                马上下一个攻击 = true;
            } 
        }
    }
    public override bool 可以切换嘛()
    {
        if (Player.空中攻击过了) return false;
 
        return true;
    }
}
public class cricleatk : atkBase
{
    public override bool 能力激活的 { get => Player.N_.圆劈; set => Player.N_.圆劈 = value; }
    public override void 接触地面()
    { 
        if (IP.方向正零负 == 0)
        {
            A.Playanim(A_N .idle_jump_to0);
            f.To_State(E_State.idle);
        }
        else
        {
            A.Playanim(A_N.run_idle_to0);
            f.To_State(E_State.run);
        }
    } 
    protected override void 单个攻击结束()
    { 
        f.To_State(E_State.sky);
    } 
    public override bool 可以切换嘛()
    {
        if (Player.玩家数值 .Boss杀手)
        {
            return true;
        }
        if (Player.圆形攻击过了)
        {
            return false;
        }
        return true;
    }
    public override void ExitState(E_State e)
    {
        base.ExitState(e);
        当前攻击特效名字 = null;
        判定框.关闭再打开();
        Player.方向更新();
    }
    public override void EnterState()
    {
        base.EnterState();
        A.Re();
        yalaAudil.I.EffectsPlay("Jump", 0);
        第N下 = 0;

        当前攻击特效名字 = "cricleatk_0_";
        当前碰撞Value = 圆形攻击;

        马上下一个攻击 = false;

        Player.圆形攻击过了 = true;
        Player.End = false;
      
        播放相应动画();
    }
    protected   override  void 开启碰撞框播放特效()
    {
        if (当前攻击特效名字 == null || 当前攻击特效名字 == "")
        {
            Debug.LogError("特效名为为空");
            return;
        }
        攻击特效.Play(当前攻击特效名字);
 
        判定框.开启一会儿(碰撞框触发时间, 判定框.Cc);
        当前攻击特效名字 = null;
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (击中效果()) {
            Player.圆形攻击过了 = false;
            提一下(Player.玩家数值.圆斩上升力);
        } 
 
    }
    Int不重复 BInt=new Int不重复();
    protected override bool 击中效果()
    {
        if (判定框.所有碰撞体 !=null&& 判定框.所有碰撞体.Count>=1)
        {
            判定框Base.打到的类型 asd;
            bool RRR = false;
            ///先判定是不是人
            if (判定框.敌人 != null && 判定框.敌人.Count >= 1) asd = 判定框Base.打到的类型.敌人;
            else
            {
                ///  碰撞框框要是碰到了 能踩但是不是OneWay 那么OneWay不会被触发 主角起飞
                ///  
                var originalAll = 判定框.所有碰撞体;
                
                // If there are any One_way colliders, prefer to process only those; otherwise process all colliders.
                List<Collider2D> targetList = null;
                if (originalAll != null && originalAll.Count > 0)
                {
                    var oneWay = originalAll.FindAll(c => c != null && c.gameObject.CompareTag(Initialize.One_way));
                    if (oneWay != null && oneWay.Count > 0)
                    {
                        targetList = oneWay;
                    }
                    else
                    {
                        targetList = new List<Collider2D>(originalAll);
                    }
                    if (Initialize_Mono.I.打包额外打印)
                    {
                        Debug.LogError("到这qqqqqqqqqqqqqqqqqqqqqqqqqwwwwa");
                    }
                }

                if (targetList != null)
                {
                    for (int ti = 0; ti < targetList.Count; ti++)
                    {
                        var col = targetList[ti];
                        if (col == null) continue;

                        var e = col;

                        bool 不空而且旋转 = false; 
                        var FF = e.GetComponent<Fly_Ground>();
                       
                        if (FF!=null)    if ( FF.旋转1)  不空而且旋转 = true;
                         
                            if (旋转箭失触发(e))
                        { 
                            // null the corresponding element in the original list so later logic sees it removed
                            if (originalAll != null)
                            {
                                int origIndex = originalAll.IndexOf(e);
                                if (origIndex >= 0) originalAll[origIndex] = null;
                            }
                            RRR = false;
                        }
                             
                        if (Initialize_Mono.I.能踩(e)  )
                        {

                            if (Initialize_Mono.I.打包额外打印)
                            {
                                Debug.LogError("到这里");
                             }
                            if (不空而且旋转) {
                                break;
                            }

                            var Rs = Player.圆斩判定.发射();
                            RRR = Rs != -999;
                            if (RRR)
                            {
                                //if (!BInt.Add(Time.frameCount, true)) break;
                                if (Initialize_Mono.I.打包额外打印)
                                {
                                    Debug.LogError("到这里aaa");
                                }

                                Player.脚底中间.DraClirl(1f,Color.white);
                                new Vector2 (Player.transform.position.x, Rs).DraClirl(1f, Color.white);

                                if (Initialize_Mono.I.打包额外打印)
                                {
                                    Debug.LogError(Player.脚底中间.y + "Rs:          " + Rs + "positionY :        " + Player.transform.position.y); 
                                }

                                var delta = Rs - Player.脚底中间.y;
                                if (delta > 0)
                                {
                                    var a = sky.双点碰撞(Player.Bounds.size.y + delta + Player.Velocity.y / 15f);
                                    var B = sky.Find(a);

                                    var aac = sky.双点碰撞(Player.Bounds.size.y + delta  );

                                    if (aac.Count > 0)
                                    {
                                        var aa = Initialize.Get碰撞Position(Player.Bounds, aac[0]);
                                        if (aa == Vector2.zero)
                                        {
                                            Player.transform.position += new Vector3(0f, delta);
                                        }
                                        else
                                        {
                                            Vector3 ca = Player.Bounds.center - (Vector3)aa;
                                            Player.transform.position -= ca;
                                        }
                                    }
                                    else
                                    {
                                        Player.transform.position += new Vector3(0f, delta);
                                    }



                                    //Player.transform.position += new Vector3(0f, delta);


                                    //if (a==null)
                                    //{
                                    //    Player.transform.position += new Vector3(0f, delta);
                                    //}
                                    //else
                                    //{ 
                                    //  if (!sky.Find(a))
                                    //    {
                                    //        ///没有雪块 
                                    //        /// 
                                    //var aa = Initialize.Get碰撞Position(Player.Bounds, a[0]);
                                    //        if (aa==Vector2.zero)
                                    //        {
                                    //            Player.transform.position += new Vector3(0f, delta);
                                    //        }
                                    //    }
                                    //}



                                }
                                if (originalAll != null)
                                {
                                    int origIndex = originalAll.IndexOf(e);
                                    if (origIndex >= 0) originalAll[origIndex] = null;
                                }
                                // stop after successful one-way handling
                                break;

                            }
                        } 
                    }
                }

                Debug.LogError(RRR);
                if (RRR) 判定框.开启判定框判定框(false, 判定框.Cc);
                //升起来
                return RRR;
            }
                if (判定框.敌人 != null && 判定框.敌人.Count >= 1)
            {
                if (Initialize_Mono.I.打包额外打印)
                {
                    Debug.LogError("到这里wwwwwwwwwwwwwwwwwwa");
                }

                #region    正常
                for (int i = 0; i < 判定框.敌人.Count; i++)
                {
                    if (判定框.敌人[i] == null) continue;
                    var e = 判定框.敌人[i];

                    bool b_ = Player.LocalScaleX_Set != 1 ? true : false;

                    var a = e.gameObject.GetComponent<Phy>();
                    Player.伤害(e);
                    a.Goto_thisWay(new Vector2(Player.Bounds.center.x, Player.Bounds.center.y - 3f));

                    bool b = e.当前hp <= 0;  //    是扣血之后判断
                    if (b)
                    {
                        //嗝屁 
                        Initialize_Mono.I.时缓(0.02f, 0.1f);
                        Player.受伤.镜头晃动(1);

                        摄像机.I.FOV_缩放并且还原(摄像机.I.当前场景默认FOV * 0.95f, 0.05f, 0.8f);
                    }
                    else
                    {
                        //没嗝屁
                        Initialize_Mono.I.时缓(0.01f, 0.08f);
                    }
                    判定框.敌人[i] = null;

                    return true;
                }
                return false;
                #endregion
            } 
        }
        return false ; 
    }
    protected override void 播放相应动画()
    {
        提一下(5);
        A.Playanim(A_N.cricleatk_0_ );
    }
 
}
public class dunatk : atkBase
{
    /// <summary>
    /// 目前， 从该状态退出 到run等状态不会退出碰撞体一半   SO 该能力暂时不用
    /// </summary>
    public override bool 能力激活的 { get =>false ; }
    public override void EnterState()
    { 
        第N下 = 0;
        当前攻击特效名字 = null;
        马上下一个攻击 = false;
        Player.End = false;
        播放相应动画();
    }
    public override void ExitState(E_State e)
    {
        第N下 = 0;
        当前攻击特效名字 = null;
        马上下一个攻击 = false; 
        判定框.关闭再打开();
        Player.方向更新();
    }
    protected override void 播放相应动画()
    {
        动画阶段 = 阶段.ready;
        当前碰撞Value = d蹲;
 
 
        if (第N下 == 0)
        {
 
               当前攻击特效名字 = T_N.dunatk_0_;
            A.Playanim(A_N.dunatk_0_);

        }
        else if (第N下 == 1)
        {
 
            当前攻击特效名字 = T_N.dunatk_1_;
            A.Playanim(A_N.dunatk_1_);
        } 
        else
        {
            第N下 = 0;
            单个攻击结束();
        }
    }
    protected override void 单个攻击结束()
    {
        if (IP.按键检测_按住(IP.k.下))
        {
            f.To_State(E_State.dun);
        }
        else
        {
            f.To_State(E_State.idle);
        } 
    }
    public override void 按下(KeyCode obj)
    {
        if (obj == IP.k.攻击)
        {
            if (动画阶段 == 阶段.Action)
            {
                马上下一个攻击 = true;
            }
        }
    }
} 
public class downatk : atkBase
{
    public override bool 能力激活的 { get => Player.N_.下落攻击 ; set => Player.N_.下落攻击 = value; }
    Vector2 接近 { get; set; }
    public override void EnterState()
    {
        Initialize.Set_碰撞(Initialize .L_Player ,Initialize .L_Only_Ground,true);
        动画阶段 = 阶段.ready;
        Player.Velocity = new Vector2(0,10f);
        A.Playanim(A_N.downatk_0_);
        接近= Player.脚底发射();
        判定框.SetBox(Down.Offcet,Down .Size);
        判定框.在主角前面();

        当前碰撞Value = 圆形攻击;
    }

    public override void ExitState(E_State e)
    {
        Initialize.Set_碰撞(Initialize.L_Player, Initialize.L_Only_Ground, false);
        判定框.关闭再打开();
           残影.I.开启残影(false); 
    }
    public override void UpdateState()
    { 
        switch (动画阶段)
        {
            case 阶段.ready:
                if (Player.Atk)
                {
                    动画阶段 = 阶段.Action;
                    Player.Atk = false;

                    A.Playanim(A_N.downatk_1_);
                    残影.I.开启残影(true);
                    Player.Velocity = new Vector2(0, -100f);
                }
                break;
            case 阶段.Action:
                if (Player.脚底中间.y - 接近.y < 2f||Player.Ground )
                { 
                    动画阶段 = 阶段.end; 
                    残影.I.开启残影(false);

                    判定框.开启一会儿(0.1f); 
                    攻击特效.Play(T_N.downatk_0_);

                    Player.Velocity = new Vector2(0, 20);
                    A.Playanim(A_N.downatk_2_); 
                }
                break;
            case 阶段.end: 
                    if (Player.End)
                    {
                        Player.End = false;
                    f.To_State(E_State.sky);
                    }
 
                break; 
        }

        击中效果();
    }

    public override void 接触地面()
    { 
        if (动画阶段 == 阶段.end)
        {
            if (IP.方向正零负 ==0)
            {
                f.To_State(E_State.idle);
            }
            else
            {
                f.To_State(E_State.run);
            }
        }
    } 
} 
public class upatk : atkBase
{
    bool 立即结束;
    public override void StateStart()
    {
        碰撞框触发时间 = 0.1f;
        当前碰撞Value.Offcet = new Vector2(2.4f, -0.4f);
        当前碰撞Value.Size = new Vector2(3.5f, 3.7f);
    }
    public override bool 可以切换嘛()
    {
        if (Player.上升攻击过了)
        {
            return false;
        }
        return true;
    }
    public override void EnterState()
    {
        立即结束 = false;
        Player.上升攻击过了 = true; 
  

        Key a = Player_input.I.Get_key(Player_input.I.k.攻击);

        //Player.曲线(5, 15, 0.3f, Player.AC);
        if (a.Keeptime > 0.1f)
        {
            if (Player.玩家数值.Boss杀手)
        {
            Player.曲线(5, 40, 0.3f, Player.AC);
        }
        else
        {
            Player.曲线(5, 15, 0.3f, Player.AC);
        }
        }
        else
        {
            立即结束 = true;
        }
        A.Playanim(A_N.upatk_0_);
        if (立即结束)
        {
            A.AnimSpeed = 1.5f;
        }
        动画阶段 = 阶段.ready;

        Velocity_Y = -1;
        时间 = 0;
        当前攻击特效名字 = T_N.upatk_0_; 
    }

    public override void ExitState(E_State e)
    {
        if (立即结束) Player.上升攻击过了 =false;
        时间 = 0;
           Velocity_Y = -1; 
    }


    float 总时间 { get; } = 0.5f;
    public override bool 能力激活的 { get => Player.N_.上升攻击; set => Player.N_.上升攻击 = value; }

    float 时间;
   /// <summary>
   /// 进入时的那啥
   /// </summary>
    float Velocity_Y = -1;
    public override void UpdateState()
    { 
            switch (动画阶段)
        {
            case 阶段.ready:
                if (Player.Atk)
                {//开启判定框
                    Player.Atk = false;
                    动画阶段 = 阶段.Action;
                     
                    开启碰撞框播放特效();
                }
                break;
            case 阶段.Action:
                if (Player.End||(立即结束&&A .当前进度 >0.9f))
                {//滞空   
                    //被曲线   结束调用
                    Player.End = false;
                    动画阶段 = 阶段.end;

                    if (!立即结束) 
                    Player.Velocity = new Vector2(-Player.LocalScaleX_Int*1.5f, 8f);

    
                    单个攻击结束();
                }
                break; 
        }
        if (击中效果()) 提一下(4.2f);
       
    }
   protected override bool          击中效果()
    {
        if (判定框.所有碰撞体 != null && 判定框.所有碰撞体.Count > 0)
        {
            for (int i = 0; i < 判定框.所有碰撞体.Count; i++)
            {
                var a = 判定框.所有碰撞体[i]; 
                旋转箭失触发(a);
                判定框.所有碰撞体[i] = null;

            }
        }
   
      
        for (int i = 0; i < 判定框.敌人.Count; i++)
        {
            if (判定框.敌人[i] == null) continue;
            var e = 判定框.敌人[i];

            bool b_ = Player.LocalScaleX_Set != 1 ? true : false;

            bool TTT = Initialize.方向_A是否在B的左边或者下面(Player.站立box.bounds .center,e.Bounds .center,false );
            float 左边 = 1;
            if (!TTT) 左边 = -1;

            Vector2 force = new Vector2(左边*3f, 7f);

            e.扣血外部力 = force;
            Player.伤害(e);

            e.GetComponent<Phy>();
            bool b = e.当前hp <= 0;  //    是扣血之后判断
            if (b)
            {
                //嗝屁
                Initialize_Mono.I.时缓(0.02f, 0.1f);
                Player.受伤.镜头晃动(1);

                摄像机.I.FOV_缩放并且还原(摄像机.I.当前场景默认FOV * 0.95f, 0.05f, 0.8f);
            }
            else
            {
                //没嗝屁
                Initialize_Mono.I.时缓(0.01f, 0.08f);
            }
            判定框.敌人[i] = null;
            return true;
        }
        return false;
    }
    protected override void 单个攻击结束()
    {
       
        if (Player.Ground)
        {
            Debug.LogError("AZZZZZZZZZZZZZ");
            f.To_State(E_State.idle);
        }
        else
        {
            Debug.LogError("BBBBBBBBBBA");
            f.To_State(E_State.sky);
        }

    } 
}
