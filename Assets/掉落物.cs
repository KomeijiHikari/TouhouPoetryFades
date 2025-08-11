using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 掉落物 : MonoBehaviour 
{
    Enemy_base E; 

 

    private void Awake()
    { 
        E = GetComponent<Enemy_base>();

        if (E!=null )
        { 
            E.销毁触发 += 销毁触发;
        }
    }

  void 销毁触发()
    {
        Debug.LogError(" 销毁触发()");

        float 百分比 =(1 - Player3.I.当前hp / Player3.I.hpMax) ;
        float 几率 = 百分比* 百分比 * 100;
         
        int 血 = Initialize.Get_随机种子().Next(0,100);
        int 钱 = Initialize.Get_随机种子().Next(0, 100);
        if (血<= 几率)
        {
          var a=  Surp_Pool.I.GetPool("加血");
            a.transform.position = transform.position;
            a.transform.SetParent(null);
        }
        if (钱 <= 几率)
        {
            var a = Surp_Pool.I.GetPool("加钱");
            a.transform.position = transform.position;
            a.transform.SetParent(null);
        }

        Debug.LogError(几率 + "       "+ 血 + (血 <= 几率) +"     "+ 几率 + "           "+ 钱 + (钱 <= 几率));
    }
}
