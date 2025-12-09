using UnityEngine;
using System.IO;
using System.Collections.Generic;
using 发射器空间;
using Sirenix.OdinInspector;
public class TextBlur : MonoBehaviour
{
   public List< Texture2D> 模糊纹理列表 = new List<Texture2D>(); // 存储生成的 

    [Button("Play_", ButtonSizes.Large)]
    public  void 生成()
    { 
        TextBlur.模糊设置 设置 = new TextBlur.模糊设置
        {
            模糊半径 = 3,
            迭代次数 = 1,
            输出文件夹 = "模糊纹理",
            保持原尺寸 = true
        };

        // 生成并保存模糊纹理
        TextBlur.批量生成模糊纹理(模糊纹理列表, 设置);
    }
    [System.Serializable]
    public class 模糊设置
    {
        public int 模糊半径 = 3;
        public int 迭代次数 = 1;
        public string 输出文件夹 = "模糊纹理";
        public bool 保持原尺寸 = true;
        public float 缩放比例 = 1.0f;
    }

    // 生成单个纹理的模糊版本
    public static Texture2D 生成模糊纹理(Texture2D 原纹理, 模糊设置 设置)
    {
        if (原纹理 == null)
        {
            Debug.LogError("原纹理不能为空");
            return null;
        }

        // 创建可读的纹理副本
        Texture2D 可读纹理 = 创建可读纹理副本(原纹理);

        // 应用模糊效果
        Texture2D 模糊纹理 = 应用模糊算法(可读纹理, 设置);

        // 清理临时纹理
        DestroyImmediate(可读纹理);

        return 模糊纹理;
    }

    // 生成并保存模糊纹理到文件
    public static void 生成并保存模糊纹理(Texture2D 原纹理, 模糊设置 设置, string 自定义文件名 = null)
    {
        Texture2D 模糊纹理 = 生成模糊纹理(原纹理, 设置);

        if (模糊纹理 == null) return;

        // 确定输出路径
        string 输出路径 = 获取输出路径(设置.输出文件夹);

        // 确定文件名
        string 文件名;
        if (!string.IsNullOrEmpty(自定义文件名))
        {
            文件名 = 自定义文件名 + ".png";
        }
        else
        {
            string 原纹理名 = 原纹理.name;
            文件名 = 原纹理名 + "_模糊.png";
        }

        string 完整路径 = Path.Combine(输出路径, 文件名);

        // 保存为PNG
        byte[] png数据 = 模糊纹理.EncodeToPNG();
        File.WriteAllBytes(完整路径, png数据);

        // 清理纹理
        DestroyImmediate(模糊纹理);

        Debug.Log("模糊纹理已保存: " + 完整路径);

        // 刷新资源数据库（在编辑器环境下）
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

    // 批量生成模糊纹理
    public static void 批量生成模糊纹理(List<Texture2D> 纹理列表, 模糊设置 设置)
    {
        if (纹理列表 == null || 纹理列表.Count == 0)
        {
            Debug.LogError("纹理列表为空");
            return;
        }

        string 输出路径 = 获取输出路径(设置.输出文件夹);
        int 成功数量 = 0;

        foreach (Texture2D 纹理 in 纹理列表)
        {
            if (纹理 == null) continue;

            try
            {
                生成并保存模糊纹理(纹理, 设置);
                成功数量++;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"处理纹理 {纹理.name} 时出错: {e.Message}");
            }
        }

        Debug.Log($"批量处理完成，成功生成 {成功数量} 个模糊纹理");
    }

    // 核心模糊算法
    private static Texture2D 应用模糊算法(Texture2D 原图, 模糊设置 设置)
    {
        // 确定输出尺寸
        int 输出宽度 = 设置.保持原尺寸 ? 原图.width : Mathf.RoundToInt(原图.width * 设置.缩放比例);
        int 输出高度 = 设置.保持原尺寸 ? 原图.height : Mathf.RoundToInt(原图.height * 设置.缩放比例);

        Texture2D 当前处理图 = 原图;
        Texture2D 结果图 = new Texture2D(输出宽度, 输出高度);

        // 如果尺寸改变，先缩放
        if (!设置.保持原尺寸)
        {
            当前处理图 = 缩放纹理(原图, 输出宽度, 输出高度);
        }

        // 应用多次模糊迭代
        for (int 迭代 = 0; 迭代 < 设置.迭代次数; 迭代++)
        {
            结果图 = 应用单次模糊(当前处理图, 设置.模糊半径);

            if (迭代 < 设置.迭代次数 - 1)
            {
                // 如果不是最后一次迭代，更新当前处理图
                if (当前处理图 != 原图) // 避免销毁原图
                {
                    DestroyImmediate(当前处理图);
                }
                当前处理图 = 结果图;
                结果图 = new Texture2D(输出宽度, 输出高度);
            }
        }

        // 清理临时纹理
        if (当前处理图 != 原图)
        {
            DestroyImmediate(当前处理图);
        }

        return 结果图;
    }

    // 单次模糊处理
    private static Texture2D 应用单次模糊(Texture2D 纹理, int 模糊半径)
    {
        Texture2D 模糊图 = new Texture2D(纹理.width, 纹理.height);
        Color[] 原像素 = 纹理.GetPixels();
        Color[] 模糊像素 = new Color[原像素.Length];

        int 宽度 = 纹理.width;
        int 高度 = 纹理.height;

        // 并行处理每个像素
        for (int i = 0; i < 原像素.Length; i++)
        {
            int x = i % 宽度;
            int y = i / 宽度;
            模糊像素[i] = 计算像素模糊值(原像素, 宽度, 高度, x, y, 模糊半径);
        }



        模糊图.SetPixels(模糊像素);
        模糊图.Apply();
        return 模糊图;
    }
    public static Color 伽马提升亮度(Color 原颜色, float 伽马值)
    {
        Color 新颜色 = 原颜色;

        // 使用伽马校正提升亮度
        新颜色.r = Mathf.Pow(原颜色.r, 1.0f / 伽马值);
        新颜色.g = Mathf.Pow(原颜色.g, 1.0f / 伽马值);
        新颜色.b = Mathf.Pow(原颜色.b, 1.0f / 伽马值);

        // 保持alpha通道不变
        新颜色.a = 原颜色.a;

        return 新颜色;
    }
    // 计算单个像素的模糊值
    private static Color 计算像素模糊值(Color[] 像素数组, int 宽度, int 高度, int x, int y, int 半径)
    {
        Color 总和 = Color.clear;
        int 计数 = 0;

        for (int 偏移X = -半径; 偏移X <= 半径; 偏移X++)
        {
            for (int 偏移Y = -半径; 偏移Y <= 半径; 偏移Y++)
            {
                int 采样X = Mathf.Clamp(x + 偏移X, 0, 宽度 - 1);
                int 采样Y = Mathf.Clamp(y + 偏移Y, 0, 高度 - 1);

                int 索引 = 采样Y * 宽度 + 采样X;
                总和 += 像素数组[索引];///采样像素颜色
                计数++;

                if (x == 0 && y == 0)
                {
                    Debug.Log(像素数组[索引]);
                }
            }
        }

        if (x == 0 && y == 0)
        {
            Debug.Log(总和 / 计数);
        }
        Color OOO = 总和 / 计数;
 
        return 伽马提升亮度(OOO, 2.2f);
    }

    // 纹理缩放
    private static Texture2D 缩放纹理(Texture2D 原纹理, int 新宽度, int 新高度)
    {
        Texture2D 缩放纹理 = new Texture2D(新宽度, 新高度);
        Color[] 缩放像素 = new Color[新宽度 * 新高度];

        for (int y = 0; y < 新高度; y++)
        {
            for (int x = 0; x < 新宽度; x++)
            {
                float u = (float)x / (新宽度 - 1);
                float v = (float)y / (新高度 - 1);

                Color 像素颜色 = 原纹理.GetPixelBilinear(u, v);
                缩放像素[y * 新宽度 + x] = 像素颜色;
            }
        }

        缩放纹理.SetPixels(缩放像素);
        缩放纹理.Apply();
        return 缩放纹理;
    }

    // 创建可读的纹理副本
    private static Texture2D 创建可读纹理副本(Texture2D 原纹理)
    {
        // 创建临时RenderTexture来复制纹理
        RenderTexture 临时RT = RenderTexture.GetTemporary(
            原纹理.width,
            原纹理.height,
            0,
            RenderTextureFormat.Default,
            RenderTextureReadWrite.Linear);

        Graphics.Blit(原纹理, 临时RT);

        // 激活临时RT并读取像素
        RenderTexture 原激活RT = RenderTexture.active;
        RenderTexture.active = 临时RT;

        Texture2D 可读纹理 = new Texture2D(原纹理.width, 原纹理.height);
        可读纹理.ReadPixels(new Rect(0, 0, 原纹理.width, 原纹理.height), 0, 0);
        可读纹理.Apply();

        // 恢复原激活的RT并释放临时RT
        RenderTexture.active = 原激活RT;
        RenderTexture.ReleaseTemporary(临时RT);

        return 可读纹理;
    }

    // 获取输出路径
    private static string 获取输出路径(string 文件夹名)
    {
        string 输出路径 = Path.Combine(Application.dataPath, 文件夹名);

        if (!Directory.Exists(输出路径))
        {
            Directory.CreateDirectory(输出路径);
        }

        return 输出路径;
    }

    // 示例使用方法
    public static void 示例用法()
    {
        // 创建模糊设置
        模糊设置 设置 = new 模糊设置
        {
            模糊半径 = 2,
            迭代次数 = 2,
            输出文件夹 = "生成的模糊纹理",
            保持原尺寸 = true
        };

        // 假设有一个纹理引用
        Texture2D 示例纹理 = null; // 这里需要实际赋值

        if (示例纹理 != null)
        {
            // 生成并保存模糊纹理
            生成并保存模糊纹理(示例纹理, 设置);
        }
    }
}