using SampleFSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Enemmy
{ 


public class 幽灵 : MonoBehaviour
    {
        Enemy_base e;
        戒备 j;

        private void Awake()
        {
            e= GetComponent<Enemy_base>();
            j= GetComponent<戒备>();
            startway = transform.position;
        }
        Vector3 startway; 
   void FixedUpdate()
    { 
            Vector2 mo;
            Vector3 target;
            if (j.发现玩家了吗)
            {
                target = Player3.I.transform.position;
            }
            else
            {
                target = startway;
            }
            var speed =  e.Move_speed;
            var c = target - transform.position;
            if (c.sqrMagnitude < speed* speed) return;
            var a = (Vector2)(c).normalized;
            mo = a * speed;
             
            e.LocalScaleX_Set = mo.x;
            e.p.Velocity  = mo;
    }
}
}