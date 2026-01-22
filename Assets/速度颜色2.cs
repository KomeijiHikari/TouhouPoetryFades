using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 速度颜色2 : MonoBehaviour
{
    Material M;
    public bool asdasd;

    public bool Deb;
    I_Speed_Is Is;

public  SpriteRenderer sp;

    [SerializeField]
    E_超速等级 E;
    private void Awake()
    {
        //速度颜色

        //材质管理

        if (sp == null)
        {
            sp = GetComponent<SpriteRenderer>();
            if (sp == null) Debug.LogError("Sp还是空" + gameObject.name + "        A " + sp);
        }
        //return;
        M = sp.material;
        Is= GetComponent<I_Speed_Is>();
 
    }
    public bool BB;

  [DisplayOnly]  public float 固定等级差;
    private void Update()
    {
        //return;
        if (Is==null)
        {
            Debug.LogError(gameObject.name+transform.position+"空 速度接口");
            return;
        }
        固定等级差 = Is.固定等级差;
        E = Initialize.Speed_toESpeed( 固定等级差);
        if (BB !=  切换Shader.I.isSpeed)
        {
            BB = 切换Shader.I.isSpeed; 
            if (!BB)
            {
                sp.material = M;
            } 
        }
 
 
    }

}
