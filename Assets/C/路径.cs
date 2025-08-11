using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class NB方法:UnityEvent {  }




[Serializable]
public class 路径
{
    [SerializeField]
    private List<点> 巡逻点列表1;

    public bool 是否循环;
    [DisplayOnly]
    [SerializeField]
    private int 巡逻点表示1 = 0;
    [DisplayOnly]
    [SerializeField]
    public 点 当前点;
    public Action 退出 { get; set; }
    public Action 循环超出 { get; set; }
    protected int 巡逻点表示 { get => 巡逻点表示1; set => 巡逻点表示1 = value; }
    public List<点> 巡逻点列表 { get => 巡逻点列表1; set => 巡逻点列表1 = value; }

    public bool 乱序播放;
    public int 循环次数;
    public int 循环最大次数;
    public float 为空时生成两边巡逻点之间的距离=3f;

    public  virtual  void Enter(GameObject D)
    {       
        //外部调用

        if (循环最大次数==0)
        {
            循环最大次数 = 1100;
        }
        循环次数=0;
        var t = D. transform.localPosition;
        if (巡逻点列表.Count==0)
        {
            //没有巡逻点就在自己两边生成巡点 
            GameObject a, b;
            a = new GameObject("a");
            a.transform.localPosition = new Vector2(t.x- 为空时生成两边巡逻点之间的距离/2,t.y);
            b = new GameObject("b");
            b.transform.localPosition = new Vector2(t.x +为空时生成两边巡逻点之间的距离/2, t.y);
            巡逻点列表.Add(new 点 (a));
            巡逻点列表.Add(new 点(b));
            是否循环 = true;

            当前点 = 巡逻点列表[巡逻点表示];
        }
        else
        {
            巡逻点表示 = 0;
               当前点 = 巡逻点列表[巡逻点表示];
        }

    
    }
    
    public virtual 点 下一个()
    {
        当前点.Invoke();
        //外部调用

        if (!乱序播放)
        {
            if (巡逻点表示 + 1 >= 巡逻点列表.Count)
            {
                //走完了
                if (是否循环)
                {
                    巡逻点表示 = 0;

                    当前点 = 巡逻点列表[巡逻点表示];
                    循环次数++;
                    if (循环次数 >= 循环最大次数)
                    {
                        循环超出?.Invoke();
                    }
                    return 当前点;
                }
                //不是循环 
                退出?.Invoke();
                return null;
            }
            else
            {
                //没走完
                巡逻点表示++;
                当前点 = 巡逻点列表[巡逻点表示];
                return 当前点;
            }
        }
        else
        {  //乱序播放 
            System.Random random = new System.Random();
            int i = random.Next(0, 巡逻点列表.Count);
            巡逻点表示 = i;
            当前点= 巡逻点列表[巡逻点表示];
            return 当前点;
        }


    }
}
