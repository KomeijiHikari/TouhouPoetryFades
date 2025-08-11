using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEditor; 
using Cinemachine;


public class 相机框 : MonoBehaviour
{
    [DisplayOnly]
    public int 编号 = -9999;
    [DisplayOnly]
    [SerializeField]
    private bool 是我否1;
    [SerializeField]
    float Fov = 10;
    [DisplayOnly]
    [SerializeField]
    PolygonCollider2D 碰撞框;

    public PolygonCollider2D 碰撞框_ { get => 碰撞框; } 
    [DisplayOnly]
    [SerializeField]
    List<相机框> 运行的相机框显示;
    private static List<相机框> 运行的相机框static { get; set; } = new List<相机框>();

    public void 刷新正在运行的相机框列表(相机框   a)
    {
        var C = new List<相机框>();
        foreach (var item in 运行的相机框static)
        {
            if (item!=null)
            {
                C.Add(item);
            }
        } 
        if (!C.Contains(this))
        {
            C.Add(this); 
        }
        编号 = C.IndexOf(this );
      运行的相机框static = C; 
    }
    private void Start()
    { 
        Event_M.I.Add(Event_M.切换场景触发_obj,来这是我的碰撞箱); 
    }
void     来这是我的碰撞箱(GameObject  obj)
    {
        //一个场景有多个碰撞箱子就会被反复调用
        if (obj.scene != gameObject.scene) return;
        摄像机.I.设置相机碰撞体(碰撞框_);
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (上一个碰撞框_!=null&& (PolygonCollider2D)上一个碰撞框_ != 碰撞框_)
        {

            if (collision == Player3.I.po  )
            { 
                摄像机.I.设置相机碰撞体((PolygonCollider2D)上一个碰撞框_);
            }
        }

    } 

    private void OnTriggerEnter2D(Collider2D collision)
    {  //一半在A   一半在B    赋值B  然后回到A   ，相机框还是B
        if (collision == Player3.I.po  )
        {

            if (摄像机.I.当前碰撞框 != null)
            {
                if (摄像机.I.当前碰撞框.gameObject != gameObject)
                { 
                    上一个碰撞框_ = 摄像机.I.当前碰撞框;
                }
            }
            if (Time.frameCount <3)
            {
                刷新();
            }

            摄像机.I.Set_Target_Fov(Fov);
            摄像机.I.设置相机碰撞体(碰撞框_);
        }
    }
 
    public bool 是我否
    {
        get => 是我否1; set
        {
            if (value  == true)
            {
                刷新();
            }

            是我否1 = value;
        }
    }
    No_Re No=new No_Re ();
    public void 刷新()
    {
        if (No.Note_Re())
        {
      
            Initialize_Mono.I.重制触发?.Invoke(gameObject.scene.buildIndex, 编号);
            //Debug.LogError(gameObject.scene.buildIndex+"__"+ 编号);
        }
    }
   static Collider2D 上一个碰撞框_;
    private new void Awake()
    {
 
 
        碰撞框 = GetComponent<PolygonCollider2D>();
        刷新正在运行的相机框列表(this); 
    }

    private void Update()
    { 
           运行的相机框显示 = 运行的相机框static;
    }
}
