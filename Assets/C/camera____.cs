using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(2)]
public class camera____ : MonoBehaviour
{/// <summary>
/// 玩家按上和下转移实现时候用的
/// </summary>
   public  GameObject target;
    public float 延迟 = 0.5f;
    [DisplayOnly]
    [SerializeField]
 bool b_;
 bool 跟踪 { get => b_;
        set {
            if (!跟踪&&value )
            {
                StartCoroutine(asdasdasd());
            }
            b_ = value;
        } }

  
    IEnumerator asdasdasd()
    {
        yield return new WaitForSeconds(0.01f);
        跟踪 = false;
    }


    private void LateUpdate()
    {
        if (!跟踪)
        {
            transform.position = Vector2.Lerp(transform.position, target.transform.position, 0.2f);
        }
    }
 
    private void Update()
    {
   
        if (Player3.I._4.当前anim.name == "run_chang_to0")
        {
            跟踪 = true;
        } 
    }



 
}
