using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 判定框Base : MonoBehaviour
{
    public CircleCollider2D Cc;
    public BoxCollider2D bc;
    public PolygonCollider2D pc;
    public bool 打到了 { get; set; } = false;
  public   enum 打到的类型
    {
        NULL,
        地面,
        敌人,
        物体耐久,
        移动平台,
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        打到的类型 打到的类型_ = 打到的类型.NULL;
        bool 地面 = collision.gameObject.CompareTag(Initialize.Ground);
        var 有血 = collision.gameObject.GetComponent<I_生命>();
        var 敌人 = collision.gameObject.GetComponent<BiologyBase>();
        I_Speed_Change 变速 = null;
        if (Player3.I.N_.时缓) 变速 = collision.gameObject.GetComponent<I_Speed_Change>();
        打到了 = true;

        ADDCo(collision);
         
        if (有血 != null)
        {
            打到的类型_ = 打到的类型.物体耐久;
        }
        if (变速 != null)
        {
            打到的类型_ = 打到的类型.移动平台;
        }
        if (敌人 != null)
        {
            打到的类型_ = 打到的类型.敌人;
        }
        switch (打到的类型_)
        {
            case 打到的类型.敌人:
                if (变速 != null)
                {
                    if (FSM.f.变速攻击)
                    {
                        Debug.LogWarning("进入CCCCCCCCCCCCCCCCC");
                        Player3.I.同速_(变速);
                    }
                }
                ADD(敌人);
                break;
            case 打到的类型.物体耐久:
                有血.被扣血(Player3.I.atkvalue, Player3.I.gameObject, 0);
                break;
            case 打到的类型.移动平台:
                if (FSM.f.变速攻击)
                {
                    Player3.I.同速_(变速);
                }
                break;
            case 打到的类型.地面:
                break;
        }
    }
    void ADDCo(Collider2D a)
    {
        Debug.LogError(所有碰撞体.Contains(a)+"      "+a.gameObject.name );
        if (!所有碰撞体.Contains(a))
        { 
            所有碰撞体.Add(a);
        }
    }
    void ADD(BiologyBase  a)
    {
        if (!敌人.Contains(a))
        {
            敌人.Add(a);
        }
    }
    public List<Collider2D> 所有碰撞体;
  public    List<BiologyBase> 敌人 { get;    set; } = new List<BiologyBase>();
    public List<BiologyBase> 击中的生命体()
    {
        return null;
    }

 

     protected SpriteRenderer sp { get; set; }
    Animator a;

    GameObject Pa;
    SpriteRenderer Fathersp { get; set; }
    private void Start()
    {
        Pa = transform.parent.gameObject ;
        Fathersp =Player3 .I. sp;
        bc.enabled = false;
        sp.sortingLayerID = Fathersp.sortingLayerID;
        sp.sortingOrder = Fathersp.sortingOrder-1;
    }
    protected virtual void Awake()
    {
        pc = GetComponent<PolygonCollider2D>();
              bc = GetComponent<BoxCollider2D>();
        sp = GetComponent<SpriteRenderer>();
        a = GetComponent<Animator>();
        bc.isTrigger = true;
    }
    public Animator GetAnimator()
    {
        return a;
    }

    Coroutine 开启;
    public void SetBox(Vector2 of,Vector2 size)
    { 
 
        bc.offset = of;
        bc.size = size;
    }
    public void 关闭再打开()
    {
        开启判定框判定框(false);
        if (开启 == null) return;
        StopCoroutine(开启);
    }
    public void 开启一会儿(float time,Behaviour B = null)
    { 
        StartCoroutine(IE_ATK(time,B));
    }

    public void 开启一会儿(float time, Vector2 Offse, Vector2 Size)
    { 
 
        开启 = StartCoroutine(IE_ATK(time, Offse, Size));
    }
    protected IEnumerator IE_ATK(float time, Vector2 Offse, Vector2 Size)
    { 
        bc.offset = Offse;
        bc.size = Size;
        开启判定框判定框(true);
        yield return 0;
        //yield return new WaitForSeconds(time);
        开启判定框判定框(false);
    }
    protected IEnumerator IE_ATK(float time, Behaviour B = null)
    { 
        开启判定框判定框(true,B);
#if UNITY_EDITOR
        Initialize_Mono.残留(bc);
#endif      
        yield return 0;
        //yield return new WaitForSeconds(time);
        开启判定框判定框(false, B);
    }
    public virtual void 在主角前面( )
    {
        sp.sortingOrder = Fathersp.sortingOrder + 1;
    }

 //   bool 开启副;
 //public void   开启副kkk()
 //   {
 //        开启副 = true;
 //   }

    public virtual void 开启判定框判定框(bool b,Behaviour  B=null)
    {
 
        if (B == null) B = bc;
 
        B.enabled = b;
        //副碰撞框切换至(b);
        if (!b)
        {//恢复 
            打到了 = false;
            所有碰撞体.Clear();
            敌人.Clear(); 
            sp.sortingOrder = Fathersp.sortingOrder - 1;
            transform.localPosition= Vector2.zero;
        }
        //sp.enabled = b;
    }

}

