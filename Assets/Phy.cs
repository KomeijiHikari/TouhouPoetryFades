using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class Phy : MonoBehaviour, I_暂停, I_Speed_Change, I_M_Ridbody2D
{
    public bool Staticrb { get =>false; }
    [SerializeField]
    public bool 浮空 = false;

    [SerializeField]
    Vector2 速度限制;
    [SerializeField]
    [DisplayOnly]
    I_M_Ridbody2D b;
    [SerializeField]
    [DisplayOnly]
    Vector2 Velocity_;

 
    public Vector2 Velocity
    {
        get
        {
            return Velocity_;
        } 
       set
        {
            if (Velocity_ != value)
            {
                if (true)
                {

                }
                if (Deb) Debug.LogError(value + "                               " + transform.position + "     " + gameObject);
                Velocity_ = value;
            }
        }
    }
    [DisplayOnly]
    [SerializeField]
    Vector2 当前_;
    public Vector2 当前
    {
        get { return 当前_; }
        private set
        {
            if (当前_ != value&&Deb)     Debug.LogError(当前 + "          BUUGGGGGBBBBBBBB " + value + gameObject); 
            当前_ = value;
        }
    }
    public void Goto_thisWay(Vector2 WoldPosition)
    {
        if (b.Staticrb)
        {
            Debug.Log(gameObject.name + transform.position + "是不动物体");
            return;
        }
  
        var 相对坐标 = WoldPosition - (Vector2)transform.position;
        Velocity = 相对坐标;
        if (Deb)
        {
            Debug.LogError("Goto_thisWay        " + 相对坐标);
        }
    }

    ///一秒后 一共移动“总值”  返回初始速度
    float 反向Y(float 总值,float tim=1)
    {
        //   总位移 = 初始速度 * ti - 0.5XGXtiXti
        //   (总位移+0.5XGXtiXti)/ti=初始速度 
        return 总值 / tim + 0.5f * G* tim;
        //return Mathf.Sqrt(总值 * 2 * G);
    }
    float DeltaTime
    {
        get
        {
            return Time.fixedDeltaTime * b.I_S.固定等级差;
        }
    }
    private void Awake()
    {

        b = GetComponent<Enemy_base>();
        if (b==null)
        {
            b =  this;
        }

        Velocity = Vector2.zero;
        if (速度限制 == Vector2.zero) 速度限制 = new Vector2(30, 10);
    }
  public   float G
    {
        get
        {
            if (浮空) return 0;
            if (b.重力增幅 == 0)
            {
                return Initialize_Mono.I.假物理重力;
            }
            else
            {
                return Initialize_Mono.I.假物理重力 * b.重力增幅;
            }

        }
    }

    public bool Stop;
    [SerializeField]
    float 摩擦力 = 0.5f;
    public float Set_摩擦力(float value = -1f)
    {
        if (value >= 0)
        {
            摩擦力 = value;
        }
        return 摩擦力;
    }

    No_Re RR = new No_Re();
    int II;

    public Vector2 碰撞预测(Vector2 target)
    ///碰撞点， 之间 的距离   返回方向*距离
    {
        if (!RR.Note_Re())
        {
            II++;
            //Debug.LogError("循环 次数   " + II + target);
            if (II > 5)
            {
                Debug.LogError("  循环次数    大于5" + target);

                return Vector2.zero;
            }
        }
        if (target == Vector2.zero) return target;
        Vector3 中心点 = b.Bounds.center;
        //var 方向 = (中心点 - 中心点 + (Vector3)target).normalized;
        var 方向 = (中心点 - 中心点 + (Vector3)target);
        方向.Normalize();

        var a = Physics2D.BoxCast(
            中心点,
            b.Bounds.size - new Vector3(0.05f, 0.05f),
            0,
            方向,
            target.magnitude * DeltaTime,
            b.碰撞检测层
            );
        if (a.collider != null)
        {///有碰撞

            if (Deb)
            {
                Debug.LogError(a.collider.gameObject.name);
                a.point.DraClirl(1, Color.blue, 1f);
            }
            ///分析有碰撞后应该往哪里走
            Vector2 碰撞点相对位置 = Initialize.Get_获取碰撞距离(b.Bounds, a.point); ;
            if (碰撞点相对位置.magnitude <= 0.08f)
            {
                碰撞点相对位置 = Vector2.zero;
            }

            if (target.x == target.y)
            {
                var 距离 = (a.point - (Vector2)中心点).magnitude - 0.3f;
                Debug.LogError("走这里");
                return 方向 * 距离;
            }
            else if (target.x > target.y)
            {
                Debug.LogError("走这里");
                return 碰撞预测(new Vector2(target.x, 碰撞点相对位置.y));
            }
            else if (target.y > target.x)
            {
                Debug.LogError("走这里");
                return 碰撞预测(new Vector2(碰撞点相对位置.x, target.y));
            }
            else
            {
                Debug.LogError("走这里");
                return Vector2.zero;
            }
        }
        else
        {
            ///没碰到
            Debug.LogError("离谱情况出现了");
            return target;
        }
    }

    public float Get_矢量长度()
    {
        return 当前.magnitude;
    }
    public Vector2 SafeVelocity
    {
        get => Velocity;
        set
        {
            if (b.Staticrb) {
                Debug.Log(gameObject .name +transform.position+"是不动物体");
                return;
            }
                if (Deb) if (value != Vector2.zero) Debug.LogError("Safe:       " + value + "                               ");
            Velocity = 碰撞预测(value);
        }
    }

    public bool 暂停 { get => 关闭1; set => 关闭1 = value; }

    int LastF;
    public void Stop_Velo()
    {
        Velocity = Vector2.zero;
        当前 = Vector2.zero;
    }
    private void OnEnable()
    {
        Stop_Velo();
    }

    public Vector2 碰撞预测2(Vector2 target)
    {
        if (target == Vector2.zero) return target;
        Vector3 中心点 = b.Bounds.center;
        var 方向 = (中心点 - 中心点 + (Vector3)target);
        方向.Normalize();

        var a = Physics2D.BoxCast(
         中心点,
         b.Bounds.size - new Vector3(0.05f, 0.05f),
         0,
         方向,
         target.magnitude * DeltaTime,
         b.碰撞检测层
         );
         
        return Initialize.Get碰撞Position(b.Bounds, a);
    }


    public float ZZZZZZZ;
    void 模拟()
    { 
       
        if (Stop) return;
        if (Velocity != Vector2.zero)
        { ///外部进入   
            LastF = Time.frameCount;
            if (Velocity.y > 0)
            {///反向计算力度
                if (!浮空) Velocity = new Vector2(Velocity.x, 反向Y(Velocity.y));
            }

            if (Velocity.x == 0 && Velocity.y != 0 && 当前.x != 0)
            {///起跳后保留惯性
                Velocity = new Vector2(当前.x, Velocity.y);
            } 
            //当前 = 动态速度限制 ? 碰撞预测(Velocity) : Velocity;
            当前 = Velocity;

            //Initialize_Mono.I.Waite(() => De(), 1f);
        }


        //if (b.I_S.Speed > 8||  Mathf .Abs(b.I_S.Speed * 当前.y)>=40)
        //当前 = 碰撞预测(当前);    
      var a=  碰撞预测2(当前);

        if (a==Vector2.zero)
        {
            ZZZZZZZ = Initialize_Mono.I.GetMin(b.I_S.固定等级差);
            var V = (Vector3)当前 * ZZZZZZZ; 

            b.transform.position += V * Time.fixedDeltaTime;
        }
        else
        {
                        Stop_Velo();

            Vector3 ca = b.Bounds.center - (Vector3)a;
            b.transform.position -= ca;
        }



        //if (Deb) Debug.LogError("aaaaaaaaa               a");
        ///重力个惯性
        var X = 当前.x;
        var Y = 当前.y;

        //没有飞行能力
        if (!浮空)
        {
            if (!b.Ground || Y > 0)
            {
                ///空中起跳   加速度
                Y  = Y - G * DeltaTime;
            }
            else if (Y < 0 && b.Ground)
            {

                Y = 0;
                //落地一瞬间归零 
            }
            else if (Velocity == Vector2.zero && b.Ground)           ///Fix  同一帧内运行第二次后    速度已经赋值为0  判断开始减速      
            { ///he V elo重叠帧不运行

                if (LastF != Time.frameCount)
                {
                    if (Y == 0)
                    {
                        ///摩擦力
                        if (Mathf.Abs(X) > 0.8f * b.I_S.固定等级差)
                        {
                            X -= Mathf.Sign(X) * DeltaTime * 摩擦力;
                        }
                        else
                        {
                            X = 0;
                        }
                    }
                }
                //在地上并且当前FIX没有外部力
            }
        }
        else
        {
            if (b.Ground)
            {

            }
            else if (LastF != Time.frameCount)
            {
                if (Mathf.Abs(X) > 0.8f * b.I_S.固定等级差) X -= Mathf.Sign(X) * DeltaTime; else X = 0;
                if (Mathf.Abs(Y) > 0.8f * b.I_S.固定等级差) Y -= Mathf.Sign(Y) * DeltaTime; else Y = 0;

                //if (Mathf.Abs(X) > 0.8f * b.I_S.固定等级差)  X -= Mathf.Sign(X) * DeltaTime * 摩擦力;  else    X = 0;
                //if (Mathf.Abs(Y) > 0.8f * b.I_S.固定等级差) Y -= Mathf.Sign(Y) * DeltaTime * 摩擦力; else Y = 0; 
            }
        }       
        // 有飞行能力（无重力）
         
        //if (!b.Ground && Y < 0 && Y > -0.1f)
        {  ///测量最高点
            //De();
        }

        当前 = new Vector2(X, Y);
        var aR = b.Get_rb();
        if (aR != null) aR.velocity = Vector2.zero;

        Velocity = Vector2.zero; 
    } 
    public bool Deb;
    [SerializeField]
    SpriteRenderer Sp; 
    private void FixedUpdate()
    { 
        if (关闭1 || b.I_S.限制)
        { 
            return;
        }

        if (Sp!=null )
        {
            Sp.transform.rotation = Quaternion.Euler( Initialize .Z1*Time.fixedTime*1000f);
        }
        ///脚底在地面下面
        var a = Physics2D.Raycast(
            new Vector2(b.Bounds.min.x + 0.01f, b.Bounds.min.y + 0.01f),
            Vector2.right,
            b.Bounds.size.x - 0.02f,
            b.碰撞检测层
            ).collider != null;
        if (a)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.1f);
        } 
        else
        { 
            模拟();
        } 
        if (b==(I_M_Ridbody2D)this&&地面!=null)
        {
            //ground = 地面.遇见了;
        }
    }
    [Button("测一下", ButtonSizes.Large)]
    public void 目标炮(Vector3 Target,float tim)
    {
        if (Ground) Ground = false;
        var a= Initialize.抛物线_Get矢量(Target - transform .position,tim,G );
        Initialize_Mono.I.Waite_同速(
            () => { transform.position.DraClirl(); }
            , tim
            );
        transform.position.DraClirl();
        Set_RealVelo(a);
    }
    [Button("测一下", ButtonSizes.Large)]
    public void 测一下(Vector2 测试速度)
    {
        if (Ground)  Ground = false;
 
        var Ti = 1;
        Velocity = 测试速度; 

        Vector2 预测 = transform.position + (Vector3)测试速度;
        预测.DraClirl(0.5f,Color .blue, Ti+1); 
        Initialize_Mono.I.Waite(
            () => { ((Vector2)transform.position).DraClirl(1,Color .red ,1f); },Ti
            );
    } 
    [Button("抛物线", ButtonSizes.Large)]
    public void 抛物线(Vector2 发射方向, Transform t)
    {  
        发射方向.Normalize();
        var 差 = transform.position - t.position;
       当前= 发射方向*  Initialize . 抛物线_Get力(发射方向, t.position,G );
    }
    public void 抛物线(Vector2 发射方向, Vector3 t)
    {
        发射方向.Normalize();
        var 差 = t-transform.position    ;
        Stop_Velo(); 

        if (发射方向.x! * 差.x < 0) 发射方向 = new Vector2(-发射方向.x, 发射方向.y);

        当前 =  发射方向  * Initialize.抛物线_Get力(发射方向, 差, G); 

    }
    public void Set_RealVelo(Vector2 V)
    {
        Stop_Velo();
        当前 = V;
    }
    private void Update()
    { 
        Speedd = b.I_S.固定等级差;
    }
    [SerializeField]
    [DisplayOnly]
    float Speedd;
    private void OnDisable()
    {
        当前 = Vector2.zero;
        Velocity = Vector2.zero;
    }

     [SerializeField]
    [DisplayOnly]
    private bool 关闭1; 

     
    [Space( )] 
    [Header("假装物理")]
    public float 重力增幅1;
    public  bool ground;
    public I_Speed_Change i_S;

    [Header("碰撞计算")]
    public LayerMask 碰撞检测层1;
    [SerializeField]
    float current_Speed_LV;
    [SerializeField]
    float speed_Lv;

    public I_Speed_Change I_S { get => (I_Speed_Change)this; }
    public LayerMask 碰撞检测层 => 碰撞检测层1;
   public new Transform transform => base.transform ;
  public   bool Ground { get => ground; set => ground = value; }

    [SerializeField] Phy_检测 地面;
    public GameObject 对象 => gameObject ;
    public float 重力增幅 { get => 重力增幅1; set => 重力增幅1 = value; }
    public System.Action 变速触发 { get ; set ; }

  public Bounds Bounds { 
        get {
            if (s != null) return s.bounds;
            else return bc.bounds; 
        }
    } 

    [SerializeField] SpriteRenderer s;
    [SerializeField] BoxCollider2D bc;

    public float Current_Speed_LV => current_Speed_LV; 
    public float Speed_Lv { get => speed_Lv; set => speed_Lv=value; } 
  public   Rigidbody2D Get_rb() { return null; }
}
interface I_M_Ridbody2D
{
    bool Staticrb { get; }
    I_Speed_Change I_S { get; }
    LayerMask 碰撞检测层 { get; }
    Bounds Bounds { get; }
    float 重力增幅 { get; set; }
    Transform transform { get; }
    bool Ground { get; set; }
    Rigidbody2D Get_rb();
}