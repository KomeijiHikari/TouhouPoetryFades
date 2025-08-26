using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace  Boss
{ 
public class 蘑菇管理 : MonoBehaviour
{
    [SerializeField]
    Sprite sp红;
    [SerializeField]
    Sprite sp蓝; 

    [SerializeField ]
    SpriteRenderer sp;
    [SerializeField]
    List<One_way> Os;
    [SerializeField]
    List<SpriteRenderer> sps;
    [SerializeField ]
    Enemy_base e;

    [SerializeField ]
    Transform 跟随点;
 
    [SerializeField]
    List<int> 升起来 = new List<int>();
    [SerializeField]
    List<Vector2 > StartWay=new List<Vector2> ();

    public static 蘑菇管理 I;

        [Button("Play_", ButtonSizes.Large)]
        public void  从这里升起蘑菇(float X)
        {
            for (int i = 0; i < Os.Count; i++)
            {
                var  O= Os[i];
                if (!升起来.Contains (i))
                {
                    //Debug.LogError("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                    //拿出来一个
                    sps[i].sprite = sp红;

                    if (
                       安全序号 == -1 &&
                        (Initialize.RandomInt(1, 3) != 1 || i + 1 == Os.Count)
                        )
                    {
                        安全序号 = i;
                        sps[i].sprite = sp蓝;
                    }

                    O.gameObject.SetActive(true);
                    O.transform.position = new Vector3(X, O.transform.position.y,0);
                    升起来.Add(i);
                    break;
                }
            } 
        }
    public void 全部销毁()
    {
        for (int i = 0; i < Os.Count; i++)
        {
            var y = Os[i];
            if (y.isActiveAndEnabled)
            {
                bool B = 安全序号 == i;
                if (B)
                {
                    特效_pool_2.I.GetPool(Os[i].transform.position, T_N.特效大爆炸);
                    安全序号 = -1;
                }
                else
                {
                    特效_pool_2.I.GetPool(Os[i].transform.position, T_N.特效大伤害);
                    var a = Physics2D.OverlapBox(transform.position, sps[i].bounds.size / 2, 0, 1 << Initialize.L_Player);

                        //Player3.I.被扣血(1, Os[i].gameObject, 0);
                        if (a != null) 
                        魔理沙.I.伤害玩家一下("蘑菇");
                    }

                Os[i].transform.localPosition = StartWay[i];
                Os[i].gameObject.SetActive(false);
            }
        }
    }
    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(this);
        }
        else
        {
            I = this;
        }

        for (int i = 0; i < Os.Count; i++)

        {
            sps.Add(Os[i].transform.GetComponent<SpriteRenderer>());
            StartWay.Add((Vector2)Os[i].transform.localPosition);
        
        } 
    }
    [SerializeField]
    float 阀;

    [SerializeField ]
    float UpSpeed;

    float Speed_ 
    {
        get
        { 
            return e.I_S.固定等级差;
        }
    }
    public bool B有蘑菇在场;
    public bool 有蘑菇在场
    {
        get
        {
            bool ji = false;
            for (int i = 0; i < Os.Count; i++)
            {
                var y = Os[i];
                if (y.isActiveAndEnabled)
                {
                    return true ;
                }
            }
            return false;
        }
    }
    private void Update()
    {
        B有蘑菇在场 = 有蘑菇在场;
    }
    private void FixedUpdate()
    {
   
        for (int i = 0; i < Os.Count; i++)
        {
            if (Os[i].有人)
            {
                var y = Os[i].transform.localPosition.y;
                if (y>-0.1f)
                {
                    ///踩到后下降
                    var x = Os[i].transform.localPosition.x;
                    y -=   Time.fixedDeltaTime * Speed_;
                    Os[i].transform.localPosition = new Vector2(x, y);
                }
                else if (y > -0.2f)
                {
                    ///下降到一定程度
                    bool B = 安全序号 == i;
                    if (B)
                    {
                        特效_pool_2.I.GetPool(Os[i].transform.position, T_N.特效大爆炸);
                        安全序号 = -1;
                    }
                    else
                    {
                        特效_pool_2.I.GetPool(Os[i].transform.position, T_N.特效大伤害);
                        var a = Physics2D.OverlapBoxAll(transform .position,sps[i].bounds .size*4 ,0,1<<Initialize .L_Player );
                        if (a != null) Player3.I.被扣血(1,Os[i].gameObject  ,0);
                    }
                
                    Os[i].transform.localPosition = StartWay[i];
                    Os[i].gameObject.SetActive(false);

            
                 var aaa=   升起来.IndexOf(i); 
                   if(aaa!=-1) 升起来.RemoveAt(aaa);
                }
              
            } 
        }

      
            ///上升de
            for (int i = 0; i < 升起来.Count; i++)
        {
            var a = 升起来[i];
            if (Os[a].transform.localPosition .y<0)
            {
      

                var x = Os[a].transform.localPosition.x;
                var y = Os[a].transform.localPosition.y+ UpSpeed*Time .fixedDeltaTime* Speed_;

                Os[a].transform.localPosition = new Vector2(x, y);

                var fff = 1 - (y / StartWay[a].y) ;
                  
                    if (fff < 阀) break; 
            }
            else
            {
                    升起来.RemoveAt(i);
                }
          
        }
    }

    public int 安全序号=-1;

        [Button("Play_", ButtonSizes.Large)]
        public void Play_()
        {

            transform.position = 跟随点.position;
            transform.localScale = 跟随点.localScale;

            var Y = sp.bounds.min.y;


            for (int i = 0; i < Os.Count; i++)
            {

                var vv = new Vector2(Os[i].transform.position.x, Y);
                var a = Physics2D.CircleCast(vv, 0.1f, Vector2.zero, 0, 1 << Initialize.L_Ground).point;
                if (a == Vector2.zero)
                {
                    sps[i].sprite = sp红;

                    if (
                       安全序号 == -1 &&
                        (Initialize.RandomInt(1, 3) != 1 || i + 1 == Os.Count)
                        )
                    {
                        安全序号 = i;
                        sps[i].sprite = sp蓝;
                    }
                    Os[i].gameObject.SetActive(true);
                    升起来.Add(i);
                }
            }
        }

    }
}