using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Tree_;
[TaskDescription(" 有发现玩家完成  没发现继续")]
public class WaiteForFindPlayer : Aweak
{
    戒备 j;
    public override void OnAwake()
    {
        j = GetComponent<戒备>();
    }
    public override TaskStatus OnUpdate()
    {

        if (j.发现玩家了吗)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Running;
        }

    }
}
