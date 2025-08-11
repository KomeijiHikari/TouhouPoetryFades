using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class 特效_pool : MonoBehaviour
{
     string 路径 = "Assets/texiao/texiao.controller";
    Animator an;
    public RuntimeAnimatorController  AC;
    Queue<GameObject> Q { get; set; } = new Queue<GameObject>();
    public static 特效_pool I { get; private set; }


    public float 残留时间 = 0.2f;
    private void Awake()
    { 
        an = GetComponent<Animator>(); 
        an.runtimeAnimatorController = AC;
        if (I != null && I != this)
        {
            Destroy(this);
        }
        else
        {
            I = this;
        }
        初始化池子l();
    }

    void 初始化池子l()  //初始化池子  （复制一个残影，并且是池子的子物体    取消激活，放回池子） 循环Count次
    {
        for (int i = 0; i < 200; i++)
        {
            GameObject obj;
            obj = new GameObject("特效");
            obj.transform.SetParent(transform);
         var a=   obj.gameObject.AddComponent<特效>();
            a.初始化(AC);
            ReturnPool(obj);
        }
    }

    private void Update()
    {

    }
    /// <summary>
    /// 丢回池子
    /// </summary>
    public void ReturnPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        Q.Enqueue(obj);
    }
    public  void  GetPool(GameObject 召唤者,string 召唤名 )
    { 
        if (Q.Count == 0)//对象池里的东西全部拿完了 
        {
            初始化池子l();
        }
        GameObject outobj = Q.Dequeue();
        outobj.SetActive(true);
        outobj.GetComponent<特效>().开播(召唤者, 召唤名);
    }
}


