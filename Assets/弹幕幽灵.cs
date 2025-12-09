using SampleFSM;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using 发射器空间;


namespace Enemmy
{
    public class 弹幕幽灵 : 泛用状态机
    {
        [SerializeField]
        UnityEvent EVENt; 
        [SerializeField]
        float 攻击范围 = 4;
         
        Enemy_base e;
        戒备 j; 
        [SerializeField]
        Atk_Anim at;


        state move = new state("move");
        state atk = new state("atk");
        private void Awake()
        {

            e = GetComponent<Enemy_base>();
            j = GetComponent<戒备>();
            startway = transform.position;
        } 

        float time;
        float cooltimeMax = 1; 
        private void Start()
        {
            当前 = move;
            e.E_重制 += () =>
            {
                time = 0;
            };

            atk.Enter += () =>
            {
                time = cooltimeMax;
                e.p.Stop_Velo();
                e.an.Play(atk.StateName);

            };
            at.ATK += () => {
                EVENt?.Invoke();
            };
            at.ATK2 += () => {
                Debug.LogError("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                to_state(move);
            };
            move.Enter += () =>
            {  
                e.an.Play(  move .StateName);

                EnterMoveV = e.p.当前; 
            };
            move.FixStay += () =>
            {
                movestartTime += Time.fixedDeltaTime;
                time -= Time.fixedDeltaTime;
                Vector3 target;
                if (j.发现玩家了吗 && time < 0)
                {
                    target = Player3.I.transform.position;
                }
                else
                {
                    target = startway;
                }
                var speed = e.Move_speed;
                var c = target - transform.position;

                if (target == Player3.I.transform.position)
                {
                    if (c.sqrMagnitude < 攻击范围 * 攻击范围)
                    {
                        to_state(atk);
                        return;
                    }
                }
                else
                {
                    if (c.sqrMagnitude < speed * speed)
                    {
                        return;
                    }
                }

                var a = (Vector2)(c).normalized;
                var mo = a * speed;
                e.LocalScaleX_Set = mo.x;

                e.p.Velocity = mo; 
            };


        } 
        Vector2 EnterMoveV;
        float movestartTime;
        Vector3 startway;
    }

}
