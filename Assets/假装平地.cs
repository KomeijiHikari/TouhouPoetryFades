using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 假装平地 : MonoBehaviour
{
    BoxCollider2D bc;
    Bounds BB { get => bc.bounds; }
    Vector2   上
    {
        get
        {
            return bc.bounds.九个点(E_方向.上) ;
        }
    }
    [SerializeField]
    [DisplayOnly]
    bool   B_R, B_L;

    [SerializeField]
    float 距离 = 1f;

 [DisplayOnly]
    [SerializeField ]
    Vector2 VA,VB;
    private void 适应()
    {
        var R = BB.九个点(E_方向.右上) + new Vector2(1, 1) * 0.2f;
        var L = BB.九个点(E_方向.左上) + new Vector2(-1, 1) * 0.2f;

 

        // B.transform.localScale = Vector2.one *2* 0.1f;
        // A.transform.localScale = Vector2.one * 2 * 0.1f;
        //A.transform.position = R;
        // B.transform.position = L;

        B_L = Physics2D.CircleCast(L, 0.001f, Vector2.zero, 0, 1 << Initialize.L_Ground).point == Vector2.zero;
        B_R = Physics2D.CircleCast(R, 0.001f, Vector2.zero, 0, 1 << Initialize.L_Ground).point == Vector2.zero;

        if (B_L)
        {
            VA = Physics2D.Raycast(L, Vector2.down, 距离, 1 << Initialize.L_Ground).point;
            Debug.DrawRay(L, Vector2.down * 距离, Color.red, 60);
        }

        if (B_R)
        {
            VB = Physics2D.Raycast(R, Vector2.down, 1.5f, 1 << Initialize.L_Ground).point;
            Debug.DrawRay(R, Vector2.down * 1.5f, Color.red,60);
 
        }
        VB.DraClirl(0.1f,Color .red ,60);
        VA.DraClirl(0.1f, Color.red , 60);

        ///应该无视I_生命周期
        Vector2 y = Vector2.zero;
        if (VA.y ._is(VB.y)  && VA != Vector2.zero)
        {
            y = VA;
            消息 = "都空";
        }
        else if (VA == Vector2.zero && VB != Vector2.zero)
        {
            y = VB;
            消息 = "右空";
        }
        else if (VB == Vector2.zero && VA != Vector2.zero)
        {
 
               y = VA;
            消息 = "左空";
        }
        else if (VA == VB && VA == Vector2.zero)
        {
            消息 = "两头在";
        }

        if (y != Vector2.zero)
        {
            var YY = y.y - bc.bounds.size.y / 2 - bc.offset.y;

            transform.position = new Vector2(transform.position.x, YY);

        }
 
    }

    [SerializeField ][DisplayOnly ]
    string 消息;
    public bool Updat;
    private void Update()
    {
        if (Updat)
        {
            适应();
        }
    }
    void Start()
    {  
        gameObject.组件(ref bc);

        Initialize_Mono.I.Waite(()=> 适应());
          

    }

}
