
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObject/生物数据")]//可以在鼠标右单击 创建 找到  可以创建一New Item类的文件
public class item : ScriptableObject//数据本地化
{
    public string itemName;//物体名称
    public Sprite itemImage;//物体图片
    public int 物品数量; //物体数量
    [TextArea]//文字可以显示多行
    public string itemInfo;//物体描述
    public E_物品类型 物品类型;

}
public    enum E_物品类型
{
    消耗品,
    /// <summary>
    /// 重要物品
    /// </summary>
    任务物品,
    能力槽
}