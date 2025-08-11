using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss血条 : MonoBehaviour
{ 
    [SerializeField] [DisableOnPlay] UI_HPMP Boss血条组件;
    [SerializeField] [DisableOnPlay] UI_韧性 UI_韧性;
    [SerializeField] [DisableOnPlay] Text Name;

    public void Boss血条_(GameObject 那个Boss, String name, bool 开关)
    {
         UI_韧性.E =那个Boss.GetComponent<Enemy_base>(); 
        Boss血条组件.生命 = 那个Boss;
        Boss血条组件.I = Boss血条组件.生命.GetComponent<I_生命>();
 

        Name.text = name;
        UI_韧性.Start();

        Initialize_Mono.I.Waite(()=> {
            Boss血条组件.AAAAA();
        },0.01f);
 
    } 
}
