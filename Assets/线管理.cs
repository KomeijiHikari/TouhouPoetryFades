using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

//[ExecuteInEditMode]
public class 线管理 : MonoBehaviour
{
    LineRenderer LR;

    [DisplayOnly] public List<Vector3> Vs=new List<Vector3>();
    private void Awake()
    {
        gameObject.组件(ref LR);
        LR.material = new Material(Shader.Find("Sprites/Default"));
    }
    private void Start()
    {
        刷新();
        
    }

    private void OnDisable()
    {
        if (LR!=null)
        {
            LR.enabled = false;
        }
    }
    public float 宽度=0.2f; 
     
      void 刷新()
    {

        if (LR == null) Awake();
        if (LR == null) return;
        LR.enabled = true;

        LR.startWidth = 宽度;
        LR.endWidth = 宽度;
        // 获取所有子物体
        int 子物体数量 = transform.childCount;

        if (子物体数量 == 0)
        {
            Debug.LogWarning("没有子物体可以连接");
            LR.positionCount = 0;
            return;
        }

        // 设置LineRenderer的点数
        LR.positionCount = 子物体数量;

        Vs.Clear();
        // 将所有子物体的位置添加到LineRenderer
        for (int i = 0; i < 子物体数量; i++)
        {
            Vector3 子物体 = transform.GetChild(i).position;
            Vs.Add(子物体);
            LR.SetPosition(i, 子物体 );
        } 
    } 
}
