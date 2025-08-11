using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Te_animcoter : AnimBase
{

    private void Start()
    {
        if (Anim_Action.I==null)
        {
            Debug.LogWarning(Anim_Action.I);
        }
        else
        {
            Anim_Action.I.ATK_Action += ASASD;
        }

    }

  protected override void Update()
    {
        检测当前播放动画进度(0.8f);
    }
    public override void 播放结束()
    {
        animator.Play("New State");
    }

    public  void  ASASD (Anim2 anim2)
    {
        animator.Play(anim2.start + "_" + TAG.十位数(anim2.playerOrder) + "_");
    }
}
