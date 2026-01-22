using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
public class One_way_bool : MonoBehaviour, I_暂停, I_碰撞状态
{
    碰撞管理 p;
    [SerializeField]
    private bool 单方向 = false; 
    Vector2 Last; 

    BoxCollider2D bc; 

    public bool Deb;
    private void Awake()
    {
        gameObject.组件(ref p);
    }
    private void Start()
    {
        Initialize.组件(gameObject, ref bc);
         
        Last = transform.position; 
    }
    [SerializeField]
    private bool 有人1;
    public Action<bool> Enter_Exite;
    [SerializeField]
    private bool 暂停1;
     
    public bool 有人
    {
        get => 有人1; set
        {

            if (有人1 != value)
            {
                Enter_Exite?.Invoke(value);
                有人1 = value;
            }
        }
    } 
    public bool 暂停
    {
        get
        { 
            return 暂停1;
        }

        set
        {
            暂停1 = value;
        }
    }

    public E_碰撞状态 E_碰撞 { get => e_碰撞; set => e_碰撞 = value; }

    int count = 0;
    [SerializeField][DisplayOnly] E_碰撞状态 e_碰撞;

    void Update()
    {
        if (暂停) return;

        if (bc.IsTouching(Player3.I.co))
        {
            有人 = true;
        }
        BoxCollider2D min = Player3.I.最低点();
        var AAA = (Vector2)min.bounds.min;
        var P = min.bounds.min.y;
        var m = bc.bounds.max.y;

        if (P > m) count = 0;
        if (Player3.I.Velocity.y > 2.5f || (P < m))
        {
            if (count == 0)
            {
                ///跳跃的动态碰撞   落地一帧会 穿到地里面
                count++;
                return;
            }

            ///玩家在平台下面
            //玩家上升
            if (有人)
            {
                有人 = false;
            }
            if (E_碰撞 != E_碰撞状态.无碰撞) 
            E_碰撞 = E_碰撞状态.触发器;
            if (Deb) Debug.LogError("VVVVVVVVVVVVVVVVVVV+");
        }
        else if (Player3.I.Velocity.y < -0.1)
        {
            //玩家下降
            if (P > m)
            {
                if (E_碰撞 != E_碰撞状态.无碰撞)
                    E_碰撞 = E_碰撞状态.碰撞; 
                if (Deb) Debug.LogError("VVVVVVVVVVVVVVVVVVV+");
            }
        }
        else if (Player3.I.Velocity.y._is(0) && P > m)
        {
            ///玩家在平台上面
            //玩家上升
            if (E_碰撞 != E_碰撞状态.无碰撞)
                E_碰撞 = E_碰撞状态.碰撞;
            if (Deb)
            {
                AAA.DraClirl(1, Color.grey);
                Debug.LogError(P + "VVVVVVVVVVVVVVVVVVV+" + m);
            }
        }

        if (  Player3.I.Velocity.y._is(0) && P + 1f > m)
        {
            if (E_碰撞 != E_碰撞状态.无碰撞)
                E_碰撞 = E_碰撞状态.碰撞;
            if (Deb) Debug.LogError("VVVVVVVVVVVVVVVVVVV+");
        }


        if (!单方向)
        {
            if (有人)
            {
                if (Player3.I.State == E_State.dun)
                {
                    if (Player_input.I.按键检测_按下(Player_input.I.k.跳跃))
                    {
                        关闭一会儿();
                    }
                }
            }
        }
    }
    public void 关闭一会儿(float time = 0.4f)
    {

        StartCoroutine(asdasd(time));
    }
    IEnumerator asdasd(float f)
    {
        E_碰撞 = E_碰撞状态.无碰撞;
        yield return new WaitForSeconds(f);
        E_碰撞 = E_碰撞状态.碰撞;

    }
}
