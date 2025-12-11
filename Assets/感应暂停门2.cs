using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

[DefaultExecutionOrder(100)]
public class 感应暂停门2 : MonoBehaviour
{
    public List<Move_P> MoveList=new List<Move_P>();
    BoxCollider2D bc;
    SpriteRenderer sp;

    MonoMager Mo;
    private void Start()
    {
          gameObject.layer = Initialize.L_Default;
        gameObject.组件(ref bc);

        gameObject.组件(ref sp);
        sp.enabled = false;


        foreach (Transform t in transform.parent)
        {
            var a = t.GetComponent<Move_P>();
            if (a != null) MoveList.Add(a);
        }

        Mo= MoveList[0].GetComponent<MonoMager>();
    } 
    Int不重复 IN=new Int不重复();
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IN.Add(Time.frameCount)) return;
        if(MoveList[0].I_S.超速等级 != E_超速等级.正常)
        if (collision.collider.CompareTag(Initialize.Player))
            {
                foreach (var item in MoveList) item.重制();

                
                if (MoveList[0].超速)
                {
                    消息.I.Come_on_Meesge("如果我更快一些就好了");
                }
                else
                {
                    消息.I.Come_on_Meesge("如果我更慢一些就好了");
                }

            }
      
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (MoveList[0].I_S.超速等级 != E_超速等级.正常)
            if (collision.CompareTag(Initialize.Player))
            foreach (var item in MoveList) item.重制();
    }
    public bool 我暂停;
    void Update()
    {
        我暂停 = false;

        var a = MoveList[0].I_S.超速等级;
        bc.isTrigger = a== E_超速等级.正常; 
        if (bc.IsTouching(Player3.I.co))
        {
            switch (a)
            {
                case E_超速等级.静止:
                case E_超速等级.低速:
                    我暂停 = true;
                    break;
                case E_超速等级.正常:
                    我暂停 = false;
                    break;
                case E_超速等级.超速:
                case E_超速等级.半虚化:
                case E_超速等级.虚化:
                case E_超速等级.虚无:
               Player3.I.To_SafeWay();
                    Player3.I.被扣血(10, gameObject, 0);
                    Initialize_Mono.I.Waite(() =>
                    {
                        Player3.I.transform.position += new Vector3(-Player3.I.LocalScaleX_Int * 0.1f, 0, 0); 
                    }, 0.20000001f);

                    我暂停 = false;
                    break;
            } 
        }
        foreach (var item in MoveList) item.暂停 = Mo .关闭|| 我暂停;
        return;


    }
}
