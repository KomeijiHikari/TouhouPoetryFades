using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tree_
{
    public class 摩擦力set : Aweak
    {
        public SharedFloat 摩擦力= 0;
        public bool 还原;
        float 原摩擦力;
        public override void OnStart()
        {
            base.OnStart();
            原摩擦力 = b.p.Set_摩擦力( );
        }
        public override TaskStatus OnUpdate()
        {
            if (还原)
            {
                b.p.Set_摩擦力(原摩擦力);
                return TaskStatus.Success;
            }
            else
            {
                b.p.Set_摩擦力(摩擦力.Value);
                return TaskStatus.Success;
            }

        }
    }
}

