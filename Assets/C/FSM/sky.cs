//using Schema.Internal.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using static BiologyBase;

public class sky : State_Base  
{
    //float 原始碰撞;
  public static  Vector2 Startsize;
    public static Vector2 StartOff;
    private Vector2 StartDunOff;
    private Vector2 StartDunSize;

    public override void AweakStatebase()
    {
        base.AweakStatebase();
        //原始碰撞 = Player.po.size.x;
        Startsize = Player.站立box.size;
        StartOff = Player.站立box.offset;

        StartDunOff= Player.蹲BOX .offset;
        StartDunSize= Player.蹲BOX.size;
    }
    float 贴墙时间max = 0.1f;
    float 贴墙时间 = 0;

    float X速度加成;
    float Y速度加成;
    public override void EnterState()
    {
        if (Initialize_Mono.I.动态跳跃碰撞)
         Player.蹲BOX.isTrigger = true;
        贴墙时间 = 0;
       前空  =Player.前空_ ;
        Y速度加成 = 0;
        X速度加成 = 0;

        if (Player.Velocity.y > 0)
            if (Player3.I.脚下 != null)
        {

         if(Player3.I.脚下.移动方向.y>0)
            {   
                Y速度加成 = Player3.I.脚下.单位速度* Player3.I.脚下.Lerp.y;
                var a = Player.Get_rb();
                Y速度加成= a.Next(1,new Vector2(0, Y速度加成)).y;

                Player.跳跃触发(new Vector2(Player.Velocity.x, Player.玩家数值.跳跃瞬间速度 + Y速度加成));
                //Debug.LogError(Player.玩家数值.跳跃瞬间速度 +"AAAAAAAAAAAAAAAA" + Y速度加成);
            }
            //Debug.LogError("WWWWWWWWWWBBBBBB" + Player3.I.脚下.移动方向.x);
            if (Player3.I.脚下.移动方向 .x!=0&&Player.LocalScaleX_Int== Player3.I.脚下.移动方向.x)
            {
                X速度加成 = Player3.I.脚下.移动方向.x* Player3.I.脚下.单位速度 * Player3.I.脚下.Lerp.x;
                //Debug.LogError( "BBBBBBBBBBBBBBBBBBBB" + X速度加成);
 
            }
            Player3.I.脚下 = null;
        
        }

        //if (!Player3.I.is原Parent)
 
            //Player3.I.ChangeFather();
            //Initialize_Mono.I.Waite(() =>
            //{
            //    if (Player.脚下==null)
            //    {
            //        Player3.I.ChangeFather();
            //    } 
            //},0.1f);
 
        //Player.po.size  = new Vector2(Player.po.size.x*0.4f, Player.po.size.y);
        不是第一次悬浮 = false;
           第一次跳跃 = false;
        //Player.前档板.enabled = true;

        if (f.I_State_L.state==E_State. wall)
        { 
            Wall_Y = transform.position.y;
        }
        else
        {
            Wall_Y = 0; 
        }
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

                Vector2 dian = Player.脚底发射(2f);
                if (dian!=Vector2 .zero)
                {
                    yalaAudil.I.EffectsPlay("Jump", 0);
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
                    yalaAudil.I.EffectsPlay("Jump", 0);
                    A.Playanim(JUMAP_name.上去);
                    特效_pool_2.I.GetPool(dian, T_N.特效跳跃).Speed_Lv = Player3.Public_Const_Speed;
                }
                break;
            case E_State.cricleatk:
                A.Playanim(JUMAP_name.下去);
                break;
            case E_State.pa:
            case E_State.wall_surfing:
                //Debug.LogError("AAAAAAAAAAAAAAAA");
                A.Playanim(JUMAP_name.中间);
                break;
            case E_State.dash:
                Player.加速(true);
                Player.dundash.冷却好了 = true; 
                //A.Playanim(JUMAP_name.下去);
                break;
        }

        //if (animname == null) return; 
        //A.Playanim(animname);
 
    }

    public override void ExitState(E_State e)
    {
        base.ExitState(e);
        Player.记录a(0);
        //transform.position += Vector3.up * 0.1f;
        if (Initialize_Mono.I.动态跳跃碰撞)
            Player.差价();


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

    float Wall_Y = 0;
    public override void FixedState()
    { 

        if (IP.方向正零负!=0)
        {
            var a = Player.返回方向(); 
            if (Player.前空_) Player.AddForce(new Vector2(a * Player.玩家数值.起步速度, 0)); 
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
        if (Player.N_.悬浮)
        { 
        if (IP.按键检测_按住(IP.k.跳跃)&& IP.Get_key( IP.k.跳跃).Keeptime>0.2f)
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
        Vector2 dian = Player.脚底发射(2.9f, 2);
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
        if (IP.按键检测_按住 (IP.k.下))
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
            var a = Player.Bounds.min-new Vector3 (0,Player.蹲BOX .edgeRadius );
            var b = new Vector3(Player.Bounds.max.x , Player.Bounds.min.y)-new Vector3 (0,Player.蹲BOX .edgeRadius );

        var pa=    Physics2D.OverlapCircle(a, 0.1f, Player.碰撞检测层);
            var pb=     Physics2D.OverlapCircle(b, 0.1f, Player.碰撞检测层);

            if (pa!=null||pb!=null)
            { 
                //Player.Ground = true;
               Player. transform.position-= new Vector3 (0,0.1f);
            }
        }
    }
    bool 前空=true;
    Sprite targetSprite;

    
    void asd()
    {
        ///内部尺寸80  box 尺寸5*5  16倍
        ///80= boxsize*16
        ///size可以求
        ///offst
      Vector4 v=  targetSprite.border;
        //X=左边框、Y=下边框、Z=右边框、W=上边框

    }
    Sprite lastsp;
    Vector2Int SPsize=Vector2Int.one*80;

    public override void UpdateState()
    {
        if (Player3.I.脚下 != null )
        {
            Player3.I.脚下  = null;
        }

        if (Initialize_Mono.I.MoveP_优化)
        {
            Player3.I.ChangeFather();
        }
 
        if (Initialize_Mono.I.动态跳跃碰撞) 
        if (lastsp!=Player.sp.sprite)
        {
            //更新并且改变尺寸
            lastsp = Player.sp.sprite; 
            Vector4 v = 删除_图片物理.Get_图片Bor(lastsp.border, SPsize,16);
            if (v.x!=5)
            {

                Player.站立box.size = new Vector2(Startsize.x, v.y);
                Player.站立box.offset = new Vector2(StartOff.x, v.w);

                    Player.蹲BOX.offset = StartDunOff;
                    Player.蹲BOX.size =   StartDunSize;
                }
        }
        //targetSprite.border
        //Vector2[] physicsShape = targetSprite.GetPhysicsShape
        //if (IP.按键检测_按住(IP.k.攻击))
        {
            if (
                Player_input.I.方向正零负 != 0 &&
 !Player.Ground
    && Player.Velocity.y < 0 
                )
            {
                if (Time.frameCount - f.Getstate(E_State.wall).ExiteFramet > 6)
                {
                    if (false)
                    //if (Player.顶死)
                    {
                        ///  原先bool判断为Player.顶死
                        f.To_State(E_State.wall);
                        return;
                    } 
                    else
                    {
                        //if(false)
                        { 
                            var a = Player.假检测(0.5f);
                            if (a != Vector3.zero)
                            {
                                贴墙时间 += Time.deltaTime;
                            }
                            //Debug.LogError(贴墙时间); 
                            if (贴墙时间 > 贴墙时间max)
                            {
                                var 差 = MathF.Abs(a.x - Player.Bounds.center.x) - (Player.Bounds.size.x / 2);

                                var Y = transform.position.y;
                                if (Wall_Y != 0 &&
                                    transform.position.y > Wall_Y///现在的值大于过去的值     比以前小不触发
                                                                 ///并且 当现在比过去的值大的太多了也不触发      
                                    && transform.position.y < Wall_Y + 1)
                                {
                                    Debug.LogError("触发AAA" + Y + transform.position.y);
                                    Y = Wall_Y;
                                }
                                if(差>1)
                                {
                                    Debug.LogError("触发   瞬移  差是" + 差 + "位置:" + transform.position.x+"方向int"+ Player.LocalScaleX_Int
                                        + "  碰撞点位置  "+a+"v2差" + (transform.position - a));
                                    return;
                                }
                                transform.position =
                                    new Vector3(transform.position.x + 差 * MathF.Sign(Player.LocalScaleX_Int), Y, transform.position.z);

                                f.To_State(E_State.wall);
                                return;
                            }

                        }
                    } 
                }
            }
        }

        if (Player.is土狼时间_Wall == -1)
        {
            bool 按下了相同 = (IP.按键检测_按下(IP.k.左) && -Player.wall_进入为正面 == -1)
    || (IP.按键检测_按下(IP.k.右) && -Player.wall_进入为正面  == 1);
            if (按下了相同)
            { 
                ((wall)f.Getstate(E_State.wall)).金庸(0.3f);///不知道为啥没用
                A.Playanim(JUMAP_name.上去); 
                Player.跳跃触发(new Vector2(-Player.wall_进入为正面 * 8f, Player.玩家数值.跳跃瞬间速度 )
                    , "登墙跳，AAAA土狼_先空格后方向");
            }
        }

 
        if (IP.按键检测_按住(IP.k.跳跃))
            if (Player.is土狼时间_Wall == 1)
            {
                ((wall)f.Getstate(E_State.wall)).金庸(0.3f);
                Player.is土狼时间_Wall = 0;
                A.Playanim(JUMAP_name.上去); 
                Player.跳跃触发(new Vector2(-Player.wall_进入为正面 * 8f, Player.玩家数值.跳跃瞬间速度)
                 , "登墙跳，BBBB土狼_先方向后空格 __持续触发");
                return;
            }
  if (IP.按键检测_按下(IP.k.跳跃))
        {  
            if (EnterTime > 0.1f || f.I_State_L.state == E_State.cricleatk)
            {
                f.To_State(E_State.cricleatk);

                return;
            }

        }

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

        if (Player.Velocity.y>0)
        {
         
            Find(双点碰撞(Player.Velocity.y / 20));
        }

        if (!第一次跳跃)
        {
        if (Player.Velocity .y<Initialize_Mono.I.下落动画速度 )
        {
                if (A.当前anim.name == A_N.jump_ )
                {
                    第一次跳跃 = true;
                    //Debug.LogError("");
                    A.Playanim(JUMAP_name.中间 ); 
                } 
            } 
        }

        if (!IP.按键检测_按住 (IP.k.跳跃))//被弹簧弹上去
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

        if (f.I_State_L.state != E_State.hit || IP.方向正零负 != 0)
        {
            Player.水平限制();
            Player.竖直限制(); 
        }
        if (IP.方向正零负 == 0)
        {
            X速度加成 = 0;
        }

        if (X速度加成!=0)
        {

            X速度加成 -= Mathf.Sign(X速度加成) * Time.deltaTime;
            Debug.LogError(Time.frameCount + "  AAAAAWAAAA  " + X速度加成);
            Player.transform.position += (Vector3)(Vector2.right * X速度加成 * Time.deltaTime);
        }
        if (IP.方向正零负 != 0) Player.水平限制();
    }

    public override void 方向改变(bool obj)
    {
        base.方向改变(obj);
        X速度加成 = 0;
    }
    Vector2 Velocity => Player.Velocity;
    LayerMask 碰撞检测层 => Player.碰撞检测层;
   m_transform transform => Player.transform;
    BoxCollider2D po => Player.蹲BOX;
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

    public static List<RaycastHit2D> 双点碰撞(float distance)
    {
        var a = new Vector2(Player3.I.Bounds.min.x, Player3.I.Bounds.max.y);
        var b = (Vector2)Player3.I.Bounds.max;
        LayerMask l = 1 << Initialize.L_Ground|1<<Initialize.L_M_Ground;

        // 使用类内的 碰撞检测层
        var mask = l;

        // 从 a 和 b 向上发射射线
        List<RaycastHit2D> hitA = new List<RaycastHit2D>(Physics2D.RaycastAll(a, Vector2.up, distance, mask));
        List<RaycastHit2D> hitB = new List<RaycastHit2D>(Physics2D.RaycastAll(b, Vector2.up, distance, mask));

        // Debug 可视化（使用 Vector3 构造以避免 Vector2/Vector3 运算二义性）
        Debug.DrawLine(new Vector3(a.x, a.y, 0f), new Vector3(a.x, a.y + distance, 0f), Color.cyan, 0.5f);
        Debug.DrawLine(new Vector3(b.x, b.y, 0f), new Vector3(b.x, b.y + distance, 0f), Color.cyan, 0.5f);

        for (int i = 0; i < hitB.Count; ++i)
        {
            hitA.Add(hitB[i]);
        }
        return hitA;
   }

 
  public static  bool Find(List<RaycastHit2D> L)
    { 
        bool asd(RaycastHit2D r)
        {
            if (r.collider!=null)
            {
                r.point.DraClirl();
                var obj = r.collider.gameObject;
                bool bo =!  obj.CompareTag(Initialize.Ground);
                if (bo)
                {
                var D=    obj.GetComponent<单方面通过>();
                if(D!=null)
                    { 
                        D.触发();
                        return true;
                    }
                } 
            }
            return false;
        }
        for (int i = 0; i < L.Count; i++)
        {
       return     asd(L[i]);
        }
        return false;
    }
    public override void 松开(KeyCode obj)
    { 
        if (obj== IP.k.攻击)
        { 
                f.To_State(E_State.skyatk);
                 
            return;
        }

        if (obj == IP.k.跳跃)
        {
            Debug.Log("向下力被触发");
            
            var y = 0f;
            if (Player.Velocity.y > 0) y = Player.Velocity.y;
            var a = y / Player.玩家数值.跳跃瞬间速度;
            a = Mathf.Min(a, 0.4f);
            //a = Mathf.Pow(a, 1.7f);
            Player.AddForce(new Vector2(0, Player.玩家数值.小跳向下力 * a));
        }
        if (obj == IP.k.左 || obj == IP.k. 右)
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

        if (obj== IP.k.跳跃)
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
        if (obj==IP.k. 攻击)
        {
            if (IP.按键检测_按住 (IP.k.下))
            {
                f.To_State(E_State.downatk);
                return;
            }
            //else if(IP.按键检测_按住(IP.上))
            //{
            //    f.To_State(E_State.upatk );
            //}
       
        }
        if (obj==IP.k. 冲刺 &&Player.前空_)
        { 
            f.To_State(E_State.skydash);
            return;
        } 
    } 
}
