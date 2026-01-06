using BehaviorDesigner.Runtime.Tasks.Unity.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using 发射器空间;

public class SpeedMager : MonoBehaviour
{
    // ========== 静态属性和字段 ==========
    public static string Public_Const_Speed_Name => "Public_Speed";
    public static string Public_Const辅助_Speed_Name => "Public_Speed辅助";
    public static SpeedMager I;

    private static float Public_Const_Speed_ = 1;

    public bool Deb;
    public static float Public_Const_Speed
    {
        get { return Public_Const_Speed_; }
        private set
        {
            if (value == 0 || float.IsNaN(value))
            {
       Debug.LogError("到底是哪里让我变成零？？");
                Public_Const_Speed_ = 1;
                return;
            }

            if (Public_Const_Speed_ != value)
            {
   
           if(SpeedMager.I.  Deb  )         Debug.LogError($"变速，由{Public_Const_Speed_}变成{value}");
                Public_Const_Speed_ = value;

                // 触发相关事件
                //I.Public_Speed_?.Invoke();
            }
        }
    }

    // ========== 公共字段和属性 ==========
    public bool 变速变速 =>Player3.I.N_.速度切换;
    public Action Public_Speed_;

    [DisplayOnly]
    [SerializeField]
    private float Speed_Leve;

    private float Last副Speed;
    public float Last副Speed1Leve
    {
        get => Last副Speed == 0 ? 1 : Last副Speed;
        set => Last副Speed = value;
    }

    public float Speed_Leve1 { get { 
        return Speed_Leve;
        }
        set {
            if (Speed_Leve!=value)
            {
                Event_M.I.Invoke(Event_M.刷新提示机关);
            }
            Speed_Leve = value;
        }
    }

    // ========== 私有字段 ==========
    private No_Re RR = new No_Re();

    // ========== Unity 生命周期方法 ==========
    private void Awake()
    {
        // 单例初始化
        if (I != null && I != this)
            Destroy(this);
        else
            I = this;

        // 加载保存的速度值
        Public_Const_Speed = Save_D.Load_Value_D<float>(Public_Const_Speed_Name);
        Speed_Leve1 = Public_Const_Speed;
        Last副Speed1Leve = Save_D.Load_Value_D<float>(Public_Const辅助_Speed_Name);
        // 可选：注册速度变化事件
        // SpeedMager.I.Public_Speed_ += 变速了;
    }


    public bool 速度不一致开始;
    public float 间隔=3;
    public float 渐变速度 = 1f;
    float 进入时间;
    private void Update()
    {
 
            if (Speed_Leve1!=Public_Const_Speed)
            {
                if(进入时间+ 间隔 < Time.time)
                if (Public_Const_Speed> Speed_Leve1)
                {
                    var a =( Public_Const_Speed / Speed_Leve1)  * Time.deltaTime* 渐变速度;
                    Public_Const_Speed -= a;

                    if (Public_Const_Speed < Speed_Leve1|| Public_Const_Speed._is(Speed_Leve1,0.001f) )
                    {
                        临时速度清除();
                    }
                }
                else
                {
                    Debug.LogError (Speed_Leve1 + "速度想要变慢？？？？" + Public_Const_Speed);
                } 
            }
            else
            {
                //Debug.LogError( Speed_Leve1+ "啊？？？" + Public_Const_Speed);
            }
        } 
    // ========== 公共方法 ==========


    /// <summary>
    /// 切换主速度与副速度
    /// </summary>
    public void 切换()
    {
        float temp = Speed_Leve1;
        Speed_Leve1 = Last副Speed1Leve;
        Last副Speed1Leve = temp;

        Public_Const_Speed = Speed_Leve1;
    }

    /// <summary>
    /// 速度同步核心方法
    /// 根据速度接口的状态更新当前速度
    /// </summary>
    public void 同速_(I_Speed_Change speedInterface)
    {
        // 1. 前置条件检查
        if (!RR.Note_Re()) return;

      if(Deb)  Debug.LogError($"{speedInterface.Speed_Lv}  速度  {speedInterface.Current_Speed_LV}");

        // 2. 判断速度状态类型并处理
        if (_is接口是静态速度(speedInterface))
        {
            h静态接口(speedInterface);
        }
        else
        {
            活跃接口(speedInterface);
        }
    }
    [Button("Play_", ButtonSizes.Large)]
    /// <summary>
    /// 直接设置速度（覆盖当前速度）
    /// </summary>
    public void SetSpeed(float CutternSpeed,float SpeedLv)
    {

       Public_Const_Speed = CutternSpeed;
        Speed_Leve1 = SpeedLv;
    }

    // ========== 私有辅助方法 ==========

    /// <summary>
    /// 检查是否为同速度等级情况
    /// </summary>
    private bool _is接口是静态速度(I_Speed_Change speedInterface)
    {
        return speedInterface.Speed_Lv == speedInterface.Current_Speed_LV;
    }

    /// <summary>
    /// 处理静态速度等级的情况
    /// </summary>
    private void h静态接口(I_Speed_Change speedInterface)
    {
        float targetSpeed = speedInterface.Current_Speed_LV;

        // 验证目标速度有效性
        if (targetSpeed == 0)
        {
            if (Deb) Debug.LogError($"出错：速度为零 {targetSpeed}");
            return;
        }

        // 检查是否需要更新
        if (Public_Const_Speed == targetSpeed)
        {
            if (Deb) Debug.LogError($"{Public_Const_Speed}      等级一致      {targetSpeed}");
            return;
        }

        // 播放速度变化效果
        视听体验(targetSpeed);

        // 根据变速模式处理
        if (变速变速)
        {
            有变速(speedInterface, targetSpeed);
        }
        else
        {
            主速度应用(speedInterface, targetSpeed);
        }
    }

    /// <summary>
    /// 处理不同速度等级的情况
    /// </summary>
    private void 活跃接口(I_Speed_Change speedInterface)
    {
        float targetSpeed = speedInterface.Current_Speed_LV;

        // 特殊情况：目标速度等于当前显示速度
        if (targetSpeed == Public_Const_Speed)
        {
            // 理论上不可能的情况
            return;
        }

        // 判断速度等级关系并相应处理
        if (speedInterface.Speed_Lv._is(Speed_Leve1))
        {
            // 相同底层等级，只更新显示速度
            Public_Const_Speed = targetSpeed;
        }
        else
        {   // 不同底层等级，只底层更新
            ProcessSpeedLevelChange(speedInterface, targetSpeed);
        }

        // 触发变速事件
        speedInterface.变速触发?.Invoke();
    }

    /// <summary>
    /// 在变速模式下处理速度变化
    /// </summary>
    private void 有变速(I_Speed_Change speedInterface, float targetSpeed)
    {
        Debug.LogError($"{speedInterface.Speed_Lv}  速度  {Public_Const_Speed}");

        // 情况1：与副速度等级一致
        if (speedInterface.Speed_Lv._is(Last副Speed1Leve, 0.001f))
        {
        if(SpeedMager.I.Deb)    Debug.LogError("AAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            切换(); // 切换主副速度
        }
        // 情况2：与当前主速度等级一致
        else if (speedInterface.Speed_Lv._is(Public_Const_Speed, 0.001f))
        {
            if (SpeedMager.I.Deb) Debug.LogError("BBBBBBBBBBBBBBBBBBBBBBBBBBBBB");
            // 和主速度一致，不需要做任何事  但是不大可能
        }
        // 情况3：全新的速度等级
        else
        {
            if (SpeedMager.I.Deb) Debug.LogError($"CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC {Public_Const_Speed}");
            主速度应用(speedInterface, targetSpeed);
        }
        进入时间 = Time.time;
    } 
    /// <summary>
    /// 应用速度变更
    /// </summary>
    private void 主速度应用(I_Speed_Change speedInterface, float targetSpeed)
    {
        SetSpeed(targetSpeed, speedInterface.Speed_Lv); 
        speedInterface.变速触发?.Invoke();
    }

    /// <summary>
    /// 处理速度等级变化
    /// </summary>
    private void ProcessSpeedLevelChange(I_Speed_Change speedInterface, float targetSpeed)
    {
        // 检查是否与副速度等级一致
        if (speedInterface.Speed_Lv._is(Last副Speed1Leve, 0.001f))
        {
            // 与副手一致：切换并更新显示速度
            切换();
            Public_Const_Speed = targetSpeed;
        }
        else
        {
            // 与副手不一致：直接赋值

            SetSpeed(targetSpeed, speedInterface.Speed_Lv); 
        }
    }

    /// <summary>
    /// 播放速度变化的效果和音效
    /// </summary>
    private void 视听体验(float targetSpeed)
    {
        // 播放变速特效
        Player3.I.变速特效(targetSpeed);

        // 根据速度变化方向播放对应音效
        if (Public_Const_Speed > targetSpeed)
        {
            yalaAudil.I.EffectsPlay("SpeedUp", 3);
        }
        else
        {
            yalaAudil.I.EffectsPlay("SpeedDown", 3);
        }
    }

    internal void 临时速度清除()
    {
        Public_Const_Speed = Speed_Leve;
    }
}
