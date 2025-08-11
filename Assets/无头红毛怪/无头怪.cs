using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleFSM;
using Sirenix.OdinInspector;
using DG.Tweening;
public class 无头怪 : 泛用状态机
{
   public  无头怪管理 w;

    

    [DictionaryDrawerSettings]
    public Dictionary<string, 单片段 >  字典;

   
  public   Transform t;

   public   亚拉动画 a;


    public state dash = new state("dash ");
    public  state idle = new state("idle");
    public state run = new state("run");
    public state sky = new state("sky");
      protected state up  ;
    protected state down;

    public   Collider2D co;
    public Rigidbody2D rd;

    [SerializeField ]
    private bool ground;

    [SerializeField]
    float Speed=1;
    [SerializeField]
    float JumpF=5;

    public bool Ground { get => ground; set { 
            if (ground!=value)
            {

                ground = value;
                if (value)
                {
                    落地();
                }
                else
                {
                    起跳();
                }
            } 
        } }
    void 起跳()
    {

    }
    void 落地()
    {

    }
      float Dasht;
  public   float DashCold;

    public float DashSpeed = 2;
    public float DashKeepTime=1;
    private void Awake()
    {
        当前 = idle;
        down = new state(" down", sky);
        up = new state("up", sky);


        dash.Enter += () =>
        { 
            Dasht = -DashCold;
 

            var a = DashSpeed * t.lossyScale.x;
            var b = t.position.x + a;
            rd.DOMoveX(b, DashKeepTime);
        };
        dash.Stay += () =>
         {
             if (Time.time -当前.time> DashKeepTime)
             {
                 to_state(idle);
             } 
         };

        down.Enter += () =>
        {
            a?.播放(字典["down"]);
        };
        up.Enter += () =>
          {
              a?.播放(字典["up"]);
              rd.velocity = new Vector2(Player_input.I.水平操作_ * Speed, JumpF);
          };
        up.Stay += () => up.Father.Stay();
        down.Stay += () => up.Father.Stay();

    


        sky.Enter += () =>
          {
              if (Ground)
              {
                  to_state(up);
              }
              else
              {
                  to_state(down);
              }
          };
        sky.Stay += () =>
        { 
            rd.velocity = new Vector2(Player_input.I.水平操作_ * Speed, rd.velocity.y);
            if (Ground&&Time.time -当前.time>0.2f)
            {
                if (Player_input.I.水平操作_!=0)
                {
                    to_state(run );
                }
                else
                {
                  to_state(idle);
                }
 
            }
            if (rd.velocity.y<0)
            {
                a?.播放(字典["down"]);
            }
        };
        run.Stay += () =>
        {
            a?.播放(字典["run"]);

            if (Player_input.I.按键检测_按下(Player_input.I.跳跃)||!Ground)
            {
                to_state(sky);
            }else  if (Player_input.I.水平操作_ != 0)
            {
                rd.velocity = new Vector2(Speed* Player_input.I.水平操作_, 0);
            }
            else if (Player_input.I.按键检测_按下(Player_input.I.攻击)) 当前 = 当前.to_state(dash);
            else
            {
                 to_state(idle);
            } 
       };
        idle.Enter += () =>
        {
            a?.播放(字典["idle"]);
        };
        idle.Stay += () =>
        {

            if (Player_input.I.按键检测_按下(Player_input.I.跳跃)||!Ground )
            {
                 to_state(sky);
            }
            else if (Player_input.I.按键检测_按下(Player_input.I.攻击)) 当前 = 当前.to_state(dash);
            else
            {
                if (Player_input.I.水平操作_ != 0)
                {
                     to_state(run);
                }
                else
                {

                    rd.velocity = Vector2.zero;
                }
            } 
        };
    }
   
    protected override void Update()
    {
        base.Update();

        Dasht += Time.deltaTime;
        if (Dasht >= 0) Dasht = 0;

        float  a= 1;
        if (t.parent != null) a = t.parent.lossyScale.x; 
        t.localScale = new Vector2(Player_input .I.方向正负* a, 1);

        地面检测();
    }
    void 地面检测()
    {
        var DI =
                       Physics2D.BoxCast(
            new Vector2(co.bounds.center.x, co.bounds.min.y),
             new Vector2(co.bounds.size.x - 0.1f, 0.01f),
             0f,
             Vector2.down,
              0.01f,
    1<<Initialize .L_Ground
             )
             .collider;
        if (DI != null)
        {
            Ground = true;
        }
        else
        { 
            Ground = false;
        }

    }
}
