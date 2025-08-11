using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 检测条件是否达到 : MonoBehaviour
{
    public float v = (13 - 4)*50;
    SpriteRenderer sp;
    BoxCollider2D bc;

    [SerializeField ]
    GameObject 提示;
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        sp = GetComponent<SpriteRenderer >();
    }

    // Update is called once per frame


    void Update()
    {
        if (Player3.I.玩家数值 .钱>=v&&sp.enabled)
        {

            sp.enabled = false;
            bc.enabled = false;
            if (提示!=null)
            {
                提示.SetActive(false);
            }         
        }
    }
}
