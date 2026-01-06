using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 提示管理速度 : MonoBehaviour,I_BOOL
{
    public float speed = 1.0f;
    [SerializeField]
    private bool 好1; 
    public bool 好 { get => 好1; set => 好1 = value; }
    public bool 反转 { get  ; set  ; }

    public bool 好嘛()
    {
        好 = Player3.Public_Const_Speed == speed; 
        return 好;
    }
}
