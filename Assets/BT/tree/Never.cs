using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Tree_;

[TaskDescription("一直返回run")]

public class Never : Aweak
{
 
    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Running;
    }
}