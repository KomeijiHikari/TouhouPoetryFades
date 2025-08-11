using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Trisibo;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class 主UI : MonoBehaviour
{
    public Boss血条  Boss血条; 
    [Space ]

    public Image 遮罩;
    public static 主UI I { get; private set; }

    public Animator 过场动画;

    public Text 钱币;

 

    public Text Speed_Lv;

    public Text 场景名;
    private void Awake()
    {

        if (I != null)
        {
            Destroy(gameObject);
        }
        else
        {
            I = this;
        }
    }
    [DisplayOnly]
    [SerializeField]
    BiologyBase Player;
    private void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<BiologyBase>();
        Event_M.I.Add(Event_M.场景保存触发, 触发存档);

        Event_M.I.Add(Event_M.UI回到战斗, 退回);


         quence = DOTween.Sequence();
        quence.AppendCallback(() => 遮罩.color = new Color (QQc.r , QQc.g , QQc.b ,0));
        quence.Append(遮罩.DOFade(1,0.15f));
        quence.AppendInterval(0.1f);
        quence.Append(遮罩.DOFade(0, QQtime));
        quence.Pause().SetAutoKill(false);
    }

    Color QQc;
    float QQtime;
    Sequence quence;
    float TTTime;
    public void 遮罩动画(Color c=default ,float time =1.5f )
    {
        if (Time.time > TTTime+0.1f)
        {
            TTTime = Time.time;
        
        if (c == default) QQc = Color.black;
        QQtime = time;

        quence .Restart ();
        }
    }
    void 退回()
    {
        Player.开启灵魂();
        Initialize.时间恢复();

        当前子菜单 =null;
    }
    public void Boss血条_(GameObject  那个Boss,String name ,bool 开关 )
    { 
            Boss血条.Boss血条_(那个Boss, name, 开关);  
            Boss血条.gameObject.SetActive(开关); 
    }
    void 触发存档(GameObject  a)
    {
        消息.I.Come_on_Meesge("已保存！"); 
    } 
    public void 播放黑幕动画()
    {
        过场动画.Play("黑");
    }
    public void 播放红动画()
    {
        过场动画.Play("红");
    }
    //[DisplayOnly]
    //[SerializeField]
    //bool 不可以切换;
    //public void 切回战斗场景了()
    //{//从二级菜单调用   缓冲时间

    //    选择爹.REmove();
    //    当前子菜单.SetActive(false);
    //    当前子菜单 = null;
    //    Player.开启灵魂();
    //    Initialize.时间恢复();
    //    StartCoroutine(ASDASD());
    //}
    //IEnumerator ASDASD()
    //{
    //    不可以切换 = true;
    //    yield return new WaitForSecondsRealtime(0.5f);
    //    不可以切换 = false;
    //}

    public 强提示 强提示;
    public GameObject 大地图;
    [SerializeField]
    SceneField 主菜单场景;
    public Button_Father_base 背包菜单;
    public Button_Father_base 战斗菜单;
    [DisplayOnly]
    public Button_Father_base 当前子菜单;
    public void 展开(Button_Father_base obj,bool 地面限制=true)
    {
        if (地面限制) if (!Player.Ground) return;//玩家站在地上 
        if (obj.gameObject .activeInHierarchy) return;//菜单在XX里面是激活的 
        //if (不可以切换) return;  //在缓冲时间内  不可释放 
        if (当前子菜单 != null) return; //有其他菜单不可释放 
        var a = obj as Text_button_Father;
        if (a != null) a.展开情景1 = Text_button_Father.E_展开情景.一号菜单;

        obj.被展开();

 
        当前子菜单 = obj;


        Player.关闭灵魂();
        Initialize.时间暂停();
    }

    public void  发送消息()
    {

    }
   
    public   void  回到主菜单()
    {
        SceneManager.LoadScene(主菜单场景.BuildIndex);
    }

    static string FormatFloat(float value)
    {
        // 使用 "F3" 格式化字符串，保留三位小数  
        return value.ToString("F3");
    }
    private void Update()
    {
        if (Speed_Lv != null)
            Speed_Lv.text = FormatFloat(Player3.Public_Const_Speed);
        //Speed_Lv.text = (Math.Round(Player3.Public_Const_Speed, 3)).ToString()+".000"; 
 
        大地图.SetActive(Player_input.I.按键检测_按住(Player_input.I.地图)); 

        //Initialize   设置  地图   背包
        //if (Input.GetButtonDown(Initialize.Bag))
        //{
        //    展开(背包菜单);
        //}
         
        if (Input.GetButtonDown(Initialize.Exite))
        {//从菜单里切回来的时候这个会触发
            if (!Player.Ground) return;//玩家站在地上 
            Player.关闭灵魂();
            Initialize.时间暂停();
            //测试.被展开();

            展开(战斗菜单);
        }

        if (钱币!=null)
        {
            钱币.text = Player3.I.玩家数值.钱.ToString();
        }
        

        场景名.text = SceneManager.GetActiveScene().name;

    }
}

 