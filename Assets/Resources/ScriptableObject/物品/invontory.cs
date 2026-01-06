
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player_input;
using static Player3;


[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObject/New Invontory")]
public class invontory : ScriptableObject
{
    public List<item> itemList = new List<item>();
    public string Name { get => "能力背包"; }
    public List<item> 读取能力()
    { 
        List<item> 能力=new List<item>();

        var N_ = Initialize.GetFieldDictionary(Player3.I.N_); 
        foreach (var item in itemList)
        {
            if (item.物品类型==E_物品类型.能力)
            {
                if (N_.ContainsKey(item.itemName))
                {
                    bool A =(bool) N_[item.itemName];
                    if (A)
                    {
                        能力.Add(item);
                    } 

                }
              
            }
        }   
        return 能力; 
    }
}
 