
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class invontoryManager : MonoBehaviour
{
    public static invontoryManager I { get; private set; }

    public invontory 我的背包;
    public GameObject  单元格父类;//在什么地方生成单元格
    public GameObject 被复制的单元格;//空单元格    必须包含solt脚本

    public Text 共享文本信息;

    [DisplayOnly]
    public List<GameObject> 临时单元格列表 = new List<GameObject>();

    private void Awake()
    {
        if (I != null)
        {
            Destroy(this);
        }
        I = this;
    }
    private void OnEnable()    
    {
        RefreshItem();

/*        I.共享文本信息.text = "";*/// 后调用了，导致第一个物品信息不显示
    }

private void Start()
    {
   
    }
    public static void UpdateItemInfo(string itemDescription)
    {
        Debug.LogError("调用" + itemDescription);
        I.共享文本信息.text = itemDescription;

        Debug.LogError("试一下" + I.共享文本信息.text);
    }
    //更新UI数据
    public static void RefreshItem()
    {
        for (int i = 0; i < I.单元格父类.transform.childCount; i++) //遍历soltGrid所有子集
        {
            if (I.单元格父类.transform.childCount == 0)
            {
                break;
            }
            //DestroyImmediate(I.单元格父类.transform.GetChild(i).gameObject);//毁灭所有子集    
            //I.单元格父类.transform.GetChild(i).gameObject.GetComponent <Text_button_Father >().Target  = null;
            Destroy(I.单元格父类.transform.GetChild(i).gameObject);//毁灭所有子集    
        }
        Debug.LogError("销毁一次了");
        for (int i = 0; i < I.我的背包.itemList.Count; i++)
        {
            I.临时单元格列表.Add(Instantiate(I.被复制的单元格));//将  单元格 增加到   单元格列表  当中
            var a = I.临时单元格列表[i].GetComponent<背包格子>();
            I.临时单元格列表[i].transform.SetParent(I.单元格父类.transform, false );//solts类表放在soltGrid下 成为子集       //不保留原先大小的缩放

            //I.单元格父类.ADD( I.临时单元格列表[i].GetComponent<背包专用按钮>());

            a.SetupSlot(I.我的背包.itemList[i]);//    背包格子丢入item单元
            a.soltID = i;//将solts类 ID序号 赋予 背包的序号
        }
        //I.单元格父类.添加完成调用(); 

        I.临时单元格列表.Clear();//清空列表
    } 
} 