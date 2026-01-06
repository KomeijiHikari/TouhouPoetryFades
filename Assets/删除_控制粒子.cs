using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class 删除_控制粒子 : MonoBehaviour
{
    public ParticleSystem pa;
    private ParticleSystem.Particle[] pas;
    private void Start()
    {
        pa = GetComponent<ParticleSystem>();

        pas = new ParticleSystem.Particle[pa.main.maxParticles];  //实例化，个数为粒子系统设置的最大粒子数.

        //new SpriteShapeMetaData
 
    }
    private void Update()
    {
        int 数量 = pa.GetParticles(pas);

        //int En = pa.GetTriggerParticles(ParticleSystemTriggerEventType.Enter,enter);
        //int Ex = pa.GetTrig
        //gerParticles(ParticleSystemTriggerEventType.Enter, exite);
        //设置粒子移动.
 
            for (int i = 0; i < 数量; i++)
            {
                float a = pas[i].startLifetime / pas[i].remainingLifetime + 1;
                pas[i].position = Target.position;
            
            }
 
        //飞向的target.position.DraClirl();
        //重新赋值粒子.
        pa.SetParticles(pas, 数量);
    }
    public Transform Target;

}
