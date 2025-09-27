using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VFolders.Libs;
 using Sirenix.OdinInspector;
 using Sirenix.OdinInspector;
 using UnityEngine.PlayerLoop;
 using System;
 using System.Linq;
 using Schema.Internal.Types;
 using UnityEngine.Serialization;
 using MonoBehaviour = UnityEngine.MonoBehaviour;
 using Quaternion = UnityEngine.Quaternion;
 using Transform = UnityEngine.Transform;
 using Vector2 = UnityEngine.Vector2;
 using Vector3 = UnityEngine.Vector3;
 using Vector4 = UnityEngine.Vector4;

 // [ExecuteAlways]
public class 我的光照2 : MonoBehaviour
{
    [SerializeField]
    private 一天色彩管理 Y;
    public static 我的光照2 I { get; private set; }
    public List<SpriteRenderer>  ForgSprites;
    public List< Renderer> 受影响;

    string DarkName="_DarkColor" ;
    string BounsName= "_Bouns"; 
    string SizeName= "_Size";
     string  WayColorName = "_WayColor";
     string WayName = "_Way";
     // string FogName = "_FogColor";
     [Space(10)]
     [SerializeField]
    private Transform Way;
    [SerializeField]
    private Transform Chlri;
    [Space(10)]
    [SerializeField]
    private Vector3 NorWay; 
    [Space(10)] 
    
    [Space(10)]
    [SerializeField]  float Fixtime;
    [SerializeField]  private float Y_;
    [SerializeField]  private float X_; 
     
    [Space(10)]
    public float speed=1;

    public bool Stop;
    [Space(10)]
    public Vector3  HourseVector;
    public    Vector3  TimeVector ;


public    Vector3  DodayVector ;

 
[Space(10)]
[SerializeField]
private Color Out_SunColor=Color.white;
[SerializeField]
private Color Out_ShaowColor=Color.white;
[Space(10)]
public Color 幕布Color;

[Space(10)]
public bool DebOutcolor;
public bool 是白天;  

Vector3 zeroTime=new Vector3(23,59,59.9999999f);

private void Awake()
{
    if (I != null && I != this)    Destroy(this); 
    else        I = this;
    Y = GetComponent<一天色彩管理>();
}

void FixedUpdate()
    {  // 获取枚举的所有值
///当暂停得时候
/// 时间增量停止
///
/// 按照houseV 变化
        
        if (speed != 0&& !Stop)
        {
            Fixtime += Time.fixedDeltaTime* speed;
      
            TimeVector=   Time_Tool. FixTimeToTimeVector3(Fixtime); 
            
            HourseVector  =new Vector3(TimeVector.x % 24,TimeVector.y,TimeVector.z) ;
        }


        float       角度 = -200;;
        // if (false)
        {
            Y_=  Time_Tool. TimeToAngle(HourseVector);
             角度 = 0;
            if (Y_<-90&&Y_>=-270)
            {
                //-90到-270;是白天
                是白天 = true;
                角度 = Y_; 
            }
            else
            {
                // -270到-90是黑夜
                角度 = Y_-180 ;
          
                是白天 = false;
            }   
        }
  
         
        Chlri.rotation = Quaternion.Euler(new Vector3(X_, 0, 角度) );
   
        
        DodayVector = Time_Tool.ConvertTimeToDate(TimeVector);

 
 
        Debug.LogError(Y.当前.E);
        
        if ( DebOutcolor) return;
        float lerp = Y.Get_lerp(  Time_Tool.TimeVector3ToFixTime(HourseVector)   );
        Out_SunColor=Color.Lerp(Y.当前.light, Y.下一个.light, lerp); 
        Out_ShaowColor = Color.Lerp(Y.当前.shadow, Y.下一个.shadow, lerp);
       
    } 
 
    private void Update ()
    {
        幕布Color = Y.Get_FogColor(Time_Tool.TimeVector3ToFixTime(HourseVector));
        NorWay =      Chlri.position - Way.position;
        if (受影响 != null)
        {
            for (int ML = 0; ML < 受影响.Count; ML++)
            { 
                var M精灵图材质 = 受影响[ML];
              if(!M精灵图材质.enabled)return;
              SpriteRenderer sp =  M精灵图材质 as      SpriteRenderer ;
              
              Vector2 Ori = M精灵图材质.bounds.min;
              Vector2 Size = (Vector2)(M精灵图材质.bounds.max) - Ori;
              Vector4 Bouns = new Vector4(Ori.x, Ori.y, Size.x, Size.y); 

              M精灵图材质.material.SetVector(WayName,  -NorWay);                 
              M精灵图材质.material.SetColor(WayColorName, Out_SunColor);        
              M精灵图材质.material.SetColor(DarkName,CHHC.Flip(Out_ShaowColor) );   
              
              if (sp!=null)
              {
                  Vector4 Size__ = new Vector4(sp.sprite.rect.size.x, sp.sprite.rect.size.y, 0, 1);
                  
                  
                  M精灵图材质.material.SetVector(BounsName, Bouns);///v2 rect位置和信息  世界坐标，精灵图左下角，坐标，精灵图尺寸
                  M精灵图材质.material.SetVector(SizeName, Size__);///v2 rect像素尺寸  W分量是 开关
              } 
            } 
        }

        if (ForgSprites!=null)
        {
            for (int F = 0; F < ForgSprites.Count; F++)
            {
                var a= ForgSprites[F] ;
                a.color = 幕布Color;
            }
        }
    }
    // Time_Tool.时间阶段 Ge_to_V3to时间阶段(Vector3 v)//时间V3 属于比他小但是最接近的
    // {
    //     List<twoColor> aaa=new List<twoColor>();
    //     for (int i = 0; i < Cs.Count; i++)
    //     {
    //         ///所有比我小的
    //         var a = Cs[i];
    //         if (a.开始时间 <Time_Tool.TimeVector3ToFixTime(v))
    //         {
    //             aaa.Add(a); 
    //         }
    //     }
    //
    //     Time_Tool.时间阶段  asd=Time_Tool.时间阶段.深夜;
    //     float MAx=-1;
    //     for (int i = 0; i < aaa.Count; i++)
    //     {
    //         //比我晓得列表找最大的
    //         var a = aaa[i]; 
    //         MAx = Mathf.Max(a.开始时间,MAx) ;
    //         if (MAx == a.开始时间) asd = a.E;
    //     } 
    //     return asd;
    // }
    
/// <summary>
/// 返回第一个比我大的
/// </summary>
/// <param name="e"></param>
/// <returns></returns>
    // Vector3 Ge_to_时间阶段toV3(   Time_Tool.时间阶段 e)
    // {
    //     for (int i = 0; i < Cs.Count; i++)
    //     {
    //         var a = Cs[i];
    //         if (e==a.E)
    //         {
    //             return a.开始时间;
    //         }
    //     }
    //     return new Vector3(-1,-1,-1);
    // }
}

public static class Time_Tool
{
    /// <summary>
    /// 将时间单位的Vector3转换回浮点时间
    /// </summary>
    /// <param name="timeVector">Vector3(x=小时, y=分钟, z=秒)</param>
    /// <returns>总秒数</returns>
    public static float TimeVector3ToFixTime(Vector3 timeVector)
    {
        float hours = timeVector.x;
        float minutes = timeVector.y;
        float seconds = timeVector.z;
        
        return (hours * 3600f) + (minutes * 60f) + seconds;
    }
    /// <summary>
    /// 将浮点时间转换为时间单位的Vector3
    /// </summary>
    /// <param name="fixtime">输入的时间值（秒）</param>
    /// <returns>Vector3(x=小时, y=分钟, z=秒)</returns>
    public static Vector3 FixTimeToTimeVector3(float fixtime)
    {
        // 计算总秒数
        float totalSeconds = fixtime;
        
        // 计算小时数
        int hours = Mathf.FloorToInt(totalSeconds / 3600f);
        
        // 计算剩余秒数（扣除小时部分）
        float remainingSeconds = totalSeconds - (hours * 3600f);
        
        // 计算分钟数
        int minutes = Mathf.FloorToInt(remainingSeconds / 60f);
        
        // 计算秒数（扣除分钟部分），保留5位小数
        float seconds = remainingSeconds - (minutes * 60f);
        
        // 确保秒数保留5位小数
        seconds = (float)System.Math.Round(seconds, 5);
        
        return new Vector3(hours, minutes, seconds);
    }
    
    /// <summary>
    /// 将Vector3时间格式转换为昼夜循环角度
    /// </summary>
    /// <param name="time">Vector3时间变量(x=小时, y=分钟, z=秒)</param>
    /// <returns>角度值(-360到0度)</returns>
    public static float TimeToAngle(Vector3 time)
    {
        // 将Vector3时间转换为总秒数
        float totalSeconds = TimeVector3ToFixTime(time);
        
        // 调用Fixtime版本的方法
        return TimeToAngle(totalSeconds);
    }


        /// <summary>
    /// 将Fixtime时间值转换为昼夜循环角度
    /// </summary>
    /// <param name="fixtime">时间值(秒)</param>
    /// <returns>角度值(-360到0度)</returns>
    public static float TimeToAngle(float fixtime)
    {
        // 24小时对应的总秒数
        const float secondsInDay = 24f * 3600f;
        
        // 确保时间在0到24小时范围内(处理超过24小时的情况)
        float normalizedTime = fixtime % secondsInDay;
        if (normalizedTime < 0)
            normalizedTime += secondsInDay;
        
        // 计算时间在一天中的比例 (0到1)
        float timeRatio = normalizedTime / secondsInDay;
        
        // 将比例转换为角度: 24小时 = -360度 (逆时针)
        // 0点对应角度0度，24点对应-360度
        float angle = -360f * timeRatio;
        
        return angle;
    }
    
    /// <summary>
    /// 将角度值转换回Vector3时间格式
    /// </summary>
    /// <param name="angle">角度值(-360到0度)</param>
    /// <returns>Vector3时间变量(x=小时, y=分钟, z=秒)</returns>
    public static Vector3 AngleToTimeVector3(float angle)
    {
        // 将角度规范到-360到0范围内
        float normalizedAngle = angle % 360f;
        if (normalizedAngle > 0)
            normalizedAngle -= 360f;
        
        // 将角度转换为时间比例
        float timeRatio = -normalizedAngle / 360f;
        
        // 计算总秒数
        float totalSeconds = timeRatio * 24f * 3600f;
        
        // 转换为Vector3时间格式
        return FixTimeToTimeVector3(totalSeconds);
    }
    
    /// <summary>
    /// 将角度值转换回Fixtime时间值
    /// </summary>
    /// <param name="angle">角度值(-360到0度)</param>
    /// <returns>时间值(秒)</returns>
    public static float AngleToFixTime(float angle)
    {
        // 将角度规范到-360到0范围内
        float normalizedAngle = angle % 360f;
        if (normalizedAngle > 0)
            normalizedAngle -= 360f;
        
        // 将角度转换为时间比例
        float timeRatio = -normalizedAngle / 360f;
        
        // 计算总秒数
        return timeRatio * 24f * 3600f;
    } 
    
 
        /// <summary>
    /// 将时间向量转换为日期向量
    /// 规则：一年360天，12个月，每月30天
    /// 只要有1秒就算一天，日分量从1开始
    /// </summary>
    /// <param name="time">时间向量(x=小时, y=分钟, z=秒)</param>
    /// <returns>日期向量(x=年, y=月, z=日)</returns>
    public static Vector3 ConvertTimeToDate(Vector3 time)
    {
        // 将时间向量转换为总秒数
        float totalSeconds = time.x * 3600f + time.y * 60f + time.z;
        
        // 计算总天数（向上取整，只要有1秒就算一天）
        float totalDays = Mathf.Ceil(totalSeconds / (24f * 3600f));
        
        // 计算年数（一年360天）
        int years = Mathf.FloorToInt((totalDays - 1) / 360f);
        
        // 计算剩余天数（扣除年份部分）
        float remainingDays = totalDays - years * 360f;
        
        // 计算月数（一月30天）
        int months = Mathf.FloorToInt((remainingDays - 1) / 30f);
        
        // 计算天数（从1开始）
        int days = Mathf.CeilToInt(remainingDays - months * 30f);
        
        // 确保天数在有效范围内（1-30）
        if (days < 1) days = 1;
        if (days > 30) days = 30;
        
        return new Vector3(years, months, days);
    }
    
    /// <summary>
    /// 将日期向量转换为时间向量
    /// </summary>
    /// <param name="date">日期向量(x=年, y=月, z=日)</param>
    /// <returns>时间向量(x=小时, y=分钟, z=秒)</returns>
    public static Vector3 ConvertDateToTime(Vector3 date)
    {
        int years = Mathf.FloorToInt(date.x);
        int months = Mathf.FloorToInt(date.y);
        int days = Mathf.FloorToInt(date.z);
        
        // 计算总天数（一年360天，一月30天）
        // 日从1开始，所以减1
        float totalDays = years * 360f + months * 30f + (days - 1);
        
        // 转换为秒数（一天24小时）
        float totalSeconds = totalDays * 24f * 3600f;
        
        // 转换为时间向量
        int hours = Mathf.FloorToInt(totalSeconds / 3600f);
        float remainingSeconds = totalSeconds - (hours * 3600f);
        int minutes = Mathf.FloorToInt(remainingSeconds / 60f);
        float seconds = remainingSeconds - (minutes * 60f);
        
        // 确保秒数保留5位小数
        seconds = (float)System.Math.Round(seconds, 5);
        
        return new Vector3(hours, minutes, seconds);
    }

 
    // if (totalHours < 6f) return 时间阶段.深夜;
    // else if (totalHours < 7.2f) return 时间阶段.黎明前;
    // else if (totalHours < 8.4f) return 时间阶段.黎明;
    // else if (totalHours < 9.6f) return 时间阶段.清晨;
    // else if (totalHours < 14.4f) return 时间阶段.白天;
    // else if (totalHours < 15.6f) return 时间阶段.傍晚;
    // else if (totalHours < 16.8f) return 时间阶段.黄昏;
    // else if (totalHours < 24f) return 时间阶段.夜晚;
    // else return 时间阶段.深夜;
    /// <summary>
    /// 一天中的时间阶段枚举
    /// </summary>
    public enum 时间阶段
    {

        深夜,  
        黎明前前,
        黎明前,    
        黎明,          
        清晨,   
        白天,
        下午,
        傍晚,         
        黄昏,          
        夜晚  ,
        前半夜
    } 
}
public static class CHHC
{
    public static Color Flip(Color c)
    {
        Vector3  Hc;
        Hc=RGBToHSL(c);
        Hc.z = 1 - Hc.z;
        return HSLToRGB(Hc);
    }
    /// <summary>
    /// 将RGB颜色转换为HSL颜色
    /// </summary>
    /// <param name="color">RGB颜色</param>
    /// <returns>HSL颜色 (x=色相[0-1], y=饱和度[0-1], z=亮度[0-1])</returns>
    public static Vector3 RGBToHSL(Color color)
    {
        return RGBToHSL(color.r, color.g, color.b);
    }

    /// <summary>
    /// 将HSL颜色转换为RGB颜色
    /// </summary>
    /// <param name="hsl">HSL颜色 (x=色相[0-1], y=饱和度[0-1], z=亮度[0-1])</param>
    /// <returns>RGB颜色</returns>
    public static Color HSLToRGB(Vector3 hsl)
    {
        return HSLToRGB(hsl.x, hsl.y, hsl.z);
    }

    /// <summary>
    /// 将HSL颜色转换为RGB颜色
    /// </summary>
    /// <param name="h">色相 [0-1]</param>
    /// <param name="s">饱和度 [0-1]</param>
    /// <param name="l">亮度 [0-1]</param>
    /// <returns>RGB颜色</returns>
    public static Color HSLToRGB(float h, float s, float l)
    {
        // 确保输入值在有效范围内
        h = Mathf.Repeat(h, 1f);
        s = Mathf.Clamp01(s);
        l = Mathf.Clamp01(l);

        // 计算中间RGB值
        Vector3 rgb = new Vector3(
            Mathf.Abs((h * 6f + 0f) % 6f - 3f) - 1f,
            Mathf.Abs((h * 6f + 4f) % 6f - 3f) - 1f,
            Mathf.Abs((h * 6f + 2f) % 6f - 3f) - 1f
        );

        // 限制在0-1范围内
        rgb.x = Mathf.Clamp01(rgb.x);
        rgb.y = Mathf.Clamp01(rgb.y);
        rgb.z = Mathf.Clamp01(rgb.z);

        // 平滑处理
        rgb.x = rgb.x * rgb.x * (3f - 2f * rgb.x);
        rgb.y = rgb.y * rgb.y * (3f - 2f * rgb.y);
        rgb.z = rgb.z * rgb.z * (3f - 2f * rgb.z);

        // 应用饱和度和亮度
        Vector3 result = Vector3.Lerp(Vector3.one, rgb, s) * l;

        return new Color(result.x, result.y, result.z);
    }


    /// <summary>
    /// 将RGB颜色转换为HSL颜色
    /// </summary>
    /// <param name="r">红色 [0-1]</param>
    /// <param name="g">绿色 [0-1]</param>
    /// <param name="b">蓝色 [0-1]</param>
    /// <returns>HSL颜色 (x=色相[0-1], y=饱和度[0-1], z=亮度[0-1])</returns>
    public static Vector3 RGBToHSL(float r, float g, float b)
    {
        // 确保输入值在有效范围内
        r = Mathf.Clamp01(r);
        g = Mathf.Clamp01(g);
        b = Mathf.Clamp01(b);

        float max = Mathf.Max(r, Mathf.Max(g, b));
        float min = Mathf.Min(r, Mathf.Min(g, b));
        float delta = max - min;

        float h = 0f;
        float s = 0f;
        float l = (max + min) * 0.5f;

        if (delta > 0.0001f) // 避免除零
        {
            s = l > 0.5f ? delta / (2f - max - min) : delta / (max + min);

            if (max == r)
            {
                h = (g - b) / delta + (g < b ? 6f : 0f);
            }
            else if (max == g)
            {
                h = (b - r) / delta + 2f;
            }
            else
            {
                h = (r - g) / delta + 4f;
            }

            h /= 6f;
        }

        return new Vector3(h, s, l);
    }

    /// <summary>
    /// 调整颜色的色相
    /// </summary>
    /// <param name="color">原始颜色</param>
    /// <param name="hueShift">色相偏移量 [0-1]</param>
    /// <returns>调整后的颜色</returns>
    public static Color AdjustHue(Color color, float hueShift)
    {
        Vector3 hsl = RGBToHSL(color);
        hsl.x = Mathf.Repeat(hsl.x + hueShift, 1f);
        return HSLToRGB(hsl);
    }

    /// <summary>
    /// 调整颜色的饱和度
    /// </summary>
    /// <param name="color">原始颜色</param>
    /// <param name="saturation">饱和度乘数</param>
    /// <returns>调整后的颜色</returns>
    public static Color AdjustSaturation(Color color, float saturation)
    {
        Vector3 hsl = RGBToHSL(color);
        hsl.y = Mathf.Clamp01(hsl.y * saturation);
        return HSLToRGB(hsl);
    }

    /// <summary>
    /// 调整颜色的亮度
    /// </summary>
    /// <param name="color">原始颜色</param>
    /// <param name="lightness">亮度乘数</param>
    /// <returns>调整后的颜色</returns>
    public static Color AdjustLightness(Color color, float lightness)
    {

        Vector3 hsl = RGBToHSL(color);
        hsl.z = Mathf.Clamp01(hsl.z * lightness);
        return HSLToRGB(hsl);
    }
    
    
    
    
}




