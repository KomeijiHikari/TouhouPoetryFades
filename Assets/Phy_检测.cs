using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class Phy_检测 : MonoBehaviour 
{
    [SerializeField ]
    GameObject I_暂停的OBJ  ;
    I_暂停 I_;
    public bool 暂停 { get {
            if (I_!=null)
            {
                return I_.暂停;
            }
            else
            { 
                return false;
            }
        } }
    [SerializeField]
    SpriteRenderer sp;
    [SerializeField]
    BoxCollider2D b;

    bool boo;
    Bounds B

    {
        get {
            if (boo) return sp.bounds;
            return b.bounds; }
    }
    private void OnEnable()
    {
        Enter = null;
        Exite = null;
    }
    public Action Exite;
    public Action Enter; 
[SerializeField]
    public RaycastHit2D[] Rs;
    [SerializeField ]
    LayerMask L;
    //List<Collider2D> CL;
    [SerializeField]
    bool 遇见了_;
    public bool PoinDeb;
    public bool 遇见了 { get=> 遇见了_; set {
            if (遇见了_ !=value)
            {
                if (value)
                {
                    Enter?.Invoke(); 
                }
                else
                {
                    Exite?.Invoke();
                }
            }
            遇见了_ = value;
        } } 
    private void OnDisable()
    {
        遇见了 = false;
        //Debug.LogError("关掉");
    }
    private void Awake()
    {
        boo = b == null; 

        if (I_暂停的OBJ!=null) I_ = I_暂停的OBJ.GetComponent<I_暂停>();
    }
    private void Update()
    {
        if (暂停) return;
   Rs =     Physics2D.BoxCastAll(B.center,
            B.size,
            0,
            Vector2.zero,
           0,
           L);


 
        遇见了 = (Rs != null)&& Rs.Length>0;
        if (PoinDeb)
        {
            foreach (var item in Rs)
            {
                item.point.DraClirl();
            }
        }
    }
    
}
