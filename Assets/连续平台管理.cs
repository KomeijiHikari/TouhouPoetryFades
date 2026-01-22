using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 未知原因，子索引最少要三个
/// 连续平台机关组 - 主控制器
/// 功能：管理平台挑战的完整流程，包括前置条件检测、状态机管理、游标移动控制等
/// 设计模式：状态机 + 事件驱动
/// </summary>
public class 连续平台管理 : MonoBehaviour, I_Dead,I_Speed_Is,I_暂停,I_Revive
{

    public bool Re { get; set; }=false;
    public float Re_Time { get; set; } = 0;
    public bool 重制()
    {
        if (Deb) Debug.LogError("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        Speed = StartSpeed;

        if (!已经初始化) return false ;

        完成=false ;
        前置条件=false ;
    for (int i = 0; i < Ls.Length; i++)
        {
            var a= Ls[i];
            a.关上();
            a.不受玩家影响 = false;
        }
        路径索引 = -1;            // 重置路径索引
        Ls[0].不受玩家影响 = true;
        // 重置挑战进度
        已完成进度 = -1;
        未完成进度 = 1;
        已激活 = 0;

        // 重置游标
        游标.transform.position = Ls[0].transform.position;
        游标.gameObject.SetActive(false);
        return true;
    }
    public bool 暂停 { get => 暂停1; set => 暂停1 = value; }
    [SerializeField]
    private float speed_Lv=1;
    // ========== 公开配置字段 ==========

    /// <summary> 前置条件：玩家是否拥有特定能力 </summary>
    public bool 前置条件;

    /// <summary> 路径管理器引用，提供游标移动的路径点 </summary>
    public 线管理 x;

    /// <summary> 路径点列表（从线管理获取） </summary>
    public List<Vector3> Vs => x.Vs;

    /// <summary> 游标物体，沿路径移动的指示器 </summary>
    public GameObject 游标;

    /// <summary> 游标检测平台激活的统一范围半径 </summary>
    public float 检测范围 = 2;

    /// <summary> 游标移动速度（单位/秒） </summary>
    public float Speed = 1;

    // ========== 私有状态枚举 ==========

    /// <summary>
    /// 系统状态枚举（内部使用）
    /// 未激活：前置条件不满足，所有平台关闭
    /// 待交互：前置条件满足，等待玩家交互第一个平台
    /// 激活成功：挑战完成，所有平台保持激活
    /// </summary>
    enum E_状态
    {
        未激活,
        待交互,
        激活成功
    }
    public SpriteRenderer 激活与否;
    // ========== 平台引用和数据 ==========

    /// <summary> 所有平台实例的数组 </summary>
    [SerializeField][DisplayOnly] 连续平台[] Ls;

    /// <summary> 防重复触发工具 </summary>
    No_Re re = new No_Re();

    // ========== 核心状态变量 ==========

    /// <summary> 挑战是否已完成（最后一个平台被激活） </summary>
    [SerializeField][DisplayOnly] public bool 完成;

    /// <summary> 当前处于激活状态的平台索引（游标范围内） </summary>
    [SerializeField][DisplayOnly] int 已激活 = 0;

    /// <summary> 游标当前所在的路径点索引 </summary>
    [SerializeField][DisplayOnly] int 路径索引 = -1;

    /// <summary> 已完成的平台索引（最后一个被交互的平台） </summary>
    [SerializeField][DisplayOnly] int 已完成进度 = -1;

    /// <summary> 下一个目标平台的索引（游标正在前往的平台） </summary>
    [SerializeField][DisplayOnly] int 未完成进度 = 0;

    // ========== 计算属性 ==========

    /// <summary> 是否正在运行挑战（玩家已触发第一个平台） </summary>
    bool 在运行 => 已完成进度 != -1;
    public bool Dead()
    {
        if (!已经初始化)   Start(); 
        完成 = true;
        游标.SetActive(false); 
        半灵.I.SetTarget(Ls[0].transform, false);
        // 激活所有平台并锁定状态
        for (int ii = 0; ii < Ls.Length; ii++)
        { 
            var a = Ls[ii];
            a.开起来();               // 确保平台激活
            a.不受玩家影响 = true;    // 锁定平台状态
        }
        激活与否.gameObject.SetActive(true);
        // 扩展点：挑战成功事件
        OnChallengeSuccess?.Invoke();
        return true;
    }
    public Action 销毁触发 { get  ; set  ; } 
    public Bounds 盒子 =>default;

    public float Speed_Lv { get => speed_Lv; set => speed_Lv = value; }

    // ========== Unity生命周期方法 ==========
    Color C;
    float StartSpeed;
    private void Awake()
    {
        StartSpeed = Speed;
    }
    void Start()
    {

        C = 激活与否.color;
        // 收集所有子平台
        Ls = GetComponentsInChildren<连续平台>();
        激活与否.gameObject.SetActive(false);
        // 设置第一个平台为特殊平台（完成后不会关闭）
        Ls[0].不受玩家影响 = true;

        // 初始化每个平台
        for (int i = 0; i < Ls.Length; i++)
        {
            var a = Ls[i];
            a.我的索引 = i;
            a.开起来_Action += 被触发;  // 订阅平台触发事件
        }

        // 初始隐藏游标
        游标.gameObject.SetActive(false);

        已经初始化 = true;
        // 扩展点：可在此处添加初始化完成事件
        OnInitialized?.Invoke();
    }
    bool 已经初始化;

    public bool Deb;
    /// <summary>
    /// 平台触发事件回调
    /// 处理玩家与平台的交互逻辑
    /// </summary>
    /// <param name="i">被触发平台的索引</param>
    private void 被触发(int i)
    {
        // 如果挑战已完成，不再响应任何触发
        if (完成) return;

        // 只有前置条件满足时才处理
        if (前置条件)
        {
            // 情况1：玩家触发第一个平台
            if (i == 0)
            {
                // 第一次触发（启动挑战）
                if (已完成进度 == -1)
                {
                    已完成进度 = 0;          // 标记第一个平台完成
                    未完成进度 = 1;          // 设置下一个目标为第二个平台

                    // 初始化游标位置并显示
                    游标.transform.position = Ls[0].transform.position;
                    游标.gameObject.SetActive(true);

                    // 扩展点：挑战开始事件
                     OnChallengeStarted?.Invoke();
                }
                // 非第一次触发（挑战已开始，再次踩到第一个平台）
                else
                {
                    // 此处可添加额外逻辑，如重新开始或忽略
                    // 扩展点：第一个平台被重复触发事件
                    OnFirstPlatformRetriggered?.Invoke();
                }
            }
            // 情况2：玩家触发非第一个平台
            else if (i == 已激活 && 已激活 == 未完成进度)
            {
                      Vector3 my = 游标.transform.position; 
                Vector3 检测点 = Vs[路径索引 + 1];
                // 检测游标是否进入目标平台范围
                if (Vector3.Distance(my, 检测点) < 检测范围)
                {
                    if (已激活 == 未完成进度)
                    {
                        //   上个范围内
                        Debug.LogError("AAAAAAAAAAAAAAAAAAAAAAAAAAAA" + 游标.transform.position);
                        //if (Deb)             Debug.Break();

                        游标.transform.position = 检测点;
                        Debug.LogError("AAAAAAAAAAAAAAAAAAAAAAAAAAAA" + 游标.transform.position);
                    }
                }
                // 前进到下一个平台
                已激活 = 0;          // 重置激活状态
                未完成进度++;       // 目标平台索引+1
                已完成进度++;       // 完成进度+1

                // 检查是否完成所有平台
                if (已完成进度 == Ls.Length - 1)
                {
                    Debug.LogError("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB");
                    销毁触发?.Invoke();
                }
            }
        }
    }

    /// <summary>
    /// 重置挑战状态
    /// 当玩家未在游标离开前交互平台时调用
    /// </summary>
    void 重置()
    {
        if (前置条件)
        {
            // 重置所有平台状态
            // 注意：这里需要确保所有平台都被重置
            // 扩展点：可添加平台重置循环

            Speed= StartSpeed;


            Ls[0].开起来();           // 重新激活第一个平台
            路径索引 = -1;            // 重置路径索引

            // 重置挑战进度
            已完成进度 = -1;
            未完成进度 = 1;
            已激活 = 0;

            // 重置游标
            游标.transform.position = Ls[0].transform.position;
            游标.gameObject.SetActive(false);

            // 扩展点：挑战重置事件
            OnChallengeReset?.Invoke();
        }

    }

    void FixedUpdate()
    {
        if (暂停) return;
        // 如果挑战未运行或已完成，不更新
        if (!在运行) return;
        if (完成) return;

        // ========== 游标移动逻辑 ==========

        Vector3 my = 游标.transform.position;
        if (路径索引 + 1==Vs.Count)
        {
            Debug.Break();
        }
        Vector3 you = Vs[路径索引 + 1];

        // 向目标路径点移动
        游标.transform.position = Vector3.MoveTowards(my, you, Speed * Time.fixedDeltaTime*((I_Speed_Is)this).固定等级差);

        // 到达路径点后更新索引
        if (Vector3.Distance(my, you) < 0.1f)
        { 
                路径索引++;
            if (路径索引 + 1 == Vs.Count)
            {
                /// 最后 的  判定失败

                Ls[未完成进度].关上();

                // 重置挑战（玩家未及时交互）
                重置();

                // 扩展点：平台取消激活事件
                OnPlatformDeactivated?.Invoke(未完成进度);
            }
        }

        // ========== 平台激活检测逻辑 ==========

        // 获取下一个目标平台的位置
        Vector3 检测点 = Ls[未完成进度].transform.position;

        // 调试绘制（仅在编辑器或调试时显示）
        Ls[未完成进度].transform.position.DraClirl(检测范围, Color.white);

        // 检测游标是否进入目标平台范围
        if (Vector3.Distance(my, 检测点) < 检测范围)
        {
            // 游标在平台范围内
            if (未完成进度 != 已激活)
            {
                // 刚进入范围：激活平台
                Ls[未完成进度].开起来();
                已激活 = 未完成进度;

                // 扩展点：平台激活事件
                OnPlatformActivated?.Invoke(未完成进度);
                //Debug.LogError(路径索引 + 2+"   "+ Vs.Count);
                if (路径索引 + 2 == Vs.Count)
                {
                    ///最后一个路径点
                    Speed /= 2;

                }
            }
            else
            {       
                // 已在范围内：持续激活状态
                // 扩展点：平台保持激活事件

  
            }
        }
        else
        {
            // 游标在平台范围外
            if (已激活 == 未完成进度)
            {
                // 刚离开范围：取消激活平台
                Ls[未完成进度].关上();

                // 重置挑战（玩家未及时交互）
                重置();

                // 扩展点：平台取消激活事件
                OnPlatformDeactivated?.Invoke(未完成进度);
            }
        }
    }
    bool Last前置条件; 
    void Update()
    {
        if (暂停) return;
        if (激活与否.isVisible)
        {
            if ( 完成)
            {
                激活与否.color = C;
            }
            else
            {
                float F = Mathf.Sin(Time.time * 4) * 0.5f + 0.5f;
                激活与否.color = new Color(C.r,C.g,C.b, F*C.a);
            }

        }
        // ========== 前置条件检测 ==========

        // 检测玩家是否拥有所需能力（这里假设是半灵能力） 
        if (Player3.I.N_.半灵)
        {

            ///检测玩家是否在附近 是的话半灵过来
            ///半灵过来后激活机关
            ///前置条件达成
            ///如果离开范围取消激活
            ///如果踩上第一个机关那么不用检测范围   取消激活条件是退出房间或者失败 
            if (!在运行&& !完成)
            { 
                ///没在运行
                ///
                Vector3 My = Ls[0].transform.position;
                bool 玩家在范围内 = Vector2.Distance(Player3.I.transform.position, My) < 7;
                半灵.I.SetTarget(Ls[0].transform, 玩家在范围内);
                if (玩家在范围内)
                { 
                    bool 半灵在范围内 = Vector2.Distance(半灵.I.transform.position, My) < 1;
                    if (半灵在范围内)
                    { 
                        Last前置条件 = true;
                    }
                    else
                    { 
                        Last前置条件 = false;
                    }
                    ///半灵没到
                }
                else
                { 
                    ///玩家没到
                    Last前置条件 = false;
                }
            }
            else
            { 
                ///正在运行 
                Last前置条件 = true;
            }

       
            //半灵.I.SetTarget(Ls[0].transform,true);


        }
        激活与否.gameObject.SetActive(Last前置条件);
        //Last前置条件 = Player3.I.N_.半灵;
        if (Last前置条件!= 前置条件)
        {
            前置条件 = Last前置条件;

            ///只会在改变的时候调用
            if (前置条件)
            {
                OnPreconditionMet?.Invoke();
                 Ls[0].开起来();
               
            }
            else
            {///不满足
                  Ls[0].关上();
                OnPreconditionFailed?.Invoke();
            }
        } 
    }



    // ========== 扩展事件定义区域 ==========

    // 建议添加的事件委托：
    public event Action OnInitialized;              // 初始化完成
    public event Action OnPreconditionMet;          // 前置条件满足
    public event Action OnPreconditionFailed;       // 前置条件不满足
    public event Action OnChallengeStarted;         // 挑战开始
    public event Action<int> OnPlatformActivated;   // 平台被激活
    public event Action<int> OnPlatformDeactivated; // 平台被取消激活
    public event Action<int> OnPlatformCompleted;   // 平台被完成
    public event Action<float> OnProgressUpdated;   // 进度更新（0-1）
    public event Action OnChallengeReset;           // 挑战重置
    public event Action OnChallengeSuccess;         // 挑战成功

    // 可视化效果接口：
    public Func<GameObject, Material> 获取平台激活材质; // 获取平台激活时的材质
    public Action<GameObject> 播放平台激活特效;         // 播放平台激活特效
    public Action<GameObject> 播放游标移动特效;         // 播放游标移动特效
    public Action OnFirstPlatformRetriggered;
    private bool 暂停1;
}