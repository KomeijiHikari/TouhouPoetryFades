using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 悬挂检测 : MonoBehaviour
{
 
    public bool Debu;

    public Vector2 手的位置=> 上检测.position ;

    [DisplayOnly]
    [SerializeField]
    bool 垂直分界线;
    [DisplayOnly]
    [SerializeField]
    bool 上遮挡;
    [SerializeField ]
    Transform 上检测;
    [DisplayOnly]
    [SerializeField]
    bool 遮挡; 

    [Space(10f)]
    [SerializeField ]
    [DisplayOnly ]
    bool 手的遮挡;
    [SerializeField]
    [DisplayOnly]
    bool 双向平台;
    [SerializeField]
    Transform 双向平台检测位置;
    [SerializeField]
    float 双向平台检测距离=1;
    [SerializeField]
    float 双向平台遮挡检测距离 = 1;

    [Space(10f)]
    public bool 满足;
    public Vector2 Poin;

    Collider2D AAAA;
    private void Start()
    {
        AAAA =双向平台检测位置 .GetComponent<BoxCollider2D>();
    }
    void 那个啥()
    {
        ((Vector2)双向平台检测位置.position).DraClirl(双向平台遮挡检测距离);
        var a = Physics2D.OverlapCircle(双向平台检测位置.position, 双向平台遮挡检测距离, 1 << Initialize.L_Ground);
        if (a != null)
        { 
                手的遮挡 = true; 
        }
        else
        {
            手的遮挡 = false;
        }

        if (!手的遮挡)
        {
            Collider2D 平台 = AAAA.bounds.碰撞(1 << Initialize.L_Ground).collider;
            双向平台 = 平台 != null &&
                             平台.gameObject.CompareTag(Initialize.One_way);
            if (双向平台)
            {
                bool 左 = Initialize.方向_A是否在B的左边或者下面(双向平台检测位置.gameObject, 平台.gameObject, false);
                float 点X = 平台.bounds.max.x;
                if (左) 点X = 平台.bounds.min.x;
                Vector2 顶点 = new Vector2(点X, 平台.bounds.max.y);
                Poin = 顶点;

            }
        }

        //if (!手的遮挡)
        //{
        //    //if (Debu) Debug.DrawRay(双向平台检测位置.position, Vector2.right * Player3.I.LocalScaleX_Set * 双向平台检测距离, Color.red, 0.1f);

        //    Collider2D 平台 = Physics2D.Raycast(双向平台检测位置.position, Vector2.right * Player3.I.LocalScaleX_Set, 双向平台检测距离, 1 << Initialize.L_Ground).collider;
        //    双向平台 = 平台 != null &&
        //                     平台.gameObject.CompareTag(Initialize.One_way);
        //    if (双向平台)
        //    {
        //        bool 左 = Initialize.方向_A是否在B的左边或者下面(双向平台检测位置.gameObject, 平台.gameObject, false);
        //        float 点X = 平台.bounds.max.x;
        //        if (左) 点X = 平台.bounds.min.x;
        //        Vector2 顶点 = new Vector2(点X, 平台.bounds.max.y);
        //        Poin = 顶点;

        //    }


        //}
    }
    private void Update()
    {
        那个啥();
       上遮挡 = Physics2D.OverlapCircle(上检测.position, 0.1f, 1 << Initialize.L_Ground) != null ;
        遮挡 =   Physics2D.OverlapCircle(transform .position ,0.1f,1<<Initialize .L_Ground )!=null;

        if (!遮挡)
        {
            if (Debu) Debug.DrawRay(transform.position, Vector2.down* 1.5f, Color.blue ,0.1f);
        
            var  Z    = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, 1 << Initialize.L_Ground);
             

            垂直分界线 = Z.collider != null&&
                !Z.collider.gameObject.CompareTag(Initialize.One_way); 

            if (垂直分界线)  Poin = new Vector2(上检测.position.x, Z.point.y); 
    
        }
        else
        {
            垂直分界线 = false;
        }


        if (双向平台 || 垂直分界线)
        {
            满足 = true;
        }
        else if (双向平台 && 垂直分界线)
        {
            Debug.LogError("离谱离谱离谱离谱离谱离谱离谱离谱离谱离谱离谱离谱离谱离谱离谱离谱离谱离谱离谱离谱离谱离谱离谱离谱");
            Poin = Vector2.zero;
            满足 = false ;
        }
        else
        {
            满足 = false;
            Poin = Vector2.zero;
        }

 
    } 
}
