using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using 流程控制;

/// <summary>
/// 关键在于  "正常的猪"  是显示的还是不显示的
/// 
/// 速度1 显示 那么锁钥结构 该锁是比要是快一个层才会  隐藏
/// /// 速度1 不显示 那么锁钥结构 该锁同层就隐藏  显示就要慢一个层
/// </summary>
public class Pig : MonoBehaviour,I_Speed_Is
{
    [SerializeField]
    private float speed_Lv=1;

    [SerializeField]
    float 厚度=1;
    [SerializeField]
    BoxCollider2D bc;

    [SerializeField]
    SpriteRenderer sp;

    Vector2 start;
    void asd()
    {

        transform.localScale =  new Vector2(Mathf.Abs(transform.localScale.x) , Mathf.Abs(transform.localScale.y));
        float X=厚度;
        X = Mathf.Min(transform.localScale.x);
        X = Mathf.Min(transform.localScale.y);
        if (厚度!=X)
        {
            厚度 *= 0.1f;
        }

    }
    private void Awake()
    {
        asd();


        sp.drawMode = SpriteDrawMode.Sliced;
        sp.size = transform.localScale;
        bc.size = sp.size;
        transform.localScale = Vector2.one;
        start = sp.size;
    }

    private void Start()
    {
        v =new Vector2(sp.bounds.size.x- 厚度, sp.bounds.size.y- 厚度) ;
        //vb = new Vector2(  v.x / sp.bounds.size.x,   v.y / sp.bounds.size.y);  
 
    }
    float ch;
    float t;
    Vector2 v;
    Vector2 vb;
    [SerializeField]
    bool 缩小;
    [SerializeField] float 频率=3;

    [SerializeField]
    E_超速等级 e_;

    Color 半透明 = new Color(1, 1, 1, 0.1f);
    void 开关(bool b)
    { 
        开关_ = b;
            bc.enabled = b;
         if (b)
        {
            sp.color = Color.white;
        }
        else
        {
            sp.color = 半透明;

        }
    }
    bool 开关_;
    private void Update()
    {
        e_ = Initialize.Speed_toESpeed(I.固定等级差);
        switch (e_)
        {

            case E_超速等级.静止:
            case E_超速等级.低速:
       
                开关(true);
                break;
            case E_超速等级.正常:
            case E_超速等级.超速:
            case E_超速等级.半虚化:
            case E_超速等级.虚化:
            case E_超速等级.虚无:

                开关(false);
                break;
        }
    }
    public float Curttent_;
    public I_Speed_Is I=>this;
    private void FixedUpdate()
    {
        if (!开关_) return;
        t+= Time.fixedDeltaTime* I.固定等级差;


        if (厚度 == 0) return;
        if (t>频率)
        {
            t = 0;
            缩小 = !缩小;
        }
        ch = t / 频率;
        if (缩小)
        {
 
            float x = Mathf.Lerp(start.x, v.x,1- ch);
            float y = Mathf.Lerp(start.y, v.y, 1 - ch);
            bc.size = new Vector2(x, y);
            sp.size = bc.size; 
        }
        else
        {
            bc.size = start;
        }
         
        //bc.size = vb;
    }
    public GameObject 对象 => gameObject;

    public Action 变速触发 { get  ; set  ; }
     

    public float Current_Speed_LV => Speed_Lv;

    public float Speed_Lv { get => speed_Lv; set => speed_Lv = value; }
}


//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Pig : MonoBehaviour, I_Speed_Change
//{
//    [SerializeField]
//    private float speed_Lv = 1;

//    [SerializeField]
//    float 厚度 = 1;
//    [SerializeField]
//    BoxCollider2D bc;

//    [SerializeField]
//    SpriteRenderer sp;
//    private void Start()
//    {
//        v = new Vector2(transform.localScale.x - 厚度, transform.localScale.y - 厚度);
//        vb = new Vector2(v.x / transform.localScale.x, v.y / transform.localScale.y);

//    }
//    float ch;
//    float t;
//    Vector2 v;
//    Vector2 vb;
//    [SerializeField]
//    bool 缩小;
//    [SerializeField] float 频率 = 3;

//    private void FixedUpdate()
//    {
//        t += Time.fixedDeltaTime * I_S.Curttent_Speed;

//        if (t > 频率)
//        {
//            t = 0;
//            缩小 = !缩小;
//        }
//        ch = t / 频率;
//        if (缩小)
//        {

//            float x = Mathf.Lerp(1, vb.x, 1 - ch);
//            float y = Mathf.Lerp(1, vb.y, 1 - ch);
//            bc.size = new Vector2(x, y);
//            Debug.Log(bc.size);
//        }
//        else
//        {
//            bc.size = Vector2.one;
//        }


//        //bc.size = vb;
//    }
//    public GameObject 对象 => gameObject;

//    public Action 变速触发 { get; set; }

//    public I_Speed_Change I_S => this;

//    public float Current_Speed_LV => Speed_Lv;

//    public float Speed_Lv { get => speed_Lv; set => speed_Lv = value; }
//}
