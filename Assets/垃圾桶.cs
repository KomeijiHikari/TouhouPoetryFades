using Enemmy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleFSM;
using Sirenix.OdinInspector;
namespace  SampleFSM
{
    public class 泛用状态机 : SerializedMonoBehaviour
    {

        [SerializeField ]
        public state 当前;
        protected state 上一个; 
        public bool DeBuG=false ; 
        protected void to_state(state a)
        {
            if (当前 == a || !a.激活) return;
            if (DeBuG) Debug.LogError("   前一个名字： " + 当前.StateName + "  下一个名字：  " + a.StateName);
            当前 = 当前.to_state(a); 
     
 
            //当前?.Exite?.Invoke();
            //当前.活动 = false;

            //上一个 = 当前;
            //当前 = a;

            //当前.活动 = true;
            //当前.timeCount = Time.frameCount;
            //当前.time = Time.fixedTime; 
            //当前?.Enter?.Invoke();
        }
  protected  virtual void Update()
        { 
   
            当前?.Stay?.Invoke(); 
        }
        protected   virtual  void FixedUpdate()
        {

            当前?.fatherFix();
            当前?.FixStay?.Invoke();
        }
        /// <summary>
        ///   A  to   B
        /// </summary>
        /// <param name="D"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        protected string N(state D, state a)
        {
            var d = D.StateName;
            var x = a.StateName;
            if (DeBuG)  Debug.Log (d + "_to_" + x+"简易状态机   ：obj为"+gameObject );

            return d + "_to_" + x;
        }
    } 
    public    class Tree
    {

    }
    [Serializable ]
    public class state
    {
        public static bool 嵌套Deb  ;
        public string StateName { get => stateName; private set => stateName = value; }
        [SerializeField]
        [DisplayOnly]
        private string stateName;

        public bool 激活 = true;

        [DisplayOnly]
        public bool 活动;

        [DisplayOnly]
        public float time;
        [DisplayOnly]
        public int timeCount;

        public Action Enter;

        public Action BaseFixStay;
        public Action FixStay;
        public Action Stay;
        public Action Exite;

        public void  fatherFix()
        {
            if (Father!=null)
            {

                Father.fatherFix();
               Father.BaseFixStay?.Invoke();
            }
        }

        public state Get_father()
        { return Father; }

        public state Father { get; private set; }
        public bool Deb;
        Vector2Int Get_关系(state Target)
        {
            state TTarget = Target;
            state My = this;
            List<state> X = new List<state>();
            List<state> Y = new List<state>();

            X.Add(My);
            Y.Add(TTarget);

            if (嵌套Deb )
            {
                Debug.LogError(StateName+X.Count +"   目标  "+TTarget .stateName+Y.Count );
          
            }
            ///为啥一个用方法一个用属性
            while (My.Father != null)
            {
                My = My.Father;
                X.Add(My);
            }

             

            while (TTarget.Father != null)
            {
                TTarget = TTarget.Father;
                Y.Add(TTarget);
            }
             
            for (int i = 0; i < X.Count; i++)    ///有相交
            {
                for (int Z = 0; Z < Y.Count; Z++)
                {
                    if (X[i] == Y[Z])
                    {
                        var v = new Vector2Int(i, Z); 
                        if (v == Vector2Int.zero)
                        { 
                            return v;
                        }
                        else
                        { 
                            return (v - Vector2Int.one);
                        }
                    }
                }
            }
            ///无相交 
          
            return new Vector2Int(X.Count, Y.Count);
        }
        int i;
        int 次数;
        public state to_state(state a)
        {
 
            if (i != Time.frameCount)
            {
                次数 = 0;
                i = Time.frameCount;
            }
            else
            {
                次数++;
                if (次数 > 10)
                {
                    Debug.LogError("同一帧调用起码十次，检查代码please");
                }
            }

            if (StateName == a.StateName || !a.激活)
            { 
                return this;
            }
             
            var v = Get_关系(a);
            if (v.x >= 0) Up(v.x); 

            活动 = false;


            a.活动 = true;
            a.time = Time.time;
            a.timeCount = Time.frameCount;
            if (v.y >= 0) a.Down(v.y);
            return a;
        } 
        public state(string s, state F) : this(s)
        {
            if (F == null) Debug.LogError("状态  " + s + "试图跟着空父类   ，可能要调整代码顺序"); 
            Father = F;
        } 
        public state(string s) { 
            StateName = s;
        } 


        void Up(int i)
        {
            if (嵌套Deb) Debug.LogError(StateName + "Up                        Exite");
            Exite?.Invoke();


            if (i > 0)
            {
                i--;
                if (Father != null) Father.Up(i);
            }
        }
        void Down(int i)
        {
            if (i > 0)
            {
                i--;
                if (Father != null) Father.Down(i);
            }


            if (嵌套Deb) Debug.LogError(StateName + "Down                       Enter");
            Enter?.Invoke();
        }
    }
}

namespace Enemmy
{

    public class 垃圾桶 : 泛用状态机
    {
        [SerializeField]
        float 弹道速度 = 1;

        protected state dead = new state("dead");
        protected state atk = new state("atk");
        protected state fang = new state("fang");
        protected state idle = new state("idle");
        Enemy_base e;
        // 监控组件，用于在场景切换或禁用时清理发射的飞行物
        监控激活碰撞框 监控;
        // 跟踪该垃圾桶发射或产生的飞行物，便于在场景切换时清理
        List<Fly_Ground> Flist = new List<Fly_Ground>();

        [Space ]
        [Header("  检查范围之内 ")]
        [SerializeField] float A, B;
        [Space]


        // (已在上方声明 Flist)
        [SerializeField]
        [DisplayOnly]
        float 距离_;
        戒备 j;
        [SerializeField]
        Atk_Anim a;
        float 距离 => (Player3.I.transform.position - transform.position).magnitude;
        enum E_距离情况
        {
            远, 打, 防御
        }
 [SerializeField ][DisableOnPlay]       E_距离情况 距离情况;

        public Transform 发射点; 
        private void Awake()
        {

            j = GetComponent<戒备>();
            e = GetComponent<Enemy_base>(); 
            当前 = idle;

            // 获取监控组件并订阅事件，用于在监控失活时清理飞行物
            gameObject.组件(ref 监控);
            if (监控 != null)
            {
                监控.是我 += (b) =>
                {
                    if (!b)
                    {
                        // 当监控变为非激活时，清理所有跟踪的飞行物并回收到对象池
                        while (Flist.Count != 0)
                        {
                            for (int i = 0; i < Flist.Count; i++)
                            {
                                var fl = Flist[i];
                                if (fl == null) continue;
                                try
                                {
                                    fl.销毁触发?.Invoke();
                                }
                                catch { }
                                Surp_Pool.I.ReturnPool(fl.gameObject);
                            }
                        }
                    }
                };
            }


            e.销毁触发 += () => { to_state(dead); };

            dead.Enter += () =>
           {
               e.an.Play(idle.StateName );
           };
            dead.Stay += () =>
            {
                if (e.当前hp > 0)
                {
                    to_state(idle);
                }
            };


                if (a != null) a.ATK += () => { 

                var a = Surp_Pool.I.GetPool(Surp_Pool.子弹);
                var b = a.GetComponent<Fly_Ground>();



                b.初始化(new Vector2(transform.localScale.x, 0), 
                    发射点.transform.position,
                    e.Speed_Lv * 弹道速度
                     );

                // 跟踪由本垃圾桶发射的子弹，便于清理
                var fg = b as Fly_Ground;
                if (fg != null)
                {
                    Flist.Add(fg);
                    // 当子弹被销毁时，从列表中移除引用
                    Action handler = null;
                    handler = () => { fg.销毁触发 -= handler; Flist.Remove(fg); };
                    fg.销毁触发 += handler;
                }
            }; 
    

                idle.Enter += () => {
                if (上一个 == fang)
                {

                    e.an.Play(N(fang, idle));
                }
                else if (上一个 == atk)
                {
                    e.an.Play(N(atk, idle));
                }
            };
            fang.Exite += () => { e.HPROCK = false; };
            fang.Enter += () => {
                e.HPROCK = true;
                if (上一个 == atk)
                {
                    e.an.Play(N(atk, fang));
                }
                else
                {
                    e.an.Play(N(idle, fang));
                }

            };
            atk.Enter += () => {
                e.an.Play(N(
                    idle, atk
                    ));
            };
        }
        protected override  void Update()
        {
            base.Update();
            距离_ = 距离;
            if (j.发现玩家了吗)
            {
                if (距离 > A)
                {
                    距离情况 = E_距离情况.远;
                }
                else if (距离 > B)
                {
                    距离情况 = E_距离情况.打;
                }
                else
                {
                    距离情况 = E_距离情况.防御;
                }
            }
            else
            {
                距离情况 = E_距离情况.远;
            }

            switch (距离情况)
            {
                case E_距离情况.远:
                    to_state(idle);
                    break;
                case E_距离情况.打:
                    to_state(atk);
                    break;
                case E_距离情况.防御:
                    to_state(fang);
                    break;
            }
        }
    }
}

