using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using 流程控制;
using static Player3;

public interface I_BOOL
{
    public bool 好嘛();
    public bool 好 { get; set; }
    public bool 反转 { get; set; }
}
public class 提示管理 : MonoBehaviour, I_BOOL
{
    public bool 反转 { get => 反转1; set => 反转1 = value; }
    public bool 好 { get => 好1; set => 好1 = value; }
    [Tooltip("没有")]
    public List<玩家能力.E_玩家能力> 没有提示s;
    [Tooltip("有")]
    public List<玩家能力.E_玩家能力> 提示s;
    [Tooltip("   或")]
    [SerializeField]
    private bool 好1;

    public bool Deb;
    [SerializeField]
    private bool 反转1;
     
    bool 或(List<玩家能力.E_玩家能力> E,bool Bb=false)
        {
        bool B= false;
        if(E==null||E.Count==0)
        {
            return true;
        }
        foreach (var item in E)
        {
            bool ASD = Shi_Fou_You_zhe_ge_nengli(item);
            if(Bb) ASD=!ASD;
            if (ASD)
            {
                B = true;
                break;
            }
        }


        return B; 
        }
    //public bool 并()
    //{
    //    foreach (var item in 提示s)
    //    {
    //        if (!asd(item))
    //        {
    //            return false;
    //        }
    //    }
    //    return true;
    //}
    bool Shi_Fou_You_zhe_ge_nengli(玩家能力.E_玩家能力 a)
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
   public  bool  好嘛()
    {
        if (Deb)
        {
            Debug.LogError(或(没有提示s)+ "          public  bool  好嘛()      " + 或(提示s)+gameObject.name+transform.position);
        }
        好 =  或(没有提示s,true)  && 或(提示s) ;
        好 = 反转 ? !好 : 好;
        return  好;
    }
}
