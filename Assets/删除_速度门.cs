using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineFreeLook;

class Momomo
{
    public BoxCollider2D bc;
    public SpriteRenderer sp;
    public Transform ts;
    public Momomo(SpriteRenderer sp)
    {

        this.sp = sp; 
        bc = this.sp.gameObject.GetComponent<BoxCollider2D>();
        ts = this.sp.gameObject.GetComponent<Transform>();
    }
}

/// <summary>
/// 该脚本只设置 sp尺寸
/// </summary>
public class 删除_速度门 : MonoBehaviour, I_Speed_Change, I_暂停
{
    [SerializeField]
    Phy_检测 P;
    Momomo A;
    Momomo B;
    [SerializeField]
    SpriteRenderer SA;
    [SerializeField]
    SpriteRenderer SB;

    BoxCollider2D bc;
    SpriteRenderer sp;
    public E_移动方式 E_方式;
    [SerializeField] Transform OrdinA;
    [SerializeField] Transform OrdinB;

    float MaxSize;
    float 间隔
    {
        get
        {
            if (E_方式==E_移动方式.水平)
            {
            return 3.5f;
            }
                return 4;
        }
    }

    [SerializeField]
    float NozeA;
    [SerializeField]
    float NozeB;

    [SerializeField]
    private float speed_Lv = 1;
    [SerializeField]
    private bool 暂停1;
    MonoMager m;
    public float Speed_Lv { get => speed_Lv; set => speed_Lv = value; }
    public bool 暂停 { get => 暂停1; set => 暂停1 = value; }

    void 初始化(Momomo Move, bool X = true)
    {
        Move.sp.drawMode = SpriteDrawMode.Tiled;
        if (X)
        {
            Move.sp.size = new Vector2(sp.size.x, 0);
            Move.bc.size = new Vector2(sp.size.x, 0.001f);
        }
        else
        {
            Move.sp.size = new Vector2(0, sp.size.y);
            Move.bc.size = new Vector2(0.001f, sp.size.y);
        }
        Move.ts.localScale = Vector2.one;  ///size就算是一  变成Tiled之后尺寸还是会变 
    }
    private void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
        sp = GetComponent<SpriteRenderer>();

        A = new Momomo(SA);
        B = new Momomo(SB);
        gameObject.组件<监控激活碰撞框>(ref J);
        gameObject.组件<MonoMager>(ref m);
        //J.是我 += (bool b) => {
        //    Re();
        //};
        sp.enabled = false;

        P.Enter += () => {
            Re();
            SpeedMager.I.临时速度清除();
            Player3.I.适应文字.开关(true);
            if (true)
            {
                Player3.I.适应文字.SetText("速度一致的话就找准空隙滑铲过去吧");
            }
       
        };
        P.Exite += () => {
            Player3.I.适应文字.开关(false);
        };

    }
    void SHuaxin()
    {
        ///玩家刷新
        ///刷新
        ///速度等级Int一致
        ///刷新
        /// 
        ///玩家速度不一致   自己高，如果慢一些那就速度更高
        ///自己慢，那就静止
    }

    监控激活碰撞框 J;
    private void Start()
    {
        SpeedMager.I.Public_Speed_ += () => {
             
            Initialize_Mono.I.Waite(() => {
                if (我比玩家快 || 我比玩家慢) Re();
            });

        };


 
            switch (E_方式)
        {
            case E_移动方式.竖直:
                MaxSize = sp.size.y;
                OrdinA.position = sp.bounds.九个点(E_方向.下);
                OrdinB.position = sp.bounds.九个点(E_方向.上);
                A.ts.localPosition = Vector2.zero;
                B.ts.localPosition = Vector2.zero;
                初始化(A);
                初始化(B);
                //bc.size = new Vector2(bc.size.x, sp.size.y); //heyiwei
                bc.size = new Vector2(sp.size.x+0.08f, sp.size.y);
                break;
            case E_移动方式.水平:
                MaxSize = sp.size.x;
                OrdinA.position = sp.bounds.九个点(E_方向.左);
                OrdinB.position = sp.bounds.九个点(E_方向.右);
                A.ts.localPosition = Vector2.zero;
                B.ts.localPosition = Vector2.zero;
                初始化(A, false);
                初始化(B, false);

                //bc.size = new Vector2(sp.size.x, bc.size.y);  hyw?
                bc.size = new Vector2(sp.size.x, sp.size.y + 0.08f);
                break;
            case E_移动方式.自由:
                break;
            default:
                break;
        }

        Re();
    }

    void Re()
    {
        NozeB = 间隔;
        NozeA = MaxSize;

        setSize(B, NozeB);
        setSize(A, NozeA);
    }

    public GameObject 对象 => gameObject;

    public Action 变速触发 { get; set; }

    public I_Speed_Change I_S => this;

    public float Current_Speed_LV => Speed_Lv;

    bool 我比玩家慢;
    bool 我比玩家快;

    public bool Deb;
    private void Update()
    {
        if (暂停) return;


        var 速度差 = I_S.固定等级差;
        var fu = 1 / Initialize_Mono.I.阀值;
        我比玩家慢 = 速度差 < fu || 速度差._is(fu);
        if (Deb) Debu.LogError(速度差+ "  慢  " + fu+ 速度差._is(fu));

        我比玩家快 = 速度差 > Initialize_Mono.I.阀值 || 速度差._is(Initialize_Mono.I.阀值);
        if (Deb) Debu.LogError(速度差 + " 快   " + Initialize_Mono.I.阀值 + 速度差._is(Initialize_Mono.I.阀值));

        if (我比玩家快 && 速度差 < Initialize_Mono.I.阀值2)
        { 
            速度差 = Initialize_Mono.I.阀值2  ;///高速加快 
        }


        if (我比玩家快|| 我比玩家慢)
        {
            bc.isTrigger = false;
            gameObject.layer= Initialize.L_M_Ground;
        }
        else
        {
            bc.isTrigger = true;
            gameObject.layer = Initialize.L_Enemy;
        }


        if (我比玩家慢)
        { 
            return; ///慢速静止
        }
        NozeB -= Time.deltaTime * 速度差;
        NozeA -= Time.deltaTime * 速度差;

        if (NozeA < -间隔) Re();

        setSize(B, NozeB);
        setSize(A, NozeA);
    }
    void setSize(Momomo mo, float f)
    {
        switch (E_方式)
        {
            case E_移动方式.竖直:
                mo.sp.size = new Vector2(mo.sp.size.x, f);
                mo.ts.localPosition = new Vector2(0, f / 2);

                f = Mathf.Abs(f);
                if (f < 0.0001) f = 0.0001f;
                mo.bc.size = new Vector2(mo.bc.size.x, f);

                break;
            case E_移动方式.水平:
                mo.sp.size = new Vector2(f, mo.sp.size.y);
                mo.ts.localPosition = new Vector2(f / 2, 0);

                f = Mathf.Abs(f);
                if (f < 0.0001) f = 0.0001f;
                mo.bc.size = new Vector2(f, mo.bc.size.y);


                break;
            case E_移动方式.自由:
                break;
        }

    }
}
