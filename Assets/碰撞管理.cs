using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 碰撞管理器
/// 负责协调一个游戏对象上多个 I_碰撞无视 接口的冲突 
/// 只管理同一个游戏对象上的碰撞器
/// </summary>
public class 碰撞管理 : MonoBehaviour
{
    I_碰撞状态[] ps;
    [SerializeField]
    Collider2D c2;
    private void Awake()
    {
        if (c2 == null)
        {
            c2 = GetComponent<Collider2D>();
        }
        ps = GetComponents<I_碰撞状态>();
    }
    private void Start()
    {
        刷新();
    }
    private void Update()
    {
        if (c2 == null || ps == null)
        {
            Debug.LogError("    " + gameObject + transform.position + "碰撞管理器无效");
            return;
        }

        e = asd();

    }

    private void 刷新()
    {
        switch (In界面显示)
        {
            case E_碰撞状态.无碰撞:
                c2.enabled = false;
                break;
            case E_碰撞状态.触发器:
                c2.enabled = true;
                c2.isTrigger = true;
                break;
            case E_碰撞状态.碰撞:
                c2.enabled = true;
                c2.isTrigger = false;
                break; 
        }
    }
    E_碰撞状态 asd()
    {
        E_碰撞状态 Out = E_碰撞状态.无碰撞;

        for (int i = 0; i < ps.Length; i++)
        {
            var a = ps[i];
      

            switch (a.E_碰撞)
            {
                case E_碰撞状态.无碰撞:
                    return E_碰撞状态.无碰撞;
                ///当同时有碰撞和触发器
                case E_碰撞状态.触发器:
                    return E_碰撞状态.触发器; 
                case E_碰撞状态.碰撞:
                    Out = E_碰撞状态.碰撞;
                    break;
            } 
        }
        return Out;
    }
 E_碰撞状态 e { get { return In界面显示; }set {
            if (In界面显示 != value)
            {
                In界面显示=value;
                刷新();
            } 
        }}
    [SerializeField][DisplayOnly]   E_碰撞状态 In界面显示=E_碰撞状态.碰撞;
}

/// <summary>
/// 碰撞无视接口
/// 任何需要控制碰撞器状态的脚本都可以实现这个接口
/// </summary>
public interface I_碰撞状态
{ 
    E_碰撞状态 E_碰撞 { get; set; }
}
public enum E_碰撞状态
{
    碰撞,
    无碰撞,
    触发器,
}