using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tree_;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("把目标设为巡逻点")]
public class ChangeTarget : Aweak
{ //被调用的时候改变Target
 
    //public List< Transform> 目标列表 ;
    public SharedVector2 tagert; 
    public override  TaskStatus OnUpdate()
    {///  1 2 3     3 2 1 
        if (value.目标列表.Count==1)
            return TaskStatus.Success;

        if (value.目标列表 == null)
        {
            return TaskStatus.Failure;
        }
        int C = GetInt(tagert);
        if (C== value.目标列表.Count-1)
        {
            //超出
            value.目标列表.Reverse();
            C = GetInt(tagert);
            C++;
        }
        else
        {
           // 在范围内
            C++; 
        }
        tagert.Value = value.目标列表[C] ;

        return TaskStatus.Success;
    }

    int GetInt(SharedVector2 t)
    {
        for (int i = 0; i < value.目标列表.Count; i++)
        {
            if (value.目标列表[i] == t.Value )
            {
                return i;
            }
        } 
        return 0;
    }
}
