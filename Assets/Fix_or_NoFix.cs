using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fix_or_NoFix : MonoBehaviour
{

    public int Fix, Upd , Trriger;
    void Update()
    {
        Upd++;
    }
    private void FixedUpdate()
    {
        Fix++;
    }
    No_Re N = new No_Re();
    private void OnTriggerStay2D(Collider2D collision)
    {
 
        Trriger++;
    }
}
