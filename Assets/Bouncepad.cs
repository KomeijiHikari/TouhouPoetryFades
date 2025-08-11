using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncepad : MonoBehaviour
{
    public Vector2 力度;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var a = collision.attachedRigidbody;
        if (a == null) return;
        if (collision.gameObject.tag != "Player") return;
        var p = collision.GetComponent<BiologyBase>();
        p.Velocity = 力度;
    }
}
