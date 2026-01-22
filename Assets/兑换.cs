 using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;  
namespace ItemMager
{ 

public class 兑换 : MonoBehaviour
{  
    [Button]
    public void 试一下(string s)
    {
        item.返回(s)?.Invoke();
    }
    public  Text_button_Father UI分量集合;
    Text_button[] 子类按钮列表;
    货物子单元[]  货物子单元shuzu;
    public Text 介绍;

    public List<CompleteItem> 可以售卖的;
   public  GameObject 预制体;
     
    private void Start()
        {
            Debug.LogError("AAAAAAAAAAAAAAAAAAAAAAAAAAA");
            if (所有物品管理.I == null) return;
            /// 仓库那边准备好了
            可以售卖的 = 所有物品管理.I.商店的;

        子类按钮列表 = UI分量集合.子类按钮列表;
        货物子单元shuzu=new 货物子单元[子类按钮列表.Length];
        for (int i = 0; i < 子类按钮列表.Length; i++)
        {
            var a = 子类按钮列表[i];
            Debug.Log(a.name);
            货物子单元shuzu[i]=a.gameObject.GetComponent<货物子单元>();
            a.MoveAction += Move;
           //a.MoveAction += (MoveDirection m) => {
           //     Initialize_Mono.I.Waite(() => { Move(m); }, 0.001f, true);
           // };

            //a.MoveAction += (MoveDirection m) => {
            //    Initialize_Mono.I.Waite(() => { Move(m); }, 0.001f, true);   };
        }
        刷新();
            ISsTART = true;
    }
        bool ISsTART;
    void Move(MoveDirection m)
    { 

        if (m==MoveDirection.Up&&当前索引==0)
        {
            格索引--;
            刷新(); 
        }
        else if(m==MoveDirection.Down && 当前索引 ==3)
        {
            格索引++;
            刷新(); 
        }
        当前索引 = 索引();

            int I = Mathf.Clamp(当前索引 + 格索引,0, 可以售卖的 .Count- 1); 
        介绍.text = 可以售卖的[I].itemInfo;
    }
    public int 格索引;
    private void OnEnable()
    {
        if (消息.I != null)
        {
            消息.I.ClearMessages();
        }
            if (ISsTART)
            {
                刷新();
            }
            else
            {
                Initialize_Mono.I.Waite(() => {
                    
                    刷新();
                
                });
            }
   
        }
    void  刷新()
    {
        格索引 = Mathf.Clamp(格索引, 0, 可以售卖的.Count - 子类按钮列表.Length);
 
        for (int i = 0; i < 货物子单元shuzu.Length; i++)
        {
            //+格索引
            var a = 货物子单元shuzu[i];
                if (i + 格索引< 可以售卖的.Count)
                { 
                    a.刷新(可以售卖的[i + 格索引]);
                } 
        } 
    }
    public int 当前索引; 
    int 索引()
    {
               for (int i = 0; i < 子类按钮列表.Length; i++)
        { 
            var a = 子类按钮列表[i];
            if (a.选中)
            { 
                return i;
            }
        }
        return -1;

    }

}
}
