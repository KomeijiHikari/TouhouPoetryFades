using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class AnimBase : MonoBehaviour
{
protected  Anim2 Next { get; set; }
    public bool 状态消息_;
    public bool 状态消息 { get => 状态消息_; set => 状态消息_ = value; }
    void 播放特效(string s)
    {//动画事件？

        特效_pool_2.I.GetPool(gameObject, s).Speed_Lv=Player3.Public_Const_Speed;
    }
    public Action 当前动画为百分之99 { get; set; }
    [HideInInspector]
    public Animator animator { get => animator_; set => animator_ = value; }
    [HideInInspector]
    protected Animator animator_;

    [SerializeField]
    protected AnimationClip[] 所有的clips;
    public List<Anim2> 所有anim;

    public Anim2 当前anim;


    protected virtual void Awake()
    {
        小地图显示.I.读取(); 
        
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            animator = gameObject.AddComponent<Animator>();
            Debug.LogError("空空空空");
        }
        所有的clips = animator.runtimeAnimatorController.animationClips;

        for (int i = 0; i < 所有的clips.Length; i++)
        {
            所有anim.Add(new Anim2(所有的clips[i]));
        }
        //foreach (var item in 所有的clips)
        //{
        //    所有anim.Add(new Anim2(item));
        //}
        当前动画为百分之99 += 播放结束;
        时间界限 = 时间界限默认值;
        awake();
        for (int i = 0; i < 所有的clips.Length; i++)
        {
            //animator.Play(所有的clips[i].name); 
        }
        info = animator.GetCurrentAnimatorStateInfo(0);
    }
    AnimatorStateInfo info;
    public virtual void 播放结束()
    {

    }

    public virtual void awake()
    {

    }
 
    public   float 时间界限默认值 { get; set; } = 0.9f;
    public float 时间界限 { get; set; }
    protected virtual void Update()
    {
        检测当前播放动画进度(时间界限);
 
    }


    int J { get; set; } 
    protected virtual     void 检测当前播放动画进度(float f)
    {
 
        var a = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        当前anim.进度 =  a* 当前anim.time;
        if (a>= f)
        {
            J++;
        }
        else if(a <f)
        { 
            J = 0;
        }
        if (J == 1)
        {

            当前动画为百分之99?.Invoke(); 
            J++; 
        } 
    }
    public string 上一个anim名字;
    /// <summary>
    /// 从下一动画的time开始播放？
    /// </summary>
    /// <param name="anim"></param>
    /// <param name="time"></param>
    protected void Playe(Anim2 anim,float time)
    {
        if (当前anim == anim)
        {
            return;
        }
        else
        {

            //Debug.Log (Time.frameCount);
            //Debug.LogError("上一个名字：" + 当前anim.name + "\n跳跃到 " + anim.name  + "的进度" + time);
            当前anim.exit();
            animator.Play(anim.name);
            StartCoroutine(Initialize.Waite(() =>
       {
           int i = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;
           animator.Play(i, 0, time); 
       }
            ));

            //animator.speed = 0.5f;
            anim.enter();
            if (状态消息)
            {
                Debug.Log("上一个名字：" + 当前anim.name + "\n下一个名字" + anim.name);
            }
 
            上一个anim名字 = 当前anim?.name;
            当前anim = anim;
        }
    }
    int Last;
    Anim2 我先;
   
    protected void Playe(Anim2 anim)
    {
  
        //迭代当前anim,相同帧检测，速度恢复，Next 恢复，时间检测恢复;

        //正常情况      被Next调用     有Next但是被强切
        if (当前anim == anim) return; 
        if (Last == Time.frameCount
            && 我先 != null  )
        {//相同的i情况
 
            Debug.LogError("同一帧调用"
                + "\n" + anim.name + animator.GetCurrentAnimatorClipInfo(0)[0].clip.name
                  + "\n" + 当前anim.name
                  + "\n" + animator.GetCurrentAnimatorStateInfo(0).normalizedTime
                   + "\n" + 我先.name  + "然后" + anim.name
                   ); ;
            if ((我先.name==A_N.run_jump_to0&& anim.name==A_N .run_0_)
                || (我先.name == A_N.idle_jump_to0 && anim.name == A_N.idle_0_))
            {
                Debug.Log("return");
                return;
            }


        }
        else
        {//不同的i情况
            我先 = anim;
           Last = Time.frameCount;
        }
 
        当前anim.exit();
        当前anim.speed = 1f;  //字段恢复
        if (animator.speed != 1)
        {
            if (状态消息) Debug.Log(  ("速度改变：速度限为     " + animator.speed + +Time.frameCount)._Color(Color.green ));
            animator.speed = 1; //速度恢复
        }

        animator.Play(anim.name);

        animator.speed = anim.speed;
         anim.enter(); if (name == A_N.pa_to_)
            Debug.LogError("BBBBBBBBBBBBBBBBBB");
        if (状态消息)
            {
                Debug.Log((Time.time +"      上一个名字：" + 当前anim.name + "\n下一个名字" +""+ anim.name+Time.frameCount)._Color(Color.white ));
            }

          当前anim = anim;

        if (Next != null&&Next.name != null)
        {
            if (状态消息) Debug.Log( ("Next置空 " + Next )._Color(Color .white));
            Next = null;
        }
        if (时间界限 != 时间界限默认值)
        {
            if (状态消息)  Debug.Log( ( "提前结束：时间界限为     " + 时间界限 + "</color>")._Color(Color.red));
      
            时间界限 = 时间界限默认值;
        } 
    }
    public void Re ()
    {
        animator.speed = 1;
         时间界限 = 时间界限默认值;
        Next = null;
    }
    public bool Debu;
    public Anim2 GetAnim(string 完整名字)
    { 
        for (int i = 0; i < 所有anim.Count; i++)
        {
            //if (Debu)
            //{
            //    Debug.LogError(所有anim[i].name);
            //}

            if (所有anim[i].name== 完整名字)
            {

                return 所有anim[i];
            }
        }
 
        Debug.LogError(" 没这个名字"+ 完整名字);
        return null;
    }

    public Anim2 GetAnim_Order(int b)
    {
        for (int i = 0; i < 所有anim.Count; i++)
        {
            if (所有anim[i].playerOrder == b)
            {
                return 所有anim[i];
            }
        }

   
        Debug.LogError("查找Order为" +b + "为空");
        return null;
    }
}
public class AniContr3 : AnimBase
{
    private Dash dash;

    public     FSM_Tag fSM_Tag;

    [DisplayOnly]
    public Anim2[] 播放列表_ = new Anim2[7];

    public Anim2[] 播放列表 { get => 播放列表_; set { 播放列表_ = value;  } }

    public Anim2 上一个anim;
    public override void awake()
    {

            fSM_Tag = GetComponent<FSM_Tag>();
            dash = GetComponent<Dash >();

        当前动画为百分之99 += to_如果是过渡动画播放结束发生的事件;

        fSM_Tag.移动事件 += run_移动动画控制;
        fSM_Tag.跳跃事件 += jump_跳跃动画控制; 
        fSM_Tag.下蹲事件 += dun_下蹲动画控制; 
        dash. 播放冲刺动画 += dash_冲刺动画播放;
        dash.冲刺动画结束 += dash_冲刺动画结束;
        //玩家受伤效果.I.播放受击动画 += hIt_受击动画控制;

        播放列表[0] = GetAnim("idle_0_");
    }

    private void hIt_受击动画控制(bool b)
    {
        if (b)
        {
            播放列表[2] = null;
            播放列表[3] = null;
            播放列表[4] = null;
            播放列表[5] = GetAnim("hit");
        }
        else
        {
            播放列表[5] =null;
        }

    }

    private void dash_冲刺动画结束()
    {
        播放列表[4] = 大动画转换(Tag_state3.dash , 播放列表[上一个指针].tag_State);
        print("      播放列表[4] = 大动画转换(Tag_state3.unknown, 播放列表[上一个指针].tag_State);");
    }

    private void dash_冲刺动画播放()
    {

        播放列表[3] = null;
        switch (Player.I.生物数值.当前底层状态)
        {
            case 功能数值.底层状态.idle:
            case 功能数值.底层状态.run:
                播放列表[4] = 大动画转换(Tag_state3.unknown, (Tag_state3.dash));
                播放列表[3] = null;
                break;
            case 功能数值.底层状态.jump:
                播放列表[4] = 大动画转换(Tag_state3.jump, (Tag_state3.skydash));
                播放列表[3] = null;
                break;
            case 功能数值.底层状态.dun:
                播放列表[4] = 大动画转换(Tag_state3.dun, (Tag_state3.dundash));
                播放列表[3] = null;
                break;

        }
    }

    private void dun_下蹲动画控制(E_Dun obj)
    {
        if (obj==E_Dun.dun)
        {
            播放列表[2] = GetAnim("dun_idle_run_to0");
        }
        else if (obj == E_Dun.meidun)
        {
            播放列表[2] = 大动画转换(Tag_state3.dun, Tag_state3.unknown);
        }

        //播放列表[1] = null;

    }

protected  override  void Update()
    {


        Playe(Reverse(播放列表));
        if (播放列表[0]==null)
        {
            Debug.LogError("0位播放为空");
        }
        更新上一指针();
    }


    private void jump_跳跃动画控制(E_Ground obj)
    {

        if (obj == E_Ground.skyUp)
        {
            播放列表[1] = GetAnim("jump_run_up");
        }
        else if (obj == E_Ground.skyMax) 
        {
            播放列表[1] = GetAnim("jump_max_");
        }
        else if (obj == E_Ground.skyDown && Player_input.I.方向正零负 == 0)
        {

            播放列表[1] = GetAnim("jump_rundown");
        }
        else if (obj == E_Ground.skyDown && Player_input.I.方向正零负 != 0)
        {

            播放列表[1] = GetAnim("jump_rundown");
        }
        else if (obj == E_Ground.round && Player_input.I.方向正零负 == 0)
        {

            播放列表[1] = 大动画转换(播放列表[1].tag_State, Tag_state3.idle);
        }
        else if (obj == E_Ground.round && Player_input.I.方向正零负 != 0)
        {

            播放列表[1] = 大动画转换(播放列表[1].tag_State, Tag_state3.run);
        }}
    private void run_移动动画控制(E_Move obj)
    {
        if (obj == E_Move.run )
        {
            播放列表[0] = 大动画转换(Tag_state3.idle, Tag_state3.run);
        }
        else if (obj == E_Move.idle)
        {
            if (当前anim.name=="run_idle_to0"
                &&Player_input.I.方向正零负_非零计时器 <= 0.5f)
            {

                播放列表[0] = GetAnim("idle_0_");

            }
            else
            {
                播放列表[0] = 大动画转换(Tag_state3.run, Tag_state3.idle);
            }
            }

        //else if ( obj == E_Move.idle&&
        //          播放列表[0].start!="idle"
        //    )
        //{
        //    //在锁死移动的情况下玩家移动在松开会这样，否则松开的时候，这个状态会保持在RUN
        //    播放列表[0] = GetAnim("idle_0_");
        //}
 
    }

public  Anim2 大动画转换(Tag_state3 last, Tag_state3 nextStart)
    {
        更新上一指针();
        foreach (var item in 所有anim)
      {
              if (last ==nextStart)
        {
                Debug.LogWarning(last + "试图转自己" );
                
            }
            if (item.Get_fineLastsTag(Tag_state3.anystate))
            {  ///anystate
                if (nextStart.ToString()==item.start)
                {

                    return item;
                }
            }
        ///可以从任意进，或者几个状态进
        ///不知道会从那个状态进
        if (last==Tag_state3.unknown)
        {
                if (item.Get_fineLastsTag(播放列表[当前读列表取指针].tag_State)
                    && nextStart.ToString ()== item.start
                    )
                {
                    return item;
                }
        }
        ///可以从任意出，或者几个状态出
        ///不知道会从那个状态
       else  if (nextStart == Tag_state3.unknown)
        {
                if (item.Get_fineLastsTag(last ) && 播放列表[上一个指针].tag_State== item.tag_State)
                {
                 
                    return item;
                }
        }

       else if (item.start== nextStart.ToString())
            {//item.start==run
                //if (last==Tag_state3.run&&nextStart== Tag_state3.idle)
                //{
                    //Debug.LogError("Tag_state3.run, Tag_state3.idle"); 
                //}
                if (item.Get_fineLastsTag(last))
                {
                    //if (last==Tag_state3.run&&nextStart == Tag_state3.idle )
                    //{
                    //Debug.LogError("Tag_state3.run, Tag_state3.idle                   Tag_state3.run, Tag_state3.idle");

                    //}
                    //当run状态跳过END切换到IDLE会出现BUG（已经修复
                    return item;
                    //找到返回的了
                }

         }
      }
        Debug.LogWarning("返回空值");
        return null;
    }


    Tag_state3 _State3;


    private void to_如果是过渡动画播放结束发生的事件()
    {
        更新上一指针();

        if (当前anim.end != null && 当前anim.next != -999)
        {
            foreach (Anim2 anim in 所有anim)
            {
                if (当前anim.removeMysefly == false)
                {//正常顺下去
                    if (anim.start==当前anim.start)
                            {
                                if (anim.playerOrder == 当前anim.next)
                                       {//如果有过渡动画，如果没有过渡动画，如果一致
                                       播放列表[当前anim.正常所属] = anim;
                                        break;
                                        }
                             }
                }
                else if(当前anim.removeMysefly == true)
                {
//是过度动画
                    if (当前anim.start==播放列表[上一个指针].start)
                    {//一样

                        播放列表[当前anim.上个状态的列表位置] = null;

                        if (anim.start == 当前anim.start)
                        {
                            if (anim.playerOrder == 当前anim.next)
                            {//如果有过渡动画，如果没有过渡动画，如果一致

                                if (状态消息)
                                {
                                    Debug.Log(当前anim.正常所属 + "" + 播放列表[当前anim.正常所属].name + "    " + anim.name);
                                }
                                播放列表[当前anim.正常所属] = anim;
                                break;
                            }
                        }
                    }
                    
                    else if(当前anim.start != 播放列表[上一个指针].start)
                    {//不一样 ，过渡
                        if (大动画转换(当前anim.tag_State, 播放列表[当前anim.正常所属].tag_State)!=null)
                        {//找到了
                            播放列表[当前anim.正常所属] = 大动画转换(当前anim.tag_State, 播放列表[当前anim.正常所属].tag_State);
                            播放列表[当前anim.上个状态的列表位置] = null;
                            break;
                        }
                        else if (大动画转换(当前anim.tag_State, 播放列表[当前anim.正常所属].tag_State) == null)
                        {//没找到
                            播放列表[当前anim.上个状态的列表位置] = null;
                            break;
                        }
                    }
                }
            }
        }


        if (当前anim.end == "_toother")
        {
            更新上一指针();


            播放列表[当前anim.正常所属] = 大动画转换(当前anim.tag_State, 播放列表[上一个指针].tag_State);
        }
        if (当前anim.end == "_null")
        {
            播放列表[当前anim.正常所属] = null;
        }
    }

    public int 当前读列表取指针;
    public  int 上一个指针;

     
 Anim2 Reverse(Anim2[] anims)
    {
        if (anims == null)
        {
            Debug.LogError("数组为空");
            return 当前anim;
        }
        for (int i = anims.Length - 1; i >= 0; i--)
        {

            if (anims[i] != null
                  && anims[i].clip != null
                && anims[i].name != null
                )
            {
                当前读列表取指针 = i;

                return anims[i];
            }
        }

        return null;

    }
    public void 更新上一指针()
    {
        for (int a = 当前读列表取指针 - 1; a >= 0; a--)
        {
            if (播放列表[a]!=null
              &&  播放列表[a].clip!=null)

            {

                上一个指针 = a;
                break;
            }
        }
    }

    public int IIIIIIIII;

}

//public class AniContr3 : MonoBehaviour
//{
//    public List<Anim2> 所有anim;

//    FSM_Tag fSM_Tag;
//    AnimationClip[] 所有的clips;
//    public Action 当前动画为百分之99;


//    Animator animator;
//    public Anim2[] 播放列表 = new Anim2[5];

//    public Anim2 当前anim;
//    public Anim2 上一个anim;
//    private void Awake()
//    {
//        fSM_Tag = GetComponent<FSM_Tag>();
//        animator = transform.GetComponent<Animator>();
//        所有的clips = animator.runtimeAnimatorController.animationClips;

//        foreach (Tag_state3 v in Enum.GetValues(typeof(Tag_state3)))
//        {
//            Debug.LogError(TAG.位数转换((int)v));
//        }

//        foreach (var item in 所有的clips)
//        {
//            所有anim.Add(new Anim2(item));
//        }

//        当前动画为百分之99 += to_如果是过渡动画播放结束发生的事件;
//        fSM_Tag.移动事件 += run_移动动画控制;
//        fSM_Tag.跳跃事件 += jump_跳跃动画控制;
//        Player_input.KeyDown += atk_攻击动画控制;

//        播放列表[0] = GetAnim("idle_0_");
//    }
//    void Update()
//    {
//        检测当前播放动画进度();
//        Playe(Reverse(播放列表));
//        ASDASDASD();
//    }

//    private void atk_攻击动画控制(KeyCode obj)
//    {
//        if (obj != Player_input.I.攻击) return;
//        播放列表[2] = 大动画转换(Tag_state3.unknown, Tag_state3.atk);
//    }

//    private void jump_跳跃动画控制(E_Ground obj)
//    {
//        int I = 1;
//        if (obj == E_Ground.skyUp)
//        {
//            播放列表[1] = 大动画转换(Tag_state3.unknown, Tag_state3.jump);
//        }
//        else if (obj == E_Ground.skyMax)
//        {
//            播放列表[1] = GetAnim("jump_max_");
//        }
//        else if (obj == E_Ground.skyDown && Player_input.I.方向正零负 == 0)
//        {
//            播放列表[1] = GetAnim("jump_idledown");
//        }
//        else if (obj == E_Ground.skyDown && Player_input.I.方向正零负 != 0)
//        {
//            播放列表[1] = GetAnim("jump_rundown");
//        }
//        else if (obj == E_Ground.round && Player_input.I.方向正零负 == 0)
//        {
//            print("1" + 播放列表[1].tag_State + Tag_state3.idle);
//            播放列表[1] = 大动画转换(播放列表[1].tag_State, Tag_state3.idle);
//        }
//        else if (obj == E_Ground.round && Player_input.I.方向正零负 != 0)
//        {
//            print("2" + 播放列表[1].tag_State + Tag_state3.run);
//            播放列表[1] = 大动画转换(播放列表[1].tag_State, Tag_state3.run);
//        }
//    }

//    private void run_移动动画控制(E_Move obj)
//    {
//        if (obj == E_Move.runstart)
//        {

//            播放列表[0] = 大动画转换(Tag_state3.idle, Tag_state3.run);

//        }
//        else if (obj == E_Move.runend)
//        {

//            播放列表[0] = 大动画转换(Tag_state3.run, Tag_state3.idle);
//        }
//    }

//    private Anim2 大动画转换(Tag_state3 last, Tag_state3 nextStart)
//    {
//        foreach (var item in 所有anim)
//        {
//            if (item.上一个 == "anystate")
//            {
//                if (nextStart.ToString() == item.start)
//                {
//                    return item;
//                }
//            }
//            ///可以从任意进，或者几个状态进
//            ///不知道会从那个状态进
//            if (last == Tag_state3.unknown)
//            {
//                if (item.上一个 == 播放列表[当前读列表取指针].start
//                    && nextStart.ToString() == item.start
//                    )
//                {
//                    return item;
//                }
//            }
//            ///可以从任意出，或者几个状态出
//            ///不知道会从那个状态
//            else if (nextStart == Tag_state3.unknown)
//            {
//                if (item.上一个 == 播放列表[上一个指针].start)
//                {
//                    return item;
//                }
//            }

//            else if (item.start == nextStart.ToString())
//            {//item.start==run

//                if (item.上一个 == last.ToString())
//                {
//                    return item;
//                }

//            }
//        }
//        return null;
//    }


//    Tag_state3 _State3;


//    private void to_如果是过渡动画播放结束发生的事件()
//    {
//        if (当前anim.end != null && 当前anim.next != -999)
//        {
//            foreach (Anim2 anim in 所有anim)
//            {
//                if (anim.start == 当前anim.start)
//                {
//                    if (anim.playerOrder == 当前anim.next)
//                    {
//                        print("anim.playerOrder" + anim.playerOrder + "当前anim.next" + 当前anim.next);
//                        播放列表[当前anim.正常所属] = anim;
//                    }
//                }
//            }
//        }
//        else
//        {
//            //print("2222222222222222222222222");
//            //播放列表[当前anim.正常所属] = 大动画转换(当前anim.tag_State, 播放列表[上一个指针].tag_State);
//        }

//        if (当前anim.removeMysefly == true
//            && 当前读列表取指针 > 0
//            )
//        {
//            print("当前anim.列表位置" + 当前anim.列表位置);
//            播放列表[当前anim.列表位置] = null;
//        }
//        if (当前anim.end == "_toother")
//        {
//            播放列表[当前anim.正常所属] = 大动画转换(当前anim.tag_State, 播放列表[上一个指针].tag_State);
//        }
//    }



//    int I;
//    public int 当前读列表取指针;
//    public int 上一个指针;

//    void 检测当前播放动画进度()
//    {
//        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99999)
//        {
//            I++;
//        }
//        else
//        {
//            I = 0;
//        }
//        if (I == 1)
//        {
//            当前动画为百分之99?.Invoke();
//        }

//    }


//    void Playe(Anim2 anim)
//    {

//        if (当前anim == anim)
//            return;
//        当前anim.exit();
//        animator.Play(anim.name);
//        anim.enter();
//        当前anim = anim;
//    }

//    Anim2 GetAnim(string 完整名字)
//    {
//        foreach (var item in 所有anim)
//        {
//            if (完整名字 == item.name)
//            {
//                return item;
//            }
//        }

//        Debug.LogError(" 没这个名字");
//        return null;
//    }
//    Anim2 Reverse(Anim2[] anims)
//    {
//        if (anims == null)
//        {
//            Debug.LogError("数组为空");
//            return 当前anim;
//        }
//        for (int i = anims.Length - 1; i >= 0; i--)
//        {

//            if (anims[i] != null
//                  && anims[i].clip != null
//                && anims[i].name != null
//                )
//            {
//                当前读列表取指针 = i;

//                return anims[i];
//            }
//        }

//        return null;

//    }
//    public void ASDASDASD()
//    {
//        for (int a = 当前读列表取指针 - 1; a >= 0; a--)
//        {
//            if (播放列表[a].clip != null
//        )
//            {
//                上一个指针 = a;
//                break;
//            }
//        }
//    }
//}
