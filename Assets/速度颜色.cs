using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class 速度颜色 : MonoBehaviour 
{
    public SpriteRenderer sp;
    public bool 调试;



    监控激活碰撞框 J;

    public GameObject I_Speed_Change;
    I_Speed_Is Is;

    [SerializeField]
    [DisplayOnly]
    float f;
    [SerializeField]
    [DisplayOnly]
    Color c;

    [SerializeField]
    [DisplayOnly]
    bool 激活的;

    bool SP显示
    {
        get { return sp.material.GetFloat(材质管理._Alpha) == 0; }
        set
        {
            float A = value ? 1 : 0;
            sp.material.SetFloat(材质管理._Alpha, A);
        }
    }

    public bool 暂停 { get   ; set  ; }

    Fly_Ground ff; 
    private void Start()
    {
        //return;
        gameObject.组件(ref J);
        ff = GetComponent<Fly_Ground>();
        if (ff == null) J.是我 += (bool b) => { 激活的 = b; };
        else 激活的 = true;

        if (I_Speed_Change == null) I_Speed_Change = gameObject;
        if (sp == null) sp = GetComponent<SpriteRenderer>();
        Is = I_Speed_Change.GetComponent<I_Speed_Is>();
     
        //i= GetComponent<I_Speed_Change>();
        if (Is == null)
        {
            Debug.LogError("空");
        }
        //sp.material = 材质管理.Get_Material(材质管理.Other); 
        M = sp.sharedMaterial;

        Sb = GameObject.Instantiate(Surp_Pool.Get_Gameobject(Surp_Pool.三角)).GetComponent<速度标识>();
        假Speed = I_Speed_Change.GetComponent<假装是I_Speed_is>() != null;
        Sb.transform.position = transform.position;
        //Sb.transform.SetParent(transform);
    }
    [DisplayOnly]
    [SerializeField]
    速度标识 Sb;
    Material M;
    E_超速等级 E;
  bool BB;

    Color 透明 = new Color(1, 1, 1, 0);

    public bool Deb;
    bool 假Speed;
    private void Update()
    {
        if (Is == null)
        {
            Debug.LogError(gameObject.name + transform.position + "空 速度接口");
            return;
        }
        Sb.gameObject.SetActive(BB);
        //E = Initialize.Speed_toESpeed(Is.固定等级差);
        if (BB != 切换Shader.I.isSpeed)
        {

            BB = 切换Shader.I.isSpeed;
            if (!BB)
            {
                ///还原
                sp.color = Color.white;
                sp.material = M;
            }
        }

        if (BB)
        {
            float cc = 0;
            //if(假Speed)
            //{
            //    //if(Is.固定等级差._is)

            //    if (Is.固定等级差>1)
            //    {
            //        cc = 0.00001f;
            //    }
            //    else
            //    {
            //        cc = -0.00001f;
            //    }

            //}
            SpeedInt = Initialize_Mono.I.GetSpeedInt(Is.固定等级差+cc);
 
            if (SpeedInt == Initialize_Mono.BugInt)
            {
                if (Is.固定等级差>1)
                {
                    sp.color = Color.red;
                }
                else  if(Is.固定等级差 < 1)
                {
                    sp.color = Color.blue;
                }
                sp.material.SetColor(材质管理._EdgeColor, 透明);
              if(Deb)  Debug.LogError("超过最大或者最小");
                刷新();
                return;
            }
            if (SpeedInt == 0)
            {
                if (Is.固定等级差>1)
                {
                    if (Deb)    Debug.LogError("         if (Is.固定等级差>1)         if (Is.固定等级差>1)");
       
                    sp.material.SetColor(材质管理._EdgeColor, Color.red);
                }
                else if(Is.固定等级差<1)
                {
                    if (Deb) Debug.LogError("            else if(Is.固定等级差<1)             else if(Is.固定等级差<1))");
                    sp.material.SetColor(材质管理._EdgeColor, Color.blue);
                } 
                //else
                //{
                //    sp.material.SetColor(材质管理._EdgeColor,new Color(1,1,1,0));
                //}
                sp.color = Color.white;
          
            
            }

            else if(SpeedInt <= -1)
            {
                sp.material.SetColor(材质管理._EdgeColor, 透明);
                sp.color = Color.blue;
            }
            else if (SpeedInt >=  1)
            {
                sp.material.SetColor(材质管理._EdgeColor, 透明);
                sp.color =Color.red;
            }

        } 
        刷新();
    }
    private void OnDisable()
    {
        if (Sb!=null)
        {
            Sb.gameObject.SetActive(false);
        }
    }
    void 刷新()
    {
        Sb.gameObject.SetActive(BB);
 
        Sb.刷新();
        Sb.transform.position = transform.position;
        Sb.SpeedInt = SpeedInt;
        Speed差 = Is.固定等级差;
    }
    float Speed差;
    [SerializeField]
    [DisplayOnly]
    int SpeedInt;
}

//public class 速度颜色 : MonoBehaviour
//{
//    public SpriteRenderer sp;
//    public bool 调试;


//    public GameObject I_Speed_Change;
//    I_Speed_Change i;

//    [SerializeField] [DisplayOnly]
//    float f;
//    [SerializeField] [DisplayOnly]
//    Color c;

//    [SerializeField] [DisplayOnly]
//    bool 激活的;

//    bool SP显示 {
//        get { return sp.material.GetFloat(材质管理._Alpha) == 0; }
//      set  {
//            float A = value ? 1 : 0;
//            sp.material.SetFloat(材质管理._Alpha, A);
//        }
//    }
//    Fly_Ground ff;

//    速度颜色2 SS;
//    private void Awake()
//    {
//        if (sp == null) sp = GetComponent<SpriteRenderer>();
//        gameObject.组件<速度颜色2>(ref SS);
//        SS.sp= sp;
//        Debug.LogError(SS.sp);
//    }
//    private void Start()
//    {
//        return;
//        gameObject.组件(ref J);
//        ff = GetComponent<Fly_Ground>();
//        if (ff == null) J.是我 += (bool b) => { 激活的 = b; };
//        else 激活的 = true;

//        if (I_Speed_Change == null) I_Speed_Change = gameObject;
//        if (sp == null) sp = GetComponent<SpriteRenderer>();
//        i = I_Speed_Change.GetComponent<I_Speed_Change>();
//        //i= GetComponent<I_Speed_Change>();
//        if (i == null)
//        {
//            Debug.LogError("空");
//        }
//        sp.material = 材质管理.Get_Material(材质管理.Other);


//        间隔 = 0.2f;
//    }
//    [SerializeField]
//    [DisableOnPlay]
//    float Current_Speed_;

//    [SerializeField]
//    [DisableOnPlay]
//    float Speed_;

//    [SerializeField]
//    [DisplayOnly]
//    E_超速等级 超速等级;
//    [SerializeField] [DisplayOnly]
//    float 透明度;
//    [SerializeField]
//    [DisplayOnly]
//    float 去色;

//    //public float min,   max,curtten;
//    [SerializeField ][DisplayOnly ]
//    bool 闪烁开关;
//    监控激活碰撞框 J;
//    public bool 闪烁开关1 { get => 闪烁开关; set {
//            if (闪烁开关!=value )
//            { 
//                闪烁开关  = value;

//            }
//        } }
//    float TTime;
//    [SerializeField]
//    [DisplayOnly]
//    float 间隔 = 0.2f;
//    private void Update()
//    {
//        return;
//        if (!激活的) return;
//        边缘颜色更新();
//        if (闪烁开关1)
//        {
//            if (Time.time - TTime> 间隔)
//            {
//                TTime = Time.time;
//                SP显示 = true;

//                sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0.8f);
//            }
//            else
//            {
//                SP显示 = false;
//            } 
//        }
//    }
//    Color No = new Color(1,1,1,0);
//    void 边缘颜色更新()
//    { 


//        去色 = 1 - Initialize.ScaleValue(i.固定等级差, 1 / Initialize_Mono.I.阀值3, 1 / Initialize_Mono.I.阀值2);
//        透明度 = 1 - Initialize.ScaleValue(i.固定等级差, Initialize_Mono.I.阀值2, Initialize_Mono.I.阀值3);

//        if (i.固定等级差 < Initialize_Mono.I.阀值3)
//        {
//            SP显示 = true;
//            闪烁开关1 = false;
//            透明度 = 0.2f + (1 - 0.2f) / 1f * 透明度;
//            //透明度 = 0.2f + (1 - 0.2f) / 1f * 透明度;
//            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 透明度);
//        }
//        else  
//        {
//            闪烁开关1 = true;
//        }


//        //比例 = Initialize.ScaleValue(i.Curttent_Speed, Initialize_Mono.I.阀值2, Initialize_Mono.I.阀值3);
//        //if (curtten != 0) 比例 = Initialize.ScaleValue(curtten, min, max);
//        Speed_ = i.固定等级差;
//        超速等级 = i.超速等级;

//        Current_Speed_ = i.Curttent_Speed;


//        if (i.Curttent_Speed> Initialize_Mono.I.阀值)
//        {
//            c = Color.red;
//        }
//        else if (i.Curttent_Speed < 1 / Initialize_Mono.I. 阀值)
//        {
//            c = Color.blue ;
//        }
//        else
//        {
//            c = No;
//        }

//        //f = Initialize.位置Value(Current_Speed_, Initialize_Mono.I.阀值);
//        //c = new Color(0, 0, 0, 0);
//        //if (f > 0)
//        //{
//        //    c = new Color(1, 0, 0, 透明度) * f;
//        //}
//        //else if (f < 0)
//        //{
//        //    var cc = learp(Color.blue, Color.black , 去色);
//        //    c = cc * -f;
//        //}


//            sp.material.SetColor(材质管理._EdgeColor, c); 

//        sp.material.SetFloat(材质管理._去色, 去色);


//    }
//    public static Color learp(Color color, Color colorNext, float speed)
//    {
//        color =
//            new Color(
//            Mathf.Lerp(color.r, colorNext.r, speed),
//            Mathf.Lerp(color.g, colorNext.g, speed),
//            Mathf.Lerp(color.b, colorNext.b, speed)
//                          );
//        return color;
//    }
//    private void OnDisable()
//    {
//        if (gameObject.activeInHierarchy)
//        {

//            sp.material.SetColor(材质管理._EdgeColor, Color.black * 0);
//            sp.material.SetFloat(材质管理._去色, 0);
//        }
//    }
//}
