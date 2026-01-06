using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 我的光照3 : MonoBehaviour
{
    public  static 我的光照3 I;
    public List<Material>   set;

    private void Awake()
    {
        if (I != null && I != this) Destroy(this);
        else I = this;
    } 
}
