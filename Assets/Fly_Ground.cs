using Schema.Builtin.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Fly_Ground : MonoBehaviour, I_Speed_Change, I_攻击, I_ReturnPool, I_Dead
{

    public bool 不下落 = false;
    public GameObject 对象 { get => gameObject; }
    public string Pool_Key_name { get; set; }
    public System.Action 变速触发 { get; set; }
    public I_Speed_Change I_S { get => (I_Speed_Change)this; }
    public float Current_Speed_LV { get => Speed_Lv_; }
    public BoxCollider2D get_bc()
    {
        return bc;
    }
    BoxCollider2D bc;
    Animator an;
    public SpriteRenderer sp;
    //碰撞伤害 p;
    [SerializeField]
    float Speed_Lv_ = 1;
    public float Speed_Lv { get => Speed_Lv_; set => Speed_Lv_ = value; }


    public float self_speed { get => self_speed1; set => self_speed1 = value; }
    [SerializeField]
    Vector2 方向_;

    float tim { get; set; }


    bool 是我;

    private void OnTriggerStay2D(Collider2D collision)
    {


        return;
        if (collision.CompareTag(Initialize.Player)) return;
        if (Initialize.Layer_is(collision.gameObject.layer, Currrtten))
        {
            Debug.LogError(LayerMask.LayerToName(collision.gameObject.layer) + "      AAAAAAA ");

            引线爆炸(collision);
        }

    }
    private void OnCollisionEnter2D(Collision2D co)
    {
        //开毁();
        //return;
        ///超速就让玩家死 
        ///正常就正常弄出可以爬上去的路（不可以单人路
        //if (co.gameObject.layer.)
        //{

        //}
        //Initialize.Layer_is(co.gameObject.layer,Currrtten);
        //bool B = ((1 << co.gameObject.layer) & Currrtten.value) > 0;// 何意喂
        //if (Time.realtimeSinceStartup - tim < 0.5f) return; // 何意喂刚初始化就发生碰撞?

        if (无视) if (无视盒子.Contains(transform.position)) return;


        if (co.collider.CompareTag(Initialize.Player))
        {
            bool 碰到的是上面 = Initialize.Vector2Int比较(co.contacts[0].normal, Vector2.down);

            if (!旋转1)
            {
                是我 = true;
                Player3.I.ChangeFather(transform);
            }

            if (不下落) return;
            if (碰到的是上面)
            {
                TTime1 = Initialize_Mono.I.F_Time_踩上去自爆的时间;
                方向 = new Vector2(方向.x, -1);
                是玩家噶的 = true;

                开毁();

            }


        }
        else
        {
            Debug.LogError("        OnCollisionEnter2D(Collision2D co)OnCollisionEnter2D(Collision2D co)           ");

            开毁();
        }
    }
    public bool 是玩家噶的;
    private void OnDisable()
    {
        if (是我)
        {
            if (Player3.I != null)
            {
                Player3.I.ChangeFather();
            }

        }
    }
    private void OnCollisionExit2D(Collision2D co)
    {
        if (co.gameObject.layer == Initialize.L_Player)
        {
            是我 = false;
            Player3.I.ChangeFather();
        }
    }

    public Vector2 方向
    {
        get
        {
            return 方向_;
        }
        set
        {

            if (方向_ != value)
            {
                方向_ = value;
            }
        }
    }
    [SerializeField]
    float atkvalue_;
    public float atkvalue { get => atkvalue_; set => atkvalue_ = value; }
    public Action 销毁触发 { get; set; }

    public Bounds 盒子
    {
        get
        {
            if (是SP而不是BC)
            {
                return sp.bounds;
            }
            return bc.bounds;

        }
    }
    public bool 是SP而不是BC = true;
    public bool 可以旋转;
    public bool 爆炸送走z = false;
    public bool 爆炸伤害 = true;
    public bool 触发冰块 = true;
    public bool 箭头伤害 = true;
    private bool 原批触发 = false;

    public bool 无视 = false;
    public Bounds 无视盒子 = default;
    private void Awake()
    {
        if (sp == null) Initialize.组件(gameObject, ref sp);

        Initialize.组件(gameObject, ref an);
        Initialize.组件(gameObject, ref bc);
        gameObject.layer = Initialize.L_M_Ground;

        //变速触发 += () => { sp.闪光(0.02f); }; 

        if (原批触发) Player3.I.圆斩对象 += asd;

        Start();
    }
    void asd(int i)
    {
        if (i == gameObject.GetInstanceID()) 开毁();
    }
    //float Selllf_Speed = 2;
    public void 初始化(Vector2 方向, Vector2 位置, float SpeedLv = 1, float atkv = 1)
    {

        tim = Time.realtimeSinceStartup;
        //atkvalue = atkv;
        transform.position = 位置;
        //self_speed = Selllf_Speed;
        this.方向 = 方向;
        Speed_Lv = SpeedLv;
    }
    /// <summary>
    ///   默认全局坐标fase
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="坐标"></param>
    /// <returns></returns>
    public Vector2 GetEdgeCenter(Vector2 direction, bool 坐标 = false)
    {
        Vector2 center = Vector2.zero;// Bounds的中心点  
        if (!坐标)
        {
            center = bc.bounds.center;
        }
        else
        {
            center = Vector2.zero;
        }

        Vector2 extents = bc.bounds.extents; // Bounds的大小的一半   
        if (direction == Vector2.right)
        {
            return new Vector2(center.x + extents.x, center.y);
        }
        else if (direction == Vector2.left)
        {
            return new Vector2(center.x - extents.x, center.y);
        }
        else if (direction == Vector2.down)
        {
            return new Vector2(center.x, center.y - extents.y);
        }
        else if (direction == Vector2.up)
        {
            return new Vector2(center.x, center.y + extents.y);
        }
        else
        {
            return Vector2.zero;
        }
    }

    public LayerMask L1; 
    public LayerMask L2;
    [DisplayOnly]
    public LayerMask Currrtten;
    [SerializeField]
    bool 销毁_;
    public bool 销毁
    {
        get => 销毁_;
        set
        {
            if (value)
            {
                if (Debul)
                {
                    Debug.LogError("         public bool 销毁    public bool 销毁      AAA");
                }

            }
            销毁_ = value;
        }
    }


    public bool 暂停 { get => 暂停1; set => 暂停1 = value; }


    /// <summary>
    /// 微妙的 多种不同毁灭情况下多种时间
    /// 碰到 墙壁
    /// 和玩家引线爆炸  和玩家箭头
    /// 箭头没有攻击时候 从上往下不发生 踩发生
    /// 箭头有的时候 分箭头时间 踩时间
    /// </summary>
    public float TTime1
    {
        get => TTime; private set
        {
            if (TTime != value)
            {
                if (Debul)
                {
                    Debug.LogError(value + "         vvv   ");
                }

                TTime = value;
            }


        }
    }
    public bool 旋转1 { get => 旋转; set => 旋转 = value; }

    public void 反作用力(int i)
    {
        transform.position -= (Vector3)方向.normalized * 帧移动距离 * i;
    }
    void 引线爆炸(Collider2D c)
    {

        if (Debul) Debug.LogError("    void 引线爆炸(Collider2D c)                " + c.gameObject.name);
        if (Time.realtimeSinceStartup - WakeTime < 0.2f) return;
        if (Player3.I.transform.parent == gameObject.transform)
        {

            Player3.I.ChangeFather();
        }

        if (!箭头伤害)
        {
            bool 方向是上下 = (方向.v2_To方向() == E_方向.上 || 方向.v2_To方向() == E_方向.下);
            if (方向是上下)
            {
                if (c.gameObject.layer == Initialize.L_Player)
                {
                    return;
                }
            }
        }

        开毁();

        if (!c.TryGetComponent<被打消失>(out var bb))
        {

            if (c.gameObject == Boss.魔理沙.I.gameObject && Boss.魔理沙.I.T扫把 == gameObject.transform)
                Event_M.I.Invoke(Event_M.扫把打到了, c.gameObject);



            if (箭头伤害) if (atkvalue != 0) if (c.TryGetComponent<I_生命>(out var sm))
                sm.被扣血(atkvalue, gameObject, Initialize.Get_随机Int());


        }

        if (c.gameObject.layer != Initialize.L_Player) TTime1 = -0.1f;
    }

    [SerializeField]
    float TTime;

    [SerializeField]
    [DisplayOnly]
    RaycastHit2D[] Ra;



    public bool 运动暂停;

    bool 旋转;


    [SerializeField]
    float 重力加速度乘 = 0.022f;
    Vector2 模拟速度_;
    [SerializeField]
    Vector2 模拟速度 = new Vector2(0, 4f);
    public void 旋转触发(int I)
    {
        if (I == -1)
        {
            Debug.LogError("圆圈  圆圈    圆圈   圆圈   圆圈aaaaaaaaaaaaaaa圆    圈");
            特效_pool_2.I.GetPool(transform.position, T_N.特效大爆炸).Speed_Lv = Speed_Lv;
            旋转1 = false;
            暂停 = false;
            销毁 = false;
            方向 = Vector2.down;
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (I == 1)
        {
            特效_pool_2.I.GetPool(transform.position, T_N.特效大爆炸).Speed_Lv = Speed_Lv;
            旋转1 = false;
            暂停 = false;
            销毁 = false;
            方向 = Vector2.up;
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (I == 0)
        {

            if (!旋转1)
            {

                旋转1 = true;
                暂停 = true;
                销毁 = false;
                方向 = Vector2.up;
                模拟速度_ = 模拟速度;
            }
            else
            {
                if (!Player3.I.N_.箭矢弹反) return;

                特效_pool_2.I.GetPool(transform.position, T_N.特效大爆炸).Speed_Lv = Speed_Lv;
                旋转1 = false;
                暂停 = false;
                销毁 = false;
                方向 = new Vector2(Player3.I.LocalScaleX_Set, 0);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

        }
    }

    bool 不会碰撞消失;
    private static   Collider2D[] s_hitBuffer = new Collider2D[8];
    ///返回相对的v2 差值
    Vector2 aaaa(Vector2 po, Vector2 value)
    {

        if (value.y == 0)
        {
            return new Vector2(po.x - value.x, 0);
        }
        if (value.x == 0)
        {
            return new Vector2(0, po.y - value.y);
        }
        Debug.LogError("这不对把啊哈啊哈" + gameObject.name + transform.position);
        return Vector2.zero;
    }
    /// <summary>
    /// 根据方向剔除  v2分量
    /// </summary>
    /// <param name="way"></param>
    /// <param name="valu"></param>
    /// <returns></returns>
    Vector2 asd(Vector2 way, Vector2 valu)
    {
        var x = way.x;
        var y = way.y;
        if (x == 0)
        {
            return new Vector2(0, valu.y);
        }
        if (y == 0)
        {
            return new Vector2(valu.x, 0);
        }
        Debug.LogError("这不对把啊哈啊哈" + gameObject.name + transform.position);
        return Vector2.zero;
    }
    public bool 用Fly限制;

    private void UUpdate()
    {
        if (方向 == Vector2.zero) return;
        if (!gameObject.activeSelf) return;
        lookme = I_S.固定等级差 * self_speed;
        //if (I_S.固定等级差>I_S.c) return;

        if (Debul)
        {
            Debug.LogError(lookme + "lookme");
        }
        if (用Fly限制)
        {
            lookme = Initialize_Mono.I.Mi.GetFlyGMin(lookme);
        }
        else
        {
            lookme = Initialize_Mono.I.Mi.GetMin(lookme);
        }

        if (Debul)
        {
            Debug.LogError(lookme + "lookme___后面");
        }
        // 缓存局部变量 
        销毁倒计时();

        var a = 无视 && 无视盒子.Contains(transform.position);
        //if(Debul)     Debug.LogError(a +"    "+ 无视盒子.size);


        if (!运动暂停)
        {
            if (!旋转1)
            {
                Vector2 n = 方向.normalized; // 仅一次归一化

                transform.position += (Vector3)n * 帧移动距离;
            }
            else
            {

                //Vector3 value =
                //    new Vector3(模拟速度_.x,MathF.Max
                //    (模拟速度_.y,Initialize_Mono.I.最大箭矢坠落速度)  );
                transform.position += ((Vector3)模拟速度_) * 帧移动距离;
            }
        }
        if (!a) 箭头();
        if (方向.x != 0)
        {
            var ls = sp.transform.localScale;
            sp.transform.localScale = new Vector3(方向.x, ls.y, 1);
        }

        // 只获取一次边界点集合 
        if (Initialize_Mono.I.Updatee) 帧移动距离 = lookme * Time.deltaTime;
        else 帧移动距离 = lookme * Time.fixedDeltaTime;
        if (Debul)
        {
            Debug.LogError("    WWWWQWQWQQQQ  " + 帧移动距离);
        }
        if (旋转1)
        {
            帧移动距离 *= Initialize.返回正负号(帧移动距离);

            float Deletime = Time.fixedDeltaTime;
            if (Initialize_Mono.I.Updatee) Deletime = Time.deltaTime;

            transform.Rotate(帧旋转速度 * Initialize_Mono.I.GetMin(I_S.固定等级差) * Deletime * 0.21f);

            float Y = 模拟速度_.y - Initialize_Mono.I.假物理重力 * 重力加速度乘 *
                Initialize_Mono.I.GetMin(I_S.固定等级差) * Deletime;
            模拟速度_.y = MathF.Max(Y, Initialize_Mono.I.最大箭矢坠落速度); // 原地更新，不创建新向量

            //Debug.LogError(模拟速度_.y);
            //if (Y < -30)  死();
        }
    }
    private void FixedUpdate()
    {
        if (!Initialize_Mono.I.Updatee)
        {
            UUpdate();

        }
    }
    private void Update()
    {
        if (I_S.固定等级差 >= Initialize_Mono.I.Sz.YesPowers[2] || 旋转1)
        {
            /// 大于2 不碰撞 
            //bc.isTrigger = true;
            gameObject.layer = Initialize.L_Enemy;
        }
        else
        {
            //bc.isTrigger = false;
            gameObject.layer = Initialize.L_M_Ground;
        }
        if (Initialize_Mono.I.Updatee)
            UUpdate();
    }
    public static bool f方向可以(Vector2 direction, Vector2 compareVector)
    {
        // 规则1: 方向向量的X只能是-1, 0, 1，Y同理
        if (!Mathf.Approximately(Mathf.Abs(direction.x), 0f) &&
            !Mathf.Approximately(Mathf.Abs(direction.x), 1f))
            return false;

        if (!Mathf.Approximately(Mathf.Abs(direction.y), 0f) &&
            !Mathf.Approximately(Mathf.Abs(direction.y), 1f))
            return false;

        // 规则2: X不为0时Y必须为0，Y不为0时X必须为0
        if (!Mathf.Approximately(direction.x, 0f) && !Mathf.Approximately(direction.y, 0f))
            return false;

        // 规则3: 不能同时为0（至少有一个方向）
        if (Mathf.Approximately(direction.x, 0f) && Mathf.Approximately(direction.y, 0f))
            return false;

        // 检查第二个向量对应分量
        if (!Mathf.Approximately(direction.x, 0f))
        {
            // X方向比较
            if (Mathf.Approximately(compareVector.x, 0f))
                return false; // 第二个向量X分量为0

            // 检查正负号是否一致（同号）
            return Mathf.Sign(direction.x) == Mathf.Sign(compareVector.x);
        }
        else // direction.y不为0
        {
            // Y方向比较
            if (Mathf.Approximately(compareVector.y, 0f))
                return false; // 第二个向量Y分量为0

            // 检查正负号是否一致（同号）
            return Mathf.Sign(direction.y) == Mathf.Sign(compareVector.y);
        }
    }
    private void 箭头()
    {

        if (旋转) return;
        var dirEnum = 方向.v2_To方向();

     var p=   bc.bounds.九个点(dirEnum);
        s_hitBuffer = Physics2D.OverlapCircleAll(p,0.2f, Currrtten);
        for (int q = 0; q < s_hitBuffer.Length; q++)
        {
           var col = s_hitBuffer[q];

            if (col.gameObject.TryGetComponent<Fly_Ground>(out var f)) continue;
            if (col == null) continue;
            if (col == bc) continue;
            if ( col.CompareTag(Initialize.Player))
            {
                if ( MathF.Abs(方向.x) ==1)
                {///横向
                    var BB = f方向可以(方向, Player3.I.Velocity);
                    if (BB)
                    {
                        //同方
                        continue;
                    }
                       else   if (Player3.I.Velocity.y < -2)
                 {
                        ///方向不一样
                        是玩家噶的 = true;
                        引线爆炸(col);
                        break; 
                    }
                }
                else
                {
                    /////这边处理竖直情况
                    continue;
                }
                break;
            }
            引线爆炸(col);
        }
        return; 
    }

    [SerializeField]
    float lookme;
    [SerializeField]
    Vector3 帧旋转速度 = new Vector3(0, 0, 800f);
    public float 帧移动距离;
    [SerializeField]
    private float self_speed1 = 1;

    public void 扣攻击(float i)
    {
    }

    public bool Debul = false;


    int LastC;
    void 开毁()
    {

        if (!销毁)
        {
            LastC = Time.frameCount;
            销毁 = true;
        }
        销毁倒计时();
    }

    /// <summary>
    /// 当帧距离太快 延迟一帧爆炸    偏移太过明显  当一帧内帧移动距离超过该值  那久不延迟 直接爆炸 
    /// </summary>
    static float 延迟爆炸最小距离 = 0.1f;
    RaycastHit2D[] Rl;
    void 销毁倒计时()
    {
        if (销毁 && !暂停)
        {
            TTime1 -= 帧移动距离 / self_speed;
            if (Debul) Debug.LogError("TTime TTime TTime +++++" + TTime1 + "   " + 帧移动距离 + transform.position);
        }

        bool 延迟 = Time.frameCount - LastC >= 1 || 帧移动距离 > 0.1f;
        if (TTime1 < 0 && 销毁 && 延迟 && !暂停)
        {
            Rl = Physics2D.BoxCastAll(盒子.center, 盒子.size + (Vector3.one * 0.5f), 0, Vector2.zero, 0);
            if (Debul)
            {
                foreach (var item in Rl)
                {
                    Debug.LogError(item.collider.gameObject);
                }
            }
            for (int i = 0; i < Rl.Length; i++)
            {
                var obj = Rl[i].collider.gameObject;



                if (爆炸伤害) if (obj.TryGetComponent<I_生命>(out var s)) s.被扣血(atkvalue, gameObject, Initialize.Get_随机Int());

                if (触发冰块) if (obj.TryGetComponent<被打消失>(out var bb)) bb.被爆炸物触发();

                if (爆炸送走z) if (obj.CompareTag(Initialize.Player)) Player3.I.安全地点();
                //obj.GetComponent<被打消失>()?.被爆炸物触发();
            }
            //a.Get_碰撞组<I_生命>()?.被扣血(atkvalue, gameObject, Initialize.Get_随机Int());
            //a.Get_碰撞组<被打消失>()?.被爆炸物触发();
            死();
        }
    }
    void 死()
    {
        if (Debul) Debug.LogError("AAAAAAAAAAAAAAA" + transform.position);

        if (是玩家噶的) Player3.I.ChangeFather();


        if (Debul)
        {
            Debug.LogError(盒子.size);
        }
        var a = 盒子.阵列盒子();
        for (int i = 0; i < a.Count; i++)
        {
            特效_pool_2.I.GetPool(a[i], T_N.特效砖块爆炸, Player3.I.sp).Speed_Lv = Speed_Lv;

        }
        销毁触发?.Invoke();


        if (回池子)
        {
            if (Pool_Key_name != null || Pool_Key_name != "")
            {
                Surp_Pool.I.ReturnPool(gameObject);
            }
        }


    }
    public bool 回池子 = true;
    float WakeTime;
    private void OnEnable()
    {
        WakeTime = Time.realtimeSinceStartup;
    }
    private void Start()
    {
        Currrtten = L1;

        Start_方向 = 方向;
        Stat_Speed_Lv = Speed_Lv;
        Stat_self_speed = self_speed;
    }
    Vector2 Start_方向;
    float Stat_Speed_Lv = 1;
    float Stat_self_speed = 1;

    [SerializeField]
    [DisplayOnly]
    private bool 暂停1;

    bool is_dead;


    public void 重制()
    {
        无视盒子 = default;
        旋转1 = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        is_dead = false;
        是玩家噶的 = false;
        Currrtten = L1;
        //Debul = false;
        TTime1 = Initialize_Mono.I.F_Time_碰到玩家后销毁时间;
        暂停 = false;
        销毁 = false;
        bc.enabled = true;
        sp.enabled = true;

        方向 = Start_方向;
        Speed_Lv = Stat_Speed_Lv;
        self_speed = Stat_self_speed;
    }

    public bool Dead()
    {
        return true;
    }

    public void 假死了(bool 假死不)
    {
        if (is_dead) return;
        if (假死不)
        {
            if (是玩家噶的) Player3.I.ChangeFather();


            bc.enabled = false;
            sp.enabled = false;
        }
        else
        {
            bc.enabled = true;
            sp.enabled = true;
        }
    }
}
