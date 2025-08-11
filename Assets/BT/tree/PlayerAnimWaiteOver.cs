using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


namespace  Tree_
{
    [TaskDescription(" 播放一个动画并且等待播放完毕")]

    public class PlayerAnimWaiteOver :Aweak
    {
        public  bool  等待播放完毕=false;
        public SharedString name;
        string 当前名字
        {
            get
            {
                if (b.an.GetCurrentAnimatorClipInfo(0).Length<1)
                {
                    Debug.LogError("卧槽"+gameObject .name+transform .position );
                    return name.Value;
                }
                return b.an.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            }
        }
 float  当前进度
        {
            get
            {
                return b.an.GetCurrentAnimatorStateInfo(0).normalizedTime;
            }
        }
        public override void OnStart()
        {
            b.an.Play( name.Value);
        }
        public override TaskStatus OnUpdate()
        {
 
            if (当前名字 != name.Value)
            {
                b.an.Play(name.Value);
                return TaskStatus.Running;
            }
            else  //等于了
            {
                if (!等待播放完毕)
                {
                    return TaskStatus.Success;
                }
                else if(当前进度 > 0.9f)
                { 
                    return TaskStatus.Success;
                }
                else
                { 
                    return TaskStatus.Running;
                }
            } 
        } 
    }
}


