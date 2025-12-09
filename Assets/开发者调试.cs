using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine; 
using 发射器空间;
using static Player3;
using static RuntimeEnvironment;


public class 开发者调试 : MonoBehaviour
{
    public EnvironmentType E;
    public static 开发者调试 I { get; private set; }
   
    private void Awake()
    {
            if (I != null)    Destroy(this); 
        else    I = this;

        E = RuntimeEnvironment.CurrentEnvironment ;
    }
    private void Start()
    {
        if (能力覆盖)
        {
            Player3.I.N_ = Debbbb;
        }
    }


    [Button("Save", ButtonSizes.Large)]
    public void Save()
    {
        Player3.I.当前hp = Player3.I.hpMax; 
        传送导点.I.最后点 = transform.position;
        Player3.I.录入安全地点(true); 
        Player3.SaveAll();
    }
    public bool 开发者模式;
    [Button("修改速度", ButtonSizes.Large)]
    public void Set(float i)
    {
        Player3.I.SetSpeed(i); 
    }
    [SerializeField]
    玩家能力 Debbbb;

    [DisableOnPlay][SerializeField]  bool 能力覆盖1;
    [DisableOnPlay][SerializeField] bool 自由起始点1; 
    public bool 自由起始点 {
        get
        {
            return 自由起始点1 && 开发者模式;
        }
        set => 自由起始点1 = value; }
    public bool 能力覆盖 { get {
            return 能力覆盖1&& 开发者模式;
        }  set => 能力覆盖1 = value; }
}

public static class RuntimeEnvironment
{
    /// <summary>
    /// 运行环境类型
    /// </summary>
    public enum EnvironmentType
    {
        Editor,             // 在Unity编辑器中运行
        DevelopmentBuild,   // 开发版本打包
        ProductionBuild,    // 正式发布版本
        DebugMode,          // 调试模式（无论打包与否）
        ProfilerAttached,   // 有性能分析器连接
        TestRunner,         // 测试环境
    }

    /// <summary>
    /// 当前运行环境
    /// </summary>
    public static EnvironmentType CurrentEnvironment
    {
        get
        {
            if (IsEditor)
                return EnvironmentType.Editor;
            else if (IsDevelopmentBuild)
                return EnvironmentType.DevelopmentBuild;
            else
                return EnvironmentType.ProductionBuild;
        }
    }

    /// <summary>
    /// 是否在Unity编辑器中运行
    /// </summary>
    public static bool IsEditor
    {
        get
        {
#if UNITY_EDITOR
            return true;
#else
            return false;
#endif
        }
    }

    /// <summary>
    /// 是否是开发版本打包
    /// </summary>
    public static bool IsDevelopmentBuild
    {
        get
        {
#if DEVELOPMENT_BUILD
            return true;
#else
            return false;
#endif
        }
    }

    /// <summary>
    /// 是否是发布版本
    /// </summary>
    public static bool IsProductionBuild
    {
        get
        {
            return !IsEditor && !IsDevelopmentBuild;
        }
    }

    /// <summary>
    /// 是否在调试模式下运行（包含编辑器和开发版本）
    /// </summary>
    public static bool IsDebugMode
    {
        get
        {
            return IsEditor || IsDevelopmentBuild || UnityEngine.Debug.isDebugBuild; ;
        }
    }

    /// <summary>
    /// 是否有性能分析器连接
    /// </summary>
    public static bool IsProfilerAttached
    {
        get
        {
            return UnityEngine.Profiling.Profiler.supported && UnityEngine.Profiling.Profiler.enabled;
        }
    }

    /// <summary>
    /// 是否在测试环境中运行
    /// </summary>
    public static bool IsTestRunner
    {
        get
        {
#if UNITY_INCLUDE_TESTS
            return true;
#else
            return false;
#endif
        }
    }

    /// <summary>
    /// 是否允许调试功能（如控制台、调试面板等）
    /// </summary>
    public static bool AllowDebugFeatures
    {
        get
        {
            return IsDebugMode && !IsProductionBuild;
        }
    }

    /// <summary>
    /// 是否应该记录详细日志
    /// </summary>
    public static bool ShouldLogVerbose
    {
        get
        {
            return IsDebugMode || IsProfilerAttached;
        }
    }
}
