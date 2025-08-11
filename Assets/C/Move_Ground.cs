using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Trigger : Groundbase
{
    private new void Awake()
    {
        base.Awake();
        bc.isTrigger = true;
    }
}
public class Ground_Ground : Groundbase
{
    private new void Awake()
    {
        base.Awake(); 


    }
}
public class Groundbase : MonoBehaviour
{
    [HideInInspector]
    public BoxCollider2D bc;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Animator an;

 
    protected  virtual   void Awake()
    {
        gameObject.layer = Initialize.L_Ground;

        Initialize.组件(gameObject, ref an);
        Initialize.组件(gameObject, ref rb);
        Initialize.组件(gameObject, ref bc);
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        rb.bodyType = RigidbodyType2D.Static;
    }
}

public class Move_Ground : Ground_Ground
{
    public float 距离 = 1f; 
}
