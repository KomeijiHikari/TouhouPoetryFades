using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 速度阀值 : MonoBehaviour
{
    [SerializeField]
    E_超速等级 超速等级;
    public I_Speed_Is I_Slist;
    private void Awake()
    {
        I_Slist = GetComponent <I_Speed_Is>();
    }
    private void Start()
    {
        float spm = 1;
        switch (超速等级)
        {
            case E_超速等级.静止:
                spm =1/(Initialize_Mono.I.阀值 * Initialize_Mono.I.阀值) ;
                break;
            case E_超速等级.低速:
                spm = 1/Initialize_Mono.I.阀值;
                break;
            case E_超速等级.正常:
                spm = 1;
                break;
            case E_超速等级.超速:
                spm = Initialize_Mono.I.阀值;
                break;
            case E_超速等级.半虚化:
                break;
            case E_超速等级.虚化:
                spm = Initialize_Mono.I.阀值 * Initialize_Mono.I.阀值;
                break;
            case E_超速等级.虚无:
                break;
            default:
                break;
        }
        I_Slist.Speed_Lv *= spm;
    }
}
