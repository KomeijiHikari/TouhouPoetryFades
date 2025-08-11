using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_Father_base : MonoBehaviour
{
    [Tooltip("如果目标是空就             默认是父类  如果零号,就是自己，  ")]
    /// <summary>
    /// //如果目标是空就             默认是父类  如果零号,就是自己，  
    /// </summary>
    public GameObject Target;
public    virtual void 被回退() { }
    public virtual void 被展开() { }
}
 
public class Text_button_Father : Button_Father_base
//三种情况，1  战斗菜单调用0号激活   2    不是从0号激活    作为子菜单存在     3   开始界面，一开始就有UI
{ 
    [SerializeField]
    E_展开情景 展开情景;
    public E_展开情景 展开情景1 { get => 展开情景; set => 展开情景 = value; }
    Text_button[] 子类按钮列表;
 
 

    bool 初始化过 { get; set; }

    /// <summary>
    /// 不是第一次调用也要重新获取焦点
    /// 为被激活状态获取的时候也会调用，因此
    /// </summary>
    void 获取引用()
    {//如果目标是空就             默认是父类  如果零号,就是自己，  
 
        if (初始化过) return;
            初始化过 = true;
            if (Target == null)
            {
                Target = transform.parent.gameObject;
                if (展开情景1 == E_展开情景.零号菜单) Target = gameObject;
                if (展开情景1 == E_展开情景.平级菜单) Target = gameObject;

            } 
        子类按钮列表 = GetComponentsInChildren<Text_button>();
        if (子类按钮列表!=null&& 子类按钮列表.Length>0)
        {
            for (int i = 0; i < 子类按钮列表.Length ; )
            {
                if (子类按钮列表[i].interactable )
                {
                    子类按钮列表[i].Select();
                    break;
                }
                else
                {
                    i++;
                    if (i > 100) break;
                }
            }
        }
      
    } 
 
    private void OnEnable()
    {
        子类按钮列表 = GetComponentsInChildren<Text_button>();
        if (!初始化过) return;

        //找到最后一个数字索引
        重新获得焦点();
    }

    /// <summary>
    /// 被0号展开    先之先        未激活前就会被调用   先是展开    然后OnE   然后是？？？？start  和aweak？
    /// 
    ///   由0 对象调用   调用后要说明自己是1号 
    ///  一号开关    如果已经打开就返回    ，关闭由回退关闭
    ///  
    /// 有返回是因为该对象被激活后，战斗菜单依旧会调用该方法，需要在这里停下
    /// </summary>
    public  override   void 被展开()           //背包    假按钮打开之后，这个obj也会打开                   因此不会被调用
    { 
        获取引用();
        switch (展开情景1)
        {
            case E_展开情景.平级菜单:
                Target.SetActive(true);
 
                break;
            case E_展开情景.零号菜单://开始场景展开
                break; 
            case E_展开情景.一号菜单://战斗场景展开
                Target.SetActive(true);
                break;
        }
        if (展开情景1== E_展开情景.一号菜单)
        {
            if (Target.activeInHierarchy)
            {

            }
                if (Target .activeSelf )
            {

            }
        }
        Initialize_Mono.I.Debug_(this.GetType(), gameObject + "被展开展开触发完毕         当前展开场景是" + 展开情景1
            +"     自己开关是"+ Target.activeSelf
            +"    面板开关是"+ Target.activeInHierarchy); 
        重新获得焦点();
    }

 
 
    public void 我被干掉力(Text_button target)
    {
        if (!gameObject .activeInHierarchy ) return;
        StartCoroutine(Initialize.Waite(() =>
        {
            子类按钮列表 = GetComponentsInChildren<Text_button>();
            子类按钮列表[0].Select();
        }));
    }
  public   enum E_展开情景
    {
        Defaul,   //某菜单的子菜单展开
        零号菜单,   //开始游戏就是激活的

        一号菜单      //战斗场景从  没有菜单情况  激活的   战斗情况赋值
 


        ,平级菜单 //被平级展开
    }
 
    void   Start()
    {   
 
        被展开();
    }

    bool 开始游戏后执行过了;
    private void OnValidate()
    {
        if (!Application.isPlaying) return;
        if (开始游戏后执行过了) return;
        开始游戏后执行过了 = true; 
        //未激活情况初次加载？
        if (gameObject .activeSelf&& gameObject.activeInHierarchy)     
        {//我是加载场景后默认true的
            展开情景1 = E_展开情景.零号菜单; 
        } 
    }
    
    [DisplayOnly]
    [SerializeField]
  public   Button_Father_base 上一个;




    /// <summary>
    /// 作为子菜单展开，获取回退对象
    /// </summary>
    /// <param name="上一个"></param>
    public void 被展开(Button_Father_base 上一个)
    {
        获取引用();

        //直接调用setActive       如果不是第一次调用也会失去焦点X    0对象会重复调用     子菜单开启，自己不开启有也会调用X 
        //杯展开和被回退保持只有一个是激活的X

        this.上一个 = 上一个;
        上一个.Target.SetActive(false );
        Initialize_Mono.I.Debug_(this.GetType(), "被展开" + gameObject.name);
 
        Target.SetActive(true);
        重新获得焦点();

    }
    
    void 重新获得焦点()
    { 
        foreach (var item in 子类按钮列表)
        {
            if (item.选中&& item!=null&& item.interactable )
            {
                Initialize_Mono.I.Debug_(this.GetType(), gameObject + "重新获得焦点准备设置悬着"  );
                StartCoroutine(Initialize.Waite(()=> 
                {
                    item.Select(); 
                })); 
                break;
            } 
        }
 
    }
    /// <summary>
    /// 被同类东西    调用
    /// </summary>
 public   override    void  被回退()
    {
        Target.SetActive(true ); 
        重新获得焦点();
 
    }
    public void  回退()   //按钮和   自身都会响应
    {
        Action_回退?.Invoke();
        switch (展开情景1)
        {
            case E_展开情景.平级菜单:
                Target.SetActive(false);
                break;
            case E_展开情景.Defaul:
                          上一个.被回退();
                Target.SetActive(false);
                break;
            case E_展开情景.零号菜单:

                break;
            case E_展开情景.一号菜单:
                 Event_M.I.Invoke(Event_M.UI回到战斗);
                Target.SetActive(false);
                break; 
        } 
        Initialize_Mono.I.Debug_(this.GetType(), gameObject +"回退触发当前展开场景是"+ 展开情景1);
    }
    public Action Action_回退;
    private void Update()
    {
        if (Input.GetButtonDown(Initialize.Exite))
        {
            回退();
        }
    } 
}
  