using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class 注册 : MonoBehaviour
{
    public bool Fog; 

    private void Start()
    {
        if (Fog)
        {
            我的光照2.I.ForgSprites.Add(GetComponent<SpriteRenderer>());
        }
        else
        {
            我的光照2.I.受影响.Add(GetComponent<Renderer>());
        }

    }
}
//public static float A_SIze(float Size, float angle)
//{
//    return Size * Mathf.Tan(angle /2* Mathf.Deg2Rad);
//}
//public static float CalculateAngleA(float W, float Hight)
//{

//    // 计算角度A（弧度转角度）
//    float angleA = Mathf.Atan2(W, Hight /2) * Mathf.Rad2Deg;
//    return (90 - angleA)*2;
//}
//void CalculateEdgesByViewport()
//{


//    targetCamera=Camera.main;
//    targetZ =transform.position.z-targetCamera.transform.position.z;//Z 轴深度（即世界空间中与摄像机 Z 轴的距离）
//    if (targetCamera == null) return;

//    // 视口的四个角落（z值为目标距离，注意：透视摄像机的z是相对于摄像机的深度）
//    // 注意：摄像机的forward方向是-Z轴，所以目标z应为正数（在摄像机前方）
//    Vector3 viewportBottomLeft = new Vector3(0, 0, targetZ);
//    Vector3 viewportTopRight = new Vector3(1, 1, targetZ);

//    // 转换为世界坐标
//    Vector3 worldBottomLeft = targetCamera.ViewportToWorldPoint(viewportBottomLeft);
//    Vector3 worldTopRight = targetCamera.ViewportToWorldPoint(viewportTopRight);

//    // 提取边缘坐标
//    float left = worldBottomLeft.x;
//    float right = worldTopRight.x;
//    float bottom = worldBottomLeft.y;
//    float top = worldTopRight.y;

//    Size=new Vector3(right-left, top-bottom, 0);

//    Debug.LogError($"视口转换得到的边界（Z={targetZ}）：");
//    Debug.LogError($"左：{left}，右：{right}，下：{bottom}，上：{top}");

//    angleA = CalculateAngleA(targetZ, GetComponent<SpriteRenderer>().bounds.size.y);
//    SizeY= A_SIze(    targetZ,angleA ) *2;
//} 