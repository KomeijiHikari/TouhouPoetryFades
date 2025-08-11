using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

    public enum E_Move
    {
        idle,
    runstart,
        run,
        runend,
    turn
    }
    public enum E_Ground
    {
       round,
       skyUp,
    skyMax,
    skyDown

    }
public enum E_Dash
{
    dash,
    candash,
    cool
}
public enum E_Dun
{
    dun,meidun
}
public enum E_Atk
{
    atk,meiatk
}
public class FSM_Tag : MonoBehaviour
{
    ATK aTK;

    public Action<E_Move> 移动事件 { get; set; }
    public Action<E_Ground> 跳跃事件 { get; set; }
    public Action<E_Dun> 下蹲事件 { get; set; }

    public float   jump_frist = 1;
    public float    jump_last= 1;

    public float turnKeepTime = 0;
    [DisplayOnly]
    float turnKeepTime_;

    [SerializeField]
    public E_Dash e_Dash = E_Dash.candash;
    [SerializeField]
 public    E_Atk e_Atk = E_Atk.meiatk;
    [SerializeField]
    E_Dun e_Dun_ = E_Dun.meidun;
    public E_Dun e_Dun
    {
        get
        {
            return e_Dun_;
        }
        set
        {
            if (e_Dun_ != value)
            {

                //e_Move_ = value;
                下蹲事件?.Invoke(value);
            }
            e_Dun_ = value;
        }
    }
  [SerializeField]
  E_Move e_Move_ = E_Move.idle;
    public E_Move e_Move
    {
        get
        {
            return e_Move_;
        }
        set
        {
            if (e_Move_!=value)
            {
                移动事件?.Invoke(value);
                if (Player.I.Ground)
                {
                    if (e_Move_ == E_Move.run && value == E_Move.idle)
                    {

                        //if (Player_input.I.方向正零负_非零计时器 >= 5f)
                        //{
                        //    Player.I.Set_velocity(Vector2.right * Player.I.朝向 * 10);

                        //}
                        //else if (Player_input.I.方向正零负_非零计时器 >= 1f)
                        //{
                        //    Player.I.Set_velocity(Vector2.right * Player.I.朝向 * 5);

                        //}
                        //else
                        //{
                        //    Player.I.Set_velocity(Vector2.right * Player.I.朝向 * 3);

                        //}
                    }
                    else if (e_Move_ == E_Move.idle && value == E_Move.run)
                    {
                        Player.I.Velocity=(Vector2.zero);

                    }
                }

       
                移动事件?.Invoke(value);
            }
            e_Move_ = value;
        }
    }
    [SerializeField]
   E_Ground e_Ground_ = E_Ground.round;
    public E_Ground e_Ground
    {
        get
        { 
            return e_Ground_;
        }
        set
        {
            if (e_Ground_ != value) 
            {

                跳跃事件?.Invoke(value);
            }
            e_Ground_ = value;
        }
    }


 
    private void Start()
    {
        Player_input.I.方向变动 += 转身计时器;
        aTK = GetComponent<ATK>();


    }

 void    攻击标签更新()
    {
        if (aTK == null) return;

        if(aTK.攻击了吗)
        {
            e_Atk = E_Atk.atk;
        }
        else if (!aTK.攻击了吗)
        {
            e_Atk = E_Atk.meiatk;
        }


    }
    public void 移动标签更新()
    {
        if (Player_input.I.方向正零负 != 0
            || Player.I.假正负0!=0)
        {

            e_Move = E_Move.run;
        }
        else
        {
            e_Move = E_Move.idle;
        }
    }
    //public void 移动标签更新()

    //{
    //    if (
    //       // Player.I.rb.velocity.x == 0
    //       //&&
    //       Player_input.I.方向正零负 == 0
    //        )
    //    {

    //        e_Move = E_Move.idle;

    //    }
    //    else if (
    //          //MathF.Abs(Player.I.rb.velocity.x) == Player.I.生物数值.最大速度
    //          Player_input.I.方向正零负 != 0
    //          //&&
    //          //MathF.Abs(Player.I.rb.velocity.x)>=3.5f
    //        )
    //    {

    //        e_Move = E_Move.run;
    //    }
    //    //else if (Player_input.I.方向正零负 != 0
    //    //    && MathF.Abs(Player.I.rb.velocity.x) < Player.I.生物数值.最大速度
    //    //    //||(Player_input.I.方向正零负 != 0&&
    //    //    )
    //    //{

    //    //    e_Move = E_Move.runstart;

    //    //}
    //    //else if (
    //    //    Player_input.I.方向正零负 == 0
    //    //    && MathF.Abs(Player.I.rb.velocity.x) < Player.I.生物数值.最大速度
    //    //    && MathF.Abs(Player.I.rb.velocity.x) > 1
    //    //    //  ||
    //    //      //Player_input.I.方向正零负==0
    //    //    )
    //    //{

    //    //    e_Move = E_Move.runend;
    //    //    //刚体在到0后还会到一个朝向相反的力（原因不明
    //    //}
    //    else if (Time.time <= turnKeepTime_ && MathF.Abs(Player.I.rb.velocity.x) < Player.I.生物数值.最大速度)
    //    {
    //        e_Move = E_Move.turn;
    //    }
    //    else
    //    {
    //        //Debug.Log("这是什么移动状态");
    //    }


    //}

    public void 下蹲标签更新()
    {
        if (Player.I.下蹲表示)
        {
            e_Dun = E_Dun.dun;
        }
        else
        {
            e_Dun = E_Dun.meidun;
        }
            
    }
    public void 跳跃标签更新()
    {
        if (Player.I.Ground == true)
        {
            e_Ground = E_Ground.round;
        }
        else if (Player.I.Velocity.y > jump_frist)
        {
            e_Ground = E_Ground.skyUp;
        }
        else if (Player.I.Velocity.y <= jump_frist && Player.I.Velocity.y >= jump_last)
        {

            e_Ground = E_Ground.skyMax;
        }
        else if (Player.I.Velocity.y < jump_last)
        {
            e_Ground = E_Ground.skyDown;
        }
        else
        {
            Debug.Log("这是什么跳跃状态");
        }

    }
 

    private void Update()
    {
        if (Player_input.I.方向正零负计时器 >= 0.9f
               ||Player_input.I.玩家输入的按键存储_按住.Count>=1
               ) 
        {
            Player.I.Linear=(0f);
        }
        移动标签更新();
        跳跃标签更新();
        下蹲标签更新();
        攻击标签更新();
    }



    /// <summary>
    /// 该状态的多少时间内会被判定为转身状态
    /// </summary>
    /// <param name="i"></param>
    public void 转身计时器(int i)
    {
        turnKeepTime_ = Time.time + turnKeepTime;
        //Debug.Log("转身");
    }
}
