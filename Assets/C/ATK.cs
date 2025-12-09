

#region

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATK : MonoBehaviour,打印消息, I_I攻击_
{
  
    public bool 状态消息_;
    public bool 状态消息 { get => 状态消息_; set => 状态消息_ = value; }
    public bool 攻击了吗_ = false;
    public bool 攻击了吗
    {
        get { return 攻击了吗_; }
        set
        {
            攻击了吗_ = value;
        }
    }



    public int i_;
    public int i
    {
        get
        {
            return i_;
        }
        set
        {
            if (i != value)
            {
                if (i == 0 && value == 1)
                {
                    进入了攻击?.Invoke();
                    攻击了吗 = true;
                }
                else if (value == 0)
                {
                    退出了攻击?.Invoke();
                    攻击了吗 = false;
                }
                else
                {
                    Anim_Action.I.攻击的间隙?.Invoke(i, value);
                }
            }
            i_ = value;
        }
    }
    AniContr3 AC3;


    public List<Anim2> ATKlist = new List<Anim2>();
    //public List<Anim2>[] ATK_list;

    public Anim2[,] ATK_list;
    public Anim2[,] DUNATK_list;




    Anim2 下一个攻击动画;

    public Action 序列动画播放完毕;

    public int 攻击位置 = 3;

    public Action 进入了攻击 { get; set; }
    public Action 退出了攻击 { get; set; }



    void 恢复空中攻击次数()
    {
        当前空中攻击剩余次数 = 当前空中攻击最大次数;
    }
    private void Start()
    {
        AC3 = GetComponent<AniContr3>();
        AC3.当前动画为百分之99 += 动画结束;
        Player_input.I.KeyDown += 攻击动画控制;
        Player.I.接触地面事件 += 恢复空中攻击次数;

        //进入了攻击 += 进入了攻击ASDASD;
        //退出了攻击 += 退出了攻击ASDASD;
        //攻击的间隙+= 攻击的间隙ASDASD;

        初始化二维(3, 3, out ATK_list, Tag_Super_state.atk);

        初始化二维(2, 2, out DUNATK_list, Tag_Super_state.dunatk);


        PrintAnim2Array(DUNATK_list);
        当前空中攻击最大次数 = 1;
        当前空中攻击剩余次数 = 当前空中攻击最大次数;
    }


    void PrintAnim2Array(Anim2[,] array)
    {
        int rows = array.GetLength(0);
        int columns = array.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Anim2 element = array[i, j];
                //Debug.Log(element.name+"["+ i +", "+j+"]");
            }
        }
    }


    void 初始化二维(int a, int b, out Anim2[,] anim2s, Tag_Super_state super_State)
    {
        anim2s = new Anim2[a, b];
        foreach (var item in AC3.所有anim)
        {
            if (item.tag_State.ToString() == super_State.ToString())
            {
                anim2s[TAG.十位数(item.playerOrder), TAG.个位数(item.playerOrder)] = item;
            }
        }
    }




    private void 动画结束()
    {
        if (AC3.播放列表[攻击位置] == null ||! AC3.播放列表[攻击位置].Is_SuperState() )
        {


            adasd();
        }


    }
    public void adasd()
    {
        if (下一个攻击动画 != null&&
           下一个攻击动画.clip != null)
        {
            //载入下一个攻击动画 
            AC3.播放列表[攻击位置] = 下一个攻击动画;

            下一个攻击动画 = null;
        }
        else
        {
            i = 0;
        }
       

    }
    [DisplayOnly]
    [SerializeField]
    public int 当前空中攻击最大次数 = 1;
    [DisplayOnly]
    [SerializeField]
     int 空次_;
    public int 当前空中攻击剩余次数
    {
        get { return 空次_; }
        set
        {
            if (空次_ <= 0) { 空次_ = 0; };
            if (空次_ >= 当前空中攻击最大次数) { 空次_ = 当前空中攻击最大次数; }
            空次_ = value;
        }
    }



    public bool 攻击第三段锁 = true;
  public  void 攻击()
    {
               switch (Player.I.生物数值.当前底层状态)
        {
            case 功能数值.底层状态.idle:
            case 功能数值.底层状态.run:
                ATK_常态攻击控制();
                break;
            case 功能数值.底层状态.jump:
                if (空次_>0)
                {
                    ATK_空中攻击控制();
    空次_ -= 1;
                }
break;
            case 功能数值.底层状态.dun:
                ATK_下蹲攻击控制();
break;
default:
                break;
        }
    }
 public  void 攻击动画控制(KeyCode obj)
    {
        if (obj != Player_input.I.k.攻击) return; 
        攻击(); 
    }
    private void ATK_下蹲攻击控制()
    {
        if (i == 0)
        {//其他状态第一次按下攻击
            i++;
            AC3.播放列表[攻击位置] = DUNATK_list[0, 0];

            AC3.播放列表[0] = AC3.GetAnim("idle_0_");
        }
        else if (i == 1 && TAG.十位数(AC3.播放列表[攻击位置].playerOrder) == 0 && TAG.个位数(AC3.播放列表[攻击位置].playerOrder) >= 0)
        {

            下一个攻击动画 = DUNATK_list[1, 0];
            i=2;
        }
        else if (i == 2 && TAG.十位数(AC3.播放列表[攻击位置].playerOrder) == 1 && TAG.个位数(AC3.播放列表[攻击位置].playerOrder) >= 0)
        {

            下一个攻击动画 = DUNATK_list[0, 0];
            i = 1;
        }
    }
    void ATK_空中攻击控制()
    {
        if (i == 0)
        {//其他状态第一次按下攻击
            i++;
            AC3.播放列表[攻击位置] = AC3.GetAnim("skyatk_jump_to0");

            AC3.播放列表[0] = AC3.GetAnim("idle_0_");
        }
    }
    void ATK_常态攻击控制()
    {
        if (i == 0)
        {//其他状态第一次按下攻击
            AC3.播放列表[攻击位置] = AC3.大动画转换(Tag_state3.unknown, Tag_state3.atk);
            AC3.播放列表[0] = AC3.GetAnim("idle_0_");
            i++;

        }
        else if (i == 1 && TAG.十位数(AC3.播放列表[攻击位置].playerOrder) == 0 && TAG.个位数(AC3.播放列表[攻击位置].playerOrder) >= 0)
        {//第二段

            下一个攻击动画 = ATK_list[1, 0];
            i++;
        }
        else if (i == 2 && TAG.十位数(AC3.播放列表[攻击位置].playerOrder) == 1 && TAG.个位数(AC3.播放列表[攻击位置].playerOrder) >= 0
         && 攻击第三段锁
            )
        {//第三段
            //if (Player_input.I.方向正零负!=0)
            //{
            //    if (Player.I.朝向== Player_input.I.方向正零负)
            //    {
            //        Debug.LogWarning(" if (Player.I.朝向== Player_input.I.方向正零负)");
            //        //Player.I.rb.AddForce(10000* Player.I.朝向 *Vector2 .right,ForceMode2D.Impulse);
            //        Player.I.rb.velocity = (10000 * Player.I.朝向 * Vector2.right);
            //    }
            //}
            下一个攻击动画 = ATK_list[2, 0];
            i++;
        }
        else if (!攻击第三段锁 && i == 2 && TAG.十位数(AC3.播放列表[攻击位置].playerOrder) == 1 && TAG.个位数(AC3.播放列表[攻击位置].playerOrder) >= 0)
        {
            下一个攻击动画 = ATK_list[0, 0];
            i = 1;
        }

    }



    private void Update()
    {
        if (攻击了吗)
        {
            if (Player_input.I.方向正零负!=0)
            {
                AC3.播放列表[0] = AC3.GetAnim("run_idle_to0");
            }

        }
    }



}



#endregion