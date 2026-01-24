using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Sirenix.OdinInspector;
public class 键位提示转换 : MonoBehaviour
{
    public Player_input.IK.IK_Name e;
    [Button]
    public string 原地键位提示(string s )
    {
        if (string.IsNullOrEmpty(s))
        {
            Debug.Log("输入的字符串为空");
            return "Nnnnnull";
        }

        // 解析字符串中的语法标记并替换
        string result = 解析键位标记(s);

        // 在实际项目中，这里应该是显示到UI或调试输出
        Debug.Log("键位提示: " + result);
        return result;
        // 如果需要显示在屏幕上（示例）
        // 可以在这里将result显示到Text组件或其他UI元素
    }

    /// <summary>
    /// 解析字符串中的键位标记并替换为对应按键
    /// </summary>
    private string 解析键位标记(string input)
    {
        // 使用正则表达式匹配所有 {枚举名} 格式的标记
        // 例如：{攻击}、{跳跃}、{上}
        Regex regex = new Regex(@"\{(\w+)\}");

        // 使用MatchEvaluator来替换每个匹配
        string result = regex.Replace(input, 替换标记);

        return result;
    }    /// <summary>
         /// 替换单个标记的回调函数
         /// </summary>
    private string 替换标记(Match match)
    {
        // match.Groups[1] 是括号内的内容，例如 "攻击"
        string enumName = match.Groups[1].Value;

        try
        {
            // 将字符串转换为枚举值
            // 注意：这里假设IK_Name枚举在Player_input.IK命名空间下
            Player_input.IK.IK_Name enumValue = (Player_input.IK.IK_Name)Enum.Parse(
                typeof(Player_input.IK.IK_Name),
                enumName
            );

            // 使用GetString方法获取按键字符串
            // 这里需要根据实际的GetString方法调用来调整
            string keyString = 按键D.GetString(enumValue);

            return keyString;
        }
        catch (ArgumentException)
        {
            // 如果枚举名无效，返回原始标记并记录错误
            Debug.LogWarning($"找不到对应的枚举值: {enumName}");
            return match.Value; // 返回原始标记，如"{攻击}"
        }
    } 
}
