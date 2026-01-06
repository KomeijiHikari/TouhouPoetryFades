using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 提示管理机关 : MonoBehaviour,I_BOOL
{
    public 生命周期管理 S;
    [SerializeField]
    private bool 好1;

    [SerializeField]
    private bool 反转1;

    public bool 好 { get => 好1; set => 好1 = value; }
    public bool 反转 { get => 反转1; set => 反转1 = value; }

    public bool 好嘛()
    {
        if (S!=null)
        {
            好 = S.在死亡笔记里面();
      
        }
        else
        {
            Debug.LogError(gameObject.name + transform.position );
            好 = false;
        }

        return 好;
    }
}
