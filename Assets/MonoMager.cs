using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoMager : MonoBehaviour
{
    监控激活碰撞框 监控;
    [SerializeField ][DisableOnPlay ]
      bool 关闭 = false;
    I_暂停[] Is;
  [SerializeField ][DisableOnPlay ]
    int 数量;

 
    private void Awake()
    {
        Is = GetComponents<I_暂停 >();
        gameObject.组件(ref 监控);
        监控.是我 += 刷新;
        数量 = Is.Length;
    }
    public void 刷新(bool b)
    {


        b = !b;
        for (int i = 0; i < Is.Length; i++)
        {
            Is[i].暂停 = b;
        }
        关闭 = b;
    }
}
