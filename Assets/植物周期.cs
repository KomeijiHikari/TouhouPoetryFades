using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 植物周期 : MonoBehaviour,I_Speed_Change
{
    
    enum 阶段
        {
        归零, 生长,日常,回去,
    }
    [SerializeField]
    Vector3 HoursTime;
    [SerializeField]
    float Ft;

    [SerializeField ]
     植物数据 a;
    [System.Serializable]
    class 植物数据
    {
        public static float GetLerp(float Start, float End, float F)
        {
            // 处理边界情况：如果Start等于End，返回0或1取决于F的位置
            if (Mathf.Approximately(Start, End))
            {
                return F >= Start ? 1f : 0f;
            }

            // 计算F在Start和End之间的比例位置
            float t = (F - Start) / (End - Start);

            // 限制在0-1范围内
            return Mathf.Clamp01(t);
        }
        [SerializeField] Vector3 Enter; 
        [SerializeField] Vector3 Exit;
        [SerializeField] Vector3 ExiteEmter;
        [SerializeField] Vector3 ExiteExite;
     public   阶段 j;

       public  float EnterF;
        public float ExitF;
        public float ExiteEmterF;
        public float  ExiteExiteF;

    public     float Lerp;
  public      void Initialize ()
        {
            EnterF = Enter.TimeVector3ToFixTime();
            ExitF= Exit.TimeVector3ToFixTime();
            ExiteEmterF=ExiteEmter.TimeVector3ToFixTime();
            ExiteExiteF=ExiteExite.TimeVector3ToFixTime ();
        }
 
     public   阶段 asd(float fix)
        {
            bool InRange(float start, float end, float value)
            {
                if (Mathf.Approximately(start, end))
                {
                    // degenerate range -> treat as empty
                    return false;
                }
                if (start < end)
                {
                    return value >= start && value < end;
                }
                else
                {
                    // wrap around (e.g., start 22 -> end 2)
                    return value >= start || value < end;
                }
            }

            // 生长: EnterF -> ExitF
            if (InRange(EnterF, ExitF, fix))
            {
                return 阶段.生长;
            }

            // 日常: ExitF -> ExiteEmterF
            if (InRange(ExitF, ExiteEmterF, fix))
            {
                return 阶段.日常;
            }

            // 回去: ExiteEmterF -> ExiteExiteF
            if (InRange(ExiteEmterF, ExiteExiteF, fix))
            {
                return 阶段.回去;
            }

            // 如果都不在上述范围，返回归零
            return 阶段.归零;
        }
    }
    Vector2 StartWay;

    [DisplayOnly]
    [SerializeField]
    private float speed_Lv;

    public GameObject 对象 =>gameObject;

    public Action 变速触发 { get  ; set ; }

    public I_Speed_Change I_S =>this;

    public float Current_Speed_LV =>Speed_Lv;

    public float Speed_Lv { get => speed_Lv; set => speed_Lv = value; }

    private void Start()
    {
        a.Initialize();

        StartWay = transform.localPosition;
        transform.localPosition = Vector2.zero;
  

    }

    private void FixedUpdate()
    {
        HoursTime = 我的光照2.I.HourseVector;
        Ft = Time_Tool.TimeVector3ToFixTime(HoursTime);
    
        a.j = a.asd(Ft);
        switch (a.j)
        {
            case 阶段.归零:
                break;
            case 阶段.生长:
                a.Lerp= 植物数据.GetLerp(a.EnterF,a.ExitF, Ft);
                break;
            case 阶段.日常:
                //a.Lerp = 植物数据.GetLerp(a.ExitF , a.ExiteEmterF, Ft);
                a.Lerp = 1;
                break;
            case 阶段.回去:
                a.Lerp =1- 植物数据.GetLerp(a.ExiteEmterF, a.ExiteExiteF, Ft);
                break;
            default:
                a.Lerp = 0;
                break;
        }
        var tt= Vector2.Lerp(Vector2.zero, StartWay, a.Lerp);
        var chazz = tt - (Vector2)transform.localPosition;
        transform.localPosition = Vector2.Lerp(Vector2.zero, StartWay, a.Lerp);

        ///帧长度=    chazz.sqrMagnitude;
        if (a.j == 阶段.生长 || a.j == 阶段.回去)
        {
            speed_Lv = chazz.sqrMagnitude / Time.fixedDeltaTime  ;
        }
        else
        {
            speed_Lv = 0;
        }
    }
}
