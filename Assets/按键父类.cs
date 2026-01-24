using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using static Player_input;

public static  class  按键D
    {
    public static Dictionary<string, object> D
    {
        get
        {
            if (d == null) { 
                按键父类.读取();
            }
            return d;
        }
        set
        {
            d = value;
        }
    }
    private static Dictionary<string, object> d;
    public static string ReplaceKeys(string s)
    {
        if (string.IsNullOrEmpty(s))
            return s;

        // 使用正则表达式匹配所有{}包裹的字符串
        return Regex.Replace(s, @"\{([^{}]+)\}", match =>
        {
            // 获取{}内的字符串
            string key = match.Groups[1].Value;

            // 从字典中查找对应的值
            object value;
            if (D.TryGetValue(key, out value))
            {
                return value.ToString();
            }

            // 如果找不到，记录错误并保留原字符串
            Debu.LogError(Bug + key);
            return match.Value; // 保留原始{}包裹的内容
        });
    }
    [Obsolete("被ReplaceKeys打爆了")]
    public static  string ReplaceKey(string s)
    {
        string 其中枚举 = Get语法内字符串_首个为准(s);
        string StringKey=     GetEnumKey(其中枚举);
        if (StringKey == Bug)
        {
        Debu.LogError(Bug);
            return Bug;
        }
        else
        {
            return Replace(s, StringKey);
        }

    }
      
    public static string Get语法内字符串_首个为准(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        Match match = Regex.Match(input, @"\{([^{}]+)\}");
        return match.Success ? match.Groups[1].Value : string.Empty;
    }
    /// <summary>
    /// 替换
    /// </summary> 
    /// <param name="replacement">注入内容</param> 
    public static string Replace(string input, string replacement)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return Regex.Replace(input, @"\{[^{}]*\}", replacement);
    }
    public static string GetEnumKey(string s)
    {
        object Nam;
     if(D.TryGetValue(s, out Nam))
        {
            return Nam.ToString();
        }
        else
        {
            Debu.LogError(Bug + s);
            return Bug;
        }
    }
    static string  Bug=> "Deb_没找到";
    public static IK.IK_Name GetEnum(string s)
    {

        if (string.IsNullOrEmpty(s)) return default(IK.IK_Name);

        // 方法1：直接使用Enum.TryParse
        if (System.Enum.TryParse<IK.IK_Name>(s, true, out IK.IK_Name result))
        {
            return result;
        }

        // 方法2：如果找不到，可以遍历枚举值进行比较
        foreach (IK.IK_Name value in System.Enum.GetValues(typeof(IK.IK_Name)))
        {
            if (value.ToString().Equals(s, System.StringComparison.OrdinalIgnoreCase))
            {
                Debug.LogError("遍历了一下找到了");
                return value;
            }
        }
        Debug.LogError(Bug + s);
        return default(IK.IK_Name);
    }
    public static  string GetString(IK.IK_Name e)
    {
        return GetEnumKey(e.ToString());
    }
}
public class 按键父类 : MonoBehaviour
{
    Text_button_Father T;
    public Dictionary<string, object> D { get
        {
            return 按键D.D;
        }
        set {
            按键D.D = value;
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
            var a = Initialize.ConvertToInstance<IK>(D);
            Save_static.SaveinText(Save_static.按键, a);
        };
    } 
    public static void 新的()
    {
        var kk = 来个新的();
        Save_static.SaveinText(Save_static.按键, kk);
        读取();
    }
    public static void 读取()
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
            按键D. D = Initialize.GetFieldDictionary(a); 
        } 
    } 
}
