using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleFSM;
namespace Enemmy
{ 
public class 垃圾桶无头 : 泛用状态机
{

    [SerializeField]
    float 弹道速度 = 1;

    protected state dead = new state("dead");
    protected state atk = new state("atk");
    protected state idle = new state("idle");
        protected Enemy_base e;

    [Space]
    [Header(" 检查范围 ")]
    [SerializeField] float A = 10f; // 攻击范围阈值
    [Space]

    [SerializeField]
    [DisplayOnly]
    float 距离_;
    戒备 j;
    [SerializeField]
    Atk_Anim a;

    float 距离 => (Player3.I != null) ? (Player3.I.transform.position - transform.position).magnitude : float.MaxValue;

    public Transform 发射点;

      
     public    SpriteRenderer sp;
        protected virtual void atkk()
        {
            var pool = Surp_Pool.I.GetPool(Surp_Pool.子弹);
            var b = pool.GetComponent<Fly_Ground>();

            b.初始化(new Vector2(transform.localScale.x, 0),
                发射点 != null ? 发射点.transform.position : transform.position,
                e != null ? e.Speed_Lv * 弹道速度 : 弹道速度
                );

            if (DeBuG) b.Debul = true;
        }
    private void Awake()
    { 
        j = GetComponent<戒备>();
        e = GetComponent<Enemy_base>();
        当前 = idle;

        if (e != null)
            e.销毁触发 += () => { to_state(dead); };

        dead.Enter += () =>
        {
            Player3.I.被扣血(20,gameObject);
            //sp.color = Color.green;
            if (e != null && e.an != null)
                e.an.Play(idle.StateName);
        };

        dead.Stay += () =>
        {
            if (e != null && e.当前hp > 0)
            {
                to_state(idle);
            }
        };

        if (a != null)
        {
                a.ATK += atkk;
        }

            ATKENTER();
    }
        protected virtual void ATKENTER()
        {
            idle.Enter += () =>
            {
                //sp.color = Color.red;
                Debug.Log("从攻击切换到待机");
                if (e != null && e.an != null) e.an.Play(N(atk, idle));

            };

            atk.Enter += () =>
            {  
                //sp.color = Color.blue;
                Debug.Log("进入攻击状态");
                if (e != null && e.an != null) e.an.Play(N(idle, atk));
            };
        }
        
    protected override void Update()
    {
        base.Update();
        距离_ = 距离;

            // 如果发现玩家并且在攻击范围内则攻击，否则保持idle

            if ( 当前!=dead) 
        if (j != null && j.发现玩家了吗)
        {
                if (j.发现玩家了吗)
                {

                    to_state(atk);
                }
                else
                {
                    to_state(idle);
                } 
        }
        else
        {
            to_state(idle);
        }
    }
}
}