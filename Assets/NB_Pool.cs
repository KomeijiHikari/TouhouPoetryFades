using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// /已废弃
/// </summary>
[DefaultExecutionOrder(100)]
public class NB_Pool :MonoBehaviour
{
  
    public static  NB_Pool I { get; private set; }
    protected  void Awake()
    {
 
        if (I != null && I != this)
        {
            Destroy(this);
        }
        else
        {
            I = this;
        }
        初始化池子();
    }
    public void ReturnPool(GameObject  obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.position = Vector2.zero;
        obj.transform.localScale = Vector2.one;
        obj.transform.SetParent(transform);
        Q.Enqueue(obj);
    }
    protected Queue<GameObject > Q { get; set; } = new Queue<GameObject>();
    protected void 初始化池子()
    {
        for (int i = 0; i < 200; i++) //初始化池子  （复制一个个体，并且是池子的子物体    取消激活，放回池子） 循环Count次
        {
            ReturnPool(初始化对象个体());
        }
    }
    protected GameObject GetPool()
    {
        if (Q.Count == 0)//对象池里的东西全部拿完了 
        {
            初始化池子();
        }
        GameObject outobj = Q.Dequeue();
        outobj.gameObject.SetActive(true);
        return outobj;
    }

    public void GetPool(Vector2 召唤点,Vector2  方向,float speed )
    {
        Debug.LogError("发射了发射了");
        var a = GetPool().GetComponent<Fly_Ground>();
        a.transform.position = 召唤点;
 

    }
    protected   GameObject 初始化对象个体()
    {
        GameObject obj;
        obj = new GameObject("子弹");
        obj.transform.SetParent(transform);
       var f= obj.AddComponent<Fly_Ground>(); 

        return obj;
    }
}
