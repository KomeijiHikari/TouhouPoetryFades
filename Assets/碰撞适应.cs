using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 吧SP改成网格
/// 如果去他喵的 碰撞尺寸等于SP尺寸
/// 否则
/// 自由变换的话SP  SImple BC 1   拉OBJ
/// 个别属性调整的的话（板子）  1的使用尺寸，不为一的不变
/// </summary>
[DefaultExecutionOrder(-1000)]
public class 碰撞适应 : MonoBehaviour
{
    SpriteRenderer sp;
    BoxCollider2D bc;

    [SerializeField]
    bool sliced = false;
    public bool 去他喵的;
    private void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
        sp = GetComponent<SpriteRenderer>();

        if (sliced)
        {
            sp.drawMode = SpriteDrawMode.Sliced;
        }
        else
        {
            sp.drawMode = SpriteDrawMode.Tiled;
        }

 
 
        //StartCoroutine(asda());
    } 
    IEnumerator asda()
    {
       //List<Vector3>Loc = new List<Vector3>();
        List<Vector3>pos=new List<Vector3>();

        List<Transform> ts = new List<Transform>();
        foreach (Transform item in transform)
        {
            ts.Add(item);
            pos.Add(item.localPosition);
            //Loc.Add(item.loc);
        }
        yield return null;

        for (int i = 0; i < ts.Count; i++)
        {
            //ts[i].position = pos[i];
            ts[i].localPosition = pos[i];
        } 
    }
    private void Start()
    {
        啊啊啊啊();
    }
    void 啊啊啊啊()
    {
        if (去他喵的)
        {
            bc.size = sp.size;
        }
        else
        {
 
            sp.size = 排除(sp.size);

            var a = 排除(bc.size);
            bc.size =new Vector2(Mathf.Abs(a.x), Mathf.Abs(a.y)) ;
            //if (Debug_) Debug.LogError(a+"        sp.size = 排除(sp.size);sp.size = 排除(sp.size);sp.size = 排除(sp.size);            A" + bc.size);

            transform.localScale = Vector2.one;
        }
    }
    Vector2 排除(Vector2  输入)
    { var ObJ尺寸 = transform.localScale;
        var 输入x = 输入.x;
        var 输入y = 输入.y;
        if (输入x == 1) 
            输入x = ObJ尺寸.x;
        if (输入y == 1)
            输入y = ObJ尺寸.y;
        return new Vector2(输入x,输入y); ;
    }
    public bool Debug_;
}
