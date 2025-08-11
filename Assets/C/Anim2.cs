using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[System.Serializable]
public class Anim2 
{
    public string name = null;
    public Tag_state3 tag_State;
    public AnimationClip clip = null;
    public float time = -999;
    [Space]
    [Header("头")]
    public string start = null;
    [Header("列表")]
    public int 上个状态的列表位置 = -999;
    public int 正常所属 = -999;
    [Space]
    [Header("尾巴")]
    public string end = null;
    public int next = -999;
    [Space]
    [Header("中间")]
    //public string 上一个 = null;
    public List<string> lasts = new List<string>();

    public   float  speed=1;
    /// <summary>
    /// 在所属大状态下的顺序
    /// </summary>
    public int playerOrder = -999;
    public bool removeMysefly;
    public List<string> centre = new List<string>();

    public float 进度_;
    public float 进度
    {
        get => 进度_;
        set=>进度_ = value;
    }

    void 普通Order()
    {
        for (int i = -3; i < 10; i++)
        {

            if (name.Contains("_" + i.ToString() + "_"))
            {
                playerOrder = i;
                break;
            }
            else
            {
                playerOrder = -99;
            }
        }
    }
    
    void 获取start(Tag_state3 v)
    {
        if (name.StartsWith(v.ToString() + "_"))
        {
            start = v.ToString();
            tag_State = v;
        }
    }
    void 获取last(Tag_state3 v)
    {
        if (name.Contains("_" + v.ToString() + "_"))
        {

            if (lasts[0] == null)
            {
                lasts[0] = (v.ToString());
            }
            else
            {
                lasts.Add(v.ToString()); 
            }

        }
    }
    
    
    void 中间池加入()
    {
        for (int i = 0; i < TAG.Tag_centre.Length; i++)
        {
            if (name.Contains(TAG.Tag_centre[i]) )
            {
                centre.Add(TAG.Tag_centre[i]);
            }
        }
        //foreach (string v in TAG.Tag_centre)
        //{
        //    if (name.Contains(v))
        //    {
        //        centre.Add(v);
        //    }
        //}
    }
    void 获取超级order()
    {
        if (Is_SuperState())
        {
            for (int i = 0; i < 30; i++)
            {

                if (name.Contains("_" + i.ToString() + "_"))
                {
                    playerOrder = i;
                    break;
                }
                else
                {
                    playerOrder = -99;
                }
            }
        }
    }
    void 获取end和next()
    {
        for (int i = 0; i < TAG.Tag_end.Length; i++)
        {
            string v = TAG.Tag_end[i];

            if (name.EndsWith(v))
            {
                end = v;
            }
            else
            {
                for (int j = -3; j < 10; j++)
                {
                    string potentialEnd = v + j;
                    if (name.EndsWith(potentialEnd))
                    {
                        end = potentialEnd;
                        next = j;
                        break; // Once found, break out of inner loop to check next value in outer loop  
                    }
                }
            }

            // If end is found, break out of outer loop to avoid processing other values in TAG.Tag_end  
            if (end != null) { break; }
        }
    }
    void 获取超级end和next()
    {
        if (Is_SuperState())
        {
            for (int i = 0; i < 30; i++)
            {

                if (name.EndsWith("_to" + i.ToString()))
                {
                    end = "_to" + i.ToString();
                    next = i;
                    break;
                }
                else
                {
                    next = -99;
                }
            }
        }


    }
    void 初次获取两个列表值()
    {
        // 获取Tag_state3枚举的所有值  
        Tag_state3[] enumValues = (Tag_state3[])Enum.GetValues(typeof(Tag_state3));

        // 遍历枚举值  
        for (int i = 0; i < enumValues.Length; i++)
        {
            Tag_state3 v = enumValues[i];

            // 检查当前枚举值是否与start匹配  
            if (v.ToString() == start)
            {
                正常所属 = TAG.十位数((int)v);
                上个状态的列表位置 = 正常所属;
                // 一旦找到匹配项，可以退出循环  
                break;
            }
        }
    }
    void 原列表值覆盖()
    {
        // 获取Tag_state3枚举的所有值  
        Tag_state3[] enumValues = (Tag_state3[])Enum.GetValues(typeof(Tag_state3));

        // 检查lasts数组的第一个元素是否不为null  
        if (lasts[0] != null)
        {
            // 遍历枚举值  
            for (int i = 0; i < enumValues.Length; i++)
            {
                Tag_state3 v = enumValues[i];

                // 检查当前枚举值是否与lasts[0]匹配  
                if (v.ToString() == lasts[0])
                {
                    上个状态的列表位置 = TAG.十位数((int)v);
                    // 一旦找到匹配项，可以退出循环  
                    break;
                }
            }
        }
    }
    void 移除自己开关是否打开()
    {
        ///1从高流向低的时候会打开
        ///2底流向高的时候不会
        ///当1，播放结束的时候这个ANIM所在的列表位置会为空
        ///当这个是一个动画的结束的时候（动画实际角度上）这个将会1    
        ///当这个是是个动画开始的时候这个将会2
        if (!Get_fineLastsTag(Tag_state3.anystate)
            //上一个 != Tag_state3.anystate.ToString()
            && 上个状态的列表位置 > 正常所属
            )
        {
            removeMysefly = true;
        }
    }

    public Anim2(AnimationClip clip_)
    {
        centre.Add(null);
        lasts.Add(null);
        clip = clip_;
        time = clip.length;
        name = clip.name;




        普通Order();


        Tag_state3[] enumValues = (Tag_state3[]) Enum.GetValues(typeof(Tag_state3));

        // 遍历枚举值  
        for (int i = 0; i < enumValues.Length; i++)
        {
            Tag_state3 v = enumValues[i];

            获取start(v);
            获取last(v);
        }


        获取超级order();
    


        中间池加入();



        获取end和next();

        获取超级end和next();


        初次获取两个列表值();

        原列表值覆盖();

        移除自己开关是否打开();
    }

    /// <summary>
    /// 看last里面有没有？这个状态
    /// </summary>
    /// <param name="_State3"></param>
    /// <returns></returns>
    public bool Get_fineLastsTag(Tag_state3 _State3)
    {
        string[] lastsArray = lasts.ToArray();
        for (int i = 0; i < lastsArray.Length; i++)
        {
            if (lastsArray[i] == _State3.ToString())
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 如果该文件名里面有这个参数的关键词就返回true
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public bool Get_fineCentreTag(string str)
    {
        if (centre == null)
        {

            return false;
        }

        for (int i = 0; i < centre.Count; i++)
        {

            if (centre[i] == str)
            {
                return true;
            }
        }
        return false;
    }




    public bool Is_SuperState()
    {
        // 获取Tag_Super_state枚举的所有值  
        Tag_Super_state[] enumValues = (Tag_Super_state[])Enum.GetValues(typeof(Tag_Super_state));

        // 检查start是否与枚举值匹配  
        for (int i = 0; i < enumValues.Length; i++)
        {
            Tag_Super_state v = enumValues[i];

            if (v.ToString() == start)
            {
                return true;
            }
        }

        return false;
    }

    AnimationEvent 返回(string s,float time)
    {
        AnimationEvent ev =new AnimationEvent();
        ev.functionName = "播放特效";
        ev.time = time;
        ev.stringParameter = s;
        return ev;
    
    }
    public void enter()
    {
        //进度 = 0;
        //if (name=="run_0_")
        //{
        //    clip.events = new AnimationEvent[2] { 返回("特效run_", 0.8f), 返回("特效run_", 0.4f) };
        //}
        //if (name=="idle_run_to0")
        //{
        //    clip.events = new AnimationEvent[1] { 返回("特效反向_", 0.15f) }; 
        //}
        if (Get_fineCentreTag("_activeFrame_"))
        {
            Anim_Action.I.ATK_Action?.Invoke(this);
        }
        if (Get_fineCentreTag("_action_"))
        {
            Anim_Action.I.空中开冲 ?.Invoke();

        }
        if (name == "skydash_jump_to0")
        {
            Anim_Action.I.Zero?.Invoke(true);
        }
    }
    public void exit()
    {
        进度 = 0;
        if (name == "skydash_jump_to0")
        {
            Anim_Action.I.Zero?.Invoke(false );
        }
        //if (end=="_toother")
        //{

        //    Player.I.角色翻转更新();
        //}
    }


}
