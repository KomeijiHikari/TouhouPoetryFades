using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Player_input;

public class 按键子类 : MonoBehaviour
{
    Text t;
    public string Nam;
    public KeyCode k;
    Text_button T;
    private void Awake()
    {
        T = GetComponent<Text_button>();
        t = GetComponent<Text>();

        T.Enter .AddListener(进入了)  ;
    }
 public void 进入了()
    {
        按键父类.I.j.WhatKey += (KeyCode k) => {
            按键父类.I.D[Nam] = k;
        };
       
    }
    private void OnEnable()
    {
        if (按键父类.I!=null) 
        刷新();
    }
    private void Start()
    {
        刷新(); 
    }
    private void 刷新()
    {
        object O;
        if (按键父类.I.D.TryGetValue(Nam, out O))
        {
            k = (KeyCode)O;

            t.text = Nam + "  " + k.ToString(); 
        }
        else
        {
            ///空值
            t.text = "没有找到";
        }


    }
}
