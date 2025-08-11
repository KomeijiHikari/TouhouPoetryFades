using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace Boss
{ 

public class 魔炮管理 : MonoBehaviour
{
    public static 魔炮管理 I;
  [SerializeField ]  GameObject 父类监控;
   [SerializeField ] GameObject 子类监控;

    [Header("魔炮")]
    [SerializeField] GameObject 父类监控1;
    [SerializeField] GameObject 子类监控1;
    [SerializeField] GameObject 父类监控2;
    [SerializeField] GameObject 子类监控2;
    [SerializeField] GameObject 父类监控3;
    [SerializeField] GameObject 子类监控3;

    [Header("星星")]
    [SerializeField] GameObject 星星1; 
    [SerializeField] GameObject 星星2; 
    [SerializeField] GameObject 星星3;

    [SerializeField] List<Phy_检测> 检测s; 
    private void Awake()
    {
        if (I != null && I != this) Destroy(this);
        else I = this;
    }
    bool fb
    {
        get
        {
            return 父类监控.activeInHierarchy;
        }
    }

    bool zb
    {
        get
        {
            return 子类监控.activeInHierarchy;
        }
    }

    public bool Fb { get => fb1; set {
            if (fb1 !=  value)
            {
                fb1 = value;
                大(value );
                    } 
        } }
    public bool Zb { get => zb1; set
        { 
            if (zb1 !=   value)
            {
                zb1 = value;
                小(value );
            }
        }
       }
    [SerializeField]
    private bool fb1;
    [SerializeField] private bool zb1;

    /// <summary> 
    /// 失效
    ///  打到了
    ///  没打到
    /// </summary>
   public  int  大炮打到玩家了=0;

    public bool 监控大炮 = false;
    private void Update()
    {
        Fb = fb;
        Zb = zb;


 
        foreach (var item in 检测s)
        {
            if (item.isActiveAndEnabled)
            {
                if (item.遇见了)
                {
                        魔理沙.I.伤害玩家一下("大炮");
                        Player3.I.被扣血(1,Boss.魔理沙.I .gameObject,0);

                    if (监控大炮 && 大炮打到玩家了==0)
                    {
                        大炮打到玩家了 =1;
                        Debug.LogError("大炮打到玩家了");
                    }
                }  
            }
        }     
    }
    void 大(bool b)
    {
        if (是星星)
        { 
            if (序号 == 1)
            {
                星星1.SetActive(b);
            }
            else if (序号 == 2)
            {
                星星2.SetActive(b);
            }
            else
            {
                星星3.SetActive(b);
            }
        }
        else
        {
            if (序号 == 1)
            {
                父类监控1.SetActive(b);
            }
            else if (序号 == 2)
            {
                父类监控2.SetActive(b);
            }
            else
            {
                父类监控3.SetActive(b);
            }
        } 
    }
    void 小(bool b)
    {
     ///星星和序号在发射前
        if (序号 == 1)
        {
            子类监控1.SetActive(b);
        }
        else if (序号 == 2)
        {
            子类监控2.SetActive(b);
        }
        else
        {
            子类监控3.SetActive(b); 
        }

        if (序号 == 3 && !是星星)
        {//是大炮
            if (b)
            {
                大炮打到玩家了 =0;
                监控大炮 =true;
            }
            else
            {
                蘑菇管理.I.全部销毁();
                if (大炮打到玩家了 == 0)
                {
                    大炮打到玩家了 = -1;
                    魔理沙.I.广播体操 = 1;
                    Debug.LogError("大炮没有打到玩家 ");  
                }
                else if (大炮打到玩家了 == 1)
                {
                    魔理沙.I.笨蛋玩家大炮++;
                }
                监控大炮 =false;
            }
        }
        else
        {
            监控大炮 = false;
        }


    }
    public bool 是星星;
    public int 序号; 
}

}
