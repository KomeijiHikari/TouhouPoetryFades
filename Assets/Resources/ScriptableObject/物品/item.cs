

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObject/生物数据")]//可以在鼠标右单击 创建 找到  可以创建一New Item类的文件
public class item : ScriptableObject//数据本地化
{
    /// <summary>
    ///静态数据   只记录初始数量选项 
    /// </summary>
    public string itemName => name;//物体名称
    public Sprite itemImage;//物体图片 
    public int 物品数量 = 1; //物体数量 
    public int 物品价值; //物品价值
    [TextArea]//文字可以显示多行
    public string itemInfo;//物体描述
    //public E_物品类型 物品类型; 
    [NonSerialized] public Action 方法;

    static Dictionary<String, Action > ItemAction = new Dictionary<String, Action >()
    {

        { "快速攻击", (  ) =>
        {
      Player3.I.玩家数值.攻击速度加成 =0.1f;
        }},
        { "冲刺加速", ( ) =>
        {
            Player3.I.N_.Dash加速 = true;
        }},
        { "灵活攻击", ( ) =>
        {
            Player3.I.N_.攻击打断 = true;
        }},        
        { "生命提升", ( ) =>
        {
            Player3.I.玩家数值.Max_Hp++;
        }},        
 
        { "无限原批", ( ) =>
        {
            Player3.I.N_.无限圆劈 = true;
        }},        
        { "移动速度提升", (  ) =>
        {
            Player3.I.玩家数值.常态速度 = 8.5f;
        }},
              
        { "墙上冲浪", ( ) =>
        {
            Player3.I.N_.墙冲浪 = true;
        }},

          { "空中冲刺", ( ) =>
        {
            Player3.I.N_.Dash加速 = true;
        }},
    };
    public static Action asd = () => { 
        Debug.LogError("没有找到对应的方法"); 
    };
    public static Action 返回(string s)
    {
      bool N=  ItemAction.TryGetValue(s, out Action Out); 
        if (!N)
        {
            Debug.LogError("没有找到对应的方法" + s);
            return asd;   
        }

        return Out;
    }
}
public    enum E_物品类型
{
    消耗品,
    /// <summary>
    /// 重要物品
    /// </summary>
    任务物品,
    能力
}