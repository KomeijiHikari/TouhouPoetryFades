using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
public class 脉冲 : MonoBehaviour
{
    RawImage RI;
    Material m;
    public static 脉冲 I;

    public float Max;
    public float Min;
    public  float  speed=1;
    static string 尺寸
    {
        get => "_Size";
    }
    static string 强度
    {
        get => "_Force";
    }
    static string 距离
    {
        get => "_t";
    }
    bool e嗯嗯nabled
    { 
    set
        {
            m.SetInt("_B", value?1:0);
        }
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

        RI = GetComponent<RawImage>();
        m = RI.material;

        m.SetFloat(距离, 0);
        e嗯嗯nabled = false;
    }

    private void Start()
    {
        defu= m.GetFloat(强度);
        Defu尺寸 = m.GetFloat(尺寸);
    }
    float defu;

    public void End_File()
    {
         e嗯嗯nabled = false;
        StopCoroutine(F);
        Set_Float(0);

        m.SetFloat(距离, -10);
        m.SetFloat(强度, defu);
        m.SetFloat(尺寸, Defu尺寸);
    }
    float Defu尺寸;

    [Button]
    public void 试一下()
    {
        File(Player3.I.transform.position,0, true,10);
    }
    public void File(Vector3 po,float 脉冲强度=0,bool 正向反向=true,float 时间=1,float 尺寸_=0.1f)
    {
        Debu.LogError(" "+脉冲强度+ 正向反向+ 时间+ 尺寸_);
        if (时间==1)
        {
            时间 = 0.15f;
        }

        m.SetFloat(尺寸, 尺寸_);
        if (脉冲强度==0)
        {
            m.SetFloat(强度, defu);
        }
        else
        {
            m.SetFloat(强度, 脉冲强度); 
        }
        var a = Camera.main.WorldToViewportPoint(po);
        Vector2 vector2 = new Vector2(Camera.main.WorldToViewportPoint(po).x, Camera.main.WorldToViewportPoint(po).y);
        m.SetVector("_Raing", vector2);


        F = StartCoroutine(ST(正向反向, 时间));
    }
    Coroutine F;

    [SerializeField]
    [Range(0,1)]
    float 实验;
    private void Update()
    {
        if (实验!=0)
        {
             e嗯嗯nabled = true; 
            Set_Float(实验);
        }
    }
    IEnumerator ST(bool 正负=true,float 时间=1)
    {
        正负 = true;
         e嗯嗯nabled = true; 
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
         e嗯嗯nabled = false ; 
        yield return null;
    }

    void Set_Float(float value)
    { 
        //float 缺口 = 0.15f;
        //value = 缺口 + (1- 缺口) / 1f* value;
        float Lerp = Mathf.Lerp(Min, Max, value);
        m.SetFloat(距离, Lerp);
    }
}
