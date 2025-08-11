using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treevalue : MonoBehaviour
{
    public List<Transform> T列表;
    public List<Vector2> 目标列表 = new List<Vector2>();
    private void Awake()
    {
        目标列表 .Clear();
        for (int i = 0; i < T列表.Count; i++)
        {
            目标列表.Add( T列表[i].position);
        }
    }
}
