using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class MonoMager : MonoBehaviour,I_暂停
{
    监控激活碰撞框 监控;
  [DisableOnPlay ]
   public   bool 关闭 = false;

    List<I_暂停> Is=new List<I_暂停>();
 
  [SerializeField ][DisableOnPlay ]
    int 数量;

    public bool 暂停 { get => 关闭; set {Debug.LogError("外部不可以设置到这里  反省"); } }

    private void Awake()
    { 
        foreach (var c in GetComponents<I_暂停>())
        {
            if (c is MonoBehaviour mb && mb == this) continue;
            Is.Add(c);
        }

        gameObject.组件(ref 监控);
        监控.是我 += 刷新;
        数量 = Is.Count;
    }
    public bool Deb;
    public void 刷新(bool b)
    {
        if (Deb)
        {
            Debug.LogError(b +"     "+ gameObject.transform.position + gameObject.name);
        }

        b = !b;
        for (int i = 0; i < Is.Count; i++)
        {
     
            Is[i].暂停 = b;
        }
        关闭 = b;
    }
}
