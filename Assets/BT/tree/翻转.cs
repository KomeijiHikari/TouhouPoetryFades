using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Tree_;
public class 翻转 : Aweak
{
	public int 设置=0;
    public SharedVector2 tagert;
    public override void OnStart()
    {
        Vector2 cha = (Vector2)transform.position - tagert.Value;
        if (tagert.Value !=Vector2 .zero)
        { 
            b.LocalScaleX_Set = -cha.x;
        }
        else
        { 
            if (设置 == 0)
            { 
                b.Flip();
            }
            else if (设置 == -1)
            {
                b.LocalScaleX_Set = -1;
            }
            else if (设置 == 1)
            {
                b.LocalScaleX_Set = 1;
            }
        }

    }
 
}