using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 分身消失 : MonoBehaviour
{
    SpriteRenderer sp;
    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        sp.material = 材质管理.Get_Material(材质管理.残影);
    }
    private void OnEnable()
    {
        f = 0; 

    } 
    float f;
    private void Update()
    {
        f += Time.deltaTime;
        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1-( f /  残影.I.残留时间) );
        if (f >= 残影.I.残留时间 )
        {
            残影.I.pool.Release(gameObject);
        }
    }
}
 
