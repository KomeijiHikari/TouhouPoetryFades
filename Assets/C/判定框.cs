using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class 判定框 : 判定框Base
{
    Biology master;

    public 玩家攻击数据 上 = new(new Vector2(3.98f, -0.48f), new Vector2(5, 4.34f), Tag_Super_state.skyatk, 1.5f);
    public 玩家攻击数据 中=new(new Vector2(3.98f, 0.88f), new Vector2(5,1.62f),Tag_Super_state.atk,1f);
    public 玩家攻击数据 下 = new(new Vector2(3.98f, -1.3f), new Vector2(5, 1.62f), Tag_Super_state.dunatk,1.1f);


    protected   override   void Awake()
    {
        base.Awake();
 


       master = GetComponentInParent<Biology>();

        if (master==null)
        {
            Debug.LogError("没有一个爹");
        }
    }

    private void Start()
    {
        bc.isTrigger = true;
        SetBox(中);
        Anim_Action.I.ATK_Action += 选择;
        开启判定框判定框(false);
    }

public  override    void 开启判定框判定框(bool b,Behaviour B=null)
    { 
        bc.enabled = b;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<I_生命>()?.被扣血(当前动作值1*Player.I.atkvalue, Player.I.gameObject, 0);
    }
    public void SetBox( 玩家攻击数据  a)
    {
        当前动作值1 = a.动作值;
        bc.offset = a.Position;
        bc.size = a.Size;
    }
    [DisplayOnly]
    [SerializeField]
    float 当前动作值=1;

    public float 攻击判定箱检测时间 = 0.2f;

    public float 当前动作值1 { get => 当前动作值; set => 当前动作值 = value; }

    private void 选择(Anim2 obj)
    {
        switch (obj.tag_State.ToString())
        {
            case "atk":
                SetBox(中);
                break;
            case "dunatk":
                SetBox(下);
                break;
            case "akyatk":
                SetBox(上);
                break;
        }
        StartCoroutine(IE_ATK(攻击判定箱检测时间));
    }



}

[System.Serializable]
public struct 玩家攻击数据
{
    Tag_Super_state tag_ { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public float  动作值 { get; set; }

    public 玩家攻击数据(Vector2 position, Vector2 size, Tag_Super_state t,float 动作值_)
    {
        Position = position;
        Size = size;
        tag_ = t;
        动作值 = 动作值_;
    }
}