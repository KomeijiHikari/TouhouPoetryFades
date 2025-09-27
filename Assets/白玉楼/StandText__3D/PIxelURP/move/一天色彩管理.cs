using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 一天色彩管理 : MonoBehaviour
{
  [SerializeField]
  private 光照数据 s;
  [SerializeField]
  private List<twoColor> Cs ;

  private void Awake()
  {
    Cs = new List<twoColor>(s.Cs);
    for (int i = 0; i < Cs.Count; i++)
    {
    var a=Cs[i];
      if (i== Cs.Count-1)
      {
        a.Next = 0;
      }
      else
      {
        a.Next = i + 1;
      }

      a.开始时间Fix = Time_Tool.TimeVector3ToFixTime(a.开始时间);
      a.结束时间Fix1= Time_Tool.TimeVector3ToFixTime(Cs[a.Next].开始时间); 
    }

    for (int i = 0; i < s.Fs.Count; i++)
    {
      var a=s.Fs[i];
      
      if (i==  s.Fs.Count-1)
      {
        a.Next = 0;
      }
      else
      {
        a.Next = i + 1;
      }

      a.开始时间Fix = Time_Tool.TimeVector3ToFixTime(a.开始时间);
      a.结束时间Fix1= Time_Tool.TimeVector3ToFixTime(s.Fs[a.Next].开始时间); 
    }
    
    
  }

    static   Vector3 zero = new Vector3(23,59,59.999999f);


 
    public Color Get_FogColor(float Fixt)
    {
      for (int i = 0; i < s.Fs.Count; i++)
      {
        var a=s.Fs[i];
        var av = a.IsMe(Fixt );
        if (av > 0 && av <= 1)
        {  
          return Color.Lerp(a.light,s.Fs[a.Next] .light,av);
        }
      }
      Debug.LogError("离谱，看看看代码把"+s.Fs.Count);
      for (int i = 0; i < s.Fs.Count; i++)
      {
        var a=s.Fs[i];
        var av = a.IsMe(Fixt ,true); 
      }

      return Color.white;
    }
    public float Get_lerp(float Fixt)
    {
      for (int i = 0; i < Cs.Count; i++)
      {
        var a=Cs[i];
        var av = a.IsMe(Fixt);
        if (av > 0 && av <= 1)
        { 
          当前 = a;
          下一个 = Cs[当前.Next];
          return av;
        } 
      } 
      return -9999;
    }
 
    
    public twoColor 当前;
    public twoColor 下一个;
}
