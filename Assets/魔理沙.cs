using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleFSM;
using BehaviorDesigner.Runtime;
using UnityEditor.Timeline;
using UnityEngine.Playables; 
using System;
using UnityEngine.Events;
using 发射器空间;
namespace Boss
{   
    public partial class 魔理沙
    {
        public 发射器 普通星;
        public 发射器 坠星; 

        随机执行 远距离j;

   
        void A_Awake()
        {
            List<Action > As=new List<Action> ();
            List<int> Is = new List<int>();

            //As.Add(() => { });
            //Is.Add(0);

            As.Add(() => { Aa++; Debug.LogError("   行为AAAAAAv+  "+Aa); });
            Is.Add( 1);

            As.Add(() => { Ba++; Debug.LogError("   行为BBBBB+  "+Ba); });
            Is.Add(1);

            远距离j = new 随机执行(As, Is);
        } 
        int Aa;
        int Ba;
      
     public   int 广播体操 = -1;

        [SerializeField]
        [DisplayOnly]
        int 蘑菇几率 = 5;
        [SerializeField]
        [DisplayOnly]
        int 几率=5;
        [SerializeField]
        [DisplayOnly]
        public  int 笨蛋玩家大炮;

        public void 伤害玩家一下(string s)
        {
            Debug.LogError(s);

            if (s=="圆劈")
            {
                Player3.I.被扣血(20, gameObject, 0);
            } 
            else if (s=="魔炮")
            {
                Player3.I.被扣血(5, gameObject, 0);
            }
            else if (s == "背后炮")
            {
                Player3.I.被扣血(20, gameObject, 0);
            }
            else if (s == A_转圈)
            {
                Debug.LogError("      else if (s == A_转圈)      else if (s == A_转圈)      else if (s == A_转圈)AAAAAAAAAAAAAAAAAAAAAAA");
                Player3.I.硬抗 = true;
                Player3.I.受伤Force = new Vector2(1, 0);
                Player3.I.被扣血(1, gameObject, 0);
            }
            else if (s == A_屁股)
            {
                Player3.I.BigHit = true;
                Player3.I.受伤Force = new Vector2 (25,15f);
                Player3.I.被扣血(1, gameObject, 0);
            }

        }
       int 普通攻击最大次数=2;
        public int 普通攻击次数;
        void 远距离()
        {
            ///四分之二几率触发  失败触发 几率上升
            if (!蘑菇管理.I.有蘑菇在场)
            {
                if (Initialize.RandomInt(1, 蘑菇几率) < 蘑菇几率 - 2 && 普通攻击次数 >= 普通攻击最大次数)
                {
                    if (DeBuG )  Debug.LogError("ndomInt(1, 蘑菇几率) < 蘑菇几率 ");
                    蘑菇几率 = 5;
                    普通攻击次数 = 0;
                    T.Play("蘑菇");
                    return;
                }
                else
                {
                    蘑菇几率++;
                }
            }
            else
            {
                //大魔炮
                //有蘑菇 
                ///四分之二几率触发  失败触发 几率上升
                if (Initialize.RandomInt(1, 几率) < 几率 - 2 && 普通攻击次数 >= 普通攻击最大次数)
                {
                    几率 = 5;
                    ///成功 
                    广播体操 = 0;
                    魔炮管理.I.序号 = 3;
                    魔炮管理.I.是星星 = false;
                    普通攻击次数 = 0;
                    T.Play("魔炮");
                     
                    return;
                }
                else
                {
                    ///失败 
                    几率++; 
                }
            }

 
            if (广播体操 == 1|| 笨蛋玩家大炮 >=2 && 普通攻击次数 >= 普通攻击最大次数)
            {
                if (!扫把.isActiveAndEnabled)
                {
                    普通攻击次数 = 0;
                      笨蛋玩家大炮 = 0;
                    发射扫把();
                    if (DeBuG) Debug.LogError("ndASDQWEQWTWERTWER几率 ");
                    return;
                } 
            }

            //到这里就是普通情况
            if (Initialize.RandomInt(1, 3) == 2)
            {
                //一半几率
                魔炮管理.I.是星星 = true;
            }
            //魔炮管理.I.是星星 = true;
            var a = Initialize.RandomInt(1, 10);
            if (a >6)
            { 
                魔炮管理.I.序号 = 1;
            }
            else if (a >3)
            { 
                魔炮管理.I.序号 = 2;
            }
            else  if( ( 魔炮管理.I.是星星))
            {  
                魔炮管理.I.序号 = 3; 
            }

            if (魔炮管理.I.序号 == 3&& !魔炮管理.I.是星星)
            {
                魔炮管理.I.是星星 = true;
            }
            T.Play("魔炮");
            普通攻击次数++;

        } 
    }

    interface I_Boss
    {
        public List<GameObject > Gs { get; set; }
    }
    public partial class 魔理沙 : 泛用状态机, I_Boss
    {
        public List<GameObject > Gs { get => ms; set => ms = value; }
        public static 魔理沙 I;
        [SerializeField]
        [DisplayOnly]
        float 扫把计时_ = -1;

        float 扫把计时
        {
            get { return 扫把计时_; }
            set
            {
                if (扫把计时_ != value)

                    扫把计时_ = value;
            }
        }
        void 发射扫把()
        {
            A.Playanim(A_fashe);
            T扫把.gameObject.SetActive(true);
            扫把.重制();
            T扫把.position = transform.position;
            扫把.方向 = new Vector2(transform.lossyScale.x, 0);
            扫把.self_speed = 5f;

            广播体操 = -1;
            扫把计时 = 0;
        }
        [SerializeField]
        Fly_Ground 扫把;
        public Transform T扫把
        {
            get
            {
                return 扫把.transform;
            }
        }

        [SerializeField]
        List<PlayableAsset> PA;
        [SerializeField]
        PlayableDirector PD;

        [SerializeField]
        AniContr_4 A;


        [SerializeField]
        Phy_检测 地面检测;
        //[SerializeField]
        //Phy_检测 后背检测;
        [SerializeField]
        Phy_检测 天空检测;
        [DisplayOnly] [SerializeField] BehaviorTree v;
        [DisplayOnly] [SerializeField] Enemy_base E;
        [SerializeField] [DisplayOnly] Phy P;

        [SerializeField] List<发射器空间.发射器> 发射器S;

        /// <summary>
        /// -1是宝宝阶段
        /// 0是正正式开始
        /// 星星有不可消弹 过度有横向子弹 
        /// 
        /// 上升加入常态
        ///空中 一种弹幕
        /// 
        /// 2阶段  速度等级上升
        /// 下砸加入地球光
        /// 场地断裂
        /// 骑扫把过去才能近战
        ///
        /// 3阶段同速
        /// 
        /// </summary>

        public E阶段 阶段 = E阶段.宝宝;
        public enum E阶段
        {
            宝宝, 一阶段, 二阶段, 三阶段
        }

        [SerializeField]
        Timeline管理 T;

        [SerializeField]
        GameObject 圆形发射器;

        private void Start()
        {
            当前 = 开场;
            A.Playanim(A_idle_sky);


            foreach (var item in 两边) item.gameObject .SetActive(false);
        }
        bool 上一是屁股;

        void Down(bool b)
        {
            if (b)
            {
                圆形发射器.SetActive(true);
                StartCoroutine(Initialize.Waite(() => { 圆形发射器.SetActive(false); }));

                刷新翻转();
                A.Playanim(A_down);
                P.Stop_Velo();
                E.Velocity = Vector2.down * 20f;
                P.浮空 = false;
                to_state(过渡);
            }
            else
            {
                刷新翻转();
                A.Playanim(A_up);
                P.浮空 = true;
                E.Velocity = Vector2.up * 10f;
                to_state(过渡);
            }
        }
       bool  Move_to(Transform target)
        { 
          var TT=  target.position - transform.position;
            var a= TT;
            a.Normalize();
            P.Velocity = a * E.Move_speed ;
            if (TT.sqrMagnitude<4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        void Move()
        {
            P.Velocity = Vector2.right * E.Move_speed * 刷新翻转();
        }
        [SerializeField]
        [DisableOnPlay]
        Phy_检测 屁股;
        [SerializeField]
        [DisableOnPlay]
        Phy_检测 近战;
        [SerializeField]
        [DisableOnPlay]
        Phy_检测 跳跃;
        [SerializeField]
        [DisableOnPlay]
        Phy_检测 下蹲;
        [SerializeField]
        [DisableOnPlay]
        Phy_检测 地形炮;
        [SerializeField]
        [DisableOnPlay]
        Phy_检测 圆劈;
        [SerializeField]
        [DisableOnPlay]
        Phy_检测 背后炮;

        [SerializeField]
        [DisableOnPlay]
        List<发射器> 两边;
        public void 登场()
        {
            半灵.I.初始化(E);
            Initialize_Mono.I.BOSS模式(gameObject, true);
            //主UI.I.Boss血条_(gameObject, "魔理沙", true); 
        }
        bool 追人;

        //public void 星星发射()
        //{
        //    星星.监控子弹 = true;
        //    星星.
        //} 
        bool 地形破坏=false ;
        private void Awake()
        {
            if (I != null && I != this) Destroy(this);
            else I = this;


            v = GetComponent<BehaviorTree>();
            E = GetComponent<Enemy_base>();
            P = GetComponent<Phy>();
            开场 = new state("开场", Air);
            idle_g = new state("idle_g", 地面);
            idle_s = new state("idle_s", Air);
            atk_g = new state("atk_g", 地面);
            atk_s = new state("atk_s", Air);
            星辉 = new state("星辉", atk_s);

            星辉_(); 
            圆劈.Enter += () =>
            {
                伤害玩家一下("圆劈");
            };
            跳跃.Enter += () =>
            {
                伤害玩家一下("魔炮");
            };
            下蹲.Enter += () =>
            {
                伤害玩家一下("魔炮");
            };
            背后炮.Enter += () =>
            {
                伤害玩家一下("背后炮");
            };


            屁股.Enter += () =>
            {
                伤害玩家一下(A_屁股);
            };
            近战.Enter += () =>
            {
                伤害玩家一下(A_转圈);
            };
            A.关键帧 += (string s) =>
            {
                Debug.LogError("AAAA                A.关键帧 += ( string s) => {" + s + "        " + Time.frameCount);
                if (s == A_屁股)
                {
                    屁股.gameObject.SetActive(true);
                    Initialize_Mono.I.Waite(() => 屁股.gameObject.SetActive(false), 0.01f);
                }
                else if (s == A_转圈)
                {
                    近战.gameObject.SetActive(true);
                    Initialize_Mono.I.Waite(() => 近战.gameObject.SetActive(false), 0.01f);
                }
            };
            E.A_恢复 += () =>
            {
                if (当前 == 倒地) Down(false);
            };
            E.A_破防 += () =>
            {
                to_state(倒地);
            };

            Event_M.I.Add(Event_M.扫把打到了, (GameObject g) =>
            {
                if (g == gameObject)
                {
                    Debug.LogError("被扫把打到了");
                    ///打到了自己
                    //笨蛋玩家扫把 = 0;
                    E.被扣血(1, gameObject, 0);
                    E.韧性(-1010);

                }
                //else if (g == Player3.I.gameObject)
                //{
                //    ///打到了玩家
                //    笨蛋玩家扫把++;
                //} 
            });
            扫把.销毁触发 += () =>
            {
                ///时间内
                if (扫把计时 > 0 && 扫把计时 < 4)
                {
                    to_state(idle_g);
                    扫把计时 = -1;
                }
                ///时间外
                扫把计时 = -1;
                扫把.gameObject.SetActive(false);

            };

            //E.被打 += () =>
            //  {
            //      if ( MathF .Abs (扫把.transform.position.x - transform.position.x)   <4f&&
            //      扫把.Currrtten==扫把.L2 )
            //      {
            //          to_state(倒地);
            //      }
            //  };

            A_Awake();
            倒地.Enter += () =>
            {
                if (阶段 == E阶段.宝宝)
                {
                    阶段 = E阶段.一阶段;
                    foreach (var item in 发射器S)
                    {
                        item.随机发射无法消弹子弹 = true;
                    }
                }
                A.Playanim(A_倒地);
            };
            A.当前动画为百分之99 += () =>
            {
                if (当前 == atk_g)
                {
                    if (A.当前名字 == A_屁股) to_state(idle_g);

                    Debug.LogError(A.当前名字 + "  A.当前动画为百分                             动画结束");
                }

            };
            T.pause += () => { };
            T.stopp += () =>
            {
                if (当前 == atk_s)
                {
                    to_state(idle_s);
                }
                if (当前 == atk_g)
                {
                    to_state(idle_g);
                }
            };

            开场.Stay += () =>
            {
                P.Velocity = E.Move_speed * Vector2.up;
                if (天空检测.遇见了)
                {
                    if (阶段 == E阶段.二阶段)
                    {
                        to_state(idle_s);
                    }
                    else    if (阶段 == E阶段.一阶段)
                    {
                        to_state(idle_s);
                    }
                    else
                    {
                        Down(true);
                    } 
                }
            };
            过渡_();
            atk_s.FixStay += () =>
            {
                if (追人)
                {
                    Move();
                    if (Mathf.Abs(距离) < 1f)
                    {
                        Down(true);
                    }
                }
                if (!地形破坏)
                {
                    if (T.PD.playableAsset.name == "地形炮" &&
   地形炮.Rs != null)
                    {
                        for (int i = 0; i < 地形炮.Rs.Length; i++)
                        {
                            var a = 地形炮.Rs[i];
                            if (a.collider.gameObject.layer == Initialize.L_Ground)
                            {
                                Debug.LogError("    if (a.collider.gameObject.layer == Initialize.L_Ground)");
                                var G = a.collider.gameObject.GetComponent<被打消失>();
                                if (G != null)
                                {
                                    Debug.LogError("    = a.collider.gameObject.GetComponent<被打消失>();");
                                    G.被扣血(9, gameObject, 0);
                                }
                            }
                            if (a.collider.gameObject.layer == Initialize.L_Player)
                            {
                                伤害玩家一下("魔炮");
                            }
                        }
                    }
                }
   
            };
            idle_s_();
            Air.Enter += () =>
            {
                刷新翻转();
                P.Stop_Velo();
                A.Playanim(A_idle_sky);
                P.浮空 = true;
            };
            地面.Enter += () =>
            {
                P.Stop_Velo();
                P.浮空 = false;
            };

            idle_g_();

        }

        private void 星辉_()
        { 
            星辉.Enter += () =>
            {
                坠星.监控子弹 = true;
                普通星.gameObject.SetActive(true);
                坠星.gameObject.SetActive(true);
                P.Stop_Velo();
            };
            星辉.FixStay += () =>
            {
                if (普通星.子弹列表.Count > 0)
                {
                    for (int i = 0; i < 普通星.子弹列表.Count; i++)
                    {
                        var axx = 普通星.子弹列表[i];
                        if (axx.生命周期 > 9.5 && axx.生命周期 < 9.7)
                        {
                            var a = axx.返回当前指向玩家的方向(transform.position);
                            axx.A角速度 = Initialize.To_方向到角度(a) / Time.fixedDeltaTime;
                        }
                    }
                }
                if (坠星.子弹列表.Count > 0)
                {
                    for (int i = 0; i < 坠星.子弹列表.Count; i++)
                    {
                        //if (Time.frameCount - 当前.timeCount < 2 )
                        //{
                        //    坠星.子弹列表[i].L线速度 = 0;
                        //    坠星.子弹列表[i].生命周期 = 2 + i;
                        //}
                          
                        Bullet Target1 = (Bullet)坠星.子弹列表[i];
                        if (i == 0)
                        {
                            if (Target1.L线速度 == 0)
                            {
                                Target1.生命周期 = 1;
                                Target1.追踪玩家 = 0f;
                                Target1.L_Acc线加速度 = 0;

                                Target1.L线速度 = 100;
                                Vector2 方向 = new Vector2(-1, -5);
                                Target1.A角速度 = Initialize.To_方向到角度(方向) / Time.fixedDeltaTime; 
 
                                Initialize_Mono.I.Waite(() =>
                                {
                                    Target1.方向 = 方向;
                                }
                                , 0.1f);
                            }
                        }
                    }
                }
                if (坠星.子弹列表.Count == 0 && Time.time - 当前.time > 2f)
                {
                    普通星.gameObject.SetActive(false );
                    坠星.gameObject.SetActive(false );
                    to_state(idle_s);
                }
            };
        }

        private void 过渡_()
        {
            过渡.Exite += () =>
            {
                foreach (var item in 两边)
                {
                    if (item.子弹列表 != null)
                        for (int i = 0; i < item.子弹列表.Count; i++)
                    {
                        var 子弹 = item.子弹列表[i];
                        子弹.L线速度 = 10;
                    }
                    item.gameObject.SetActive(false);
                }
            };
            过渡.Enter += () =>
            {
                foreach (var item in 两边)
                {
                    item.gameObject.SetActive(true);
                }
            };
            过渡.Stay += () =>
            {
 
                if (Time.time - 当前.time > 0.2f)
                {
                    if (地面检测.遇见了)
                    {
                        to_state(idle_g);
                    }
                    else if (天空检测.遇见了)
                    {
                        to_state(idle_s);
                        圆形发射器.SetActive(true);
                        StartCoroutine(Initialize.Waite(() => { 圆形发射器.SetActive(false); }));

                    }
                }
            };
        }

        private void idle_g_()
        {
            idle_g.Enter += () =>
            {
                if (Time.time - E.被打时间 < 0.6f && !上一是屁股)
                {
                    ///上次是屁股就不触发
                    上一是屁股 = true;
                    to_state(atk_g);
                    A.Playanim(A_屁股);
                    刷新翻转();
                }
                else
                {
                    上一是屁股 = false;
                    A.Playanim(A_idle);
                }
            };
            idle_g.Stay += () =>
            {
                if (Time.time - 当前.time > 1f)
                {
                    var 要翻转 = !方向对着玩家;
                    switch (条件判断())
                    {
                        case 情况.远距离:
                        case 情况.中距离:
                            /// 星星和魔炮 
                            ///当有蘑菇在场就放  超大 光炮 
                            ///或者    远距离
                            远距离();
                            to_state(atk_g);
                            break;
                        case 情况.近距离:
                            if (方向对着玩家)
                            {
                                ///或者起飞  
                                if (阶段 != E阶段.宝宝 && Initialize.RandomInt(1, 5) == 1)
                                {
                                    Down(false);
                                    普通攻击次数++;
                                    break;
                                }
                                T.Play("三连击");
                                to_state(atk_g);
                                普通攻击次数++;
                                break;
                            }
                            else
                            {
                                普通攻击次数++;
                                T.Play("后背炮");
                                要翻转 = false;
                                to_state(atk_g);
                                break;
                            }
                        default:
                            Debug.LogError("一个都不是都不是都不是都不是都不是都不是");
                            break;
                    }
                    if (要翻转) 刷新翻转();


                }
            };
        }

        private void idle_s_()
        {
            idle_s.FixStay += () =>
            {
                switch (阶段)
                {
                    case E阶段.宝宝:
                        ///预备下砸
                        追人 = true;
                        to_state(atk_s);
                        break;
                    case E阶段.一阶段:
                        if (上一个 == 星辉)
                        {
                            Down(true);
                        }
                        else
                        {
                            to_state(星辉);
                        }

                        break;
                    case E阶段.二阶段:
                        if (!地形破坏)
                        {
                            if (Move_to(中心点))
                            {
                                to_state(atk_s);
                                P.Stop_Velo();
                                T.Play("地形炮");
                            }
                        }
                        else
                        {

                        }
        
                        break;
                    case E阶段.三阶段:
                        break;
                    default:
                        break;
                }
            };
        }

        new private void FixedUpdate()
        {
            base.FixedUpdate();

            if (扫把计时 >= 0)
            {
                扫把计时 += Time.fixedDeltaTime * E.I_S.固定等级差;
            }
            if (扫把计时 > 4)
            {
                if (当前 == atk_g)
                {
                    to_state(idle_g);
                    扫把计时 = -1;
                }
            }
        }
        public bool 有蘑菇在场;
        new void Update()
        {
            base.Update();
            有蘑菇在场 = 蘑菇管理.I.有蘑菇在场;
            方向对着玩家 = Initialize.返回正负号(transform.localScale.x) == Initialize.返回正负号(距离);
            情况_ = 条件判断();
        }
        public bool 方向对着玩家;


        protected state idle_s;
        protected state idle_g;
        protected state atk_g;
        protected state atk_s;

        protected state 过渡 = new state("过度");
        protected state Air = new state("Air");

        protected state 开场;
        protected state 星辉;

        protected state 地面 = new state("地面");
        protected state 倒地 = new state("倒地");

        float X { get { return transform.position.x; } }
        enum 情况
        {
            //翻转,
            //后背,
            远距离,
            中距离,
            近距离
        }

        public float 中近分界线 = 7;
        public float 中远分界线 = 35;
        string A_转圈 = "转圈";
        string A_屁股 = "屁股";
        string A_fashe = "fashe";
        string A_idle = "idle";
        string A_up = "up";
        string A_down = "down";
        string A_idle_sky = "idle_sky";
        string A_倒地 = "倒地";
        //string A_fashe_sky = "fashe_sky";

        [SerializeField]
        情况 情况_;
        float 距离
        {
            get
            {
                return Player3.I.transform.position.x - X;
            }
        }



        情况 条件判断()
        {
            if (Mathf.Abs(距离) > 中远分界线)
            {
                return 情况.远距离;
                ///远距离
            }
            else if (Mathf.Abs(距离) > 中近分界线)
            {
                return 情况.中距离;
                ///中距离
            }
            else
            {
                return 情况.近距离;
                ///近距离
            }
        }
        [SerializeField]
        Transform 中心点;
        [SerializeField]
        private List<GameObject > ms;

        int 刷新翻转()
        {
            var a = Initialize.返回正负号(距离);
            transform.localScale = new Vector2(a, 1);
            return a;
        }
    }


    public struct 随机执行
    {
        List<Action> As;
        List<int> Is;
        public 随机执行(List<Action> a, List<int>  b)
        {
            As = a;
            if (b!=null)
            {
                Is = b;
            }
            else
            {
                Is = new List<int> ();
                for (int i = 0; i < As.Count ; i++)
                {
                    Is.Add(1);
                }
            }
           
        }
        public void  Go()
        {
        int i=    GetWeightedRandomIndex(Is);
            As[i]?.Invoke();
            //Initialize.RandomInt
        }
        public static int GetWeightedRandomIndex(List<int> weights)
        {
            // 1. 计算所有权重总和
            int totalWeight = 0;
            foreach (int weight in weights)
            {
                totalWeight += weight;
            }

            // 2. 生成随机数（范围：0到总权重-1）
            int randomValue = Initialize.RandomInt(0, totalWeight  );

            // 3. 根据随机值确定选中的索引
            int accumulatedWeight = 0;

            for (int i = 0; i < weights.Count; i++)
            {
                accumulatedWeight += weights[i];

                // 当累加权重超过随机值时，返回当前索引
                if (randomValue < accumulatedWeight)
                {
                    return i;
                }
            }

            // 如果所有权重为0或列表为空，返回0
            return 0;
        }

    }
    public enum My_Taskstate
    {
     NUll,   run,Su, Fa
    }

 
}
