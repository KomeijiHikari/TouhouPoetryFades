using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JUMAP_name
{
 
    public static string 上去 { get; private set; } = "jump_";
    public static string 中间 { get;  private set; } = "jump_max";
    public static string 下去 { get; private set; } = "jump_down";
}
public class AniContr_4 : AnimBase
{
    Timeline管理 T;
    public Action<string > 关键帧;
    public void 触发(string  s)
    {
        ///Timeline播放动画不会带当前动画名
        关键帧?.Invoke(s);
    }
    public void 触发()
    {
        ///Timeline播放动画不会带当前动画名
        关键帧?.Invoke(当前名字);     
    }
 public    string 当前名字
    {
        get
        { 
            return animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        }
    } 
    public bool 翻转开关 { get; set; }
    public override void awake()
    {
        T = GetComponent<Timeline管理 >();
          当前动画为百分之99 += to_如果是过渡动画播放结束发生的事件; 

        时间界限默认值 = 0.95f;
        时间界限 = 时间界限默认值;
    }

 
    //IEnumerator  adsasd()
    //{
    //    for (int i = 0; ; i++)
    //    {
    //        var a = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
    //        if ( a!= 当前anim.name)
    //        {
    //            动画状态机代理 = false;
    //               当前anim =GetAnim(a) ;
    //            break;
    //        }
    //        yield return null;
    //    } 
    //}
    [DisplayOnly]
    [SerializeField]
    float AnimSpeed_;

    string speed { get; } = "speed";
   /// <summary>
   /// 标准时间
   /// </summary>
    public float 当前进度
    {
        get
        {
            var f= animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (f>1)
            {
                return 1;
            }
            return f;

        }
    }
   public float AnimSpeed { 
        get {
            AnimSpeed_ = animator.speed;
            return animator.speed; 
        } 
        set {
            AnimSpeed_ = animator.speed;
            //if (AnimSpeed_!=value )
            //{
            //    Debug.Log("发生变动"+value );
            //}
            if (value<0)
            {
                animator.SetFloat(speed, value);
                animator.speed = 1;
            }
            else
            {
                animator.SetFloat(speed, 1);
                animator.speed = value;
            }

        } }
    public void NextAnim(Anim2 N, float Time,float speed)
    {
        AnimSpeed = speed;
        NextAnim(N, Time);
    }
    public void NextAnim(Anim2 N, float Time)
    {
        时间界限 = Time;
        NextAnim(N);
    }
    public void NextAnim(Anim2 N  )
    {
        Debug.Log(Initialize ._Color("调用 了",Color.red));
        Next = N;
    }
    
    private void to_如果是过渡动画播放结束发生的事件()
    {
         
        if (!enabled) return;


        bool bBB = false; 

        if (Next != null && Next.name!=null)
        { 
            Playe(Next); 
        }
else  foreach (Anim2 anim in 所有anim)
        {
            if (anim.start == 当前anim.start)
            { 
                if (anim.playerOrder == 当前anim.next)
                {
                        //某order==当前next
                   if(状态消息)     Debug.Log(当前anim.name + "顺到     :" + anim.name); 
                        Playe(anim);
                    break;
                } 
            }  
        }

    }

 

    public void Playanim(string name,float time)
    {
        if (!enabled) return;
        Playe(GetAnim(name),time);
    }
    public void Playanim(string name)
    {
 
        if (!enabled) return;
        if (T != null) T.停止播放();
        Playe(GetAnim(name)); 
    }
    [SerializeField]
    bool 点击;

    [SerializeField ][DisplayOnly ]
    float 进度;
    protected  override void Update()
    {
        if (!enabled) return;
        if (点击)
        {
            点击 = false;
            animator.Play(当前anim.name);
        }
        进度 = 当前进度;
        检测当前播放动画进度(时间界限);
        if (当前anim .name=="idle_0_")
        {
            翻转开关 = false;
        }
        if (当前anim.name == "run_0_")
        {
            翻转开关 = true;
        } 
        }
    }

