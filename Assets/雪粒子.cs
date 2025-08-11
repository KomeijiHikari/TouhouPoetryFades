using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class 雪粒子 : MonoBehaviour
{
    EmissionModule emission;
       MainModule main;
    [SerializeField] 
    float  SpeedLV_=1;
    [SerializeField]
    [DisableOnPlay]
    float 粒子Speed_;
    public float 粒子Speed { get; set; } = 1;
    ParticleSystem lizi;
    [SerializeField ]
    [DisableOnPlay]
    float Y;
 
    float 最初数据;
    bool 发射 { get { return emission.enabled; }
        set { emission.enabled = value; }
    }

    [SerializeField] float 下落数量;
    [SerializeField] float 上升数量;
   float 移动发射粒子
    {
        get { return emission.rateOverDistanceMultiplier;}
        set { emission.rateOverDistanceMultiplier = value;} 
    }
    float 秒发射粒子
    {
        get { return emission.rateOverTimeMultiplier; }
        set { emission.rateOverTimeMultiplier = value; }
    }
    private void Awake()
    {
        lizi = GetComponent<ParticleSystem>(); 
        if (lizi == null)
        {
            Debug.LogError("没有粒子组件");
        }
          main  = lizi.main;
        emission = lizi.emission; 
    }
    private void Start()
    {
        if (粒子Speed == 0)
        {
            粒子Speed = 1;
        }
        var e = lizi.emission;
        e.enabled = false; 
        Initialize_Mono.I.Waite(() => e.enabled = true);
        最初数据 = 秒发射粒子;
 

    }

    private void Update()
    {
        //if (transform.parent!= Player3.I.transform)
        //{
        //    if (Player3.I.Ground)
        //    {
        //        transform.position = new Vector2(Player3.I.transform.position .x, Player3.I.transform.position.y+30);
        //        transform.SetParent(Player3.I.transform);
        //    }
        //}     

        粒子Speed = 1*SpeedLV_ / Player3.Public_Const_Speed;
           粒子Speed_ = 粒子Speed;
        if (transform.position.y< Y)//下降
        {
            移动发射粒子 = 0;
            秒发射粒子 = 下落数量;
            //发射 = false;
        }
        else  if (transform.position.y   > Y)//上升
        {
            发射 = true;
            移动发射粒子 = 上升数量;
            秒发射粒子 = 最初数据;
        }
        else
        {
            //if (!发射) Initialize_Mono .I.Waite(() => 发射=true,  0.02f );
            发射 = true;
            秒发射粒子 = 最初数据;
            移动发射粒子 = 0;
        }
        Y = transform.position.y;
        main.simulationSpeed = 粒子Speed;  
    }
}
