using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_father : MonoBehaviour
{
    public Transform father;
    public string GroundTag = "Ground";
    private void Awake()
    {
        father = GetComponent<Transform>();

        for (int i = 0; i < father.childCount; i++)
        {
            var child = father.GetChild(i).gameObject;
            PolygonCollider2D po = child?.AddComponent< PolygonCollider2D > ();
            po.autoTiling = true;

             
        }
    }

}
