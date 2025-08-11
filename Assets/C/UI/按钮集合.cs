using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Trisibo;
using UnityEngine.Events;

public class 选择爹 : MonoBehaviour
{
    [SerializeField]
 public    NB方法  回退方法;
    public GameObject last;

  public  List<选择爹> 所有集合_1;
       static List<选择爹> 所有集合 { get; set; }

    public List<选择爹> 所有集合_ {
        get
        {
            所有集合_1= 所有集合;
            return 所有集合;
        }
        set
        {
            所有集合_1 = 所有集合;
            所有集合 = value;
        }
    }

    void ADD(选择爹 a)
    {
        if (所有集合_ == null)
        {
            REmove();
        }
        if (所有集合_.Contains(a)) return;
        //foreach (var item in 所有集合_)
        //{
        //    Debug.LogError(item.gameObject.name + "添加");
        //}
        Debug.LogError("添加");
        所有集合_.Add(a);
    }

 

    protected  virtual   void OnEnable()//会在未加载场景，Start  之前调用            //战斗切回主场景       会先添加，然后触发导致   添加失败
    {//启用的时候将自己添加，如果有上一个将上一个关闭  

        if (Initialize.所有的场景都加载完了嘛() )
        {
            ADD(this);
        }
        else
        { 
 

            Initialize_Mono.I.等待一帧执行方法_检测原obj是否启用(
                gameObject,
                () => {
                    ADD(this);
                }
            );
        }

 
      var i= 所有集合_.IndexOf(this);
        if (i > 0)          //不能return   否则子类不会调用
        {
            if (所有集合_[i - 1] != null)
            { 
                所有集合_[i - 1].gameObject.SetActive(false);
            } 
        } 
    }
    private void OnDestroy()
    {
        REmove();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
 public    static   void REmove()
    {
        //Debug.LogError("清理了");
 

        所有集合 = new List<选择爹>(); 
    }
    public void ASDASD(string  s)
    {
        Debug.LogError(s);
    }
 
    public void Back()
    {
        int i = 所有集合_.IndexOf(this);
        foreach (var item in 所有集合_)
        {
            Debug.LogError (item.name+"准备移除      索引是"+i);
        }

        if (所有集合_.Count>1)  所有集合_.Remove(this); 

        if (i <= 0) return;
        所有集合_[i - 1].gameObject.SetActive(true);
      transform .parent.gameObject .SetActive(false);

    }

}
public class 按钮集合 : 选择爹
{

 public    List<GameObject> OBJ集合=new List<GameObject> ();
 
 IEnumerator  首位相接()
    {
        yield return null;
        for (int i = 0; i < OBJ集合.Count; i++)
        {
            var a = OBJ集合[i].GetComponent<fauk_you >();
             if (i==0)
            {
                Navigation NEW = new Navigation ();
                NEW.mode = Navigation.Mode.Explicit;
                NEW.selectOnUp = OBJ集合[OBJ集合.Count - 1].GetComponent<Selectable>();
                NEW.selectOnDown = OBJ集合[i+1].GetComponent<Selectable>();

 
                a.navigation =NEW ;
            }
            else     if (i == OBJ集合.Count-1)
            {
                Navigation NEW = new Navigation
                {
                    mode = Navigation.Mode.Explicit,
                    selectOnUp = OBJ集合[i - 1].GetComponent<Selectable>(),
                    selectOnDown = OBJ集合[0].GetComponent<Selectable>()
                };

                a.navigation = NEW;
            }
            else 
            { 
                Navigation NEW = new Navigation();
                NEW.mode = Navigation.Mode.Explicit;
                NEW.selectOnUp = OBJ集合[i - 1].GetComponent<Selectable>();
                NEW.selectOnDown = OBJ集合[i + 1].GetComponent<Selectable>();


                a.navigation = NEW;
            }
        }
    }
    public  void   ADD(GameObject a)
    {
        if (!OBJ集合 .Contains (a))
        {
            OBJ集合.Add(a); 
        }
        if (a.transform.parent!=gameObject)
        {
            a.transform.SetParent(transform);
        }

    }
 
    private void Start()
    {
        if (EventSystem.current != null) 
            if (OBJ集合.Count >= 1)
                if (last == null)
               last=OBJ集合[0];
 
    }



    protected override  void OnEnable()
    { 
        base.OnEnable(); 
        foreach (Transform child in transform)
        {
            ADD(child.gameObject);
        } 
        if (EventSystem.current != null)
        {
            if (OBJ集合.Count >= 1)
            { 
                if (last ==null)
                { 
 
                    StartCoroutine(一帧设置(OBJ集合[0]));
                   
                }
                else
                {
                    //StartCoroutine(
                    // last.GetComponent<My_Button>().重新获取()
                    // );

                    StartCoroutine(一帧设置(last)); 
                }
            }
        }
        StartCoroutine(首位相接());
    }
 IEnumerator 一帧设置(GameObject  a)   //新的按钮集合激活  OnEnle函数调用设置当前OBJ后    仍然会丢失，并且不会激活OnSele 函数
    {
        yield return null;
        while (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(a);
        }
        Debug.Log(EventSystem.current.currentSelectedGameObject+"\n\n"+"这是");
        EventSystem.current.SetSelectedGameObject(a);
    }
    //public void 回退()
    //{
    //    //回退
    //    if (当前按钮集合 != this) return;
    //    if (上一个按钮集合 == null) return;

    //    当前按钮集合 = 上一个按钮集合;
    //    E_M.I.Invoke(E_M.退回UI触发);
    //    当前按钮集合.OnEnable();
    //    transform.parent.gameObject.SetActive(false);
    //    上一个按钮集合 = null;
    //}
    [SerializeField]
    SceneField 主菜单;
    public void  退回菜单()
    {
        SceneManager.LoadScene(主菜单.BuildIndex);

    }
 
}
