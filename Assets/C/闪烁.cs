using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class 闪烁 : MonoBehaviour
{

 public    GameObject father;
   private  Light2D  ld;

    private void Awake()
    {
        Initialize.组件(gameObject, ref ld);
        //father = gameObject;
        //初始化();
    }
    /// <summary>
    /// 从左下角逆时针依次设置    坐标是相对原点的局部变换，受size的影响      比如-0.5代表当前尺寸的一半
    /// 可以套碰撞体的尺寸来写代码
    /// </summary>
    /// <returns></returns>
    Vector3 [] 设置照亮范围(BoxCollider2D collider2D )
    { Vector2 s = collider2D.size/2;
        var vs = new Vector3[4];
        vs[0] = (Vector2 )new Vector2(-s.x, -s.y);
        vs[1] = (Vector2)new Vector2( s.x, -s.y);
        vs[2] = (Vector2)new Vector2(s.x, s.y);
        vs[3] = (Vector2) new Vector2(-s.x, s.y);
        return vs;
    }
    public void 初始化()
    {
        ld.enabled = false;
        Collider2D c = father.gameObject.GetComponent<Collider2D>();
        SpriteRenderer sp = father.gameObject.GetComponent<SpriteRenderer>();
        gameObject.transform.parent = father.transform;
        transform.position = c.bounds.center;


        ld.SetShapePath(设置照亮范围((BoxCollider2D)c));
        ld.lightType = Light2D.LightType.Freeform;
        foreach (var item in ld.shapePath)
        {
            Debug.LogError(item);
        }
        //ld.lightType = Light2D.LightType.Point;
        ld.intensity =10;

        ld.pointLightOuterRadius = c.bounds.max.y;
        ld.overlapOperation = Light2D.OverlapOperation.Additive;
       
        ld.blendStyleIndex = sp.sortingLayerID;

        ld.lightOrder = sp.sortingOrder;
    } 
    public    IEnumerator 开闪一下( float t)
    {
        ld.enabled = true; 
        yield return new WaitForSeconds(t);
      ld.enabled = false;
    }
}
