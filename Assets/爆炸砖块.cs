using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//interface I_Destroy
//{

//    Action 销毁触发 { get; set; }
//    Bounds 盒子 { get; }
//}
//public interface I_Re
//{
//    Action 重生 { get; set; }
//    Bounds 盒子 { get; }
//    public bool Re { get; set; }
//    public float Re_Time { get; set; }
//    public void 重制();
//}
/// <summary>
/// 接受场景从这里接受     
/// </summary>
public class 爆炸砖块 : MonoBehaviour
{                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
    I_Dead I;
    I_Speed_Change C;
    I_Revive R;

    List<GameObject> 特效列表 = new List<GameObject>();
    private void Start()
    {
        C = GetComponent<I_Speed_Change>();
           I =GetComponent <I_Dead>();
        R = GetComponent<I_Revive>();
         

        if (R != null)
        {
            //R.重生 += ()=>特效列表.ForEach( 
            // (GameObject g) =>
            // {
            //    g.SetActive(true); 
            //   }
            //); 

            //Initialize_Mono.I.重制触发 += R.重制;
            if (R.Re)
            {
                var T = R.盒子.阵列盒子();
                 foreach (var B in T)
                {
                    var a = 特效_pool_2.I.GetPool(B, T_N.特效闪闪);
                    特效列表.Add(a.gameObject);
                    a.transform.SetParent(transform);
                } 
            }
        } 


        //if (I != null)
        //{
 
        //I.销毁触发 += ()=> {
        //    if (特效列表 != null) 特效列表.ForEach((GameObject g) => { g.SetActive(false ); });
        //    var a =  I.盒子 .阵列盒子(); 
        //    for (int i = 0; i < a.Count; i++)
        //    {
        //  var B=      特效_pool_2.I.GetPool(a[i], T_N.特效砖块爆炸, Player3.I.sp); 
        //    }
        //};
        //}


        if (C != null) C.变速触发+=() => {

            //var a = I.盒子.阵列盒子();
            //for (int i = 0; i < a.Count; i++)
            //{
            //   //var B= 特效_pool_2.I.GetPool(a[i], T_N.特效变速, Player3.I.sp);
            //    B.an.updateMode = AnimatorUpdateMode.UnscaledTime;
            //    B.transform.SetParent(transform);
            //    B.Speed_Lv = Player3.Public_Const_Speed;
            //}
        };
    }
 
     
}
