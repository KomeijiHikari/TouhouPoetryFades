using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 小白 : SampleFSM.泛用状态机
{
    Enemy_base e;
 
 
    private void Awake()
    {
        //碰撞伤害开关
        e = GetComponent<Enemy_base>();
    }
    protected override void Update()
    {
        if (e.暂停) return;
        base.Update();
        if (e.an.AnimatorNameIs() != "atk")
        {
            e.碰撞开关 = false;
        }
    }
}
