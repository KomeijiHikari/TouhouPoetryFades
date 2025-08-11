using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Events;

//[ExecuteInEditMode]   编辑情况下也运行
public class 控制粒子 : MonoBehaviour
{
    public UnityEvent  Enter;
    public Vector2 MoveToPosition_(Vector2 My, Vector2 targetPosition, float 移动距离)
    {
        Vector2 目标差 = targetPosition - My;
        // 计算目标位置与当前位置之间的方向向量  
        if (目标差.sqrMagnitude <= 移动距离*移动距离)  return 目标差;
       
        else  return 目标差.normalized * 移动距离;
         
    }
    public Vector2 MoveToPosition(Vector2  My,Vector2 targetPosition, float distance)
    { 
        // 计算目标位置与当前位置之间的方向向量  
        Vector2 direction = (targetPosition - My).normalized; 
        // 根据方向和距离计算新的位置  
        return My + direction * distance;

    }
    public Collider2D co; 
    [SerializeField ]
public ParticleSystem pa;
    private ParticleSystem.Particle[] pas;
        private List<ParticleSystem.Particle>    enter=new List<ParticleSystem.Particle>() ;
    private List<ParticleSystem.Particle> exite = new List<ParticleSystem.Particle>();
    public Transform 飞向的target;  //目标位置.(手动拖拽)

    [SerializeField ]
    bool 开关;
    private void Awake()
    {
        pas = new ParticleSystem.Particle[pa.main.maxParticles];  //实例化，个数为粒子系统设置的最大粒子数.
    }
    public  void  初始化()
    {
        pas = new ParticleSystem.Particle[pa.main.maxParticles];  //实例化，个数为粒子系统设置的最大粒子数.
        var a = pa.trigger;
        if (co!=null)
        {
               a.AddCollider(co);
        } 
    }
    public float speed;
    private void OnParticleTrigger()
    {
        int En = pa.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        //int Ex = pa.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, exite);

        for (int i = 0; i < En; i++)
        {
            var a = enter[i];
            a.startColor = new Color32(250, 0, 0, 255);
            a.remainingLifetime = 0f;
            enter[i] = a;

            Debug.LogError("AAAAAAAAAAAAAAAAAAAAAAAA                 打到目标() 打到目标() 打到目标() 打到目标() 打到目标()");
            Enter?.Invoke();
        }
        pa.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    }
    [SerializeField ][DisplayOnly]
    float time;

    void Update()
    {
        if (开关)
        {
            time = 0; 
             开关 = false;
        }
        time += Time.deltaTime;
        //获取当前激活的粒子.
        int 数量 = pa.GetParticles(pas);

        //int En = pa.GetTriggerParticles(ParticleSystemTriggerEventType.Enter,enter);
        //int Ex = pa.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, exite);
        //设置粒子移动.
        if (time>1)
        {
            for (int i = 0; i < 数量; i++)
            {
                float a = pas[i].startLifetime / pas[i].remainingLifetime + 1;
                pas[i].position += (Vector3)MoveToPosition_(pas[i].position, 飞向的target.position, speed * a * Time.fixedDeltaTime);

                //pas[i].position = MoveToPosition(pas[i].position, 飞向的target.position, speed * a * Time.fixedDeltaTime); 
            }
        } 

        //重新赋值粒子.
        pa.SetParticles(pas, 数量);
    }
} 
