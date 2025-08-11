using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleFSM;

public class 无头怪管理 : 泛用状态机
{
    [SerializeField ]
    Phy_检测 p;

    无头怪 w;

    public GameObject 脖子;

    public 亚拉动画 a;

 public   Transform t;
    public  Collider2D b; 
    public Rigidbody2D r;

    public Transform t1; 
    public Collider2D b1; 
     public Rigidbody2D  r1;

    public  state 完整 = new state("完整");
    public state 头 = new state("头");
    public state 身子 = new state("身子");

    Vector2 startway;




   
void 是完整(int i)
    {
        if (i==1)
        {
            r1.bodyType = RigidbodyType2D.Dynamic;
            r1.WakeUp();

            w.dash.激活 = true ;
            w.sky.激活 =false;

            w.t= t1;
            w.co = b1;
            w.rd = r1;

            脖子.SetActive(true) ;

            w.a = null;
        }
        else if (i == 0)
        {
            r1.bodyType = RigidbodyType2D.Kinematic;
            r1.Sleep();

            w.dash.激活 = false;
            w.sky.激活 = true;

            脖子.SetActive(false);

            t1.localPosition = startway;
            t1.rotation = Quaternion.Euler(Vector3.zero);
            t1.localScale =Vector2 .one ;

            w.t = t;
            w.co = b;
            w.rd = r;

            w.a = a;
        }
        else if (i ==   -1)
        {
            w.t = t ;
            w.co = b  ;
            w.rd = r ;

            w.a = a;
        }
    }
    private void Awake()
    {
        w = GetComponent<无头怪 >();
        w.w = this;

        startway = t1.transform.localPosition;

        完整.Enter += () =>
        {
            是完整(0);
        };
        完整.Stay += () =>
        {
            if (Player_input.I.按键检测_按下(Player_input.I.攻击))
            {
                to_state(头);

            }
        };
        头.Enter += () =>
        {
            是完整(1);
            w.当前 = w.当前.to_state(w.dash);
   
        };
        头.Stay += () =>
        {
            if (p.遇见了 && Time.time - 当前.time > 0.2f)
            {
            
                to_state(完整); 
            }
           
        };
    }
 
    private void Start()
    {

        当前 = 完整;

        完整.Enter?.Invoke();




    }
    new  private   void Update()
    {
        base.Update(); 
    }
}
