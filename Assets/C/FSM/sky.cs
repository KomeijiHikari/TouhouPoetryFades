using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sky : State_Base  
{


    //float 原始碰撞;
    public override void AweakStatebase()
    {
        base.AweakStatebase();
        //原始碰撞 = Player.po.size.x;
    }
    public override void EnterState()
    {
   前空  =Player.前空_ ;
        if (!Player3.I.is原Parent)
        {
            Player3.I.ChangeFather();
        }
        //Player.po.size  = new Vector2(Player.po.size.x*0.4f, Player.po.size.y);
        不是第一次悬浮 = false;
           第一次跳跃 = false;
        //Player.前档板.enabled = true;
 
 
        switch (f.I_State_L .state)
        { 
            case E_State.upatk: 
                Player.玩家数值.跳跃剩余跃次数--; 
                break;
            case E_State.wall:
            case E_State.downatk:
                Player.方向更新();
                if (Player.Velocity.y <= 0)
                {
                    Player.玩家数值.跳跃剩余跃次数--;
                    var a = A.GetAnim(JUMAP_name.中间);
                    a.speed = 1.5f;
                    A.Playanim(JUMAP_name.中间);
                }
                else
                {
                    A.Playanim(JUMAP_name.上去);
                }
                break;
            case E_State.hit :
            case E_State.skyatk:
                if (A.当前anim .name==A_N .skyatk_0back_)
                {
                    A.NextAnim(A .GetAnim (JUMAP_name.中间));
                }
                else
                {
                    A.Playanim(JUMAP_name.中间);
                }
                break;
            case E_State.atk:
            case E_State.dun: 
            case E_State.run:
            case E_State.idle:
               case E_State.skydash:
                Vector2 dian = Player.脚底发射(0.9f);
                if (dian!=Vector2 .zero)
                {
                    特效_pool_2.I.GetPool(dian, T_N .特效跳跃 ).Speed_Lv =Player3 .Public_Const_Speed;
 
                }
                if (Player.Velocity .y<=0)
                {
                    Player.玩家数值.跳跃剩余跃次数--;
                    var a=  A.GetAnim(JUMAP_name.中间);
                    a.speed=1.5f;
                    A.Playanim(JUMAP_name.中间);
                }
                else
                { 
                    A.Playanim(JUMAP_name.上去);
                }
                break;
            case E_State.cricleatk:
            case E_State.pa:
                //Debug.LogError("AAAAAAAAAAAAAAAA");
                A.Playanim(JUMAP_name.中间);
                break;
        }

        //if (animname == null) return; 
        //A.Playanim(animname);
 
    }
    public override void ExitState()
    {
        base.ExitState();
    
        //Player.前档板.enabled = false;
        //Player.po.size = new Vector2(原始碰撞, Player.po.size.y);
    }
    public override void 按下跳跃()
    {
        if (f.I_State_L .state==E_State.run)
        {
            if (Time.time - f.Getstate(E_State.run).ExiteTime<0.1f)
            {
                Debug.LogError("土狼时间");
                A.Playanim(JUMAP_name.上去);
                Player.跳跃触发();
                第一次跳跃 = false;
            }
        }
        f.To_State(E_State.cricleatk);
        return;
        ///多端跳
        //if (Player.玩家数值.Boss杀手)
        //{
  
  
        //}


        //if (Player.玩家数值 .跳跃剩余跃次数>0)
        //{
        //    Player.玩家数值.跳跃剩余跃次数--;
        //    A.Playanim(JUMAP_name.上去);
        //    Player. 跳跃触发();


        //    第一次跳跃 = false;
        //}
        //else
        //{
        //    Player.闪光();
        //}

    }


    bool 不是第一次悬浮;
    float 悬浮速度=-1;
    public override void FixedState()
    { 

        if (IP.方向正零负!=0)
        {
            if (Player.前空_) Player.AddForce(new Vector2(IP.方向正零负 * Player.玩家数值.起步速度, 0));
        }
        if (IP.水平操作_ == 0 && Player.Velocity.y < 0)
        {
            float 百分比 = MathF.Abs(Velocity.x) / Player.玩家数值.常态速度;

            百分比 = Mathf.Clamp(百分比, 0, 1f);
            if (百分比 > 0.25f)
            {
                Player.AddForce(-Player.LocalScaleX_Set * Vector2.right *7.5f * 百分比);
            } 
        }


        if (f.I_State_L.state!=E_State.hit||IP.方向正零负!=0)
        {
            Player.水平限制();
            Player.竖直限制();
        }
   

    

        if (Player.N_.悬浮)
        {
            //Debug.LogError("  if (Player.N.悬浮)  if (Player.N.悬浮)  if (Player.N.悬浮)");
        if (IP.按键检测_按住(IP.跳跃)&& IP.Get_key( IP.跳跃).Keeptime>0.2f)
        {
                if (false )
                    if (Player.Velocity.y < 悬浮速度)
            {
                if (A.当前anim.name!= A_N.air)
                {
                    Player.Velocity = new Vector2(Player.Velocity.x,0);
                A.Playanim(A_N.air);
                    if (!不是第一次悬浮)
                    {
                    不是第一次悬浮 = true;
                        //Player.Velocity = new Vector2(Player.Velocity.x, 10);
                    }
                }
                else
                {
                    Player.AddForce(new Vector2(0,80f)); 
                } 
            }
        }
        }
        if (Player_input .I .方向正零负!=0
            &&Player.顶死
            &&!Player.Ground
            )
        { 
            if (Time.frameCount- f.Getstate(E_State.wall).ExiteFramet>6)
            {
                     f.To_State(E_State.wall);
                return;
            } 
        }
        if (IP.竖直正负零!=0)
        {
            if (Player.ladder)
            {
                if (Time .time -   f.Getstate(E_State .ladder).ExiteTime>0.5f )
                {
                    f.To_State(E_State.ladder);
                }

            }
        }

        if (f.I_State_L .state==E_State.wall
            || f.I_State_L.state == E_State.skydash)
        {
            if (Player.Ground)
            {
                接触地面();
            }
        }
        if (Player.Velocity .y==0&& Player.Ground)
        {
            接触地面();
        } 
    }

    public override void 接触地面()
    {
        Vector2 dian = Player.脚底发射(0.9f);
        if (dian != Vector2.zero)
        {
            特效_pool_2.I.GetPool(dian, T_N.特效落地).Speed_Lv = Player3.Public_Const_Speed;
        }

        //特效_pool_2.I.GetPool(Player.脚底发射(), "特效落地");
        //if (IP.按键检测_按住(IP.冲刺))
        //{
        //    f.To_State(E_State.dash); 
        //}
        //else  
        if (IP.按键检测_按住 (IP.下))
        { 
                f.To_State(E_State.dun); 
        }
        else
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

 
    }

    bool 第一次跳跃 { get; set; }

    void 卡在()
    {
        if (Player.Velocity ==Vector2 .zero )
        {
            var a = Player.Bounds.min-new Vector3 (0,Player.po .edgeRadius );
            var b = new Vector3(Player.Bounds.max.x , Player.Bounds.min.y)-new Vector3 (0,Player.po .edgeRadius );

        var pa=    Physics2D.OverlapCircle(a, 0.1f, Player.碰撞检测层);
            var pb=     Physics2D.OverlapCircle(b, 0.1f, Player.碰撞检测层);

            if (pa!=null||pb!=null)
            { 
                Player.Ground = true;
            }
        }
    }
    bool 前空=true;
    public override void UpdateState()
    {
        if (Player.悬挂.满足)
        {
            if (IP.方向正零负 == Player.LocalScaleX_Set)
            {
                if (Player.Velocity .y<0)
                {
                    if (EnterTime > 0.3f)///刚提扫怀念过去就切换
                    {
                        if (Player.脚底发射(0.5f) == Vector2.zero)///保持一定距离
                        {
                            f.To_State(E_State.pa);
                        }
                    }
                } 
            } 
        }
        if (前空 != Player.前空_&& !Player.前空_) ///撞墙补正
        {
            前空 = Player.前空_;

            //Debug.LogError(transform.position + "         " + Player.Velocity.y);
            //Debug.LogError(Player.Velocity .y);
            //Vector2  pos = transform.position;
            //Vector2 Velo = new Vector2 (Player.Velocity.x, Player.Velocity.y+4);
            Player.Velocity= new Vector2(Player.Velocity.x, Player.Velocity.y + 3.5f);
            //Initialize_Mono.I.Waite(   ()=> {
            //    Player.Velocity = Velo;
            //    transform.position = pos;
            //    Debug.LogError(transform.position + "         " + Player.Velocity.y);
            //}   );
        }


        if (!第一次跳跃)
        {
        if (Player.Velocity .y<8 )
        {
                if (A.当前anim.name == A_N.jump_ )
                {
                    第一次跳跃 = true;
                    A.Playanim(JUMAP_name.中间 ); 
                } 
            } 
        }

        if (!IP.按键检测_按住 (IP.跳跃))//被弹簧弹上去
        {
            if (Player.Velocity .y>Player.玩家数值.跳跃瞬间速度)
            {
                A.Playanim(JUMAP_name.上去); 
                第一次跳跃 = false;
            }  
        }
        下落降落平台检测();

        卡在();
        //if (IP.按键检测_按住(IP.攻击)&& IP.按键检测_按住(IP.下))
        //{

        //}
    }
    Vector2 Velocity => Player.Velocity;
    LayerMask 碰撞检测层 => Player.碰撞检测层;
    Transform transform => Player.transform;
    BoxCollider2D po => Player.po;
    void 下落降落平台检测()
    {
        if (Velocity.y > -0.5f) return;
        var DI_ =
              Physics2D.BoxCast(
   new Vector2(po.bounds.center.x, po.bounds.min.y),
    new Vector2(po.bounds.size.x - 0.3f, 0.1f),
    0f,
    Vector2.down,
     0.4f + po.edgeRadius,
   碰撞检测层
    )
    .collider;
        if (DI_ != null)
        {
            if (   DI_.gameObject.layer == Initialize.L_M_Ground)
            {
                float ca = Initialize.获取两碰撞体最近方向的插值(Player.gameObject, DI_.gameObject);
                transform.position = new Vector2(transform.position.x, transform.position.y - ca); 
            }
        }
    }

    public override void 松开(KeyCode obj)
    { 
        if (obj== IP.攻击)
        { 
                f.To_State(E_State.skyatk);
                 
            return;
        }
 
            if (obj == IP.跳跃)
            { 
            var y = 0f;
            if (Player.Velocity.y > 0) y = Player.Velocity.y;
            var a = y / Player.玩家数值.跳跃瞬间速度; 
            a = Mathf.Pow(a, 1.7f); 
            Player.AddForce(new Vector2(0, Player.玩家数值.小跳向下力 * a)); 
        }
        if (obj == IP.左 || obj == IP.右)
        {
            //Player.Velocity = Player.Velocity;
            if (Player.Velocity.y<0)
            {
                //Debug.LogError("                小于零"            );
                Player.Velocity = new Vector2(Player.Velocity.x *  8 / 10, Player.Velocity.y);
            }

            //float 比例 = Mathf.Abs(Player.Velocity.x) / Player.玩家数值.常态速度;
            //float foce = Player.玩家数值.水平相反力 * -Player.LocalScaleX_Set * 比例;

            //Debug.LogError(foce + "       " + Player.Velocity);
            //Player.AddForce(new Vector2(foce, 0));
        }

        if (obj== IP.跳跃)
        {
            if (A.当前anim.name == A_N.air)
            {
                A.Playanim(JUMAP_name.下去);
            }
        }
    
    }
 
    public override void 按下(KeyCode obj)
    {
        //if (obj ==IP.格挡)
        //{
        //    f.To_State(E_State.cricleatk);
        //    return;
        //}
        if (obj==IP.攻击)
        {
            if (IP.按键检测_按住 (IP.下))
            {
                f.To_State(E_State.downatk);
                return;
            }
            //else if(IP.按键检测_按住(IP.上))
            //{
            //    f.To_State(E_State.upatk );
            //}
       
        }
        if (obj==IP.冲刺&&Player.前空_)
        { 
            f.To_State(E_State.skydash);
            return;
        } 
    } 
}
