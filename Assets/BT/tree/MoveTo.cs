using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public enum 移动方式
{
    水平, 竖直, 面, 跳跃_水平,跳跃
}
namespace   Tree_
{ 
    public class MoveTo : Aweak
    { 
         public SharedFloat Speed = 0;
        public SharedFloat JumpSpeed = 0;
        public SharedVector2 tagert;

        public SharedFloat WaiteTime;

        bool Realy_time=true ; 
        float 被挡住了多久 { get; set; }
        public override void OnStart()
        {
            被挡住了多久 = 0; 
        } 
        public override void OnAwake()
        {
            base.OnAwake();  
        }
        public override TaskStatus OnUpdate()
        {
            if (tagert.Value ==Vector2 .zero)
            {
                return TaskStatus.Success;
            }
            //JumpCoolTim_ -= Time.deltaTime;
            if (!b.前空_)
            {
                被挡住了多久 += Time.deltaTime;

                float ttt = WaiteTime.Value;
                if (Realy_time )
                {
                    ttt = WaiteTime.Value*1/b.I_S .固定等级差 ;
                }
                if (被挡住了多久 >= ttt)
                {
              if (Debug_) Debug.LogError("CC111AAAAAAAAAAAAAAAAAAAAAAAA11111 1111C"+gameObject);
                    return TaskStatus.Failure;
                }
            }
            else
            {
                被挡住了多久 = 0;
            }
 
            Vector2 cha = (Vector2)transform.position - tagert.Value ;
            bool 超过 = false;
            if (b.LocalScaleX_Set>0)
            {
                if (cha.x>0&&Mathf .Abs(cha.x)<5)
                {//超过
                    超过 = true;
                }
            }
            else
            {
                if (cha.x < 0 && Mathf.Abs(cha.x) < 5)
                {//超过
                    超过 = true;
                }
            }

 
            //++  cha 为+  超过
            //--  cha为超过
            var t =  b.前面有坑(8f, 8, 0.2f,2f)  ;
            switch (b.当前移动方式 )
            {
                case 移动方式.水平:
                    if (!b.Ground )
                    {
                        return TaskStatus.Failure;
                    }
                    if (超过)
                        ///走过头
                        //if (Mathf.Abs(cha.x) <=10 *b .I_S .Current_Speed*Time.fixedDeltaTime)
                    {//接近
                        //b.LocalScaleX_Set = -cha.x;
                        if (b.Ground) b.Velocity = Vector2.zero;
                        //b.瞬移至(  new Vector2  )( tagert.Value .x,transform .position .y);
                        //if (Debug_) Debug.LogError("CAAAAAAAAAAAAAAAAAAA111111111111C");
                        return TaskStatus.Success;
                    }
                    else if (t == BiologyBase.地面情况.有坑)
                    {
                        if (Debug_) Debug.LogError("QQQQQQQQQQQQQQQQQQQQQQQQQC");
           
                        return TaskStatus.Success; //有坑就呆着知道玩家走掉
                    }
                    else if(b.前空_)
                    { //前进
                        //if (Debug_) Debug.LogError("QWWWWWWWWWWWWWWWWWWWWWW     "+ new Vector2(-(int)Mathf.Sign(cha.x) * Speed.Value, 0));
                        b.Velocity = new Vector2(-(int)Mathf.Sign(cha.x) * Speed.Value, 0);
                        return TaskStatus.Running; //有坑就呆着知道玩家走掉
                    } 
                    break;
                case 移动方式.跳跃_水平: 
                    if (  Mathf .Abs(cha.x) <= 1f)
                    {//接近
                        if (b.Ground) b.Velocity = Vector2.zero;
                        Debug.LogError("CCCCCCCCCCCCCCCCC");
                        return TaskStatus.Success;
                    }
                    else if (t == BiologyBase.地面情况.有坑)
                    {  
                        return TaskStatus.Running; //有坑就呆着知道玩家走掉
                    }
                    else
                    { 
                        if (b.Ground)
                        {

                            if (!b.前空_ /*&& qian *//*&& JumpCoolTim_<= 0*/)
                            {//地上就跳   有遮挡 
                             //JumpCoolTim_ = JumpCoolTime;
                             //Debug.LogError(    JumpCoolTim_);  
                                return TaskStatus.Failure;
                            }
                            else if (t == BiologyBase.地面情况.有坑 && b.前空_)
                            {
                                //前空，且有坑 
                                b.Velocity = new Vector2(b.LocalScaleX_Set * Speed.Value, JumpSpeed.Value);
                                return TaskStatus.Running;
                            }
                            else if (b.前空_)
                            {// 地上 前空       前进  
                                Debug.LogError("AAAAAAAAAAAAAA");
                                b.Velocity = new Vector2(-(int)Mathf.Sign(cha.x) * Speed.Value, 0);
                                return TaskStatus.Running;
                            }
                        } 
                    }
                    break;
            }

            return TaskStatus.Running;
        }
}
    public class Aweak : Action
    {
        //v.DisableBehavior(true); ///在当前节点  停止
        //    v.EnableBehavior(); ///恢复
        protected Treevalue value;
        protected BehaviorTree tree;
        protected Enemy_base  b;

        protected bool Debug_ { get => b.Debug_; }
        public override void OnAwake()
        {
            value = GetComponent<Treevalue>();
            b = GetComponent<Enemy_base>();
            tree = GetComponent<BehaviorTree>();
        }
    }
    }
 