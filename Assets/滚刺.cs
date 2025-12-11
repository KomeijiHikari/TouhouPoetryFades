using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleFSM;
using Sirenix.OdinInspector;
using System;
using Unity.VisualScripting;

namespace Enemmy
{
    public class 滚刺 : MonoBehaviour ,I_攻击
    {

       
        生命周期管理 ss;
        [SerializeField]
        SpriteRenderer sr;
        Phy p;
        Enemy_base e;
        Bounds s;

        [SerializeField]
        bool 顺时针;
 
        float movespeed=>e.Speed_Lv*e.Move_speed;

        public float atkvalue { get => atkvalue1; set => atkvalue1 = value; }

        // 存储 bounds 的四个端点（顺序：左上、右上、右下、左下）
        Vector3[] corners;

        // 当前目标角索引
        int currentTargetIndex = 0;
        // 到达目标的容差（平方距离）
        const float arriveSqrThreshold = 0.001f;
         
         
        private void Start()
        {
            ss=GetComponent<生命周期管理>(); 

            p =GetComponent<Phy>();
            e = GetComponent<Enemy_base>();
             s=new Bounds(sr.bounds.center, sr.bounds.size+(e.co.bounds.size +Vector3.one*0.08f));

            e.E_重制 += () => { currentTargetIndex = startInx; };

            // 计算四个端点并保存到 corners
            Vector3 min = s.min;
            Vector3 max = s.max;
            float z = s.center.z;

            Vector3 topLeft = new Vector3(min.x, max.y, z);
            Vector3 topRight = new Vector3(max.x, max.y, z);
            Vector3 bottomRight = new Vector3(max.x, min.y, z);
            Vector3 bottomLeft = new Vector3(min.x, min.y, z);

            corners = new Vector3[] { topLeft, topRight, bottomRight, bottomLeft };

            // 可视化四条边（仅用于调试）
            Debug.DrawLine(topLeft, topRight, Color.green, 1f);
            Debug.DrawLine(topRight, bottomRight, Color.green, 1f);
            Debug.DrawLine(bottomRight, bottomLeft, Color.green, 1f);
            Debug.DrawLine(bottomLeft, topLeft, Color.green, 1f);

            刷新();
        }

        int startInx;
        private void 刷新()
        {
            // 选取最近的角作为起点，并设置下一个目标为顺时针/逆时针方向的角
            if (corners.Length > 0)
            {
                int nearest = 0;
                float minDist = float.MaxValue;
                for (int i = 0; i < corners.Length; i++)
                {
                    float d = (transform.position - corners[i]).sqrMagnitude;
                    if (d < minDist)
                    {
                        Debug.LogError(i + "      "+d + "      " + minDist + "   ");
                        minDist = d;
                        nearest = i;

                    }
                    Debug.LogError(i+"      "+d+"   ");
                }
                startInx = currentTargetIndex; 
                // 目标为 nearest 的下一个点（按顺时针或逆时针）
                //currentTargetIndex = 顺时针 ? (nearest + 1) % corners.Length : (nearest - 1 + corners.Length) % corners.Length;
                //Debug.LogError(currentTargetIndex);
            }
        }
        [SerializeField]
        float speed;
        [SerializeField]
        private float atkvalue1=10;

        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    if (collision.CompareTag(Initialize.Player)) 
        //        Player3.I.被扣血(10,gameObject,Time.frameCount);
 
        //}
        private void FixedUpdate()
        { 
            // 如果没有角点或速度为0则不移动
            if (corners == null || corners.Length == 0) return;
            if (Mathf.Approximately(movespeed, 0f)) return;

            Vector3 target = corners[currentTargetIndex];
            ((Vector2)target).DraClirl(3,Color.blue,3f);
            speed = movespeed * Time.fixedDeltaTime;
             
            transform.position = Vector3.MoveTowards(transform.position, target, speed); 
            //transform.position += Vector3.one * 0.01f;
            // 到达目标后切换到下一个目标
            if ((transform.position - target).sqrMagnitude <= arriveSqrThreshold)
            {
                int delta = 顺时针 ? 1 : -1;
                currentTargetIndex = (currentTargetIndex + delta + corners.Length) % corners.Length;
            }
        }

        public void 扣攻击(float i)
        {
            throw new NotImplementedException();
        }
    }


}
