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
 
            当前?.Exite?.Invoke();
            当前.活动 = false;

            上一个 = 当前;
            当前 = a;

            当前.活动 = true;
            当前.timeCount = Time.frameCount;
            当前.time = Time.time; 
            当前?.Enter?.Invoke();
        }
  protected  virtual void Update()
        { 
            当前?.Stay?.Invoke(); 
        }
        protected   virtual  void FixedUpdate()
        { 
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
        public static bool 嵌套Deb;
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
        public Action FixStay;
        public Action Stay;
        public Action Exite;

        public state Get_father()
        { return Father; }

        public state Father { get; private set; }
        public bool Deb;
        Vector2Int Get_关系(state Target)
        {
            state My = this;
            List<state> X = new List<state>();
            List<state> Y = new List<state>();

            X.Add(My);
            Y.Add(Target);
            while (My.Get_father() != null)
            {
                My = My.Get_father();
                X.Add(My);
            }
            while (Target.Father != null)
            {
                Target = Target.Father;
                Y.Add(Target);
            }
            for (int i = 0; i < X.Count; i++)    ///有相交
            {
                for (int Z = 0; Z < Y.Count; Z++)
                {
                    if (X[i] == Y[Z])
                    {
                        var v = new Vector2Int(i, Z);
                         
                        if (v == Vector2Int.zero) return v;
                        else return (v - Vector2Int.one);
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
                if (Deb)
                {
                    Debug.LogError("   前一个名字： " + StateName + "  下一个名字：  " + a.StateName + "\n" + a.激活);
                }
                return this;
            }

            if (Deb )  Debug.LogError("   前一个名字： " + StateName + "  下一个名字：  " + a.StateName  );
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
        public state(string s)=> StateName = s;
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

        [Space ]
        [Header("  检查范围之内 ")]
        [SerializeField] float A, B;
        [Space]

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

                if (DeBuG) b.Debul = true;
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

