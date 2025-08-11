using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///  一种碰到了停下来
/// pla 是不是自己     决定离开后是继续之前进度还是当前进度
///  踩上去之后是否要停止移动动
/// 
/// ，离开继续 原来的位置      pla 不是自己pla 是个幽灵
/// 一种碰到了停下来，离开继续当前位置变换   pla是自己
///  一种碰到了停下来， 踩上去之后停止移动
///  
/// 当速度过快，玩家触碰会被摊开并且受到伤害    并且踩不上去
/// </summary>
public partial  class Move_P : Groundbase, I_Speed_Change,I_攻击, I_暂停
{
    public GameObject 对象 { get => gameObject; }
    public One_way O;
    public bool 超速_;
    public System.Action 变速触发 { get; set; }
    public enum 方式
    {
        竖直,
        水平,
        自由
    } 
    [SerializeField]
public     方式 移动方式;

    [SerializeField ][DisplayOnly]
    Vector3 a;
    [SerializeField]
    [DisplayOnly]
    Vector3 b;

  [SerializeField ]  Transform Pla;
    public Transform A;
    public Transform moveto;
    [SerializeField] float Self_speed = 1;

    [SerializeField] bool   Loop= true;

 
    [SerializeField]
    float speed_Lv;
    public float Speed_Lv { get=> speed_Lv; set => speed_Lv=value; }
    public float Current_Speed_LV { get => speed_Lv; }
     
 public SpriteRenderer sp { get; private set; }

    [SerializeField]
    float WaitTime=0.2f;

    [SerializeField]
    float WaitTime2=0.2f;

    [SerializeField]
    bool 可以移动=true;

    [SerializeField]
    bool 踩到之后不动 =false;

    [Space ]
    [SerializeField ][DisplayOnly]
    Vector3 next;
    Vector3 next_ { get { 
            return next; } set { 
            next = value; } }

    public float atkvalue { get => atkvalue; set => atkvalue=value; }

    public void 重新设置点位()
    {
        bool 改AA = false, 改BB=false ;
        if (next_ == a)
        {
            改AA = true;
        }
        else if (next_ == b)
        {
            改BB= true;
        }
        else
        {
            Debug.LogError("离谱");
        }
        a = A.localPosition;
        b = moveto.localPosition;
        if (改AA )
        {
            next_ = a;
        }
        else if (改BB)
        {
            next_ = b;
        }
    }
    private void Start()
    {
        Player3.I.Public_Speed_ += () => {
            if (Player3.Public_Const_Speed > speed_Lv) 重制();
        };
        if (speed_Lv==0)
        {
            speed_Lv = 1;
        }
 

        a = A == null? Pla.localPosition:A.localPosition;
        b = moveto.localPosition;
        next_ =过滤(a) ;
        gameObject.tag = Initialize.MovePlatform;
        gameObject.layer= Initialize.L_M_Ground; 
    }
    [DisplayOnly]
    [SerializeField ]
    Material m;
 
    float Speed_原先;
    public void Set_LV(float LV)
    {
        Speed_原先 = Speed_Lv;
          Speed_Lv = LV;
    }
    public void Re_LV( )
    {
        if (Speed_原先==0)
        {
            Debug.LogError("离谱          离谱      离谱      离谱");
        }
        Speed_Lv = Speed_原先;
    }
    private new void Awake()
    {
        gameObject.AddComponent<MonoMager >();
        StatWay = transform.position;
        base.Awake();
        O = GetComponent<One_way >();
        m = GetComponent<SpriteRenderer>().material;
        sp = GetComponent<SpriteRenderer>();

    var a=    GetComponent<生命周期管理 >() ;
        if (a != null) a.仅活着 = true;

        if (Speed_Lv == 0)
        {
            Speed_Lv = 1;
        }
            if (Self_speed ==1)
        {
            Self_speed = 1;
        }
         transform.localPosition = 过滤(transform.localPosition); 
    }
    
    float LastPlayerLV;
    float LastLV;
    void 碰撞情况刷新()
    {
        if (LastPlayerLV!=Player3.Public_Const_Speed )
        {
            LastPlayerLV = Player3.Public_Const_Speed;
            刷刷新();
        }
        if (LastLV!=speed_Lv)
        {
            LastLV = speed_Lv;
            刷刷新();
        }
        if (!超速&& O!=null )
        {
            bc.isTrigger = O .应该无视;
        }
    }
    void 刷刷新()
    {
        bc.isTrigger = 超速;
    }
    void move()
    {

      if( !I_S. 限制)   Pla.localPosition = Vector3.MoveTowards(Pla.localPosition, next_,   帧移动距离);
        if (Vector3.Distance(Pla.localPosition, next_) <= 0.1) change();  
    }
    public int 方向
    {
        get
        {
            return     (int)Initialize.返回和对方相反方向的标准力(Pla.localPosition,next_) .x         ;
        }
    }
    /// <summary>
    ///      过快，会闪动 
    ///      Update 里  Player 抵消，PlayerForce里会卡
    /// </summary>
   public float 帧移动距离
    {
        get
        {
            return (I_S.固定等级差) * Time.fixedDeltaTime*Self_speed;
        }
    }
    IEnumerator  Waite(float time)
    {
        while (time>0)
        {
            time -= Time.fixedDeltaTime* I_S.固定等级差 * Self_speed;

            yield return new WaitForFixedUpdate( );
        }
 
        可以移动 = true;
    }
    void change()
    {
        if (Loop)
        { 
            if (next_==a)
        {
            可以移动 = false;
                if (WaitTime!=999) StartCoroutine(Waite(WaitTime));
            next_ =过滤(b)   ; 
        }
        else
        {
            可以移动 = false;
                if (WaitTime2 != 999) StartCoroutine(Waite(WaitTime2));
                next_ = 过滤(a);
            } 
        }
        else
        {
            可以移动 = false;   
            Pla.localPosition = 过滤(b);
            if (WaitTime != 999) StartCoroutine(Waite(WaitTime));
        }
    }
    bool 因为玩家我不该动;

    float time; 
 
    Vector2 过滤(Vector2 ini )
    {
        switch (移动方式)
        {
            case 方式.竖直:
                return new Vector3(0, ini.y, 0);
            case 方式.水平:
                return new Vector3(ini.x, 0, 0);
            case 方式.自由:
                return ini;
            default:
                return Vector2.zero;
        }
    }
 


    private void OnTriggerStay2D(Collider2D c )
    {
        bool 碰到的是玩家 = c.gameObject.CompareTag(Initialize.Player);
        if (碰到的是玩家&&超速)
        {
            平台伤害();
        }
    }
    void 平台伤害()
    {
        Player3.I.To_SafeWay();
        Player3.I.被扣血(I_S.固定等级差, gameObject, 0);
    }
    private void OnCollisionEnter2D(Collision2D c)
    {
        //if (gameObject.layer!=Initialize .L_Ground) return;
         
        bool 碰到的是玩家 = c.collider ==Player3.I.po;
        if (碰到的是玩家) 
        { 
            bool 碰到的是上面 = Initialize.Vector2Int比较(c.contacts[0].normal,Vector2 .down); 
 
                if ( 碰到的是上面)
                { 
                    if (踩到之后不动)
                    { 
                        因为玩家我不该动 = true;
                    }
                    else
                {
                    Debug.LogError(Initialize.Vector2Int比较(c.contacts[0].normal, Vector2.down, true));
                    Player3.I.脚下 = this;
                    Player3.I.ChangeFather(transform );
                    }
                } 
             
        } 
    }
    [SerializeField ][DisplayOnly ]
    float 帧移动距离_;
    private void Update()
    {
        if (暂停) return;
        碰撞情况刷新();
        帧移动距离_ = 帧移动距离;
        超速_ = 超速;
    }
    private void OnCollisionExit2D(Collision2D collision)
    { 
        if (collision.gameObject.CompareTag(Initialize.Player))
        {
        因为玩家我不该动 = false;

            Player3.I.脚下 =null;
            Player3.I.ChangeFather();
        }
    } 

    private void FixedUpdate()
    {
        if (暂停) return;
        if (Pla==transform )
        {
            if (因为玩家我不该动) return;
            if (!可以移动) return;
            move();
        }
        else
        {
            move();
            if (因为玩家我不该动) return;
            if (!可以移动) return;
            transform.position = Pla.position;//同步
        } 
    }


}
public partial class Move_P : I_Revive,I_假死
{
  
    public void 扣攻击(float i)
    {
        throw new System.NotImplementedException();
    }
    public bool 超速 { get { return I_S.固定等级差 > Initialize_Mono.I.阀值; } }

    public I_Speed_Change I_S { get => (I_Speed_Change)this; }
  [SerializeField ][DisplayOnly ]
    private bool 暂停1;
    public bool 暂停 { get => 暂停1; set => 暂停1 = value; }
    public Bounds 盒子 { get => sp.bounds; }
    public bool Re { get; set; } = true;
    public float Re_Time { get; set; }

    Vector2 StatWay;

    public void Event_重制()
    {
        重制();
    }
    public bool 重制()
    {
        transform.position = StatWay;
        return true;
    }

    public void 假死了(bool 假死不)
    {
        if (假死不)
        {
            sp.enabled = false;
            bc.enabled = false;
        }
        else
        {
            sp.enabled = true;
            bc.enabled = true;
        }
    }
}