using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEditor;
using UnityEngine.Tilemaps;
using Cinemachine;
using System.Reflection;
using System.Linq;
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

 
/// <summary>
/// FPS显示
/// </summary>
 



[DefaultExecutionOrder(-100)]
public class Initialize_Mono:MonoBehaviour
{
    [SerializeField]
    Transform  焦点; 
    public void BOSS模式( GameObject boss ,bool b)
    {
        主UI.I.Boss血条_(boss,boss.name,b);
     var a=  boss.GetComponent<Boss.I_Boss>().Gs ;
        foreach (var item in a)
        {
            item.SetActive(b)  ;
        } 
    }

    public bool 显示点位置=false;

 public    LayerMask 能量子弹碰撞;
    public   float 假物理重力  = 9.8f;

    public Color 能力道具指示颜色=Color .yellow;
    public Color 搜集物品指示颜色 = Color.green;

    public Action<string> Key_Action;

    public LayerMask 戒备检测层;
    public float 压缩倍数;
    public Action<int,int> 重制触发 { get; set; } 
    public AnimationCurve defaul_Curve;

    [SerializeField]
    float f_Time_弹反销毁时间;
    [SerializeField ]
      float f_Time_踩上去自爆的时间;
    public float Speed_Max=100;
    public float Speed_Min=0.000001f;
    [DisplayOnly ]
    [SerializeField ]
    float Speed;
    public float 敌人回耐久速度; 
   public  List<消息> 消息列表 = new List<消息>();
    //public List<消息> 消息列表 { get {
    //        消息列表显示 = 消息列表1;
    //        return 消息列表1;
    //    }
    //    set {
    //        消息列表1 = value;
    //        消息列表显示 = 消息列表1;
    //    } }
    //public List<消息> 消息列表显示;

    public     bool 状态消息总闸;
    public static Initialize_Mono I { get; private set; } = new Initialize_Mono();


    [DisplayOnly]
    public List<String > 事件字典显示;

    public    List<AnimationClip>  asddd;
    public  Animator a ;
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

    public bool 能踩(Collider2D  C)
    {
        return (C.IsTouchingLayers(Initialize.L_Player) || C.CompareTag(Initialize.One_way)) &&Initialize . Layer_is(C.gameObject.layer, Player3.I.碰撞检测层);
        //return C.IsTouchingLayers(Player3.I.碰撞检测层) || C.CompareTag(Initialize.Ground);
    }
    private void Awake()
    {
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


        Save_D.存档字典_ = null;
        Save_D.Load();


        DeadPla.I.DeadList = null;
        DeadPla.I.读取();
        DeadPla.I.DE();
    } 
    public void 改变一会儿时间(float 真实时间,float 速率)
    {
        改变时间= StartCoroutine(asd( 真实时间, 速率));
    }
    IEnumerator asd(float 真实时间, float 速率)
    {
        Initialize.TimeScale = 速率;
        yield return new WaitForSecondsRealtime(真实时间);
        Initialize.TimeScale = 1;
    }


    [SerializeField]
    bool NEW消息列表=false ;  
    public   void Debug_(Type t, object Message )
    {
 
        string a = t.Name; 
        bool 有=false ;
 
        foreach (var item in I.消息列表)
        {
            if (item.Key ==a)
            {
                有 = true;
            }
        } 
        if (!有)
        {//没有
            Debug.Log ("添加了"+a);
            I.消息列表.Add(new 消息 (a,true));
        } 
        int 索引 = -1;
        for (int i = 0; i < I.消息列表.Count; i++)
        {
            if (I.消息列表[i].Key==a)
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
 
    void asd(Scene  b, Scene a)
    {
        SceneManager.activeSceneChanged -= asd;
        Initialize .    时间恢复();
    }

    public static void 闪烁(GameObject gameObject,float 时间,float 间隔)
    {
        SpriteRenderer sp =new SpriteRenderer();
        Initialize.组件(gameObject,ref sp);
       I. StartCoroutine(进入某冲刺模式(sp,间隔));
    }
   static  IEnumerator 进入某冲刺模式(SpriteRenderer sp,float 间隔)
    {
        sp.enabled = false;
        yield return new WaitForSeconds(1f);
        sp.enabled = false;
    }
    [SerializeField]
    private Sprite sp;

    //WaitForSeconds waite { get; } = new WaitForSeconds(1);
    public static void 残留( GameObject  G,  Vector2  a,Vector2 b)
    {
 

        var ga = new GameObject("残留");
        ga.transform.SetParent(G.transform);
        var SPP = ga.AddComponent<SpriteRenderer>();
        SPP.sprite = I.sp;
        SPP.color = Color.red;
        SPP.DOFade(0, 1);
        ga.transform.localPosition = a;
        ga.transform.localScale =b;
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
    IEnumerator 等待时间执行方法(Action a, float time,bool b=false)
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="a">            阿三大苏打</param>
    /// <param name="time"></param>
    /// <param name="真实时间"></param>
    public void Waite(Action a,float time=0,bool 真实时间=false)
    { 
            StartCoroutine(等待时间执行方法(a, time,真实时间)); 
    } 
    public void 等待一帧执行方法_检测原obj是否启用(GameObject  G, Action a)
    {
        Debug.LogError(G.name );
            StartCoroutine(等待激活一帧后执行方法(G,a)); 
    }

        IEnumerator 等待激活一帧后执行方法( GameObject  G, Action a)
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
   public  bool 时缓不动;
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
        if (时缓协程!=null)
        { 
            StopCoroutine(时缓协程); // 如果已经有其他协程在运行，先停止它们  
        }
        时缓协程= StartCoroutine(SetTimeCoroutine(setTime, endTime));
    }

public     Coroutine 时缓协程;
    WaitForSecondsRealtime 零点零2秒 = new WaitForSecondsRealtime (0.02f);
    [SerializeField]
    float 阀值_=5;
    public float 阀值 { get { 
            return 阀值_; 
        }
    }
    [SerializeField]
    [DisplayOnly]
    public float 负阀值_=100;
    [SerializeField]
    [DisplayOnly]
    public float 阀值2_5_;
    [SerializeField ] 
    float 阀值2_=10;
    [SerializeField] 
    float 阀值3_=50;
  
    public float 负阀值 { get =>1/ 负阀值_; }
    public float 阀值2 { get => 阀值2_; }
    public float 阀值2_5 { get =>  (阀值3+ 阀值2)/2; }
    public float 阀值3 { get => 阀值3_; }
    public float F_Time_踩上去自爆的时间 { get => f_Time_踩上去自爆的时间; set => f_Time_踩上去自爆的时间 = value; }
    public float F_Time_弹反销毁时间 { get => f_Time_弹反销毁时间; set => f_Time_弹反销毁时间 = value; }

    private void Update()
    {
        阀值2_5_ = 阀值2_5;
         阀值3_ = 阀值3;
        阀值2_ = 阀值2;
        if (Player3.I != null)
            Speed = Player3.Public_Const_Speed; 
        if (NEW消息列表 )
        {
             消息列表 = new List<消息>();
            NEW消息列表 = false;
        }
    }
}

public enum E_方向
{
    Null, 上, 下, 左, 右, 左上, 左下, 右上, 右下,
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
        if (Time .time>间隔+TTime )
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
    int C=-1;
 
    public bool Note_Re( )
    { 
        if (C  != Time.frameCount)
        {
            C  = Time.frameCount;  
            return true;
        }
        else
        { 
            return false;
        }
    } 
}

 
public static class Initialize
{ 
    public  static E_方向 Get_盒子八方向(   Bounds bounds, Vector2 point)
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
        return   E_方向.Null; // 或者抛出异常  
    }
    public static Vector2   Get_获取碰撞距离(Bounds  s,Vector2  point)
    {
        float minX =  s.min.x;
        float maxX =  s.max.x;
        float minY =  s.min.y;
        float maxY = s.max.y;
        float Hx = s.size.x / 2;
        float Hy = s.size.y / 2;
        Vector2 C = s.center;

        Vector2 TargetPO = Vector2.zero;
        switch (Get_盒子八方向(s,point))
        {
            case E_方向.Null:
                return C; 
            case E_方向.上:
            case E_方向.下:
                TargetPO = new Vector2( C.x,point .y-Hy );
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
    public static Vector2Int[] Get_v2_IntArry( this  Vector2Int  vI )
    {
        Vector2Int[]  outt= new Vector2Int[9] ;
        outt[0] = new Vector2Int(vI.x, vI.y + 1);
        outt[1] =new Vector2Int (vI.x, vI.y);
        outt[2] = new Vector2Int(vI.x, vI.y-1);
        outt[3] = new Vector2Int(vI.x + 1, vI.y + 1);
        outt[4] = new Vector2Int(vI.x + 1, vI.y);
        outt[5] = new Vector2Int(vI.x + 1, vI.y-1);
        outt[6] = new Vector2Int(vI.x-1, vI.y + 1);
        outt[7] = new Vector2Int(vI.x - 1, vI.y);
        outt[8] = new Vector2Int(vI.x - 1, vI.y - 1);
        return outt;
    }
    public static Color Set_Alpha(this  Color  C,float  a )
    { 
        return new Color (C.r ,C.g,C.b,a);
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
        var a = Get_随机种子().Next (-2147483648, 2147483647); 
        return a;
    }
public static    System.Random Get_随机种子()
    {
        var seed = Guid.NewGuid().GetHashCode();
        System.Random r = new System.Random(seed );
        return r;
    }
    public static void Copy_SpriteRenderto( this  SpriteRenderer sp,SpriteRenderer  toMe,int  前后=0,bool 替换图=false )
    {
        if (sp == null) Debug.LogError("Sp是空");
        if (toMe == null) Debug.LogError(" toMe是空");
        toMe.sortingLayerID = sp.sortingLayerID;
        toMe.sortingOrder = sp.sortingOrder+ 前后;
        if (替换图)
        {
            toMe.sprite = sp.sprite ;
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
    public static object  Copy( this   object _object)
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
        return new Vector2(1,-1);
    }
    static List<Vector2> Vector2_L { get; } = new List<Vector2>() {
        Vector2 .down,Vector2.left ,Vector2.right, Vector2.up , Vector2.one, -Vector2.one,右下角(),-右下角()};
    public static List<Vector2> 边上三点(this Bounds B,E_方向 E)
    {// v2 里有0    除0外相同  数字
        //v2 没0    xy各为0
        List<Vector2 > List=new List<Vector2> ();
   var a=     E.方向To_v2();
        var X = a.x;
        var Y = a.y;
        if (a==Vector2 .zero)
        {
            Debug.LogError("不接受  v2 变量");
        }
        else
        { 
            if (X*Y==0)
            { 
                // v2 里有0    除0外相同  数字
                if (X!=0)
                {
                    Vector2_L.ForEach((Vector2 v) => {
                        if (v.x==X)
                        { 
                            List.Add(v);
                        }
                    });
                }
                else if (Y!=0)
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
                List.Add(new Vector2 (0,Y));
                List.Add(new Vector2(X, 0));
            }
        }

        for (int i = 0; i < List.Count; i++)
        {
            List[i] = B.九个点(List[i].v2_To方向());
        } 
        return List;
    }
    public static void  集体开关( this   List<Component> 组件列表,bool 开关)
    {
        if (组件列表==null&&组件列表 .Count<1)
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
    public static IEnumerator Q弹(this Transform t, float forc, float time,  bool 改变位置 = true, bool 真实时间 = false,AnimationCurve a = null,bool 还原= true)
    {
        if (a == null) a = Initialize_Mono.I.defaul_Curve;
        float 一 = 0;
        var 原始尺寸 = new Vector2(t.localScale.x.Sign() * 1, 1);
        var 原始P = t.localPosition;
        while (一 < 1)
        {
            if (真实时间) 一 += Time.fixedUnscaledTime  * 1 / time;
            else 一 += Time.fixedDeltaTime * 1 / Player3.Public_Const_Speed / time;
             
            var f = (a.Evaluate(一) - 0.5f) * forc;
            //Debug.LogError(f + "                              " + 一);
            Initialize.等体积压缩(t, f, 改变位置);
            yield return new WaitForFixedUpdate();
        }
        if (还原)  t.localPosition = 原始P; 
        t.localScale = 原始尺寸;
    }
    public static 相机框 Get_摄像框(this Transform t)
    {
        var TTT = Physics2D.CircleCastAll(t.position, 1f, Vector2.zero, 0)?.Get_碰撞组<相机框>();
        return TTT;
    }

    public static     int Get_摄像框编号(this Transform t)
    {
        var TTT = Physics2D.CircleCastAll(t.position, 1f, Vector2.zero, 0)?.Get_碰撞组<相机框>();
        if (TTT == null)
        { 
            ((Vector2)t.position).DraClirl(10);
            return -999;
        }
        else
        {
            if (TTT.编号==-9999)
            {
                Debug.LogError(TTT + "没初始化好");
            }
          
            return TTT.编号;
        }
    }
    public static   IEnumerator Time_State(float time, Action End = null, Action Fixupdate = null, Action Start = null)
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
    public static   RaycastHit2D [] 碰撞列表(this Bounds s,int layer,float 加成=1)
    {
        var a = Physics2D.BoxCastAll(s.center, s.size* 加成, 0, Vector2.zero, 0, layer);
 
        return a;
    }
    public static Color Lerp( this  Color color, Color colorNext, float speed)
    {
        color = new Color(
            Mathf.Lerp(color.r, colorNext.r, speed),
            Mathf.Lerp(color.g, colorNext.g, speed),
            Mathf.Lerp(color.b, colorNext.b, speed)
                          );
        return color;
    }
    public static bool  _is(this  float a,float b,float 精度=0.0001f)
    {  
        return Mathf.Abs(a) - Mathf.Abs(b) < Mathf.Abs(精度);
    }
    public static void Dra(this Bounds B, Color C = default)
    {
        if (C == Color.white * 0f) C = Color.red;
        Debug.DrawLine(B.min,B.九个点(E_方向 .左上));
        Debug.DrawLine(  B.九个点(E_方向.左上), B.max);
        Debug.DrawLine(B.max, B.九个点(E_方向.右下));
        Debug.DrawLine(B.九个点(E_方向.右下), B.min);
    }
    public static void DraClirl(this    Vector2 o, float 距离=0.1f,Color C=default,float time=0.1f )
    {
        if (C==Color .white *0f) C = Color.red;
    if(Initialize_Mono .I.显示点位置)    Debug.Log(o+" 一下");
        var  二一_ = new Vector2(2,1);
        var  二一 = new Vector2(-2,1);

        var 一二_ = new Vector2(-1, 2);
        var 一二 = new Vector2(1,2);

        var ZZ = new Vector2(1, -1);
        Debug.DrawRay(o, 一二_.normalized * 距离, C,time );
        Debug.DrawRay(o, -一二_.normalized * 距离, C, time);
        Debug.DrawRay(o, 一二.normalized * 距离, C, time);
        Debug.DrawRay(o, -一二.normalized * 距离, C, time);

        Debug.DrawRay(o,二一_.normalized * 距离, C, time);
        Debug.DrawRay(o, -二一_.normalized * 距离, C, time);
        Debug.DrawRay(o, 二一.normalized * 距离, C, time);
        Debug.DrawRay(o, -二一.normalized * 距离, C, time);

        Debug.DrawRay(o, Vector2.up.normalized * 距离, C, time);
        Debug.DrawRay(o, Vector2.down.normalized * 距离, C, time);
        Debug.DrawRay(o, Vector2.left.normalized * 距离, C, time);
        Debug.DrawRay(o, Vector2.right.normalized * 距离, C, time);
        Debug.DrawRay(o, Vector2.one.normalized * 距离, C, time);
        Debug.DrawRay(o, Vector2.one.normalized * -1 * 距离, C, time);
        Debug.DrawRay(o,ZZ.normalized * 距离, C, time);
        Debug.DrawRay(o, ZZ.normalized * - 距离, C, time);
    }
     public static int   Sign( this  float  a )
    { 
        if (a > 0) return 1;
        else if (a < 0) return -1;
        else return 0; 
    }
    public static T Get_碰撞组<T>(this  RaycastHit2D[] Hit) where T : Component
    {
        Component Out=null;
        for (int i = 0; i < Hit.Length ; i++)
        {
            var c = Hit[i].collider;
            if (c != null)
            {
         
                Out=c.GetComponent<T>();
                if (Out!=null) return (T)Out; 
                  
            }
        }
        return   (T)Out;
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
        if (v.x == 0 || v.y == 0 || target .x == 0 || target.y==0)
        {
            Debug.Log ("返回0了这不科学"+ v+"          "+target  );
        }
       bool  Out=   v.x  ==  target.x ||  v.y  ==  target.y ;
   
        return Out;
    }
    /// <summary>
    /// 返回9个方向
    /// </summary>
    /// <param name="我"></param>
    /// <param name="你"></param>
    /// <returns></returns>
  public static Vector2Int 你在我的哪里(this Transform  我,Transform  你)
    {
        Vector2 cha = 你.position - 我.position;
        var x = cha.x > 0 ? 1 : -1;
        var y = cha.y > 0 ? 1 : -1;
        return new Vector2Int(x,y);
    }
   static  int ASD( float t)
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
    public static  List<Vector2> 阵列盒子(this  Bounds 销毁盒子)
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
        return new Vector2( v.x ,y);
    }
    public static Vector2 Set_X(  this Vector2 v,float x)
    {
        return new Vector2(x,v.y);
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
            Debug.LogError("F小于0        值为      "+F);
        }
        if (value==0)
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
            return  ((value - (1 )) / (1 - (1 / F))) ;
        }

        // 如果value不在上述范围内，则理论上不会执行到这里（除非F非常接近0或1）  
        // 但为了完整性，可以抛出一个异常或返回一个错误值  
        throw new ArgumentException("离谱");
    }

    public static E_方向 v2_To方向( this  Vector2  v2)
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
                return new Vector2Int(-1,1);
            case E_方向.左下:
                return Vector2Int.one*-1;
            case E_方向.右上:
                return Vector2Int.one;
            case E_方向.右下:
                return new Vector2Int( 1, -1);
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
    public static  bool Is_方向(Vector2  v,E_方向  方向)
    {
        switch (方向)
        { 
            case E_方向.上:
                if (v.y>0)       return true; 
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
    public static string  Obj_E { get; } = "Obj_E";
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
    public static string MovePlatform { get; } = "MovePlatform";
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
    private static   string MateriaPath { get; } = "Material";
    static System.Random  random = new System.Random();
    // 生成一个随机整数，范围在[min, max)  
    public   static  Vector2 MoveToPosition(Vector2 My, Vector2 targetPosition, float distance)
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
    public static float  等体积压缩(Transform t, float v, bool 改变位置 = true)
    {

        var y =1+v;
        var x =1-v;

        if (Mathf.Abs(t.localScale.y) == y) return   0;

        var cha = t.localScale.y - y;
        var py = t.localPosition.y- cha*2.5f ;

        if (改变位置)    t.localPosition = new Vector2(t.localPosition.x, py);
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
        if (value >= 0) return  1;
        else if (value < 0) return -1;
        else     return 0; 
    }
    public static  bool  Layer_is(int layer,LayerMask lm)
    {
        ///如果一个层 obj.layer 是 4
//        layermask 是 0001 0010，也就是 18
//（1 << 4） &18 就会是 0001 0000
        bool B = ((1 <<  layer) & lm.value) > 0;
        return B;
    }
    /// <summary>
    /// 每次使用都会会获取一次
    /// </summary>

    public static float Lerp均衡插值(  float b误差值, int n步数)
    {
 
        var b = 1 - Mathf.Pow(b误差值, 1 / n步数);
        int 次数 = 1;
        while (b==1)
        {
 
            b = 1 - Mathf.Pow(b误差值, 1 / (float)n步数);
            n步数++;
            次数++;
            Debug.LogError(1 / (float)n步数);
            if (次数>10)
            {
                break;
            }
        }
        if (b==1)
        {
            Debug.LogError("卧槽"+ b误差值 + "" + 1 / (float)n步数);
        }
        return 1- Mathf.Pow(b误差值, 1 / (float)n步数);
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
    public static bool  Vector2Int比较(Vector2 a,Vector2 b,bool bug=false )
    {
        if (bug)
        {
            Debug.LogError(a.x+"_"+a.y+"\n          " +(int)a.x + "_" + (int)b.x + "\n         " + (int)a.y + "_" + (int)b.y);
        }

        return (int)a.x == (int)b.x && (int)a.y == (int)b.y;
    }
    public static string  Get_CutternAnimName(Animator a)
    {
            return a.GetCurrentAnimatorClipInfo(0)[0].clip.name; ;
    }
       public static List<GameObject> 扇形检测(Vector2 原点, float 距离, float 角度, float 扇形角度, int 精度, LayerMask layerMask,bool 穿透=false ,bool Deb =false )
    {
        List<GameObject> detectedObjects = new List<GameObject>();
        float angleStep = 扇形角度 / (精度 - 1);

        for (int i = 0; i < 精度; i++)
        {
            float currentAngle = 角度 - 扇形角度 / 2f + angleStep * i;
            Vector2 rayDirection = new Vector2(Mathf.Sin(currentAngle * Mathf.Deg2Rad), Mathf.Cos(currentAngle * Mathf.Deg2Rad));

#if UNITY_EDITOR
            if (Deb )      Debug.DrawRay(原点, rayDirection * 距离, Color.green);
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
                if (!穿透)break ;
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
     return   Physics2D.GetIgnoreLayerCollision(a, b);
    }
    public static int GetNumOfString(string str)
    {
        int num;
        string strNum = System.Text.RegularExpressions.Regex.Replace(str, @"[^0-9]+", "");
        if (strNum == "") return 0;
        num = int.Parse(strNum);
        return num;
    }
    public static IEnumerator Waite(Action a )
    { 
        yield return null; 
        a.Invoke();
    }
    public static string _Color(this object o, Color c)
    { 
        var a = ColorUtility.ToHtmlStringRGBA(c);  
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

    public static  float   TimeScale{get => Time.timeScale; set
        {         Time.timeScale = value;     }
 }
    public static void 时间暂停()
    {
        TimeScale = 0; 
    }


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void 时间恢复()
    {
        if (Initialize_Mono.I==null||Initialize_Mono.I.gameObject ==null )
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
    public static float  屏幕横纵比
    {
        get
        {
            return (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        }
    }
    public static  bool V2比较_A大于B(Vector2 A,Vector2 B)
    { 
            return ! (B.x > A.x || B.y > A.y);  
    }
 
    /// <summary>
    /// /返回为零说明现有的更大
    /// </summary>
    /// <param name="现有的碰撞框Size"></param>
    /// <param name="摄像机目标Size"></param>
    /// <returns></returns>
    public static float 返回兼容相机碰撞框的摄像机尺寸(Vector2 现有的碰撞框Size, float 摄像机目标Size)
    {  
        Vector2 摄像机目标尺寸;  
        ///68  34
        //得出实际摄像机尺寸
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
            Debug.LogError("出");
            return Mathf.Min(X, Y) - 0.0001f;
        }
        else if (X == Y && X == 0)
        {
            Debug.LogError("出");
            //Debug.LogError("为甚么呀" + 摄像机目标尺寸+"       "+ Target_OrthographicSize+ "     碰撞框  " + 现有的碰撞框Size);
         ///目标尺寸小于目标尺寸
            return 0;
        }
        ///XY 一个有
        else
        {
            Debug.LogError("出");
            return Mathf.Max(X, Y)-0.0001f;
        }
    }
    public static GameObject 获取已加载场景根节点的TAG是的(string  tag)
    {
 
           var a = 获取已加载场景中所有根节点的obj();
        foreach (var item in a)
        {
            if (item.tag==tag)
            {
                return item;
            } 
        } 
        return null;

    }
    public   static List<GameObject> 获取已加载场景中所有根节点的obj()
    {//该方法在场景未加载完时，未加载的场景返回的OBJ列表为空,因此要放在UPdate里
        List<GameObject> G = new List<GameObject>(); 
        for (int i = 0; i < SceneManager .sceneCount ; i++)
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
    public static bool  所有的场景都加载完了嘛()
    {
 
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene a = SceneManager.GetSceneAt(i);
            if (a != null)
            { 
                if (a.GetRootGameObjects ().Length==0)
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
    public static Vector2  朝向对象(GameObject my,GameObject  target)
    {
        var a = my.transform.position.x;
        var b = target.transform.position.x;
        int I = a -b>0 ? -1 : 1;
        return new Vector2(I,1);
    }
    public static void 闪光(this  SpriteRenderer sp, float time,bool b=true)
    {

        sp.material.SetColor(材质管理._SpriteColor, Color.white);
            Initialize_Mono.I.Waite(()=> {
                if (sp!=null)
 
                sp.material.SetColor(材质管理._SpriteColor, new Color (1,1,1,0));
            }
            , time, b
            );
       
        }

    public static Vector2 返回和对方相反方向的标准力(Vector2  m, Vector2  y)
    { 
        float o = (m - y).x;
        float p = (m - y).y;
        o = o > 0 ? 1 : -1;
        p = p > 0 ? 1 : -1;
        //Debug.LogError(你+"          "+我+o+"         "+p);
        return new Vector2(o, p);
    }
    public   static  Vector2  返回和对方相反方向的标准力(GameObject  my,GameObject  Target)
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
        Vector2 P = B ;

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
            return O.y - P.y< 0;
        }
        else
        {
            return O.x - P.x< 0;
        }
    }

    /// <summary>
    /// A是不是在B的旁边
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <param name="true就是算Y_flase就是X"></param>
    /// <returns></returns>
    public static bool 接近_判断A减B的绝对距离是否小于等于_范围(GameObject A, GameObject B, float 范围 ,bool true就是算Y_flase就是X)
    {
        Vector2 O = A.transform.position;
        Vector2 P = B.transform.position;
        if (true就是算Y_flase就是X)
        {
            return MathF.Abs(  O.y - P.y) <= 范围;
        }
        else
        {
            return MathF.Abs(O.x - P.x) <= 范围;
        }
    }
    public static void 检测移动平台下落( BiologyBase B)
    { 
      var  po = B.co;
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
    public static void 组件<T>(this  GameObject gb, ref T co) where T : Component
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
    public static string 扫把打到了 { get; } = "扫把打到了";
    public static string 场景保存触发 { get; } = "场景保存触发";
    public static string 剧情触发 { get; } = "剧情触发";
    public static string 对话退出 { get; } = "对话退出";
    public static string 切换场景触发_obj { get; } = "切换场景触发";
    internal static string 对话触发_OBJ { get; } = "对话";
    internal static  string UI回到战斗 { get; } = "退回";

    public List<String> 事件列表  = new List<String>();

    private Dictionary<string, UnityEvent<GameObject>> eventDictionary_G = new Dictionary<string, UnityEvent<GameObject>>();
    private Dictionary<string, UnityEvent > eventDictionary = new Dictionary<string, UnityEvent >();
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

        UnityEvent  thisEvent = null;
        if (eventManager.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(方法);
        }
        else
        {
            thisEvent = new UnityEvent ();
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
    public void Remove(string eventName, UnityAction  方法)
    {
        事件列表.Remove(eventName);

        if (eventManager == null) return;
        UnityEvent  thisEvent = null;
        if (eventManager.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(方法);
        }
    }
    public void Invoke(string eventName,GameObject  obj)
    { 
        UnityEvent<GameObject> thisEvent = null;
        if (eventManager.eventDictionary_G.TryGetValue(eventName, out thisEvent))
        { 
            thisEvent.Invoke(obj);
        }
    }

    public void Invoke(string eventName )
    {
        UnityEvent  thisEvent = null;
        if (eventManager.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke( );
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
