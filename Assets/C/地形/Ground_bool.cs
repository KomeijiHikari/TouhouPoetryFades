using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Is_boolBase : MonoBehaviour
{
    public int groundlayer { get; set; }
    public LayerMask Layer;
    private void Awake()
    {
        groundlayer = LayerMask.NameToLayer("Ground");
        Layer = 1 << groundlayer;
    }
}

public class Ground_bool : Is_boolBase
{
    public Action 接触地面事件 { get; set; }
    public Action 离开地面事件 { get; set; }
    [DisplayOnly]
  public   bool InGround_;
    public bool InGround
    {
        get
        {
            return InGround_;
        }
        set
        {
            if((InGround_==false)&&value ==true)
            {

                接触地面事件?.Invoke();
                InGround_ = value;
            }
            if ((InGround_ == true) && value == false)
            {

                离开地面事件?.Invoke();
                InGround_ = value;
            }
            InGround_ = value;
        }
    }

    //public FSM player;


    float sheXianJuli = 0.5f;
    RaycastHit2D ray;



    private void Start()
    {

    }
    void Update()
    {

     InGround =Shexian();

    }
   bool  Shexian()
    {
        for (int i = 0; i < 3; i++)
        {
            var Q = new Vector2(transform.position.x - 0.55f + i * 0.55f, transform.position.y);
            ray = Physics2D.Raycast(Q, Vector2.down, sheXianJuli,1<< groundlayer);
            Debug.DrawRay(Q, Vector2.down, Color.red,0.2f);

            if (ray) return true;
        }

        return false;
        //hit = Physics2D.Raycast(transform.position, Vector2.down,sheXianJuli, groundlayer);
        //~(1 << 8)  层8意外的所有图层   (1 << 8)图彭8
        //Debug.DrawRay(transform.position, Vector2.down, Color.red, 0.2f);
        //Ininground_ = hit;
    }
}
