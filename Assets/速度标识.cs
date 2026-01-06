using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class 速度标识 : MonoBehaviour
{
   SpriteRenderer sp;
    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        V = sp.size;
    }
    public bool Deb;
    Vector2 V;
    [Button("Play_", ButtonSizes.Large)]
    public void 刷新()
    {
        if (SpeedInt == Initialize_Mono.BugInt)
        {
            if(Deb)
            Debug.LogError("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            sp.enabled = false;
            return;
        }
            sp.enabled = SpeedInt != 0; 
        if(SpeedInt > 0)
        {
            sp.flipX = false;

        }
        else  
        {
            sp.flipX = true;
        }
        //if (transform.lossyScale.x < 0) sp.flipX = !sp.flipX;

        if (SpeedInt !=0)
        {
            sp.size=new Vector2(V.x*Mathf.Abs(SpeedInt), V.y);

        }
    }

    public int SpeedInt;
}
