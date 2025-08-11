using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "特效", menuName = "ScriptableObject/New 特效")]
public class 特效模板 : ScriptableObject
{ 
    public enum  位置中心类型
    {
                   原点,
                   碰撞体,
    }
    public List<AnimationClip>    clipS;
    public 位置中心类型 位置类型;
   public AnimationClip clip; 


    public bool 地面限定;
    public bool 是在目标的前面嘛;
    public bool 是跟随目标;
    public Vector2 偏移;

    public bool 同步玩家=false ;
}
