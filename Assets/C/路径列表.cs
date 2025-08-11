using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 

public class 路径列表 : MonoBehaviour
{
    BiologyBase e;



 
    public List<路径> 路径列表_;

    [DisplayOnly]
    public 路径 当前执行的路径;
    [DisplayOnly]
    public int 下标_;
    public int 下标 { get => 下标_; set => 下标_ = value; }

    [DisplayOnly]
    private 点 目标1;
    public 点 目标 { get => 目标1; set => 目标1 = value; }
    public float 巡逻点判断范围 = 3;


    public Action<点> 到达目标点Enter { get => 到达目标点Enter1; set => 到达目标点Enter1 = value; }

    private Action<点> 到达目标点Enter1;

  public  void Awake()
    {
        e = GetComponent<BiologyBase>();
        if (巡逻点判断范围 == 0)
        {
            Debug.LogWarning("这个" + gameObject + "的巡逻点检测范围未设置");
            巡逻点判断范围 = 3;
        }
        if (路径列表_ == null)
        {
            Debug.LogError("巡逻点列表为空");
        }
        else
        {
            下标 = 0;
            Enter_(路径列表_[下标]);
        }

    }
    public void Enter_(路径 a)
    { 
        if (当前执行的路径 != null)
        { 
            当前执行的路径.退出 -= 切换;
            当前执行的路径.循环超出 -= 循环超出;
        } 
        当前执行的路径 = a;
        当前执行的路径.Enter(this.gameObject);
        当前执行的路径.循环超出 += 循环超出;
        当前执行的路径.退出 += 切换;
        目标 = 当前执行的路径.当前点;
    }

    private void 循环超出()
    {
        Debug.LogError("ASDASDASDASDASD");
        下标 = 0;
        Enter_(路径列表_[下标]);
    }

    private void 切换()
    {
        if (下标 + 1 >= 路径列表_.Count)
        {
            //所有路径走完了
            当前执行的路径.是否循环 = true;
            目标 = 当前执行的路径.下一个();
        }
        else
        { 
            Enter_(路径列表_[下标 + 1]);
            下标++;
        }

    }

    public void moving()
    {
        if (目标.obj == this.gameObject)
        {
            到达目标点Enter.Invoke(目标 );
            Debug.LogError("到终点到终点乐乐了");
            return;
        } 
        目标 = 当前执行的路径.当前点;
        if (Initialize.接近_判断A减B的绝对距离是否小于等于_范围(this.gameObject, 目标.obj , 巡逻点判断范围, false))
        { 
            到达目标点Enter.Invoke(目标 );
            目标 = 当前执行的路径.下一个();
        }
        else
        {
           e. 向目标水平移动(目标.obj);
        }

    }

}
