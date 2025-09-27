using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[ExecuteAlways]
public class 视差背景管理 : MonoBehaviour
{
    public List<SpriteRenderer> Sp;
    [Button("Play_", ButtonSizes.Large)]
    private void 刷新()
    { 
        if (Sp!=null)
        {
            for (int i = 0; i < Sp.Count; i++)
            {

                var a = Sp[i];
                Vector2 O = Camera.main.WorldToScreenPoint(a.bounds.min);
                if (O.x   <0)
                {
                    continue;
                }
                
                while (O.x >0)
                {
                    O = Camera.main.WorldToScreenPoint(a.bounds.min);
 
                    a.transform.localScale *= 1.01f;
                    
                }
                Vector2 v = a.transform.localScale;
                a.transform.localScale = new Vector3(
                        (float)Math.Round(v.x  , 2), (float)Math.Round(v.y , 2),1
                        );
 
            }
        }
    }
}
