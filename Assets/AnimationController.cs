using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController_base : MonoBehaviour
{
    Animator animator;
    public bool 状态消息_;
    public bool 状态消息 { get => 状态消息_; set => 状态消息_ = value; }
    string speed { get; } = "speed";
    [DisplayOnly]
    [SerializeField]
    float AnimSpeed_;

    public float NormalizedTime {   get {    return  animator.GetCurrentAnimatorStateInfo(0).normalizedTime;   }   }
    public float TimeLength { get { return animator.GetCurrentAnimatorClipInfo(0)[0].clip.length; } }
    public float FrameRate { get { return animator.GetCurrentAnimatorClipInfo(0)[0].clip.frameRate; } }
    public string 当前anim { get { return animator.GetCurrentAnimatorClipInfo(0)[0].clip.name; } }



    public float NextSpeed { get; set; } = 1;
    public float Speed
    {
        get
        {
            AnimSpeed_ = animator.speed;
            return animator.speed;
        }
        set
        {
            AnimSpeed_ = animator.speed;
            if (value < 0)
            {
                animator.SetFloat(speed, value);
                animator.speed = 1;
            }
            else
            {
                animator.SetFloat(speed, 1);
                animator.speed = value;
            }

        }
    } 
  public   string Next { get; set; }
 public    float 时间界限 { get; set; }
    public float 跳转时间 { get; set; }
    [SerializeField ][DisplayOnly] string 当前anim_;

    protected virtual void Awake()
    {
        animator= GetComponent<Animator>();
    }
    string 监控;
    protected virtual void Update()
    {
        if (监控 != 当前anim)
        {
            if (状态消息) Debug.Log("上一个：    " + 监控 + "下一个：  " + 当前anim);
            监控 = 当前anim;
        }

        float a = 0.9f;
        if (时间界限 != 0) a = 时间界限;
 
        if (NormalizedTime>a)
        {
            J++;
        }
        else if(NormalizedTime<0.9f)
        {
            J = 0;
        }

        if (J==1)
        {
            当前动画为百分之99();
        } 
    }
    int J;
    void 当前动画为百分之99()
    {
        if (Next !=null)
        {
            Debug.LogError("调用Next");
            Playanim(Next);
        }

    }
    int Last; 
    public void Playanim(string anim)
    {        //迭代当前anim,相同帧检测，速度恢复，Next 恢复，时间检测恢复,下一段跳跃播放

        if (当前anim == anim)
        {//迭代当前anim
            if (状态消息) Debug.Log( 当前anim +   "        重复触发");
            return;
        }
        if (Time.frameCount != Last)
        {//相同帧检测
            Last = Time.frameCount;
            if (状态消息) Debug.Log("上一个：    " + 当前anim + "下一个：  " + anim + "        同一帧触发");
        }
        if (Speed != 1)
        {//当前速度恢复
            Speed = 1;
        }
        if (NextSpeed != 1)
        {//设置下一个动画的速度
            Speed = NextSpeed;
            NextSpeed = 1;
        }
        if (Next != null)
        {//Next 恢复
            Next = null;
        }
        if (时间界限 != 0)
        {
            // 时间检测恢复
            时间界限 = 0;
        }
        if (跳转时间!=0)
        {
            //下一段跳跃播放
            animator.Play(anim,0, 跳转时间);
            跳转时间 = 0;
        }
        else
        {
            animator.Play(anim);
        }
        监控 = anim;
   if (状态消息)     Debug.Log("上一个：    " + 当前anim +   "下一个：  " + anim  );
    }  
}

public class AnimationController: AnimationController_base
{
    [DisplayOnly]
  public  bool 翻转开关;

    protected  override void Update()
    {
        if (!enabled) return;
 
        if (当前anim == "idle_0_")
        {
            翻转开关 = false;
        }
        if (当前anim  == "run_0_")
        {
            翻转开关 = true;
        }


    }
}
 
