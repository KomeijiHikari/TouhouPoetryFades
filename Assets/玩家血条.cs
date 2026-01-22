using UnityEngine;
using UnityEngine.UI; // 可能需要用于自动布局

public class 玩家血条 : MonoBehaviour
{
    public 单位血 预制体; // 单个血格的预制体

    // 使用属性封装字段，并加入变更检查
    private int _currentHp;
    public int Int_Hp_
    {
        get => _currentHp;
        set
        {
            if (_currentHp != value)
            {
                _currentHp = Mathf.Clamp(value, 0, Int_Maxhp_); // 确保血量在有效范围
                更新血格显示(); // 血量变化时，只更新显示状态
            }
        }
    }

    private int _maxHp;
    public int Int_Maxhp_
    {
        get => _maxHp;
        set
        {
            if (_maxHp != value && value > 0) // 最大血量变化且有效时才更新
            {
                _maxHp = value;
                _currentHp = Mathf.Min(_currentHp, _maxHp); // 调整当前血量不超过新上限
                调整血格数量(); // 最大血量变化时，调整血格数量
            }
        }
    }

    private 单位血[] _血格数组; // 改用数组，访问更快
    // private List<单位血> Ds; // 原List，已替换

    private void Start()
    {
        同步玩家数据(v测试hp, m测试最大hp);
        // 初始化时确保血格数量正确
        if (_血格数组 == null || _血格数组.Length != _maxHp)
        {
            调整血格数量();
        }
        更新血格显示(); // 初始化显示

        for (int i = 0; i < _血格数组.Length; i++)
        {
            _血格数组[i]. 初始化(); 
        }
      
    }

    // 将原来的Update逻辑改为由属性触发，避免每帧无意义检查
    // void Update() { ... } // 原Update函数已移除

    /// <summary>
    /// 调整血格数量以匹配最大血量。
    /// 只创建缺少的或销毁多余的血格，避免全部重做。
    /// </summary>
    private void 调整血格数量()
    {
        if (预制体 == null || transform == null)
        {
            Debug.LogError("玩家血条：预制体或Transform未设置！");
            return;
        }

        int 需要数量 = Int_Maxhp_;
        if (_血格数组 != null && _血格数组.Length == 需要数量)
        {
            return; // 数量已匹配，无需操作
        }

        // 1. 创建新的数组
        var 新数组 = new 单位血[需要数量];

        if (_血格数组 != null)
        {
            // 2. 复制已有的血格
            int 复制数量 = Mathf.Min(需要数量, _血格数组.Length);
            for (int i = 0; i < 复制数量; i++)
            {
                新数组[i] = _血格数组[i];
            }

            // 3. 销毁多余的血格（如果新上限比旧上限小）
            for (int i = 需要数量; i < _血格数组.Length; i++)
            {
                if (_血格数组[i] != null && _血格数组[i].gameObject != null)
                {
                    Destroy(_血格数组[i].gameObject);
                }
            }
        }

        // 4. 创建新增的血格（如果新上限比旧上限大）
        for (int i = _血格数组 != null ? _血格数组.Length : 0; i < 需要数量; i++)
        {
            新数组[i] = Instantiate(预制体, transform);
            // 可以在这里设置新血格的本地位置或布局，推荐使用Unity的布局组件
        }

        // 5. 替换旧数组
        _血格数组 = 新数组;

        // 6. 【可选】确保血格在层级视图中的顺序正确，便于调试查看
        for (int i = 0; i < _血格数组.Length; i++)
        {
            if (_血格数组[i] != null)
            {
                _血格数组[i].transform.SetSiblingIndex(i);
                _血格数组[i].初始化(); // 初始化状态
            }
        } 
    }

    /// <summary>
    /// 根据当前血量更新所有血格的显示状态。
    /// 此方法仅在血量或最大血量变化时调用。
    /// </summary>
    private void 更新血格显示()
    {
        if (_血格数组 == null) return;

        for (int i = 0; i < _血格数组.Length; i++)
        {
            if (_血格数组[i] != null)
            {
                // 血量大于索引i的血格应显示为“满”或“启用”
                _血格数组[i].B = (i < _currentHp); // 假设 B=true 表示满血/启用
            }
        }
    } 
    private void Update()
    {
        if (Player3.I.hpMax != Int_Maxhp_ || Player3.I.当前hp != Int_Hp_)
        {
            同步玩家数据(Player3.I.当前hp, Player3.I.hpMax);
        }

        return;
        if (m测试最大hp != Int_Maxhp_ || v测试hp != Int_Hp_)
        {
            同步玩家数据(v测试hp, m测试最大hp);
        }
    }
    // 提供外部接口，从玩家数据同步（可在玩家受伤/治疗时调用）
    public void 同步玩家数据(float 当前hp, float hpMax)
    {
        // 只同步整数部分，或根据你的游戏逻辑四舍五入
        Int_Maxhp_ = Mathf.RoundToInt(hpMax);
        Int_Hp_ = Mathf.RoundToInt(当前hp);
    }

    public int v测试hp=1;
 public    int m测试最大hp=3;
    // 保留原有的与Player3的链接属性，但现在通过 同步玩家数据 方法驱动更新
    // float Int_Hp { get ... } // 可考虑删除，改为直接使用属性或同步方法 
}