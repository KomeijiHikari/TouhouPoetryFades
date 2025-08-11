using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 背包页面
/// </summary>
public class No_select_father : Button_Father_base
{
    [SerializeField][DisplayOnly ]
    假选择[] 假选择列表;
    [SerializeField][DisplayOnly]
    private 假选择 当前选中1;
    public 假选择 当前选中 { get => 当前选中1; set
        {
            if (当前选中1!=value)                        //被改变的时候更新加选择下标；   未设置不改变
            当前选中1 = value;
        } }

    private 假选择 上一个选中;
    public int 当前下标1 { get => 下标; set
        {

            if (value <0)
            {
                下标 = 假选择列表.Length - 1;
            }
            else if(value >= 假选择列表.Length)
            {
                下标 = 0;
            }
            else
            {
                下标 = value;
            }

        } 
            }
    [DisplayOnly]
    [SerializeField]
    int 下标=0;

    [DisplayOnly]
    [SerializeField]
    private int 标签方向1;
    public int 标签方向
    {
        get { return 标签方向1; }
        set 
        { 
            if (value!=0&& 标签方向1==0)
            {//旧的是0   新的是正负1
                当前下标1 += value;

                标签方向1 = value;
               
                假选择列表[当前下标1].Select();
            } 
            标签方向1 = value; 
        }
    }


    bool 初始化过;
    void 获取引用()
    {
        if (初始化过) return;
        初始化过 = true;
        //目标列表 = 按钮爹的爹.GetComponentsInChildren<Text_button_Father>();
        假选择列表 = GetComponentsInChildren<假选择>();
        foreach (Transform  item in 按钮爹的爹.transform) 目标列表.Add(item.GetComponent<Text_button_Father>());
         



        if (目标列表.Count != 假选择列表.Length) Debug.LogError("数组长度不匹配");


        for (int i = 0; i < 目标列表.Count ; i++)
        {
      if(i!=0)     目标列表[i].gameObject.SetActive(false);
            目标列表[i].展开情景1 = Text_button_Father.E_展开情景.平级菜单;
        }
  
 
        if (!Target .activeInHierarchy)
        {
            Debug.LogError("我去事关者的");
        }
        假选择列表[0].Select();
    }
    private void Awake()
    {
        获取引用(); 
    }

    public Text_button_Father 返回当前数组(假选择  a)
    {
        if (假选择列表==null)
        {
            Debug.LogError("离谱。是空的");
        }
        if (假选择列表.Length==0)
        {
            Debug.LogError("离谱。是空的长度是0");
        }
        for (int i = 0; i < 假选择列表.Length ; i++)
        {
            if (假选择列表[i]==a)
            {
                return 目标列表[i];
            }
        }

        Debug.LogError("离谱，一个都找不到");
        return null;
    }
    [SerializeField]
    GameObject 按钮爹的爹;
    [SerializeField]
    [DisplayOnly]
 List <  Text_button_Father>  目标列表;

    private void Update()
    {
        标签方向 = (int)Input.GetAxisRaw(Initialize.BagSwitch);
        if (Input .GetButtonDown(Initialize .Exite))
        {
            被回退();
        }
    }

 

public  override  void 被回退()
    { 
        Target.SetActive(false);
        Event_M.I.Invoke(Event_M.UI回到战斗);
    }

    public override void 被展开()
    {
        Target.SetActive(true);
        获取引用();

    }
}
