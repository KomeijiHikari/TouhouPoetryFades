using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 痕迹 : MonoBehaviour
{
    public static 痕迹 I;
    public static string 痕迹Name { get; set; } = "痕迹";
    Action asd => Player3.I.生命归零;
    GameObject 死亡图片预制体;

    public 路径点 L;
    List<Vector3> List => L.路径点List;
    bool IsNull => L == null || List == null;

    public int Count;

    [Serializable]
    public class 路径点
    {
        public List<Vector3> 路径点List;
    }

    private void Awake()
    {
        if (I != null && I != this) Destroy(this);
        else I = this;
    }

    public void Load()
    {
        L = Save_D.Load_Value_D<路径点>(痕迹Name, true);
        if (L == null)
        {
            L = new 路径点();
        }

        if (L.路径点List == null)
        {
            L.路径点List = new List<Vector3>();
        }
    }

    /// <summary>
    /// 不应该走D 因为生命周期
    /// </summary>
    public void Save()
    {
        if (IsNull) return;
        Save_D.Add(痕迹.痕迹Name, 痕迹.I.L, true);
    }

    public float 间隔时间 = 9f;
    float t_;

    private void Update()
    {
        if (IsNull) return;
        Count = List.Count;

        // 每间隔时间尝试添加一个点
        if (Time.time > t_ + 间隔时间)
        {
            t_ = Time.time; // 重置计时器
            Vector3 pos = new Vector3(
                Player3.I.transform.position.x,
                Player3.I.transform.position.y,
                0
            );

            // 功能1：只有与上一个点的距离超过6时才添加
            if (List.Count == 0 || Vector3.Distance(pos, List[List.Count - 1]) > 6f)
            {
                List.Add(pos);
            }
        }
    }

    // 功能2：死亡时保存，并将所有点的Z坐标标记为1
    public void DeathSave()
    {
        if (IsNull) return;

        // 将列表中所有点的Z坐标设置为1
        for (int i = 0; i < List.Count; i++)
        {
            Vector3 v = List[i];
            v.z = 1f;
            List[i] = v;
        }

        Save(); // 保存修改后的路径点
    }
}
