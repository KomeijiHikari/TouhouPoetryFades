using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
 
public  abstract  class Pool_base : MonoBehaviour
{
    public SpriteRenderer sp { get; set; }

    protected Animator an;
    protected  Queue<特效模板管理> Q { get; set; } = new Queue<特效模板管理>();
    protected virtual void Awake()
    {
        an = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        初始化池子();
    }
    protected void   初始化池子()
    { 
        for (int i = 0; i < 200; i++) //初始化池子  （复制一个个体，并且是池子的子物体    取消激活，放回池子） 循环Count次
        {
            
                
            ReturnPool(初始化对象个体());
        }
    }
    public void ReturnPool(特效模板管理 obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.position = Vector2.zero;
        obj.transform.localScale=Vector2.one;
        obj.transform.SetParent(transform);
        Q.Enqueue(obj);
    }
protected 特效模板管理 GetPool()
    {
        if (Q.Count == 0)//对象池里的东西全部拿完了 
        {
            初始化池子();
        }
        特效模板管理 outobj = Q.Dequeue();
        outobj.gameObject.SetActive(true);
        return outobj;
    }

    protected abstract 特效模板管理 初始化对象个体();
    }

public class 特效_pool_2 : Pool_base
{  
    public static 特效_pool_2 I { get; private set; } 
    public List<特效模板> 列表;
    public float 残留时间 = 0.2f;
protected override void Awake()
    {
        base.Awake();
        if (I != null && I != this)     Destroy(this); 
        else     I = this;
       

    }
    protected override 特效模板管理 初始化对象个体()
    {
        GameObject obj;
        obj = new GameObject("特效");
        obj.transform.SetParent(transform);
        //obj.gameObject.AddComponent<速度颜色>();
        var a = obj.gameObject.AddComponent<特效模板管理>();
        a.初始化(an.runtimeAnimatorController, 列表);
        return a;
    }
    public bool Debug_;
    /// <summary>
    /// 获取池子
    /// </summary> 
    public 特效模板管理 GetPool(Vector2  召唤点, string 召唤名,bool   朝向= true, SpriteRenderer Tsp = null )
    {
        if (Debug_)  Debug.Log (召唤点 + "        点召唤      " + 召唤名+"   SP    :"+ Tsp);
        var a = GetPool();

        return a .Play(召唤点, 召唤名, 朝向, Tsp);
    }
    public 特效模板管理 GetPool(GameObject 召唤者, string 召唤名,SpriteRenderer s =null)
    {
        if (特效_pool_2 .I.Debug_ )   Debug.Log (召唤者+"        召唤      "+ 召唤名);
        var a = GetPool();
        a.Play(召唤者, 召唤名,s);
        return a;
    }
    [SerializeField]
    KeyCode K;
    [SerializeField ]
    string 测试;
    private void Update()
    {
        if (K!= KeyCode.None&& 测试!=null && Input.GetKeyDown(K))
        {
            GetPool(gameObject.transform .position , 测试); 
        }
    }
}


