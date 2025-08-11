using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 压缩毛巾 : MonoBehaviour 
{
    [Header("弹性")]
    [SerializeField]
    float 弹性=0;
  
    Animator an=>e.an ; 
     Collider2D FatherB  => (BoxCollider2D)e.co;
     BoxCollider2D Bc;
    One_way O;

    [SerializeField ]
    Enemy_base e;
   float my_Y坐标
    {
        get
        {

            return transform.localPosition.y ;
        }
        set
        {
            transform.localPosition = new Vector3(transform.localPosition.x, value);
        }
    }
    float  an_Y坐标
    {
        get
        {
            return an.transform.localPosition.y;
        }
        set {
 
            an.transform.localPosition = new Vector2(an.transform.localPosition.x,value );
        } 
    }
    float an_Y尺寸
    {
        get
        {
            return an.gameObject.transform.localScale.y;
        }
        set
        {

            an.gameObject.transform.localScale = new Vector2(an.gameObject.transform.localScale.x, value);
        }
    }
    float an_X尺寸
    {
        get
        {
            return an.gameObject.transform.localScale.x;
        }
        set
        { 
            an.gameObject.transform.localScale = new Vector2(value, an.gameObject.transform.localScale.y);
        }
    }


 
    float 压缩倍数 { get =>Initialize_Mono .I .压缩倍数; }

    public float Time_ { get => time_; set
        {
            time_ = Mathf.Clamp(value ,-0.2f,0.2f);
        }
    }

    [SerializeField ]
    [DisplayOnly]
    bool B;


 
    bool 运行过了;

    Vector2 v2差;

  public   bool 只能站一会儿;
    private void Start()
    {
        Bc = GetComponent<BoxCollider2D>();
        O = GetComponent<One_way>();
        O.Enter_Exite += ASD;

        if (运行过了) return;
        运行过了 = true;
        原始Y坐标 = my_Y坐标; 
    }
    float 原始Y坐标;
    private void OnCollisionEnter2D(Collision2D co)
    {
        if (co.gameObject.CompareTag(Initialize.Player))
        {
            for (int i = 0; i < co.contacts.Length; i++)
            {
                if (co.contacts[i].collider.CompareTag(Initialize.Player))
                {
                    bool 碰到的是上面 = Initialize.Vector2Int比较(co.contacts[i].normal, Vector2.down);
                    if (碰到的是上面)
                    {
                        if (弹性==0)
                        { 
                            ASD(true); 
                        } 
                    } 
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D co)
    { 
        if (co.gameObject.CompareTag(Initialize.Player))
        {
            ASD(false);
            //Player3.I.transform.SetParent(Player3.I.Player_Father); 
        }
    }
    private void Awake()
    { 
        v2差 = FatherB.transform.position - transform.parent.transform.position;
    }

    void ASD(bool b)
    {
        if (弹性==0)
        { 
            if (b)
            { 
                Player3.I.ChangeFather(transform );

                if (只能站一会儿)
                {
                    Initialize_Mono.I.Waite(()=> {
                        GetComponent<One_way>()?.关闭一会儿( Initialize_Mono .I. F_Time_踩上去自爆的时间);
                    }
                    ,0.5f*Player3.Public_Const_Speed );
                }
            }
            else
            {
                Player3.I.ChangeFather();
            }
            B = b;
        }
        else
        {
            Player3.I.Velocity = Player3.I.Velocity.Set_Y(弹性);
        }

    }
    
    private void Update()
    {
 
        if (FatherB == null) return;
        transform.parent    .position = FatherB.transform.position - ((Vector3)v2差);

        if (e.I_S.限制)
        {
            Bc.enabled = false;
        }
    }
    [SerializeField ]
    float time_;
    private void FixedUpdate()
    {
        if (e.I_S.限制) return;
        float T = B ? -压缩倍数 : 0;
        cur = Mathf.Lerp(cur ,T,0.25f); 
      float f= Initialize.等体积压缩(an.transform, cur )*2;

        if (f!=0)
        {
            my_Y坐标 = f;
        }
    }
    [SerializeField ]
    float cur;
 
}
