using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;


//密度，下落速度,  尺寸     偏移速度   
public class 雪粒子_2 : MonoBehaviour
{
    ParticleSystem p;
    [Range(-1,1.5F)]
    [SerializeField ]
    float Speed=1;
    MainModule main;
    EmissionModule emi;
    ShapeModule sha; 
    Bounds B;
[SerializeField ]
    float 缩放;

    [SerializeField ]
    Transform A_,B_;
    public Vector2 Flow
    {
        get
        {
            return Camera.main.transform.position;
        }
    }
    float Anim_Speed
    {
        get
        {
            return main.simulationSpeed;
        }
        set
        {
            main.simulationSpeed = value;
        }
    }
    Vector2 LastV;
 Vector2    My_V
    {
        get
        {
            return transform.position;
        }
        set
        {
            transform.position = value;
        }
    }
    Vector2 cha;
    int i;
    Vector2 Last;

    private void Awake()
    { 
           p = GetComponent<ParticleSystem >();
        main = p.main;
        emi = p.emission;
        sha = p.shape;
    }
    MinMaxCurve SSSS (MinMaxCurve curve,float size)
    { 
        curve.constant *= size;
        curve.constantMax *= size;
        curve.constantMin *= size;
        return curve;
    }
    private void Start()
    {
 
        main.startSize= SSSS(main.startSize,缩放);

        main.startSpeed = SSSS(main.startSpeed, 缩放);

   var a=2 - 缩放;
        emi.rateOverTime = SSSS(emi.rateOverTime, a); 
    }

    private void Update()
    {
        i++;
 
        if (i >100)
        {
            if (Flow == My_V) return;

            cha = Flow - Last;
            My_V += cha * Speed;

       
        } 
        else
        {
            My_V = Flow;
        }
        Last = Flow;

        B = new Bounds(transform .position + par_PO, sha.scale);
        if (i > 100)
        { 
            par_PO -= (Vector3)cha * Speed;
            par_PO += (Vector3)cha;
        }
        if (A_!=null&& B_ != null)
        {
            A_.transform.position = B.min;
            B_.transform.position = B.max;
        }

        Anim_Speed = 1 / Player3.Public_Const_Speed;

        cha = Vector2.zero ;
    }
    Vector3  par_PO
    {
        get
        {
            return new Vector3(sha.position.x, -sha.position.z); ;
        }set
        {
            sha.position = new Vector3(value.x, 0, -value.y);
        }
    }


 
}
