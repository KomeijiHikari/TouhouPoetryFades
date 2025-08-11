using System;
using System.Collections;
using System.Collections.Generic;
using Trisibo;
using UnityEngine;
using UnityEngine.SceneManagement;
public class 传送导点 : MonoBehaviour
{
    [DisplayOnly]
    public 切换场景 [ ] 场景名列表作切换传送门检测=new 切换场景[2] ;
 
  SceneField 存档的场景;
    public void ADD(切换场景 a)
    {
        for (int i = 0; i < 场景名列表作切换传送门检测.Length; i++)
        {
            if (场景名列表作切换传送门检测[i] == null)
            {
                场景名列表作切换传送门检测[i] = a;
                return;
            }
            else if (场景名列表作切换传送门检测[i]==this)
            {
                return;
            }
        }
    }
    public void UnLoad(切换场景 a)
    {
        for (int i = 0; i < 场景名列表作切换传送门检测.Length; i++)
        {
            if (场景名列表作切换传送门检测[i] == a)
            {
                场景名列表作切换传送门检测[i] = null; 
                break;
            }
        }
        if (场景名列表作切换传送门检测[ 0]==null&& 场景名列表作切换传送门检测[1] == null)
        {
            //a.加载(gameObject);

            Event_M.I.Invoke(Event_M.切换场景触发_obj ,a.gameObject);
        }
    }
 public static    传送导点 I { get; private set; }
    [DisplayOnly]
    [SerializeField]
    传送点 传送点; 
    private void Start()
    {
        if (I!=null )
        {
            Destroy(this);
        }
        else
        {
            I = this;
        }
        StartCoroutine(等());
        GetComponent<I_生命>().生命归零 += (() => 传送());  
    }
 
    public void 传送()
    {
        StartCoroutine(等());
    }
    IEnumerator 等 ()
    {
 
        for ( ;  ;  )
        {
            if (Initialize.所有的场景都加载完了嘛())
            { 
                GameObject a = Initialize.获取已加载场景根节点的TAG是的("传送点");
                if (a == null)
                {
                    Debug.LogError("没有传送点");
                }
                else
                { 
                    传送点 = a.GetComponent<传送点>();
                    Initialize.设置和当前活动场景为这个obj的场景(传送点.gameObject);
                    //gameObject.transform.position = 传送点.传送点坐标;
                    if (最后点!=Vector2 .zero)
                    {
                        gameObject.transform.position = 最后点;
                    }
                    else
                    {
                        Debug.LogError("传送点为？");
                    }

Event_M.I.Invoke(Event_M.切换场景触发_obj, this.gameObject);
                }
                yield break;
            }
            else 
            {
                // 还没加载好  
                yield return null; // 等待一帧   
            }
        }  
    }


 public   Vector2 最后点
    {
        get => Save_static.LoadinText<Vector2 >(Save_static.存档点位 );
    set => Save_static.SaveinText(Save_static.存档点位, value);
    }
 
 
}


