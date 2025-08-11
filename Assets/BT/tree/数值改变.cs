using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


namespace Tree_
{

[TaskDescription(" V 1 为本，对v1用v2程度的变换")]
public class 数值改变 : Conditional
{
    public SharedInt v1 = 0; 
    public SharedInt v2 = 0;
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
                if (v1.Value>= v2.Value)
                {
                    return TaskStatus.Success;
                }
                else
                {
                    return TaskStatus.Failure;
                } 
            case E_运行类型.增值:
                v1.Value += v2.Value;
                return TaskStatus.Success;
            case E_运行类型.赋值:
                v1.Value  = v2.Value;
                return TaskStatus.Success;
            default:
                Debug.LogError("离谱");
                return base.OnUpdate(); 
        }

    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }
}
}