using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


namespace Tree_
{ 
[TaskDescription("计时用                   V 1 为本， v1  比   v2大成功    否则 继续       增值是持续增加    ")]

    public class 数值增加 : Action
{
    public SharedFloat F_v1 = 0; 
    public SharedFloat F_v2 = 0;
    enum E_运行类型
    {
        大小,
        增值,
        赋值,
    }

    [SerializeField ]
  E_运行类型 m_运行类型;
    public override TaskStatus OnUpdate()
    {
        switch (m_运行类型)
        {
            case E_运行类型.大小:
                if (F_v1.Value>= F_v2.Value)
                {
                    return TaskStatus.Success;
                }
                else
                {
                    return TaskStatus.Running ;
                    }
                case E_运行类型.增值:
                    return TaskStatus.Running ;
                case E_运行类型.赋值:
                    F_v1.Value = F_v2.Value;
                    return TaskStatus.Success;
                default:
                    Debug.LogError("离谱");
                    return base.OnUpdate();
            }

    }
 
        public override void OnFixedUpdate()
    {
            switch (m_运行类型)
            { 
                case E_运行类型.增值:
                    F_v1.Value += Time.fixedDeltaTime; 
                    break; 
            }
        }
}
}