using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static UnityEngine.UI.Selectable;

public class 假选择 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    , IPointerExitHandler
//IPointerEnterHandler      就是不要悬浮
{
    //响应鼠标事件         
    //响应移动 
 


    // Colors used for a color tint-based transition.
    [FormerlySerializedAs("colors")]
    [SerializeField]
    private ColorBlock m_Colors = ColorBlock.defaultColorBlock;

    [SerializeField]
    Image image;

 
 
    No_select_father father;

    bool 已经引用了;
    void 获取引用()
    {
        if (已经引用了  ) return;
        已经引用了 = true;
        image.color = m_Colors.normalColor;
        father = transform.parent.GetComponent<No_select_father>();
    }
    private void Awake()
    {
        获取引用();
    }
    #region   鼠标事件
    private void LateUpdate()
    {
        在我这里点击 = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        在我这里点击 = true;
        Select();
    }
    bool 在我这里点击 { get; set; }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (father.当前选中 == this)
        {
            image.color = m_Colors.selectedColor;
        }
        else
        {
            回弹();
        }

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (father.当前选中 != this)
        {
            点击回弹();
        }
        else
        {
            选中();
        }
    }
    public void 触发()
    {
        //image.color = m_Colors.pressedColor;    //鼠标效果
    }
    #endregion
    private void Update()
    {
        if (Input.GetButtonDown(Initialize.Enter ))
        {
            if (father.当前选中 == this)
            {
                image.color = m_Colors.normalColor;
            }
        }
    }
    public void Select()
    {
        获取引用();
        if (!gameObject .activeSelf||!gameObject.activeInHierarchy)
        {
            Debug.LogError(gameObject +"未激活");
            return;
        }
        if (father.当前选中 == this) return;

 

        if (father.当前选中 != null) father.当前选中.回弹(); 
        father.当前选中 = this;
        father.当前选中.选中();
        Initialize_Mono.I.Debug_(this.GetType(),"选中了"+gameObject .name );

        if (在我这里点击)  触发(); 

    }
    public void 点击回弹()
    {
        image.color = m_Colors.normalColor;
    }
    public  void  回弹()
    {
        image.color = m_Colors.normalColor;
        //上个数组的回退
        //var a=father.
        //     father.返回当前数组(     father.当前索引    -当前方向                       ).回退     
        father.返回当前数组(this).回退();
    }
    public  void   选中()
    { //点击响应 
            image.color = m_Colors.selectedColor; 
        father.返回当前数组(this).被展开(  );
    }

    //[SerializeField]
    //NB方法 是个方法;

    //Text_button_Father [];
 



}
