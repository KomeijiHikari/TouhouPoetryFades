 
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using VFolders.Libs;

[DefaultExecutionOrder(100)]
public class 感应暂停门 :  MonoBehaviour,I_暂停

{
    Phy_检测 p;
    //[SerializeField]
    //Move_P m;

    public List<Move_P> MoveList;

    BoxCollider2D bc;
    private void Awake()
    {
        gameObject.AddComponent<感应暂停门2>();
  enabled = false;
        p = GetComponent<Phy_检测>();
       
        if (p!=null) Object.Destroy(p);

        return;
        GetComponent<SpriteRenderer>().enabled=false;
        gameObject.layer = Initialize.L_Default; 
        gameObject.组件(ref bc);
        //Initialize.组件( gamepbject,ref bc);
    }
   
    private void Start()
    {
        foreach (Transform t in transform.parent)
        {
            var a= t.GetComponent<Move_P>();
            if (a != null)    MoveList.Add(a) ;
        }
        //Player3.I.Public_Speed_ +=()=> {

        //    Debug.LogError("A                            Player3.I.Public_Speed_A   Player3.I.Public_Speed_");
        //    m.重制();
        //} ;

        p = GetComponent<Phy_检测>();

        
        p.Enter += () => {
            foreach (var item in MoveList) item.重制();
        };

        //p.Exite += () => {
        //    Debug.LogError("A                         wwwwwwwwwwwwwwAAAAAAAAAAAAAAAAA");
        //    if (m.暂停 == true)
        //    {
        //        m.暂停 = false ;
        //    }
        //};
    }
    [DisplayOnly]
    [SerializeField]
    E_超速等级 e_;

    public bool 暂停 { get    ; set  ; }

    public bool  伤害=false;
    private void Update()
    {
        e_ = MoveList[0].I_S.超速等级;

        bool 暂停 = false;
        if (p.遇见了)
        {
    

            switch (e_)
            {

                case E_超速等级.静止:
                case E_超速等级.低速:
                    暂停 = true;
                    break;
                case E_超速等级.正常:
                    暂停 = false;
                    break;
                case E_超速等级.超速: 
                case E_超速等级.半虚化: 
                case E_超速等级.虚化: 
                case E_超速等级.虚无:
                    if (伤害)    Player3.I.To_SafeWay();
         
                    暂停 = false;
                    break;
            }
        }
        foreach (var item in MoveList) item.暂停= 暂停;
        //else
        //{

        //    m.暂停 = false;
        //}
    }

}
