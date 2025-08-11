using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 摄像机 : MonoBehaviour
{
    public static Vector2 to_屏幕坐标(Bounds b, Vector2 va)
    { 
        // 计算Bounds的左下角和右上角坐标  
        Vector2 bottomLeft = (Vector2)b.center - new Vector2(b.size.x / 2, b.size.y / 2);
        Vector2 topRight = (Vector2)b.center + new Vector2(b.size.x / 2, b.size.y / 2);

        // 检查点va是否在Bounds内  
        if (va.x >= bottomLeft.x && va.x <= topRight.x && va.y >= bottomLeft.y && va.y <= topRight.y)
        {
            // 计算va在Bounds内的相对坐标（0到1之间）  
            Vector2 relativePosition = new Vector2(
                (va.x - bottomLeft.x) / b.size.x,
                (va.y - bottomLeft.y) / b.size.y
            );
            return relativePosition;
        }
        else
        {
            // 抛出异常，因为点va不在Bounds内  
            Debug.LogError("在屏幕外");
            return new Vector2(99,99);
        }
    }
    public static 摄像机 I { get; private set; }
     
    public Vector2  返回对应屏幕尺寸(float 尺寸)
    {
        return new Vector2(尺寸 * Initialize.屏幕横纵比 , 尺寸) * 2f;
    }
    public   Bounds Camera_Bounds
    {
        get
        {
            return new Bounds(Camera.main.transform.position, 返回对应屏幕尺寸(Fov ));
        }
    }
    /// <summary>
    /// 当16单位      宽为1 时，fov为0.564
    /// </summary>
    public    float 完美 { get; } = 0.564f;
    public float Y
    {
        get { return  Fov   * 2; }
    }
    public float X
    {
        get { return Fov * Initialize.屏幕横纵比 * 2; }
    }

    public float 全局默认Fov { get => 原Fov1; set => 原Fov1 = value; }

    [SerializeField]
    CinemachineVirtualCamera 默认;
public CinemachineVirtualCamera c { get=> 默认; private set => 默认=value; }
    [DisplayOnly]
    [SerializeField]
    public  CinemachineConfiner2D 碰撞组件;

  public   float Fov {  get { return c.m_Lens.OrthographicSize; }
       private  set { c.m_Lens.OrthographicSize = value; }
    }
 public void FOV_直接至(float  f)
    {
        Fov = f;
    }


    public  void FOV_缓动至( float FOV,float time, bool 用直线 = false)
    {
        if (Coroutine_缓动 != null)
            StopCoroutine(Coroutine_缓动);
 
            Coroutine_缓动 = StartCoroutine(IE_FOV(FOV, time ));
 
    }
    IEnumerator IE_FOV2(float FOV, float time, bool 用直线 = false,float time2=0 )
    {
        float TargetFov = (FOV > 当前场景真正最大FOV) ? 当前场景真正最大FOV : FOV;
        float Enter_tim = Time.realtimeSinceStartup;
        float now_time = Time.realtimeSinceStartup;
        float 分子 = 0;
        if (用直线)
        {
            float 分母 = time / 0.04f;
            分子 = (FOV - Fov) / 分母;
        }
        while (time + Enter_tim > now_time)  //第一次进入 10+1大于10
        {
            ///时间内循环
            now_time = Time.realtimeSinceStartup;  //      NOW增量
            if (用直线)
            {
                Fov += 分子;
            }
            else
            {
                if (Mathf.Abs(Fov - TargetFov) > 0.01)
                {
                    Fov = Mathf.Lerp(Fov, TargetFov, 0.05f);
                }
            }
            yield return new WaitForFixedUpdate();
        }
        if (time2!=0)
        {
            while (time + Enter_tim > now_time)  //第一次进入 10+1大于10
            {

            }
     }
        Debug.LogError("完成");
        Coroutine_缓动 = null;
    }

    IEnumerator IE_FOV(float FOV, float time,bool 用直线=false, float time2 = 0)
    {
        //Debug.LogError(" 目标" + FOV + "         时间1   " + time + "      时间2       " + time2 + "            " + "            ");
        float 接近值 = 0.01f;

        float TargetFov = (FOV > 当前场景真正最大FOV) ? 当前场景真正最大FOV : FOV; 
        float Enter_tim = Time.realtimeSinceStartup;
        float now_time = Time.realtimeSinceStartup;

        float 迭代时间 = Time.fixedDeltaTime;
        float 分母次数 = time / 迭代时间;
        float f等距距离 = Mathf .Abs((FOV - Fov)) / 分母次数;
        float 插值 = 0;

      

        if (!用直线)
        {  
            插值 = Initialize.Lerp均衡插值( 接近值 , (int)分母次数);
        } 

        while (time + Enter_tim > now_time)  //第一次进入 10+1大于10
        {
            ///时间内循环
            now_time = Time.realtimeSinceStartup;  //      NOW增量
            if (用直线)
            {
                Fov += f等距距离;
            }
            else
            {
 
                    Fov = Mathf.Lerp(Fov, TargetFov, 插值);
 //Initialize_Mono .I.
            }
            yield return new WaitForFixedUpdate();
        }

        if (time2!=0)
        {
            TargetFov = 当前场景默认FOV;
  Enter_tim = Time.realtimeSinceStartup;
  now_time = Time.realtimeSinceStartup;

                迭代时间 = Time.fixedDeltaTime; 
            分母次数 = time2 / 迭代时间;
                  f等距距离 = Mathf.Abs((FOV - Fov)) / 分母次数;
                 插值 = 0;
            if (!用直线)
            { 
                插值 = Initialize.Lerp均衡插值(接近值, (int)分母次数); 
            }
            while (time2 + Enter_tim > now_time)  //第二次
            {
                ///时间内循环
                now_time = Time.realtimeSinceStartup;  //      NOW增量
                if (用直线)
                {
                    Fov += f等距距离;
                }
                else
                {
                    //Debug.LogError(TargetFov+"      "+ 插值);
                    Fov = Mathf.Lerp(Fov, TargetFov, 插值); 
                }
                yield return new WaitForSecondsRealtime(迭代时间) ;
            }
        } 
        Coroutine_缓动 = null;
    }
    public   void FOV_还原(  float time =1, bool 用直线 = false)
    {
        if (Coroutine_缓动 != null)
            StopCoroutine(Coroutine_缓动);

        Coroutine_缓动 = StartCoroutine(IE_FOV(全局默认Fov, time, 用直线)); 
    }
    Coroutine Coroutine_缓动 { get;set; }
    /// <summary>
    /// F           进入     退出  直线
    /// </summary>
    /// <param name="FOV"></param>
    /// <param name="time"></param>
    /// <param name="time1"></param>
    /// <param name="用直线"></param>
    public  void FOV_缩放并且还原(float FOV, float time,   float time1=0   , bool 用直线 = false)
    {
        if (Coroutine_缓动 == null)
        { 
            Coroutine_缓动 = StartCoroutine(IE_FOV(FOV, time,false, time1));
        } 
    }
    private void Awake()
    {
        if (I != null)
        {
            //已经有实例

            Destroy(gameObject); 
        }
        else
        {
            I = this; 
        }
        Brain = GetComponent<CinemachineBrain>();
     碰撞组件 = c.gameObject.   GetComponent<CinemachineConfiner2D>();
        全局默认Fov = Fov;
        C_k = null;
    }

    float Target_Fov;
    public void Set_Target_Fov(float FovValue)
    {
        Target_Fov = FovValue; 
    }
    private void Start()
    {
        //c = GetComponent<CinemachineVirtualCamera>();
        if (c.m_Follow == null)
        {
            设置相机跟随(GameObject .FindGameObjectWithTag    ("Player"));
        }

        //Initialize_Mono.I.Waite(() => FOV_缩放并且还原(5, 3, 1), 2f);
        //EventManager.I.Invoke(EventManager.切换场景触发, gameObject);
    }  
   public  void 设置摄像机位置(Vector3 v)
    {
        transform.position = new Vector3(v.x,v.y,-10);
    }
    [DisplayOnly]
    public float 当前场景真正最大FOV;
    public float 当前场景默认FOV;
    public Collider2D 当前碰撞框
    {
        get
        { 
                if (碰撞组件.m_BoundingShape2D==null)  return null; 

            return 碰撞组件.m_BoundingShape2D ;
        }
    }

    [SerializeField]
    [DisplayOnly]
    相机框 C_k;
  public   void 刷新碰撞框(相机框  k)
    {
        if (C_k!=null)
        {
            //Debug.LogError(C_k.编号 + "关闭了" );
            C_k.是我否 = false;
        }
        C_k = k;
        //Debug.LogError(C_k.编号 + "打开了");
        C_k.是我否 = true;
    }
 
    public void 设置相机碰撞体(PolygonCollider2D po)
    { 
        var a = po.GetComponent<相机框>(); 
        摄像机.I.刷新碰撞框(a);
        if (c == null)
        {
            c = GetComponent<CinemachineVirtualCamera>();
            Debug.LogError("看样子是重新获取了");
        }
        if (碰撞组件==null)
        {
            碰撞组件= c.gameObject .GetComponent<CinemachineConfiner2D>();
        }
 
          碰撞组件.InvalidateCache();
        碰撞组件.m_BoundingShape2D = po;
        碰撞组件.InvalidateCache();

        当前场景真正最大FOV = Initialize.返回兼容相机碰撞框的摄像机尺寸(碰撞组件.m_BoundingShape2D.bounds.size, 1000);


        float Result_Fov = Target_Fov;
        ///有没有目标
        if (Result_Fov == 0) 当前场景默认FOV = 全局默认Fov;
        else 当前场景默认FOV = Result_Fov;

        ///允不允许
        if (当前场景真正最大FOV > 当前场景默认FOV) Result_Fov = 当前场景默认FOV;
        else Result_Fov = 当前场景真正最大FOV;
        Fov = Result_Fov;
        Target_Fov = 0;


        //if (Target_Fov!=0)
        //{
        //    Debug.LogError(Target_Fov+"          见鬼   ");
        //    if (当前场景真正最大FOV > Target_Fov)
        //    {
        //        Result_Fov = Target_Fov;
        //    }
        //    else
        //    {
        //        Result_Fov = 当前场景真正最大FOV;
        //    }
        //}
        //else
        //{
        //    Debug.LogError(当前场景真正最大FOV+ "       "+ 全局默认Fov + "  全局默认Fov全局默认Fov全局默认Fov全局默认Fov     ");
        //    if (当前场景真正最大FOV < 全局默认Fov)
        //    {
        //        Result_Fov = 当前场景真正最大FOV;
        //    }
        //    else
        //    {
        //        Result_Fov = 全局默认Fov; 
        //    }
        //}
        //Fov = Result_Fov;
        //当前场景默认FOV = Result_Fov;
        //Target_Fov = 0;
        /////原来的和现在的
        ///////返回为0说明现在的更大
        //var b = Initialize.返回兼容相机碰撞框的摄像机尺寸(碰撞组件.m_BoundingShape2D.bounds.size, 全局默认Fov);
        //if (b != 0)
        //{
        //    Fov = b;
        //    当前场景默认FOV = b;
        //}
        //else
        //{
        //    Fov = 全局默认Fov;
        //    当前场景默认FOV = 全局默认Fov;
        //}
    }
    public void 设置相机跟随(GameObject  p)
    {
        if (c == null) { 
        c = GetComponent<CinemachineVirtualCamera>();
            Debug.LogError("看样子是重新获取了");
        }
 
        设置摄像机位置(p.transform.position);
                c.PreviousStateIsValid = false; 
                c.Follow = p.transform; 
    }
 
    [DisplayOnly]
    [SerializeField ]
    private float 原Fov1;

    [DisplayOnly]
    [SerializeField]
    public Vector2  C_Size ;
    public CinemachineBrain Brain {  get; private set; } 
}
