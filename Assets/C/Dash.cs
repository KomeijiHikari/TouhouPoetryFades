using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  enum E_dash
{
    下铲,
    常态,
    空中
}

[System.Serializable]
public class DASH
{
    [HideInInspector]
    public    E_dash my;
    DASH()
    {

    }
    public DASH(float 冲刺持续时间, float 冲刺速度, float 基础冲刺速度, float 冲刺冷却时间, E_dash my)
    {
       this. 冲刺持续时间 = 冲刺持续时间;
        this.冲刺速度 = 冲刺速度;
        this.基础冲刺速度 = 基础冲刺速度;
        this.冲刺冷却时间 = 冲刺冷却时间;
        this.my = my;

    }
public  float 冲刺持续时间_;

    public float 冲刺持续时间 = 0.2f;
    public float 冲刺速度 = 40f;
    public float 基础冲刺速度 = 5f;
    public float 冲刺冷却时间 = 2f;

    public bool 冲刺锁=true;
    [SerializeField]
    bool 冷却好了_ =true;
    public bool 冷却好了
    {
        get
        {
            if (冲刺锁)
            {
                return 冷却好了_ ;
            }
            else
            {
                return false;
            }

        }
        set
        {
            //if (冷却好了_==false &&value ==true)
            //{
            //    Debug.LogError("改变改变改变改变改变改变改变改变改变");
            //}
            冷却好了_  = value;
        }
    }
    [SerializeField]
     bool 冲刺显示_ = false;
    public bool 冲刺显示
    {
        get { return 冲刺显示_; }
        set
        {
            if (冲刺显示_ != value)
            {
                if (冲刺显示_  && !value)
                {
                    //Player.I.Linear = 3; 
                }
            }
            冲刺显示_ = value;
        }
    }
    public void DeBUg()
    {
        Debug.LogError (冲刺持续时间 + "冲刺持续时间" + 冲刺速度 + "冲刺速度" + 基础冲刺速度+ "基础冲刺速度" + 冲刺冷却时间 + "冲刺冷却时间"  );
    }
    public bool 发力显示 { get; set; }
}
public class Dash : MonoBehaviour
{
    public Action 播放冲刺动画 { get; set; }
    public Action 冲刺动画结束 { get; set; }

 public DASH 下蹲dASH ;
    public DASH dASH;
    public DASH 空中dASH;
    void Start()
    {
        下蹲dASH.my = E_dash.下铲;
        下蹲dASH.my = E_dash.常态;

        Player.I.出了洞 += 出了洞;
        Anim_Action.I.Zero += 归零;
        Anim_Action.I.空中开冲 += 空中开冲;
    }

    private void 空中开冲()
    {
        输入DASH数据(空中dASH);
    }

    public bool 归零开关=false;

    private void 归零(bool b)
    {
        归零开关 = b;
    }

    public bool 进入自动滑铲模式力 { get; private  set; }
    void 出了洞()
    {
        if (!下蹲dASH.冲刺显示)
        {
            进入自动滑铲模式力 = false;
            StartCoroutine(某冲刺结束(下蹲dASH));
            Player.I.下蹲恢复到其他状态();
        }
    }
    private void 结束下铲()
    {

  
        if (下蹲dASH.冲刺显示)
        {
            进入自动滑铲模式力 = false;
            StartCoroutine(某冲刺结束(下蹲dASH));
            Player.I.下蹲恢复到其他状态();
        }

        //Player.rb.AddForce(new Vector2(20, 0) * Player.朝向,ForceMode2D.Impulse);
    }
    void 下蹲dash结束后没有按下蹲跳出()
    {
        if (Player.I.下蹲表示)
        {
            if (!下蹲dASH .冲刺显示)
            {
            if (!Player_input.I.按键检测_按住(Player_input.I.下))
            {
                if (Player.I.头空_)
                {
                    Player.I.下蹲恢复到其他状态();
                }

            }

            }
        }
    }
    void 进入自动滑铲模式了()
    {
        if (进入自动滑铲模式力)
        {
            if (Player.I.下蹲表示 || 下蹲dASH.冲刺显示)
            {

                if (!Player.I.前空_)
                {//前面有东西
                    Debug.LogError("ASSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS");
                    Player.I.LocalScaleX_Int = -Player.I.LocalScaleX_Int;
                    Player.I.transform.localScale = new Vector2(-Player.I.transform.localScale.x, Player.I.transform.localScale.y);
                }
            }

            if (Player .I .头空_)
            {
                结束下铲();
            }
        }
    }
    void 撞到墙或者腾空跳出()
    {
        if (Player.I.下蹲表示 || 下蹲dASH.冲刺显示)
        {
            if (!Player.I.Ground
                || Player.I.头空_ && !Player.I.前空_
                )
            {
                结束下铲();

            }
        }
    }
    private void Update()
    {
 

        if (Player_input.I.D_I [Player_input.I.冲刺].no_State >= 0.9f
          || Player_input.I.玩家输入的按键存储_按住.Count >= 1
          )
        {
            Player.I.Linear=0f;
        }
        if (!进入自动滑铲模式力)
        {
            //下蹲dASH.发力显示 = false;
            //Debug.LogError("ASSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS");
        }
        else
        {
            //下蹲dASH.发力显示 = true ;
        }


        //dASH.发力显示 = false;
    }

    public bool 空中DASH行为 { get; set; }
    private void FixedUpdate()
    {
        Dash_(Player.I.LocalScaleX_Int, dASH);
        Dash_(Player.I.LocalScaleX_Int, 下蹲dASH);
        Dash_(Player.I.LocalScaleX_Int, 空中dASH);
    }

    private void LateUpdate()
    {

        下蹲dash结束后没有按下蹲跳出();
        进入自动滑铲模式了();
        撞到墙或者腾空跳出();

        if (Player_input.I.按键检测_按下(Player_input.I.冲刺))
        {


            switch (Player.I.生物数值.当前底层状态)
            {
                case 功能数值.底层状态.idle:
                case 功能数值.底层状态.run:
                    输入DASH数据(dASH);
                    Player.I.冲刺表示 = true;
                    break;
                case 功能数值.底层状态.jump:
                    if ( 空中dASH.冷却好了)
                    {
                        Player.I.冲刺表示 = true;
                        空中DASH行为 = true;
                           播放冲刺动画?.Invoke();
                        Player.I.速度限制开关 = false;
                        Player_input.I.输入开关 = false;
                    }
       
                    break;
                case 功能数值.底层状态.dun:
                    Player.I.冲刺表示 = true;
                    输入DASH数据(下蹲dASH);
                    break;
            }
        } 
        if (归零开关)
        {
            Player.I.Velocity = Vector2.zero; 
        }
    }
    void 输入DASH数据(DASH  当前dASH)
    {
        if (!当前dASH.冷却好了) return;
        if (Player.I.生物数值.当前底层状态!=功能数值.底层状态.jump)
        {
            播放冲刺动画?.Invoke();
        }

        StartCoroutine(进入某冲刺模式( 当前dASH));

    }


    IEnumerator 进入某冲刺模式(DASH 当前dASH)
    {
        当前dASH.冷却好了 = false;
        //残影.I.开启残影(true);
        当前dASH.冲刺显示 = true ;
        当前dASH.发力显示 = true;
        Player_input.I.输入开关 = false;
        当前dASH.冲刺持续时间_ = 当前dASH.冲刺持续时间;
        //Debug.LogError(当前dASH.冲刺持续时间_+" yield return new WaitForSeconds(当前dASH.冲刺持续时间);1111111111111111111111");
        yield return new WaitForSeconds(当前dASH.冲刺持续时间);
        Debug.Log(" yield return new WaitForSeconds(当前dASH.冲刺持续时间);1111111111111111111111");
        if (!Player.I.头空_&&下蹲dASH.冲刺显示)
        {
            进入自动滑铲模式力 = true;
            Debug.Log("            进入自动滑铲模式力 = true;            进入自动滑铲模式力 = true;            进入自动滑铲模式力 = true;2222222222222222222");
        }
        else
        {
            Debug.Log("" + Player.I.头空_ +"@@@@@" + 下蹲dASH.冲刺显示+"@@@"+""+"            StartCoroutine(某冲刺结束(当前dASH));3333333333333333333333333333333333333333333");
            StartCoroutine(某冲刺结束(当前dASH));
        }


    }
    IEnumerator 某冲刺结束(DASH 当前dASH)
    {
        if (当前dASH.冲刺显示==true)
        {
            残影.I.开启残影(false);
            Player.I.冲刺表示 = false ;
            当前dASH.冲刺显示 = false;
            冲刺动画结束?.Invoke();
            if (空中DASH行为)
            {
                空中DASH行为 = false;
            }
            Player.I.Velocity=Vector2.zero;
            Player_input.I.输入开关 = true;
        }
        yield return new WaitForSeconds(当前dASH.冲刺冷却时间 - 当前dASH.冲刺持续时间);
        当前dASH.冷却好了 = true;
        Debug.LogError("触发触发触发触发触发触发触发触发触发触发触发触发");
    }


    public void Dash_(int 朝向, DASH aSH)
    {
        if (!aSH.冲刺显示) return;
        if (!aSH.发力显示) return;
        aSH.冲刺持续时间_ -= Time.deltaTime;
        if (aSH.冲刺持续时间_<=0)
        {
            aSH.冲刺持续时间_ = 0;
        }
        Player.I.Velocity= new Vector2((朝向 * aSH.冲刺速度 * aSH.冲刺持续时间_ / aSH.冲刺持续时间) + (朝向 * aSH.基础冲刺速度), 0);
 
    }
}


