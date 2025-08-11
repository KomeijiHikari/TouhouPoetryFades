using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class 销毁自己在重生 : MonoBehaviour, I_Dead, I_Revive
{
    SpriteRenderer sp;
    BoxCollider2D bc;
    Animator an;

    [SerializeField]
    private bool re = true;
    [SerializeField]
    private float re_Time;


    Vector2 StartWay;
 
    private void Awake()
    {
        StartWay = transform.position;
        gameObject.layer = Initialize .L_Ground ; 

        Initialize.组件(gameObject, ref sp);
        Initialize.组件(gameObject, ref bc);  
    }  
     
    public Bounds 盒子 => sp.bounds;

    public bool Re { get => re; set => re = value; }
    public float Re_Time { get => re_Time; set => re_Time = value; }
    public Action 销毁触发 { get; set; }

    [SerializeField ] 
    float 下落时间=0.5f;

   
    private void OnCollisionEnter2D(Collision2D c )
    {
        bool 碰到的是上面 = Initialize.Vector2Int比较(c.contacts[0].normal, Vector2.down);
        if (c.gameObject .layer ==Initialize .L_Player&& 碰到的是上面)
        {
            Player3.I.ChangeFather(transform); 
       var a=     Initialize.Time_State(
           下落时间, 
           () => { 销毁触发?.Invoke(); } ,
           () => { 
               transform.position += (Vector3)Vector2.down * Time.fixedDeltaTime*2; }
       );
            StartCoroutine(a);
        }  
    }
    void 开关(bool b = false)
    {
        if (!b)
        { 
            sp.enabled = false;
            bc.enabled = false;

        }
        else
        {
            if (Time.time- Last>0.1f)
            {
                Last = Time.time;


                sp.enabled = true;
                bc.enabled = true;
            } 
        }
    }
    float Last;
  public   bool Dead()
    {
        开关();
        return true;
    } 
    public bool  重制()
    { 
        transform.position = StartWay;  
            开关(true);
            return true; 
    } 


}
