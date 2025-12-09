using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class 发射下落物
{
    public float 投射物发射速度;
    SpriteRenderer sp;

    [Range (0,1)]
    [SerializeField ]
    float 开始间隔=0;
    [SerializeField ]
    float 间隔=1;
    float Ti;
    float 释放的时间点;
    监控激活碰撞框 监控;
 
    //[SerializeField]
    List<Fly_Ground> Fs= new List<Fly_Ground>();

    float StartT;
 
    public bool 预播放b;

    public SpriteRenderer 无视盒子SP;
    private void Start()
    {

        sp = GetComponent<SpriteRenderer>();


        gameObject.组件(ref 监控);

        F = Surp_Pool.I.GetValue(Surp_Pool.下坠刺).GetComponent<Fly_Ground>();
        监控.是我 += (b) => { 
            if (b)
            { 
                预播放(); 
            }
            else
            {
                Ti = 0.0001f;
                释放的时间点 = StartT;
                while (Fs.Count != 0)
                {
                    //因为不明原因 一次遍历清理不干净
                    for (int i = 0; i < Fs.Count; i++)
                    {
                        var a = Fs[i];
                        a.销毁触发?.Invoke();
                        Surp_Pool.I.ReturnPool(a.gameObject);
                    } 
                }  
            } 
        };
        //StartT = -(1 / (间隔1 * speed_Lv));
        //释放的时间点 = StartT;

    }
    Bounds B=default;

    int LastC; 
    public bool De=false ;

    public float 间隔1 { get => 间隔  ;    set => 间隔 = value; }

    public float 开始间隔1 { get => 开始间隔 ; set => 开始间隔 = value; }
     
    Fly_Ground F;

    public Transform 预播放距离ts;
    void 预播放()
    {
        if (!预播放b) return;

        //var 初始差 = (开始间隔1 * (间隔1/speed_Lv) * 速度);
        float 最终长度 = 4;
        if (预播放距离ts!=null)
        {
            最终长度 = transform.position.y - 预播放距离ts.position.y;
        }
        else
        {
            var p = Physics2D.Raycast(
transform.position, Vector2.down, 100, 1 << Initialize.L_Ground | 1 << Initialize.L_M_Ground
).point;
            最终长度 = transform.position.y - p.y;
            if (最终长度 == 0)
            {
                最终长度 = 20;
                //最终长度= Vector2.Distance(transform.position, p) - sp.bounds.min.y;
            }
        }




        //   if (De) Debug.LogError(最终长度+"             AAAAAAAAAAAAAAAAAAAAA");
        最终长度 = (最终长度 / F.self_speed) ;
      var  步数 =(int) 最终长度;             

        最终长度 = (步数 * F.self_speed) ;//   11秒   速度为4  长度为  44
        var 步距 = F.self_speed * 间隔1 ;//速度4 间隔2    步距  8
         

        int  中间数量 = (int )(最终长度 / 步距);
        for (int i = 0; i < 中间数量+1; i++)
        {
            var 点 = ( (i * 步距)  ) * F.方向 ;
            点 = 点 + (Vector2 )transform.position;
         play().position = 点; 
        if(De)    点.DraClirl(2, Color.red, 10);
        }
    }
 
 Transform play()
    {
        Fly_Ground f= Surp_Pool.I.GetPool(Surp_Pool.下坠刺).GetComponent<Fly_Ground>();

        if (无视盒子SP != null)
        { 
            f.无视盒子 = 无视盒子SP.bounds; 
        }

        f.Speed_Lv  = speed_Lv;
        if (投射物发射速度 != 0) f.Speed_Lv = 投射物发射速度;
        Fs.Add(f);


        Action handler = null;

   handler = () => {
       Fs.Remove(f); 
       f.销毁触发 -= handler; 
   };

f.销毁触发 += handler;
        return f.transform;
    }
    [SerializeField]
    float 帧;
    private void Update()
    {
        for (int i = 0; i < Fs.Count; i++)
        {
            var a= Fs[i];
            a.Speed_Lv = speed_Lv;
        }
    }
    private void FixedUpdate()
    {
        if (暂停) return;
        帧 = I_S.固定等级差;
        //帧 = Initialize_Mono.I.GetMin(帧);
        Ti += Time.fixedDeltaTime* 帧/F.self_speed;
        if (LastC!=Time.frameCount)
        {
            LastC = Time.frameCount;

        if (Ti>间隔1  + 释放的时间点 )
        {
            释放的时间点 = Ti;
                play().position = transform.position; 

            }
        }
    }

 
}

public partial class 发射下落物 : MonoBehaviour, I_Dead, I_Revive, I_Speed_Change,I_暂停
{
    public GameObject 对象 { get => gameObject; }
    [SerializeField] private float speed_Lv=1;
    [SerializeField] private bool re;
    [SerializeField] private float re_Time;
    [SerializeField]
    [DisplayOnly]
    private bool 暂停1;

    public bool Re { get => re; set => re = value; }
    public float Re_Time { get => re_Time; set => re_Time = value; }
    public Action 销毁触发 { get; set; }
    public Action 变速触发 { get; set; }

    public Bounds 盒子 => sp.bounds;


    public I_Speed_Change I_S => this;

    public float Current_Speed_LV => Speed_Lv;

    public float Speed_Lv { get => speed_Lv; set => speed_Lv = value; }
    public bool 暂停 { get => 暂停1; set => 暂停1 = value; }

    public bool Dead()
    {
        return true;
    }

    public bool 重制()
    {  
        return true;
    }
}
