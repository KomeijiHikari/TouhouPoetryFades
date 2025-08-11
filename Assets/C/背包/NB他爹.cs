using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NB他爹 : 选择爹
{
 
    [DisplayOnly]
    public List<背包专用按钮> NB列表;
    GridLayoutGroup GLG;
    private void Awake()
    {
        GLG = GetComponent<GridLayoutGroup>();
    }
    //private void Start()
    //{
    //    GLG = GetComponent<GridLayoutGroup>();
    //    刷新();
    //}
  //protected  override void OnEnable()
  //  {
  //      base.OnEnable();

  //      刷新();
  //      //if (GLG!=null)
  //      //{
  //      //    if (NB列表.Count!=0)
  //      //    {
  //      //        Start();
  //      //    }
  //      //}
  //  }
 
    //protected override void OnDisable()
    //{
    //    last = null;
    //    for (int i = 0; i < transform.childCount; i++)
    //    {
    //        Destroy( transform.GetChild(i).gameObject);//毁灭所有子集    
    //    }
    //}

 
  public  void 添加完成调用()
    { 
        当前坐标 = new Vector2(1, 1);

        if (NB列表.Count>0)
        {
            if (NB列表[0]!=null)
            {
                last = NB列表[0].gameObject;
            }

        }
        else
        {
            Debug.LogError("翻车了");
        } 
        if (EventSystem.current!=null)
        {
            EventSystem.current.SetSelectedGameObject(last);
        }

    }

 
    [DisplayOnly]
    [SerializeField]
    Vector2 当前坐标;
     
    public void OnMove(AxisEventData eventData,背包专用按钮 a)
    {
        //被调用，先检测坐标有没有超出索引
        var 下一个坐标 = new Vector2(当前坐标.x + eventData.moveVector.x, 当前坐标.y - eventData.moveVector.y);
     var 下一个索引=   Initialize.转换出去(下一个坐标, GLG.constraintCount);
        下一个坐标 = Initialize.转换进去(下一个索引, GLG.constraintCount);
        var 当前索引 = Initialize.转换出去(当前坐标, GLG.constraintCount);
        if (下一个索引 >= NB列表.Count||下一个索引 < 0) return; 
        if (eventData.moveVector.y == 0 && Mathf .Abs ( 当前坐标.x - 下一个坐标.x)!=1) return;
 
        NB列表[当前索引].真退出(eventData);
        //返回(当前坐标).真退出(eventData);
        //当前坐标.x += eventData.moveVector.x;
        //当前坐标.y -= eventData.moveVector.y;

        当前坐标 = 下一个坐标;
        EventSystem.current.SetSelectedGameObject(NB列表[下一个索引].gameObject);
    }
  public   void ADD(背包专用按钮 nB )
    {
        if (!NB列表.Contains(nB))
        { 
            NB列表.Add(nB);
            var I = NB列表.IndexOf(nB);
            nB.坐标 = Initialize.转换进去(I, GLG.constraintCount);
        }
    }
 
}
