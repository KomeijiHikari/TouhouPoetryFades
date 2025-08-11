using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 碰撞伤害 : MonoBehaviour
{
    BoxCollider2D bc;
    public bool 灵魂=true; 

    [SerializeField ]
    Enemy_base  e;
    public Action Atk;
    private void Awake()
    {
        gameObject.layer = Initialize.L_Enemy_hit_collision;
        bc = GetComponent<BoxCollider2D>();
        e =GetComponentInParent <Enemy_base>();
        bc.isTrigger = true;
    } 

    private void Update()
    { 
        bc.enabled = !(e.I_S.限制||e.is_Dead );

        if (e.碰撞开关 ==false )
        {
            bc.enabled = false;
        }
              //乘数 =(   Initialize.ScaleValue(e.I_S.Speed, Initialize_Mono.I.负阀值,  1))* e.I_S.Curttent_Speed; 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!灵魂) return;   
        if (!collision.CompareTag(Initialize.Player)) return;
        if (e == null) return;

         var value = e.atkvalue  ;
        
            if (value == 0) return;
        collision.GetComponent<I_生命>()?.被扣血( value, transform .parent.gameObject, 0); 
    }
}
