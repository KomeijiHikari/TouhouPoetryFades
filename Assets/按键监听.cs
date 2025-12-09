using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 按键监听 : MonoBehaviour
{
    Text_button_Father BF;
    public Action<KeyCode> WhatKey;
    private void Awake()
    {
        BF=GetComponent<Text_button_Father>();
        BF.Action_回退+= () =>
        {
            WhatKey = delegate { }; ;
        };
    }
    void Update()
    {
      KeyCode k=  Player_input.getKeyDownCode();
        if (k!=KeyCode.None)
        {
            ///有输入
            if (k==KeyCode.Escape)
            {      ///退回 不做改变
                //BF.被回退();
                //BF.回退();
            }
            else  
            {
                ///正确输入了
                WhatKey?.Invoke(k);

                BF.回退();
            }
        }
 
            ///无输入   继续
 
     
    }
}
