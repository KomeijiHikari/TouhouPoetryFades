using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_P_Father : MonoBehaviour
{
    [DisplayOnly]
    [SerializeField ]
 Move_P[]  ms;
    [SerializeField ]
    BoxCollider2D 超速碰撞框;
    [SerializeField ]
    Transform trA,trB;
    Move_P 主
    {
        get
        {
            if (ms!=null)
            {
                if (ms.Length>0)
                {
                    return ms[0];
                }
            }
            Debug.LogError("MOVE_P  为空 ");
            return null;
        }
    }
    void 超速()
    {
        var x = 0f;
        var y = 0f;


        float poX = 0, poY = 0;
        switch (主.移动方式)
        {
            case Move_P.方式.竖直:
                y = Mathf.Abs(trA.position.y - trB.position.y); 
                x = 主.sp.size.x;

                poY = ASD(trA.localPosition.y, trB.localPosition.y);
                break;
            case Move_P.方式.水平:
                y = Mathf.Abs(trA.position.x - trB.position.x);
                x = 主.sp.size.y;

                poX = ASD(trA.localPosition.x , trB.localPosition.x) ;
                break;
            case Move_P.方式.自由:
                break;
        }
        switch (主.移动方式)
        {
            case Move_P.方式.竖直:
            case Move_P.方式.水平:
                //Debug.LogError(poX+"        "+ poY);
                超速碰撞框.transform.localPosition  = new Vector2(poX, poY);
                超速碰撞框.size = new Vector2(x+0.2f,y + 0.2f);
                break;
        }
    }
    float  ASD(float A,float B)
    {
        var f= (A + B) / 2;
            return f; 
    }
    void Start()
    {
        ms= GetComponentsInChildren<Move_P>();
        for (int i = 0; i < ms.Length; i++)
        { 
            var a = ms[i];  
        }
        超速();
        //Initialize_Mono.I.Waite( ()=>   超速());
        超速碰撞框.gameObject .SetActive(false)  ;
    }

    [Space]
  public   Transform Move_ToA;
  public   Transform Move_ToB;
    public void Trriger_重新设置( )
    {
        for (int i = 0; i < ms.Length; i++)
        {
            var a = ms[i];
            if (Move_ToA!=null&& Move_ToB!=null)
            {
                a.A.position = Move_ToA.position;
                a.moveto.position = Move_ToB.position;
                a.重新设置点位();
            }
            else
            {
                Debug.LogError(gameObject +"没有改变后的点");
            }

        }
    }

    [SerializeField ]
      E_超速等级 超速等级;
    private void Update()
    {
        超速等级 = 主.I_S.超速等级;
        switch (超速等级)
        {
            case E_超速等级.低速:
                break;
            case E_超速等级.正常:
                break;
            case E_超速等级.超速:
                break;
            case E_超速等级.半虚化:
                break;
            case E_超速等级.虚化:
                break;
            case E_超速等级.虚无:
                break;
            default:
                break;
        }
    }

 
    float 原先Lv;
    /// <summary>
    /// unity event触发
    /// </summary>
    /// <param name="LV"></param>
    public void Set_LV(float LV)
    {
        float 原先 = -11;
        for (int i = 0; i < ms.Length; i++) //获取当前
        {
            原先 = ms[i].Speed_Lv; 
        }

        if (原先 != LV)   //对比   不同
        {
            原先Lv = 原先;
        }
        else //对比   相同
        {
            LV = 原先Lv;
        }
        for (int i = 0; i < ms.Length; i++)
        {
            var a = ms[i];
            a.Set_LV(LV);
        }
  
    }
    /// <summary>
    /// unity event触发
    /// </summary>
    /// <param name="LV"></param>
    public void Re_LV()
    {
        for (int i = 0; i < ms.Length; i++)
        {
            var a = ms[i];
            a.Re_LV();
        } 
    }
}
