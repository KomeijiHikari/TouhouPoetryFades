using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class 背包专用按钮 : My_Button
    , ISelectHandler//    设置当前的时候    
    , IDeselectHandler,                  //    失去选中
    IMoveHandler//选中   WASD
                //,ISubmitHandler//选中   按下ENTER 
{



    public    Vector2 坐标;
    [DisplayOnly]
    [SerializeField]
  NB他爹  组合爹;
    背包格子 格子;
   new  void    Start()
    {
        base .Start();
        格子 = GetComponent<背包格子>();
        组合爹 = transform.parent.GetComponent<NB他爹>();
 
    }
     
    public override void OnMove(AxisEventData eventData)
    {
        Debug.LogError("调用");
        组合爹.OnMove(eventData,this);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        Start();
 
            格子.显示文本信息();
 

    }

    public override void 真退出(BaseEventData eventData)
    {
        base.真退出(eventData);
    }
}
