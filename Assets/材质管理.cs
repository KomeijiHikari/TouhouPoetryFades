using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 材质管理 :MonoBehaviour
{
    public static string Unli_Orther { get; } = "Unli_Orther";
    public static string Other { get; } = "Other";
            public static string _去色 { get; } = "_NoColor";
    public static string _EdgeColor { get; } = "_EdgeColor"; 
    public static string _SpriteColor { get; } = "_SpriteColor";
    public static string _Alpha { get; } = "_Alpha";
    public static string 残影 { get; } = "残影";
    public static Material Get_Material(string  s)
    {
        Material a= Resources.Load("Material/"+s) as Material;
        if (a==null )
        {
            Debug.LogError("寻找材质路径没找到："+ "Material/" + s);
            return null;
        }
        else
        {
            return a;
        }
    } 
}
