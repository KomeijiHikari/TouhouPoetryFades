using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class Phy_检测 : MonoBehaviour , I_暂停
{

    public bool 暂停 { get => 暂停1; set => 暂停1 = value; }
    [SerializeField]
    SpriteRenderer sp; 
     
    //private void OnEnable()
    //{
    //    Enter = null;
    //    Exite = null;
    //}
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
                    if (Deb)
                    {
                        Debug.LogError(gameObject .name+"触发              触发");
                    }
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

        if (GetComponent<MonoMager>() == null)
        {
            gameObject.AddComponent<MonoMager>();
        }
         
        //if (I_暂停的OBJ!=null) I_ = I_暂停的OBJ.GetComponent<I_暂停>();
    }
    public bool   Deb;
    [SerializeField]
    [DisplayOnly]
    private bool 暂停1;

    //public float 角度;
    private void Update()
    {
        if (暂停) return;
        if (Deb)
        {
            Debug.LogError("AAAAAAAAAAA"+ transform.lossyScale + sp.bounds.center);
        }
        ///原先Bouns 会跟着变换改变Bouns大小
        ///之后 的话 保持transform   比例正常


        //Rs =     Physics2D.BoxCastAll(B.center,
        //         B.size,
        //         0,
        //         Vector2.zero,
        //        0,
        //        L);    transform.rotation.eulerAngles.z



        ///size 值不能有负数
        Rs = Physics2D.BoxCastAll(sp .bounds .center, 
             new Vector2(MathF.Abs(transform.lossyScale.x)  , transform.lossyScale.y)   ,
              transform.rotation.eulerAngles.z,
               Vector2.zero,
              0,
              L); 
        遇见了 = (Rs != null)&& Rs.Length>0;

        if (Deb) Debug.LogError(Rs.Length);

        if (PoinDeb)
        {
            foreach (var item in Rs)
            {
                item.point.DraClirl(1, Color.blue);
            }
        }
    }
    
}
