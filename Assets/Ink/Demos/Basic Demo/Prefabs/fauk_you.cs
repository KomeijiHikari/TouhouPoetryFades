using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;



public class My_Button : Selectable, IPointerClickHandler, IEventSystemHandler, ISubmitHandler,ICancelHandler
{
    [DisplayOnly]   [SerializeField ]   bool 被选中1;
    //[SerializeField]
    //NB方法 退出方法;
    [SerializeField]
    NB方法 MY;
    protected 选择爹 爹;

    public     bool 被选中 { get { 
            return 被选中1; 
        } set {
            if (被选中1&&!value)
            {
                取消();
            }
            if (!被选中1 && value)
            {
                选中();
            }
            被选中1 = value; } }

    
   protected  virtual   void  取消()   {  }
    protected virtual void 选中()  {   }
    protected override void Start()
    {
        爹 = transform.parent.GetComponent<选择爹>();
    }
    private void Update()
    {
        if (EventSystem.current != null)
        {
            Debug.Log(EventSystem.current?.currentSelectedGameObject);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(重新获取());
        }

    }
    public override void OnPointerDown(PointerEventData eventData)//鼠标按下
    {
      //if(  Application.platform== RuntimePlatform.WindowsEditor) 
    }
    public override void OnPointerEnter(PointerEventData eventData)//鼠标进入范围
    { 
 
    }
    public  override void OnPointerExit(PointerEventData eventData) {  }
    public override void OnPointerUp(PointerEventData eventData) { }
    public   void OnPointerClick(PointerEventData eventData) { }//继承BUTTon的花这个要   over

    public   void OnSubmit(BaseEventData eventData)//继承BUTTon的花这个要   over
    {
        DoStateTransition(SelectionState.Pressed , false);
        StartCoroutine(OnFinishSubmit());
            MY?.Invoke();
    }
    private IEnumerator OnFinishSubmit()   //奥NB   不愧是button
    {
        var fadeTime = colors.fadeDuration;
        var elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        DoStateTransition(currentSelectionState, false);
    }
    public void OnCancel(BaseEventData eventData)
    {
        var a = transform.parent.GetComponent<选择爹>();
 
        a.回退方法?.Invoke(); 
    }
    public override void OnDeselect(BaseEventData eventData) { }
    public IEnumerator 重新获取()
    {
        yield return null;
        //Initialize.获取同级物体(gameObject);
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (爹==null)
            {
                爹 = GetComponent<选择爹 >();
            }
            if (爹.last == null)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
                //第一次进来
            }
            else
            {

                EventSystem.current.SetSelectedGameObject(爹.last);
            }
        }
    }
 
    protected override void OnDestroy()
    {
        if (EventSystem.current == null) return;
        EventSystem.current.SetSelectedGameObject(null);
    }
 
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        被选中 = true;
        if (爹!=null)
        {
            爹.last = gameObject;
        }

    }
    public override void OnMove(AxisEventData eventData)
    {
        bool 假退出 = false;
        if (navigation.selectOnLeft == null)
        {
            if (eventData.moveDir == MoveDirection.Left)
            {
                假退出 = true;
            }
        }
        if (navigation.selectOnRight == null)
        {
            if (eventData.moveDir == MoveDirection.Right)
            {
                假退出 = true;
            }
        }
        if (navigation.selectOnDown == null)
        {
            if (eventData.moveDir == MoveDirection.Down)
            {
                假退出 = true;
            }
        }
        if (navigation.selectOnUp == null)
        {
            if (eventData.moveDir == MoveDirection.Up)
            {
                假退出 = true;
            }
        }

        if (!假退出)
        {

            真退出(eventData);
        }
        base.OnMove(eventData);


    }
    public virtual void 真退出(BaseEventData eventData)
    {
        被选中 = false;
        base.OnDeselect(eventData);
    }


}
public class fauk_you : My_Button
{
    public Text My;
    int Text_Size;

    Image 底子;
    float 底子透明度;

    protected override void Awake()
    {
        My = GetComponent<Text>();
        if (My != null)
        {
            Text_Size = My.fontSize;
        }
        底子 = GetComponentInChildren<Image>();

        底子透明度 = 0.5f;
        底子.color = new Color(底子.color.r, 底子.color.g, 底子.color.b, 0f);
    }
    protected override void Start()
    {
        base.Start(); 
    }

    protected override void 取消()
    {
        选中(false );
    }

    protected override void 选中()
    {
        选中(true);
    } 
    protected override void OnEnable()
    {
        base.OnEnable();
        去你妈的();
        选中(被选中);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        去你妈的();
                选中(被选中);
    } 
    void 去你妈的()
    {
        if (底子 == null) return;
        var Z = 底子.GetComponent<Canvas>();
        if (Z == null) return;
        Z.sortingOrder = Z.sortingOrder;
    }
    public void 选中(bool boo)
    {
        去你妈的();
        //去你妈的();
        if (boo)
        {

            //if (我被选中力) return;

            var a = 底子.color;
            底子.color = new Color(a.r, a.g, a.b, 底子透明度);

            My.fontSize = (int)(Text_Size * 1.5);
            Debug.Log("BBBBBBBBBBBBBBBBBBBBBBBBBBBBB"+gameObject .name);
            //我被选中力 = boo;
        }
        else
        {
            //if (!我被选中力) return;

            var a = 底子.color;
            //底子.DOFade(0, 0.2f);
            底子.color = new Color(a.r, a.g, a.b, 0);
            My.fontSize = Text_Size;
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAA" + gameObject.name);
            //我被选中力 = boo;
        }
    } 

}
