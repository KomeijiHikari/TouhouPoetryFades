using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;


/// <summary>
/// 玩家速度越快，应该距离玩家越远
/// </summary>

public class 焦点 : MonoBehaviour
{
    public static 焦点 I;
 enum E_跟随状态
    {
       跟主角,
       不跟主角
    }

    [SerializeField]
     E_跟随状态 跟随状态;
     
    public new Transform transform
    {
        get
        {
            return base.transform;
        }
    }
    public Vector3 Loca_应该在的位置
    {
        get
        {
            var x = 摄像机.I.Camera_Bounds.size.x * 相对屏幕比例.x;
            var y = 摄像机.I.Camera_Bounds.size.x * 相对屏幕比例.y;
            return new Vector3(x, y, transform.localPosition.z);
        }
    }
    public Vector2 相对屏幕比例;
    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(this);
        }
        else
        {
            I = this;
        }
    }
     
    private void Update()
    {  

        switch (跟随状态)
        {
            case E_跟随状态.跟主角:
                transform.localPosition = Loca_应该在的位置;
                break;
            case E_跟随状态.不跟主角:
                break; 
        } 
    } 
    public void set_Parent(Transform target)
    {
        transform.SetParent(target);
        if (target== Player3.I.transform)
        {
            跟随状态 = E_跟随状态.跟主角;
        }
    }
} 
 

#region
//public class 焦点 : MonoBehaviour
//{

//    public bool 开启;
//    float m;
//    float TargetFloat;
//    float 减少值;
//    float m__;

//    [SerializeField]
//    float 减少多少百分比 = 0.3f;
//    [SerializeField]
//    float 回弹速度 = 0.025f;
//    [SerializeField]
//    float 进入速度 = 0.01f;
//    private void Awake()
//    {
//        摄像机.I.设置相机跟随(gameObject);
//    }
//    private void Update()
//    {

//        transform.localPosition = new Vector2(摄像机.I.X * 0.25f, transform.localPosition.y);
//        开启 = Player3.I.Ground && Player_input.I.方向正零负_非零计时器 > 0.5f && Player3.I.Velocity != Vector2.zero;
//        //开启 =   Player3.I.Velocity!=Vector2.zero ;
//        if (开启)
//        {
//            TargetFloat = 0;
//            减少值 = Mathf.Lerp(减少值, TargetFloat, 进入速度);

//        }
//        else
//        {
//            TargetFloat = 摄像机.I.当前场景默认FOV * 减少多少百分比;
//            减少值 = Mathf.Lerp(减少值, TargetFloat, 回弹速度);
//        }




//        摄像机.I.Fov = 摄像机.I.当前场景默认FOV - 减少值;
//    }
//}
#endregion