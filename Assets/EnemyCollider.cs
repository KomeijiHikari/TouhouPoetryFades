using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    public bool Åöµ½ { get; set; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(Initialize.Player)) return;
        Åöµ½ = true;
    }   
}
