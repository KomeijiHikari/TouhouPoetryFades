using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class 玩家受伤效果 : MonoBehaviour
{

    [Header("受击效果")]
    [Space]
    [Tooltip("无敌时间 ")]
    [SerializeField]
    float godTime = 4.63f;
    [Tooltip("闪烁间隔时间")]
    public float flickerHZ = 0.15f;
    [Tooltip("硬直时间")]
    public float stiffnessTime一般 = 0.2f;
    [Tooltip("大硬直时间")]
    public float stiffnessTime大硬直 = 1f;
    [DisplayOnly]
    CinemachineImpulseSource CIS;
    Light2D LI2D { get; set; }


    public Action<bool> 播放受击动画 { get; set; }

    bool 不可以受伤 { get; set; }
    public float stiffnessTime_;
    float godTime_ { get; set; }
    WaitForSeconds waitForSeconds { get; set; }//程序等待时间



    public void EnterHit(float damage, float force, GameObject obj, bool 硬抗 = false)
    {
        if (Player3.I.HPROCK) return;

        var c = Initialize.返回和对方相反方向的标准力(gameObject, obj);


        EnterHit(damage, 硬抗);

        //if (obj != gameObject) 
        //    Debug.LogError(force+"              AAAAAAAAAAAAAAAAAAA"+Time.frameCount);

        if (Player3.I.受伤Force == Vector2.zero)
        {
            Player3.I.Velocity = new Vector2(c.x * force, 10f);
        }
        else
        {
            Player3.I.Velocity = new Vector2(c.x * Player3.I.受伤Force.x, Player3.I.受伤Force.y);
        }


    }

    [DisplayOnly]
    public bool 受伤效果;
    public void EnterHit(float damage, bool 硬抗 = false)
    {
        if (Player3.I.HPROCK) return;

        if (不可以受伤) return;
        Player3.I.闪光();

        if (硬抗)
        {
            主UI.I.播放红动画();
            镜头晃动(10);
            Initialize.TimeScale = 0.1f;
            Initialize_Mono.I.Waite(() => Initialize.TimeScale = 1f, 0.5f, true);

            return;
        }
        yalaAudil.I.EffectsPlay("PlayerHit",1); 

        受伤效果 = true;
        godTime_ = godTime;
        stiffnessTime_ = stiffnessTime一般;
        不可以受伤 = true;

        主UI.I.播放红动画();
        镜头晃动(10);
        Initialize.TimeScale = 0.1f;
        Initialize_Mono.I.Waite(() => Initialize.TimeScale = 1f, 0.5f, true);
        //Initialize_Mono.I.时缓(0.5f, 0.2f);

        Player_input.I.输入开关 = false;
    }
    private void Update()
    {

        GodTime_update();
        if (受伤效果)
        {
            stiffnessTime_ -= Time.deltaTime;
            Player3.I.an.speed = 0;
            if (stiffnessTime_ <= 0)//硬直时间过去了
            {
                StartCoroutine(Flicker());
                受伤效果 = false;
                Player3.I.an.speed = 1;
                播放受击动画?.Invoke(false);
                Player_input.I.输入开关 = true;

            }
        }

    }

    void GodTime_update()
    {

        if (!不可以受伤) return;

        //Debug.LogError(godTime_  +"godTime_ >= 0");
        if (godTime_ > 0)
        {//无敌时间继续
            godTime_ -= Time.deltaTime;
            Player3.I.HPROCK = true;

        }
        else
        {//无敌时间结束 
            不可以受伤 = false;
            Player3.I.HPROCK = false;
        }
    }

    IEnumerator Flicker()
    {
        for (int i = 0; ; i++)
        {
            //LI2D .enabled = true; 
            Player3.I.sp.material.SetFloat(材质管理._Alpha, 0.1f);
            //Player3.I.sp.color = new Color(Player3.I.sp.color.r, Player3.I.sp.color.g, Player3.I.sp.color.b, );
            yield return waitForSeconds;
            Player3.I.sp.material.SetFloat(材质管理._Alpha,  1f);
            //Player3.I.sp.color = new Color(Player3.I.sp.color.r, Player3.I.sp.color.g, Player3.I.sp.color.b, 1f);
            //LI2D.enabled = false ;
            yield return waitForSeconds;

            if (godTime_ <= 0) break;
        }
        Player3.I.sp.color = new Color(Player3.I.sp.color.r, Player3.I.sp.color.g, Player3.I.sp.color.b, 1f);
    }

    Light2D 在自己的底部生成灯光()
    {
        GameObject Light2Dl;

        Light2Dl = new GameObject("Light");
        Light2Dl.transform.parent = transform;
        Light2Dl.transform.position = Vector2.zero;

        return Light2Dl.AddComponent<Light2D>();
    }
    private void Awake()
    {
        LI2D = GetComponentInChildren<Light2D>();


        Initialize.组件(gameObject, ref CIS);


        waitForSeconds = new WaitForSeconds(flickerHZ);
    }

    public void 镜头晃动(float 力度)
    {
        GetComponent<CinemachineImpulseSource>().GenerateImpulse(transform.localScale.x * 力度);
    }
}

