using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surp_Pool : SerializedMonoBehaviour
{
    public static string 白块 { get => "白块"; }
    public static string 下坠刺 { get => "下坠刺"; }
    public static string 能量子弹 { get => "能量子弹"; }
    public static string 子弹 { get => "子弹"; } 
    public static Surp_Pool I { get; private set; }
   [DictionaryDrawerSettings]
    public Dictionary<string, GameObject> 池子字典; 
    Dictionary<string, Queue<GameObject>> 池子字典_  = new Dictionary<string, Queue<GameObject>>();

    public int 数量 = 200;
    private void Awake()
    {
        if (I != null && I != this)    Destroy(this); 
        else        I = this; 

        foreach (var item in 池子字典)
        {
            string s = item.Key;
            Queue<GameObject> q = new Queue<GameObject>();
            池子字典_.Add(s, q);
            初始化池子(s);
        }

        //Message();
    } 
    void Message()
    {
        string messg = null;
        foreach (var item in 池子字典)
        {
            messg += "\n大池子是：" + item.Key + "数量为 ：" + 池子字典_[item.Key].Count + "                   ";
            foreach (var obj in 池子字典_[item.Key])
            {
                messg += "\n" + obj.name;
            }
        }
        Debug.LogError(messg);
    }
    protected void 初始化池子(string 那个池子)
    {
        //string messg=null;
        //messg = messg + "\n" + a.name + "回到了=>        " + 那个池子;
        //Debug.LogError(messg);
        for (int i = 0; i < 数量; i++) //初始化池子  （复制一个个体，并且是池子的子物体    取消激活，放回池子） 循环Count次
            {
            var a = 初始化对象个体(那个池子);

                ReturnPool(a, 那个池子);
            }

    } 
    public GameObject GetValue(string 哪一个池子)
    {   
        return 池子字典[哪一个池子];
    }
public  GameObject GetPool(string 哪一个池子)
    {
        bool BB = false;
        if (池子字典_[哪一个池子].Count==0)
        {
            BB = true;
            初始化池子(哪一个池子); 
        } 

        GameObject outobj = 池子字典_[哪一个池子].Dequeue(); 
        if (outobj.activeInHierarchy)
        {
            ///出来的OBJ有几率 是 已经出来的（API：Dequeue 出来并且删除  没删除）
            return GetPool( 哪一个池子);
        }        
        outobj.SetActive(true); 
        return outobj;
    } 
    public void ReturnPool(GameObject obj,string 哪一个池子=null)
    {
        //Debug.LogError(" ReturnPool(GameObject obj,string 哪一个池子=null                )"  +obj.name);
   var a= obj.GetComponent<I_ReturnPool>();
        if (a==null)
        {
            Debug.LogWarning("池子单元 对象接口而口为空");
        }
        else
        {
            a.重制();
            if (哪一个池子 == null && a.Pool_Key_name == null) Debug.LogWarning("没法玩了，都是空");

                if (哪一个池子 ==null)  ///子物体结束调用
            {
                哪一个池子 = a.Pool_Key_name;
            }
            else  ///初始化时调用
            {
                a.Pool_Key_name = 哪一个池子;
            } 
                 
        }
        obj.SetActive(false);
        obj.transform.position = Vector2.zero;
        obj.transform.localScale = Vector2.one;
        obj.transform.SetParent(transform);
        池子字典_[哪一个池子].Enqueue(obj);
    }
    protected  GameObject 初始化对象个体(string s)
    {
       var a= Instantiate(池子字典[s]);
        return a;
    }
    private void Update()
    {
        //if (Input .GetKeyDown(KeyCode.Y))
        //{
        //    //GetPool("墙");
        //}
    }
}
interface  I_ReturnPool
{
    string Pool_Key_name { get; set; }
    void     重制();
}