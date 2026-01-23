using Cinemachine;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor; 
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using static BiologyBase;
using static 生命周期管理;

interface 打印消息
{
    //背包菜单左右切换背包
    public bool 状态消息 { get; set; }
}

 
/// <summary>
/// 设置属性只读
/// </summary>
public class DisplayOnly : PropertyAttribute
{

} 
public static class Debug_
{
  public   enum E_DebugState
    {
        None,
        Deb,
        Count,
    }
static    Debug_.E_DebugState e_DebugState => Initialize_Mono.I.e_DebugState;

    public static void LogError(object s, UnityEngine.Object c = null)
    {
        switch (e_DebugState)
        {
            case E_DebugState.None:
                return;
            case E_DebugState.Count:
                if (c == null)
                {
                    Debug.LogError($"[{Time.frameCount}] {s}");
                }
                else
                {
                    Debug.LogError($"[{Time.frameCount}] {s}", c);
                }
                return;
            case E_DebugState.Deb:
            default:
                if (c == null)
                {
                    Debug.LogError(s);
                }
                else
                {
                    Debug.LogError(s, c);
                }
                return;
        }
    }

    // 统一的日志警告方法
    public static void LogWarning(object s, UnityEngine.Object c = null)
    {
        switch (e_DebugState)
        {
            case E_DebugState.None:
                return;
            case E_DebugState.Count:
                if (c == null)
                {
                    Debug.LogWarning($"[{Time.frameCount}] {s}");
                }
                else
                {
                    Debug.LogWarning($"[{Time.frameCount}] {s}", c);
                }
                return;
            case E_DebugState.Deb:
            default:
                if (c == null)
                {
                    Debug.LogWarning(s);
                }
                else
                {
                    Debug.LogWarning(s, c);
                }
                return;
        }
    }

    // 统一的日志方法
    public static void Log(object s, UnityEngine.Object c = null)
    {
        switch (e_DebugState)
        {
            case E_DebugState.None:
                return;
            case E_DebugState.Count:
                if (c == null)
                {
                    Debug.Log($"[{Time.frameCount}] {s}");
                }
                else
                {
                    Debug.Log($"[{Time.frameCount}] {s}", c);
                }
                return;
            case E_DebugState.Deb:
            default:
                if (c == null)
                {
                    Debug.Log(s);
                }
                else
                {
                    Debug.Log(s, c);
                }
                return;
        }
    }
}
 
/// <summary>
/// FPS显示
/// </summary> 
[DefaultExecutionOrder(-100)]
public class Initialize_Mono : MonoBehaviour
{
    public Debug_.E_DebugState e_DebugState;
    public List<提示管理设置> TsL = new List<提示管理设置>();
    private void Start()
    {  

         Event_M.I.Add(Event_M.刷新提示机关, 提示机关刷新);
        重制触发 += (int i, int q) => { Waite(提示机关刷新, 0.01f); };
        切换Shader.I.SpeedAction += (bool b) => { Waite(提示机关刷新, 0.01f); };
        Waite(提示机关刷新, 0.01f); 
    }
    void 提示机关刷新 ()
    {
          foreach (var item in TsL)    item.刷新(); 
    }
    public void BOSS模式(GameObject boss, bool b)
    {
        主UI.I.Boss血条_(boss, boss.name, b);
        var a = boss.GetComponent<Boss.I_Boss>().Gs;
        foreach (var item in a)
        {
            item.SetActive(b);
        }
    }

    public bool 显示点位置 = false;

    public float 假物理重力 = 9.8f;

    public Color 能力道具指示颜色 = Color.yellow;
    public Color 搜集物品指示颜色 = Color.green;

    public Action<string> Key_Action;

    public LayerMask 戒备检测层;
    public float 压缩倍数;

    //public Action<int, int> 机关重制触发 { get; set; }
    public Action  小地图刷新 { get; set; }
 
    public Action<int, int> 重制触发 { get; set; }
    public AnimationCurve defaul_Curve;

    [SerializeField]
    float f_Time_弹反销毁时间; 
    [SerializeField]
    private float f_Time_碰到玩家后销毁时间 = 0.1f;
    [SerializeField]
    float f_Time_踩上去自爆的时间;
    public float Speed_Max = 100;
    public float Speed_Min = 0.000001f;
    [DisplayOnly]
    [SerializeField]
    float Speed;
    public float 敌人回耐久速度;
    public float 重生平台存活时间 = 1;
    public List<消息> 消息列表 = new List<消息>();
    //public List<消息> 消息列表 { get {
    //        消息列表显示 = 消息列表1;
    //        return 消息列表1;
    //    }
    //    set {
    //        消息列表1 = value;
    //        消息列表显示 = 消息列表1;
    //    } }
    //public List<消息> 消息列表显示;

    public bool 状态消息总闸;
    public static Initialize_Mono I { get; private set; } = new Initialize_Mono();

 
    [DisplayOnly]
    public List<String> 事件字典显示;

    public List<AnimationClip> asddd;
    public Animator a;
    [Serializable]
    public struct 消息
    {
        [SerializeField]
        [DisplayOnly]
        public string Key;
        [SerializeField]

        public bool Value;

        public 消息(string key, bool value)
        {
            Key = key;
            Value = value;
        }
    }

    [SerializeField]
    [DisplayOnly]
    bool 已经调用过;

    Coroutine 改变时间;

    public bool 能踩(Collider2D C)
    {
        return (C.IsTouchingLayers(Initialize.L_Player) || C.CompareTag(Initialize.One_way)) && Initialize.Layer_is(C.gameObject.layer, Player3.I.碰撞检测层);
        //return C.IsTouchingLayers(Player3.I.碰撞检测层) || C.CompareTag(Initialize.Ground);
    }


    public class 数值范围
    {
        // 基数（例如示例中的 5）
        public readonly float Speed;  
      public readonly  List<float > YesPowers = new List<float>();
        public readonly List<float> NoPowers = new List<float>();


        public 数值范围(float baseValue, int maxPositive )
        {
            Speed=baseValue;
            for (int i = 0; i < maxPositive; i++)
            {
                YesPowers.Add( Mathf.Pow(Speed,i)  );
                NoPowers.Add(1/YesPowers[i]);
                 
            }
        }

        /// <summary>
        /// 返回 n 使得 base^n <= value < base^(n+1)
        /// n 可为负（例如 base=5，value=0.2 返回 -1，因为 5^-1 == 0.2）
        /// 若 value < 最小缓存返回 -MaxNegative；若 value >= 最大缓存返回 MaxPositive
        /// </summary>
        public int GetInt(float value)
        {
            if ( value>YesPowers[YesPowers.Count - 1]|| value < NoPowers[NoPowers.Count - 1])
            {
                return BugInt;
            }
            if (value>1)
            { 
                for (int i = 0; i < YesPowers.Count; i++)
                {
                    //Debug.LogError(YesPowers[i]);
                    if (value+0.001f< YesPowers[i] )
                    {
                        return i-1;
                    }
                }
            } else if(value < 1)
            {
                for (int i = 0; i <NoPowers.Count; i++)
                {
                    //Debug.LogError(NoPowers[i]);
                    if (value - 0.0000001f > NoPowers[i] )
                    {
                        return -(i - 1);
                    }
                }
            }
          return 0;
        }

 
    }

    //public static Speed数值到Int;
    // public     class 数值范围 
    // {
    //public float Speed;
    //     private readonly List<float> Powers;
    //     private readonly List<float> 负Powers;
    //     public 数值范围(float Sp,int I)
    //     {
    //         Speed= Sp;
    //         for (int i = 0; i < I; i++)
    //         {
    //             Powers[i] = Mathf.Pow(Speed, I);
    //             负Powers[i] = 1 / Powers[i];
    //         }
    //     }
    //     public int GetInt(float Sp)
    //     { 
    //         if (Sp>1)
    //         {
    //             /// sp为5返回为1 为25 返回为2  sp为6返回1  sp为2 返回为0
    //         }
    //         else if(Sp < 1)
    //         {
    //             /// sp为0.2返回为1 为0.04 返回为2    sp为0.19 返回为1   sp为0.1返回为0 
    //         }
    //         return 0;
    //     }
    // }

    private void Awake()
    {
 
        QualitySettings.vSyncCount = 0;
        // 锁定为60帧
        Application.targetFrameRate = 60;
        事件字典显示 = Event_M.I.事件列表;
        if (I != null && I != this)
        {
            Destroy(this);
        }
        else
        {
            var a = I.消息列表;
            I = this;
            I.消息列表 = a;
        }
        SceneManager.activeSceneChanged += asd;   //切换场景在切换回来之后     时间必须重制 
        if (敌人回耐久速度 == 0)
        {
            敌人回耐久速度 = 10;
        }

        LoadPla_and_D(); 

        Sz = new 数值范围(阀值,8 );
    }
    public static void LoadPla_and_D()
    {
        Save_D.存档字典_ = null;
        Save_D.Load();


        DeadPla.I.DeadList = null;
        DeadPla.I.读取();
        DeadPla.I.DE();
    }
   public 数值范围 Sz { get; private set; }
    public void 改变一会儿时间(float 真实时间, float 速率)
    {
        改变时间 = StartCoroutine(asd(真实时间, 速率));
    }
    IEnumerator asd(float 真实时间, float 速率)
    {
        Initialize.TimeScale = 速率;
        yield return new WaitForSecondsRealtime(真实时间);
        Initialize.TimeScale = 1;
    }


    [SerializeField]
    bool NEW消息列表 = false;
    public void DebugList_(Type t, object Message)
    {

        string a = t.Name;
        bool 有 = false;

        foreach (var item in I.消息列表)
        {
            if (item.Key == a)
            {
                有 = true;
            }
        }
        if (!有)
        {//没有
            Debug.Log("添加了" + a);
            I.消息列表.Add(new 消息(a, true));
        }
        int 索引 = -1;
        for (int i = 0; i < I.消息列表.Count; i++)
        {
            if (I.消息列表[i].Key == a)
            {
                有 = true;
                索引 = i;
            }
        }
        if (!有)
        {
        }
        //现在肯定有了
        if (I.消息列表[索引].Value)
        {
            //是开着的
            Debug.Log(a + "发送:\t\t    " + Message);
        }
    }

    void asd(Scene b, Scene a)
    {
        SceneManager.activeSceneChanged -= asd;
        Initialize.时间恢复();
    }

    public static void 闪烁(GameObject gameObject, float 时间, float 间隔)
    {
        SpriteRenderer sp = new SpriteRenderer();
        Initialize.组件(gameObject, ref sp);
        I.StartCoroutine(进入某冲刺模式(sp, 间隔));
    }
    static IEnumerator 进入某冲刺模式(SpriteRenderer sp, float 间隔)
    {
        sp.enabled = false;
        yield return new WaitForSeconds(1f);
        sp.enabled = false;
    }
    [SerializeField]
    private Sprite sp;

    //WaitForSeconds waite { get; } = new WaitForSeconds(1);
    public static void 残留(GameObject G, Vector2 a, Vector2 b)
    {


        var ga = new GameObject("残留");
        ga.transform.SetParent(G.transform);
        var SPP = ga.AddComponent<SpriteRenderer>();
        SPP.sprite = I.sp;
        SPP.color = Color.red;
        SPP.DOFade(0, 1);
        ga.transform.localPosition = a;
        ga.transform.localScale =new Vector3(b.x, b.y,1) ;
        ga.transform.SetParent(null);
    }

    public static void 残留(BoxCollider2D bo)
    {
        //   var T = bo.gameObject.transform;
        //   Vector2 o = bo.offset;
        //   Vector2 si = bo.size;

        //   var ga = new GameObject("残留");
        //   ga.transform.SetParent(T);
        //var SPP=ga.AddComponent <SpriteRenderer>();
        //   SPP.sprite=I.sp;
        //   SPP.color = Color.red;
        //   SPP.DOFade(0,1);
        //   ga.transform.localPosition = o;
        //   ga.transform.localScale = si;
        //   ga.transform.SetParent(null);
    }
    //IEnumerable asdasd(SpriteRenderer  a)
    //{
    //    float time = 0;
    //    for (int i = 0;  ; i++)
    //    {
    //        time += Time.deltaTime;
    //        var no = 1-(time / 留着);
    //        a.color = new Color();
    //        yield return waite;
    //        break;
    //    }

    //}
    //IEnumerator 等待真实时间执行方法(Action a, float time)
    //{
    //    yield return new WaitForSecondsRealtime(time);
    //    a.Invoke();
    //}
    IEnumerator 等待变速时间执行方法_同速(Action a, float time)
    {
        float TT = 0;
        while (TT < time)//false执行
        {

            yield return new WaitForFixedUpdate();
            TT += Time.fixedDeltaTime;
        }
        a.Invoke();
    }
    IEnumerator 等待时间执行方法(Action a, float time, bool b = false)
    {
        if (b)
        {
            yield return new WaitForSecondsRealtime(time);
        }
        else
        {
            yield return new WaitForSeconds(time);
        }
        a.Invoke();
    }

    public void Waite_同速(Action a, float time)
    {
        StartCoroutine(等待变速时间执行方法_同速(a, time));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="a">            阿三大苏打</param>
    /// <param name="time"></param>
    /// <param name="真实时间"></param>
    public void Waite(Action a, float time = 0, bool 真实时间 = false)
    { 
        StartCoroutine(等待时间执行方法(a, time, 真实时间));
    }
    public void 等待一帧执行方法_检测原obj是否启用(GameObject G, Action a)
    {
        Debug.LogError(G.name);
        StartCoroutine(等待激活一帧后执行方法(G, a));
    }

    IEnumerator 等待激活一帧后执行方法(GameObject G, Action a)
    {
        if (!G.activeInHierarchy)
        {//如果没激活
            while (!G.activeInHierarchy)
            {
                yield return null;
            }
        }
        else
        {//如果激活了
            yield return null;
            a.Invoke();
        }
    }
    public bool 时缓不动;
    // 协程方法  
    IEnumerator SetTimeCoroutine(float setTime, float endTime)
    {
        时缓不动 = false;
        float startTime = Time.realtimeSinceStartup; // 获取开d始协程时的真实时间  
        float elapsedTime = 0f; // 协程已过去的时间   
        while (elapsedTime < endTime)
        {
            while (时缓不动)
            {
                yield return null;
            }


            // 计算已经过去的时间  
            elapsedTime = Time.realtimeSinceStartup - startTime;

            // 使用Mathf.Lerp来平滑地改变TimeScale  
            //Initialize .    TimeScale = Mathf.Lerp(Initialize.TimeScale, setTime, elapsedTime / endTime);              666
            Initialize.TimeScale = Mathf.Lerp(Initialize.TimeScale, setTime, 0.5f);
            // 等待直到下一帧  
            yield return 零点零2秒;

            // 如果在endTime之前再次调用此协程，我们需要重新计算endTime  
            // 这里假设你有一个外部的方法来调用StopCoroutine并重启它  
            // 例如：StopCoroutine(myCoroutine); myCoroutine = StartCoroutine(SetTimeCoroutine(newSetTime, newEndTime));  
        }
        for (int i = 0; i < 10; i++)
        {
            Initialize.TimeScale = Mathf.Lerp(Initialize.TimeScale, 1, 0.5f);
            yield return 零点零2秒;
        }
        // 到达endTime后，将TimeScale设置为1  
        Initialize.TimeScale = 1;

    }

    // 外部调用这个方法来启动协程  


    public void 时缓(float setTime, float endTime)
    {
        Initialize.TimeScale = 1f;
        if (时缓协程 != null)
        {
            StopCoroutine(时缓协程); // 如果已经有其他协程在运行，先停止它们  
        }
        时缓协程 = StartCoroutine(SetTimeCoroutine(setTime, endTime));
    }

    public Coroutine 时缓协程;
    WaitForSecondsRealtime 零点零2秒 = new WaitForSecondsRealtime(0.02f);
    [SerializeField]
    float 阀值_ = 5;
    public float 阀值
    {
        get
        {
            return 阀值_;
        }
    }
    [SerializeField]
    [DisplayOnly]
    public float 负阀值_ = 100;
    [SerializeField]
    [DisplayOnly]
    public float 阀值2_5_;
    [SerializeField]
    float 阀值2_ = 10;
    [SerializeField]
    float 阀值3_ = 50;
   public bool   打包额外打印;
    [SerializeField]
    float 物理阀值 = 50;
    [SerializeField]
    float Flyground物理阀值 = 10;
    public float 负阀值 { get => 1 / 负阀值_; }
    public float 阀值2 { get => 阀值* 阀值; }
    public float 阀值2_5 { get => (阀值3 + 阀值2) / 2; }
    public float 阀值3 { get => 阀值 * 阀值 * 阀值  ; }
    public float F_Time_踩上去自爆的时间 { get => f_Time_踩上去自爆的时间; private  set => f_Time_踩上去自爆的时间 = value; }
    public float F_Time_碰到玩家后销毁时间 { get => f_Time_碰到玩家后销毁时间; private set => f_Time_碰到玩家后销毁时间 = value; }
    public float 物理阀值1 { get => 物理阀值; set => 物理阀值 = value; }
    public float Flyground物理阀值1 { get => Flyground物理阀值; set => Flyground物理阀值 = value; }
    public class GetMinFloat
    {
        public float GetFlyGMin(float Spee )
        {
           var a= Mathf.Min(Spee, Initialize_Mono.I.Flyground物理阀值1);
 
            return a;
        }
        public float GetMin(float Speed)
        {
            return Mathf.Min(Speed, Initialize_Mono.I.物理阀值1);
        }
    }
    public GetMinFloat Mi=new GetMinFloat();
    public   float GetMin(   float Speed)
    {
        return Mathf.Min(Speed,   物理阀值1);
    }
  public static int BugInt { get; set; } = 1145 ;
    public bool Updatee;
    public float 全局默认Fov=12f;
    public float 光照最大速度=9999;
    public float 最大箭矢坠落速度=-1.8f;
    public bool 动态跳跃碰撞=true;
    public  float 下落动画速度=8;
    public bool MoveP_优化;
    public  GameObject 速度标识;
    public float 子弹引线时间=1.5f;
    public float 校准值=0.0089f;

    public int GetSpeedInt(float Speed)
    {
 
        return Sz.GetInt(Speed);
    }


    private void Update()
    { 
        
        阀值2_5_ = 阀值2_5;
        阀值3_ = 阀值3;
        阀值2_ = 阀值2;
        if (Player3.I != null)
            Speed = Player3.Public_Const_Speed;
        if (NEW消息列表)
        {
            消息列表 = new List<消息>();
            NEW消息列表 = false;
        }
    }
}

public enum E_方向
{
    Null, 上, 下, 左, 右, 左上, 左下, 右上, 右下, 上上,下下,左左, 右右
}
public class No_假Fix
{
    float 间隔;
    float TTime;
    public No_假Fix(float 间隔)
    {
        this.间隔 = 间隔;
    }
    public bool FixUpdate()
    {
        if (Time.time > 间隔 + TTime)
        {
            TTime = Time.time;
            return true;
        }
        else
        {
            return false;
        }

    }
}
public class No_Re
{
    int C = -1;

    public bool Note_Re()
    {
        if (C != Time.frameCount)
        {
            C = Time.frameCount;
            return true;
        }
        else
        {
            return false;
        }
    }
}
 

 
public static  class   Rb反算
{ /// <summary>
  /// 简化版本：根据位移和方向计算需要的初速度（只考虑重力）
  /// </summary>
  /// <param name="rb">Rigidbody2D组件</param>
  /// <param name="displacement">目标位置相对于当前位置的位移</param>
  /// <param name="direction">施加力的方向（不需要单位化）</param>
  /// <returns>需要设置的初速度Vector2</returns>
    public static Vector2 V2Frist(
     this Rigidbody2D rb,
        Vector2 displacement,
        Vector2 direction)
    {
        // 方向归一化
        Vector2 dir = direction.normalized;

        // 获取重力加速度
        float gravity = Mathf.Abs(Physics2D.gravity.y);

        // 计算需要的总速度大小
        // 使用公式：v = sqrt(2 * g * h)
        // 其中 h = displacement.magnitude * sin(θ)
        // θ 是位移与水平方向的夹角
        float displacementAngle = Mathf.Atan2(displacement.y, displacement.x);
        float dirAngle = Mathf.Atan2(dir.y, dir.x);
        float angleDiff = dirAngle - displacementAngle;

        // 计算沿方向的分量
        float speed = Mathf.Sqrt(2 * gravity * displacement.magnitude * Mathf.Abs(Mathf.Cos(angleDiff)));

        // 返回速度向量
        return dir * speed;
    }
    /// <summary>
    /// 完整版本：根据位移和方向计算需要的力（考虑所有物理参数）
    /// </summary>
    /// <param name="rb">Rigidbody2D组件</param>
    /// <param name="displacement">目标位置相对于当前位置的位移</param>
    /// <param name="direction">施加力的方向（不需要单位化）</param>
    /// <param name="maxIterations">最大迭代次数（默认50）</param>
    /// <param name="tolerance">容差（默认0.01）</param>
    /// <returns>需要设置的力Vector2</returns>
    public static Vector2 V2Next(
     this Rigidbody2D rb,
        Vector2 displacement,
        Vector2 direction,
        int maxIterations = 50,
        float tolerance = 0.01f)
    {
        // 参数验证
        if (direction.sqrMagnitude < 0.001f)
        {
            Debug.LogError("方向向量不能为零");
            return Vector2.zero;
        }

        // 方向归一化
        Vector2 dir = direction.normalized;

        // 获取刚体物理参数
        float mass = rb.mass;
        float drag = rb.drag;
        float angularDrag = rb.angularDrag;
        float gravityScale = rb.gravityScale;
        Vector2 gravity = Physics2D.gravity * gravityScale;

        // 简化版本作为初始猜测
        Vector2 initialForce = V2Frist(rb, displacement, direction);
        float initialMagnitude = initialForce.magnitude;

        // 使用二分法寻找合适的力大小
        float minForce = 0f;
        float maxForce = initialMagnitude * 10f; // 初始猜测的10倍作为上限
        Vector2 bestForce = Vector2.zero;
        float bestError = float.MaxValue;

        for (int i = 0; i < maxIterations; i++)
        {
            // 计算当前测试的力大小（二分法）
            float testMagnitude = (minForce + maxForce) * 0.5f;
            Vector2 testForce = dir * testMagnitude;

            // 计算施加此力后的运动轨迹终点
            Vector2 endPosition = SimulateForceTrajectory(
                Vector2.zero, // 起始位置（相对位置）
                testForce,    // 施加的力
                mass,
                drag,
                gravity,
                5f,           // 模拟时间（足够长以观察最终位置）
                0.02f         // 时间步长
            );

            // 计算与目标位移的误差
            float error = Vector2.Distance(endPosition, displacement);

            // 更新最佳解
            if (error < bestError)
            {
                bestError = error;
                bestForce = testForce;
            }

            // 检查是否满足精度要求
            if (error < tolerance)
            {
                Debug.Log($"在第{i + 1}次迭代找到合适解，误差：{error:F4}");
                return testForce;
            }

            // 调整搜索范围
            // 比较终点位置与目标位置
            float distanceToTarget = Vector2.Dot(endPosition.normalized, dir);
            float targetDistance = Vector2.Dot(displacement.normalized, dir);

            if (distanceToTarget < targetDistance)
            {
                // 距离不够，增加力
                minForce = testMagnitude;
            }
            else
            {
                // 距离过大，减小力
                maxForce = testMagnitude;
            }

            // 如果搜索范围太小，提前结束
            if (maxForce - minForce < 0.01f)
            {
                Debug.Log($"搜索范围过小，使用最佳解，误差：{bestError:F4}");
                return bestForce;
            }
        }

        Debug.LogWarning($"在{maxIterations}次迭代后未找到精确解，使用最佳近似，误差：{bestError:F4}");
        return bestForce;
    }

    /// <summary>
    /// 模拟施加力后的轨迹
    /// </summary>
    private static Vector2 SimulateForceTrajectory(
        Vector2 startPos,
        Vector2 initialForce,
        float mass,
        float drag,
        Vector2 gravity,
        float totalTime,
        float timeStep = 0.02f)
    {
        Vector2 position = startPos;
        Vector2 velocity = initialForce / mass; // 力转化为初速度

        float currentTime = 0f;
        float dt = timeStep;

        while (currentTime < totalTime)
        {
            // 计算时间步长
            dt = Mathf.Min(dt, totalTime - currentTime);

            // 计算加速度：重力加速度 + 阻力加速度
            Vector2 acceleration = gravity; // 重力

            // 空气阻力（与速度方向相反）
            Vector2 dragAcceleration = -drag * velocity;
            acceleration += dragAcceleration;

            // 使用半隐式欧拉法（更稳定）
            velocity += acceleration * dt;
            position += velocity * dt;

            // 如果速度接近零且位置接近稳定，提前结束
            if (velocity.sqrMagnitude < 0.001f && acceleration.sqrMagnitude < 0.001f)
            {
                break;
            }

            currentTime += dt;
        }

        return position;
    }

    /// <summary>
    /// 快速近似版本：使用能量守恒原理
    /// </summary>
    public static Vector2 V2Last(
       this Rigidbody2D rb,
        Vector2 displacement,
        Vector2 direction)
    {
        // 方向归一化
        Vector2 dir = direction.normalized;

        // 获取物理参数
        float mass = rb.mass;
        float drag = rb.drag;
        float gravityScale = rb.gravityScale;
        float gravity = Mathf.Abs(Physics2D.gravity.y * gravityScale);

        // 计算位移在方向上的投影
        float displacementInDirection = Vector2.Dot(displacement, dir);

        // 考虑阻力的能量损失因子
        // 经验公式：能量损失与距离和阻力成正比
        float energyLossFactor = 1f + drag * displacement.magnitude * 0.1f;

        // 计算需要的动能
        // E = 0.5 * m * v² = m * g * h + 阻力损失
        float requiredHeight = Mathf.Abs(displacement.y);
        float requiredEnergy = mass * gravity * requiredHeight * energyLossFactor;

        // 计算速度大小
        float speed = Mathf.Sqrt(2f * requiredEnergy / mass);

        // 考虑角度因素调整
        float angle = Vector2.Angle(dir, displacement.normalized) * Mathf.Deg2Rad;
        float angleFactor = 1f / Mathf.Max(0.3f, Mathf.Cos(angle));
        speed *= angleFactor;

        // 计算力的大小（力 = 质量 * 速度 / 时间，这里假设时间=1）
        float forceMagnitude = mass * speed;

        return dir * forceMagnitude;
    }   
    /// <summary>
         /// 版本1：只考虑重力，计算在指定时间内跳跃到目标位置所需的速度
         /// </summary>
         /// <param name="rb">Rigidbody2D组件（仅用于获取当前位置和重力）</param>
         /// <param name="flightTime">到达目标的飞行时间（秒）</param>
         /// <param name="displacement">目标位置相对于当前位置的位移（目标位置 - 当前位置）</param>
         /// <returns>需要设置的初速度Vector2</returns>
    public static Vector2 Frist(
 this  Rigidbody2D rb,
    float flightTime,
    Vector2 displacement,
    int maxIterations = 100,
    float tolerance = 0.01f)
    {
        // 参数验证
        if (flightTime <= 0.001f)
        {
            Debug.LogError("飞行时间必须大于0");
            return Vector2.zero;
        }

        // 获取刚体参数
        float mass = rb.mass;
        float drag = rb.drag;
        float gravityScale = rb.gravityScale;
        Vector2 gravity = Physics2D.gravity * gravityScale;

        // 使用简单公式作为初始猜测
        Vector2 initialGuess = Next(rb, flightTime, displacement);

        // 使用牛顿-拉弗森法迭代优化
        Vector2 currentVelocity = initialGuess;
        int II = 0;
        for (int i = 0; i < maxIterations; i++)
        {
            // 模拟轨迹
            Vector2 predictedPos = SimulateTrajectory(
                Vector2.zero, // 从原点开始
                currentVelocity,
                mass,
                drag,
                gravity,
                flightTime
            );

            // 计算误差
            Vector2 error = displacement - predictedPos;
            float errorMagnitude = error.magnitude;

            // 检查是否达到精度要求
            if (errorMagnitude < tolerance)
            {
                Debug.Log($"迭代{i + 1}次后收敛，最终误差：{errorMagnitude:F4}");
                return currentVelocity;
            }

            // 计算雅可比矩阵（数值近似）
            float h = 0.001f; // 微小变化量

            // 对vx求偏导
            Vector2 velXPlus = currentVelocity + new Vector2(h, 0);
            Vector2 posXPlus = SimulateTrajectory(Vector2.zero, velXPlus, mass, drag, gravity, flightTime);
            Vector2 dPos_dVx = (posXPlus - predictedPos) / h;

            // 对vy求偏导
            Vector2 velYPlus = currentVelocity + new Vector2(0, h);
            Vector2 posYPlus = SimulateTrajectory(Vector2.zero, velYPlus, mass, drag, gravity, flightTime);
            Vector2 dPos_dVy = (posYPlus - predictedPos) / h;

            // 构建2x2雅可比矩阵
            float[,] J = new float[2, 2]
            {
                { dPos_dVx.x, dPos_dVy.x },
                { dPos_dVx.y, dPos_dVy.y }
            };

            // 计算雅可比矩阵的行列式
            float det = J[0, 0] * J[1, 1] - J[0, 1] * J[1, 0];

            // 如果行列式太小，使用梯度下降法替代
            if (Mathf.Abs(det) < 1e-6f)
            {
                // 梯度下降：速度 = 速度 + 学习率 * 误差
                float learningRate = 0.1f;
                currentVelocity += error * learningRate;
                continue;
            }

            // 计算雅可比矩阵的逆
            float[,] J_inv = new float[2, 2]
            {
                { J[1, 1] / det, -J[0, 1] / det },
                { -J[1, 0] / det, J[0, 0] / det }
            };

            // 牛顿-拉弗森法更新：v_new = v_old + J⁻¹ * error
            float deltaVx = J_inv[0, 0] * error.x + J_inv[0, 1] * error.y;
            float deltaVy = J_inv[1, 0] * error.x + J_inv[1, 1] * error.y;

            currentVelocity += new Vector2(deltaVx, deltaVy);

            // 限制速度变化幅度，防止发散
            float maxDelta = 10f;
            if (currentVelocity.magnitude > initialGuess.magnitude * 10f)
            {
                currentVelocity = initialGuess;
                Debug.LogWarning($"迭代发散，重置为初始猜测");
            }

            II++;
        }
        Debug.LogError(II);
        Debug.LogWarning($"在{maxIterations}次迭代后未收敛");
        return currentVelocity;
    }
    private static Vector2 SimulateTrajectory(
        Vector2 startPos,
        Vector2 startVelocity,
        float mass,
        float drag,
        Vector2 gravity,
        float totalTime,
        float timeStep = 0.01f)
    {
        Vector2 position = startPos;
        Vector2 velocity = startVelocity;
        float currentTime = 0f;
        int II=  0;
        while (currentTime < totalTime)
        {
            // 计算时间步长（不超过剩余时间）
            float dt = Mathf.Min(timeStep, totalTime - currentTime);

            // 计算合力：重力 + 阻力
            Vector2 force = mass * gravity; // 重力

            // 阻力：-drag * velocity
            Vector2 dragForce = -drag * velocity;
            force += dragForce;

            // 计算加速度：a = F / m
            Vector2 acceleration = force / mass;

            // 使用欧拉法更新速度和位置
            velocity += acceleration * dt;
            position += velocity * dt;

            currentTime += dt;

            II++;
        }
        Debug.LogError(II);
        return position;
    }
    /// <summary>
    /// 版本2：完整考虑刚体设置（重力、质量、线性阻力、重力缩放等）
    /// 使用数值迭代法求解，更精确但计算量更大
    /// </summary>
    /// <param name="rb">Rigidbody2D组件</param>
    /// <param name="flightTime">到达目标的飞行时间（秒）</param>
    /// <param name="displacement">目标位置相对于当前位置的位移</param>
    /// <param name="maxIterations">最大迭代次数（默认100）</param>
    /// <param name="tolerance">容差（默认0.01）</param>
    /// <returns>需要设置的初速度Vector2，如果计算失败返回Vector2.zero</returns>
    public static Vector2 Next(
   this Rigidbody2D rb,
    float flightTime,
    Vector2 displacement)
    {
        // 获取当前重力（取绝对值，因为Unity中重力是负值）
        float gravity = Mathf.Abs(Physics2D.gravity.y);

        // 计算水平速度分量：vx = Δx / t
        float velocityX = displacement.x / flightTime;

        // 计算垂直速度分量：vy = (Δy + 0.5 * g * t²) / t
        // 使用公式：y = vy * t - 0.5 * g * t²  (因为重力向下)
        // 重排得：vy = (y + 0.5 * g * t²) / t
        float velocityY = (displacement.y + 0.5f * gravity * flightTime * flightTime) / flightTime;

        return new Vector2(velocityX, velocityY);
    }
    public static Vector2 Last(
  this Rigidbody2D rb,
    float flightTime,
    Vector2 displacement)
    {
        // 获取刚体参数
        float drag = rb.drag;
        float gravity = Mathf.Abs(Physics2D.gravity.y * rb.gravityScale);

        // 计算有效重力（考虑阻力对下落的影响）
        // 阻力会减小有效重力，这里使用经验公式
        float effectiveGravity = gravity * Mathf.Exp(-drag * flightTime * 0.5f);

        // 计算速度分量
        float velocityX = displacement.x / flightTime;

        // 调整X速度，考虑阻力影响
        // 阻力会使水平速度随时间衰减
        float dragFactorX = Mathf.Exp(drag * flightTime) - 1f;
        velocityX = velocityX * drag * flightTime / dragFactorX;

        // 计算Y速度
        // 使用有效重力代替实际重力
        float velocityY = (displacement.y + 0.5f * effectiveGravity * flightTime * flightTime) / flightTime;

        // 调整Y速度，考虑阻力影响
        float dragFactorY = 1f + drag * flightTime * 0.5f;
        velocityY *= dragFactorY;

        return new Vector2(velocityX, velocityY);
    }
}
 

public static class Initialize
{
    public static void  TryPlay(this Animator animator, string c)
    {
      if(animator.AnimatorHasState(c))
        {
            animator.Play(c);
        }
    }
    public static bool AnimatorHasState(this Animator animator, string stateName, int layerIndex = 0)
    {
        // 参数有效性检查
        if (animator == null)
        {
            Debug.LogWarning("AnimatorHasState: Animator参数为null");
            return false;
        }

        if (string.IsNullOrEmpty(stateName))
        {
            Debug.LogWarning("AnimatorHasState: 状态名称为空或null");
            return false;
        }

        if (layerIndex < 0 || layerIndex >= animator.layerCount)
        {
            Debug.LogWarning($"AnimatorHasState: 层索引{layerIndex}超出有效范围(0-{animator.layerCount - 1})");
            return false;
        }

        // 核心检测逻辑：将状态名转换为Hash ID并使用HasState API
        int stateHash = Animator.StringToHash(stateName);
        return animator.HasState(layerIndex, stateHash);
    }
    /// <summary>
    /// 重载：将字典赋值到已存在的实例（适合无法新建实例的场景，如 Unity MonoBehaviour）
    /// </summary>
    /// <param name="instance">已存在的实例</param>
    /// <param name="fieldDict">字段名字典</param>
    /// <param name="includePrivateFields">是否赋值私有字段</param>
    /// <exception cref="ArgumentNullException">实例为 null 时抛出</exception>
    public static void PopulateInstance(object instance, Dictionary<string, object> fieldDict, bool includePrivateFields = false)
    {
        if (instance == null)
            throw new ArgumentNullException(nameof(instance), "目标实例不能为 null");
        if (fieldDict == null || fieldDict.Count == 0)
            return;

        // 构建字段检索的绑定标志
        BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
        if (includePrivateFields)
            bindingFlags |= BindingFlags.NonPublic;

        Type targetType = instance.GetType();
        foreach (var kvp in fieldDict)
        {
            try
            {
                // 获取目标类型的对应字段
                FieldInfo field = targetType.GetField(kvp.Key, bindingFlags);
                if (field == null)
                {
                    Console.WriteLine($"警告：类型 {targetType.Name} 中未找到字段 {kvp.Key}，跳过赋值");
                    continue;
                }

                // 校验值类型与字段类型匹配（避免类型不兼容赋值）
                object valueToSet = kvp.Value;
                if (valueToSet != null && !field.FieldType.IsAssignableFrom(valueToSet.GetType()))
                {
                    // 尝试类型转换（如 int 转 float，string 转 int 等）
                    try
                    {
                        valueToSet = Convert.ChangeType(valueToSet, field.FieldType);
                    }
                    catch (InvalidCastException)
                    {
                        Console.WriteLine($"警告：字段 {kvp.Key} 类型 {field.FieldType.Name} 与值类型 {valueToSet.GetType().Name} 不兼容，跳过赋值");
                        continue;
                    }
                }

                // 赋值到实例字段
                field.SetValue(instance, valueToSet);
                Console.WriteLine($"成功为字段 {kvp.Key} 赋值：{valueToSet ?? "null"}");
            }
            catch (FieldAccessException ex)
            {
                Console.WriteLine($"错误：无法访问字段 {kvp.Key}，原因：{ex.Message}");
            }
            catch (TargetException ex)
            {
                Console.WriteLine($"错误：字段 {kvp.Key} 赋值目标无效，原因：{ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"处理字段 {kvp.Key} 时发生异常：{ex.Message}");
            }
        }
    }
    /// <summary>
    /// 将字典转换为指定类型的实例（赋值实例字段）
    /// </summary>
    /// <typeparam name="T">目标类型（需有无参构造函数）</typeparam>
    /// <param name="fieldDict">字段名字典（键：字段名，值：字段值）</param>
    /// <param name="includePrivateFields">是否赋值私有字段</param>
    /// <returns>赋值后的目标类型实例</returns>
    /// <exception cref="InvalidOperationException">类型无无参构造函数时抛出</exception>
    public static T ConvertToInstance<T>(Dictionary<string, object> fieldDict, bool includePrivateFields = false)
        where T : new()
    {
        // 创建目标类型实例（约束 T 必须有无参构造函数）
        T instance = new T();
        PopulateInstance(instance, fieldDict, includePrivateFields);
        return instance;
    }
    /// <summary>
    /// 将对象的字段名和字段值转换为字典（键：字段名，值：字段数据）
    /// </summary>
    /// <param name="source">源实例（不能为 null）</param>
    /// <param name="includePrivateFields">是否包含私有字段（默认：否）</param>
    /// <param name="includeStaticFields">是否包含静态字段（默认：否）</param>
    /// <returns>包含字段名和值的字典</returns>
    /// <exception cref="ArgumentNullException">源实例为 null 时抛出</exception>
    public static Dictionary<string, object> GetFieldDictionary(object source, bool includePrivateFields = false, bool includeStaticFields = false)
    {
        // 参数校验
        if (source == null)
            throw new ArgumentNullException(nameof(source), "源实例不能为 null");

        // 构建反射绑定标志（控制字段检索范围）
        BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
        if (includePrivateFields)
            bindingFlags |= BindingFlags.NonPublic;
        if (includeStaticFields)
            bindingFlags |= BindingFlags.Static;

        // 获取类型的所有字段
        Type sourceType = source.GetType();
        FieldInfo[] fields = sourceType.GetFields(bindingFlags);

        // 初始化字典并填充字段名和值
        Dictionary<string, object> fieldDict = new Dictionary<string, object>();
        foreach (FieldInfo field in fields)
        {
            try
            {
                // 读取字段值（静态字段需传入 null，实例字段传入源实例）
                object fieldValue = field.GetValue(field.IsStatic ? null : source);
                fieldDict.Add(field.Name, fieldValue);
            }
            catch (FieldAccessException ex)
            {
                Console.WriteLine($"无法访问字段 {field.Name}：{ex.Message}");
            }
            catch (TargetException ex)
            {
                Console.WriteLine($"字段 {field.Name} 赋值目标无效：{ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"处理字段 {field.Name} 时发生异常：{ex.Message}");
            }
        }

        return fieldDict;
    }
    
    public static  int Speed_toIntSpeed(float g固定等级差)
    {
        var a = Speed_toESpeed( g固定等级差);

        //switch (a)
        //{
        //    case E_超速等级.静止:
        //    case E_超速等级.低速:
        //        return -1;
        //        break;
        //    case E_超速等级.低速:
        //        return -1; 
        //    case E_超速等级.正常:
        //        return 0; 
        //    case E_超速等级.超速:
        //        break;
        //    case E_超速等级.半虚化:
        //        break;
        //    case E_超速等级.虚化:
        //        break;
        //    case E_超速等级.虚无:
        //        break;
        //    default:
        //        break;
        //}
        return 0;
    }
    public  static E_超速等级  Speed_toESpeed(float g固定等级差)
    {
        E_超速等级 e_ = E_超速等级.正常; 
 
                if (g固定等级差 < 1 / Initialize_Mono.I.阀值 || g固定等级差._is(1 / Initialize_Mono.I.阀值))
                    e_ = E_超速等级.低速;
                if (g固定等级差 < Initialize_Mono.I.负阀值 || g固定等级差._is(1 / Initialize_Mono.I.负阀值))
                    e_ = E_超速等级.静止;
                if (g固定等级差 >= Initialize_Mono.I.阀值)
                    e_ = E_超速等级.超速;
                if (g固定等级差 >= Initialize_Mono.I.阀值2)
                    e_ = E_超速等级.半虚化;
                if (g固定等级差 >= Initialize_Mono.I.阀值2_5)
                    e_ = E_超速等级.虚化;
                if (g固定等级差 >= Initialize_Mono.I.阀值3)
                    e_ = E_超速等级.虚无;
                return e_; 
    }
    public static RaycastHit2D[] 碰撞预测碰撞预测(Bounds s, Vector2 方向, float 距离, LayerMask l)
    {
        return Physics2D.BoxCastAll(
    s.center,
    s.size - new Vector3(0.05f, 0.05f),
    0,
    方向,
   距离,
    l) ;

    }
     public  static  Vector2 Get碰撞Position(Bounds myBounds, RaycastHit2D hit, float skin = 0.001f)
    {
        if (!hit.collider) return Vector2.zero;

        Vector2 normal = hit.normal.normalized;
        // myBounds.extents 是半尺寸（沿世界轴），将其在法线方向上的投影长度估算为：
        float proj = Mathf.Abs(myBounds.extents.x * normal.x) + Mathf.Abs(myBounds.extents.y * normal.y);
        // 将中心移动到碰撞点外侧，保证边界与碰撞点贴合（再加上一个微小的 skin）
        Vector2 flushCenter = hit.point + normal * (proj + skin);
        //(flushCenter+Vector2.right*0.2f).DraClirl(3, Color.red, 1);
        return flushCenter;
    }
    public static List<T> 随机列表<T>(this List<T> list)
    {
        // 创建列表的副本以避免修改原始列表
        var shuffledList = new List<T>(list);
        int n = shuffledList.Count;

        // Fisher-Yates 洗牌算法
        while (n > 1)
        {
            n--;
            var rng = new System.Random();
            int k = rng.Next(n + 1);
            T value = shuffledList[k];
            shuffledList[k] = shuffledList[n];
            shuffledList[n] = value;
        }

        return shuffledList;
    }
    public static List<Vector2> 中间并列点(Vector2 target, int count, float offset)
    {
        // 检查count是否为正奇数
        if (count <= 0 || count % 2 == 0)
        {
            Debug.LogError("参数count必须是正奇数");
            return null;
        }

        // 计算中心点索引
        int centerIndex = count / 2;

        // 初始化结果列表
        List<Vector2> points = new List<Vector2>(count);

        // 生成点列表
        for (int i = 0; i < count; i++)
        {
            // 计算当前点相对于中心点的偏移量
            float xOffset = (i - centerIndex) * offset;
            // 创建新点，Y坐标与target相同，X坐标按偏移量计算
            Vector2 newPoint = new Vector2(target.x + xOffset, target.y);
            points.Add(newPoint);
        }

        return points;
    }
    public static Vector3 Z1 { get => new Vector3(0, 0, 1); }


    /// <summary>
    /// 修正版本：考虑物理阀值和固定等级差的抛物线计算
    /// </summary>
    /// <param name="发射方向">发射方向（不需要单位化）</param>
    /// <param name="目标位移">目标位移（目标位置 - 发射位置）</param>
    /// <param name="G">重力加速度（正数）</param>
    /// <param name="固定等级差">固定等级差</param>
    /// <param name="物理阀值">物理阀值（默认30）</param>
    /// <returns>需要的初速度Vector2</returns>
    public static Vector2 抛物线_Get力2(
        Vector2 发射方向,
        Vector2 目标位移,
        float G,
        float 固定等级差 )
    {
        float 物理阀值 = Initialize_Mono.I.物理阀值1;
        // 方向归一化
        Vector2 direction = 发射方向.normalized;

        // 获取ZZZZZZZ因子（你的GetMin方法）
        float zFactor = Mathf.Min(固定等级差, 物理阀值);

        // 实际有效的时间缩放因子
        // 位移计算中的总缩放因子是：zFactor * speedLevel
        // 因为：单位位移 = 当前 * zFactor * (Time.deltaTime * speedLevel)
        float effectiveScale = zFactor * 固定等级差;

        // 重力加速度的缩放因子
        // 重力更新：Y = Y - G * (Time.deltaTime * speedLevel)
        float gravityScale = 固定等级差;

        // 我们需要解这个系统：
        // dx = vx * effectiveScale * t
        // dy = vy * effectiveScale * t - 0.5 * (G * gravityScale) * t²

        // 这等价于解：
        // dx' = vx * t
        // dy' = vy * t - 0.5 * G' * t²
        // 其中：
        // dx' = dx / effectiveScale
        // dy' = dy / effectiveScale
        // G' = G * gravityScale / (effectiveScale * effectiveScale)

        Vector2 scaledDisplacement = 目标位移 / effectiveScale;
        float scaledGravity = G * gravityScale / (effectiveScale * effectiveScale);

        // 使用标准的抛物线公式计算缩放后的速度
        Vector2 scaledVelocity = CalculateParabolicVelocitySimple(
            direction,
            scaledDisplacement,
            scaledGravity
        );

        // 缩放回实际速度
        return scaledVelocity * effectiveScale;
    }
    private static Vector2 CalculateParabolicVelocitySimple(
       Vector2 direction,
       Vector2 displacement,
       float G)
    {
        // 分解位移到方向和垂直方向
        float displacementInDir = Vector2.Dot(displacement, direction);
        Vector2 perpendicularDir = new Vector2(-direction.y, direction.x);
        float displacementPerp = Vector2.Dot(displacement, perpendicularDir);

        // 如果位移与方向垂直分量很小，使用简单公式
        if (Mathf.Abs(displacementPerp) < 0.001f)
        {
            // 直线运动
            float speed___ = Mathf.Sqrt(2 * G * displacementInDir);
            return direction * speed___;
        }

        // 使用能量守恒法计算所需速度
        // 需要的总能量 = 势能 + 动能（用于垂直位移）
        float requiredHeight = Mathf.Abs(displacement.y);
        float angle = Vector2.Angle(direction, displacement) * Mathf.Deg2Rad;

        // 考虑角度的影响
        float effectiveHeight = requiredHeight / Mathf.Max(0.1f, Mathf.Sin(angle));

        // 计算速度
        float speed = Mathf.Sqrt(2 * G * effectiveHeight);

        return direction * speed;
    }
    public static float 抛物线_Get力3(Vector2 发射方向, Vector3 坐标差, float 重力, float 时间倍率)
    {
        // 参数验证
        if (时间倍率 <= 0)
        {
            Debug.LogError($"时间倍率必须大于0，当前值：{时间倍率}");
            return 0;
        }

        if (Mathf.Approximately(发射方向.x, 0))
        {
            Debug.LogError($"发射方向的X分量不能为0，当前方向：{发射方向}");
            return 0;
        }

        // 获取方向分量
        float Cx = 发射方向.x;
        float Cy = 发射方向.y;

        // 获取位移分量
        float Mx = 坐标差.x;
        float My = 坐标差.y;

        // 计算分母部分
        float 分母 = (Cy * Mx / Cx - My) * Cx * Cx;

        // 检查分母是否接近0（无解的情况）
        if (Mathf.Abs(分母) < 0.0001f)
        {
            Debug.LogError($"抛物线无解：发射方向={发射方向}，坐标差={坐标差}，分母接近0");
            return 0;
        }

        // 计算速度平方值
        float V平方 = (0.5f * 重力 * Mx * Mx) / 分母;

        // 检查结果是否有效
        if (V平方 < 0)
        {
            // 无实数解的情况：目标位置在当前发射角度下无法到达
            Debug.LogWarning($"抛物线无实数解：发射方向={发射方向}，坐标差={坐标差}，重力={重力}");
            return 0;
        }

        // 计算速度大小
        float 速度 = Mathf.Sqrt(V平方);

        // 时间倍率的影响：
        // 实际物理意义：时间倍率改变时，重力加速度的表现会变化
        // 如果时间变慢（时间倍率>1），相同位移需要更小的速度
        // 如果时间变快（时间倍率<1），相同位移需要更大的速度
        // 但根据公式推导，时间倍率在计算中会被约去，所以需要单独处理

        // 方法1：根据时间倍率调整速度（物理意义：保持轨迹形状但改变时间）
        // 速度 = 速度 / 时间倍率;

        // 方法2：根据时间倍率调整重力（物理意义：保持速度但改变重力表现）
        // 这取决于您想要的效果

        // 根据您的需求，这里返回不考虑时间倍率的速度
        // 如果需要在代码中使用时间倍率，可以在调用后处理：
        // float 最终速度 = 抛物线_Get力(方向, 坐标差, 重力, 1) / 时间倍率;

        return 速度;
    }
    /// <summary>
    ///  X总位移 = 初始X*力 * ti 
    ///  Y总位移 = 初始Y*力 * ti - 0.5 * G*ti*ti
    ///  
    /// ti= 总位移x/初始X /力         代入  后者方程
    /// </summary> 
    /// 固定力求方向？
    public static float 抛物线_Get力(Vector2 发射方向, Vector3 坐标差, float 重力 )
    {
        var Cx = 发射方向.x;
        var Cy = 发射方向.y;
        var Mx = 坐标差.x;
        var My = 坐标差.y;
        float VValue = (0.5f * 重力 * Mx * Mx) /
            ((Cy * Mx / Cx - My) * Cx * Cx);
        if (VValue < 0)
        {
            /// 0.45 0.89   0.34 2.8           角度超出上限
            Debug.LogError(" (Vector2 发射方向, Vector3 坐标差,float  重力)" + 发射方向 + "   " + 坐标差 + "  " + 重力);
            return 0;
        }
        return (float)Math.Sqrt(VValue);
    }
    public static  float 校准(float A)
    {
        // 基础双曲线衰减项：模拟位移放大效应
        float hyperbolicTerm = 1.0f / (1.0f + 0.0045f * A);

        // 正弦调制项：模拟时间步长效应的周期性影响
        // 频率和相位根据数据点调整
        float sineTerm = 1.0f + 0.025f * Mathf.Sin(0.15f * A - 0.8f);

        // 混合权重：随A增大，正弦项影响增强
        float weight = Mathf.Clamp01((A - 5f) / 20f);

        // 最终校准值 = 双曲线项 × [1 + 权重×(正弦项-1)]
        // 这样当A小时，以双曲线为主；A大时，正弦调制增强
        float calibration = hyperbolicTerm * (1.0f + weight * (sineTerm - 1.0f));

        // 添加一个小的线性漂移项，补偿未建模的效应
        float drift = 0.0005f * (A - 15f);

        return calibration + drift;
    }
    /// <summary>
    ///  一排 点  高度相同  X不同    结果出来的Y速度一致
    /// </summary> 
    public static Vector2 抛物线_Get矢量(Vector3 差, float tim, float g)
    {
        var X = 差.x / tim;
        var Y = 差.y / tim + g * tim / 2;
        return new Vector2(X, Y);
    }


    public static int 头尾(int 总数, int 索引, int 步)
    {
        if (总数 <= 0)
            throw new ArgumentException("总数必须大于0");

        // 使用 long 避免整数溢出
        long 临时 = (long)索引 + 步;
        long 模 = 临时 % 总数;

        // 处理负数结果
        if (模 < 0)
        {
            模 += 总数;
        }

        return (int)模;
    }
    public static bool 是奇数(int num)
    {
        return num % 2 == 0 ? false : true;
    }

    /// <summary>
    /// 在两点之间生成均匀分布的插值点
    /// </summary>
    /// <param name="a">起点坐标</param>
    /// <param name="b">终点坐标</param>
    /// <param name="I">插入的点数（不包括端点）</param>
    /// <returns>生成的插值点列表</returns>
    public static List<Vector3> 单线段插值(Vector2 a, Vector2 b, int I)
    {
        List<Vector3> points = new List<Vector3>();

        // 数学原理：线性插值公式
        // $P(t) = (1-t) \cdot \mathbf{a} + t \cdot \mathbf{b}, \quad t \in [0,1]$
        for (int i = 1; i <= I; i++)
        {
            // 计算插值比例 t = i/(I+1)
            // 确保点在 a 和 b 之间，不包括端点
            float t = (float)i / (I + 1);

            // 应用线性插值公式
            Vector2 point = (1 - t) * a + t * b;
            points.Add(point);
        }

        return points;
    }
    /// <summary>
    /// 在线段上均匀插入点
    /// </summary>
    /// <param name="points">原始点列表（至少2个点）</param>
    /// <param name="I">每段线段插入的点数</param>
    /// <returns>插值后的点列表</returns>
    public static List<Vector2> 多线段均匀插点(List<Vector2> points, int I)
    {
        // 验证输入
        if (points == null || points.Count < 2)
        {
            Debug.LogError("需要至少2个点才能构成线段");
            return points ?? new List<Vector2>();
        }

        List<Vector2> result = new List<Vector2>();

        // 添加第一个点
        result.Add(points[0]);

        // 处理每条线段
        for (int i = 0; i < points.Count - 1; i++)
        {
            // 在当前线段上插入点
            for (int j = 1; j <= I; j++)
            {
                // 计算插值比例
                float t = j / (float)(I + 1);
                // 线性插值
                Vector2 newPoint = Vector2.Lerp(points[i], points[i + 1], t);
                result.Add(newPoint);
            }
            // 添加下一个原始点
            result.Add(points[i + 1]);
        }
        return result;
    }
    public static RaycastHit2D 碰撞两点检测(Vector2 v1, Vector2 v2, LayerMask l)
    {
        var a = Physics2D.Linecast(v1, v2, l);
        return a;
    }
    public static E_方向 Get_盒子八方向(Bounds bounds, Vector2 point)
    {
        // 检查点是否在Bounds内部  
        if (bounds.Contains(point))
        {
            return E_方向.Null; // 或者可以定义一个表示“内部”的枚举值  
        }

        // 边界值  
        float minX = bounds.min.x;
        float maxX = bounds.max.x;
        float minY = bounds.min.y;
        float maxY = bounds.max.y;

        // 检查各个位置  
        if (point.x < minX && point.y > minY && point.y < maxY)
            return E_方向.左;
        if (point.x > maxX && point.y > minY && point.y < maxY)
            return E_方向.右;
        if (point.y < minY && point.x > minX && point.x < maxX)
            return E_方向.下;
        if (point.y > maxY && point.x > minX && point.x < maxX)
            return E_方向.上;

        // 四个角  
        if (point.x < minX && point.y < minY)
            return E_方向.左下;
        if (point.x > maxX && point.y < minY)
            return E_方向.右下;
        if (point.x < minX && point.y > maxY)
            return E_方向.左上;
        if (point.x > maxX && point.y > maxY)
            return E_方向.右上;

        // 如果都不满足，理论上不应该走到这里（除非Bounds设置不正确）  
        return E_方向.Null; // 或者抛出异常  
    }
    public static Vector2 Get_获取碰撞距离(Bounds s, Vector2 point,bool Deb=false)
    {
        float minX = s.min.x;
        float maxX = s.max.x;
        float minY = s.min.y;
        float maxY = s.max.y;
        float Hx = s.size.x / 2;
        float Hy = s.size.y / 2;
        Vector2 C = s.center;

        Vector2 TargetPO = Vector2.zero;

        var a = Get_盒子八方向(s, point);
        if (Deb)
        {
Debug.LogError( a.ToString());
        }
        switch (a)
        {
            case E_方向.Null:
                return Vector2.zero;
            case E_方向.上:
            case E_方向.下:
                TargetPO = new Vector2(C.x, point.y - Hy);
                break;
            case E_方向.左:
            case E_方向.右:
                TargetPO = new Vector2(point.x - Hx, C.y);
                break;
            case E_方向.左上:
            case E_方向.左下:
            case E_方向.右上:
            case E_方向.右下:
                TargetPO = new Vector2(point.x - Hx, point.y - Hy);
                break;
        }

        return TargetPO - C;
    }
    public static List<Vector2> Get_v2(Vector2 center, float distance)
    {
        List<Vector2> points = new List<Vector2>(9);

        // 添加中心点（索引0）
        points.Add(center);

        // 8个方向向量（已归一化）
        Vector2[] directions = new Vector2[8]
        {
            new Vector2(0, 1),      // 上
            new Vector2(1, 1),      // 右上
            new Vector2(1, 0),      // 右
            new Vector2(1, -1),     // 右下
            new Vector2(0, -1),     // 下
            new Vector2(-1, -1),    // 左下
            new Vector2(-1, 0),     // 左
            new Vector2(-1, 1)      // 左上
        };

        // 生成8个方向点
        for (int i = 0; i < 8; i++)
        {
            // 计算方向点位置：中心点 + 方向 * 距离
            Vector2 dirPoint = center + directions[i].normalized * distance;
            points.Add(dirPoint);
        }

        return points;
    }
    public static Vector2Int[] Get_v2_IntArry(this Vector2Int vI)
    {
        Vector2Int[] outt = new Vector2Int[9];
        outt[0] = new Vector2Int(vI.x, vI.y + 1);
        outt[1] = new Vector2Int(vI.x, vI.y);
        outt[2] = new Vector2Int(vI.x, vI.y - 1);
        outt[3] = new Vector2Int(vI.x + 1, vI.y + 1);
        outt[4] = new Vector2Int(vI.x + 1, vI.y);
        outt[5] = new Vector2Int(vI.x + 1, vI.y - 1);
        outt[6] = new Vector2Int(vI.x - 1, vI.y + 1);
        outt[7] = new Vector2Int(vI.x - 1, vI.y);
        outt[8] = new Vector2Int(vI.x - 1, vI.y - 1);
        return outt;
    }
    public static Color Set_Alpha(this Color C, float a)
    {
        return new Color(C.r, C.g, C.b, a);
    }

    /// <summary>
    /// C#获取一个类在其所在的程序集中的所有子类
    /// </summary>
    /// <param name="parentType">给定的类型</param>
    /// <returns>所有子类的名称</returns>
    public static List<string> GetSubClassNames(Type parentType)
    {
        var subTypeList = new List<Type>();
        var assembly = parentType.Assembly;//获取当前父类所在的程序集``
        var assemblyAllTypes = assembly.GetTypes();//获取该程序集中的所有类型
        foreach (var itemType in assemblyAllTypes)//遍历所有类型进行查找
        {
            var baseType = itemType.BaseType;//获取元素类型的基类
            if (baseType != null)//如果有基类
            {
                if (baseType.Name == parentType.Name)//如果基类就是给定的父类
                {
                    subTypeList.Add(itemType);//加入子类表中
                }
            }
        }
        return subTypeList.Select(item => item.Name).ToList();//获取所有子类类型的名称
    }
    public static Vector2 To_角度到方向(float angle)
    {
        // 将角度转换为弧度
        float radian = angle * Mathf.Deg2Rad;

        // 使用余弦和正弦计算方向分量
        return new Vector2(
            Mathf.Cos(radian),
            Mathf.Sin(radian)
        ).normalized; // 二次确保单位长度
    }
    public static float To_方向到角度(Vector2 direction)
    {
        // 规范化输入向量确保为单位向量
        direction.Normalize();

        // 使用Mathf.Atan2计算弧度角（范围：-π 到 π）
        float radian = Mathf.Atan2(direction.y, direction.x);

        // 将弧度转换为角度（0-360度）
        float angle = radian * Mathf.Rad2Deg;
        return angle < 0 ? angle + 360 : angle;
    }
    public static int Get_随机Int()
    {
        var a = Get_随机种子().Next(-2147483648, 2147483647);
        return a;
    }
    public static System.Random Get_随机种子()
    {
        var seed = Guid.NewGuid().GetHashCode();
        System.Random r = new System.Random(seed);
        return r;
    }
    public static void Copy_SpriteRenderto(this SpriteRenderer sp, SpriteRenderer toMe, int 前后 = 0, bool 替换图 = false)
    {
        if (sp == null) Debug.LogError("Sp是空");
        if (toMe == null) Debug.LogError(" toMe是空");
        toMe.sortingLayerID = sp.sortingLayerID;
        toMe.sortingOrder = sp.sortingOrder + 前后;
        if (替换图)
        {
            toMe.sprite = sp.sprite;
        }
    }
    public static void AddUnique<T>(this List<T> list, T item)
    {
        if (!list.Contains(item))
        {
            list.Add(item);
        }
    }
    /// <summary>
    ///  搬运的必须是属性   {get;set;}
    /// </summary>
    /// <param name="_object"></param>
    /// <returns></returns>
    public static object Copy(this object _object)
    {
        Type T = _object.GetType();
        object o = Activator.CreateInstance(T);

        PropertyInfo[] PI = T.GetProperties();
        for (int i = 0; i < PI.Length; i++)
        {
            PropertyInfo P = PI[i];
            P.SetValue(o, P.GetValue(_object));
        }

        return o;
    }
    public static Vector2 右下角()
    {
        return new Vector2(1, -1);
    }
    static List<Vector2> Vector2_L { get; } = new List<Vector2>() {
        Vector2 .down,Vector2.left ,Vector2.right, Vector2.up , Vector2.one, -Vector2.one,右下角(),-右下角()};
    public static List<Vector2> 边上三点(this Bounds B, E_方向 E,bool Deb=false)
    {// v2 里有0    除0外相同  数字
        //v2 没0    xy各为0
        List<Vector2> List = new List<Vector2>();
        var a = E.方向To_v2();
        var X = a.x;
        var Y = a.y;
        if (a == Vector2.zero)
        {
            Debug.LogError("不接受  v2 变量");
        }
        else
        {
            if (X * Y == 0)
            {
                // v2 里有0    除0外相同  数字
                if (X != 0)
                {
                    Vector2_L.ForEach((Vector2 v) => {
                        if (v.x == X)
                        {
                            List.Add(v);
                        }
                    });
                }
                else if (Y != 0)
                {
                    Vector2_L.ForEach((Vector2 v) => {
                        if (v.y == Y)
                        {
                            List.Add(v);
                        }
                    });
                }
                else
                {
                    Debug.LogError("离谱");
                }
            }
            else
            {

                //v2 没0    xy各为0
                List.Add(new Vector2(X, Y));
                List.Add(new Vector2(0, Y));
                List.Add(new Vector2(X, 0));
            }
        }

        for (int i = 0; i < List.Count; i++)
        {

        
            List[i] = B.九个点(List[i].v2_To方向());
            if (Deb) Debug.LogError(List[i]);
        }
        return List;
    }
    public static void 集体开关(this List<Component> 组件列表, bool 开关)
    {
        if (组件列表 == null && 组件列表.Count < 1)
        {
            Debug.LogError("组件为空，或者组件列表长度小于1");
            return;
        }
        for (int i = 0; i < 组件列表.Count; i++)
        {
            var B = 组件列表[i];
            if (B == null) continue;
            if (B is Behaviour)
            {
                ((Behaviour)B).enabled = 开关;
            }
            else if (B is Renderer)
            {
                ((Renderer)B).enabled = 开关;
            }
            else
            {
                Debug.LogError("离谱   该组件怎么关啊" + B);
            }
        }


    }
    /// <summary>
    /// 使用限制     loca 尺寸必须是1 1  必须是某个子物体
    /// 最好不要   极短时间内同个物体反复调用 ，非要这样，要做好维护这个协程和恢复PO的手段
    /// 短时间内快速使用会出现位移
    /// force  默认是1
    /// </summary>
    /// <param name="a"></param>
    /// <param name="t"></param>
    /// <param name="forc"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static IEnumerator Q弹(this Transform t, float forc, float time, bool 改变位置 = true, bool 真实时间 = false, AnimationCurve a = null, bool 还原 = true)
    {
        if (a == null) a = Initialize_Mono.I.defaul_Curve;
        float 一 = 0;
        var 原始尺寸 = new Vector2(t.localScale.x.Sign() * 1, 1);
        var 原始P = t.localPosition;
        while (一 < 1)
        {
            if (真实时间) 一 += Time.fixedUnscaledTime * 1 / time;
            else 一 += Time.fixedDeltaTime * 1 / Player3.Public_Const_Speed / time;

            var f = (a.Evaluate(一) - 0.5f) * forc;
            //Debug.LogError(f + "                              " + 一);
            Initialize.等体积压缩(t, f, 改变位置);
            yield return new WaitForFixedUpdate();
        }
        if (还原) t.localPosition = 原始P;
        t.localScale = 原始尺寸;
    }
    public static 相机框 Get_摄像框(this Transform t)
    {
        var TTT = Physics2D.CircleCastAll(t.position, 1f, Vector2.zero, 0)?.Get_碰撞组<相机框>();
        return TTT;
    }

    public static int Get_摄像框编号(this Transform t)
    {
        var TTT = Physics2D.CircleCastAll(t.position, 0.1f, Vector2.zero, 0)?.Get_碰撞组<相机框>();
        if (TTT == null)
        {
            ((Vector2)t.position).DraClirl(10);
            return -999;
        }
        else
        {
            if (TTT.编号 == -9999)
            {
                Debug.LogError(TTT + "没初始化好");
            }

            return TTT.编号;
        }
    }
    public static IEnumerator Time_State(float time, Action End = null, Action Fixupdate = null, Action Start = null)
    {
        Start?.Invoke();
        float t = Time.time;
        while (Time.time < t + time)
        {
            yield return null;
            Fixupdate?.Invoke();
        }
        End?.Invoke();
    }
    public static RaycastHit2D 碰撞(this Bounds s, int layer, float 加成 = 1)
    {
        var a = Physics2D.BoxCast(s.center, s.size * 加成, 0, Vector2.zero, 0, layer);

        return a;
    }
    public static RaycastHit2D[] 碰撞列表(this Bounds s, int layer, Vector2 size ,bool Deb=false)
    {
        if (s==default)
        {
            return null;
        }
        var a = Physics2D.BoxCastAll(s.center, s.size+ (Vector3)size, 0, Vector2.zero, 0, layer);
        if (Deb)
        {
            s.Dra(Color.white, 3f);
        }
        return a;
    }
    public static RaycastHit2D[] 碰撞列表(this Bounds s, int layer, float 加成 = 1)
    {
        var a = Physics2D.BoxCastAll(s.center, s.size * 加成, 0, Vector2.zero, 0, layer);

        return a;
    }
    public static Color Lerp(this Color color, Color colorNext, float speed)
    {
        color = new Color(
            Mathf.Lerp(color.r, colorNext.r, speed),
            Mathf.Lerp(color.g, colorNext.g, speed),
            Mathf.Lerp(color.b, colorNext.b, speed)
                          );
        return color;
    }
    public static bool _is(this Vector2 a, Vector2 b,bool 或=false, float 精度 = 0.0001f)
    {
        if (或) return a.x._is(b.x) || a.y._is(b.y);
        return a.x._is(b.x)&&a.y._is(b.y);
        //return Mathf.Abs(Mathf.Abs(a) - Mathf.Abs(b)) < 精度;
    }
    public static bool _is(this float a, float b, float 精度 = 0.0001f)
    { 
        return Mathf.Abs(Mathf.Abs(a) - Mathf.Abs(b))    < 精度 ;
    }
    public static void Dra(this Bounds B, Color C = default,float time =0.1f)
    {
        if (C == default) C = Color.red;
        Debug.DrawLine(B.min, B.九个点(E_方向.左上), C, time);
        Debug.DrawLine(B.九个点(E_方向.左上), B.max, C, time);
        Debug.DrawLine(B.max, B.九个点(E_方向.右下), C, time);
        Debug.DrawLine(B.九个点(E_方向.右下), B.min, C, time);
    }
    public static void DraClirl(this Vector3 o, float 距离 = 0.1f, Color C = default, float time = 0.1f)
    {
        DraClirl((Vector2)o, 距离, C, time);
    }
    public static void DraClirl(this Vector2 o, float 距离 = 0.1f, Color C = default, float time = 0.1f)
    {
        if (C == Color.white * 0f) C = Color.red;
        if (Initialize_Mono.I.显示点位置) Debug.Log(o + " 一下");
        var 二一_ = new Vector2(2, 1);
        var 二一 = new Vector2(-2, 1);

        var 一二_ = new Vector2(-1, 2);
        var 一二 = new Vector2(1, 2);

        var ZZ = new Vector2(1, -1);
        Debug.DrawRay(o, 一二_.normalized * 距离, C, time);
        Debug.DrawRay(o, -一二_.normalized * 距离, C, time);
        Debug.DrawRay(o, 一二.normalized * 距离, C, time);
        Debug.DrawRay(o, -一二.normalized * 距离, C, time);

        Debug.DrawRay(o, 二一_.normalized * 距离, C, time);
        Debug.DrawRay(o, -二一_.normalized * 距离, C, time);
        Debug.DrawRay(o, 二一.normalized * 距离, C, time);
        Debug.DrawRay(o, -二一.normalized * 距离, C, time);

        Debug.DrawRay(o, Vector2.up.normalized * 距离, C, time);
        Debug.DrawRay(o, Vector2.down.normalized * 距离, C, time);
        Debug.DrawRay(o, Vector2.left.normalized * 距离, C, time);
        Debug.DrawRay(o, Vector2.right.normalized * 距离, C, time);
        Debug.DrawRay(o, Vector2.one.normalized * 距离, C, time);
        Debug.DrawRay(o, Vector2.one.normalized * -1 * 距离, C, time);
        Debug.DrawRay(o, ZZ.normalized * 距离, C, time);
        Debug.DrawRay(o, ZZ.normalized * -距离, C, time);
    }
    public static int Sign(this float a)
    {
        if (a > 0) return 1;
        else if (a < 0) return -1;
        else return 0;
    }
    public static T Get_碰撞组<T>(this RaycastHit2D[] Hit) where T : Component
    {
        Component Out = null;
        for (int i = 0; i < Hit.Length; i++)
        {
            var c = Hit[i].collider;
            if (c != null)
            {

                Out = c.GetComponent<T>();
                if (Out != null) return (T)Out;

            }
        }
        return (T)Out;
    }
    /// <summary>
    /// 根据碰撞盒子 和 方向 和 目标点 判断目标点是否在碰撞盒子指定范围内
    /// V表示了正方形的9个范围  0表示中间 1，1表示右上角  -1，0表示左边中间
    /// -1，0表示 pos坐标的X是否小于盒子最小X Y是否小于盒子最大Y大于最小Y  （bounds.min和bounds.max）
    /// 1，1表示  pos坐标的X是否 大于盒子最大X Y是否大于盒子最大Y   （bounds.min和bounds.max）
    /// </summary>
    /// <param name="B">自己的碰撞盒子</param>
    /// <param name="V">检测的目标  Xy分量只能是正负一或者0</param>
    /// <param name="pos"> 目标点  </param>
    /// <returns>目标点是否在指定范围内</returns>
    public static bool is_Boun判断(Bounds B, Vector2Int V, Vector3 pos)
    {
        // 第一步：校验V的合法性（仅允许-1/0/1）
        //if (V.x < -1 || V.x > 1 || V.y < -1 || V.y > 1)
        //{
        //    Debug.LogError("Vector2Int V的X/Y分量只能是-1、0、1");
        //    return false;
        //}

        // 第二步：提取碰撞盒子的X/Y轴极值（忽略Z轴，按2D逻辑处理）
        float boundsMinX = B.min.x;
        float boundsMaxX = B.max.x;
        float boundsMinY = B.min.y;
        float boundsMaxY = B.max.y;

        // 第三步：拆解目标点的X/Y坐标
        float targetX = pos.x;
        float targetY = pos.y;

        // 第四步：分别判断X、Y轴是否符合V指定的范围规则
        bool xCheck = false;
        bool yCheck = false;

        // X轴判断逻辑
        switch (V.x)
        {
            case -2:
                return targetX < boundsMinX;
            case 2:
               return targetX > boundsMaxX;
            case -1: // 目标点X < 盒子最小X（左边）
                xCheck = targetX < boundsMinX;
                break;
            case 0: // 目标点X 在盒子X范围内（中间）
                xCheck = targetX >= boundsMinX && targetX <= boundsMaxX;
                break;
            case 1: // 目标点X > 盒子最大X（右边）
                xCheck = targetX > boundsMaxX;
                break;
        }

        // Y轴判断逻辑
        switch (V.y)
        {
            case -2: 
                return targetY < boundsMinY;
            case 2: 
                return targetY > boundsMaxY;
            case -1: // 目标点Y < 盒子最小Y（下边）
                yCheck = targetY < boundsMinY;
                break;
            case 0: // 目标点Y 在盒子Y范围内（中间）
                yCheck = targetY >= boundsMinY && targetY <= boundsMaxY;
                break;
            case 1: // 目标点Y > 盒子最大Y（上边）
                yCheck = targetY > boundsMaxY;
                break;

        }

        // 第五步：X和Y轴都满足时，返回true
        return xCheck && yCheck;
    }
    /// <summary>
    ///  任意符号相同返回true
    ///  
    /// </summary>
    /// <param name="v"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static bool is_四方向比较(this Vector2Int v, Vector2Int target)
    {
        if (v.x == 0 || v.y == 0 || target.x == 0 || target.y == 0)
        {
            Debug.Log("返回0了这不科学" + v + "          " + target);
        }
        bool Out = v.x == target.x || v.y == target.y;

        return Out;
    }  
         /// <summary>
         /// 返回9个方向
         /// </summary>
         /// <param name="我"></param>
         /// <param name="你"></param>
         /// <returns></returns>
    public static Vector2Int 你在我的哪里(this Vector3 我, Vector3 你,float f=0f, bool Deb = false)
    {
        Vector2 cha = 你 - 我;
        if (Deb)
        {
            我.DraClirl(2, Color.white, 1);
            你.DraClirl(2, Color.blue, 1);
            Debug.LogError(cha);
        }
        var x = cha.x > 0 ? 1 : -1;
        var y = cha.y > 0 ? 1 : -1;

        if (Mathf.Abs(cha.x)   < f) x = 0;
        if (Mathf.Abs(cha.y) < f) y = 0;
        return new Vector2Int(x, y);
    }
    public static Vector2Int 你在我的哪里(this Vector3  我, Transform 你, float f = 0.001f, bool Deb = false)
    {
        return 你在我的哪里(我, 你.position,f,Deb);
    } 
    public static Vector2Int 你在我的哪里(this Transform 我, Transform 你, float f = 0.001f, bool Deb=false)
    { 
        return 你在我的哪里(我.position,你,f,Deb);
    } 
    static int ASD(float t)
    {
        var f = Mathf.Abs(t);
        if (t < 1)
        {
            return 1;
        }
        else
        {
            return Mathf.RoundToInt(t);
        }
    }
    public static List<Vector2> 阵列盒子(this Bounds 销毁盒子)
    {
        List<Vector2> Out = new List<Vector2>();

        var min = 销毁盒子.min;
        int X = ASD(销毁盒子.size.x);
        int Y = ASD(销毁盒子.size.y);


        float h_X = 销毁盒子.size.x / X;
        float h_Y = 销毁盒子.size.y / Y;
        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j < Y; j++)
            {
                var r = new Vector2(min.x + (i * h_X) + h_X / 2, min.y + (j * h_Y) + h_Y / 2);

                Out.Add(r);
            }
        }
        return Out;
    }
    public static GameObject 射线检测排除自己(this GameObject Self, RaycastHit2D[] S)
    {
        GameObject Out = null;
        for (int i = 0; i < S.Length; i++)
        {
            RaycastHit2D hit = S[i];
            GameObject o = hit.collider.gameObject;
            if (o != Self)
            {
                return o;
            }
        }
        Debug.LogError("空空空");
        return Out;
    }
    public static Vector2 Set_Y(this Vector2 v, float y)
    {
        return new Vector2(v.x, y);
    }
    public static Vector2 Set_X(this Vector2 v, float x)
    {
        return new Vector2(x, v.y);
    }

    /// <summary>
    /// 输入   21，20，30    输出0.1   
    /// </summary>
    /// <param name="value"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float ScaleValue(float value, float min, float max)
    {
        // 检查max是否大于min  
        if (max <= min)
        {
            return value;
            //throw new ArgumentException("Max value must be greater than min value.", nameof(max));
        }

        // 检查value是否在min和max之间  
        if (value <= min)
        {
            return 0; // 根据你的要求返回min，但通常应返回一个错误指示值  
        }
        else if (value >= max)
        {
            return 1; // 根据你的要求返回max，但通常应返回一个错误指示值  
        }
        else
        {
            // 计算并返回value在min和max之间的相对位置  
            return (value - min) / (max - min);
        }
    }
    /// <summary>
    /// 输入   1 ，5，-5，  输出0.2
    /// </summary>
    /// <param name="value"></param>
    /// <param name="max"></param>
    /// <param name="min"></param>
    /// <returns></returns>
    public static float 位置Value(float value, float F)
    {
        // 检查F是否合法（非零且非负）  
        if (F <= 0)
        {
            Debug.LogError("F小于0        值为      " + F);
        }
        if (value == 0)
        {
            return 0;
        }
        // 当value小于等于1/F时，返回-1  
        if (value <= 1 / F)
        {
            return -1;
        }

        // 当value大于等于F时，返回1  
        if (value >= F)
        {
            return 1;
        }

        // 当value等于1时，返回0  
        if (Math.Abs(value - 1) < float.Epsilon)
        {
            return 0;
        }

        // value在1和F之间时，计算位置  
        if (value > 1 && value < F)
        {
            return (value - 1) / (F - 1);
        }

        // value在1和1/F之间时，计算位置  
        if (value > 1 / F && value < 1)
        {
            return ((value - (1)) / (1 - (1 / F)));
        }

        // 如果value不在上述范围内，则理论上不会执行到这里（除非F非常接近0或1）  
        // 但为了完整性，可以抛出一个异常或返回一个错误值  
        throw new ArgumentException("离谱");
    }

    public static E_方向 v2_To方向(this Vector2 v2)
    {
        // 假设v2的x和y分量只能是1, 0, -1  
        if (v2 == Vector2.zero)
        {
            return E_方向.Null;
        }

        if (v2.x == 0)
        {
            if (v2.y == 1)
            {
                return E_方向.上;
            }
            else
            {
                return E_方向.下;
            }
        }
        else if (v2.y == 0)
        {
            if (v2.x == 1)
            {
                return E_方向.右;
            }
            else
            {
                return E_方向.左;
            }
        }
        else
        {
            if (v2.x == 1)
            {
                if (v2.y == 1)
                {
                    return E_方向.右上;
                }
                else
                {
                    return E_方向.右下;
                }
            }
            else
            {
                if (v2.y == 1)
                {
                    return E_方向.左上;
                }
                else
                {
                    return E_方向.左下;
                }
            }
        }
    }
    public static Vector2Int 方向To_v2(this E_方向 e)
    {
        switch (e)
        {
            case E_方向.Null:
                return Vector2Int.zero;
            case E_方向.上:
                return Vector2Int.up;
            case E_方向.下:
                return Vector2Int.down;
            case E_方向.左:
                return Vector2Int.left;
            case E_方向.右:
                return Vector2Int.right;
            case E_方向.左上:
                return new Vector2Int(-1, 1);
            case E_方向.左下:
                return Vector2Int.one * -1;
            case E_方向.右上:
                return Vector2Int.one;
            case E_方向.右下:
                return new Vector2Int(1, -1);
            case E_方向.上上:
                return Vector2Int.up*2; 
            case E_方向.下下:
                return Vector2Int.down * 2;
            case E_方向.左左:
                return Vector2Int.left * 2;
            case E_方向.右右:
                return Vector2Int.right * 2;
        }
        return Vector2Int.zero;
    }
    public static Vector2 九个点(this Bounds B, E_方向 f)
    {

        Vector2 c = B.center;
        float x = B.size.x / 2;
        float y = B.size.y / 2;
        switch (f)
        {
            case E_方向.上:
                x = 0;
                break;
            case E_方向.下:
                y = -y;
                x = 0;
                break;
            case E_方向.左:
                x = -x;
                y = 0;
                break;
            case E_方向.右:
                y = 0;
                break;
            case E_方向.左上:
                x = -x;
                break;
            case E_方向.左下:
                return B.min;
            case E_方向.右上:
                return B.max;
            case E_方向.右下:
                y = -y;
                break;
            case E_方向.Null:
                break;
            case E_方向.上上:
                break;
            case E_方向.下下:
                break;
            case E_方向.左左:
                break;
            case E_方向.右右:
                break;
        }
        c += new Vector2(x, y);
        return c;
    }
    /// <summary>
    /// 四方向
    /// </summary>
    /// <param name="v"></param>
    /// <param name="方向"></param>
    /// <returns></returns>
    public static bool Is_方向(Vector2 v, E_方向 方向)
    {
        switch (方向)
        {
            case E_方向.上:
                if (v.y > 0) return true;
                break;
            case E_方向.下:
                if (v.y <= 0) return true;
                break;
            case E_方向.左:
                if (v.x >= 0) return true;
                break;
            case E_方向.右:
                if (v.x <= 0) return true;
                break;
        }
        return false;
    }

    /// <summary>
    /// Obj 时间
    /// </summary>
    public static string Obj_E { get; } = "Obj_E";
    public static LayerMask L_小地图 { get; } = LayerMask.NameToLayer("小地图");//Air_wall
    public static LayerMask L_Air_wall { get; } = LayerMask.NameToLayer("Air_wall");//Air_wall
    /// <summary>
    /// 旧的脚踩箱 
    /// 能量子弹，和玩家跟Ground交互   脚踩层
    /// </summary>
    public static LayerMask L_Box_Ground { get; } = LayerMask.NameToLayer("Box_Ground");

    public static LayerMask L_Enemy_hit_collision { get; } = LayerMask.NameToLayer("Enemy_hit_collision");
    public static LayerMask L_M_Ground { get; } = LayerMask.NameToLayer("M_Ground");
    /// <summary>
    /// 只有Player  有碰撞
    /// </summary>
    public static LayerMask L_Default { get; } = LayerMask.NameToLayer("Default");
    public static LayerMask L_Only_Ground { get; } = LayerMask.NameToLayer("Only_Ground");
    public static LayerMask L_Enemy { get; } = LayerMask.NameToLayer("Enemy");
    public static LayerMask Only_Player { get; } = LayerMask.NameToLayer("Only_Player");

    public static LayerMask L_Player { get; } = LayerMask.NameToLayer("Player");
    public static LayerMask L_Ladder { get; } = LayerMask.NameToLayer("Ladder");
    public static int S_小地图 { get; } = SortingLayer.NameToID("小地图");

    public static LayerMask L_No_Player { get; } = LayerMask.NameToLayer("No_Player");
    public static LayerMask L_Null { get; } = LayerMask.NameToLayer("Null");
    public static LayerMask L_Ground { get; } = LayerMask.NameToLayer("Ground");
    /// <summary>
    /// Tag
    /// </summary>
    public static string MovePlatform { get; } = "MovePP";
    public static string Player { get; } = "Player";
    public static string Ground { get; } = "Ground";
    public static string One_way { get; } = "One_way";
    public static string Vertical { get; } = "Vertical";
    public static string Horizontal { get; } = "Horizontal";
    public static string BagSwitch { get; } = "BagSwitch";
    public static string Bag { get; } = "Bag";
    public static string Exite { get; } = "Cancel";
    public static string Enter { get; } = "Submit";


    public static float 打腿 { get; private set; } = 0.5f;
    public static float 打胸 { get; private set; } = 2.5f;

    private static string scenePath { get; } = "Scenes";
    private static string MateriaPath { get; } = "Material";
    static System.Random random = new System.Random();
    // 生成一个随机整数，范围在[min, max)  
    public static Vector2 MoveToPosition(Vector2 My, Vector2 targetPosition, float distance)
    {

        // 计算目标位置与当前位置之间的方向向量  
        Vector2 direction = (targetPosition - My).normalized;
        // 根据方向和距离计算新的位置  
        return My + direction * distance;

    }

    /// <summary>
    /// 0   为不变  返回Y坐标改变值
    /// </summary>
    /// <param name="t"></param>
    /// <param name="v"></param>
    public static float 等体积压缩(Transform t, float v, bool 改变位置 = true)
    {

        var y = 1 + v;
        var x = 1 - v;

        if (Mathf.Abs(t.localScale.y) == y) return 0;

        var cha = t.localScale.y - y;
        var py = t.localPosition.y - cha * 2.5f;

        if (改变位置) t.localPosition = new Vector2(t.localPosition.x, py);
        t.localScale = new Vector2(Mathf.Sign(t.localScale.x) * x, y);


        return py;
        //if (v == 0) return;
        //var y = 1 + v;
        //var x = 1 - v;

        //if (Mathf.Abs(t.localScale.y) == y) return;

        //var cha = t.localScale.y - y;
        //var py = t.localPosition.y - cha / 2;

        //if (改变位置) t.localPosition = new Vector2(t.localPosition.x, py);
        //t.localScale = new Vector2(Mathf.Sign(t.localScale.x) * x, y);


    }
    public static int 返回正负号(float value)
    {
        if (value >= 0) return 1;
        else if (value < 0) return -1;
        else return 0;
    }
    /// <summary>
    /// 包含否
    /// </summary> 
    public static bool Layer_is(int layer, LayerMask lm)
    {
        ///如果一个层 obj.layer 是 4
        //        layermask 是 0001 0010，也就是 18
        //（1 << 4） &18 就会是 0001 0000
        bool B = ((1 << layer) & lm.value) > 0;
        return B;
    }
    /// <summary>
    /// 每次使用都会会获取一次
    /// </summary>

    public static float Lerp均衡插值(float b误差值, int n步数)
    {

        var b = 1 - Mathf.Pow(b误差值, 1 / n步数);
        int 次数 = 1;
        while (b == 1)
        {

            b = 1 - Mathf.Pow(b误差值, 1 / (float)n步数);
            n步数++;
            次数++;
            Debug.LogError(1 / (float)n步数);
            if (次数 > 10)
            {
                break;
            }
        }
        if (b == 1)
        {
            Debug.LogError("卧槽" + b误差值 + "" + 1 / (float)n步数);
        }
        return 1 - Mathf.Pow(b误差值, 1 / (float)n步数);
    }
    /// <summary>
    ///  返回值的范围包括minValue但不包括maxValue
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int RandomInt(int min, int max)
    {
        return random.Next(min, max);
    }
    public static bool Vector2Int比较(Vector2 a, Vector2 b, bool bug = false)
    {
        if (bug)
        {
            Debug.LogError(a.x + "_" + a.y + "\n          " + (int)a.x + "_" + (int)b.x + "\n         " + (int)a.y + "_" + (int)b.y);
        }

        return (int)a.x == (int)b.x && (int)a.y == (int)b.y;
    }
    public static string Get_CutternAnimName(Animator a)
    {
        return a.GetCurrentAnimatorClipInfo(0)[0].clip.name; ;
    }
    public static List<GameObject> 扇形检测(Vector2 原点, float 距离, float 角度, float 扇形角度, int 精度, LayerMask layerMask, bool 穿透 = false, bool Deb = false)
    {
        List<GameObject> detectedObjects = new List<GameObject>();
        float angleStep = 扇形角度 / (精度 - 1);

        for (int i = 0; i < 精度; i++)
        {
            float currentAngle = 角度 - 扇形角度 / 2f + angleStep * i;
            Vector2 rayDirection = new Vector2(Mathf.Sin(currentAngle * Mathf.Deg2Rad), Mathf.Cos(currentAngle * Mathf.Deg2Rad));

#if UNITY_EDITOR
            if (Deb) Debug.DrawRay(原点, rayDirection * 距离, Color.green);
#endif

            RaycastHit2D[] hits = Physics2D.RaycastAll(原点, rayDirection, 距离, layerMask);

            foreach (RaycastHit2D hit in hits)
            {
                if (Deb)
                {
                    hit.point.DraClirl();
                }
                GameObject hitObject = hit.collider.gameObject;

                // 如果物体不在已检测列表中，则添加  
                if (!detectedObjects.Contains(hitObject))
                {
                    detectedObjects.Add(hitObject);
                }
                if (!穿透) break;
            }

        }

        return detectedObjects;
    }
    //    public static List<GameObject> 扇形检测(Vector2 原点, float 距离, float 角度, float 扇形角度, int 精度, LayerMask layerMask)
    //    {
    //        List<GameObject> detectedObjects = new List<GameObject>();
    //        float angleStep = 扇形角度 / (精度 - 1);

    //        for (int i = 0; i < 精度; i++)
    //        {
    //            float currentAngle = 角度 - 扇形角度 / 2f + angleStep * i;
    //            Vector2 rayDirection = new Vector2(Mathf.Sin(currentAngle * Mathf.Deg2Rad), Mathf.Cos(currentAngle * Mathf.Deg2Rad));
    //            RaycastHit2D hit = Physics2D.Raycast(原点, rayDirection, 距离, layerMask);
    //#if UNITY_EDITOR
    //            Debug.DrawRay(原点, rayDirection* 距离, Color.green );
    //#endif
    //            if (hit.collider != null)
    //            {
    //                GameObject hitObject = hit.collider.gameObject;
    //                if (!detectedObjects.Contains(hitObject))
    //                {
    //                    detectedObjects.Add(hitObject);

    //                    // 可选：在编辑器模式下绘制射线  

    //                }

    //                // 检查是否为地面层，并中断后续检测  
    //                if (hitObject.layer == LayerMask.NameToLayer("Ground"))
    //                {
    //                    break;
    //                }
    //            }
    //        }

    //        return detectedObjects;
    //    }


    /// <summary>
    /// 忽略碰撞
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="target"></param>
    public static void Set_碰撞(LayerMask a, LayerMask b, bool target)
    {

        Physics2D.IgnoreLayerCollision(a, b, target);
    }
    public static bool Get_碰撞(LayerMask a, LayerMask b)
    {
        return Physics2D.GetIgnoreLayerCollision(a, b);
    }
    public static int GetNumOfString(string str)
    {
        int num;
        string strNum = System.Text.RegularExpressions.Regex.Replace(str, @"[^0-9]+", "");
        if (strNum == "") return 0;
        num = int.Parse(strNum);
        return num;
    }
    public static IEnumerator Waite(Action a)
    {
        yield return null;
        a.Invoke();
    }
    public static string _Color(this object o, Color c)
    {
        var a = UnityEngine.ColorUtility.ToHtmlStringRGBA(c);
        return "<color=#" + a + ">" + o + "</color>";
    }
    public static Vector2 转换进去(int i, int Const)
    {
        var Y = i / Const + 1;
        var X = i % Const + 1;
        return new Vector2(X, Y);
    }
    public static int 转换出去(Vector2 v2, int Const)
    {
        var I = ((v2.y - 1) * Const) + v2.x - 1;
        return (int)I;
    }

    public static float TimeScale
    {
        get => Time.timeScale; set
        { Time.timeScale = value; }
    }
    public static void 时间暂停()
    {
        TimeScale = 0;
    }


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void 时间恢复()
    {
        if (Initialize_Mono.I == null || Initialize_Mono.I.gameObject == null)
        {
            Debug.LogError("那里调用的");
            return;
        }
        if (Initialize_Mono.I.时缓协程 != null)
            Initialize_Mono.I.StopCoroutine(Initialize_Mono.I.时缓协程);

        TimeScale = 1;
    }

    public static Transform[] 获取同级物体(GameObject obj)
    {
        var p = obj.transform.parent;

        return p.GetComponentsInChildren<Transform>();
    }
    public static void 设置和当前活动场景为这个obj的场景(GameObject obj)
    {

        SceneManager.SetActiveScene(obj.scene);
    }
    public static float 屏幕横纵比
    {
        get
        {
            return (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        }
    }
    public static bool V2比较_A大于B(Vector2 A, Vector2 B)
    {
        return !(B.x > A.x || B.y > A.y);
    }
    public static Vector2 GetCarmeraSize(this Camera camera, float zDepth)
    {
        // 计算半高：使用 GetCarmeraAngle2_SIze 方法的逻辑
        float halfHeight = zDepth * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);

        // 计算半宽
        float halfWidth = halfHeight * camera.aspect;

        // 返回完整尺寸：宽度 = 半宽 * 2，高度 = 半高 * 2
        return new Vector2(halfWidth * 2f, halfHeight * 2f);
    }
    /// <summary>
    /// GetAngle反函数   知道角度求尺寸
    /// </summary>
    /// <param name="W"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static float GetCarmeraAngle2_SIze(float W, float angle)
    {
        return W * Mathf.Tan(angle / 2 * Mathf.Deg2Rad) ;
    }
    /// <summary>
    /// Ver  竖向整尺寸转换摄像机 FOV纵向角度
    /// </summary>
    /// <param name="W"></param>
    /// <param name="Hight"></param>
    /// <returns></returns>
    public static float GetSize2CarmeraAngle( float Hight,float W,bool b=false)
    {
        if (b) Debug.LogError(Hight + "    " + W);
        float angleA = Mathf.Atan2(W, Hight  ) * Mathf.Rad2Deg;

        if (b) Debug.LogError(angleA);
        return (90 - angleA) * 2;
    }
    /// <summary>
    /// /返回为零说明现有的更大
    /// </summary>
    /// <param name="现有的碰撞框Size"></param>
    /// <param name="摄像机目标Size"></param>
    /// <returns></returns>
    public static float 返回兼容相机碰撞框的摄像机尺寸(Vector2 现有的碰撞框Size, float 摄像机目标Size,float W=0)
    {

        Vector2 摄像机目标尺寸;
        if (W!=0)
        {
            ///W 不为0 代表FOV
            ///FOV转换成尺寸
            摄像机目标Size = GetCarmeraAngle2_SIze(摄像机目标Size, W)/2;
            摄像机目标尺寸 = new Vector2(摄像机目标Size * 屏幕横纵比, 摄像机目标Size) * 2;
            Debug.LogError(摄像机目标尺寸+"    "+ 摄像机目标Size);
        }

        ///68  34
        //得出实际摄像机想要的尺寸
        摄像机目标尺寸 = new Vector2(摄像机目标Size * 屏幕横纵比, 摄像机目标Size) * 2;

        float X = 0, Y = 0;
        if (摄像机目标尺寸.x >= 现有的碰撞框Size.x)
        {
            X = 现有的碰撞框Size.x / 屏幕横纵比 / 2;
        }
        if (摄像机目标尺寸.y >= 现有的碰撞框Size.y)
        {
            Y = 现有的碰撞框Size.y / 2;
        }

        ///XY 都有
        if (X != 0 && Y != 0)
        { 
            return Mathf.Min(X, Y) - 0.0001f;
        }
        else if (X == Y && X == 0)
        { 
            //Debug.LogError("为甚么呀" + 摄像机目标尺寸+"       "+ Target_OrthographicSize+ "     碰撞框  " + 现有的碰撞框Size);
            ///目标尺寸小于目标尺寸
            return 0;
        }
        ///XY 一个有
        else
        { 
            return Mathf.Max(X, Y) - 0.0001f;
        }
    }
    public static GameObject 获取已加载场景根节点的TAG是的(string tag)
    {

        var a = 获取已加载场景中所有根节点的obj();
        foreach (var item in a)
        {
            if (item.CompareTag(tag)  )
            {
                return item;
            }
        }
        return null;

    }
    public static List<GameObject> 获取已加载场景中所有根节点的obj()
    {//该方法在场景未加载完时，未加载的场景返回的OBJ列表为空,因此要放在UPdate里
        List<GameObject> G = new List<GameObject>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene a = SceneManager.GetSceneAt(i);
            if (a != null)
            {
                foreach (var item in a.GetRootGameObjects())
                {
                    G.Add(item);
                }
            }
        }

        return G;
    }
    /// <summary>
    /// 判断依据是某某场景的跟OBJ数组长度
    /// </summary>
    /// <returns></returns>
    public static bool 所有的场景都加载完了嘛()
    {

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene a = SceneManager.GetSceneAt(i);
            if (a != null)
            {
                if (a.GetRootGameObjects().Length == 0)
                {
                    return false;
                }
            }
        }
        return true;
    }

    //public static PolygonCollider2D 获取场景中的相机碰撞箱子(GameObject  obj)
    //{
    //    PolygonCollider2D 相机碰撞框1 =null;
    //    foreach (var item in obj.scene.GetRootGameObjects())
    //    { 
    //        相机碰撞框1 = item.GetComponent<相机框>()?.碰撞框_;
    //        if (相机碰撞框1 != null) break; 
    //    }
    //    if (相机碰撞框1 == null)
    //    {
    //        Debug.LogError(obj.name +"的场景获取碰相机碰撞箱失败");
    //    } 
    //    return 相机碰撞框1;
    //}
    //static  public GameObject Tag获取GameObject(string  tag)
    // {
    //     var B = GameObject.FindGameObjectsWithTag(tag);
    //     if (B.Length > 0)
    //     {
    //         return B[0];
    //     }
    //     else
    //     {
    //         return null;
    //     }
    // }
    public static float GetAngle(Vector2 from_, Vector2 to_)
    {
        //两点的x、y值  
        float x = to_.x - from_.x;
        float y = to_.y - from_.y;

        //斜边长度  
        float hypotenuse = Mathf.Sqrt(Mathf.Pow(x, 2f) + Mathf.Pow(y, 2f));

        //求出弧度  
        float cos = x / hypotenuse;
        float radian = Mathf.Acos(cos);

        //用弧度算出角度  
        float angle = 180 / (Mathf.PI / radian);

        if (y < 0)
        {
            angle = 360 - angle;
        }
        if ((y == 0) && (x < 0))
        {
            angle = 180;
        }
        return angle;
    }

    //public static void RefreshAllScene()
    //{
    //    string path = Path.Combine(Application.dataPath, scenePath);
    //    string[] files = Directory.GetFiles(path, "*.unity", SearchOption.AllDirectories);
    //    EditorBuildSettingsScene[] scenes = new EditorBuildSettingsScene[files.Length];
    //    for (int i = 0; i < files.Length; ++i)
    //    {
    //        int index = files[i].IndexOf("Assets");
    //        string _path = files[i].Remove(0, index);
    //        scenes[i] = new EditorBuildSettingsScene(_path, true);
    //    }
    //    EditorBuildSettings.scenes = scenes;
    //}
    public static Vector3 朝向对象(GameObject my, GameObject target)
    {
        var a = my.transform.position.x;
        var b = target.transform.position.x;
        int I = a - b > 0 ? -1 : 1;
        return new Vector3(I, 1, 1);
    }
    //public static void 闪光(this SpriteRenderer sp, float time, bool b = true)
    //{

    //    //sp.color = Color.white;
    //    sp.material.SetColor(材质管理._SpriteColor, Color.white);
    //    Initialize_Mono.I.Waite(() => {
    //        if (sp != null) 
    //            sp.material.SetColor(材质管理._SpriteColor, new Color(1, 1, 1, 0));
    //    }
    //    , time, b
    //    ); 
    //}


    public static void 闪光(this SpriteRenderer sp, float time, bool b = true)
    { 
        Material  M= sp.sharedMaterial;
         
        if (M.name == 材质管理.闪光) return;
       
        sp.material = 材质管理.Get_Material(材质管理.闪光); 
        Initialize_Mono.I.Waite(() =>
        { 
            sp.material = M;
        }
        ,time
        ,b 
        );
    }
    //IEnumerator
    public static Color 透明= new Color(1, 1, 1,1);

    public static Vector2 返回和对方相反方向的标准力(Vector2 m, Vector2 y)
    {
        float o = (m - y).x;
        float p = (m - y).y;
        o = o > 0 ? 1 : -1;
        p = p > 0 ? 1 : -1;
        //Debug.LogError(你+"          "+我+o+"         "+p);
        return new Vector2(o, p);
    }
    public static Vector2 返回和对方相反方向的标准力(GameObject my, GameObject Target)
    {
        Vector2 你 = Target.transform.position;
        Vector2 我 = my.transform.position;

        //Debug.LogError(你+"          "+我+o+"         "+p);
        return 返回和对方相反方向的标准力(我, 你);
    }
    public static void 向目标水平移动(float 速度, GameObject gameObject, GameObject target)
    {
        if (gameObject.GetComponent<Rigidbody2D>() == null)
        {
            gameObject.AddComponent<Rigidbody2D>();
        }
        int 朝向;
        var a = target.transform.position.x - gameObject.transform.position.x;
        if (a <= 0)
        {
            朝向 = -1;
        }
        else
        {
            朝向 = 1;
        }

        //gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(  速度* 朝向, 0f);
        gameObject.transform.Translate(new Vector2(朝向 * 速度, 0));
    }
    public static void 向目标上下移动(float 速度, GameObject gameObject, GameObject target)
    {
        if (gameObject.GetComponent<Rigidbody2D>() == null)
        {
            gameObject.AddComponent<Rigidbody2D>();
        }
        int 朝向;
        var a = target.transform.position.y - gameObject.transform.position.y;
        if (a <= 0)
        {
            朝向 = -1;
        }
        else
        {
            朝向 = 1;
        }
        //gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 朝向 * 速度);
        //gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 朝向 * 速度);
        gameObject.transform.Translate(new Vector2(0, 朝向 * 速度));
    }
    public static bool 方向_A是否在B的左边或者下面(Vector2 A, Vector2 B, bool true就是算Y_flase就是X)
    {
        Vector2 O = A;
        Vector2 P = B;

        if (true就是算Y_flase就是X)
        {
            return O.y - P.y < 0;
        }
        else
        {
            return O.x - P.x < 0;
        }
    }
    public static bool 方向_A是否在B的左边或者下面(GameObject A, GameObject B, bool true就是算Y_flase就是X)
    {
        Vector2 O = A.transform.position;
        Vector2 P = B.transform.position;
        if (true就是算Y_flase就是X)
        {
            return O.y - P.y < 0;
        }
        else
        {
            return O.x - P.x < 0;
        }
    }

    /// <summary>
    /// A是不是在B的旁边
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <param name="true就是算Y_flase就是X"></param>
    /// <returns></returns>
    public static bool 接近_判断A减B的绝对距离是否小于等于_范围(GameObject A, GameObject B, float 范围, bool true就是算Y_flase就是X)
    {
        Vector2 O = A.transform.position;
        Vector2 P = B.transform.position;
        if (true就是算Y_flase就是X)
        {
            return MathF.Abs(O.y - P.y) <= 范围;
        }
        else
        {
            return MathF.Abs(O.x - P.x) <= 范围;
        }
    }
    public static void 检测移动平台下落(BiologyBase B)
    {
        var po = B.co;
        var DD = Physics2D.BoxCast(
new Vector2(po.bounds.center.x, po.bounds.min.y),
new Vector2(po.bounds.size.x - 0.5f, 0.1f),
0f,
Vector2.down,
0.5f,
1 << LayerMask.NameToLayer("Ground")
)
.collider;
        if (DD != null)
        {
            if (DD.GetComponent<I_Speed_Change>() != null)
            {
                if (!B.Ground)
                {

                    if (B.Velocity.y <= 0)
                    {
                        float ca = Initialize.获取两碰撞体最近方向的插值(B.gameObject, DD.gameObject);
                        B.transform.position = new Vector2(B.transform.position.x, B.transform.position.y - ca);
                    }

                }
            }
        }
    }
    public static float 获取两碰撞体最近方向的插值(GameObject a, GameObject b)
    {
        if ((a.transform.position.y - b.transform.position.y > 0))
        {
            //A在B的上面
            float A = a.GetComponent<Collider2D>().bounds.min.y;
            float B = b.GetComponent<Collider2D>().bounds.max.y;
            return A - B;
        }
        else if (a.transform.position.y - b.transform.position.y < 0)
        {    //B在A的上面
            float A = a.GetComponent<Collider2D>().bounds.max.y;
            float B = b.GetComponent<Collider2D>().bounds.min.y;
            return A - B;
        }
        else
        {
            return 0;
        }
    }
    /// <summary>
    /// 添加组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="gb"></param>
    /// <param name="co"></param>
    public static void 组件<T>(this GameObject gb, ref T co) where T : Component
    {
        if (co != null) return;
        co = gb.GetComponent<T>();
        if (co == null)
        {
            co = gb.gameObject.AddComponent<T>();
        }
    }

}


public class Event_M
{
    public static string 刷新提示机关 { get; } = "刷新提示机关";
    public static string 扫把打到了 { get; } = "扫把打到了";
  
    public static string 场景保存触发 { get; } = "场景保存触发";
    public static string 剧情触发 { get; } = "剧情触发";
    public static string 对话退出 { get; } = "对话退出";
    public static string 切换场景触发_obj { get; } = "切换场景触发";
    internal static string 对话触发_OBJ { get; } = "对话";
    internal static string UI回到战斗 { get; } = "退回";

    public List<String> 事件列表 = new List<String>();

    private Dictionary<string, UnityEvent<GameObject>> eventDictionary_G = new Dictionary<string, UnityEvent<GameObject>>();
    private Dictionary<string, UnityEvent> eventDictionary = new Dictionary<string, UnityEvent>();
    private static Event_M eventManager = new Event_M();


    private Event_M()
    {

    }

    public static Event_M I
    {
        get
        {
            return eventManager;
        }
    }
    public void Add(string eventName, UnityAction 方法)
    {
        if (!事件列表.Contains(eventName))
            事件列表.Add(eventName);

        UnityEvent thisEvent = null;
        if (eventManager.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(方法);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(方法);
            eventManager.eventDictionary.Add(eventName, thisEvent);
        }
    }
    public void Add(string eventName, UnityAction<GameObject> 方法)
    {
        if (!事件列表.Contains(eventName))
            事件列表.Add(eventName);

        UnityEvent<GameObject> thisEvent = null;
        if (eventManager.eventDictionary_G.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(方法);
        }
        else
        {
            thisEvent = new UnityEvent<GameObject>();
            thisEvent.AddListener(方法);
            eventManager.eventDictionary_G.Add(eventName, thisEvent);
        }
    }

    public void Remove(string eventName, UnityAction<GameObject> 方法)
    {
        事件列表.Remove(eventName);

        if (eventManager == null) return;
        UnityEvent<GameObject> thisEvent = null;
        if (eventManager.eventDictionary_G.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(方法);
        }
    }
    public void Remove(string eventName, UnityAction 方法)
    {
        事件列表.Remove(eventName);

        if (eventManager == null) return;
        UnityEvent thisEvent = null;
        if (eventManager.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(方法);
        }
    }
    public void Invoke(string eventName, GameObject obj)
    {
        UnityEvent<GameObject> thisEvent = null;
        if (eventManager.eventDictionary_G.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(obj);
        }
    }

    public void Invoke(string eventName)
    {
         UnityEvent thisEvent = null;
        if (eventManager.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}
//  else
//  {
//      c.m.SetColor(材质管理._SpriteColor, Color.white);

//      Initialize_Mono.I.Waite(() => {
//          c.m.SetColor(材质管理._SpriteColor, new Color(1, 1, 1, 0));
//      }, 0.1f
//);
//  }
//public static IEnumerator DOShakePosition_(this Transform t, Vector2 方向, float time, float 间隔)
//{
//    if (方向 != Vector2.zero)
//    {
//        WaitForFixedUpdate W = new WaitForFixedUpdate();
//        Vector2 Enter_way = t.localPosition;
//        float 长度 = 方向.magnitude;
//        Vector2 标准方向 = 方向.normalized;
//        int 次数 = (int)(time / 间隔);
//        float 插值 = Lerp均衡插值(0.001f, 次数);


//        float Enter_Time = 0;
//        int I = 0;
//        while (true)
//        {
//            Vector2 Targe = default;
//            长度 = Mathf.Lerp(长度, 0, 插值);
//            Targe = 长度 * 方向;
//            while (Time.unscaledTime <= Enter_Time + 间隔)
//            {



//                //Vector2 End = 长度 * 标准方向;
//                //t.localPosition = Enter_way + End;
//                yield return W;


//            }
//            Enter_Time = Time.unscaledTime;
//            t.localPosition = Enter_way + Targe;
//            I++;
//        }


//    }
//    else Debug.LogError("离谱.原地震动");
//}

//public static void 闪光(GameObject 原身j, float time)
//{
//    闪烁 闪;
//    闪 = 原身j.GetComponent <闪烁>();
//    if (闪 == null)
//    {//为空就初始化

//        //GameObject light;


//        //light = new GameObject("闪光");
//        //light.transform.SetParent(原身j.transform);

//        闪 = 原身j.AddComponent<闪烁>();

//        闪.father = 原身j;
//        闪.初始化();
//    }
//    闪.StartCoroutine(闪.开闪一下(time));
//}

//public static    void 闪光(GameObject 原身j,float time)
//{
//    闪烁 闪;
//    闪 = 原身j.GetComponentInChildren<闪烁>();
//    if (闪 == null)
//    {//为空就初始化

//        GameObject light;


//        light = new GameObject("闪光");
//        light.transform.SetParent(原身j.transform);

//        闪 = light.AddComponent<闪烁>();

//        闪.father = 原身j;
//        闪.初始化();
//    }
//    闪.StartCoroutine(闪.开闪一下(time));
//}n
