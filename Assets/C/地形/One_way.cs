using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class One_way :MonoBehaviour
{
    bool 有干扰组件;
    [DisableOnPlay]
    public bool 应该无视;
    Vector2 Last;

    public bool 顶一下玩家;

    BoxCollider2D bc;

    public bool 单方向;

    public bool Deb;
    private void Start()
    { 
        Initialize.组件(gameObject, ref bc);

        有干扰组件 = GetComponent<Move_P>() != null;
        Last = transform.position;
        gameObject.tag = Initialize.One_way;
    }
   [SerializeField]
    private bool 有人1;
    public Action<bool> Enter_Exite;

    public bool 有人 { get => 有人1; set {
            if (有人1 != value)
            {
                Enter_Exite?.Invoke(value );
                有人1 = value;
            }
             } }
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (!enabled) return;
    //    if (!collision.gameObject.CompareTag(Initialize.Player)) return;
    //    有人 = false; 
    //}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{ 
    //    if (!enabled) return;
    //    if (!collision.gameObject.CompareTag(Initialize.Player)) return;
    //    有人 = true;
    //}



    void Update()
    {
        if (bc.IsTouching(Player3 .I.co))
        {
            有人 = true;
        }
        var AAA = (Vector2)Player3.I.po.bounds.min;
        var P = Player3.I.po.bounds.min.y;
        var m = bc.bounds.max.y; 
       
        if (Player3.I.Velocity.y >2.5f|| (P < m))
        {
            ///玩家在平台下面
            //玩家上升
            if (有人)
            {
                有人 = false;
            }
            应该无视 = true;

            if (Deb) Debug.LogError("VVVVVVVVVVVVVVVVVVV+");
        }
        else if (Player3.I.Velocity.y <- 0.1)
        {
            //玩家下降
            if (P > m)
            {
                应该无视 = false;
                if (Deb) Debug.LogError("VVVVVVVVVVVVVVVVVVV+");
            }
        }
        else if (Player3.I.Velocity.y ._is(0) && P > m)
        { 
            ///玩家在平台上面
            //玩家上升
            应该无视 = false;
            if (Deb)
            {
                AAA.DraClirl(1,Color.grey);
            Debug.LogError(P + "VVVVVVVVVVVVVVVVVVV+"+ m);
            }
        }

        if (顶一下玩家&& Player3.I.Velocity.y._is(0) && P+1f > m)
        {
            应该无视 = false;
            if (Deb) Debug.LogError("VVVVVVVVVVVVVVVVVVV+");
        }

        if (!有干扰组件)
        {
            if (应该无视 ==false ) ///下降的时候检测离地面有多远
            {
                if (Last!=(Vector2 )transform .position )
                {
                        Last = (Vector2)transform.position;
              
                         var b = bc.bounds;
                         var Max =new Vector2(b.center.x , b.max.y);  

                         var a = Physics2D.RaycastAll(Max,Vector2.down,0.5f,1<<Initialize .L_Ground);
                    for (int i = 0; i < a.Length; i++)
                    {
                        if (a[i].collider.gameObject != gameObject &&   a[i].collider.gameObject.GetComponent<I_Revive>() == null)
                        {
                            应该无视 = true;
                            if (Deb) Debug.LogError("VVVVVVVVVVVVVVVVVVV+");
                            break;
                        } 
                    } 
                }
            }
            bc.isTrigger = 应该无视;
        }


        if (!单方向)
        {
            if (有人)
            {
                if (Player3.I.State == E_State.dun)
                {
                    if (Player_input.I.按键检测_按下(Player_input.I.跳跃))
                    {
                        关闭一会儿();
                    }
                }
            }
        } 
    } 

    public void 关闭一会儿(float time=0.4f)
    {

        StartCoroutine(asdasd(time));
    }
    IEnumerator asdasd(float f)
    {
        bc.enabled = false;
        yield return new WaitForSeconds(f);
        
        bc.enabled = true;
    }
}
