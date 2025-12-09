using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using 流程控制;
using static Player3;

public class 提示管理 : MonoBehaviour
{
    public bool 可以;
    public List<玩家能力.E_玩家能力> 提示s;
 

    public bool 或()
    {
        foreach (var item in 提示s)
        {
            if (asd(item))
            {
                return true;
            }
        }
        return false;
    }
    public bool 并()
    {
        foreach (var item in 提示s)
        {
            if (!asd(item))
            {
                return false;
            }
        }
        return true;
    }
    bool asd(玩家能力.E_玩家能力 a)
    {
        if (Player3.I == null || Player3.I.N_ == null) return false;
        switch (a)
        {
            case 玩家能力.E_玩家能力.墙冲浪:
                return Player3.I.N_.墙冲浪;
            case 玩家能力.E_玩家能力.上升攻击:
                return Player3.I.N_.上升攻击;
            case 玩家能力.E_玩家能力.空中Dash:
                return Player3.I.N_.空中Dash;
            case 玩家能力.E_玩家能力.时缓:
                return Player3.I.N_.时缓;
            case 玩家能力.E_玩家能力.圆劈:
                return Player3.I.N_.圆劈; 
            default:
                return false;
        }
    }
    void asdasd( )
    {
        可以 = 或();
        gameObject.SetActive(可以); 
    }
    private void Awake()
    {
        流程控制 .流程控制.I.提示List.Add(this);
Event_M.I.Add(Event_M.刷新提示机关, asdasd);
    }
    private void Start()
    {
        asdasd();
    }
}
