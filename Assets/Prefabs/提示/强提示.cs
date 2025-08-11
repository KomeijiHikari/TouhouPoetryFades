using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 强提示 : MonoBehaviour
{
    Image ima; 
    Text_button_Father T;  

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
    public void 开 (GameObject obj )
    {
        if (Time.frameCount !=CCC)
        {
            CCC = Time.frameCount;

      Instantiate(obj,transform );
            初始化();
     

            主UI.I.展开(T,false );
        }

    }
}
