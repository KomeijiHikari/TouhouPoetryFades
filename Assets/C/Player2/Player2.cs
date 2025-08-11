using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System;



public class Player2 :MonoBehaviour
{
 public    Animator an;
  public   AniContr_4 _4;
    public   DASH2 普通dash;
    public DASH2 滑铲;
    public DASH2 skydash;

 void ASAD(DASH2  dASH)
    {
        StartCoroutine(开起来(dASH));
        dASH.恢复 -= ASAD;
    }
    IEnumerator 开起来(DASH2 dASH)
    {
        yield return new WaitForSeconds(dASH.冲刺冷却时间);
        dASH.冷却好了 = true;
    }
    public DASH2 返回DASH2(int i)
    {
        switch (i)
        {
            case -1:
                滑铲.恢复 += ASAD;
                return 滑铲;
            case 0:
                普通dash.恢复 += ASAD;
                return 普通dash;
            case 1:
                skydash.恢复 += ASAD;
                return skydash;
        }
        Debug.LogError("I不是三个值进来的I是:        "+i);
        return null;
    }


    [DisplayOnly]
    public Vector2 监控;
    public 功能数值Base 主角数值;

    public Rigidbody2D rb;
    public CapsuleCollider2D CC;

    public static string 按下 { get; } = "按下";
    public static string 松开 { get; } = "松开";
    public static string 按住 { get; } = "按住";
    public static string 方向 { get; } = "方向";
    
    private void Awake()
    { 
        _4 = GetComponent<AniContr_4>();
        an = GetComponent<Animator>();
        //朝向 = 1;
        CC= GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        Player_input.I.KeyDown += 按下_;
        Player_input.I.KeyUp += 松开_;
        Player_input.I.KeyState += 按住_;
        Player_input.I.方向变动 += 方向_;

        Offset_ = CC.offset;
        Size_ = CC.size;
    }



    private void 按下_(KeyCode obj)
    {
        CustomEvent.Trigger(gameObject, 按下, obj);
        if (obj== Player_input.I.跳跃)
        {
            CustomEvent.Trigger(gameObject, "按下跳跃");
        }
    }
    private void 松开_(KeyCode obj)
    {
        CustomEvent.Trigger(gameObject, 松开, obj);
    }
    private void 按住_(KeyCode obj)
    {
        CustomEvent.Trigger(gameObject, 按住, obj);

        if (obj == Player_input.I.左 || obj == Player_input.I.右)
        {
            CustomEvent.Trigger(gameObject, "按住方向");
        }
    }
    private void 方向_(int obj)
    {
        CustomEvent.Trigger(gameObject, "方向改变");
        rb.velocity = new Vector2(0, rb.velocity.y);
        ASDASD = 0;
    }
    [DisplayOnly ]
    [SerializeField]
   public  float ASDASD;
    public float 距离=0.5f;
    private void FixedUpdate()
    {
        ASDASD += Time.fixedDeltaTime;
    }
    private void Update()
    {
        监控 = rb.velocity;

        前后和头(距离); 
    }
    public void 加力()
    {
        rb.AddForce( new Vector2(  Player_input.I.方向正零负* 主角数值.起步速度,0));
    }
    public void 地面水平速度限制if_uppdate() 
    {
        if (Math.Abs(rb.velocity.x) >= 主角数值.常态速度)
        {
            rb.velocity = new Vector2(主角数值.常态速度 * Player_input.I.方向正零负, rb.velocity.y);
        }
    }
    public void 朝向update()
    {
        transform.localScale = new Vector2(Player_input.I.方向正负, transform.localScale.y);
    }

    public void 减速()
    {

    }
Vector2 Offset_;
   Vector2 Size_;
    public float  OffsetY;
    public float  SizeY;
    public void 进入碰撞体一半()
    {


        CC.offset = new Vector2(CC.offset.x, OffsetY);
        CC.size = new Vector2(CC.size.x, SizeY);
    }

    public void 退出碰撞体一半()
    {

        CC.offset = Offset_;
        CC.size = Size_;
    }




public void 跳跃()
    {
        if (主角数值.跳跃剩余跃次数 >= 1)
        { 
            rb.velocity = new Vector2(rb.velocity.x, 主角数值.跳跃瞬间速度);
        }


            主角数值.跳跃剩余跃次数-=1;


    }


    [DisplayOnly]
    [SerializeField]
 bool Ground_=true;
    public bool Ground
    {
        get
        {
            return Ground_;
        }
        set
        {


            if ((Ground_ == false) && value == true)
            {
                    接触地面();
                    Ground_ = value;
            }
            if ((Ground_ == true) && value == false)
            {
                离开地面();
                Ground_ = value;
            }


        }

    }

    private void 离开地面()
    {
        CustomEvent.Trigger(gameObject, "离开地面");
    }

    public float 跳跃欲输入时间 = 0.1f;
    private void 接触地面()
    {
        主角数值.跳跃剩余跃次数 = 主角数值.最大跳跃次数;
        CustomEvent.Trigger(gameObject, "接触地面");
        if (Player_input.I.D_I[Player_input.I.跳跃].down_State < 0.1)
        {
            CustomEvent.Trigger(gameObject, "按下跳跃");
        }
    }

    public bool 判断水平有输入()
    {
        return Player_input.I.方向正零负 != 0;
    }
    void  前后和头(float 距离)
    {
        var DI =
           Physics2D.BoxCast(
new Vector2(CC.bounds.center.x, CC.bounds.min.y),
 new Vector2(CC.bounds.size.x - 0.5f, 0.1f),
 0f,
 Vector2.down,
  0.2f,
1 << LayerMask.NameToLayer("Ground")
 )
 .collider;
        if (DI != null)
        {
            Ground = true;
            if (DI.GetComponent<Move_Ground>() != null)
            {
                float ca = Initialize.获取两碰撞体最近方向的插值(this.gameObject, DI.gameObject);
                transform.position = new Vector2(transform.position.x, transform.position.y - ca);
            }
        }
        else
        {
            if (rb.velocity.y != 0)
            {
                Ground = false;
            }

        }
    }


}
