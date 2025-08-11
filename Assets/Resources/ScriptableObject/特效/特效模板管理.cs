using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class 特效模板管理 : MonoBehaviour, I_Speed_Change
{
    public GameObject 对象 { get => gameObject; }
    public System.Action 变速触发 { get; set; }
    public bool 同步玩家;
    public float Current_Speed_LV { get => Speed_Lv; set => Speed_Lv = value; }
    String 当前名字
    {
      get
        {
            return      an.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        }
    }
    float 当前进度
    {
        get
        {
            return an.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }
    }

 
    public float Speed_Lv { get => speed_Lv; set => speed_Lv = value; }

    List<特效模板> 列表;
  public  Animator an;
    SpriteRenderer sp;

    Vector2 Last;
    Transform father { get;set; }
    bool 竖直监控;
    [SerializeField]
    [DisplayOnly]
    private float speed_Lv=1;

  public   特效模板 播放特效(string name)
    {
        特效模板 当前特效模板 = null; 
        for (int i = 0; i < 列表.Count; i++) if (列表[i].clip.name == name) 当前特效模板 = 列表[i];   //获取特效

        if (当前特效模板 == null)
        {
            Debug.LogError("数据容器  里没有这个名字           " + name);
            return null;
        }
        else
        {
            //Debug.LogError("数据容器   " + name);
        }

        string Playname = 当前特效模板.clip.name;
        if (当前特效模板.clipS != null)
        {
            int i = Initialize.RandomInt(0, 当前特效模板.clipS.Count);
            if (i != 0) Playname = 当前特效模板.clipS[i - 1].name;

        }
        A_name = name;
        Time_ = Time.frameCount;
        an.Play(Playname);        //播放

        return 当前特效模板;

    }
    private void OnEnable()
    {
        if (A_name!=null)
        {
            an.Play(A_name);
        }    
    }
 
    public 特效模板管理 Play(Vector2 v2, string name, bool 朝向 = true, SpriteRenderer Tsp = null)
    {

        特效模板 a = 播放特效(name);
        if (a == null) Debug.LogError("空空空空空");
        transform.position = v2 + a.偏移;


        if (Tsp != null)
        {
            sp.sortingLayerID = Tsp.sortingLayerID;   //显示次序 
            sp.sortingOrder = Tsp.sortingOrder + 1;
        }
        else
        {
            sp.sortingLayerID = 特效_pool_2.I.sp.sortingLayerID;   //显示次序 
            sp.sortingOrder = 特效_pool_2.I.sp.sortingOrder + 1;
        }

        int I = 朝向 ? 1 : -1;
        transform.localScale = new Vector2(I, 1);
        return this;
    }
    public string A_name { get; set; }

    public I_Speed_Change I_S { get => (I_Speed_Change)this; }

 
    public void Play(GameObject 召唤者, string name, SpriteRenderer sp_ = null)
    {
        Time_ = Time.frameCount;
         
        竖直监控 = false;
        Last = Vector2.zero;
        if (召唤者 == null) return;
        Transform tt = 召唤者.transform;
        Vector2 碰撞体中心点 = 召唤者.GetComponent<Collider2D>().bounds.center;
        SpriteRenderer ts = 召唤者.GetComponent<SpriteRenderer>(); 
        if (ts == null) ts = sp_;
        if (ts == null) Debug.LogError("SP 还是空的"); 
        特效模板 当前特效模板 = 播放特效(name);
        if (当前特效模板 == null) return; 


        sp.sortingLayerID = ts.sortingLayerID;   //显示次序
        if (当前特效模板.是在目标的前面嘛)
            sp.sortingOrder = ts.sortingOrder + 1;
        else
            sp.sortingOrder = ts.sortingOrder - 1;


        transform.SetParent(tt);
        transform.localScale = Vector2.one;
        switch (当前特效模板.位置类型)        //中心点
        {
            case 特效模板.位置中心类型.原点:
                transform.localPosition = Vector2.zero + 当前特效模板.偏移;
                break;
            case 特效模板.位置中心类型.碰撞体:
                transform.position = 碰撞体中心点 + new Vector2(tt.localScale.x * 当前特效模板.偏移.x, 当前特效模板.偏移.y);
                //Debug.LogError( transform . localScale.x); 
                break;
        }

        同步玩家 = 当前特效模板.同步玩家;

        if (当前特效模板.是跟随目标)
        {
            if (当前特效模板.地面限定)
            {
                Last = transform.position;
                竖直监控 = true;
            }
        }
        else
        {
            transform.SetParent(father);
        }
    }
    internal void 初始化(RuntimeAnimatorController c, List<特效模板> list)
    {

        Initialize.组件(gameObject, ref sp);
        Initialize.组件(gameObject, ref an);
        father = transform.parent;
        列表 = list;
        an.runtimeAnimatorController = c;
    }
    int Time_;
    private void Update()
    {
        if (竖直监控)
        {
            if (transform.position.y != Last.y && Last != Vector2.zero)
            {
                竖直监控 = false;
                transform.SetParent(father);
            }
            else
            {
                Last = transform.position;
            }
        }
        if (!代理回归)
        {
           ///重启后莫名回归NULL
            if (当前名字 == "NULL" && Time.frameCount - Time_ > 3 && 当前进度>0.99f)
            {
                //Debug.LogError("返回" + A_name);
                Speed_Lv = 1;
                an.updateMode = AnimatorUpdateMode.Normal;
                同步玩家 = false;
                A_name = null;
                特效_pool_2.I.ReturnPool(this );

            }
        }

        if (同步玩家)
        {
            Speed_Lv = Player3.Public_Const_Speed;
        }
        an.speed = Speed_Lv / Player3.Public_Const_Speed;
    }
    public bool 代理回归=false ;
}
