using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Sirenix.OdinInspector;



/// <summary>
/// 单个平台行为控制器
/// 功能：管理平台的状态、检测玩家交互、提供视觉反馈
/// 设计模式：状态模式 + 事件监听
/// </summary>
public class 连续平台 : MonoBehaviour, I_碰撞状态
{
 
    // ========== 组件引用 ==========

    /// <summary> 平台精灵渲染器，用于视觉表现 </summary>
    [SerializeField] SpriteRenderer sp;

    /// <summary> 平台碰撞体，用于物理交互 </summary>
    [SerializeField] BoxCollider2D bc;

    // ========== 状态变量 ==========

    /// <summary> 平台是否已被玩家交互完成（只读，用于调试显示） </summary>
    [DisplayOnly] public bool 是;

    /// <summary> 平台是否不受玩家影响（完成后锁定） </summary>
    public bool 不受玩家影响;

    // ========== 本地变量 ==========

    /// <summary> 平台在序列中的索引 </summary>
    public int 我的索引;

    // ========== 事件委托 ==========

    /// <summary>
    /// 平台触发事件
    /// 参数：平台索引
    /// 当玩家成功交互平台时触发
    /// </summary>
    public Action<int> 开起来_Action;

    // ========== 视觉参数 ==========

    /// <summary> 平台未激活时的缩放 </summary>
    Vector2 No = new Vector2(0.2f, 3.5f);

    /// <summary> 平台激活时的缩放 </summary>
    Vector2 Yes = new Vector2(3.5f, 0.2f);

    // ========== 临时变量 ==========

    /// <summary> 防重复触发协程引用 </summary>
    Coroutine C;

    /// <summary> 触发延迟等待时间 </summary>
    WaitForSeconds W = new WaitForSeconds(0.35f);
    private Action<int> OnPlatformCompletedByPlayer;
    private Action<int> OnPlatformInteractDelayed;

    // ========== Unity生命周期方法 ==========
    碰撞管理 p;

    private void Start()
    {
        // 订阅玩家圆斩事件
        Player3.I.圆斩对象 += asd;
    }
    private void Awake()
    {
        gameObject.组件(ref p);
        // 获取或添加必要组件
        gameObject.组件(ref bc);
        gameObject.组件(ref sp);

        // 扩展点：可在此处添加更多组件初始化
        // 例如：特效粒子系统、动画控制器等

 

        // 初始化平台为关闭状态
        开关(false);

        // 扩展点：平台初始化完成事件
        OnPlatformInitialized?.Invoke(我的索引);
    }
 

    /// <summary>
    /// 防重复触发协程
    /// 防止玩家在短时间内重复触发平台
    /// </summary>
    IEnumerator WW()
    {
        yield return W;

        // 触发平台事件

        // 标记平台为已完成
        if (!是)
        {
            是 = true;
        }

        // 清空协程引用
        C = null;

        // 扩展点：平台交互延迟完成事件
        OnPlatformInteractDelayed?.Invoke(我的索引);
    }

    /// <summary>
    /// 碰撞检测 - 玩家站在平台上
    /// 当玩家停留在平台上时触发
    /// </summary>
    private void OnCollisionStay2D(Collision2D collision)
    {
        // 检查碰撞对象是否为玩家
        if (collision.gameObject.CompareTag(Initialize.Player))
        {
            // 确保玩家是从上方站在平台上（避免侧面碰撞触发）
            if (Player3.I.Bounds.min.y - 0.01f > bc.bounds.max.y)
            {
                // 启动防重复触发协程
                if (C == null)
                {
                    C = StartCoroutine(WW());

                    开起来_Action?.Invoke(我的索引);
                    // 扩展点：玩家开始站立在平台上
                    OnPlayerStartStanding?.Invoke(我的索引);
                }
            }
        }
    }
     

    // ========== 公共控制方法 ==========

    /// <summary>
    /// 激活平台（主要用于编辑器按钮和外部调用）
    /// </summary>
    [Button]
    public void 开起来()
    { 
        开关(true);

        // 扩展点：平台被外部激活事件
        OnPlatformActivatedExternally?.Invoke(我的索引);
    }

    /// <summary>
    /// 关闭平台
    /// </summary>
    public void 关上()
    {
        重置状态();

        // 扩展点：平台被关闭事件
        OnPlatformDeactivatedExternally?.Invoke(我的索引);
    }
    public bool Deb;
    /// <summary>
    /// 平台开关核心逻辑
    /// 控制平台的视觉状态和物理交互
    /// </summary>
    /// <param name="b">true=开启, false=关闭</param>
    void 开关(bool b)
    { 
        if (b)
        { 
          Debug.LogError(我的索引 + "BBBBBBB" + b);
            // 开启状态
            transform.localScale = Yes;   // 应用激活缩放
            E_碰撞 = E_碰撞状态.碰撞;
                              是 = false;
            开了嘛 = true;
            // 扩展点：平台开启时的额外效果
            // 例如：播放音效、粒子特效、动画等
            PlayActivationEffects();
        }
        else
        {
            if (Deb) Debug.LogError(我的索引 + "AAAAAAAAAAAAA" + b);
            // 关闭状态
            开了嘛 = false;
            transform.localScale = No;    // 应用未激活缩放 
            E_碰撞 = E_碰撞状态.无碰撞;
            // 扩展点：平台关闭时的额外效果
            // 例如：播放关闭音效、渐变消失等
            PlayDeactivationEffects();
        }
    }
    bool 开了嘛;
    [SerializeField][DisplayOnly] E_碰撞状态 e_碰撞;

    public E_碰撞状态 E_碰撞 { get => e_碰撞; set => e_碰撞 = value; }

    private void PlayDeactivationEffects()
    { 
    }

    private void PlayActivationEffects()
    { 
    }

    // ========== 每帧更新 ==========

    private void Update()
    {
        // 如果碰撞体启用且平台标记为已完成
 
            if (是)
            {
                // 如果平台不受玩家影响，保持激活状态
                if (不受玩家影响) return;

                // 否则关闭平台（被玩家完成交互）
                开关(false);

                // 扩展点：平台被玩家完成事件
                OnPlatformCompletedByPlayer?.Invoke(我的索引);
            }
        设置颜色(Color.blue);
    }

    /// <summary>
    /// 圆斩能力交互回调
    /// 当玩家使用圆斩能力击中平台时触发
    /// </summary>
    /// <param name="i">被击中物体的InstanceID</param>
    void asd(int i)
    {
        // 检查是否是本平台被击中
        if (i == gameObject.GetInstanceID())
        {

            // 立即触发平台交互（无延迟）
            开起来_Action?.Invoke(我的索引);
            是 = true;

            // 扩展点：平台被圆斩击中事件
            OnPlatformHitBySlash?.Invoke(我的索引);
        }
    }

    // ========== 扩展方法区域 ==========
    
    // 建议添加的扩展方法：
    
    /// <summary> 设置平台视觉材质</summary>
    public void 设置材质(Material mat) { if (sp != null) sp.material = mat; }

    //// <summary> 设置平台颜色</summary>
    public void 设置颜色(Color color) { if (sp != null) sp.color = color; }

    //// <summary> 播放平台激活特效</summary>
    public void 播放激活特效()
    { /* 播放粒子特效等 


/// <summary> 播放平台完成特效 </summary>
public void 播放完成特效() { /* 播放完成动画等 */
    }

/// <summary> 重置平台状态（用于挑战重置） </summary>
public void 重置状态()
{
    是 = false;
    开关(false);
    if (C != null) StopCoroutine(C);
    C = null;
}

// 建议添加的事件：
public event Action<int> OnPlatformInitialized;           // 平台初始化完成
public event Action<int> OnPlatformActivatedExternally;   // 平台被外部激活
public event Action<int> OnPlatformDeactivatedExternally; // 平台被外部关闭
public event Action<int> OnPlayerStartStanding;           // 玩家开始站立
public event Action<int> OnPlatformHitBySlash;            // 平台被圆斩击中
 
    // ========== 编辑器辅助方法 ==========

 
}