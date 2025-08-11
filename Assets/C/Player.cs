using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

interface 操控
{
    public void 交互();

    public void 跳跃();
    public void 向目标水平移动(GameObject obj);
}
interface 攻击
{ 
    public void 攻击();
}



 public   interface set_get
{
    public Vector2 Velocity { get; set; }
    public float Linear { get; set; }
    public float GravityScale { get; set; }
 
    public void AddForce(Vector2 vector2 ,ForceMode2D mode2D)
    {

    }
    public void AddForce(Vector2 vector2)
    {

    }
}
public class Biology : BiologyBase
{
 

    public bool 下蹲表示 { get; set; }

    public Action 出了洞 { get; set; }
    /// <summary>
    /// 头上没东西是true
    /// </summary>
    ///     [SerializeField]

 

    public 功能数值 生物数值;

    float localScale_X;
  protected   override   void 出了洞嘛()
    {
        if (下蹲表示)
        {
            出了洞?.Invoke();
        }
    }

    new  private void Awake()
    {
        base.Awake();


        //ground_B = 在自己的底部生成地面检测();
        A_wake();



        //ground_B.接触地面事件 += 接地动作;
        //ground_B.离开地面事件 += 离地动作;
  接触地面事件 += 接地动作;
  离开地面事件 += 离地动作;

        localScale_X = transform.localScale.x;

    }

    public virtual void 离地动作()
    {
 
    }

    Ground_bool 在自己的底部生成地面检测()
    {
        GameObject Ground_bool;
        Ground_bool gb;
        Ground_bool = new GameObject("Ground_bool");
        Ground_bool.transform.parent = this.transform;

        Ground_bool.transform.localPosition = new Vector2(0, -sp.bounds.size.y / 2 / transform.localScale.y);
        return gb = Ground_bool.AddComponent<Ground_bool>();
    }

    

     private void Update()
    {
 
        水平和竖直速度限制();
        U_pdate();
        生物数值.更新();
    }

    private void Start()
    {
  
        生物数值.Relyon(this, "妖梦灵魂");
        Player_input.I.方向变动 += 角色翻转更新;
        S_tart();


    }

 void FixedUpdate()
    {
 
        F_ixedUpdate();
    }

    public virtual void 接地动作()
    {

    }
    public bool 翻转开关_ = true;
    public bool 翻转开关
    {
        get
        {
            return 翻转开关_;
        }
        set
        {
            翻转开关_ = value;
        }
    }
    public void 角色翻转更新(int i)
    {
        //Debug.LogError();
        if (!翻转开关) return;

        LocalScaleX_Int = i;
        transform.localScale = new Vector3(i * localScale_X, transform.localScale.y, transform.localScale.z);


    }
    public void 角色翻转更新()
    {
        if (Player_input.I.按键检测_按住(Player_input.I.右)
            || Player_input.I.按键检测_按住(Player_input.I.左)
            )
        {
            if (灵魂1)
            {
                LocalScaleX_Int = Player_input.I.方向正负;
            }

            transform.localScale = new Vector3(LocalScaleX_Int/* * localScale_X*/, transform.localScale.y, transform.localScale.z);
        }
     


    }
    public bool 速度限制开关_=true;
    public bool 速度限制开关
    {
        get
        {
            return 速度限制开关_;
        }
        set
        {
            速度限制开关_ = value;
        }
    }

    public override Action 生命归零 { get ; set ; }
    public override Action 被打 { get ; set ; }
    public override float 当前hp { get ; set ; }
    public override float hpMax { get ; set ; }

    private void 水平和竖直速度限制()
    {
        if (速度限制开关==true )
        {
            if (Math.Abs(Velocity.x) >= 生物数值.常态速度)
            {
                Velocity=(new Vector2(生物数值.常态速度 * LocalScaleX_Int, Velocity.y));
            }
            if (Velocity.y < -生物数值.最大下落速度)
            {
                Velocity=(new Vector2(Velocity.x, -生物数值.最大下落速度));

            }
        }
       }
        

    public virtual void A_wake()
    {
    }
    public virtual void S_tart()
    {

    }
    public virtual void F_ixedUpdate()
    {

    }
    public virtual void U_pdate()
    {
    }


}
//public string Sprite路径= "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/Capsule.png";
//Sprite sprite = AssetDatabase.LoadAssetAtPath(Sprite路径,typeof (Sprite ))as Sprite);
//public Sprite Sprite;




//    [DisplayOnly]显示不改变一个public
//  [SerializeField] 强行显示并且可改私有
//    [HideInInspector]隐藏public 属性

//    [Range(0, 1)]限定范围
//   [Space]空一行
//[Header("移动参数")]  空一行注解
//[Tooltip("超过这个数开始播放跑步 ")]属性注释

public interface I_攻击
{
    public float atkvalue { get; set; }

    public void 扣攻击(float  i);

}
public  interface I_生命
{
    public Action 生命归零 { get; set; }
    public float 当前hp { get; set; }
    public float hpMax { get; set; }
    public bool HPROCK { get; set; }
 

    public void 被扣血(float  i,GameObject obj,int key);
    public void 扣最大上限(float i);
}

[System.Serializable]
public class 功能数值Base : I_Save
{
    [Header("Boss参数")]
    public bool Boss杀手; 
    private float 水平相反力1 = -190;
    [SerializeField]  private float 水平相反力1_BOSS  = -190;
    private int 最大跳跃次数1_BOSS => 最大跳跃次数1 + 1;
    [SerializeField]   private float 常态速度1_BOSS = 12;
    [SerializeField] private float 起步速度1_BOSS = 100;
    [SerializeField]  private float 跳跃瞬间速度1_BOSS = 25;
    [SerializeField] private float 最大下落速度1_BOSS = 20;
    [SerializeField] private float 最大重力1_BOSS = 5;
    [SerializeField] private float 常态重力1_BOSS = 2;
    [SerializeField] private float 圆斩上升力1_BOSS = 19;

    [Space]
    [SerializeField]
    private float 常态速度1 = 8;
    [SerializeField]
    private float 起步速度1 = 40;
    public float 常态速度
    {
        get
        {
            if (Boss杀手) return 常态速度1_BOSS;
            else return 常态速度1;
        }
        set => 常态速度1 = value;
    }
    public float 圆斩上升力
    {
        get
        {
            if (Boss杀手) return 圆斩上升力1_BOSS;
            else return 圆斩上升力1;
        } 
    }
    public float 起步速度
    {
        get
        {
            if (Boss杀手) return 起步速度1_BOSS;
            else return 起步速度1;
        }
        set => 起步速度1 = value;
    }
    [SerializeField]
    [Space]
    [Header("跳跃参数")]
    private int 最大跳跃次数1 = 1;
    public int 最大跳跃次数
    {
        get
        {
            if (Boss杀手) return 最大跳跃次数1_BOSS;
            else return 最大跳跃次数1;
        }
        set => 最大跳跃次数1 = value;
    }
    [SerializeField]
    protected int 剩余跃次数;
    [SerializeField]
    private float 跳跃瞬间速度1 = 17;
    [SerializeField] private float 圆斩上升力1  = 14;
    public float 跳跃瞬间速度
    {
        get
        {
            if (Boss杀手) return 跳跃瞬间速度1_BOSS;
            else return 跳跃瞬间速度1;
        }
        set => 跳跃瞬间速度1 = value;
    }
    public int 跳跃剩余跃次数
    {
        get
        {
            return 剩余跃次数;
        }
        set
        {
            剩余跃次数 = value;
        }

    }
    [Header("跳跃下落参数")]
    private float 最大下落速度1 = 20;
    private float 最大重力1 = 5;
    private float 常态重力1 = 2;
    public float 下落速度判断临界负数值 = 0.3f;
    [Range(0, 1)]
    public float 下落过渡速度 = 0.5f;
    public float 小跳向下力 { get; set; } = -1450f;
    public float 水平相反力 {
        get
        {
            if (Boss杀手) return 水平相反力1_BOSS;
            else return 水平相反力1;
        }
        set => 水平相反力1 = value; }
    public float 当前Hp;
    public float Max_Hp;
    public float Atk;

    public int 钱;


    public string Name { get => "功能数值数据"; }
    public float 最大下落速度 {
        get
        {
            if (Boss杀手) return 最大下落速度1_BOSS;
            else return 最大下落速度1;
        }
        set => 最大下落速度1 = value; }
    public float 最大重力 {
        get
        {
            if (Boss杀手) return 最大重力1_BOSS;
            else return 最大重力1;
        }
        set => 最大重力1 = value; }
    public float 常态重力 {
        get
        {
            if (Boss杀手) return 常态重力1_BOSS;
            else return 常态重力1;
        }
        set => 常态重力1 = value; }

    public void 保存()
    {
        if (当前Hp == 0)
        {
            Debug.LogError("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAASSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS");
        }
        string s = JsonUtility.ToJson(this);
        Save_D.Add(Name, s);
    }
    public void 读取()
    {
        if (Save_D.存档字典_.ContainsKey(Name))
        {

            Player3.I.玩家数值 = Save_D.Load_Value_D<功能数值Base>(Name, true);
        }
        else
        {
            保存();
        }
    }
}
[System.Serializable]
public class 功能数值: 功能数值Base
{
    public enum 底层状态
    {
        idle,
        run,
        jump,
        dun
    }
    public  底层状态 当前底层状态;
    Biology biology { get;set; }


    [Space]
    [Header("移动参数")]
    [DisplayOnly]
    public Vector2 当前速度监控;

     
    public void 更新()
    {
        
        if (biology == null)
        {
            Debug.Log("空了");
        }
        else
        {
            当前速度监控 = biology.Velocity;
        }


        if (biology.Ground )
        {
            if (biology. 下蹲表示)
            {
                当前底层状态 = 底层状态.dun;
            }
            else if (biology.Velocity.y != 0)
            {
                当前底层状态 = 底层状态.run;
            }
            else
            {
                当前底层状态 = 底层状态.idle;
            }
        }
        else
        {
            当前底层状态 = 底层状态.jump;
        }
    }
    public void Relyon(Biology b, string i)
    {
        当前底层状态 = 底层状态.idle;
 
        this.biology = b;

        剩余跃次数 = 最大跳跃次数;

    }

  
}
public partial class   Player : Biology, I_生命, I_攻击
{
    protected override bool 灵魂 {
        get
        { 
            Player_input.I.输入开关 = 灵魂1;
            return 灵魂1;
        }
        set
        { 
            Player_input.I.输入开关 = value;
            灵魂1 = value;
        }
    }

    [DisplayOnly]
    [SerializeField]
    float 当前hp_;
    bool HPROCK_;
    public  override  bool   HPROCK { get { return HPROCK_; }
        set { HPROCK_ = value; }
    }
    public override float     当前hp
    {
        get
        {
            return 当前hp_;
        }
        set
        {
            if (!HPROCK)
            {
                if (value <=0)
                {
                    当前hp_ = 0;
                    生命归零?.Invoke();
                }
                else
                {
                    当前hp_ = value;
                } 
            } 
        }
    }
 
    [SerializeField]
    float atk_value = 10;
    public override float atkvalue
    {
        get
        {
            return atk_value;
        }
        set
        {
            atk_value = value;
        }
    }

    //public override float hpMax { get;  set; }
    public 玩家受伤效果 受伤;

 
    public override void 被扣血(float i,GameObject obj,int SKey)
    { 
            受伤.EnterHit(i, 10, obj);

        当前hp -= i;

    }

 
}
/// <summary>
/// 什么时候能干什么1
/// Player  类就是 把按键输入和（改变刚体绑定）
/// </summary>
/// 


[DefaultExecutionOrder(-9)]



public partial class Player : Biology 
{

   

    public static Player I { get; private set; }
    Biology_Actioan P_Action;
    [SerializeField]
    private bool 冲刺表示_;
    public bool 冲刺表示
    {
        get
        {
            return 冲刺表示_;
        }
        set
        {
            if (冲刺表示_ && !value)
            {//关掉的时候
                Player.I.速度限制开关 = true;
            }
            else if (!冲刺表示_ && value)
            {//打开的时候
                Player.I.速度限制开关 = false;
            }
            冲刺表示_ = value;
        }
    }

    [SerializeField]
    public bool 跳跃开关_ = true;
    public bool 跳跃开关
    {
        get
        {
            return 跳跃开关_;
        }
        set
        {
            跳跃开关_ = value;
        }
    }
    public bool 移动开关_ = true;
    public bool 移动开关
    {
        get
        {
            return 移动开关_;
        }
        set
        {
            移动开关_ = value;
        }
    }
    public void 闪光(float time)
    {
        Light2D light2D = GetComponentInChildren<Light2D>();
        if (light2D != null)
        {
            StartCoroutine(开闪一下(light2D, time));
        }
        else
        {
            Debug.LogError("灯光为空但是被调用，这是个空引用");

        }

    }

    IEnumerator 开闪一下(Light2D light2D,float t)
    {
        light2D.enabled = true;
        yield return new WaitForSeconds(t);
        light2D.enabled = false;
    }

    public override void A_wake()
    { 
        受伤 = GetComponent<玩家受伤效果 >();
          当前hp = 100;
 
        if (I != null&&I!=this)
        {
            Destroy(gameObject  );
        }
        else
        {
            I = this;
        }
        //if (I != null && I != this)
        //{
        //    Destroy(this);
        //}
        //else
        //{
        //    I = this;
        //}

        P_Action = new Biology_Actioan().B_Actioan(this);


 
    }
    [HideInInspector]
    public Vector2 检测交互机关的范围 = new Vector2(5, 5);

 
   

    public void 松开空格引发的事件(KeyCode k)
    { 
        if (k != Player_input.I.跳跃 ||Player.I.Velocity.y <= 0) return;
        P_Action.小跳向下力(生物数值.小跳向下力);
    }
    public override void 跳跃()
    {

        生物数值.跳跃剩余跃次数--;
        P_Action.jump_瞬间向上力(生物数值.跳跃瞬间速度);
        P_Action.jump_下落恢复重力(生物数值.常态重力);
    }
    public void 按下跳跃引发的事件(KeyCode k)
    {
        if (!跳跃开关) return;
        if (k != Player_input.I.跳跃 || 生物数值.跳跃剩余跃次数 <= 0) return;
        跳跃();
    }
    public override void 离地动作()
    {
        生物数值.跳跃剩余跃次数 = 生物数值.最大跳跃次数 - 1;
        松开下蹲(Player_input.I.下);
 
    }
    public override void S_tart()
    { 

        Player_input.I.KeyDown += 按下跳跃引发的事件;
        //Player_input.I.KeyDown += 按下交互引发的事件;


        Player_input.I.KeyState += 按住下蹲;
        Player_input.I.KeyUp += 松开下蹲;
        Player_input.I.KeyUp += 松开空格引发的事件;

    }
    public override void 接地动作()
    {
        生物数值.跳跃剩余跃次数 = 生物数值.最大跳跃次数;
        P_Action.jump_下落恢复重力(生物数值.常态重力);
     Velocity=(Vector2.zero)  ;
    }
    public override void 右移()
    {
        假正负0 = 1;

    }
    public void 停止()
    {
        假正负0 = 0;
    }
    public override void 左移( )
    {

        假正负0 = -1;
    }
   public  int 假正负0;
    public override void 向目标水平移动(GameObject obj)
    {
        Debug.LogError(obj.name);

        //if (Initialize.方向_A是否在B的左边或者下面(this.gameObject, obj, false))
        //{
        //    朝向 = 1;
        //    P_Action.move(1, 生物数值.起步速度);
        //}
        //else
        //{
        //    朝向 = -1;
        //    P_Action.move(-1, 生物数值.起步速度);
        //}

    }

   
    public override void F_ixedUpdate()
    {
        if (假正负0 == -1)
        {
            LocalScaleX_Int = -1;
            transform.localScale = new Vector3(LocalScaleX_Int/* * localScale_X*/, transform.localScale.y, transform.localScale.z);
            P_Action.move(-1, 生物数值.起步速度);
        }
        else if (假正负0 == 1)
        {
            LocalScaleX_Int = 1;
            transform.localScale = new Vector3(LocalScaleX_Int/* * localScale_X*/, transform.localScale.y, transform.localScale.z);
            P_Action.move(1, 生物数值.起步速度);
        }

        P_Action.jump_下落逐渐施加额外重力(生物数值.下落速度判断临界负数值, 生物数值.最大重力, 生物数值.下落过渡速度);



        if (移动开关)
        {
            P_Action.move(Player_input.I.方向正零负, 生物数值.起步速度); 
        }
        else if (速度限制开关)
        {
   Velocity = (new Vector2(0, Velocity.y)) ;
        }
    }
    private void LateUpdate()
    {
    }

    public override void U_pdate()
    {
        if (Player_input.I.按键检测_按下(Player_input.I.上))
        {
            特效_pool.I.GetPool(gameObject,"特效run_");

        }
 


    }
    public float offY = -1.30f;
    public float sizeY = 1.8f;

    public bool 下蹲锁 = true;
    public void 按住下蹲(KeyCode k)
    {
        if (k != Player_input.I.下) return;
        if (!下蹲锁) return;
        if (!Ground) return;

        P_Action.进入碰撞体一半(offY, sizeY);


        下蹲表示 = true;
    }

    public void 松开下蹲(KeyCode k)
    {

        if (k != Player_input.I.下) return;
        if (!头空_ ) return;
        if(冲刺表示) return;
        下蹲恢复到其他状态();
    }
    public void 下蹲恢复到其他状态()
    {

        P_Action.退出碰撞体一半();
        下蹲表示 = false;
    }

    void asdasd()
    {
        //PlayerPrefs.
    }
}

/// <summary>
/// 改变他的刚体
/// </summary>
public class Biology_Actioan
{
    CapsuleCollider2D po;
    Biology b;
    Vector2 Offset { get; set; }
    Vector2 Size { get; set; }


    public Biology_Actioan B_Actioan(Biology bi)
    {
        b = bi;
        po = (CapsuleCollider2D)b.co;
        Offset = po.offset;
        Size = po.size;
  
        return this;
    }
    public void move(int 朝向, float 起步速度)
    {
        Player.I. AddForce(new Vector2(朝向, 0) * 起步速度);
        //b.Addforce(new Vector2(朝向, 0) * 起步速度);
    }
    public void 小跳向下力(float 向下力的大小)
    {
        Player.I. AddForce(Vector2.down * 向下力的大小, ForceMode2D.Force);
 
    }
    public void jump_瞬间向上力(float 瞬间速度)
    {
        Player.I.Velocity = new Vector2(Player.I.Velocity.x, 瞬间速度);
        //b.Set_velocity(new Vector2(b.Get_velocity().x, 瞬间速度)) ;
    }

    public void jump_下落逐渐施加额外重力(float 下落速度判断临界负数值, float 目标, float 插值)
    {
        if (Player.I.Velocity .y >= 下落速度判断临界负数值) return;
        Player.I.GravityScale= Mathf.Lerp(Player.I.GravityScale, 目标, 插值);     
        //b.Set_gravity(Mathf.Lerp(b.Get_gravity(), 目标, 插值)) ;

    }

    public void jump_下落恢复重力(float 常态重力)
    {
        Player.I.GravityScale = 常态重力;
        //b.Set_gravity( 常态重力);
    }



    public void 进入碰撞体一半(float oiffY, float sizeY)
    {


        po.offset = new Vector2(Offset.x, oiffY);
        po.size = new Vector2(Size.x, sizeY);
    }

    public void 退出碰撞体一半()
    {
        po.offset = Offset;
        po.size = Size;
    }



}
