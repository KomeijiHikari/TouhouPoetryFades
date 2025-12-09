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
    适应文字 s;
    public float   WiteTime=2;

    RectTransform r;
    CanvasGroup C;
    Vector2 StartWay;

    Sequence quence;

    // queue for incoming messages
    private readonly Queue<string> messageQueue = new Queue<string>();
    private bool isPlaying = false;

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


        s= T.GetComponent<适应文字>();
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

        // attach completion handler to drive queue playback
        quence.OnComplete(OnSequenceComplete);

        quence.Pause().SetAutoKill(false);
    }
    [SerializeField ][DisplayOnly ]
    bool kai; 
    public void Come_on_Meesge(string  s)
    { 
        Debug.Log("消息队列收到消息："+s);
        // enqueue and start playback if idle
        if (string.IsNullOrEmpty(s)) return;
        messageQueue.Enqueue(s);
        if (!isPlaying)
        {
            StartNext();
        }
    }

    // start next message from queue
    private void StartNext()
    {
        if (messageQueue.Count == 0) return;
        var next = messageQueue.Dequeue();
        // set text
        this.s.SetText(next);
        isPlaying = true;
        if (quence == null)
        {
            // safety: if sequence not built, immediately mark done
            isPlaying = false;
            return;
        }
        quence.Restart(); 
    }

    // DOTween onComplete handler
    private void OnSequenceComplete()
    {
        isPlaying = false;
        // play next if any
        if (messageQueue.Count > 0)
        {
            // start next on next frame to avoid reentrancy
            StartCoroutine(StartNextNextFrame());
        }
    }

    private IEnumerator StartNextNextFrame()
    {
        yield return null;
        StartNext();
    }
 }
