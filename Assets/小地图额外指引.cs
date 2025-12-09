using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 小地图额外指引 : MonoBehaviour
{
 
    小地图显示 显示;

    生命周期管理 生命周期;
    [SerializeField ][DisplayOnly ]
    SpriteRenderer sp;

    bool 存在=true;
    private void Awake()
    {
        生命周期 = GetComponent<生命周期管理 >();
 
    }

    private void Update()
    {
        if (Player3.I.N_ . 地图道具解锁)
        {

        
        if (生命周期.当前==生命周期 .死亡)
        {
            存在 = false;
    
          sp.enabled = false;
            this.enabled = false;
        }
        else if(存在)
        {
            //显示.有东西 =((显示.当前1 == 显示.半透明) || 显示.当前1 == 显示.闪烁)&&大地图;
            // sp.enabled = (显示.当前1 == 显示.全显示) && 大地图;
     
        }
        }
    }
    private void Start()
    {
       var 相机 = transform.Get_摄像框(); 
        显示= 相机.GetComponent<在小地图上另外显示 >().Get_小弟().gameObject .GetComponent <小地图显示>();
        //显示.State_Action += 活;

        //Initialize_Mono.I.Waite(() =>
        //{
        //    NB(显示.当前1.StateName);
        //});

        生成obj();
    }
 
    bool 大地图 => 主UI.I.大地图.activeSelf;
 

    //private void NB(小地图显示.显示状态 obj)
    //{ 
    //    sp.enabled = obj == 小地图显示.显示状态.全显示;
    //    显示.有东西=obj == 小地图显示.显示状态.半透明; 
    //}

    void 生成obj()
    {
        var a = Surp_Pool.I.GetPool(Surp_Pool.地图白块);
        a.layer = Initialize.L_小地图;
        sp = a.GetComponent<SpriteRenderer>();
        sp.material = 材质管理.Get_Material(材质管理.Unli_Orther);
        sp.sortingLayerID = Initialize.S_小地图;
        sp.sortingOrder = 3;
        a.transform.position = transform.position;
        a.transform.localScale = Vector2.one * 6f;
        sp.material.SetColor(材质管理._SpriteColor, Initialize_Mono.I.搜集物品指示颜色);
        sp.enabled = false;
        这才是小地图 = a;
    }
    public GameObject 这才是小地图;
}
