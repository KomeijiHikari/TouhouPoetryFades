using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class 背包格子 : MonoBehaviour//放在预制体上显示信息
{
    [DisplayOnly]
 public     item soltItem;
    public Image soltImage;
    public Text soltText;
    [DisplayOnly]
    public string 文本信息;
    [DisplayOnly]
    public int soltID;//物品ID

    //public GameObject itemInSolt;

    public void 显示文本信息()//鼠标点击事件
    {
        Debug.LogError("调用"+ 文本信息);
        invontoryManager.UpdateItemInfo(文本信息);//显示文本信息
    }
    public void SetupSlot(item items)   //接受物品信息   东西放在碗里面
    {
        //if (items == null)                         不知道这是个啥
        //{   
        //    itemInSolt.SetActive(false);
        //    return;
        //}

        soltImage.sprite = items.itemImage;
        soltText.text = items.物品数量.ToString();

        soltItem = items; 
        文本信息 = items.itemInfo;
    }
} 