using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "弹问的动画", menuName = "片段")]
public class 单片段 : ScriptableObject
{
    public List<Sprite> sps;
    public  float 单帧长=0.1f;
    public bool 循环;
    public float 总时长 { get {  return sps.Count *单帧长;      } }

 
} 