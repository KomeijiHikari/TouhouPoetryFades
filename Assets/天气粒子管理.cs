using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]

public class 天气粒子管理 : MonoBehaviour
{
    public static 天气粒子管理 I;
     private void Awake()
    {
        if (I != null && I != this) Destroy(this);
        else I = this;
    }
    public bool 开关;
    public List<GameObject> 天气粒子列表;

    bool b;
    private void Start()
    {
        天气粒子列表.ForEach(a => a.SetActive(开关));
    }
    private void Update()
    {
        if (b!= 开关)
        {
            b = 开关;
            天气粒子列表?.ForEach(a => a.SetActive(开关));
        }
    }
}
