using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 门 : MonoBehaviour, I_Ground_Action
{
    protected Animator an;
    protected BoxCollider2D bc;
    private void Awake()
    {
        an = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        if (gameObject.layer.ToString() != "Ground")
        {
            gameObject.layer = LayerMask.NameToLayer("Ground");
        }
    }
 public    void Ground_action()
    {
        Debug.LogWarning("触发了了了");
        bc.enabled = false;
        an.Play("to上");
    }
}
