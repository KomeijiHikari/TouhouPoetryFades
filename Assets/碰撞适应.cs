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

    public bool 去他喵的;
    private void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
        sp = GetComponent<SpriteRenderer>();
        sp.drawMode = SpriteDrawMode.Tiled;

 
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
            bc.size = 排除(bc.size);

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
