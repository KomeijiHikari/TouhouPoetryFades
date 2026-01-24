using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Key阿三大苏打 : MonoBehaviour
{
    [Header("按键序列")]
    [Tooltip("玩家需要按下的三个按键")]
    public KeyCode[] keySequence = new KeyCode[3] { KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow };

    [Header("触发事件")]
    public UnityEvent onSequenceCompleted;

    [Header("设置")]
    [Tooltip("两次按键之间的最大时间间隔（秒）")]
    public float maxTimeBetweenKeys = 1.0f;
    [Tooltip("重置时是否清除已按下的按键")]
    public bool clearOnReset = true;
    [Tooltip("是否启用调试信息")]
    public bool debugMode = false;

    private List<KeyCode> pressedKeys = new List<KeyCode>();
    private float lastKeyPressTime;

    void Update()
    {
        // 检查超时重置
        if (pressedKeys.Count > 0 && Time.time - lastKeyPressTime > maxTimeBetweenKeys)
        {
            if (debugMode) Debug.Log($"按键序列超时，已重置");
            ResetSequence();
            return;
        }

        // 监听所有按键输入
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                ProcessKeyPress(key);
                break; // 每帧只处理一个按键
            }
        }
    }

    void ProcessKeyPress(KeyCode key)
    {
        // 记录按键时间
        lastKeyPressTime = Time.time;

        // 检查是否按对了序列中的下一个键
        if (key == keySequence[pressedKeys.Count])
        {
            pressedKeys.Add(key);

            if (debugMode) Debug.Log($"按对了第{pressedKeys.Count}个键: {key}");

            // 检查是否完成了整个序列
            if (pressedKeys.Count == keySequence.Length)
            {
                if (debugMode) Debug.Log($"按键序列完成！触发事件");
                onSequenceCompleted?.Invoke();
                ResetSequence();
            }
        }
        else
        {
            if (debugMode) Debug.Log($"按错了键: {key}，期望: {keySequence[pressedKeys.Count]}，重置序列");
            ResetSequence();

            // 如果按错的是第一个键，可以立即开始新的序列
            if (key == keySequence[0])
            {
                pressedKeys.Add(key);
                if (debugMode) Debug.Log($"重新开始，按对了第1个键: {key}");
            }
        }
    }

    void ResetSequence()
    {
        if (clearOnReset)
        {
            pressedKeys.Clear();
        }
        else
        {
            // 不清空，但检查是否有部分匹配
            // 这里可以添加更复杂的逻辑，比如检查是否与序列开头部分匹配
        }
    }

    void OnEnable()
    {
        ResetSequence();
        lastKeyPressTime = Time.time;
        if (debugMode) Debug.Log($"按键监听器已启用，需要按下的序列: {string.Join(" → ", keySequence)}");
    }

    void OnDisable()
    {
        ResetSequence();
        if (debugMode) Debug.Log("按键监听器已禁用");
    }

    /// <summary>
    /// 手动设置新的按键序列
    /// </summary>
    public void SetKeySequence(KeyCode key1, KeyCode key2, KeyCode key3)
    {
        keySequence = new KeyCode[3] { key1, key2, key3 };
        ResetSequence();
    }

    /// <summary>
    /// 获取当前已按下的按键数量
    /// </summary>
    public int GetPressedKeyCount()
    {
        return pressedKeys.Count;
    }

    /// <summary>
    /// 获取按键序列的字符串表示
    /// </summary>
    public string GetKeySequenceString()
    {
        return string.Join(" → ", keySequence);
    }
}