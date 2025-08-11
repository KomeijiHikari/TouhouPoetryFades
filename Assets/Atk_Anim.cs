using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atk_Anim : MonoBehaviour
{


    [SerializeField ]
    public     Vector2 原先碰撞位置_to;
    Vector2 原先碰撞位置;
    [SerializeField ]
     Collider2D 伤害碰撞箱;


    [SerializeField]
    public Vector2 size;
    [SerializeField]
    BoxCollider2D father;

    [SerializeField]
    public  GameObject 关掉和开启;
    public Action ATK;
    public void  Anim_Action()
    { 
        ATK?.Invoke();
    }
    private void Awake()
    {
        if (伤害碰撞箱!=null)
        {
        原先碰撞位置 = 伤害碰撞箱.transform.localPosition;

        }
    }

    private void Update()
    {
        if (伤害碰撞箱 != null)
        {
            伤害碰撞箱.transform.localPosition = 原先碰撞位置 + 原先碰撞位置_to;
        }
    }
}
