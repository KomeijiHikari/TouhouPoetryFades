using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 宝箱 : MonoBehaviour, I_生命
{
    BoxCollider2D bc;

    [SerializeField]
    private float 当前hp1;

    public Action 生命归零 { get ; set  ; }
    public float 当前hp { get => 当前hp1; set => 当前hp1 = value; }
    public float hpMax { get; set; } = 10;
    public bool HPROCK { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void 扣最大上限(float i)
    {
        throw new NotImplementedException();
    }

    public void 被扣血(float i, GameObject obj, int key)
    {
        当前hp1 -= i;
    }
}
