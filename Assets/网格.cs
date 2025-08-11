using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class 网格 : MonoBehaviour
{
    public static 网格 I { get; set; }
    public Tilemap T { get; set; }
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
        T = GetComponentInChildren<Tilemap>();
    }
}