
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemOnWord : MonoBehaviour
{
    public item thisItem;//物体类型
    
  public  invontory playerInvontory;//去到背包

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AddNewItem();
        }
    }
 
    //背包增加物品
    public void AddNewItem()
    {
         
        if (!playerInvontory.itemList.Contains(thisItem))//判断物品不包含在列表当中
        {
            for (int i = 0; i < playerInvontory.itemList.Count; i++)//寻找空列表项
            {
                if (playerInvontory.itemList[i] == null)
                {
                    playerInvontory.itemList[i] = thisItem;
                    break;
                }
            }


        }
        else
        {
            thisItem.物品数量 += 1;
        }
        //invontoryManager.RefreshItem();
        Destroy(gameObject);
    }

}