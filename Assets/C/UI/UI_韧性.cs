using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 

public class UI_韧性: MonoBehaviour
{
    public void AAAAA()
    {

        Debug.LogError(上面.enabled + "         " + 下面.enabled); ;
    }
    public  Enemy_base E;
    //public Text text;
    public Image 上面;
    public Image 下面;

    Image 这里;
     


      float   比例=  3;
    public bool 显示
    {
        get
        {
            return 这里.enabled;
        }
        set
        {
            上面.enabled = value;
            下面.enabled = value;
            这里.enabled = value;
        }
    }

    public   Color colorLast,colorStart; 
  public   void Start()
    { 
        这里 = GetComponent<Image>();
        下面.color = colorStart;  
    } 
    float 开始PoY;
    void Update()
    {
        HpChange();
        //if (text !=null) text.text = $"{ Hp}/{ MaxHp}"; 
    }
    float Last韧性;

    [SerializeField ]
    [DisableOnPlay]
    bool B;
    void  HpChange()
    { 
        if (Last韧性!=韧性)
        {
            B = Last韧性 > 韧性;
            Last韧性 = 韧性;

        }
        if (B)
        {
            //扣血
            下面.color = 下面.color.Lerp(colorStart, 0.1f); 

            下面.fillAmount = Mathf.Lerp(下面.fillAmount, 韧性 / Max韧性, 0.1f);
            上面.fillAmount = 韧性 / Max韧性;
        }
        else
        {            //回血
            下面.color = 下面.color.Lerp(colorLast, 0.1f); 

            下面.fillAmount = 韧性 / Max韧性;
            上面.fillAmount = Mathf.Lerp(上面.fillAmount, 韧性 / Max韧性, 0.1f);
        }
        ASDAS1 = 韧性 ;
        ASDAS2 =  Max韧性;
        ASDAS的 = 韧性 / Max韧性;
    }
    public float ASDAS1;
    public float ASDAS2;
    public float ASDAS的;
  float 韧性
    {
        get =>E.韧性_+ E.Max韧性;
    }
 float    Max韧性
    {
        get => E.Max韧性*2;
    }

}
