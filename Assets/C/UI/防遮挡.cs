using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 防遮挡 : MonoBehaviour
{
    [SerializeField]
    List<Component> 组件列表;

    RectTransform r;
    BoxCollider2D bc;

    [SerializeField ]
    List<Image> Image列表;
    void asd( float ab)
    {
        for (int i = 0; i < Image列表.Count; i++)
        {
            var a = Image列表[i];
            a.color = new Color(a.color.r, a.color.g, a.color.b, ab);
        }
    }
    private void Awake()
    {
        r = GetComponent<RectTransform>();
      gameObject .组件(ref  bc);
        bc = GetComponent<BoxCollider2D >();
        bc.isTrigger = true; 
        bc.size = r.rect.size;
    }

    int TT;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision .gameObject .layer ==Initialize .L_Player )
        {
            if (TT!=Time.frameCount )
            {
                开关(false);
                TT = Time.frameCount;
            }
           
        }
 

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Initialize.L_Player)
        { 
        
            开关(true);
        }
    }
    private void Update()
    {
        bc.size = new Vector2(bc.size.x, r.sizeDelta.y);
    }
 
    public   void  开关(bool B)
    {
 
        组件列表.集体开关(B);
    } 
}
