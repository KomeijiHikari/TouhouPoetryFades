using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Player_input;

public class 按键父类 : MonoBehaviour
{
    Text_button_Father T;
    public     Dictionary<string, object> D=new Dictionary<string, object>();
    public static 按键父类 I;

    public 按键监听 j;

    //public IK kk;

    private void Awake()
    { 
        if (I != null ) Destroy(this);
        else   I = this;
        //Debug.LogError();

        T = GetComponent<Text_button_Father>();

        读取();
        T.Action_回退 += () =>
        {
            //Debug.LogError("AAAAAAAA AAAAAAAAAAAAAAAAAAAAA");
            var a = Initialize.ConvertToInstance<IK>(D);
            Save_static.SaveinText(Save_static.按键, a);
        };
    }

    //private void OnEnable()
    //{
    //    读取();
    //}
 
    public void 新的()
    {
        var kk = 来个新的();
        Save_static.SaveinText(Save_static.按键, kk);
        读取();
    }
    public void 读取()
    {
        Debug.LogError("      读取()读取()读取()读取()           ");
        var a = Save_static.LoadinText<IK>(Save_static.按键);
        if (a == null)
        {
            新的();
            读取();
        }
        else
        {
            D= Initialize.GetFieldDictionary(a);
            //kk = Initialize.ConvertToInstance<IK>(D);
        }


    }

    public void ASDAsD()
    {

    }
}
