using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
interface I_消弹
{
    public bool 被消弹(float i);
}
public class Bullet_base : MonoBehaviour, I_Speed_Change
{
    /// <summary>
    ///  应该是一个列表
    ///  
    /// 时间达到？时，invok 并且移除方法
    /// 
    /// </summary>
    //[DictionaryDrawerSettings]

    [SerializeField] BoxCollider2D bc;
    public List<float> Times = new List<float>();
    public List<Action> Actions = new List<Action>();

    private void OnDisable()
    {
        Times.Clear();
        Actions.Clear();
    }
    public void Add(float T, Action A)
    {
        if (!Times.Contains(T))
        {
            Times.Add(生命周期 - T);
            Actions.Add(A);
        }
    }
    void 字典刷新()
    {
        for (int i = 0; i < Times.Count; i++)
        {
            if (生命周期 < Times[i])
            {
                Actions[i]?.Invoke();
                Times[i] = -999;
            }
        }
    }
    public static float 方向转角度(Vector2 a, Vector2 b)
    {
        var t = a - b;
        t.Normalize();
        return Initialize.To_方向到角度(t) / Time.fixedDeltaTime;
    }
    public bool Deb;

    public float L线速度;
    public float A角速度
    {
        get => a角速度; set
        {
            //if (a角速度 != value)
            //{
            //    Debug.LogError("  原先  " + a角速度 + "  后来     " + value);
            //}
            a角速度 = value;
        }

    }
    [DisplayOnly]
    [SerializeField]
    float a角速度;
    public float L_Acc线加速度;
    public float A_Acc角加速度;
    public float Max速度 = int.MaxValue;
    public float 生命周期 = 5;

    [SerializeField] Vector2 方向1;
    public Vector3 eulerAngles;
    public bool 自身旋转;
    public new Transform transform => base.transform;
    public GameObject 对象 => base.gameObject;

    public Action 变速触发 { get; set; }

    public I_Speed_Change I_S { get => (I_Speed_Change)this; }

    public float Current_Speed_LV
    {
        get
        {
            if (L线速度 == 0)
            {
                return Speed_Lv;
            }
            else
            {
                return Speed_Lv * L线速度;
            }
        }
    }


    public float Speed_Lv { get => speed_Lv; set => speed_Lv = value; }
    public Vector2 方向 { get => 方向1; set {
            if (方向1 != value)
            {
                if (Deb)
                {
                    Debug.LogError(value);
                }
            }
            方向1 = value; } }

    public float speed_Lv = 1f;
    [Range(0, 1)]
    public float 追踪玩家 = 0;

    public Vector2 返回当前指向玩家的方向(Vector2 v = default)
    {
        if (v == default)
        {
            v = transform.position;
        }
        Vector2 玩家方向 = (Vector2)(Player3.I.transform.position - (Vector3)v);
        玩家方向.Normalize();
        return 玩家方向;
    }
    [SerializeField]
    protected LayerMask 子弹碰撞;

    [DisplayOnly]
    [SerializeField]
    protected float Current_Speed;
    protected virtual void Update()
    {
        Current_Speed = Current_Speed_LV;
        //if (bc.IsTouching(Player3.I.站立box)|| bc.IsTouching(Player3.I.蹲BOX))
        if (bc.IsTouchingLayers(1 << Initialize.L_Player)) 
     {
            if (Player3.I.Bounds.min.y>=bc.bounds.max.y )
            {
                if (PlayerisUp==false)
                {
                    刚刚在我头上?.Invoke( );
                }
                PlayerisUp = true; 
            }
        }
        else
        {
            PlayerisUp = false;
        }
    }
    public Action 刚刚在我头上;
    bool PlayerisUp;

    public float 真实移动速度 => Initialize_Mono.I.GetMin(L线速度 * I_S.固定等级差);
    ///线速度就是自身速度

    /// <summary>
    /// 碰到  地面  玩家
    /// 申明周期无
    /// </summary>
    protected virtual    void FixedUpdate()
    {

        L线速度 = Mathf.Clamp(L线速度 + L_Acc线加速度 * Time.fixedDeltaTime * I_S.固定等级差, -Max速度, Max速度); 
        A角速度 += A_Acc角加速度 * Time.fixedDeltaTime * I_S.固定等级差; 
        if (!自身旋转)
        {
  
                Vector2 玩家方向 = 返回当前指向玩家的方向();

 
  
                方向 = Vector2.Lerp(Initialize.To_角度到方向(A角速度 * Time.fixedDeltaTime), 玩家方向, 追踪玩家);

 
            var 下一目标 = position +   (Vector3)方向 * Time.fixedDeltaTime * 真实移动速度;

            var Ray = Initialize.碰撞两点检测(position, 下一目标, 子弹碰撞);
        var a= Ray.point;

            if (a == Vector2.zero)
            { 
                position = 下一目标;
            }
            else  
            {
                ////下一帧将会发生碰撞 
                ///
                if (Ray.collider.CompareTag(Initialize.Player))
                {
                    position = 下一目标; 
                }
                else
                {
                    ///停止
                    ///
                    position = a; 
                } 
            }
        }
        else
        { 
            transform.rotation *= Quaternion.Euler(new Vector3(0, 0, 1f) * A角速度 * Time.fixedDeltaTime * I_S.固定等级差);
 
            position +=   Vector3.right * Time.fixedDeltaTime * 真实移动速度;
            //transform.Translate(L线速度 * Vector2.right * Time.fixedDeltaTime * I_S.固定等级差, Space.Self); 
            //if (Deb) Debug.LogError("");
        }

        生命周期 -= Time.fixedDeltaTime * 真实移动速度;

        字典刷新(); 

        if (生命周期 <= 0)
        {
            我死了();
        }
    }
    
    public Action<Bullet_base> 结束;
    protected virtual void 我死了()
    {
        Destroy(gameObject);
    }

    public Vector3 position
    {
        get { return transform.position; }
        set { transform.position=value;
        
        }
    }  
}

public class Bullet : Bullet_base, I_攻击, I_ReturnPool, I_消弹, I_消失进度
{
    Sprite StartSprite;
    SpriteRenderer sp;
    private void Start()
    {
        sp = GetComponentInChildren<SpriteRenderer>();
        StartSprite = sp.sprite;

        刚刚在我头上 += () => {
            if (deadtime==-1)
            {
                deadtime = 0;
            }
        };
    }
    [SerializeField]
    float deadtime = -1;

    /// <summary>
    ///    难崩
    /// </summary>
    /// <param name="collision"></param>
    protected override void FixedUpdate() 
    {

        base.FixedUpdate();
        var a = Physics2D.BoxCast(transform.position, transform.localScale, 0f, Vector2.zero, 0f,  子弹碰撞);
        if (a)
        {
            if (a.collider!=null&&!a.collider.isTrigger)
            {

      
            if (a.transform.gameObject.layer == Initialize.L_Player && deadtime == -1f)
                {
                    if (!无害化)
                    {
                        if (a.collider.gameObject == Player3.I.gameObject)
                        {
                            结束?.Invoke(this);
                            deadtime = 0;
                        }
                    } 
            }
            else if (a.transform.gameObject.layer == Initialize.L_Ground)
            {
                我死了();
                }
            }
        }
         
        if (deadtime >= 0)
        {
   
            deadtime += Time.fixedDeltaTime * I_S.固定等级差;
            进度 = deadtime / Initialize_Mono.I.子弹引线时间;
 
            if (deadtime > Initialize_Mono.I.子弹引线时间)
            {
                我死了();
            }
        }
        是 = deadtime >= 0;
    }


    public bool 是 { get; set; }
    public float 进度 { get; set; }
    public float atkvalue { get; set; } = 10f;
    public string Pool_Key_name { get; set; } = Surp_Pool.能量子弹;
    public void 扣攻击(float i)
    {
    }
    private void OnEnable()
    {
        重制();
    }
    public void 重制() {
        if (sp!=null)
        {

        sp.sprite = StartSprite;
        }
        无害化 = false;
    }
    public bool 成为平台 = true;
    public bool 可以被消灭 = true;

 


      bool 无害化;
    /// <summary>
    /// 返回BOOL表示有没有成功
    /// </summary>
    /// <returns></returns>
    public bool 被消弹(float i)
    {
        Debu.LogError(i+"AAAAAAAAAAAAAAAAAAAAAAA");
        if (可以被消灭)
        {
            if (成为平台)
            {           
                ///平A可以让 子弹无害化   
                ///原批只能让子弹消失？？
                ///
                ///消失0   无害1
                if (i._is(1))
                {
                    if (!无害化)
                    {///第一次触发
                        无害化 = true;
                        deadtime = 0f;
                        特效_pool_2.I.GetPool(transform.position, T_N.特效消弹);
                        sp.sprite = Initialize_Mono.I.无害子弹图片;
                    }
                    else
                    {
                        ///反复触发无效
                    }
                }
                else
                {
                    if (!无害化)
                    {
                        特效_pool_2.I.GetPool(transform.position, T_N.特效消弹);

                        无了();
                    }
             
                } 

            } 
            return true;
        }
        else
        {
            return false;
        }
    }
    void 无了()
        {
        deadtime = -1; 
        结束?.Invoke(this);
        Surp_Pool.I.ReturnPool(gameObject);

    }
    
  protected override void 我死了()
    {
 
        if(!无害化)
        {
            var a = Physics2D.OverlapCircle(transform.position, 0.5f, 1 << Initialize.L_Player);
            if (a != null) Player3.I.被扣血(atkvalue, gameObject, 0);
        }


        可以被消灭 = true;
        特效_pool_2.I.GetPool(transform .position , T_N.特效闪光爆炸);

        无了();
    }

 
}


