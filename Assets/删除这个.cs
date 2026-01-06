using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 删除这个 : MonoBehaviour
{
    public Material M;
    public bool asdasd;

    SpriteRenderer sp;

    string DarkName = "_DarkColor";
    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();

        M= sp.sharedMaterial;
    }
    private void Update()
    {
        if (asdasd)
        {
            sp.material.SetColor(DarkName, Color.red);
        }
        else
        {
            sp.material = M;
        }
    }

}
