using SampleFSM;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Enemmy
{
    public class 冲刺幽灵 : 泛用状态机
    { 
        [SerializeField]
        float 攻击范围=4;

        [SerializeField]
        Phy_检测 pj;
        Enemy_base e;
        戒备 j;
        [SerializeField]
        Atk_Anim at;
         

      state move=new state("move");
        state atk = new state("atk");
        private void Awake()
        { 

            e = GetComponent<Enemy_base>();
            j = GetComponent<戒备>();
            startway = transform.position;
        }
        bool atkatk;

        float time;
        float cooltimeMax=1;

        float lerpTime =0.7f ;
        private void Start()
        {
            当前 = move;
            e.E_重制 += () =>
            {
                time = 0;
            };

            atk.Enter += () =>
            {
                PlayerEnterpo = Player3.I.transform.position;
                atkatk = false;
                e.p.Stop_Velo();
                e.an.Play(N(move, atk)); 
          
            };
            at.ATK += () => {
 
                if (当前==atk)
                {
      
                    if (atkatk == false)
                    {
                        旋转();
                        atkatk = true;
                        time = cooltimeMax;
                        e.碰撞开关 = true;

                        e.p.Velocity = (Vector2)(PlayerEnterpo - transform.position).normalized
                        * e.Move_speed * 10  ;
                    }
                    else
                    { ///第二次触发动画事件
                        to_state(move );
                    }
                }
           
            };
            atk.FixStay += () =>
            { 
                if (pj.遇见了&& atkatk)
                {
                    to_state(move );
                }
            }; 
            move .Enter += () =>
            {
                旋转(false);
                e.碰撞开关 = false;
                e.an.Play(N(atk, move));

                EnterMoveV = e.p.当前;
                movestartTime = lerpTime*0.1f; 
            };
            move .FixStay += () =>
            {
                movestartTime += Time.fixedDeltaTime;
                time -= Time.fixedDeltaTime;
                Vector3 target;
                if (j.发现玩家了吗 && time<0)
                {
                    target = Player3.I.transform.position;
                }
                else
                {
                    target = startway;
                } 
                var speed =  e.Move_speed ;
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

                float LL = movestartTime/ lerpTime;
                LL = Mathf.Min(LL,1);
                LL = 1 - (1 - LL)* (1 - LL) ; 
                e.p.Velocity = Vector2.Lerp(EnterMoveV, mo, LL);
                //e.p.Velocity = mo;
                if (pj.遇见了)
                {
                    EnterMoveV = Vector2.zero;
                }
            };


        } 
        void 旋转(bool b=true)
        {
            if (b)
            {
                var JJ = Initialize.To_方向到角度(PlayerEnterpo - transform.position);
                if (transform.localScale.x < 0) JJ += 180;
                e.an.transform.rotation = Quaternion.Euler(0, 0, JJ);
            }
            else
            {
                e.an.transform.rotation = Quaternion.Euler(Vector3.zero);
            }

        }
        Vector3 PlayerEnterpo;

        Vector2 EnterMoveV;
      float  movestartTime;
        Vector3 startway;  
    }

}
