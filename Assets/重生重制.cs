using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface  I_deadTo_Re
{
    void 重制();
}
public class 重生重制 : MonoBehaviour
{
    I_deadTo_Re D_R;
    private void Start()
    {
        D_R = GetComponent<I_deadTo_Re>();
        if (D_R != null)
        { 
            Player3.I.生命归零 += () => { 
                D_R.重制();
            };
        }
        else
        {
            Debug.LogError(gameObject .name +"  该物体没有实现接口的脚本");
        }
    }

 
}
