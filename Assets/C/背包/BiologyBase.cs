using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
[DefaultExecutionOrder(-10)]
public abstract class BiologyBase : MonoBehaviour, set_get, 操控, I_攻击, I_生命
{
    public bool transformDeb;
    protected virtual void Update()
    {
        transform.transformDeb = transformDeb;
    }
    public class m_transform
    {
        public bool transformDeb;
        Transform transform_真实;
        public Transform transform
        {
            get
            {
                if (transformDeb) Debug.Log("有东西试图访问" + transform_真实.gameObject.name + transform_真实.position);
                return transform_真实;
            }
        }
        public m_transform(Transform s)
        {
            transform_真实 = s;
        }
        public void SetParent(Transform s)
        {
            if (transformDeb)
            {
                if (transform_真实.parent!=s)
                {
                    Debug.Log(Time.frameCount + "修改父对象从" + s + "修改为" + s);
                }
            }
            transform_真实.SetParent(s);
        }
        public Transform parent => transform_真实.parent;
        public Vector3 localPosition
        {
            get { return transform_真实.localPosition; }
            set
            {
                if (transformDeb)
                {
                    if (transform_真实.localPosition != value)
                    {

                    Debug.Log(Time.frameCount + "修改局部坐标：" + transform_真实.localPosition + "修改后：" + value);
                    }
                }
                transform_真实.localPosition = value;
            }
        }
        public Vector3 lossyScale => transform_真实.lossyScale;
        public Vector3 position
        {
            get { return transform_真实.position; }
            set
            {
                if (transformDeb)
                {
                    if (transform_真实.position != value)
                    {
                        Debug.Log(Time.frameCount + "修改坐标：" + transform_真实.position + "修改后：" + value+"差值："+(value - transform_真实.position));
                    }
                }
                transform_真实.position = value;
            }
        }
        public Vector3 localScale
        {
            get { return transform_真实.localScale; }
            set
            {
                if (transformDeb)
                {
                    if (transform_真实.localScale!=value)
                    {
                    Debug.Log(Time.frameCount + "修改尺寸：" + transform_真实.localScale + "修改后：" + value+transform.gameObject.name); 
                    }
                }

                transform_真实.localScale = value;
            }
        }
        public Quaternion rotation
        {
            get { return transform_真实.rotation; }
            set
            {
                if (transformDeb) Debug.Log(Time.frameCount + "修改旋转：" + transform_真实.rotation + "修改后：" + value);
                transform_真实.rotation = value;
            }
        }
    }
    new public m_transform transform { get; set; }
    public Vector2 扣血外部力 { get; set; }
    public bool 真实动画
    {
        get
        {

            if (an.updateMode == AnimatorUpdateMode.UnscaledTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        set
        {
            if (value)
            {
                an.updateMode = AnimatorUpdateMode.UnscaledTime;
            }
            else
            {
                an.updateMode = AnimatorUpdateMode.Normal;
            }
        }
    }

    public 移动方式 当前移动方式;



    //new public Transform transform { get { return base.transform; } }
    /// <summary>
    /// 翻转
    /// </summary>
    public virtual void Flip()
    {

        LocalScaleX_Set = -LocalScaleX_Set;
    }
    /// <summary>
    ///   返回正负1
    ///   设置必须是正数或者负数
    /// </summary>
    public float LocalScaleX_Set
    {
        get { return Mathf.Sign(transform.lossyScale.x); }
        set {
            if ((int)Mathf.Sign(value) != (int)Mathf.Sign(LocalScaleX_Int))
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
        }
    }
    /// <summary>
    ///   只能是尺寸是1单位
    /// </summary>
    public int LocalScaleX_Int
    {
        get
        {
            return (int)transform.lossyScale.x;
        }
        set
        {
            if (value != 1 || value != -1)
            {
                Debug.LogError("我勒个去与");
            }
            transform.localScale = new Vector3((int)value, transform.localScale.y, 1);
        }
    }

    public bool Trigger
    {
        get
        {


            return co.isTrigger;
        }
        set { co.isTrigger = value; }
    }
    public Vector2 反向 { get { return new Vector2(-LocalScaleX_Set, 0); } }
    public Vector2 正向 { get { return new Vector2(LocalScaleX_Set, 0); } }
    public Vector2 反面脚底 { get { return new Vector2(LocalScaleX_Set == 1 ? Bounds.min.x : Bounds.max.x, Bounds.min.y); } }
    public Vector2 正面头顶 { get { return new Vector2(LocalScaleX_Set == 1 ? Bounds.max.x : Bounds.min.x, Bounds.max.y); } }
    public Vector2 反面中间 { get { return new Vector2(反面脚底.x, Bounds.center.y); } }
    public Vector2 正面中间 { get { return new Vector2(正面头顶.x, Bounds.center.y); } }
    public Vector2 脚底中间 { get { return new Vector2(Bounds.center.x, Bounds.min.y); } }
    public Bounds Bounds
    {
        get
        {
            if (co == null)
            {
                return new Bounds();
            }
            return co.bounds;
        }
    }
    [SerializeField]
    protected bool 灵魂1;
    protected virtual bool 灵魂
    {

        get => 灵魂1;
        set
        {
            灵魂1 = value;
        }
    }

    [SerializeField]
    bool Ground_;
    public Action 接触地面事件 { get; set; }
    public Action 离开地面事件 { get; set; }

    public enum 地面情况
    {
        跨不过去,
        平地,
        有坑,
        UnKnow,
    }


    protected virtual void 接触地面_()
    {

        接触地面事件?.Invoke();
    }
    protected virtual void 离开地面_()
    {
        离开地面事件?.Invoke();
    }

    public bool Ground
    {

        get
        {
            return Ground_;
        }
        set
        {

            if (Ground != value)
            {
                if (速度调试)
                {
                    Debug.LogError(value._Color(Color.green) + Time.frameCount);
                }
                //bool LastGround_ = Ground_;

                if (!Ground_ && value)
                {
                    接触地面_();
                }
                if (Ground_ && !value)
                {
                    //if (gameObject==Player3.I.gameObject)
                    //    if (Player3.I.脚下==null)
                    离开地面_();
                }
                Ground_ = value;
            }


        }
    }
    public Animator an;

    public  SpriteRenderer sp;
    //[HideInInspector] 
    [DisplayOnly]
    public Collider2D co;
    [HideInInspector] Rigidbody2D rb;
    public bool Staticrb { get => rb.bodyType == RigidbodyType2D.Static; }
    public Rigidbody2D Get_rb()
    {
        return rb;
    }
    [DisplayOnly]
    [SerializeField]
    bool 前;
    public virtual bool 前空_
    {
        get { return 前; }
        set { 前 = value; }
    }
    [DisplayOnly]
    public bool 后;
    public virtual bool 后空_
    {
        get { return 后; }
        set { 后 = value; }
    }
    [DisplayOnly]
    [SerializeField]
    bool 头 = true;

    /// <summary>
    /// true为空
    /// </summary>
    public virtual bool 头空_
    {

        get { return 头; }
        set
        {
            if (头 != value)
            {
                if (头 == false && value == true)
                {
                    出了洞嘛();

                }
            }
            头 = value;
        }
    }
    public virtual void 开启灵魂()
    {

        if (!灵魂)
        {
            灵魂 = true;
        }

    }
    public virtual void 关闭灵魂()
    {
        if (灵魂)
        {
            灵魂 = false;
        }


    }
    public virtual void 右移()
    {

    }
    public virtual void 左移()
    {

    }
    public virtual void 向目标水平移动(GameObject obj)
    {
    }
    protected virtual void 出了洞嘛()
    {

    }

    public void AddForce(Vector2 vector2, ForceMode2D mode2D)
    {
        rb.AddForce(vector2, mode2D);
    }
    public void AddForce(Vector2 vector2)
    {
        //if (gameObject.CompareTag(Initialize.Player)&& Initialize_Mono.I.MoveP_优化 && Player3.I.脚下 != null)
        //{
        //    Player3.I.transform.localPosition += new Vector3(Player_input.I.方向正零负 * Player3.I.玩家数值.常态速度, 0, 0) * Time.fixedDeltaTime;
        //    return;
        //}
        //if (vector2 !=Vector2 .zero )
        //{
        //    Debug.LogError("加力"+ vector2);
        //}
        rb.AddForce(vector2);
    }
    /// <summary>
    /// 重力
    /// </summary>
    public float GravityScale
    {
        get { return rb.gravityScale; }
        set { rb.gravityScale = value; }
    }
    /// <summary>
    /// 摩擦力
    /// </summary>
    public float Linear
    {
        get { return rb.drag; }
        set { rb.drag = value; }
    }
    [SerializeField]
    protected bool Velocity调试;
    [SerializeField]
    protected bool 速度调试;
    public virtual Vector2 Velocity
    {
        get {
            if (Velocity调试) Debug.LogWarning("Get" + rb.velocity + "obj   name:" + gameObject);
            return rb.velocity;

        }
        set {
            if (Velocity调试) Debug.Log(rb.velocity + "改后" + value + "obj   name:" + gameObject);
            rb.velocity = value;

        }
    }

    public virtual float atkvalue { get; set; }
    public abstract Action 生命归零 { get; set; }

    public abstract Action 被打 { get; set; }
    public abstract float 当前hp { get; set; }
    public abstract float hpMax { get; set; }
    public virtual bool HPROCK { get; set; }

    public class M_tsvalueList
    {
        [SerializeField]
        public List<M_tsvalue> TsL;

        //[SerializeField]
        //public List<Vector3> qwe;
        //[SerializeField]
        //public List<bool> aaa;
        public void Add(Vector3 po, bool B)
        {
            TsL.Add(new M_tsvalue(po, B));

        }
        public M_tsvalueList(List<M_tsvalue> l)
        {

            TsL = l;
            Debug.LogError("NEWNEWNEWNEW");
        }
        public M_tsvalue Have(Vector3 pos)
        {
            if (TsL == null)
            {
                return M_tsvalue.Null;
            }
            for (int i = 0; i < TsL.Count; i++)
            {
                if (TsL[i].pos == pos)
                {
                    return TsL[i];
                }
            }
            return M_tsvalue.Null;
        }
    }
    [Serializable]
    public class M_tsvalue
    {
        public static M_tsvalue Null;
        [SerializeField]
        public Vector3 pos;
        [SerializeField]
        public bool Deb;

        public M_tsvalue(Vector3 pos, bool deb)
        {
            this.pos = pos;
            Deb = deb;
        }
    }
    private void OnDestroy()
    {
        TsL_I = null;
    }
    public static void Save假tr实例(M_tsvalueList s)
    {
        //var a =new   M_tsvalueList(new List<M_tsvalue>());
        //var a =new M_tsvalue(Vector2.zero,true);
        //var a =new List<M_tsvalue>();
        //a.Add(new M_tsvalue(Vector2.zero, true));

        //var j = JsonUtility.ToJson(a, true);
        //Debug.LogError(j+"aaaaaa");

        Save_static.SaveinText(s, Save_static.假tr实例, true);
    }
    public static M_tsvalueList TsL_I;
    protected virtual void Awake()
    {
        transform = new m_transform(gameObject.transform);

        if (TsL_I == null || TsL_I.TsL== null)
        {
            ///第一次运行
            ///没有实例  
            var a = Save_static.LoadinText<M_tsvalueList>(Save_static.假tr实例);
        if (a == null|| a.TsL==null)
        {  
                ///没有存档
            TsL_I = new M_tsvalueList(new List<M_tsvalue>());
            Debug.LogError(TsL_I);
                ///来个新的
            Save假tr实例(TsL_I);
            //Debug.LogError("EEEEEEEEEEEEEEEEE");
        }
        else
        {
                ///有存档
                ///读取记录
                //Debug.LogError("WQEQWEQWEQWEQ");
            TsL_I = a; 
        }
        }

        if (TsL_I!=null&&TsL_I.TsL != null)
        {
            ////到这边保证大的不为空
            var aa = TsL_I.Have(transform.position);
            if (aa  != null)
            {
                //Debug.LogError("WWWWWWWWWWWWW ");
                ///有我了
                transformDeb = aa.Deb;
            }
            else
            {
      
                ///没有我
                TsL_I.Add(transform.position, transformDeb);
                //Debug.LogError(transform.position+"QQQQQQQQQQQQQQQQQQQ " + TsL_I.TsL.Count);
            }
        }
        else
        {
            //Debug.LogError("AAAAAAAAAAAAAAAAAAAAAAA迷惑");
        }



            Initialize.组件(gameObject, ref  an);
        Initialize.组件(gameObject, ref co);
        Initialize.组件(gameObject, ref sp);
        Initialize.组件(gameObject, ref rb);

        //rb = this.gameObject.GetComponent<Rigidbody2D>();
        
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

    }
public virtual LayerMask  碰撞检测层
    {
        get
        {
            return 1 << Initialize.L_Ground;
        }
    }
protected virtual void 前后和头(float 距离,float DI横)
    {

        //float DD = Ground ? 0 : 0.3f;
        var DI =
                         Physics2D.BoxCast(
              new Vector2(co.bounds.center.x, co.bounds.min.y),
               new Vector2(co.bounds.size.x - DI横, 0.01f),
               0f,
               Vector2.down,
                0.01f,
         碰撞检测层
               )
               .collider;
        if (DI != null)
        {
            Ground = true;
        }
        else
        {
 
                Ground = false;
        }


        头空_ =
        Physics2D.BoxCast(
       new Vector2(co.bounds.center.x, co.bounds.max.y),
        new Vector2(co.bounds.size.x - 0.5f, 1),
        0f,
        Vector2.up,
         距离,
碰撞检测层
        )
        .collider == null;

  前空_ =
      Physics2D.BoxCast(
      new Vector2(正面头顶 .x, co.bounds.center.y),
      new Vector2(0.01f, co.bounds.size.y - 0.4f),
      0f,
    正向,
 0.05f,
碰撞检测层
      )
      .collider == null;

        后空_ =
 Physics2D.BoxCast(
 new Vector2(反面脚底.x, co.bounds.center.y),
 new Vector2(0.01f, co.bounds.size.y - 0.4f),
 0f,
    反向,
 0.05f,
碰撞检测层
 )
 .collider == null;

 
    }

    public bool 左空 { 
        get {
            if (LocalScaleX_Set ==1)
            {
                return 后空_;
            }
            else
            {
                return 前空_;
            } 
        } 
    }
    public bool 右空 {

        get
        {
            if (LocalScaleX_Set != 1)
            {
                return 后空_;
            }
            else
            {
                return 前空_;
            }
        }
    }
    /// <summary>
    /// 空为真     有实体为假
    /// </summary>
    public virtual  bool  悬空检测()
    { 
        var a = new Vector2(co.bounds.min.x, co.bounds.min. y+0.1f); 
        var b = new Vector2(co.bounds.max.x, co.bounds.min.y + 0.1f);
     var ba=   Physics2D.Raycast(a,Vector2.down ,0.2f,碰撞检测层).collider==null;
        var bb = Physics2D.Raycast(b, Vector2.down, 0.2f, 碰撞检测层).collider == null;
        return bb || ba;
    }
 
    public virtual void 交互()
    {
    }


    public virtual void 跳跃()
    {

    }

    public virtual void 扣攻击(float i)
    {
    }

    public virtual void 被扣血(float i, GameObject obj,int Key)
    {
    }

    public virtual void 扣最大上限(float i)
    {
    }
}
