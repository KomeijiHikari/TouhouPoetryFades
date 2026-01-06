using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 挤死 : MonoBehaviour
{
    public BoxCollider2D A;
    public BoxCollider2D B;
    public LayerMask 碰撞层;
    int c;

    RaycastHit2D[] a = null;
    RaycastHit2D[] b = null;
    Vector2 V=new Vector2(-0.5f,-1.5f);
    private void Update()
    {

        a = null;
        b = null;
        if (B.enabled) b = B.bounds.碰撞列表(碰撞层, V);
        if (A.enabled) a = A.bounds.碰撞列表(碰撞层, V);


        if (b != null)
            for (int i = 0; i < b.Length; i++)
            {
                即死(b[i].collider);
            }

        if (a != null)
            for (int i = 0; i < a.Length; i++)
            {
                即死(a[i].collider);
            }
        if (a == null && b == null)
        {
            持续时间 = 0;
        }

        if (持续时间 > 最大持续时间)
        {
            if (Player3.I.蹲BOX.enabled)
            {
                Player3.I.被扣血(999f, gameObject, 0);
                持续时间 = 0;
            }
        }
    }
 public    float 最大持续时间=0.2f;
    float 持续时间;
    void 即死(Collider2D c)
    {
        if (!c.isTrigger&&c.gameObject .tag ==Initialize .Ground  )
        {
            持续时间 += Time.deltaTime;

        }

    }

  
}
