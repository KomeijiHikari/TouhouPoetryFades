using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly_Ground : MonoBehaviour, I_Speed_Change, I_攻击, I_ReturnPool, I_Dead,I_假死
{

    public bool 不下落 = false;
    public GameObject 对象 { get => gameObject; }
    public string Pool_Key_name { get  ; set  ; }
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
    private void OnCollisionEnter2D(Collision2D co)
    {
 
        bool B = ((1 << co.gameObject.layer) & Currrtten.value) > 0;
        if (Time.realtimeSinceStartup - tim < 0.5f) return;

        if (co.gameObject.layer == Initialize.L_Player)
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
 
                TTime1 = Initialize_Mono.I.F_Time_踩上去自爆的时间 ;
                方向 = new Vector2(方向.x, -1);
                是玩家噶的 = true;
                开毁();
            }


        }
    }
    public bool 是玩家噶的;
    private void OnDisable()
    {
        if (是我)
        {
            Player3.I.ChangeFather();
        }
    }
    private void OnCollisionExit2D(Collision2D co)
    {
        if (co.gameObject.layer == Initialize.L_Player)
        {
            是我=false;
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

    public Bounds 盒子 { get {
            if (是SP而不是BC)
            {
                return sp.bounds;
            }
            return bc.bounds;
            
        } }
    public bool 是SP而不是BC=true;

    private void Awake()
    {
        if (sp == null) Initialize.组件(gameObject, ref sp);

        Initialize.组件(gameObject, ref an);
        Initialize.组件(gameObject, ref bc);
        gameObject.layer = Initialize.L_M_Ground;

        //变速触发 += () => { sp.闪光(0.02f); };
        TTime1 = Initialize_Mono.I.F_Time_踩上去自爆的时间;

        //销毁触发 += () =>
        //{
        //    var a = 盒子.阵列盒子();
        //    for (int i = 0; i < a.Count; i++)
        //    {
        //        var B = 特效_pool_2.I.GetPool(a[i], T_N.特效砖块爆炸, Player3.I.sp);
        //    }
        //};

        Start();
         
    }

    float Selllf_Speed = 2;
    public void 初始化(Vector2 方向, Vector2 位置, float SpeedLv = 1, float atkv = 1)
    {

        tim = Time.realtimeSinceStartup;
        //atkvalue = atkv;
        transform.position = 位置;
        self_speed = Selllf_Speed;
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
            销毁_ = value;
        }
    }


    public bool 暂停 { get => 暂停1; set => 暂停1 = value; }
    public float TTime1 { get => TTime;private     set => TTime = value; }
    public bool 旋转1 { get => 旋转; set => 旋转 = value; }

    public void 反作用力(int i)
    {
        transform.position -= (Vector3)方向.normalized * 帧移动距离 * i;
    }
    void 引线爆炸(Collider2D c)
    {
        //Debug.LogError("AAAAAAAAAAAAAAAA                "      +c.gameObject .name);
        bool 方向是上下 = (方向.v2_To方向() == E_方向.上 || 方向.v2_To方向() == E_方向.下);
        if (Time.realtimeSinceStartup - WakeTime < 0.2f) return;
        if (Player3.I.transform.parent == gameObject.transform)
        {
        
               Player3.I.ChangeFather();
        }
        if (方向是上下)
        {
            if (c.gameObject.layer == Initialize.L_Player)
            {
                return;
            }
        }
        开毁();
        if (c.GetComponent<被打消失>() == null)
        {

            if (Boss.魔理沙.I.T扫把 == gameObject.transform)
            { 
                    Event_M.I.Invoke(Event_M.扫把打到了,c.gameObject);
          } 
        

            c.GetComponent<I_生命>()?.被扣血(atkvalue, gameObject, Initialize .Get_随机Int());
        }

        if (c.gameObject.layer != Initialize.L_Player) TTime1 = -0.1f;
    }
 
    [SerializeField]
    float TTime  ;
    [SerializeField]
    [DisplayOnly]
    GameObject Last;
    [SerializeField]
    [DisplayOnly]
    Collider2D Currtent;

    [SerializeField]
    [DisplayOnly]
    RaycastHit2D[] Ra;

    public bool 可以旋转;

    public bool 运动暂停;

    bool 旋转;


    
    Vector2 模拟速度_;
   Vector2 模拟速度=new Vector2 (0,4f);
    public void 旋转触发(int I)
    {
        Debug.LogError("chufa?");
        if (!旋转1)
        {  
            旋转1 = true;
            暂停 = true;
            销毁 = false;
            方向 = Vector2.up; 
            模拟速度_ = 模拟速度;
        }     else  if (I == -1)
        {
            特效_pool_2.I.GetPool(transform.position, T_N.特效大爆炸);
            旋转1 = false;
                暂停 = false;
                销毁 = false;
                方向 = Vector2.down;
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (I == 1)
        {
            特效_pool_2.I.GetPool(transform.position, T_N.特效大爆炸);
            旋转1 = false;
                暂停 = false;
                销毁 = false;
                方向 = Vector2.up;
                transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (I == 0)
        {
            特效_pool_2.I.GetPool(transform .position ,T_N.特效大爆炸);
            旋转1 = false;
            暂停 = false;
            销毁 = false;
            方向 = new Vector2(Player3.I.LocalScaleX_Set, 0);
            transform.rotation = Quaternion.Euler(0, 0,0); 
        }
        if (旋转1 ==false )
        {
            不会碰撞消失 = true;
            Initialize_Mono.I.Waite(
                () => { 不会碰撞消失 = false;  },0.5f
                );
        }
    }

    bool 不会碰撞消失;
    private void FixedUpdate()
    {
        //((Vector2)盒子.center).DraClirl(2f,Color.cyan);
        if (方向 == Vector2.zero) return;
        if (!gameObject.activeSelf) return;
        if (!运动暂停) 
        {
            if (!旋转1) transform.position += (Vector3)方向.normalized * 帧移动距离;
            else   transform.position += (Vector3)模拟速度_ *Time.fixedDeltaTime  ;
        } 

        //var o = GetEdgeCenter(方向 );

        if (方向.x != 0) sp.transform.localScale = new Vector2(方向.x, sp.transform.localScale.y);


        //var o = bc.bounds.九个点(方向.v2_To方向()) + 方向 * 0.01f;
        if (Debul) Debug.LogError(方向.v2_To方向());
        var L = bc.bounds.边上三点(方向.v2_To方向());
        //RaycastHit2D Hit;
        Collider2D Hit = null;
        for (int i = 0; i < L.Count ; i++)
        {
            var item = L[i];
            if (Debul) Debug.LogError(item);
            if (Debul) Debug.DrawRay(item, 方向 * 0.1f, Color.blue);
            if (!不会碰撞消失)
            {
                Hit = Physics2D.Raycast(item, 方向, 0.1f, Currrtten).collider;
            }
            if (Hit==bc)    break; 

            if (Hit == null)
            {
                Currtent = null;
            }
            else
            {
                if (方向 == Vector2.up && Hit.gameObject.layer == Initialize.L_Player)
                {
                    ///被玩家弹飞
                    是玩家噶的 = true;
                    Hit = null;
                }
                ///碰到了其他东西
                Currtent = Hit;
                break;
            }
        } 
        if (Currtent == null)
        {
            Last = null;
        }
        else if(Currtent.gameObject != Last)
        {
            Last = Currtent.gameObject;
            引线爆炸(Currtent);
        }


        //帧移动距离 = Initialize.返回正负号(帧移动距离) * Mathf.Min(Mathf .Abs (帧移动距离) , Initialize_Mono.I.阀值2_5 * 0.01f);
        if (旋转1)
        {
            transform.Rotate(帧旋转速度 * Time.fixedDeltaTime);
            var Y = 模拟速度_.y;
            Y-=Initialize_Mono .I.假物理重力*0.3f* Time.fixedDeltaTime *  I_S.固定等级差;

            //Debug.LogError(Y);
            模拟速度_ = new Vector2(模拟速度_.x, Y );

            if (Y < -30) 死();
        }
        else
        {
            帧移动距离 = I_S.固定等级差 * self_speed * Time.fixedDeltaTime;
            帧移动距离 = Mathf.Min(帧移动距离, Initialize_Mono.I.阀值2_5 * 0.01f);
        } 
               销毁倒计时();
    }
 Vector3 帧旋转速度=new Vector3(0,0,800f) ;
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
        LastC = Time.frameCount;

        销毁 = true; 
    }
    void 销毁倒计时()
    {
        if (销毁 && !暂停)
        {
            TTime1 -= 帧移动距离;
            if (Debul)    Debug.LogError("TTime TTime TTime +++++" + TTime1);
        
        }

        if (TTime1 < 0 && 销毁 && Time.frameCount - LastC > 1 && !暂停)
        {
            var a = Physics2D.BoxCastAll(盒子.center, 盒子.size * 2, 0, Vector2.zero, 0, 1 << Initialize.L_Ground);
            if (Debul)
            {
                foreach (var item in a)
                {
                    Debug.LogError(item.collider.gameObject);
                }
            }

            a.Get_碰撞组<被打消失>()?.被爆炸物触发();
            死();
        } 
    } 
    void 死()
    {
      

        if (是玩家噶的) Player3.I.ChangeFather();


        if (Debul)
        {
            Debug.LogError(盒子.size);
        }
        var a = 盒子.阵列盒子();
        for (int i = 0; i < a.Count; i++)
        {
            var B = 特效_pool_2.I.GetPool(a[i], T_N.特效砖块爆炸, Player3.I.sp);
        }
        销毁触发?.Invoke();


        if (回池子)
        { 
        if (Pool_Key_name != null|| Pool_Key_name != "")
        { 
            Surp_Pool.I.ReturnPool(gameObject);
        }
        }


    }
    public bool  回池子=true ;
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
        旋转1 = false; 
        transform.rotation = Quaternion.Euler(0, 0, 0);
        is_dead =false ;
          是玩家噶的 = false;
        Currrtten = L1;
        Debul = false;
        TTime1 =Initialize_Mono .I.   F_Time_弹反销毁时间;
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


            bc.enabled = false ;
            sp.enabled = false ;
        }
        else
        {
            bc.enabled =true;
            sp.enabled = true;
        }
    }
}
