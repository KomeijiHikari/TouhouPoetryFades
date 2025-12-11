using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleFSM;
using System;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using 发射器空间;
public   interface I_Dead
{    /// <summary>
     /// 本  管理调用
     /// </summary>
    bool Dead();
    /// <summary>
    /// 本体触发  管理处理   
    /// </summary>
    Action 销毁触发 { get; set; }
    Bounds 盒子 { get; } 
}
public interface I_Revive
{
 
    Bounds 盒子 { get; }
    public bool Re { get; set; }
    public float Re_Time { get; set; }
    public bool  重制();
}
public interface I_假死
{
    public GameObject 对象 { get; }
    public void 假死了(bool 假死不);
}

public  partial  class 生命周期管理 : 泛用状态机, I_假死
{
 
 
    public GameObject 对象 => gameObject;
    public struct DeadPla:I_Save
    { 
         public       void DE()
        {
            if (DeadList == null) DeadList = new List<Vector2>();
           string name = null;
            for (int i = 0; i < DeadPla.I.DeadList.Count; i++)
            {
                var a = DeadPla.I.DeadList[i];

                name = name + "\n" + a;
            }
            //Debug.LogError(name);
        }
        public bool 对比(Vector2 V)
        {
            if (DeadList == null) return false;
            for (int i = 0; i < DeadList.Count; i++)
            {
                var a = DeadList[i];
                if (a.x._is(V.x) && a.y._is(V.y))
                {
                    return true ;
                }
            }
            return false;
        }
        public static DeadPla I;
        public  void Add(Vector2 v)
        {
            //DE();
            if (I.DeadList == null)
            {
                Debug.LogError("空");
                I.DeadList = new List<Vector2>();
            }
            var c = new List<Vector2>();
            for (int i = 0; i < I.DeadList.Count ; i++) c.Add(I.DeadList[i]); 
            if (!c.Contains(v))
            {
                c.Add(v);
            }
            I.DeadList = c;
 
            保存(); 
        }


        public string Name => Save_static.已经死掉的机关;
        public void 保存()
        {
            Save_D.Add(Name, JsonUtility.ToJson(I, true)); 
        }

        public void 读取()
        { 
            I=Save_D.Load_Value_D<DeadPla>(Name,true); 
        }

        [SerializeField ]
        List<Vector2> DeadList_;
        public List<Vector2> DeadList
        {
            get  {
                return DeadList_;
            }
            set { 
                DeadList_ = value;
            }
        } 
    } 
    [DisplayOnly ]
public state 死亡 = new state("死亡");
    [DisplayOnly]
    public state 活动= new state("活动");
    [DisplayOnly]
    public state 假死; 
    public Enemy_base E;
    public I_Dead D { get; private set; }
    public I_Revive R { get; private set; }
    public I_Speed_Change S { get; private set; }
 

 [SerializeField ][DisplayOnly ]
    float tim;

    [SerializeField ][DisplayOnly]
    bool 不参与场景;

    [SerializeField]
    UnityEvent isDeadEnter;
 bool 不参与场景复活()
    { 
            if (R==null)
            {
                return   false;
            }
            else
            {
                if (R.Re_Time != 0)
                {
                return false;
                }
                if (R.Re==false)
                {
                    return true;
                }
                return false;
            } 
    }

 [SerializeField ][DisplayOnly]
    List<GameObject> 特效列表;

    监控激活碰撞框 监控;

    public bool 需要动画 =true;
    private void Awake()
    {

        假死 =  new state("假死", 活动);

        mypo = transform.position;
        gameObject.组件(ref 监控);
        //监控 = gameObject.AddComponent<监控激活碰撞框>();
         D = GetComponent<I_Dead>();
 
        R = GetComponent<I_Revive>();
        S = GetComponent<I_Speed_Change>();

        E = GetComponent<Enemy_base >();
        if(需要动画)  gameObject . AddComponent<平台动画效果>();
        不参与场景 = 不参与场景复活();  

        if (DeBuG)
        {
            Debug.LogError("AAAAAAAAAAAAAAAAAA" + gameObject.name + gameObject.transform.position);
            假死.Deb = true;
            活动.Deb = true;
            死亡.Deb = true;
        }
    }
    [Button("Play_", ButtonSizes.Large)]
    public    void Play_()
    {
        if (R != null)
        {
            R.重制();
        }
    }
    private void Start()
    {
        if (DeBuG) Debug.LogError(gameObject + "         " + transform.position);

        DD();
        RR();
        假死State();

        当前 = new state("Nullll");
 
            ///或许 当有临时存档点的时候 延迟关闭该脚本

            当前 = 当前.to_state(初始化状态());
        }
    private void 假死State()
    {
        假死.Enter += ()=>{ 
            Event_销毁();  
        };
        假死.Exite += () => {
            R.重制();
        };
    }
    public void 假死了(   bool 去死)
    {
        if (当前 ==死亡) Debug.LogError("死了还想假死 啊"+gameObject +transform .position ); 

        if (去死)
        {
            if (不参与场景)
            {
                当前 = 当前.to_state(死亡);
            }
            else
            {
                当前 = 当前.to_state(假死);
            }
       
        }
        else 
        {
            当前 = 当前.to_state(活动);
        }
       
    }
    [SerializeField ][DisplayOnly ]
    Vector2 mypo;
    protected  override   void   Update()
    {
        base.Update(); 
    }
    public  void 存()
    {
        if (DeBuG) Debug.LogError("销毁触发  调用  死亡状态" + gameObject);
        ///把自己放进实体堆
        if (R == null || (!R.Re&&R.Re_Time ==0))
        {
            if (DeBuG)
            {
                Debug.LogError("AAAAAAAA    销毁触发  调用  死亡状态        "+ mypo);
            }
            DeadPla.I.Add(mypo); 
        }
        
    }
    state 初始化状态()
    {
        if (DeBuG) Debug.LogError("出出出1");
        if (DeBuG)
        { 
            if (DeadPla.I.DeadList!=null)
            {
                Debug.LogError("    " + DeadPla.I.DeadList.Count);
            }
        } 
        if (DeadPla.I.DeadList == null)
        {
            if (DeBuG) Debug.LogError("出出出1");
            return 活动;
        }
        else
        { 
            if (DeadPla.I.DeadList.Contains(mypo))
            { 

                if (DeBuG) Debug.LogError("出出出2");
                    D?.Dead(); 
                return 死亡;
            }
            else
            {
                if (DeBuG) Debug.LogError("出出出3");
                return 活动;
            }
        } 
    }
    public void Event_销毁()
    {
        if (D!=null) D?.销毁触发?.Invoke(); 
    }
 

    private void DD()
    {
        if (D == null) return;

        D.销毁触发 += () => {

            if (DeBuG) Debug.LogError("销毁触发  调用  死亡状态"+gameObject );
            to_state(死亡); };

        死亡.Enter += () =>
        {
            if (DeBuG) Debug.LogError("销毁触发  调用  死亡状态" + gameObject);
            D.Dead();
            效果_死亡Enter?.Invoke(); 
            isDeadEnter?.Invoke();

                       效果_不复活?.Invoke (false);
            DeadTime = Time.time;
            存();
        }; 
    }
    [SerializeField]
    float 重生时间 = 0f;
    public bool 更安全的地点 = false;
    public  bool 重生时不等待玩家 = true;
    public Action<bool> 效果_不复活 { get; set; }
    public Action 效果_死亡Enter;
    public  Action 效果_活动Enter;

    public bool  真实时间复活 = false;
    void RR()
    { 
        if (R == null) return; 
        活动.Enter += () =>
        {

            if (不参与场景复活()) 效果_不复活?.Invoke(true);

            效果_活动Enter?.Invoke();
            R.重制(); 
        };

        if (R.Re)
            {
            监控.是我+= 重制; 
            } 
            if (R.Re_Time != 0)
            {
 
            死亡.Enter += () =>
                {
                    重生时间 = 0f; 
                };
            死亡.FixStay += () =>
            {
                重生时间 += Time.fixedDeltaTime;
                //if (CanLive())
                //    {
                //        // accumulate time while there is no collision

                //    }
                //    else
                //    {
                //        // If you want the progress to reset when collision happens, uncomment next line:
                //        // 无碰撞累计 = 0f;
                //    }
                var required = R.Re_Time  ;
                if (!真实时间复活)
                    required*= Player3.Public_Const_Speed;

                if (required <= 0f)
                    {
                        复活进度 = 0f;
                        return;
                    }

                    复活进度 = Mathf.Clamp01(重生时间 / required);

                    // trigger revive when accumulated time reaches required time
                    if (重生时间 >= required)
                   {
                    if ( 重生时不等待玩家)
                    {
                        if (!CanLive())

                        {
 
                            Player3.I.安全地点();
                        }
                
                        Event_复活赛();
                        杀();
                    }
                    else
                    {
                        if (CanLive())
                        {
                            Event_复活赛();
                        }
                    }
    
                    } 
                };
            }
    }

    Vector2 复活检测范围
    {
        get
        {
            if (重生时不等待玩家)
            {
                ///干掉玩家
                return Vector2.one*0.1f;
            }
            else
            {
                ///等待玩家
                return Vector2.zero;
            }
        }
    }
    void 杀()
    {
        if (DeBuG)
        {
            Debug.LogError("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" + 盒子.size+盒子);
        }
    
  var a= 盒子.碰撞列表(1 << Initialize.L_Player, 复活检测范围,DeBuG)  ;
        for (int i = 0; i < a.Length; i++)
        {
            if (DeBuG) Debug.LogError(a[i].collider.gameObject.name);
            var b= a[i] .collider.gameObject.GetComponent<I_生命>();
            Debug.LogError(a[i].collider.gameObject.name+ "        AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA      ");
            if (b!=null)
            {
                b.被扣血(-50, gameObject, 0);
            }

        }
 
  
    }
    [DisplayOnly]  public  float 复活进度;

    [Space ]
    [DisplayOnly ]   [SerializeField ]  float 已经过去的;
    [DisplayOnly] [SerializeField ] float 界限;

    public void Event_复活赛 ()
    {
        to_state(活动);
    }
    float DeadTime;
    Bounds 盒子
    {
        get
        {
            if (D!=null)
            {
                return D.盒子;
            }
            if (R != null)
            {
                return R.盒子;
            }
            else
            {
                Debug.LogError("离谱  该obj没有   D  和R  的接口组件  obj名字："+gameObject );
                return default;
            }
        }
    }

    int I;
    bool CanLive()
    { 
        //bool A = Time.frameCount > I + 5;

        bool B = 盒子.碰撞列表(1 << Initialize.L_Player,1f)?.Get_碰撞组<Player3>() == null;
        if ( B)
        {
            I = Time.frameCount;
        }
        if (DeBuG) Debug.LogError(盒子.size + " " + 盒子.center + "      " + I + "  " + B);
        return   B;
    }

    protected override  void FixedUpdate()
    {
        base.FixedUpdate(); 
    }

    public bool 仅活着=false ;
    public bool  闪闪发光特效=true;
    void 重制(bool b)
    { 
        if (b)
        {
            if (R == null) return;
            if (当前 == 假死) return;
            if (R.Re) to_state(活动);
            if (仅活着||当前==活动)    R.重制();
 
        }
    }
    //void 重制(int  场景,int 编号)
    //{
    //    if (DeBuG )  Debug.LogError(场景 + "     " + 编号+gameObject );
    //    if (场景==gameObject .scene .buildIndex &&编号== 所属相机编号)
    //    {
    //        if (R == null) return;
    //        if ( !R.Re) return; 
    //        to_state(活动);
    //        if (仅活着)
    //        {
    //            R.重制();
    //        }
    //    }
    //}
}
