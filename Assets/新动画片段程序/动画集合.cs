using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class 动画集合 : MonoBehaviour, 亚拉动画片段
{ 
    public 亚拉动画片段[] Ans; 
    public Transform 原点;
    public float 角度;
    public bool Deb;
    private void Awake()
    { 
        Ans = transform.GetComponentsInChildren<亚拉动画片段>(); 
    }
 
 
    [Button("播放", ButtonSizes.Large)]
    public void 播放()
    {
        for (int i = 0; i < Ans.Length; i++)
        {
            var a = Ans[i];
            if (a!=this) a.播放(); 
 
        }
    }
    [Button("结束播放", ButtonSizes.Large)]
    public void 结束播放()
    {
        for (int i = 0; i < Ans.Length; i++)
        {
            var a = Ans[i];
            if (a != this) a.结束播放();
        }
    }
}
