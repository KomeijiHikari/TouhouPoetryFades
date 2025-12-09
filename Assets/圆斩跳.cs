using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 圆斩跳 : MonoBehaviour
{
    [SerializeField ]
    BoxCollider2D Box;

    public float FF=4.7f;
   //bool ASD(List<bool > BL )
   // {
   //     if (BL != null && BL.Count >= 0)
   //     {
   //         for (int i = 0; i < BL.Count; i++)
   //         {
   //             var b = BL[i];
   //             if (!b) break;


   //         }
   //     }
   //     return false;
   // }
   int   答案(List<int >  a)
    {
        if (a != null && a.Count >= 0)
        {
            for (int i = 0; i < a.Count; i++)
            {
                var I = a[i];
                if (I == 1)
                {
                    return i;
                }
                else if (I == 0)
                {
                    continue;
                }
                else  
                { 
                    return -2;
                }
            }
        } 
            return -2;
    
    }
    public float    发射()
    {
        bool 方向 = Player3.I.transform.localScale.x == 1;
        var a = GenerateRaycastResults(方向, Box.bounds, 9, Player3.I.碰撞检测层);
         
        return a;
    }

    //-1 原点遮挡
    //0没碰到
    // 1 碰到了
    public float  GenerateRaycastResults(bool FX, Bounds B, int I, LayerMask LM)
    {
        List<int> results = new List<int>();
        List<Vector2> startPoints = new List<Vector2>();
        // 新增：保存对应的碰撞体（若有）
        List<Collider2D> hitColliders = new List<Collider2D>();

        // 在底边生成均匀分布的点
        float bottomY = B.min.y;

        // 计算X轴上的间距
 
        if (I < 1) Debug.LogError("I  不对劲");
        float spacing = I > 1 ? (B.max.x - B.min.x) / (I - 1) : 0; 
        // 从左到右生成所有点
        for (int i = 0; i < I; i++)
        {
            float xPos = B.min.x + i * spacing;

            Vector2 dan = new Vector2(xPos, bottomY);
            results.Add(-2);
            startPoints.Add(dan);
            hitColliders.Add(null);
        }
        //Debug.LogError(I + "      " + startPoints.Count+"    " + results.Count);
        // 检查每个点的碰撞
        for (int i = 0; i < startPoints.Count; i++)
        {
            var point = startPoints[i];

            var a = Physics2D.OverlapCircle(point, 0.01f , LM);
 
            if (a!=null)
            { 
                point.DraClirl(0.1f, Color.blue, 3f);
     
                results[i] = -1;
                hitColliders[i] = a;
            }
            else
            {
                //Debug.LogError(point);
                point.DraClirl(0.1f, Color.red, 3f);
                // 2. 向下发射射线
                RaycastHit2D R = Physics2D.Raycast(point, Vector2.down, FF, LM);
                Debug.DrawRay(point, Vector2.down* FF, Color.red, 3f);

                if (R.collider != null)  
                { 
                    R.point.DraClirl(0.1f,Color.red,3f);
                    // 特效位置记录为射线命中的点
                    startPoints[i] = R.point;
                    results[i] = 1;
                    hitColliders[i] = R.collider;
                }
                else
                {
                    results[i] = 0;
                }
            }

        }

        // 处理方向参数
        if (!FX)
        {
            startPoints.Reverse();
            results.Reverse(); // 当FX为false时反转结果顺序
            hitColliders.Reverse();
        }

        var da = 答案(results);

        if (da != -2)
        {
            // 打到了：先播放特效（使用命中点）
            特效_pool_2.I.GetPool(startPoints[da], T_N.特效圆跳);

            // 对命中的碰撞体执行操作（若存在）
            var col = hitColliders[da];
            if (col != null)
            {
 
                    Player3.I.圆斩对象?.Invoke(col.gameObject.GetInstanceID()); 
                // 示例：如果命中对象上有 单方面通过 组件，则触发它 

                // 你可以在这里加入其他需要对碰撞体执行的逻辑，例如：
                // - 发送消息：col.SendMessage("OnHitByCircleJump", SendMessageOptions.DontRequireReceiver);

            }

            // 返回命中点的 y 值（保持原有返回语义）
            return startPoints[da].y;
        }
        // 没打到
        else return -999;


    }
}
