using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class 空中dash触发 : 感应
{
    float coolMax=1;
    float cool;
    [SerializeField]
    UnityEvent 被触发的事件;
    public bool dash触发了;

    public static float EnterTime_Max = 3;
    public  float EnterTime = 3; 
   

    public float 进度
    {
        get { return   1- (EnterTime / EnterTime_Max); }
    }

    [SerializeField]
    SpriteRenderer sr;
    Color stc;
    private void Start()
    {
        if (sr != null)
        {
            stc = sr.color;
        }
        Player3.I.Dash传送触发 += asd;
    }
    void asd(int i)
    {
        if (i == gameObject.GetInstanceID())
        {
            Debug.LogError(" 是我"+gameObject.name);
            cool = coolMax;
        }
    }
    private void FixedUpdate()
    {

        if (玩家进入了检测范围)
        { 
            EnterTime -= Time.fixedDeltaTime;
            if (EnterTime <= 0)
            {
                被触发的事件?.Invoke();
                EnterTime = EnterTime_Max;
            }
        }
        else
        {
            EnterTime = EnterTime_Max;
        }

        if (sr!=null)
        {
            sr.color = Color.Lerp(stc,Color.black, EnterTime/ EnterTime_Max);
        }
    }
    public float jjj;
    protected override void Update()
    {

        base.Update(); 
        if (sp != null)
        {
            if (玩家进入了检测范围)
            {
                sp.color = Color.black;
            }
            else
            {
                sp.color = StartC;
            }
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1 - jjj);

        }
        jjj = 进度;
        cool -= Time.deltaTime;
        if (cool < 0)
        {
            dash触发了 = 玩家进入了检测范围 && Player3.I.State == E_State.skydash;
            if (dash触发了)
            {
                被触发的事件?.Invoke();
            }
        } 
    }
}
