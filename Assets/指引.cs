using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 指引 : MonoBehaviour, I_Dead, I_Revive
{
    [SerializeField]
    bool 初始开关;
    [SerializeField]
    List<Component> 组件列表;
    SpriteRenderer sp;
    public Action 销毁触发 { get    ; set    ; }
    public Bounds 盒子 =>sp.bounds ;
    public bool Re { get; set; } = false;
    public float Re_Time { get  ; set ; } 
    public bool Dead()
    {
        组件列表.集体开关(false );
        return true;
    } 
    public bool 重制()
    {
        组件列表.集体开关(true);
        return true;
    } 
    private void Awake()
    {
        sp = GetComponent<SpriteRenderer >();
    }

    private void Start()
    {
        组件列表.集体开关(初始开关 );
    }
}
