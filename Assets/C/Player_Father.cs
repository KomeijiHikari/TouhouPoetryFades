using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Father : MonoBehaviour
{
 public  static    Player_Father I;

    Rigidbody2D RB;
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

        RB = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        RB.velocity = new Vector2(
Player_input.I.方向正零负 * 9f, 0
            );
        //transform.Translate(new Vector2(Player_input.I.方向正零负 * 0.1f, 0));
    }
}
