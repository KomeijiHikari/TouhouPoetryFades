using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Player_input;

public static  class  按键D
    {
    public static Dictionary<string, object> D;

    public static  string GetString(IK.IK_Name e)
    {
        object Nam;
        if (D.TryGetValue(e.ToString(), out Nam))
        {
            return Nam.ToString();
        }
        else
        {
            Debug_.LogError("Deb_没找到" + e);
            return "Deb_没找到";
        }


    }
}
public class 按键父类 : MonoBehaviour
{
    Text_button_Father T;
    public Dictionary<string, object> D { get
        {
            return D;
        }
        set {
            D=value;
        }
    }
    public static 按键父类 I;

    public 按键监听 j;

    //public IK kk;

    private void Awake()
    { 
        if (I != null ) Destroy(this);
        else   I = this;
        //Debug.LogError();

        D= new Dictionary<string, object>();

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
