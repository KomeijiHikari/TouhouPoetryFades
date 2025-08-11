
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;//事件系统 包含鼠标事件

public class itemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler//检测UI
{
    public Transform originalParent;
    public invontory MyBag;
    public int currentItemID;//当前物品ID；
    public void OnBeginDrag(PointerEventData eventData)//鼠标位置到物品上才触发
    {
        originalParent = transform.parent;//获得原始父集
        currentItemID = originalParent.GetComponent<背包格子>().soltID;
        transform.SetParent(transform.parent.parent);//解决图层掩盖问题  将他设置为父级同辈
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;//不能被鼠标射线检测到
    }

    public void OnDrag(PointerEventData eventData)
    {

        transform.position = eventData.position;
        //  Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);//控制台显示 鼠标 射线 获得物品 的名字
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            if (eventData.pointerCurrentRaycast.gameObject.name == "item Image")//有物品 实现物品交换
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);//父子集关系 与背包有关
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;//图像位置

                //itemList的物品存储位置改变
                var temp = MyBag.itemList[currentItemID];
                MyBag.itemList[currentItemID] = MyBag.itemList[eventData.pointerPressRaycast.gameObject.GetComponentInParent<背包格子>().soltID];
                MyBag.itemList[eventData.pointerPressRaycast.gameObject.GetComponentInParent<背包格子>().soltID] = temp;

                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
            if (eventData.pointerCurrentRaycast.gameObject.name == "slot(Clone)")//没物品
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
                GetComponent<CanvasGroup>().blocksRaycasts = true;

                MyBag.itemList[eventData.pointerPressRaycast.gameObject.GetComponentInParent<背包格子>().soltID] = MyBag.itemList[currentItemID];
                if (eventData.pointerPressRaycast.gameObject.GetComponentInParent<背包格子>().soltID != currentItemID)//避免原地放 导致物品从背包消失
                {
                    MyBag.itemList[currentItemID] = null;
                }
                return;
            }
        }
        //其他任何位置都归位
        transform.SetParent(originalParent);
        transform.position = originalParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}