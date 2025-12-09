using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 上海玩家 : MonoBehaviour,I_攻击
{
    SpriteRenderer sp;
    BoxCollider2D bc;

    public float atkvalue { get => Atkvalue; set => Atkvalue=value; }

    [SerializeField] float Atkvalue;
    private void Awake()
    {
        sp = GetComponent<SpriteRenderer >();
        bc = GetComponent<BoxCollider2D>();
        bc.isTrigger = true;
    }
    public void 开关(bool b)
    {
        sp.enabled = b;
        bc.enabled = b; 
    }
    No_Re N = new No_Re();
    //private void OnTriggerEnter2D(Collider2D co )
    //{
    //    if (co.gameObject .layer==Initialize .L_Player  )
    //    {
    //        if (co!=Player3.I.站立box) return;
    //        if (!N.Note_Re()) return;

    //        if (Player3.I.HPROCK )
    //        {
    //            Player3.I.To_SafeWay();
    //            Player3.I.Velocity = Vector2.zero;
    //        }
    //        else  
    //        {
    //            Player3.I.被扣血(atkvalue, gameObject, 0);

    //            if (Player3.I.当前hp <=0)
    //            {
    //                Initialize_Mono.I.Waite(() => Player3.I.To_SafeWay(),1.2f);
    //            }
    //            else
    //            {
    //                Initialize_Mono.I.Waite(() => Player3.I.To_SafeWay(), 0.1f);
    //            }
      
    //            //Player3.I.Velocity = Vector2.zero;
    //        }

           

    //    }
    //}

    private void OnTriggerStay2D(Collider2D co )
    {
        if (co.gameObject.layer == Initialize.L_Player)
        {
            if (co != Player3.I.站立box) return;
            if (!N.Note_Re()) return;

            if (Player3.I.HPROCK)
            {
                Player3.I.To_SafeWay();
                Player3.I.Velocity = Vector2.zero;
            }
            else
            {
                Player3.I.被扣血(atkvalue, gameObject, 0);

                if (Player3.I.当前hp <= 0)
                {
                    Initialize_Mono.I.Waite(() => Player3.I.To_SafeWay(), 1.2f);
                }
                else
                {
                    Initialize_Mono.I.Waite(() => Player3.I.To_SafeWay(), 0.1f);
                } 
            } 
        }
    }
    public void 扣攻击(float i)
    {
        throw new System.NotImplementedException();
    }
}
