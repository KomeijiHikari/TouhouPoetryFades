using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class flow : MonoBehaviour
{
    public bool 简单跟随;
   
    public bool can_lerp = true;
    public bool can_moveSpeedAstrict = false;
    public bool can_piao = true;
 
    public float zhenFu = 10f;//振幅
    public float HZ = 1f;//频率
    public float XzhenFu = 2f;//振幅
    public float XHZ = 0.2f;//频率

    public Transform TargetTransform;
    public float moveSpeed;


    Vector3 targetPosition;

    Vector3 差;
    private void Awake()
    {
        if (简单跟随) 差 = transform.position-TargetTransform.position ;
    }
    private void Update()
    {
        if (简单跟随)     transform.position = TargetTransform.position + 差;
    }
    private void LateUpdate()
    {
        if (简单跟随) return;
        if (transform.position != TargetTransform.position)
        {

            targetPosition = new Vector3(
                TargetTransform.localPosition.x,
                TargetTransform.localPosition.y, 
                transform.localPosition.z);
            //初始化目标坐标是   玩家坐标的XY，自己的Z
 
                if (can_piao)
                {
                    targetPosition.y = TargetTransform.position.y + Mathf.Sin(Time.fixedTime * Mathf.PI * HZ) * zhenFu;
                    targetPosition.x = TargetTransform.position.x + Mathf.Sin(Time.fixedTime * Mathf.PI * XHZ) * XzhenFu;

                }
                if (can_lerp)
                {
                    transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                }
                else
                {
                transform.position = new  Vector2(TargetTransform.position.x, TargetTransform.position.y);
                    }
            
        }

    }

     
    



}
