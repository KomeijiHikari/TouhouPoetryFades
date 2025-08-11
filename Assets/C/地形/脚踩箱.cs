using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 脚踩箱 : MonoBehaviour
{ 
    BoxCollider2D b;
    Rigidbody2D rb;
    void Start()
    {
        gameObject.layer = Initialize.L_Box_Ground;
        rb = GetComponent<Rigidbody2D>();
        b = GetComponent<BoxCollider2D>();
    }
    /// <summary>
    /// 玩家在上面
    /// </summary>
    bool 在上面开碰撞
    {
        get
        {
            return Player3.I.脚底中间.y > b.bounds.max.y;
        }
    }  
    void Update()
    { 
        Initialize.Set_碰撞(Initialize.L_Box_Ground, Initialize.L_Player, !在上面开碰撞);  
    }
}
