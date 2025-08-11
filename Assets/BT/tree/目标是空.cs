using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Tree_;

[TaskDescription("墙体检测")] 

public class 目标是空 : Aweak
{
    public SharedVector2 tagert;
    public bool  动态翻转;
    public void asdasd()
    {
        if (tagert != null && tagert.Value != null)
        {
            //目标在身后
            //if (b.Debug_)
            //{ 
            //Debug.LogError("    tagert.Value   " + tagert.Value+ "  transform.position.x      " + transform.position.x +  "    ::: " +(tagert.Value.x - transform.position.x).Sign()+"         "+ b.LocalScaleX_Set);
            //    tagert.Value.DraClirl(2,Color .green ,0.5f);
            //}
            目标在身后 = (tagert.Value.x - transform.position.x).Sign() == b.LocalScaleX_Set;
            if (动态翻转)
            {
                if (tagert != null && tagert.Value != null) 反转 = (tagert.Value.x - transform.position.x).Sign() == b.LocalScaleX_Set;
            }
        }
        //if ( 动态翻转)
        //{
        //    if (tagert != null && tagert.Value != null)  反转 = (tagert.Value.x - transform.position.x).Sign() == b.LocalScaleX_Set; 
        //} 
    }
    [SerializeField]
    [DisplayOnly]
    bool 目标在身后;

    enum E_检测类型
    {
        地面,
        前,后,头,
    }
    [SerializeField ]
    E_检测类型 检测类型 ;


    public bool 不空则返回失败=false;
    public bool 反转 = false;
    float time;
    public override void OnStart()
    {
        time = 0;
    }
    TaskStatus 哼哼哼(bool b)
    {
      
        if (反转)
        {
            b = !b;
        }
        if (b)
        {
    return TaskStatus.Success;
        }
        else
        {
            if (不空则返回失败)
            {
                return TaskStatus.Failure;
            }
            else
            {
                return TaskStatus.Running;
            }
        } 
    }
    public override TaskStatus OnUpdate()
    {
        asdasd();
        time += Time.deltaTime;
        if (time < 0.1f)
        {
            return TaskStatus.Running;
        }
        else
        {
            switch (检测类型)
            {
                case E_检测类型.地面:
                    return 哼哼哼(!b.Ground);
                case E_检测类型.前:
                    return 哼哼哼( b.前空_); 
                case E_检测类型.后:
                    return 哼哼哼( b.后空_); 
                case E_检测类型.头:
                    return 哼哼哼( b.头空_); 
                default:
                    Debug.LogError("离谱，一个都没有选上");
                    return TaskStatus.Running;
 
            } 
        }
    }
}