using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class 按键计时器显示 : MonoBehaviour
{
    public Text text;
    public Text text2;
    public Text text3;
    public Text text4;
    public Text text5;

    public void ANYKEY()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    Debug.LogError("Current Key is : " + keyCode.ToString());
                }
            }

        }
    }
    private void Update()
    {
        text.text = 三个数字(0);
        text2.text = 三个数字(1);
        text3.text = 三个数字(2);
        text4.text = 三个数字(3);
        text5.text = 三个数字(4);
    }
    //+ ":" +
    //(Player_input.D_I?[Player_input.Instance_player_Input.玩家输入的按键存储_按下[1]]?.Keytime).ToString();






    public string 三个数字(int i)
    {


        string a = Player_input.I.玩家输入的按键顺序_显示.Count > i ?
               ((Player_input.I.D_I?[Player_input.I.玩家输入的按键顺序_显示[i]].my_Key).ToString()) + ":" +
                (Player_input.I.D_I?[Player_input.I.玩家输入的按键顺序_显示[i]]?.KeytimeDown).ToString() :
                       "null";
        string b = Player_input.I.玩家输入的按键顺序_显示.Count > i ?
            ((Player_input.I.D_I?[Player_input.I.玩家输入的按键顺序_显示[i]].my_Key).ToString()) + ":" +
             (Player_input.I.D_I?[Player_input.I.玩家输入的按键顺序_显示[i]]?.Keytime).ToString() :
                   "null";
        string c = Player_input.I.玩家输入的按键顺序_显示.Count > i ?
            ((Player_input.I.D_I?[Player_input.I.玩家输入的按键顺序_显示[i]].my_Key).ToString()) + ":" +
            (Player_input.I.D_I?[Player_input.I.玩家输入的按键顺序_显示[i]]?.KeytimeUp).ToString() :
                   "null";
        string d = Player_input.I.玩家输入的按键顺序_显示.Count > i ?
            ((Player_input.I.D_I?[Player_input.I.玩家输入的按键顺序_显示[i]].my_Key).ToString()) + ":" +
            (Player_input.I.D_I?[Player_input.I.玩家输入的按键顺序_显示[i]]?.Keeptime).ToString() :
                    "null";
        return a + "\n" + b + "\n" + c + "\n" + d;
    }
}
