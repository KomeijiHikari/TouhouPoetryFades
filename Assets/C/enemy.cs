using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


[Serializable]
public class 点
{

    [SerializeField]
    private NB方法 行为 = new NB方法();
    [SerializeField]
    public GameObject obj;
    public 点()
    {

    }
    public 点(GameObject obj_, NB方法 nB)
    {
        行为 = nB;
        obj = obj_;
    }
    public 点(GameObject obj_)
    {
        obj = obj_;
    }
    public void Invoke()
    {
        行为?.Invoke();
    }

}
public class enemy : BiologyBase, I_攻击,I_生命
{
    [Reload("Textures/2D/Sparkle.png")]
    [DisplayOnly]
    [SerializeField] Sprite m_LightCookieSprite;
 
    protected override bool 灵魂
    {
        get
        { 
            return 灵魂1;
        }
        set
        { 
            灵魂1 = value;
        }
    }

    [NonSerialized]
  public   CapsuleCollider2D cap;
    public 路径列表 moving;
    [NonSerialized]
    public 戒备 戒备;

    public Vector2 速度监控;
    public 功能数值Base 数值;
    public bool 跳跃开关;

    public EnemyCollider 碰撞;

    public Action<E_enemyState> 时间结束;

    public Enemy_atk A_;
    Rigidbody2D rbb;



    [DisplayOnly]
    [SerializeField]
    public GameObject 攻击目标_;
    public GameObject 攻击目标 { get;set; }

    
   private void Update()
    { 
       A_ .enabled =灵魂;
        戒备.enabled = 灵魂;
        if (碰撞.碰到)
        {
            碰撞.碰到 = false;

            Player3.I.被扣血(10,gameObject, 0);
        }
    }
    new private void Awake()
    {

        base.Awake();
        sp = GetComponent<SpriteRenderer>();
        A_ = GetComponent<Enemy_atk>();
        戒备 = GetComponent<戒备>();
        cap = GetComponent<CapsuleCollider2D>();
        rbb = GetComponent<Rigidbody2D>();
        moving = GetComponent<路径列表>();
        碰撞=GetComponentInChildren<EnemyCollider>();

        GravityScale =(5f);

        if (hpMax_==0)
        {
            hpMax_ = 100;
        }
        当前hp = hpMax_;
        if (atkvalue==0)
        {
            atkvalue = 10;
        }
    }

    public void text(string  a)
    {
        Debug.LogError(a);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //collision.gameObject.GetComponent<I_生命>()?.被扣血(atkvalue,this.gameObject );
    }

    

    public void 开始计时(E_enemyState e_, float time)
    {
        StartCoroutine(asdasdadad(e_, time));
    }
    IEnumerator asdasdadad(E_enemyState e_, float time)
    { 
        yield return new WaitForSeconds(time);

        时间结束?.Invoke(e_);

    }


    void FixedUpdate()
    {
 
        攻击目标 = 戒备.返回一个玩家;
    }

    public void 追击()
    {
 
        if (攻击目标 == null) return;
        向目标水平移动(攻击目标);
    }
    public  override  void    向目标水平移动(GameObject target)
    {
 
        if (Initialize.方向_A是否在B的左边或者下面(this.gameObject, target, false))
        {
            Velocity=(new Vector2(1 * 数值.常态速度, 0));
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            Velocity=(new Vector2(-1 * 数值.常态速度, 0));
            transform.localScale = new Vector2(-1, 1);
        }

    }
    public  override   Action 生命归零 { get; set; }
    public   float 当前hp_;
    public override float 当前hp { get => 当前hp_; set => 当前hp_ = value; }
    public float hpMax_;
    public override float hpMax { get => hpMax_; set => hpMax_ = value; }
    public float atkvalue_;
    public override float atkvalue { get => atkvalue_; set => atkvalue_ = value; }
    public override bool HPROCK { get ; set  ; }
    public override Action 被打 { get  ; set  ; }

    public override void 扣攻击(float i)
    {

    }

    public override   void 被扣血(float i, GameObject obj, int SKey)
    {
        if (DEAD) return; 
        被打?.Invoke();

        特效_pool_2.I.GetPool(gameObject , "特效受击");
        当前hp -= i;

        sp.闪光(0.05f);
 
        if (当前hp<=0)
        {
            DEAD = true;
            Velocity=(Vector2 .zero);
            生命归零 += asdasdad; 
            生命归零.Invoke();
        }

    }
 
    bool DEAD;
    private void asdasdad()
    {

           生命归零 -= asdasdad;
        an.Play("dead");

        特效_pool_2.I.GetPool (gameObject ,"dead");
        gameObject.SetActive(false);
        //var a = an.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        //var b = an.GetCurrentAnimatorStateInfo(0).length;
        //Debug.LogError(b+"    "+a);
        //StartCoroutine(凌迟(a));
    }

    //IEnumerator  凌迟(float t)
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    gameObject.SetActive(false);
    //}


    public void 扣最大上限(float i)
    {
    }

    public override void 交互()
    { 
    }
 

    public override void 跳跃()
    { 
    }
}
