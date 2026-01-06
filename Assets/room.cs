using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class room : MonoBehaviour
{
 public CompositeCollider2D T;
    private void Start()
    {
        T= transform.GetComponentInChildren<Grid>().GetComponentInChildren<Tilemap>().GetComponent<CompositeCollider2D>();
        顶点 = GetAllCompositeColliderVertices(T);

        Bounds b = T.bounds;
        b.Dra(Color.red,10);
        for (int i = 0; i < 顶点.Count; i++)
        {
            var P = 顶点[i];
            for (int j = 0;j < P.Count; j++)
            {
                var vx= P[j]; 
                bool B = false;
         
                //if (B) vx.DraClirl(3,Color.white,3);
                //if (B) Debug.LogError("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB");
            }
        }
    }
 

    List<List<Vector2>>顶点 = new List<List<Vector2>>();
    public static List<List<Vector2>> GetAllCompositeColliderVertices(CompositeCollider2D compositeCollider)
    {
        // 初始化返回结果（外层=路径，内层=该路径下的顶点）
        List<List<Vector2>> allPathVertices = new List<List<Vector2>>();

        // 校验入参是否有效
        if (compositeCollider == null)
        {
            Debug.LogError("传入的CompositeCollider2D为null！");
            return allPathVertices;
        }

        // 1. 获取CompositeCollider2D的总路径数
        int totalPathCount = compositeCollider.pathCount;

        // 2. 遍历每一条路径
        for (int pathIndex = 0; pathIndex < totalPathCount; pathIndex++)
        {
            // 3. 获取当前路径的顶点总数
            int vertexCount = compositeCollider.GetPathPointCount(pathIndex);

            // 4. 创建数组存储当前路径的本地顶点数据
            Vector2[] localVerticesArray = new Vector2[vertexCount];

            // 5. 填充本地顶点数据
            compositeCollider.GetPath(pathIndex, localVerticesArray);

            // 6. 初始化当前路径的顶点列表（存储世界坐标）
            List<Vector2> currentPathWorldVertices = new List<Vector2>();

            // 7. 遍历当前路径的所有顶点，转换为世界坐标并存储
            for (int vertexIndex = 0; vertexIndex < vertexCount; vertexIndex++)
            {
                // 本地坐标 → 世界坐标（关键：基于CompositeCollider2D所在对象的Transform转换）
                Vector2 worldPos = compositeCollider.transform.TransformPoint(localVerticesArray[vertexIndex]);
                currentPathWorldVertices.Add(worldPos);
            }

            // 8. 将当前路径的顶点列表添加到总结果中
            allPathVertices.Add(currentPathWorldVertices);
        }

        return allPathVertices;
    }
}
