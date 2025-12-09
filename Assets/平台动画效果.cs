using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 平台动画效果 : MonoBehaviour
{
    public Action Q弹结束;
    生命周期管理 P;

    List<GameObject> 特效列表;
    public I_Dead D { get=> P.D; }
    public I_Revive R { get => P.R; }
    public I_Speed_Change S { get=>P.S; }
    void DDD()
    {

        Debug.LogError(P);
        Debug.LogError(D);
        Debug.LogError(D.盒子);
    }
 
    private void Awake()
    {
        P = GetComponent<生命周期管理>();

        //return;
        P.效果_不复活 += (bool BBB) => {
            if (BBB&&P.闪闪发光特效)
            {
                特效列表 = new List<GameObject>();

                if (P==null|| D==null || D.盒子==null)
                {
                    if (P.DeBuG )     DDD(); 
                }
                else
                {
                    var a = D.盒子.阵列盒子();
                    for (int i = 0; i < a.Count; i++)
                    {
                        var B = 特效_pool_2.I.GetPool(a[i], T_N.特效闪闪);
                        B.代理回归 = true;
                        B.transform.SetParent(transform);
                        特效列表.Add(B.gameObject);
                    }
                }

            }
            else
            {
                if (特效列表 != null) 特效列表.ForEach((GameObject g) => { g.SetActive(false); }); 
            }

        };

        P.效果_死亡Enter += ( ) => { 
            var a = D.盒子.阵列盒子();
            for (int i = 0; i < a.Count; i++)
            {
                var B = 特效_pool_2.I.GetPool(a[i], T_N.特效砖块爆炸, Player3.I.sp);
            }
        };

        P.效果_活动Enter += () => {
            if (P.E == null)
            {

            }
            if (Time.frameCount >5)
            {
                var B = transform.Q弹(0.8f, 0.5f, false, true, null, false);
                Initialize_Mono.I.Waite(() => Q弹结束?.Invoke(), 0.5f);
                StartCoroutine(B);
            }


        };
    }

    public void 重置特效(Bounds Bb)
    {
        var a = Bb.阵列盒子();
        for (int i = 0; i < a.Count; i++)
        {
            var B = 特效_pool_2.I.GetPool(a[i], T_N.特效砖块爆炸, Player3.I.sp);
        }
    }
}
