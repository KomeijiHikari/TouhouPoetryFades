using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 速度颜色 : MonoBehaviour
{
    public SpriteRenderer sp;
    public bool 调试;


    public GameObject I_Speed_Change;
    I_Speed_Change i;

    [SerializeField] [DisplayOnly]
    float f;
    [SerializeField] [DisplayOnly]
    Color c;

    [SerializeField] [DisplayOnly]
    bool 激活的;

    bool SP显示 {
        get { return sp.material.GetFloat(材质管理._Alpha) == 0; }
      set  {
            float A = value ? 1 : 0;
            sp.material.SetFloat(材质管理._Alpha, A);
        }
    }
    Fly_Ground ff;
    private void Start()
    {
        gameObject.组件(ref J);
        ff = GetComponent<Fly_Ground>();
        if (ff == null) J.是我 += (bool b) => { 激活的 = b; };
        else 激活的 = true;

        if (I_Speed_Change == null) I_Speed_Change = gameObject;
        if (sp == null) sp = GetComponent<SpriteRenderer>();
        i = I_Speed_Change.GetComponent<I_Speed_Change>();
        //i= GetComponent<I_Speed_Change>();
        if (i == null)
        {
            Debug.LogError("空");
        }
        sp.material = 材质管理.Get_Material(材质管理.Other);


        间隔 = 0.2f;
    }
    [SerializeField]
    [DisableOnPlay]
    float Current_Speed_;

    [SerializeField]
    [DisableOnPlay]
    float Speed_;

    [SerializeField]
    [DisplayOnly]
    E_超速等级 超速等级;
    [SerializeField] [DisplayOnly]
    float 透明度;
    [SerializeField]
    [DisplayOnly]
    float 去色;

    //public float min,   max,curtten;
    [SerializeField ][DisplayOnly ]
    bool 闪烁开关;
    监控激活碰撞框 J;
    public bool 闪烁开关1 { get => 闪烁开关; set {
            if (闪烁开关!=value )
            { 
                闪烁开关  = value;
         
            }
        } }
    float TTime;
    [SerializeField]
    [DisplayOnly]
    float 间隔 = 0.2f;
    private void Update()
    {
        if (!激活的) return;
        边缘颜色更新();
        if (闪烁开关1)
        {
            if (Time.time - TTime> 间隔)
            {
                TTime = Time.time;
                SP显示 = true;

                sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0.8f);
            }
            else
            {
                SP显示 = false;
            } 
        }
    }
    Color No = new Color(1,1,1,0);
    void 边缘颜色更新()
    { 

 
        去色 = 1 - Initialize.ScaleValue(i.固定等级差, 1 / Initialize_Mono.I.阀值3, 1 / Initialize_Mono.I.阀值2);
        透明度 = 1 - Initialize.ScaleValue(i.固定等级差, Initialize_Mono.I.阀值2, Initialize_Mono.I.阀值3);

        if (i.固定等级差 < Initialize_Mono.I.阀值3)
        {
            SP显示 = true;
            闪烁开关1 = false;
            透明度 = 0.2f + (1 - 0.2f) / 1f * 透明度;
            //透明度 = 0.2f + (1 - 0.2f) / 1f * 透明度;
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 透明度);
        }
        else  
        {
            闪烁开关1 = true;
        }


        //比例 = Initialize.ScaleValue(i.Curttent_Speed, Initialize_Mono.I.阀值2, Initialize_Mono.I.阀值3);
        //if (curtten != 0) 比例 = Initialize.ScaleValue(curtten, min, max);
        Speed_ = i.固定等级差;
        超速等级 = i.超速等级;

        Current_Speed_ = i.Curttent_Speed;


        if (i.Curttent_Speed> Initialize_Mono.I.阀值)
        {
            c = Color.red;
        }
        else if (i.Curttent_Speed < 1 / Initialize_Mono.I. 阀值)
        {
            c = Color.blue ;
        }
        else
        {
            c = No;
        }
       
        //f = Initialize.位置Value(Current_Speed_, Initialize_Mono.I.阀值);
        //c = new Color(0, 0, 0, 0);
        //if (f > 0)
        //{
        //    c = new Color(1, 0, 0, 透明度) * f;
        //}
        //else if (f < 0)
        //{
        //    var cc = learp(Color.blue, Color.black , 去色);
        //    c = cc * -f;
        //}

 
            sp.material.SetColor(材质管理._EdgeColor, c); 
   
        sp.material.SetFloat(材质管理._去色, 去色);


    }
    public static Color learp(Color color, Color colorNext, float speed)
    {
        color =
            new Color(
            Mathf.Lerp(color.r, colorNext.r, speed),
            Mathf.Lerp(color.g, colorNext.g, speed),
            Mathf.Lerp(color.b, colorNext.b, speed)
                          );
        return color;
    }
    private void OnDisable()
    {
        sp.material.SetColor(材质管理._EdgeColor, Color.black *0);
        sp.material.SetFloat(材质管理._去色, 0);
    }
}
