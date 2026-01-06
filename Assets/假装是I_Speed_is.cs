using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class 假装是I_Speed_is : MonoBehaviour,I_Speed_Is
{
    [SerializeField]
    GameObject G;
    I_Revive R;

    [Tooltip("如果为0 调用倍率  如果是114514    速度speed1       Int就是0")]
    public float 我说是啥就是啥 = 0;
    public float 倍率=1;


    public float Speed_Lv { get => speed_Lv; set => speed_Lv = value; }
    速度颜色 a;
    [SerializeField]
    [DisplayOnly]
    private float speed_Lv;

    float ReturnSp()
    {
        float Speed = 0;
        if (我说是啥就是啥==114514)
        {
            Speed = 1;
        }
       else if (我说是啥就是啥 == 0)
        {
            Speed = 倍率 / R.Re_Time;
            return (倍率 / R.Re_Time);
        }
        else
        {
            Speed = Mathf.Pow(Initialize_Mono.I.阀值, 我说是啥就是啥);
 //if (Speed>1)
 //           {
 //               Speed += 0.0001f;
 //           }
 //           else
 //           {
 //               Speed -= 0.00001f;
 //           }
        }
            return Speed;

    }
    void Start()
    {
        if (G==null)
        {
            R=GetComponent<I_Revive>();
        }
        else
        {
            R =G. GetComponent<I_Revive>();
        }
        speed_Lv = ReturnSp();


        gameObject.组件<速度颜色>(ref a);
    } 
}
