using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static 生命周期管理;

public class 小地图的门 : MonoBehaviour
{
    [DisplayOnly ]
    [SerializeField ]
    int 编号A=-1;
    [DisplayOnly]
    [SerializeField]
    int 编号B = -1;
    SpriteRenderer sp;

    void 获取()
    {

        var a = sp.bounds.碰撞列表(1 << Initialize.L_小地图);
        if (a != null && a.Length == 2)
        {
            foreach (var item in a)
            {
                if (item.collider.gameObject != gameObject)
                {
                    var CC = item.collider.gameObject.GetComponent<相机框>();
                    //Debug.LogError(CC.编号);
                    if (编号A == -1)
                    {
                        编号A = CC.编号;
                    }
                    else
                    {
                        编号B = CC.编号;
                    }

                }
            }
        }
        else
        {
            Debug.LogError("碰到的碰撞框  数量为" + a.Length+gameObject .name+transform .position );
        }
        if (DeadPla.I.对比(transform.position))
        {
            有我 = true;
        }
        选择();
    }
    void Start()
    {
        sp = GetComponent<SpriteRenderer >();
        Initialize_Mono.I.重制触发 += 进入框框;

        获取();
        //Initialize_Mono.I.Waite(() => {



        //}); 
    }

    [SerializeField]
    [DisplayOnly]
    bool 有我;
    [SerializeField]
    [DisplayOnly]
    bool 开启;

    public bool 开启1 { get => 开启; set {
            if (开启 != value )
            {
                开启 = value;
                if (value )
                {
                    if (!有我)
                    {
                        DeadPla.I.Add(transform.position);
                        有我 = true; 
                    } 
                }
                选择();
            }
        } }
    void  选择()
    {
        if (!有我 )
        {
            sp.enabled = false;
        }
        else
        {
            sp.enabled = true ;
            if (开启1)
            {
                sp.color = sp.color.Set_Alpha(1); 
            }
            else
            {
                sp.color = sp.color.Set_Alpha(0.8f);
            }
        }
    }
    private void 进入框框(int arg1, int arg2)
    {
        if (arg1 != gameObject.scene.buildIndex) return;

        if (arg2 == 编号A|| arg2 == 编号B)
        {
            开启1 = true;
        }
        else
        {
            开启1 = false;
        }
    }
}
