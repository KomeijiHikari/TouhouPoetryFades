using Schema.Builtin.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 圆斩跳 : MonoBehaviour
{
    [SerializeField ]
    BoxCollider2D Box;
    [SerializeField]
    BoxCollider2D 竖向判定Box;
    public float FF=> 竖向判定Box.bounds.size.y; 
   int   答案(List<int >  a)
    {
        if (a != null && a.Count >= 0)
        {
            for (int i = 0; i < a.Count; i++)
            {
                var I = a[i];
                if (I == 1)
                {
                    ///没有顶到，命中  返回索引   0和正数
                    return i;
                }
                else if (I == 0)
                {
                    ///没有顶到，没有命中，下一个
                    continue;
                }
                else  if(I == -1)
                {
                    ///只有可能是  -1
                    ///头顶到那就立即返回  负数索引
                    ///如果是0             返回-99
                    if (i == 0) return -99;
                      else      return -i;
                }
                else if (I==-2)
                {
                    ///默认值
                    Debug.LogError("怎么是-2");
                    return -999;
                }else
                {
                    Debug.LogError("神秘数字"+I);
                    return -999 ;
                }
            }
        }
        Debug.LogError("怎么走到这里的" );
        return -999;

    }
    Vector2 最小判定可以原批间距 = Vector2.one ;
    public Vector2 竖向(bool FX, Bounds B, int I, LayerMask LM)
    {
        ///纵向发射  从上往下遍历 原点朝向目标点发射
        ///碰撞点X坐标  空，有
        ///对比X坐标和自己距离有 远然后近 就记录 （ 可能有多组   （小于剔除）
        /// 从最远点   -0.1上下发射射线  储存 上下点相差的距离  和下方碰撞点的值  储存
        /// 比较最低点
        ///---------------------------------
        ///第一下失败之后遍历纵向点发射， 远然后近 就记录 （ 可能有多组   （小于剔除）
        ///从上往下的列表 
        ///立即发射
        List <float > 距离x=new List<float>();
        List<Vector2 > Poins=new List<Vector2>();
        List<Vector2> StartPoins = new List<Vector2>();
        List<int> res=new List<int>();
        float bottomX = B.center.x; 
        if (I < 1) Debug.LogError("I  不对劲");
        float spacing = I > 1 ? (B.max.y - B.min.y) / (I - 1) : 0;

        var 方向 = Vector2.right;
        if (!FX)  方向 *= -1;

        // 从上到下生成所有点
        for (int i = 0; i < I; i++)
        {
            float yPos = B.max.y - i * spacing;

            Vector2 dan = new Vector2( bottomX,yPos);

            dan.DraClirl(0.01f,Color.white, 3f);
            res.Add(-2);

            ///如果为空那么 就是  v2.zero
            Vector2 poin = Physics2D.Raycast(dan, 方向, Box.size.x, LM).point;

            StartPoins.Add(dan);
            Poins.Add(poin) ;
            距离x.Add(asd(poin));///记录距离
            Deb横向(dan, 3);
            //results.Add(-2); 
            //startPoints.Add(dan);
            //hitColliders.Add(null);
        }
        void Deb横向( Vector2 dan,float time)
        {

            Debug.DrawRay(dan, 方向 * Box.size.x, Color.black, time);
        }

        float asd(Vector2 v)
        {
            if (v==Vector2.zero)
            {
                return 99;
            }
            float juli = v.x - bottomX;
            if (!FX) juli *= -1;
            return juli;
        }
        for (int i = 0; i < 距离x.Count; i++)
        {
            if (i + 1 != Poins.Count)
            {
                if (距离x[i]> 距离x[i+1] + 最小判定可以原批间距.x)
                {

                    ///排除 下一个点是无碰撞的情况
                    if (距离x[i + 1]!=0)
                    {
                        if (距离x[i]==99)
                        {
                            return StartPoins[i];
                        }

                        Deb横向(StartPoins[i],4);
                        var 后退坐标X = Poins[i].x - 方向.x * 0.1f;
                        Vector2 Poin2 = new Vector2(后退坐标X, Poins[i].y);
                      Vector2 up=  Physics2D.Raycast(Poin2,Vector2.up,5, LM).point;
                        Vector2 down = Physics2D.Raycast(Poin2, Vector2.down, 5, LM).point;
                        ///应该不用判断空  否则太奇怪了
                        if (up.y- down.y > 最小判定可以原批间距.y)
                        {
                            Poin2.DraClirl(0.5f, Color.red, 5);
                            ///返回该点
                            return Poins[i];
                        } 
                    }
                }
            } 
        }

        return Vector2.zero;
    }
    public Vector2     发射()
    {
        bool 方向 = Player3.I.transform.localScale.x == 1;
        var a = Vector2.zero;

        a = GenerateRaycastResults(方向, Box.bounds, 9, Player3.I.碰撞检测层);
        if (cricleatk.容错原批判定)
            if (a.x == -1)
            {
           float y=       竖向(方向, 竖向判定Box.bounds, 9, Player3.I.碰撞检测层).y;
                a = GenerateRaycastResults(方向, Box.bounds, 9, Player3.I.碰撞检测层,y);
            }
        Player3.I.消弹(); 
        return a;
    }

    //-1 原点遮挡
    //0没碰到
    // 1 碰到了
    public Vector2  GenerateRaycastResults(bool FX, Bounds B, int I, LayerMask LM,float Y=0)
    { 
        List<int> results = new List<int>();
        List<Vector2> startPoints = new List<Vector2>();
        var Sp = Player3.I.transform.position;
        Vector2 MaxPoins=new Vector2(Sp.x, Sp.y-10);
        // 新增：保存对应的碰撞体（若有）
        List<Collider2D> hitColliders = new List<Collider2D>();

        float bottomY = B.min.y;
        if (Y != 0) bottomY = Y;
        // 在底边生成均匀分布的点
         
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
 
            ///默认为-2
            if (a!=null&&!a.isTrigger)
            { 
                ///头顶到了 -1
                point.DraClirl(0.1f, Color.blue, 3f);
     
                results[i] = -1;
                hitColliders[i] = a;
            }
            else
            {
                ///头没有顶到  
                point.DraClirl(0.1f, Color.red, 3f);
                // 2. 向下发射射线
                RaycastHit2D R = Physics2D.Raycast(point, Vector2.down, FF, LM);
                Debug.DrawRay(point, Vector2.down* FF, Color.red, 3f);

                if (R.collider != null)
                {           ///头没有顶到  命中 1
                    R.point.DraClirl(0.1f,Color.red,3f);
                    // 特效位置记录为射线命中的点 
                    if (R.point.y > MaxPoins.y)
                    {
                        MaxPoins = R.point;
                    }
                    startPoints[i] = R.point;
                    results[i] = 1;
                    hitColliders[i] = R.collider;

                    //if (R.collider != null)
                    //{
                    //    Debug.LogError(R.collider.gameObject.name + "   Player3.I.圆斩对象  ");
                    //    Player3.I.圆斩对象?.Invoke(R.collider.gameObject.GetInstanceID());
                    //    // 示例：如果命中对象上有 单方面通过 组件，则触发它   
                    //}
                }
                else
                {//头没有顶到  没有命中 
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
        if (da == -999)  Debug.LogError("?????????这不可能");
        if (da >= 0)
        {
            // 打到了：先播放特效（使用命中点）
           
                特效_pool_2.I.GetPool(MaxPoins, T_N.特效圆跳).Speed_Lv = Player3.Public_Const_Speed;
            // 对命中的碰撞体执行操作（若存在）

            var col = hitColliders[da];
            if (col != null)
            {
                Debug.LogError(col.gameObject.name + "   Player3.I.圆斩对象  ");
                Player3.I.圆斩对象?.Invoke(col.gameObject.GetInstanceID());
                // 示例：如果命中对象上有 单方面通过 组件，则触发它   
            }

            // 返回命中点的 y 值（保持原有返回语义）MaxPoins

            //return new Vector2(1, startPoints[da].y); 
            return new Vector2(1, MaxPoins.y);

        }
        else if (da != -999)
        {  /// 头顶到了 
           /// 挡住头功能去掉         
            if (false)
            {  
            Debug.LogError("AAAAAAAAAAAAAA");
            ///索引为0
            if (da==-99)
            {
                特效_pool_2.I.GetPool(startPoints[0], T_N.特效圆跳失败).Speed_Lv = Player3.Public_Const_Speed;
                return new Vector2(-1, startPoints[0].y);
            }
            else if(da<0)
            {   
                特效_pool_2.I.GetPool(startPoints[-da], T_N.特效圆跳失败).Speed_Lv = Player3.Public_Const_Speed;
                return new Vector2(-1, startPoints[-da].y);
            }
            else
            {
                Debug.LogError("不应该到这里");
                  return new Vector2(-1,0);
            }
            }
            return Vector2.left;
        }
        // 没打到 或者默认
        else return Vector2.zero;

        ///再次判定 的情况是负数的情况
    }
}
