using SampleFSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
public class 强制战斗 :MonoBehaviour
{
    enum E_状态
    {
        默认,
        正在,
        完成
    }
    [SerializeField] E_状态 e;
    /// <summary>
    ///外部引用生命周期 接收生命周期的  是否复活存档
    /// </summary>
    监控激活碰撞框 j;
    [SerializeField] 生命周期管理 s;
    private void Awake()
    {
        gameObject.组件(ref j);
        set(阻碍List, false);
        set(捷径List, false);
        e = E_状态.默认;

        j.是我 += (bool b) => {
            刷新();
        }; 
    }
    [Button]
    public void 刷新()
    {
        if (s.在死亡笔记里面() || s.当前 == s.死亡)
        {
            完成了();
        }
        else 
        {
            e = E_状态.默认;
        } 

    }
    /// <summary>
    /// 外部unity事件调用
    /// 
    ///进入游戏后被生命周期调用或者死亡后被调用
    ///
    /// 或者进出摄像机调用 //刷新
    /// </summary>
    [Button]
    public void 完成了()
    {
        if(Deb)
        {
            Debug.LogError("完成了"+gameObject);
        }
        e = E_状态.完成;
    }
    public bool Deb;
    [Button]
    public void 触发()
    {
        e = E_状态.正在;
    }
    public List<GameObject> 阻碍List;
    public List<GameObject> 捷径List;

    public bool 阻碍设置;
    public bool 捷径设置;

      bool 阻碍b_;
      bool 捷径b_;

    void set(List<GameObject> gs,bool b   )
    {
        for (int i = 0; i < gs.Count; i++)
        {
            gs[i].SetActive( b );

        }
    }
    void 正在()
    {
        捷径设置 = false;
        阻碍设置= true;
    }
    void 默认()
    {
        捷径设置 = false;
        阻碍设置 = false;
    }
    void 完成()
    {
        阻碍设置 = false;
        捷径设置 = true;
    }
    private void Update()
    {
        switch (e)
        {
            case E_状态.默认:
                默认();
                break;
            case E_状态.正在:
                正在();
                break;
            case E_状态.完成:
                完成();
                break; 
        }
        if (阻碍设置!= 阻碍b_)
        {
            阻碍b_ = 阻碍设置;
            set(阻碍List , 阻碍b_);
        }

        if (捷径b_!= 捷径设置)
        {
            捷径b_ = 捷径设置;
            set(捷径List, 捷径b_);
            
        }

    }

}
