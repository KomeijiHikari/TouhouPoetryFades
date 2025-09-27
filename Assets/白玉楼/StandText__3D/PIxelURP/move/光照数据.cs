using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "光照数据", menuName = "ScriptableObject/new   光照数据")]
public class 光照数据 : ScriptableObject
{
    public List<twoColor> Cs;
    public List<Fog> Fs;
}

public  class ColorListPool 
{
    public  int Next;
    public  Vector3 开始时间; 
    public Color  light;
    public float 开始时间Fix; 
    [SerializeField]
      float 结束时间Fix;

    public float 结束时间Fix1
    {
        get
        {
            if (Next==0)
            {
                return 结束时间Fix + 24*3600;
            }

            return 结束时间Fix;
        }
        set => 结束时间Fix = value;
    }

    public float IsMe(float tim,bool B=false)
    {
        // 计算时间区间长度
        float intervalLength = 结束时间Fix1 - 开始时间Fix;

        if (Next==0)
        {
            if (tim< 开始时间Fix)
            {
                tim+= 24*3600;
            } 
        } 
        // 计算时间在区间中的位置比例
        float position = (tim - 开始时间Fix) / intervalLength;

        if (B)
        {
          
            Debug.LogError("结束"+结束时间Fix1 +"      开始"+开始时间Fix+"     减一下后"+ intervalLength+
                         "  输入"+  tim +"  最后"+position);
        } 
  
        return position;
    }
}

[System.Serializable]
public class Fog:ColorListPool 
{ 

 
}
[System.Serializable]
public  class twoColor:ColorListPool 
{ 
    public Color shadow;  // 阴影颜色
    public  Time_Tool.时间阶段 E;   
}