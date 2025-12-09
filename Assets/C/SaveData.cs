using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Trisibo;
using UnityEngine.SceneManagement;
using System;

public class SaveData : MonoBehaviour 
{
    [SerializeField]
    [DisplayOnly]
    int 最后保存的保存点序号;

    [SerializeField ][DisplayOnly]
   string  最后保存的场景的名字;
 public     struct txt 
    {
 
       public  int 最后保存的保存点序号;
        public string 最后保存的场景的名字;
 
    }

    private void Start()
    {
        loadText();
        Event_M.I.Add(Event_M  .场景保存触发, 保存最后场景);
    }
    void 保存最后场景(GameObject a)
    {
        var co = GetComponent<Collider2D>();
       var  R = Physics2D.Raycast(co.bounds.min, Vector2 .down, 2f, 1 << Initialize .L_Ground );
        if (R.collider!=null)
        {
            Scene Z = R.collider.gameObject.scene; 
            最后保存的场景的名字 = Z.name  ;
            saveText(); 
        }

    }

    
    void  开箱(txt Cla)
    {
  
        最后保存的场景的名字 = Cla.最后保存的场景的名字; 
    }
    txt 装箱()
    {
        var a = new txt();
        a.最后保存的场景的名字 = 最后保存的场景的名字;
 
        return a;
    }
    public void savebypre()
    {
        Save_static.InPref("class", 装箱());
    }
    public void Loadbypre()
    {
        开箱(JsonUtility.FromJson<txt>(Save_static.OutPref("class")));
    }
    public  void saveText()
    {
        Save_static.SaveinText(Save_static.text, 装箱());
    }
    public void loadText()
    { 
        开箱(Save_static.LoadinText<txt>(Save_static.text));
    }

    //[UnityEditor .MenuItem ("Tools/DeleText")]
    //public void DeleText()
    //{
    //    Save_static.DeletsText(Save_static.text);
    //}
 
}

/// <summary>
/// 该类用于 序列化   Dictionary<String, object>  字典
/// </summary>
public class J_Dictionary
{
    [SerializeField]
    public List<String> Key = new List<string>(); 
    [SerializeField]
    public List<Value> Value_  = new List<Value>();
    public J_Dictionary(Dictionary<String, object> D)
    {
        foreach (var item in D)
        {
            Key.Add(item.Key);
            Value_ .Add(  Value .Set (item.Value )  );
        }
    }
    public   Dictionary<String, object> To_D( )
    {
        Dictionary<String, object> Out = new Dictionary<string, object>();
        if (Key.Count != Value_ .Count)
        {
            throw new NotImplementedException();
        }
        for (int i = 0; i < Key.Count; i++)
        {
            Out.Add(Key[i], Value_ [i].Get());
        }
        return Out;
    } 
}

/// <summary>
/// 开始游戏后载入一次load 到内存  
/// 游戏内字典作为容器进行读取
/// 保存的时候Save 该字典
/// </summary>
public static class Save_D
{
#if UNITY_EDITOR
    [UnityEditor.MenuItem("Tools/打印字典")]
#endif
    public static void  打印()
    {
        string s = "";
        foreach (var item in 存档字典_)
        {
            s += "\n" +"key:"+ item.Key + "    value:" + item.Value;
        }
        Debug.LogError(s);
    }
    //static void Save_Value_D(this object  value ,string  name   )
    //{
    //    if (name==null)
    //    {
    //        Debug.LogError("名字怎么能是空的");
    //    }
    //    存档字典_.Add(name,value);
    //}
    public static T Load_Value_D<T>(string name,bool Jes=false )
    {
        if (存档字典_ != null)
        {
            if (存档字典_.Count != 0)
            {
                if (存档字典_.ContainsKey(name))
                {
                    object   o = 存档字典_[name];
                    if (o != null)
                    {
                        if (Jes)
                        {
                            var a = JsonUtility.FromJson<T>(o.ToString ());
                            return a;
                        }
                        else
                        {
                            if (o is T type)
                            {
                                var a = (T)o;
                                return a;
                            }
                            else
                            {
                                Debug.LogError("有key但是  类型不符");
                                return default;
                            }
                        }

                    }
                    else
                    {
                        Debug.LogError("有key但是  空字符串");
                        return default;
                    }
                }
                else
                {
                    //Debug.LogError("无该 key      " + name);
                    return default;
                }
            }
            else
            {
                Debug.LogError("长度为0");
                return default;
            }

        }
        else
        {
            Debug.LogError("离谱，是空");
            return default;
        } 
    }

    public static Dictionary<String, object> Load(int i = 0)
    {
        var a = Save_static.LoadinText<J_Dictionary>(Save_static.字典 + i);
        if (a==null)
        {
            return null;
        }
        else
        {

            return a.To_D();
        } 
    }
    public static void Save(int i = 0 )
    { 
     var a=       存档字典_._toJ();
        a.SaveinText(Save_static.字典 + i);
    }

    /// <summary>
    /// 没有就添加  有就替换
    /// </summary>
    /// <param name="name"></param>
    /// <param name="o"></param>
    public static  void  Add(string name,object o )
    {
        Initialize_Mono.I?.Key_Action?.Invoke(name );
        if (存档字典_.ContainsKey(name))
        {
            存档字典_[name] = o;
        }
        else
        {
            存档字典_.Add(name,o);
        }
    }
  static Dictionary<String, object> 存档字典; 
    public  static Dictionary<string, object> 存档字典_
    {
        get
        {
            if (存档字典==null)
            {
                var a = Load();
                if (a == null)
                {
                    存档字典 = new Dictionary<string, object>();
                    Save( );
                }
                else
                {
                    存档字典 = a;
                }
            }  
            return 存档字典;
        }
     set
        {
            if (value==null)
            {
                存档字典 = null;
            }
            存档字典 = value;
        }
    } 
    public static J_Dictionary _toJ(this Dictionary<String, object> D)
    {
        return new J_Dictionary(D);
    }
}
public static class Save_static
{
    public static string 按键 { get; } = "按键";
    public static string 已经死掉的机关 { get; } = "机关";
    public static string text { get; } = "text";
    public static string 存档点位 { get; } = "Save_way";
    public static string 能力数据 { get; } = "能力";

    public static string  字典 { get; } = "字典";

    public static string 小地图 { get => 小地图显示.I.Name; }

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Tools/删键")]
#endif 
    public static void  DeleKey()
    {
        DeletsText(按键 + ".txt");
    }
#if UNITY_EDITOR
    [UnityEditor.MenuItem("Tools/删档")]
#endif

    public static void 删除所有()
    {
        DeletsText(小地图 + ".txt");
        DeletsText(存档点位+".txt");
        DeletsText(text + ".txt");
        DeletsText(已经死掉的机关 + ".txt");
        for (int i = 0; i  <10; i ++)
        {
            DeletsText(字典+i + ".txt");
        }
    }
    public static string OutPref(string k)
    {
        return PlayerPrefs.GetString(k, null);
    }
    public static  void InPref(string  k,object  o)
    {
        var j = JsonUtility.ToJson(o);

        PlayerPrefs.SetString(k,j);
        PlayerPrefs.Save();
    }

    public static void SaveinText( this  object obj, string saveFileName)
    {
        SaveinText( saveFileName,  obj);
    }
    public static void SaveinText(string  saveFileName,object obj)
    { 
   
        //如果已经存在，就会覆盖掉
        var j = JsonUtility.ToJson(obj,true); 
        var path = Path.Combine (Application.persistentDataPath,saveFileName+".txt");
        if (Initialize_Mono.I.打包额外打印)
        {
            Debug.LogError("文件名字:" + saveFileName + "路径    " + path+"内容 "+j);
        }


        try
        {
            File.WriteAllText(path, j);
#if UNITY_EDITOR
            Debug.Log("存到了" + path);
#endif
        }
        catch (System.Exception   e)
        {
#if UNITY_EDITOR
            Debug.LogError($"存到{path}.\n{e}");
#endif
        } 
    }   
    

  
    /// <summary>
          /// 加载
          /// </summary>
          /// <param name="saveFileName"></param>
    public static T LoadinText <T>(string saveFileName)
    {
        //Only. 防止重复();
        var path = Path.Combine(Application.persistentDataPath, saveFileName) + ".txt";
        try
        {
            var j = File.ReadAllText(path);
            var data = JsonUtility.FromJson<T>(j); 
            return data;
        }
        catch (System.Exception   e)
        {
#if UNITY_EDITOR
            Debug.LogWarning($"读到{path}.\n{e}"+"转换成"  );
#endif

            return default;
        }
    } 
    /// <summary>
    /// 删档
    /// </summary>
    /// <param name="saveFileName"></param>
    public static  void DeletsText (string saveFileName)
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        try
        {
            File.Delete(path );
        }
        catch (System.Exception  e)
        {

#if UNITY_EDITOR
            Debug.LogError($"读到{path}.\n{e}");
#endif
        }

    } 
}

public class Only
{
    static int T;
    static int C;
    public static void    防止重复(int i=500)
    {
        if (T!=Time .frameCount)
        {
            C = 0;
               T = Time.frameCount;
        }
        else
        {
            C++;
        }

        if (C> i)
        {
            throw new System.ApplicationException();
        }
    }
} 

//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using UnityEngine;

//   [System.Serializable]
//public class SaveData : MonoBehaviour
//{
//    public string name = "player";
//    public int Leve = 0;
//    public Vector3 POS;


//    [System.Serializable]
//    struct Savvvv
//    {
//        //EventManager.I.Add(传送点.触发, 触发存档);
//        public string name;
//        public int Leve;
//        public Vector3 POS;
//    }



//    public void savebypre()
//    {
//        Save_Data.InPref("class", 装箱());
//    }
//    public void Loadbypre()
//    {
//        var j = Save_Data.Outtttt("class");
//        var Cla = JsonUtility.FromJson<Savvvv>(j);
//        开箱(Cla);
//    }

//    const string text = "text.ala";


//    void 开箱(Savvvv Cla)
//    {
//        name = Cla.name;
//        Leve = Cla.Leve;
//        POS = Cla.POS;
//    }
//    Savvvv 装箱()
//    {
//        var a = new Savvvv();
//        a.name = name;
//        a.POS = POS;
//        a.Leve = Leve;
//        return a;
//    }
//    public void saveText()
//    {
//        Save_Data.SaveinText(text, 装箱());
//    }
//    public void loadText()
//    {
//        var a = Save_Data.LoadinText<Savvvv>(text);
//        name = a.name;
//        Leve = a.Leve;
//        POS = a.POS;
//    }
//    [UnityEditor.MenuItem("Tools/DeleText")]
//    public void DeleText()
//    {
//        Save_Data.DeletsText(text);
//    }
 
//}
 

//public static class Save_Data
//{
//    public static void InPref(string k, object o)
//    {
//        var j = JsonUtility.ToJson(o);

//        PlayerPrefs.SetString(k, j);
//        PlayerPrefs.Save();
//    }

//    public static void SaveinText(string saveFileName, object obj)
//    {
//        //如果已经存在，就会覆盖掉
//        var j = JsonUtility.ToJson(obj, true);
//        var path = Path.Combine(Application.persistentDataPath, saveFileName);

//        try
//        {
//            File.WriteAllText(path, j);
//#if UNITY_EDITOR
//            Debug.Log("存到了" + path);
//#endif
//        }
//        catch (System.Exception e)
//        {
//#if UNITY_EDITOR
//            Debug.LogError($"存到{path}.\n{e}");
//#endif
//        }



//    }


//    public static T LoadinText<T>(string saveFileName)
//    {
//        var path = Path.Combine(Application.persistentDataPath, saveFileName);
//        try
//        {
//            var j = File.ReadAllText(path);
//            var data = JsonUtility.FromJson<T>(j);

//            return data;
//        }
//        catch (System.Exception e)
//        {
//#if UNITY_EDITOR
//            Debug.LogError($"读到{path}.\n{e}" + "转换成");
//#endif

//            return default;
//        }
//    }

//    public static void DeletsText(string saveFileName)
//    {
//        var path = Path.Combine(Application.persistentDataPath, saveFileName);
//        try
//        {
//            File.Delete(path);
//        }
//        catch (System.Exception e)
//        {

//#if UNITY_EDITOR
//            Debug.LogError($"读到{path}.\n{e}");
//#endif
//        }

//    }

//    public static string Outtttt(string k)
//    {
//        return PlayerPrefs.GetString(k, null);
//    }

//}
