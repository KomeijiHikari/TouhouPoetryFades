using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 切换Shader : MonoBehaviour
{
    public static 切换Shader I;

    [Tooltip("是否强制所有 TextQieHuan 显示为红色")]
    private bool isSpeed_ = false;

    // shader 名称（确保与文件中的 Shader \"<name>\" 一致）
    const string ShaderName = "Hidden/TextQieHuan";
    const string Keyword = "SHOW_RED";

    // 可选：缓存已找到的材质，避免每帧查找
    List<Material> cachedMaterials = new List<Material>();
    Shader targetShader;

    public bool isSpeed
    { get {  
        return isSpeed_;
        } set {
            if (isSpeed_ != value)
            {
                SpeedAction?.Invoke(value);
            }
            isSpeed_ = value;
        } }
    public Action<bool> SpeedAction;
    void Awake()
    {
        if (I != null && I != this) Destroy(this);
        else I = this;
        targetShader = Shader.Find(ShaderName);
        RefreshMaterials();
        // 将初始状态应用一次
        ApplyKeyword(isSpeed);
    }

    // 在编辑器中切换时也更新材质集合
    public void RefreshMaterials()
    {
        cachedMaterials.Clear();
        if (targetShader == null) return;

        // 遍历场景中的所有渲染组件并收集使用该 shader 的材质（注意共享材质）
        var renderers = FindObjectsOfType<Renderer>();
        foreach (var r in renderers)
        {
            if (r.sharedMaterials == null) continue;
            foreach (var m in r.sharedMaterials)
            {
                if (m == null) continue;
                if (m.shader == targetShader && !cachedMaterials.Contains(m))
                {
                    cachedMaterials.Add(m);
                }
            }
        }

        // 还可以扫描 UI 的 CanvasRenderer -> Graphic.material
        var graphics = FindObjectsOfType<UnityEngine.UI.Graphic>();
        foreach (var g in graphics)
        {
            var mat = g.material;
            if (mat != null && mat.shader == targetShader && !cachedMaterials.Contains(mat))
                cachedMaterials.Add(mat);
        }
    }

    void OnValidate()
    {
        // 编辑器属性变更时生效
        if (Application.isPlaying) ApplyKeyword(isSpeed);
        else
        {
            // 在编辑模式下仍然刷新列表（可选）
            targetShader = Shader.Find(ShaderName);
            RefreshMaterials();
        }
    }

    void Update()
    {
        // 运行时如果需要实时切换（你也可以通过其他方式调用 ApplyKeyword）
        ApplyKeyword(isSpeed);
    }

    void ApplyKeyword(bool enable)
    {
        //if (targetShader == null) targetShader = Shader.Find(ShaderName);
        //if (targetShader == null) return;

        // 全局关键字（会影响所有使用该关键字的 shader 变体）
        if (enable) Shader.EnableKeyword(Keyword);
        else Shader.DisableKeyword(Keyword);

        //// 针对缓存的每个材质单独启/停关键字（避免影响其它 shader）
        //for (int i = 0; i < cachedMaterials.Count; i++)
        //{
        //    var m = cachedMaterials[i];
        //    if (m == null) continue;
        //    if (enable) m.EnableKeyword(Keyword);
        //    else m.DisableKeyword(Keyword);
        //}
    }

    // 调用此方法强制刷新场景中使用该 shader 的材质集合（例如在动态加载对象后）
    //public void ForceRefreshAndApply()
    //{
    //    RefreshMaterials();
    //    ApplyKeyword(isRed);
    //}
}
