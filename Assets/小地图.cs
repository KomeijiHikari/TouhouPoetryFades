using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 小地图 : MonoBehaviour
{
    [SerializeField ]
    Camera 小地图摄像机;

   public  float fov; 
    private void Update()
    {
        if (摄像机.I!=null && 摄像机.I.碰撞组件!=null && 摄像机.I.碰撞组件.m_BoundingShape2D!=null)
        {

            Vector2 v = 摄像机.I.碰撞组件.m_BoundingShape2D.transform.position;
            小地图摄像机.transform.position = new Vector3(v.x, v.y, 小地图摄像机.transform.position.z);
        }

    }
}
