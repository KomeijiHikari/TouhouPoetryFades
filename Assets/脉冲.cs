using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 脉冲 : MonoBehaviour
{
    SpriteRenderer sp;
    Material m;
    public static 脉冲 I;
 
    public  float  speed=1;
    static string 尺寸
    {
        get => "_Size";
    }
    static string 强度
    {
        get => "_Force";
    }
    static string t
    {
        get => "_t";
    }
    private void Awake()
    {

        if (I != null && I != this)
        {
            Destroy(this);
        }
        else
        {
            I = this;
        }

        sp = GetComponent<SpriteRenderer>();
        m = sp.material;

        m.SetFloat(t, 0);
        sp.enabled = false;
    }

    private void Start()
    {
        defu= m.GetFloat(强度);
    }
    float defu;

    public void End_File()
    {
        sp.enabled = false;
        StopCoroutine(F);
        Set_Float(0);
        m.SetFloat(强度, defu);
        m.SetFloat(尺寸,0.1f);
    }
    public void File(Vector2 po,float 脉冲强度=0,bool 正向反向=true,float 时间=1,float 尺寸_=0.1f)
    { 
        m.SetFloat(尺寸, 尺寸_);
        if (脉冲强度==0)
        {
            m.SetFloat(强度, defu);
        }
        else
        {
            m.SetFloat(强度, 脉冲强度); 
        }
        transform.position = po;


        F= StartCoroutine(ST(正向反向, 时间));
    }
    Coroutine F;

    [SerializeField]
    [Range(0,1)]
    float 实验;
    private void Update()
    {
        if (实验!=0)
        {
            sp.enabled = true;
            Set_Float(实验);
        }
    }
    IEnumerator ST(bool 正负=true,float 时间=1)
    {
        正负 = true;
        sp.enabled = true; 
        var Starttime =Time.time;
        if (正负)
        { 
            var f = 0f;
            Set_Float(0);
            while (f< 时间)
            {
                f += speed * Time.deltaTime;
                var 比例 = f / 时间;

                Set_Float(比例); 
                yield return null;
            }
        }
        else
        {
            var f = 0f;
            Set_Float(1);
            while (f < 时间)
            {
                f += speed * Time.deltaTime;
                       var 比例 =1-(f / 时间) ;
                Set_Float(比例); 
                yield return null;
            }
        }



        Set_Float(0);
        sp.enabled = false ; 
        yield return null;
    }

    void Set_Float(float value)
    { 
        float 缺口 = 0.15f;
        value = 缺口 + (1- 缺口) / 1f* value;
        m.SetFloat(t, value);
    }
}
