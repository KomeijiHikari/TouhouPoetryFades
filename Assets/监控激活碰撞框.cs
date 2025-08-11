using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 监控激活碰撞框 : MonoBehaviour
{
    [SerializeField]
    [DisplayOnly]
    int 所属相机编号 = -999;

    public Action<bool> 是我;
    private void Start()
    {
        所属相机编号 = transform.Get_摄像框编号();
        Initialize_Mono.I.重制触发 += 响应; 
    }
 public void 刷新()
    {

    }
    private void 响应(int 场景, int 编号)
    {
        var a = 场景 == gameObject.scene.buildIndex && 编号 == 所属相机编号;

        if (gameObject.name == "molisa")
        {
          bool AAA = 场景 == gameObject.scene.buildIndex && 编号 == 所属相机编号; 
        }
        if (a)
        { 
        } 
        是我?.Invoke(场景 == gameObject.scene.buildIndex && 编号 == 所属相机编号);  
    }
}
