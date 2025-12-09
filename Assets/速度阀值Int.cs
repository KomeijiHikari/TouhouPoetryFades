using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 速度阀值Int : MonoBehaviour
{
    [SerializeField]
    int 超速等级;
    public I_Speed_Is I_Slist;

    public float 应该;
    private void Awake()
    {
        I_Slist = GetComponent<I_Speed_Is>();
    }
    private void Start()
    {  
        var a=  Mathf.Pow(Initialize_Mono.I.阀值, 超速等级);
        if (I_Slist!=null)
        {
            I_Slist.Speed_Lv *= a;
        }

        应该 = a;
    }
}
