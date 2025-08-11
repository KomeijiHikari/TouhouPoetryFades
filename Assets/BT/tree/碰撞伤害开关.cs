using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tree_;
using BehaviorDesigner.Runtime.Tasks;

public class 碰撞伤害开关 : Aweak
{
    public bool 开关;
    public override TaskStatus OnUpdate()
    {
        b.碰撞开关 = 开关;
        return TaskStatus.Success;
    }
}
