using Enemmy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using 发射器空间;

public class 机关箭矢 : MonoBehaviour, I_暂停,I_Speed_Is
{
    监控激活碰撞框 J;
    MonoMager M;

    public bool 暂停 { get  ; set  ; }
    public float Speed_Lv { get => speed_Lv; set => speed_Lv = value; }

    List<Fly_Ground> Flist = new List<Fly_Ground>();
    private void Awake()
    {
        gameObject.组件<监控激活碰撞框>(ref J);
        gameObject.组件<MonoMager>(ref M); 

        J.是我 += (b) =>
        {
            if (!b)
            {
                // 当监控变为非激活时，清理所有跟踪的飞行物并回收到对象池
                while (Flist.Count != 0)
                {
                    for (int i = 0; i < Flist.Count; i++)
                    {
                        var fl = Flist[i];
                        if (fl == null) continue;
                        try
                        {
                            fl.销毁触发?.Invoke();
                        }
                        catch { }
                        Surp_Pool.I.ReturnPool(fl.gameObject);
                    }
                }
            }
            else
            {
                time = 间隔 - 0.01f;
            }
        };
    }
    public float 间隔=3;
    float time;
    public float 弹道speed=1;
    [SerializeField]
    private float speed_Lv=1;
    I_Speed_Is I => this;

    private void FixedUpdate()
    { 
        if (暂停) return;
        time += Time.fixedDeltaTime* I.固定等级差;
        if (time > 间隔)
        {
            time = 0;

            var a = Surp_Pool.I.GetPool(Surp_Pool.子弹);
            var b = a.GetComponent<Fly_Ground>();


            b.初始化(new Vector2(transform.localScale.x, 0), transform.position, 弹道speed*speed_Lv);

            var fg = b as Fly_Ground;
            if (fg != null)
            {
                Flist.Add(fg);
                // 当子弹被销毁时，从列表中移除引用
                Action handler = null;
                handler = () => { fg.销毁触发 -= handler; Flist.Remove(fg); };
                fg.销毁触发 += handler;
            }
        }
    }
 
}
