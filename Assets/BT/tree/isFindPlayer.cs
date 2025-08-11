using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tree_;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


[TaskDescription(" 有发现玩家完成  没发现失败")]
public class isFindPlayer : Conditional
{
    Enemy_base b;
    戒备 j;

    public bool 比较距离=false ;
    public SharedFloat 距离= 10;

    public override void OnAwake()
    {
        b = GetComponent<Enemy_base>();
        j = GetComponent<戒备>();
    }
    public override TaskStatus OnUpdate()
    {
        if (比较距离)
        {
       var f=     Mathf.Abs(Player3.I.transform.position.x - gameObject.transform.position.x);
            if (f< 距离.Value)
            {
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }
        if ( j.返回一个玩家 !=null)
        {
            //b.Velocity = Vector2.zero;
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }

    }

}
 