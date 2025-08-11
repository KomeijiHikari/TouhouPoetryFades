using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 唯一Key : MonoBehaviour
{
    public string Key;
    生命周期管理 S;

    bool 开始检测;
    private void Awake()
    {
        S = GetComponent<生命周期管理 >();
        S.效果_死亡Enter += () => {
            Save_D.Add(Key,"是啊");
        };
    }

    private void Start()
    {
        Initialize_Mono.I.Key_Action += asd;  ///运行后监听
        Initialize_Mono.I.Waite(()=> {      ///运行前监听

            if (Save_D.存档字典_.ContainsKey(Key))
        {
            S.Event_销毁();
        } 
        
        });
    }

    private void asd(string obj)
    {
        if (obj==Key)
        {
            S.Event_销毁();
        }
    }
}
