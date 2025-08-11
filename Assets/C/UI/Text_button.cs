using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Text_button : Selectable, ISubmitHandler
{
    [SerializeField]
 public NB方法  Enter;
    [SerializeField]
    public NB方法 EnterSelect;
    [SerializeField]
    public NB方法 EnterExit;
    public override void OnPointerDown(PointerEventData eventData)//鼠标按下
    {
     
        //if(  Application.platform== RuntimePlatform.WindowsEditor) 
    }
    public override void OnPointerEnter(PointerEventData eventData)//鼠标进入范围
    { 
    }
    public override void OnPointerExit(PointerEventData eventData) { }
    public override void OnPointerUp(PointerEventData eventData) { }

    void Navigate(AxisEventData eventData, Selectable sel)
    {
        if (sel != null && sel.IsActive())
        {
            eventData.selectedObject = sel.gameObject;
        base.OnDeselect(eventData);
            选中=false;
            Initialize_Mono.I.Debug_(this.GetType(), "取消了" + gameObject.name);
 
        } 
    }
    private void Update()
    { 
        if (EventSystem.current!=null)
        { 
            //Initialize_Mono.I.Debug_(this.GetType(), EventSystem.current.currentSelectedGameObject);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (选中 )
            {
                Select();

                //StartCoroutine(Initialize.等一帧执行方法(() => Select()));
 
            }
        }
        //if (EventSystem.current != null)
        //{
        // if(   EventSystem.current.currentSelectedGameObject==null )
        //    {
        //        if (选中)
        //        {                                                                                                                                           //他妈的
        //            base.Select();
        //            Debug.LogError("                                                                            我去  调用了                                  ");
        //        }
        //    }
        //}

        }

 
    public override void OnMove(AxisEventData eventData)
    {
        base.OnMove(eventData);

        switch (eventData.moveDir)
        {
            case MoveDirection.Right:
                Navigate(eventData, FindSelectableOnRight());
                break;

            case MoveDirection.Up:
                Navigate(eventData, FindSelectableOnUp());
                break;

            case MoveDirection.Left:
                Navigate(eventData, FindSelectableOnLeft());
                break;

            case MoveDirection.Down:
                Navigate(eventData, FindSelectableOnDown());
                break;
        }
        //base.OnDeselect(eventData);    因为移动到边缘在移动，当前SELE  没有被改变但是调用所以要判断        要不要用                      
        //旁边没有就不用取消
    }
    public override void OnDeselect(BaseEventData eventData) { }// 因为鼠标点击所以  必须重写

    [SerializeField]
  bool 选中1=false ;

    public bool 选中 { get => 选中1;
        set
        {
            if (选中1&&!value)
            {
                退出后要做的();
            }
            else if (!选中1 && value)
            {
                选中后要做的();
            }
           选中1=value;
        }
    }
    protected  virtual void  选中后要做的()
    {
        EnterSelect?.Invoke();
    }
    protected virtual void 退出后要做的()
    {
        EnterExit?.Invoke();
    }
    public override void OnSelect(BaseEventData eventData)//菜单初创立的时候       当前菜单已经转移但是     Sekect不会调用该方法
    { 
        base.OnSelect(eventData);
        选中 = true; 
    }

    protected override void OnDestroy()
    {
        Text_button_Father f=null;
        Transform Tf = transform ;
        int i=0;
        while (f==null)
        {
            Tf = Tf .parent;
               f = Tf.GetComponent<Text_button_Father>();
            i++;
            if (i > 10) break;
        }
        if (f!=null)
        {
            f.我被干掉力(this);
        } 
    }
    public override void Select()
    {

        //if (EventSystem.current == null) return;
        if (this==null)
        { 
            return;
        }
        base.Select();
        选中 = true;//正常选择的时候不会调用该方法   所以还是要true


        //StartCoroutine(Initialize.Waite(() => 
        //{
        //    if (选中)
        //    {
        //        if (EventSystem .current .currentSelectedGameObject !=this.gameObject )
        //        {
        //            Debug.LogError("触发");
        //            base.Select();
        //        }
        //    }


        //}
        //    ));
        //StartCoroutine(asdasd(0.1f));
    }
    private IEnumerator asdasd(float  time)
    {
        yield return new WaitForSecondsRealtime(time);
        if (EventSystem.current.currentSelectedGameObject != this.gameObject)
        {
            base.Select();
        }
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

        DoStateTransition(currentSelectionState, false);//返回原先
 
    }
    //bool 可被交互;
    //public override bool IsInteractable()
    //{
    //    return 可被交互; 
    //}
    public  void  OnSubmit(BaseEventData eventData)
    {
        Enter?.Invoke(); 
    }
   
}
