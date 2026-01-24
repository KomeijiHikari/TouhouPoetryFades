using Schema.Internal.Types;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Windows;
public class 键位提示转换 : UnityEngine.MonoBehaviour
{
    public Player_input.IK.IK_Name e;

    public string UnityEvent_String;
    public string UnityEvent键位提示()
    { 
        return 键位提示(UnityEvent_String);
    }
    public static string   键位提示(string s, Player_input.IK.IK_Name e)
    {
        return s.Replace("#", 按键D.GetString(e));
    } 
    [Button]
    public string 键位提示(string s )
    { 
        return 键位提示(s,e); 
    } 
}
