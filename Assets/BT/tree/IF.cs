using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Tree_;

[TaskDescription(@"如果开关为true   那运行")]
public class IF : Decorator
{
    public  bool 反转;
    public SharedBool g;       
    public override bool CanExecute()
        { 
            return g.Value;
        }
    public override void OnChildExecuted(TaskStatus childStatus)
    {
        base.OnChildExecuted(TaskStatus.Success );
    }

}