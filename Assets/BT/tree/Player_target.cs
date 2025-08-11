using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Tree_;

[TaskDescription("把目标设为玩家的坐标,不返回")]
public class Player_target : Aweak
{  
    戒备 j;
    public SharedVector2 tagert;
 
    public override void OnStart()
    {
        if (j.返回一个玩家 != null)
        {
            tagert.Value = j.返回一个玩家.transform.position;

            Vector2 cha = (Vector2)transform.position - tagert.Value;
            b.LocalScaleX_Set = -cha.x;
        }
        else
        {
            Debug.LogError("        AAAAAAAAAAAAAAAAAAAAAAAAA");
        }


 
    }
    public override void OnAwake()
    {
        base.OnAwake();
        j = GetComponent <戒备> ();
    }

}
