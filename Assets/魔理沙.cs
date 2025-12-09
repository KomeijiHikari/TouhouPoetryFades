using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleFSM;
using BehaviorDesigner.Runtime; 
using UnityEngine.Playables; 
using System;
using UnityEngine.Events;
using 发射器空间;
namespace Boss
{   
    public partial class 魔理沙
    {
        public 发射器 蘑菇发射器;
        public 发射器 星辉发射器;
        public 发射器 坠星发射器;
        public 发射器 雷击发射器;
        public 发射器 雷击发射器1;

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
            Debug.LogError("       伤害玩家一下(string s) 伤害玩家一下(string s)    else if (s == A_转圈)AAAAAAAAAAAAAAAAAAAAAAA"+s);

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
                if (Initialize.RandomInt(1, 蘑菇几率) < 蘑菇几率 - 2  || 普通攻击次数 >= 普通攻击最大次数)
                {
                    //if (DeBuG )  Debug.LogError("ndomInt(1, 蘑菇几率) < 蘑菇几率 ");
                    蘑菇几率 = 5;
                    普通攻击次数 = 0;
                    A.Playanim(A_转圈);
                    ATK_g时间 = 1;
                    蘑菇发射器.gameObject.SetActive(true );
                    Initialize_Mono.I.Waite(
                        () => 蘑菇发射器.gameObject.SetActive(false)
                     ,0.1f );
                    //StartCoroutine(       Initialize.Waite(  () => { 蘑菇发射器.gameObject.SetActive(false ); }   )      ，    );
                    //Debug.LogError("                        蘑菇");
                    //T.Play("蘑菇");
                    to_state(atk_g);
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
                if (Initialize.RandomInt(1, 几率) < 几率 - 2 || 普通攻击次数 >= 普通攻击最大次数)
                {
                    几率 = 5;
                    ///成功 
                    广播体操 = 0;
                    魔炮管理.I.序号 = 3;
                    魔炮管理.I.是星星 = false;
                    普通攻击次数 = 0;
                    T.Play("大魔炮"); 
                    return;
                }
                else
                {
                    ///失败 
                    几率++; 
                }
            }

 
            if (广播体操 == 1|| 笨蛋玩家大炮 >= 2 || 普通攻击次数 >= 普通攻击最大次数)
            {
                if (!扫把.isActiveAndEnabled)
                {
                    广播体操 = -1;
                    普通攻击次数 = 0;
                    笨蛋玩家大炮 = 0;
                    to_state(扫把atk);
             
                    return;
                } 
            }

            //到这里就是普通情况
            //if (Initialize.RandomInt(1, 3) == 2)
            //{
            //    //一半几率
            //    魔炮管理.I.是星星 =!魔炮管理.I.是星星;
            //}
            魔炮管理.I.是星星 = !魔炮管理.I.是星星;
            //魔炮管理.I.是星星 = true;
            if (魔炮管理.I.是星星)
            {
                int  A =  0 ; 
               while (A == 魔炮管理.I.序号)
                {
                    A = Initialize.RandomInt(1, 4);
                }
                魔炮管理.I.序号 = A;
            }
            else
            {
                魔炮管理.I.序号 = Initialize.RandomInt(1, 3);   
            }
            var a = Initialize.RandomInt(1, 10);
            //if (a >6)
            //{ 
            //    魔炮管理.I.序号 = 1;
            //}
            //else if (a >3)
            //{ 
            //    魔炮管理.I.序号 = 2;
            //}
            //else  if( ( 魔炮管理.I.是星星))
            //{  
            //    魔炮管理.I.序号 = 3; 
            //}

            //if (魔炮管理.I.序号 == 3&& !魔炮管理.I.是星星)
            //{
            //    魔炮管理.I.是星星 = true;
            //}
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

        float 扫把计时最大 =4;
        float 扫把计时
        {
            get { return 扫把计时_; }
            set
            {
                if (扫把计时_ != value)

                    扫把计时_ = value;
            }
        }
     public   Transform Ta, Tb;
        void 扫把atk_()
        { 
            Event_M.I.Add(Event_M.扫把打到了, (GameObject g) =>
            {
                if (g == gameObject)
                {
                    Debug.LogError("被扫把打到了");
                    ///打到了自己
                    //笨蛋玩家扫把 = 0;
                    E.被扣血(1, gameObject, 0);
                    if (E.韧性_ - 1000 > -1100)
                    {
                        E.韧性(-1000);
                    }
                    else
                    {
                        E.韧性(-1100);
                    }

                } 
            });
            扫把.销毁触发 += () =>
            {
                ///时间内
                if (扫把计时 > 0 && 扫把计时 < 扫把计时最大)
                {
                    if (当前 != 倒地)
                    {
                        if (当前 == atk_g)
                        {
                            if (!扫把2.isActiveAndEnabled)
                            { 
                                to_state(idle_g);
                            }
                        }
                    } 
                } 
                    ///时间外
                    扫把计时 = -1;
                    扫把.gameObject.SetActive(false); 
            };
            扫把atk.Enter += () => {
                //if (地形破坏)
                //{
                //    扫把2.gameObject.SetActive(true);
                //    扫把2.重制();
                //    扫把2.transform.position = Tb.position;

                //    if (transform.lossyScale.x == 1) 扫把2.transform.position = Tb.position;
                //    else 扫把2.transform.position = Ta.position;
                //    扫把2.方向 = new Vector2(-transform.lossyScale.x, 0);

                //    扫把2.self_speed = 5f;

                //    扫把2计时 = 0;
                //}
                A.Playanim(A_fashe);
                扫把.gameObject.SetActive(true); 
                扫把.重制();
                T扫把.position = transform.position;
                扫把.方向 = new Vector2(transform.lossyScale.x, 0);
                if (地形破坏)
                { 
                    扫把.self_speed = 4f;
                }
                else
                {
                    扫把.self_speed = 9f;
                }

                广播体操 = -1;
                扫把计时 = 0;
            };
            FixedUpdate_Action += () => {
                //if (扫把2计时 >= 0)
                //{
                //    扫把2计时 += Time.fixedDeltaTime * E.I_S.固定等级差;
                //}
                //if (扫把2计时 > 4)
                //{
                //    if (当前 == atk_g)
                //    {
                //        to_state(idle_g);
                //        扫把2计时 = -1;
                //    }
                //}

                if (扫把计时 >= 0)
                {
                    扫把计时 += Time.fixedDeltaTime * E.I_S.固定等级差;
                }

                if (扫把计时 > 扫把计时最大)
                {
                    if (当前 == atk_g)
                    {
                        to_state(idle_g);
                        扫把计时 = -1;
                    }
                }
            };
        }
        [SerializeField]
        Fly_Ground 扫把2;
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

        [SerializeField] List<发射器空间.发射器> 红色子弹;
        [SerializeField] List<发射器空间.发射器> 随机弹幕;
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
            宝宝, 一阶段,   三阶段
        }

        [SerializeField]
        Timeline管理 T;

        [SerializeField]
        GameObject 圆形发射器;

        private void Start()
        {
            当前 = NULL;
            A.Playanim(A_idle_sky);

            随机弹幕 = 随机弹幕.随机列表();

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
            Debug.LogError("    Move_to(Transform target)  ");
          var TT=  target.position - transform.position;
 
            if (TT.sqrMagnitude<4)
            {
                P.Stop_Velo();
                return true;
            }
            else
            {
                var a = TT;
                a.Normalize();
                P.Velocity = a * E.Move_speed;
                return false;
            }
        }
        void Move(Vector2  Target )
        {
           var BB= Target .x- X;
            var a = Initialize.返回正负号( Target.x - X);
            transform.localScale = new Vector2(a, 1);
            P.Velocity = Vector2.right * E.Move_speed * a;
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

        [Header("   ")]
        [SerializeField]
      Transform   地面A;
        [SerializeField]
        Transform 地面B;

        [Header(" ")]
        [SerializeField]
        [DisableOnPlay]
        List<发射器> 两边;

        public void 退场()
        {
            半灵.I.初始化(null,false);
            Initialize_Mono.I.BOSS模式(gameObject, false);
            to_state(NULL);
        }
        public void 登场()
        {
            Debug.LogError("AAAAAAAAAAAAAAAA");
            半灵.I.初始化(E);
            Initialize_Mono.I.BOSS模式(gameObject, true);
            //主UI.I.Boss血条_(gameObject, "魔理沙", true); 、
            to_state(开场);
        }
        [SerializeField ]
        bool 追人;

        //public void 星星发射()Down
        //{
        //    星星.监控子弹 = true;
        //    星星.
        //} 
    public     bool 地形破坏=false ;
        [SerializeField]
        private float aTK_s时间 = -1;
        [SerializeField]
        private float aTK_g时间 =-1;

        float 进入的血量;
        float 倒地最大扣血量 =100;
        private void Awake()
        {
            if (I != null && I != this) Destroy(this);
            else I = this;


            v = GetComponent<BehaviorTree>();
            E = GetComponent<Enemy_base>();
            P = GetComponent<Phy>();

            idle_g = new state("idle_g", 地面);
            idle_s = new state("idle_s", Air);
            atk_g = new state("atk_g", 地面);
            atk_s = new state("atk_s", Air);
            星辉 = new state("星辉", atk_s);
            星坠 = new state("星坠", atk_s);
            雷击 = new state("雷击", atk_s);
            扫把atk = new state("扫把", atk_g);
            星坠_();
            星辉_();
            雷击_();
            Start_();
            扫把atk_();

            过渡_();

            idle_g_();
            idle_s_();

            atk_s_();
            atk_g_();
            倒地_();
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
                    if (阶段 == E阶段.一阶段)
                    {
                        to_state(idle_s);
                    }
                    else
                    {
                        Down(true);
                    }
                }
            };

            Air.Enter += () =>
            {
                弹幕攻击次数1 = 0;
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
        }

        private void 倒地_()
        {
            E.A_恢复 += () =>
            {
                if (当前 == 倒地) Down(false);
            };
            E.A_破防 += () =>
            {
                Debug.LogError("AAAAAAAAAAAAA                           AAAAAAAAAAAAAAAAAA              ");
                to_state(倒地);
            };
            倒地.FixStay += () =>
            {
                //Debug.LogError(E.当前hp+"     " + 进入的血量+"     " + 倒地最大扣血量);
                if (进入的血量-E.当前hp    > 倒地最大扣血量)
                {
                    Down(false);
                }
            };
            倒地.Enter += () =>
            {
                进入的血量 = E.当前hp;
                随机弹幕 = 随机弹幕.随机列表();
                if (阶段 == E阶段.宝宝)
                {

                    阶段 = E阶段.一阶段;

                    foreach (var item in 红色子弹)
                    {
                        item.随机发射无法消弹子弹 = true;
                    }
                }
                else if(阶段== E阶段.一阶段)
                {
                    if (地形破坏 ==true)
                    {
                        阶段 = E阶段.三阶段;

                    }
                }
                A.Playanim(A_倒地);

            };
        }

        private void atk_g_()
        {
            atk_g.FixStay += () =>
            {
                退出ATK_g();
            };
            atk_g.Exite += () =>
            {
                ATK_g时间 = -1;
            };
        }
        void 退出ATK_g()
        {
            if (aTK_g时间 > 0 && Time.fixedTime - 当前.time > aTK_g时间)
            {
                if (当前 != 倒地)
                    to_state(idle_g);
            }
        }
        void 退出ATK_s()
        {
            if (aTK_s时间 > 0 && Time.fixedTime - 当前.time > aTK_s时间)
            {
                if (当前 != 倒地)
                    to_state(idle_s);
            }
        }
        private void atk_s_()
        { 
            atk_s.Exite += () =>
            {
                foreach (var item in 随机弹幕)
                {
                    if (item.gameObject.activeInHierarchy)
                    {
                        item.gameObject.SetActive(false);
                    }
                }
                aTK_s时间 = -1;
            };
            atk_s.FixStay += () =>
            {
                退出ATK_s();

                if (追人)
                {
                    Vector2 target = default;
                    if (地形破坏)
                    {
                       
                        float A,B,P;
                        P = Player3.I.transform.position.x;
                        A = 地面A.position .x ;
                        B = 地面B.position.x;

                        if (Mathf .Abs (P-A)< Mathf.Abs(P - B))
                        {
                            target = 地面B.position;
                            //A  更近
        
                        }
                        else
                        {
                            //B 更近
                            target = 地面A.position;
                        }
                       
                        if (Mathf.Abs(target.x- transform.position.x) < 1f)
                        {
                            追人 = false;
                            Down(true);
                        }
                    }
                    else
                    {
                        target = Player3.I.transform.position;
                    }
                    Move(target);

           

                    if (Mathf.Abs(距离) < 1f)
                    {
                        追人 = false ;
                        Down(true);
                    }
                }
               
                    if (T.PD.playableAsset.name == "地形炮" &&
                                                                地形炮.Rs != null&&
                                                             T.PD.state==PlayState.Playing)
                    {
                         for (int i = 0; i < 地形炮.Rs.Length; i++)
                        {
                            var a = 地形炮.Rs[i];
                            if (a.collider.gameObject.layer == Initialize.L_Ground)
                            {
                                  var G = a.collider.gameObject.GetComponent<被打消失>();
                                if (G != null)
                                {
                                      G.被扣血(9, gameObject, 0);
                                    if (G.当前hp<=0)
                                    {
                                        地形破坏 = true;
                                    } 
                                }
                            }
                            if (a.collider.gameObject.layer == Initialize.L_Player)
                            {
                                伤害玩家一下("魔炮");
                            }
                        }
                    }
                
            };
        }

        void 顺序(发射器 F)
        {
              if (F.子弹列表.Count > 0)
                for (int IA = 0; IA < F.子弹列表.Count; IA++)
                {
                     Bullet T = (Bullet)F.子弹列表[IA];
                    float 间隔 = IA * 0.5f;
                    //T.生命周期 = 20 + 间隔;
                    T.Add(间隔, () => {
                        T.L线速度 = 100;

                          Vector2 方向 = new Vector2(-1, -5);
                        T.A角速度 = Initialize.To_方向到角度(方向) / Time.fixedDeltaTime;

                        Initialize_Mono.I.Waite(() => { T.方向 = 方向; }, 0.1f);
                    });
                }
        }
        private void 雷击_()
        {

            雷击 .Enter += () =>
            {
                雷击发射器 .监控子弹 = true;
                雷击发射器 .gameObject.SetActive(true);
                雷击发射器1.监控子弹 = true;
                雷击发射器1.gameObject.SetActive(true);
                P.Stop_Velo();

                顺序(雷击发射器1);
                顺序(雷击发射器);
 
            };

            雷击.FixStay += () => {
                if (雷击发射器.子弹列表.Count==0)
                {
                    雷击发射器1.gameObject.SetActive(false );
                    雷击发射器.gameObject.SetActive(false );
                    to_state(idle_s);
                } 
            };
        }

        private void Start_()
        {
            近战.Deb = true;
            屁股.Deb = true;
            背后炮.Deb = true;
            下蹲.Deb = true;
            跳跃.Deb = true;
            圆劈.Deb = true;

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
                Debug.LogError("AAAA                 屁股.Enter += () => += ()   屁股.Enter += () =>) => ");
                伤害玩家一下(A_屁股);
            };
            近战.Enter += () =>
            {
                Debug.LogError("AAAA              近战.Enter += () =>近战.Enter += () =>近战.Enter += () => " );
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


            //E.被打 += () =>
            //  {
            //      if ( MathF .Abs (扫把.transform.position.x - transform.position.x)   <4f&&
            //      扫把.Currrtten==扫把.L2 )
            //      {
            //          to_state(倒地);
            //      }
            ////  };

            //坠星发射器.发射.AddListener(() =>
            //{

            //    int I = 1;
            //    if (坠星发射器.次数 > 1 && Initialize.是奇数(坠星发射器.次数)) I = -1;
            //    坠星发射器.transform.position = 坠星发射器.transform.position + (I * 3 * Vector3.one);
            //});
        }

        void 星坠_()
        {
            星坠.Enter += () =>
            { 
                坠星发射器.监控子弹 = true;
                坠星发射器.gameObject.SetActive(true);
                P.Stop_Velo();

                aTK_s时间 = 7;
            }; 
            星坠.FixStay+= 退出ATK_s ;
        }
        private void 星辉_()
        {

            星辉.Enter += () =>
            {
                星辉发射器.监控子弹 = true;
                星辉发射器.gameObject.SetActive(true);

                星辉发射器.初始化 += (Bullet_base B) => {
                    B .Add( 0.3f, () => {
                        B.A角速度 = Initialize.To_方向到角度(B.返回当前指向玩家的方向(transform.position)) / Time.fixedDeltaTime;
                        B.L线速度 = 10;
                    });
                };

                aTK_s时间 = 7;
            };
            星辉.FixStay += 退出ATK_s;

            //星辉.FixStay += () =>
            //{
            //    if (星辉发射器.子弹列表.Count > 0)
            //    {
            //        for (int i = 0; i < 星辉发射器.子弹列表.Count; i++)
            //        {
            //            var axx = 星辉发射器.子弹列表[i];
            //            if (axx.生命周期 > 9.5 && axx.生命周期 < 9.7)
            //            {
            //                var a = axx.返回当前指向玩家的方向(transform.position);
            //                axx.A角速度 = Initialize.To_方向到角度(a) / Time.fixedDeltaTime;
            //            }
            //        }
            //    }
 
            //};
        }

        private void 过渡_()
        {
            过渡.Exite += () =>
            {
                if (阶段 != E阶段.宝宝)
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

                if (阶段 != E阶段.宝宝)
                    Initialize_Mono.I.Waite_同速 (()=> {
                        foreach (var item in 两边) item.gameObject.SetActive(true);
                    },0.2f);
                        
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

        [Header("被打的时间")]
        [Space]
        public float idle_S时间=1;
        public float idle_G时间 = 1;

        private void idle_g_()
        {
            idle_g.Enter += () =>
            {
                if (Time.time - E.被打时间 < 0.6f && !上一是屁股&& Mathf.Abs(距离)  <3f)
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
                if (Time.time - 当前.time > idle_G时间)
                {
                    //Debug.LogError(Time.time+"          "+ 当前.time+"         " + idle_G时间);
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
                                if (阶段 != E阶段.宝宝 && Initialize.RandomInt(1, 4) == 1)
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
        [SerializeField ]
        int 弹幕攻击次数;
        [SerializeField]
        int 弹幕最大攻击次数;
           
        public void 弹幕()
        {

            var a = 随机弹幕[索引];
            if (a.gameObject == 圆形发射器.gameObject)
            {
                Debug.LogError("       else if (a == 圆形发射器) ");
                aTK_s时间 = 7;
                to_state(atk_s);
                圆形发射器.SetActive(true);
            }
            else if (a.gameObject == 雷击发射器.gameObject)
            {
                Debug.LogError("   if (a==雷击发射器)  ");
                to_state(雷击);
            }
            else if (a.gameObject == 星辉发射器.gameObject)
            {
                Debug.LogError("  se if (a == 星辉发射器)  ");
                to_state(星辉);
            }
            else if (a.gameObject == 坠星发射器.gameObject)
            {
                Debug.LogError("     if (a == 坠星发射器)  ");
                to_state(星坠);
            }
            else
            {
                Debug.LogError(a + "    数据  " + (索引 - 1));
            }


            if (索引== 随机弹幕.Count -1)
            {
                索引 = 0;
            }
            索引++;
            弹幕攻击次数1++;
        }
        int 索引;
        [SerializeField ]
        bool 升空过了=false ;
        private void idle_s_()
        {
            idle_s.FixStay += () =>
            { 
                if (Time.time - idle_s.time < idle_S时间) return; 
                switch (阶段)
                {
                    case E阶段.宝宝:
                        ///预备下砸
                        追人 = true;
                        to_state(atk_s);
                        break;
                    case E阶段.一阶段:


                        ///该阶段不会主动上升  
                        ///刚进入在空中两次弹幕   DOWN   被击倒  UP  两次弹幕   地图炮  DOWN  极限火花第三阶段 

                         if (弹幕攻击次数1<弹幕最大攻击次数 )
                        {
                            弹幕();
                        }
                        //两次弹幕放完  并且升过空
                        //着个方法会反复调用在idle_s中反复调用
                        else if (升空过了&&!地形破坏 )
                        {
                            if (Move_to(中心点))
                            {

                                Debug.LogError("                      Move_to(中心点))  ");
                                to_state(atk_s);
                                P.Stop_Velo();
                                T.Play("地形炮");
                            }

                     
                        }

                        else
                        {
                            Debug.LogError("                           升空过了 = true;     升空过了 = true;  ");
                            升空过了 = true;
                            追人 = true;
                            to_state(atk_s);

                        }
                   
                        break;   
                    case E阶段.三阶段:
                        break;
                    default:
                        break;
                }
            };
        }
        Action FixedUpdate_Action;
        new private void FixedUpdate()
        {
            if (阶段==E阶段.一阶段)
            { 
                弹幕最大攻击次数 = 2;
            } 
            base.FixedUpdate();
            FixedUpdate_Action?.Invoke(); 
      
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
        protected state 扫把atk; 

        protected state 过渡 = new state("过度");
        protected state Air = new state("Air");

        protected state 开场 = new state("开场");
        protected state NULL = new state("NULL");

        protected state 持续弹幕;
        protected state 星辉;
        protected state 星坠;
        protected state 雷击;

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
                if (Player3.I==null)
                {
                    return 0;
                }
                return Player3.I.transform.position.x - X;
            }
        }

        public float ATK_g时间 { get => aTK_g时间; set {
 
                aTK_g时间 = value;
            } 
           }

        public int 弹幕攻击次数1 { get => 弹幕攻击次数; set {
                if (弹幕攻击次数1 != value)
                {
                    Debug.LogError(value);
                }
            弹幕攻击次数 = value; }
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
        private float 扫把2计时_;

        int 刷新翻转()
        {
            var a = Initialize.返回正负号(距离);
            transform.localScale = new Vector2(a, 1);
            return a;
        }
    }

     
    public enum My_Taskstate
    {
     NUll,   run,Su, Fa
    }

 
}
