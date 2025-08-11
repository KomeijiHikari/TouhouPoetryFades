using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Anim_Action : MonoBehaviour
{
    FSM_Tag Fsm_Tag;
    public static   Anim_Action I { get; private set; }

    public  判定框 pd; 
    Animator animator;


    public Action<Anim2> ATK_Action { get; set; }
    public Action<bool> Zero { get; set; }
    public Action 空中开冲 { get; set; }
    public Action<float> LOCKtime { get; set; }


    float 最大水平速度容器;
    float 起步水平速度容器;



    public Action<int, int> 攻击的间隙 { get; set; }

    //[RuntimeInitializeOnLoadMethod]
    //static void start()
    //{
    //    I = null;


    //}
    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(this);
        }
        else
        {
            I = this;
        }
        Fsm_Tag = GetComponentInChildren<FSM_Tag>();
        pd = GetComponentInChildren<判定框>();
        animator = transform.GetComponent<Animator>();
        攻击的间隙 += 攻击间隙;


    }
    private void Start()
    {
        最大水平速度容器 = Player.I.生物数值.常态速度;
        起步水平速度容器 = Player.I.生物数值.起步速度;
    }
    private void Update()
    {

        if (Fsm_Tag.e_Dun == E_Dun.dun)
        {
            Player.I.跳跃开关 = false;
            Player.I.移动开关 = false;
            if (Fsm_Tag.e_Atk==E_Atk.atk)
            {
                Player.I.翻转开关 = false;
            }
            else if(Fsm_Tag.e_Atk == E_Atk.meiatk)
            {
                Player.I.翻转开关 =true;
            }

        }
        else if (Fsm_Tag.e_Dun == E_Dun.meidun)
        {
            Player.I.跳跃开关 = true;
            Player.I.移动开关 = true;
            if (Fsm_Tag.e_Atk == E_Atk.atk)
            {
                Player.I.跳跃开关 = false;
                Player.I.移动开关 = false;
                Player.I.翻转开关 = false;
            }
            else if (Fsm_Tag.e_Atk == E_Atk.meiatk)
            {
                Player.I.翻转开关 =true;
             //if (Time.time - Player_input.I.D_I[Player_input.I.攻击].Keytime < 1)
             //   {
                    Player.I.角色翻转更新();
                //}

            }
        }
    }

 

    public void LOCKTIME(float f)
    {

        StartCoroutine(IE_LOCK(f));
    }

    IEnumerator IE_LOCK(float time)
    {
        if (能力开着的嘛)
        {
            ALL_LOCK(true);
        }
 
        yield return new WaitForSeconds(time);
        if (!能力开着的嘛)
        {
        ALL_LOCK(false);

        }
        //Player_input.I.检查是否一致(Player.I.朝向);
        //LOCKTIME = 0;
    }
    public void 攻击间隙(int a,int b)
    {
        Player.I.角色翻转更新();
        Debug.LogWarning("攻击间隙");
    }


    public bool 能力开着的嘛 =false;
    public  void ALL_LOCK( bool b)
    {

        if (b)
        {
            能力开着的嘛 = false;
            Player.I.跳跃开关 = false;
            Player.I.翻转开关 = false;
            //   player.玩家数值.最大速度 =0;
            //player.玩家数值.起步速度 = 0;
            Player.I.移动开关 = false;



            ///原因是TAG那里直接切到IDLE动画控制哪里无法正确处理信息
        }
        else
        {
            能力开着的嘛 = true;
            Player.I.跳跃开关 = true;
            Player.I.翻转开关 = true ;
            Player.I.移动开关 =true;
            //player.玩家数值.起步速度 = 起步水平速度容器;
            //player.玩家数值.最大速度  = 最大水平速度容器;
        }
    }

    private void LateUpdate()
    {

    }
}
