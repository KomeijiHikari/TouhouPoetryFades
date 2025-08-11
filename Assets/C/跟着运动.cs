using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 跟着运动 : MonoBehaviour
{
    [SerializeField]
    Collider2D  Targetc;
    [SerializeField ]
    Transform Target;
    Vector3 差;
    [SerializeField ]
    bool 零距离;
    private void Awake()
    {
   if(Target!=null)     差 = transform.position - Target.transform.position;
        if (Targetc != null) 差 = transform.position - Targetc.bounds.center;
        if (零距离) 差 = Vector2.zero;
    }
    private void Update()
    {
        if (Targetc != null) transform.position = Targetc.bounds.center + 差;
        if (Target != null) transform.position = Target.transform.position+差;
    }
}
