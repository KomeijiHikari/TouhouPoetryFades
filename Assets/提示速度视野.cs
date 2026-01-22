using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 提示速度视野 : MonoBehaviour, I_BOOL
{
    [SerializeField]
    [DisplayOnly]
    private bool 好1;
    [SerializeField]
    private bool 反转1;

    public bool 好 { get => 好1; set => 好1 = value; }
    public bool 反转 { get => 反转1; set => 反转1 = value; }

    public bool 好嘛()
    {
        好 = 切换Shader.I.isSpeed;
        好 = 反转 ? !好 : 好;
        return 好;
    }
}
