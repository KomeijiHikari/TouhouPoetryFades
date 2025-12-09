using System;
using System.Collections;
using System.Collections.Generic;
using Trisibo;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


[Serializable]
public class 加强点
{
    public
      GameObject obj;
       [SerializeField]
    private UnityEvent<GameObject> 行为 = new UnityEvent<GameObject>();
    public void Invoke( )
    {
 
           行为?.Invoke(new GameObject ("ASDASDASD"));
    }
}

/// <summary>
/// 依赖目标场景有一个剧情触发点预制体，那个脚本中实现剧情需要的所有点，点的长度必须匹配
/// </summary>
public class 剧情控制器 : MonoBehaviour
{
    操控 操控_;
    I_攻击 攻击_;

    static 剧情控制器 I { get;    }
 

    public List<场景剧情> 场景剧本列表;
    [DisplayOnly]
    [SerializeField]
    场景剧情 C_场景剧本;
    [DisplayOnly]
    [SerializeField ]
    触发剧情 C_触发剧情;

    [Serializable]
  public  class 场景剧情
    {
 
     public   BiologyBase 生物;
        public List<加强点> 剧本;
       public  SceneField 场景;

    }
    private void Start()
    {
        
            Event_M.I.Add(Event_M.切换场景触发_obj, 获取该场景的剧本位置信息);
            Event_M.I.Add(Event_M.剧情触发, 开始剧情);
        }
    void   获取该场景的剧本位置信息(GameObject  a)
    {
        Scene S = a.scene;
        bool 匹配=false;
        foreach (var item in 场景剧本列表)
        {
            if (item .场景.Name==a.scene.name)
            {
                匹配 = true;
                C_场景剧本 = item;
            }
        }
        if (!匹配)
        {
            Debug.LogError("不匹配");
            return;
        }

 
        foreach (var item in S.GetRootGameObjects())
        { 
            if (item.tag== "剧情点")
            { 
                C_触发剧情 = item.GetComponent<触发剧情>(); 
            }
        }
        if (C_触发剧情==null)
        {
            Debug.LogError("没找到");
            return;
        }

        if (C_场景剧本.剧本.Count != C_触发剧情.位置列表.Length)
        {
            Debug.LogError("长度不匹配");
            return;
        }

        for (int i = 0; i < C_触发剧情.位置列表.Length; i++)
        {
            C_场景剧本.剧本[i].obj = C_触发剧情.位置列表[i];
        }
 

    }
    [SerializeField] [DisplayOnly]    加强点 当前;
    [SerializeField][DisplayOnly]    int 下标;
    [SerializeField ]
    [DisplayOnly]
    bool 开始剧情了嘛;

    public  static   void  Break( )
    {
 
        //this.C_场景剧本
    }
    void   开始剧情()
    {
        if (开始剧情了嘛) return;
            开始剧情了嘛 = true;
        检测 = true;

        if (C_场景剧本.生物==null)
        {
            C_场景剧本.生物 = Player.I;
        }
       C_场景剧本 . 生物.关闭灵魂();

        C_场景剧本.剧本 [0].Invoke();
        下标++;
        Debug.LogError("开始");
        下个目标点 = C_场景剧本.剧本[下标].obj;
    }
    [SerializeField][DisplayOnly]
    bool 检测;

   //public  void 关闭检测()
   // {
   //     检测 = false;
   // }
   // public void 开启检测()
   // {
   //     检测 = true;
   // }
 public   void 延迟切换(float time)
    {
        检测 = false;
        StartCoroutine(延迟( time));
    }
    IEnumerator  延迟(float time)
    {
        yield return new WaitForSeconds(time);
        检测 = true;
    }
    public GameObject 下个目标点;
    private void Update()
    {
        if (开始剧情了嘛 && 检测)
        {
            if (下标 + 1 <= C_场景剧本.剧本.Count)
            {
                //没超出
                var a = C_场景剧本.生物.transform.position.x - 下个目标点.transform.position.x;
                if (MathF.Abs(a) <= 5)
                {//接近了
                    C_场景剧本.剧本[下标].Invoke();
                    Debug.LogError("接近触发");
                    下标++;
                    下个目标点 = C_场景剧本.剧本[下标].obj;
                }
            }
            else
            {         // 超出
                C_场景剧本.生物.开启灵魂();
                开始剧情了嘛 = false; 
                检测 = false;
                下标 = 0;
            }
        }
    }
}
