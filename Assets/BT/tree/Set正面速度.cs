using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Tree_;
public class Set正面速度 : Aweak
{
    public bool 反向;
    public bool Safe;
    public bool Add;
    int 朝向
    {
        get
        {
            if (反向)
            {
                return    -b.LocalScaleX_Int;
            }
            return b.LocalScaleX_Int;
        }
    }
 
    public SharedVector2 Speed  ;
    public override void OnStart()
    {
        var a = Vector2.zero;
        if (Add)
        {
            a = new Vector2(朝向 *(Speed.Value.x + Mathf .Abs(b.Velocity.x) ) , Speed.Value.y + b.Velocity.y); 
        }
        else
        {
            a = new Vector2(朝向 * Speed.Value.x, Speed.Value.y);
        }
        if (Safe)
        {
            a = b .p.碰撞预测(a);
        }
        b.Velocity = a;
    }
}