using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 强提示 : MonoBehaviour
{
    Image ima; 
    Text_button_Father T;
    public Action Action强提示_回退 { get => T.Action_回退;
        set => T.Action_回退 = value;
    } 
    bool 初始化过; 
    void 初始化()
    {
        if (!初始化过)
        {
            初始化过 = true;

            ima = GetComponent<Image>(); 
            T = GetComponent<Text_button_Father>();

            T.Action_回退 += 回退了;
        }
    }

    private void 回退了()
    {
        foreach (Transform child in transform )
        {
            Destroy(child.gameObject );
        }
    }
 

    int CCC;
    public Button_Father_base 开 (GameObject obj )
    {
        if (Time.frameCount !=CCC)
        {
            CCC = Time.frameCount;

      Instantiate(obj,transform );
            初始化();
     

        return    主UI.I.展开(T,false );
        }
        return null;
    }
}
