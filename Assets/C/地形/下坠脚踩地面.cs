using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 下坠脚踩地面 : MonoBehaviour
{
    BoxCollider2D b;
    Rigidbody2D rb;
    void Start()
    {
        gameObject.layer = Initialize.L_Ground;
        rb = GetComponent<Rigidbody2D>();
        b = GetComponent<BoxCollider2D>(); 
     }
    private void OnCollisionExit2D(Collision2D co )
    {
        if (co.gameObject .layer==Initialize .L_Player)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;

        }
    }
    private void OnCollisionEnter2D(Collision2D co )
    {
        if (co.gameObject.layer == Initialize.L_Player)
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
    private void FixedUpdate()
    {
 
        //if (Physics2D.Raycast(new Vector2(b.bounds.center.x, b.bounds.min.y - 0.01f), Vector2.down, 0.1f, Player3.I.检测层).collider==null)
        //{
        //    rb.bodyType = RigidbodyType2D.Dynamic;
        //}
        //else
        //{
        //    rb.bodyType = RigidbodyType2D.Static;
        //}
    }
}
