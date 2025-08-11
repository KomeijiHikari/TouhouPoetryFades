using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class 特效_anim
{
    [DisplayOnly ]
    [SerializeField]
    public AnimationClip clip;
 public   float time;
    [DisplayOnly]
    [SerializeField]
    public string name;
    [DisplayOnly]
    [SerializeField]
    public string 播放name;
    [DisplayOnly]
    [SerializeField]
    public  Vector2 位置;

    public bool 是在目标的前面嘛;
    public bool 是跟随目标;
    [DisplayOnly]
    [SerializeField]
   public string 假的name;
    public   特效_anim(AnimationClip clip_)
    {
        clip = clip_;
        time = clip.length;
        name = clip.name;
        string[] a = name.Split("_");
        假的name = a[0] + "_";
        string[] b = a[1].Split(";");
        是在目标的前面嘛 = b[0] == "1";
        是跟随目标 = b[1] == "1";
        位置 = ATK_Clip.字符串转v2(b[2]);

        播放name = name.Replace(".", "_");
    }
}
public class 特效 :MonoBehaviour
{
    Animator an;
    SpriteRenderer sp;

    AnimationClip[] 所有的clips;

    public List<特效_anim> 特效_Anim列表 = new List<特效_anim>();
    // Update is called once per frame

    Action 当前动画为百分之99 { get; set; }
 
    private void OnEnable()
    {
transform.DetachChildren();
    }

    public void   开播(GameObject   t,string  s)
    {
        if (t == null) return;
        Transform tt = t.transform;
        Vector2 中心点 = t.GetComponent<Collider2D>().bounds.center;


        特效_anim a = 返回ANIM(s);
        if (a == null) return;
        an.Play(a.播放name  );

        SpriteRenderer ts = t.GetComponent<SpriteRenderer>();
        if (ts == null) return;
        sp.sortingLayerID = ts.sortingLayerID;
        if (a.是在目标的前面嘛)
        {


            sp.sortingOrder = ts.sortingOrder+1;
        }
        else
        {
            sp.sortingOrder = ts.sortingOrder-1;
        }
        transform.position = 中心点+ new  Vector2 ( tt.localScale .x* a.位置.x, a.位置.y);
      transform.SetParent(tt);


        //transform.localPosition = Vector3.zero;
        //transform.localPosition = a.位置;

        if (!a.是跟随目标)
        {
       var f=    transform .parent.parent;
            if (f!=null)
            {
                transform.SetParent(f);
            }
            else
            {
                transform.SetParent(null);
            }

            transform.localScale = tt.localScale;
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    public void 初始化(RuntimeAnimatorController controller)
    {
 
        Initialize.组件(gameObject, ref sp); 
        Initialize.组件(gameObject ,ref an );
        an.runtimeAnimatorController = controller;  
       所有的clips = an.runtimeAnimatorController.animationClips; 
        foreach (var item in 所有的clips)
        {
            特效_Anim列表.Add(new 特效_anim(item));
        } 
        当前动画为百分之99 += 动画结束;
    }
    public  特效_anim     返回ANIM(String s)
    {
        foreach (var item in 特效_Anim列表)
        {
            if (item.假的name==s)
            {
                return item;
            }
        }
        Debug.LogError("返回为空");
        return null;
    }
    private void  动画结束()
    {

        特效_pool.I.ReturnPool(gameObject);

    }


    int J;
    protected void 检测当前播放动画进度(float f)
    {
        //if (当前 == null) return;
        //当前.进度 = an.GetCurrentAnimatorStateInfo(0).normalizedTime * 当前.time;
        if (an.GetCurrentAnimatorStateInfo(0).normalizedTime >= f)
        {
            J++;
        }
        else
        {
            J = 0;
        }
        if (J == 1)
        {

            当前动画为百分之99?.Invoke();
        }


    }

    void Update()
    {
        检测当前播放动画进度(0.9f);
    }
}
