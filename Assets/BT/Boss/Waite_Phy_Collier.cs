using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskDescription(@"如果开关为否   那一直运行")]
public class Waite_Phy_Collier : Decorator
{ 
    public SharedBool g;       
    public override bool CanExecute()
        { 
            return !g.Value ;
        }

}