using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Phy : MonoBehaviour, I_暂停
{ 
    [SerializeField]
    public bool 浮空   = false; 

    [SerializeField ] 
  Vector2 速度限制; 
    [SerializeField ]
    [DisplayOnly]
    Enemy_base b;
    [SerializeField ][DisplayOnly  ]
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
                if (Deb )    Debug.LogError(value + "                               "+transform .position+"     "+gameObject  );
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
            if (当前_ != value)
            {
                //Debug.LogError(当前 + "          BUUGGGGGBBBBBBBB " + value + gameObject);
            }

            当前_ = value;
        }
    }
    public void Goto_thisWay(Vector2 WoldPosition)
    {
        var 相对坐标 = WoldPosition - (Vector2)transform.position;
        Velocity = 相对坐标;
        if (Deb)
        {
            Debug.LogError("Goto_thisWay        " + 相对坐标);
        }
    }
    float 算力(float 总值)
    {
        return Mathf.Sqrt(总值 * 2 * G);
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
        b.GravityScale = 0;

        Velocity = Vector2.zero;
        if (速度限制==Vector2 .zero )   速度限制 = new Vector2(30, 10);
    }
    float G
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
                return Initialize_Mono  .I.假物理重力* b.重力增幅;
            }

        }
    }

    public bool Stop;
    [SerializeField]
    float 摩擦力 = 0.5f;
    public float Set_摩擦力(float value=-1f)
    {
        if (value>=0)
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
        if (!RR.Note_Re() )
        {
            II++;
            Debug.LogError("循环 次数   " +II+ target);
            if (II>5)
            {
                Debug.LogError("  循环次数    大于5"+target);

                return Vector2.zero;
            }
        }
        if (target == Vector2.zero) return target;
        var 中心点 = b.Bounds.center;
        //var 方向 = (中心点 - 中心点 + (Vector3)target).normalized;
        var 方向 = (中心点 - 中心点 + (Vector3)target) ;
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

            if (Deb) {
                Debug.LogError(a.collider.gameObject.name);
                a.point.DraClirl(1,Color.blue ,1f);
            }
         ///分析有碰撞后应该往哪里走
            Vector2 碰撞点相对位置 = Initialize. Get_获取碰撞距离(b.Bounds,a.point); ;
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
                return 碰撞预测( new Vector2 (target.x, 碰撞点相对位置.y)    );
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

            //if (碰撞点相对位置.magnitude <= 0.08f)
            //{ //   零距离  
            //    if (v.x == v.y)
            //    {
            //        var 距离 = (a.point - (Vector2)中心点).magnitude - 0.3f;
            //        return 方向 * 距离;
            //    }
            //    else if (v.x > v.y)
            //    {
            //        return new Vector2(碰撞点相对位置.x, v.y);
            //    }
            //    else if (v.y > v.x)
            //    {
            //        return new Vector2(v.x, 碰撞点相对位置.y);
            //    }
            //    else
            //    {
            //        return Vector2.zero;
            //    } 
            //}
            //else
            //{ //   有距离

            //    if (v.x==v.y)
            //    {
            //        var 距离 = (a.point - (Vector2)中心点).magnitude - 0.3f;
            //        return 方向 * 距离;
            //    }
            //    else if (v.x > v.y)
            //    { 
            //        return new Vector2(碰撞点相对位置.x, v.y);
            //    }
            //    else if (v.y > v.x)
            //    {
            //        return new Vector2(v.x, 碰撞点相对位置.y);
            //    }
            //    else
            //    {
            //        return  Vector2 .zero ;
            //    }
            //}
        }
        else
        {
            ///没碰到
            Debug.LogError("离谱情况出现了");
            return target;
        }
    }

    public float  Get_矢量长度()
    { 
        return 当前.magnitude ;
    }
    public Vector2 SafeVelocity
    {
        get => Velocity;
        set
        {
            if (Deb) if (value != Vector2.zero) Debug.LogError("Safe:       "+value + "                               ");
            Velocity = 碰撞预测(value);
        }
    }

    public bool 暂停 { get => 关闭1; set => 关闭1 = value; }

    int LastF;
    public void Stop_Velo()
    { 
        Velocity = Vector2.zero;
        当前 = Vector2.zero ; 
    }
 
    void 模拟()
    {

        if (Stop) return;
        if (Velocity != Vector2.zero)
        { ///外部进入   
            LastF = Time.frameCount;
            if (Velocity.y > 0)
            {///反向计算力度
               if(!浮空) Velocity = new Vector2(Velocity.x, 算力(Velocity.y));
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

        var V = (Vector3)当前*b.I_S .固定等级差 ;
        if (速度限制 != Vector2.zero)
        { 
            if (速度限制.x != 0) V .x = Mathf.Clamp(V.x, -速度限制.x, 速度限制.x);
            if (速度限制.y != 0) V.y = Mathf.Clamp(V.y, -速度限制.y, 速度限制.y);
        }

        b.transform.position += V*Time.fixedDeltaTime  ;

        ///重力个惯性
        var X = 当前.x;
        var Y = 当前.y;

        //没有飞行能力
        if (!浮空)
        { 
            if (!b.Ground || Y > 0)
            {
                ///空中起跳   加速度
                Y -= G * DeltaTime;
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
                if (Mathf.Abs(X) > 0.8f * b.I_S.固定等级差) X -= Mathf.Sign(X) * DeltaTime ; else X = 0;
                if (Mathf.Abs(Y) > 0.8f * b.I_S.固定等级差) Y -= Mathf.Sign(Y) * DeltaTime  ; else Y = 0;

                //if (Mathf.Abs(X) > 0.8f * b.I_S.固定等级差)  X -= Mathf.Sign(X) * DeltaTime * 摩擦力;  else    X = 0;
                //if (Mathf.Abs(Y) > 0.8f * b.I_S.固定等级差) Y -= Mathf.Sign(Y) * DeltaTime * 摩擦力; else Y = 0; 
            } 
        }        // 有飞行能力（无重力）




        //if (!b.Ground && Y < 0 && Y > -0.1f)
        {  ///测量最高点
            //De();
        }
 
        当前 = new Vector2(X, Y);
        var a = b.Get_rb();
        if (a!=null)a.velocity = Vector2.zero;

        Velocity = Vector2.zero;
    }

 

  public   bool Deb;
    private void FixedUpdate()
    {
 
        if (关闭1 || b.I_S.限制)
        {

            return;
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
            transform.position =new Vector2(transform.position.x, transform.position.y+0.1f);
        }
 
        else
        {
            模拟();
        } 
    }
    private void Update()
    {
        if (Key != KeyCode.None)
        {
            if (Input.GetKeyDown(Key))
            {
                Velocity = 测试速度;
            }
        }
        Speedd = b.I_S.固定等级差;
    }
    [SerializeField ][DisplayOnly ]
    float Speedd;
    private void OnDisable()
    {
        当前 = Vector2.zero;
        Velocity = Vector2.zero;
    }


    [SerializeField] KeyCode Key;
    [SerializeField] Vector2 测试速度;
    [SerializeField]
    [DisplayOnly]
    private bool 关闭1;
}
