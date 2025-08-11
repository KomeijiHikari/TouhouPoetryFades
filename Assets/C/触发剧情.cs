using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 触发剧情 : MonoBehaviour
{
    public GameObject[] 位置列表;
    BoxCollider2D bc;
    void Start()
    { 
        bc = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        var a = collision.gameObject;
        if (a.tag == "Player")
        {
            //var L = a.AddComponent<剧情执行>();
            Event_M.I.Invoke(Event_M.剧情触发 );
        }
    }


}
