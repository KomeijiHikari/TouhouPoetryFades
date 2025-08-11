using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 

public class UI_HPMP : MonoBehaviour
{
    public RectTransform rt;
    //public Text text;
    public Image 上面;
    public Image 下面;

    Image 这里;
     


      float   比例=  3;

    internal void AAAAA()
    {
        下面.enabled = true;
        上面.enabled = true; 
        GetComponent<Image>().enabled =true; 
    }

    //public bool 显示
    //{
    //    get
    //    {
    //        return 这里.enabled;
    //    }
    //    set
    //    {
    //        上面.enabled = value;
    //        下面.enabled = value;
    //        这里.enabled = value;
    //    }
    //}

    public   Color colorLast,colorStart; 
    public   void Awake()
    {
        rt = GetComponent<RectTransform >();
        这里 = GetComponent<Image>();
        下面.color = colorStart;
        I = 生命.GetComponent<I_生命>();

        if (尺寸变化)
        { 
            开始PoY = rt.anchoredPosition.y;
            比例 = rt.sizeDelta.y / 100;
        }


    }
    public bool 尺寸变化=false ;
    float 开始PoY;
    void Update()
    {
        HpChange(); 

        if (尺寸变化)
        {
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, MaxHp * 比例);
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, 开始PoY + (MaxHp - 100) * 比例 / 2);    ///100  为-0    300         200为-150   150  
        }
    }
    float LastHp;

    [SerializeField ]
    [DisableOnPlay]
    bool B;
    void  HpChange()
    { 
        if (LastHp!=Hp)
        {
            B = LastHp > Hp;
            LastHp = Hp; 
        }
        if (B)
        {
            //扣血
            下面.color = 下面.color.Lerp(colorStart, 0.1f);
            //hpImageRad.color = Mathf_.p(hpImageRad.color, colorStart, 0.1f);

            下面.fillAmount = Mathf.Lerp(下面.fillAmount, Hp / MaxHp, 0.1f);
            上面.fillAmount = Hp / MaxHp;
        }
        else
        {            //回血
            下面.color = 下面.color.Lerp(colorLast, 0.1f);
            //hpImageRad.color =Mathf_.p(hpImageRad.color,colorLast,0.1f);

            下面.fillAmount = Hp / MaxHp;
            上面.fillAmount = Mathf.Lerp(上面.fillAmount, Hp / MaxHp, 0.1f);
        }
         
    }
public   GameObject 生命;
  public   I_生命 I;
  float Hp
    {
        get =>  I.当前hp;
    }
 float    MaxHp
    {
        get =>  I.hpMax;
    }

}
