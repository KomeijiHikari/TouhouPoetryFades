using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DASH2
{

    public Action<DASH2> 恢复;
    public float 冲刺持续时间;
    public float 冲刺基础速度;
    public float 冲刺加速度;
    public float 冲刺冷却时间;
    [DisplayOnly]
    [SerializeField]
    bool 冷却好了_ = true;
    public bool 冷却好了
    {
        get { return 冷却好了_; }
        set
        {
            if (冷却好了_ && !value)
            {
                恢复.Invoke(this);
            }
            冷却好了_ = value;
        }
    }

}
