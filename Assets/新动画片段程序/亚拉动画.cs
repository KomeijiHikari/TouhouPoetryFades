using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public  interface 亚拉动画片段
{
  void  播放();
    void 结束播放();
}
public class 亚拉动画 : MonoBehaviour, 亚拉动画片段
{
    public Action<string> 动画结束; 
    [SerializeField ]
    SpriteRenderer sp;
    Sprite  Deful;
    [SerializeField] 单片段 P;

    [SerializeField]
    private List<Sprite> sps;
    public bool 随组件播放;
 
    public bool 播放ing;

    int I;
    float time;
    [SerializeField ]
    private bool 循环;

    private void OnEnable()
    {
        
        if (随组件播放)    播放(); 
    }
    private void OnDisable()
    {
        if (随组件播放)  结束播放();
     
    }
    private void Awake()
    { 

        sp = GetComponent<SpriteRenderer>();
        var a = sp.enabled;
        Deful = sp.sprite;
        结束播放();
        sp.sprite = Deful;
        sp.enabled = a;


    }
    [Button("结束播放", ButtonSizes.Large)]
   public  void 结束播放()
    {
        sp.enabled = false;
        播放ing = false;
        sp.sprite = Deful;
        I = 0;
        time = 0;
    }
    public void 播放(单片段 pp)
    {
        P = pp;
        sp.enabled = true; ;
        time = 0;
        I = 0;
        播放ing = true;
    }
 
    [Button("播放", ButtonSizes.Large)]
    public void 播放()
    {
 
  
        sp.enabled = true; ;
        time = 0;
        I = 0;
        播放ing = true;
    }
    [Button("从中间开始播放", ButtonSizes.Large)]

    //相对时间   绝对时间  帧数播放 
    public void 播放(float a)
    {
        time = 0;
        I = 0;
        播放ing = true;
    }
     //相对时间   绝对时间  帧数播放 
    public void 播放(int a)
    {
        time = 0;
        sp.enabled = true; ;
        if (a+1< Sps .Count&&a>-1)
        {
            I = a;
            播放ing = true;
        }
        else
        {
            Debug.LogError("草泥马的  你动画歪了"+gameObject .name+transform .position );
        }
    }
    float 单帧长
    {
        get {
            if (P==null)
            {
                return 0.1f;
            }
            return P.单帧长;
        }
    }

    public bool 循环1 { get
        {
            if (P==null)
            {
                return  循环;
            }
            return P.循环;
        }
            
             }

    public List<Sprite> Sps { get { 
                if (P == null)
        {
                return sps;
            }
            else
            {
                return P.sps;
            }
        } set => sps = value; }

    private void Update()
    { 
        if (播放ing)
        {
            time += Time.deltaTime;
         sp.sprite =  Sps[I];
            if (time> 单帧长 )
            {
                time -=  单帧长;
                if (I+1<  Sps.Count )
                {
                    I++;
                }
                else if (I + 1==  Sps.Count)
                {
                    if ( 循环1)
                    {
                        I = 0;
                    }
                    else
                    {
                        if (P!=null) 动画结束?.Invoke(P.name);
                        else 动画结束?.Invoke(null);

                        结束播放();
                    } 
                } 
            }
        }
    }
}
