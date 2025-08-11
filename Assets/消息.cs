using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 消息提示  强弱    弱：条田谈起消息，资源窗口数字动态改变   不影响操作
/// 中  占领一块地方
/// 强  暂停，弹窗     一段时间内不能退出     然后按键退出
/// </summary>
public class 消息 : MonoBehaviour
{
    public float timee; 
    public static 消息 I;
    public Vector2 FormPo;
    public Text T;
    public  float   WiteTime=2;

    RectTransform r;
    CanvasGroup C;
    Vector2 StartWay;

    Sequence quence;

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
        C = GetComponent<CanvasGroup >();
        r = GetComponent<RectTransform >( );


   
    }
    private void Start()
    {
        StartWay =r.anchoredPosition ;

        r.anchoredPosition = FormPo; 
        C.alpha = 0;


        quence = DOTween.Sequence();

        quence.Append(r.DOAnchorPos(StartWay , timee).SetEase (Ease.OutQuint));
        quence.Join(C.DOFade(1, timee));
        quence.AppendInterval(WiteTime);
        quence.Append(r.DOAnchorPos(FormPo, timee).SetEase(Ease.InQuint));
        quence.Join(C.DOFade(0, timee));

        quence.Pause().SetAutoKill(false);
    }
    [SerializeField ][DisplayOnly ]
    bool kai;


 //void asd(bool b)
 //   {
 //       if (b)
 //       {
 //           kai = true;
 //           r.anchoredPosition = FormPo;
 //           C.alpha = 0;

 //           T1.PlayForward();
 //           T2.PlayForward();
 //       }
 //       else
 //       {
 //           kai = false ;
 //           C.alpha = 1;

 //           T1.PlayBackwards();
 //           T2.PlayBackwards();
 //       }

 //   }

    public void Come_on_Meesge(string  s)
    {
        T.text = s;

        quence.Restart();
        //quence.Play();
        //asd(true); 
        //Initialize_Mono.I.Waite(
        //    () => { asd(false); }
        //    , WiteTime
        //    );


    }
 
}
