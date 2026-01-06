using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleFSM;
using Enemmy;
using System;
using Cysharp.Threading.Tasks;
public class 荆棘 : 泛用状态机
{
    state waite = new state("waite");
    state idle=new state("idle");
    state atk = new state("atk");
    public List<生物荆棘> 固定ObjList; 
    戒备 j;
    Enemy_base E;
    监控激活碰撞框 监控;
    float GameTime;
    float 最大=3f;
    float waite最大 = 1f;
    void Start()
    {
        j = GetComponent<戒备>();
        E =GetComponent<Enemy_base>();
        E.A_受击 += 受击;
        当前 = idle;
        gameObject.组件(ref 监控);

        UniTask.Delay(TimeSpan.FromSeconds(0.6f)).ContinueWith(() => { 
            for (int i = 0; i < 固定ObjList.Count; i++)
            {
                固定ObjList[i].关闭();
            }
        }  ).Forget();

 

        if (监控 != null)
        {
            监控.是我 += (b) =>
            { 
                if (!b)
                { 
                    if (当前!=idle)
                    { 
                        to_state(idle);
                    }
                }
            };
        }
        idle.Enter+= () =>
        {
            E.an.Play(idle.StateName);
        };
        idle.Stay+= () =>
        {
            if (j.发现玩家了吗_)
            {
                to_state(atk);
            }
        };
        //j.发现玩家了嘛+=(bool b   )=>
        //{
        //    if (j.发现玩家了吗_)
        //    {
        //        to_state(atk);
        //    } 
        //};
        atk.Stay+= () =>
        {
            GameTime += Time.deltaTime*E.I_S.固定等级差;
            if (GameTime> 最大)
            {

                if (j.发现玩家了吗_)
                {
                    to_state(waite);
                }
                else
                {
                    to_state(idle);
                }
            }
        };
        waite.Enter += () =>
        {
            GameTime = 0;
            E.an.Play(waite.StateName);
        };
        waite.Stay += () =>
        {
            GameTime += Time.deltaTime*E.I_S.固定等级差;
            if (j.发现玩家了吗_)
            {
                ///冷却
                if (GameTime > waite最大)
                {
                    to_state(atk);
                }
            }
            else
            {
                to_state(idle);
            } 
        };

        atk.Enter += ()=>
         {
             ///感叹号         然后释放  
             GameTime = 0;
             E.an.Play(N(idle, atk));
             开关(true);
         }; 
        atk.Exite += () =>
        {
            开关(false);
        };
    }
 
    void 开关( bool b)
    {
        for (int i = 0; i < 固定ObjList.Count; i++)
        {
            固定ObjList[i].开关(b);
        }
    }
    private void 受击()
    {
        to_state(atk);
    }
}
