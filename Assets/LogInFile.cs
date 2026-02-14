using System;
using System.IO;
using System.Text;
using UnityEngine;

/// <summary>
/// 全局日志管理类（支持发布包日志输出+文件持久化，日志自动存到log文件夹）
/// </summary>
public class LogInFile : MonoBehaviour
{
    // 单例实例
    private static LogInFile _instance;
    public static LogInFile I
    {
        get
        {
            if (_instance == null)
            {
                // 自动创建挂载对象，避免手动添加
                GameObject logObj = new GameObject("[LogInFile]");
                _instance = logObj.AddComponent<LogInFile>();
                DontDestroyOnLoad(logObj);
            }
            return _instance;
        }
    }

    #region 配置项
    // 是否启用日志（发布包建议开启，方便排查问题）
    public bool enableLog = true;
    // 是否写入日志文件（核心需求）
    public bool enableFileWrite = true;
    // 日志文件夹名称（新增：固定为log）
    private readonly string _logFolderName = "log";
    // 日志文件存储路径（包含log文件夹）
    private string _logFilePath;
    // 日志文件名称（按日期命名，避免覆盖）
    private string _logFileName;
    #endregion

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            InitLogPath(); // 初始化路径（包含创建log文件夹）
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 初始化日志文件路径（核心修改：自动创建log文件夹）
    /// </summary>
    private void InitLogPath()
    {
        // ====== 关键修改1：拼接log文件夹路径并自动创建 ======
        string logFolderPath = Path.Combine(Application.persistentDataPath, _logFolderName);
        // 检查文件夹是否存在，不存在则创建（CreateDirectory会自动处理多级目录，存在则无操作）
        if (!Directory.Exists(logFolderPath))
        {
            try
            {
                Directory.CreateDirectory(logFolderPath);
                // 编辑器/开发包下打印创建成功日志
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                Debug.Log($"日志文件夹创建成功：{logFolderPath}");
#endif
            }
            catch (Exception e)
            {
                // 文件夹创建失败时记录错误（不影响游戏运行）
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                Debug.LogError($"日志文件夹创建失败：{e.Message}");
#endif
                Console.WriteLine($"日志文件夹创建失败：{e.Message}");
            }
        }

        // 日志文件命名：Log_年月日_时分秒.txt
        _logFileName = $"Log_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
        // ====== 关键修改2：日志文件路径包含log文件夹 ======
        _logFilePath = Path.Combine(logFolderPath, _logFileName);

        // 写入日志头（标记日志开始时间）
        WriteLogToFile($"===== 游戏日志开始 [{DateTime.Now:yyyy-MM-dd HH:mm:ss}] =====", false);
    }

    #region 公开日志接口（兼容编辑器/发布包）
    /// <summary>
    /// 普通日志
    /// </summary>
    /// <param name="content">日志内容</param>
    /// <param name="withTime">是否带时间戳</param>
    public static void Log(string content, bool withTime = true)
    {
        if (!I.enableLog) return;

        string logContent = withTime ? $"[{DateTime.Now:HH:mm:ss}] [帧:{Time.frameCount}] [INFO] {content}" : content;

        // 编辑器下正常输出到Console，发布包下输出到系统控制台（可选）
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        Debug.Log(logContent);
#else
        Console.WriteLine(logContent); // 发布包下输出到系统控制台（Windows可看到）
#endif

        // 写入日志文件
        if (I.enableFileWrite)
        {
            I.WriteLogToFile(logContent);
        }
    }

    /// <summary>
    /// 警告日志
    /// </summary>
    public static void LogWarning(string content, bool withTime = true)
    {
        if (!I.enableLog) return;
 
        string logContent = withTime ? $"[{DateTime.Now:HH:mm:ss}] [帧:{Time.frameCount}] [INFO] {content}" : content;

#if UNITY_EDITOR || DEVELOPMENT_BUILD
        Debug.LogWarning(logContent);
#else
        Console.WriteLine(logContent);
#endif

        if (I.enableFileWrite)
        {
            I.WriteLogToFile(logContent);
        }
    } 
    #endregion

    #region 私有文件写入方法
    /// <summary>
    /// 写入日志到文件（异步写入，避免阻塞主线程）
    /// </summary>
    /// <param name="content">日志内容</param>
    /// <param name="addNewLine">是否自动换行</param>
    private void WriteLogToFile(string content, bool addNewLine = true)
    {
        try
        {
            // 异步写入，避免卡顿游戏主线程
            if (addNewLine) content += Environment.NewLine;

            // 追加模式写入（不会覆盖已有内容）
            File.AppendAllTextAsync(_logFilePath, content, Encoding.UTF8);
        }
        catch (Exception e)
        {
            // 日志写入失败时，降级输出到控制台（仅编辑器/开发包）
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogError($"日志写入失败：{e.Message}");
#endif
        }
    }

    /// <summary>
    /// 获取当前日志文件路径（方便调试/查看）
    /// </summary>
    public static string GetLogFilePath()
    {
        return I._logFilePath;
    }

    /// <summary>
    /// 获取log文件夹路径（新增：方便外部查看文件夹位置）
    /// </summary>
    public static string GetLogFolderPath()
    {
        return Path.Combine(Application.persistentDataPath, I._logFolderName);
    }
    #endregion

    /// <summary>
    /// 游戏退出时写入日志尾
    /// </summary>
    private void OnApplicationQuit()
    {
        WriteLogToFile($"===== 游戏日志结束 [{DateTime.Now:yyyy-MM-dd HH:mm:ss}] =====", true);
    }
}