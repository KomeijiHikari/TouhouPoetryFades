using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class 跨场景 : MonoBehaviour
{
    //[DisplayOnly]
    //[SerializeField]private string 当前场景1;
    [DisplayOnly]
    [SerializeField] private string 上一个场景1;
    [DisplayOnly]
    [SerializeField] private string 上上个场景1;

    [DisplayOnly]
    [SerializeField] private string 当当前场景a;
    public static 跨场景 I { get; private set; }
    static int intt;

    public Action<string> 从传送门加载场景;
    public Action 新加载了一个场景 { get; set; }

    [DisplayOnly]
    [SerializeField]
    private static Vector2 下个场景的出生点1;
    public Vector2 下个场景的出生点显示_只显示;
    public Vector2 下个场景的出生点
    {
        get
        {
            下个场景的出生点显示_只显示 = 下个场景的出生点1;
            return 下个场景的出生点1;
        }

        private set
        {

            Debug.LogError(下个场景的出生点1 + "GGGGGGGGGGGG" + value);

            下个场景的出生点1 = value;
        }
    }
    [SerializeField]
    bool 场景切换消息;

    //public string 当前场景 { get => 当前场景1; set {
    //        if (当前场景1!=value)
    //        {
    //            当前场景1 = value;
    //            Debug.LogError("运行第" + intt++ + "次");
    //            场景切换消息列表.Add(当前场景1);
    //        }
    //    } }
    public string 上一个场景 { get => 上一个场景1; set {
            if (上一个场景1 != value)
            {
                上上个场景 = 上一个场景1;
                上一个场景1 = value;
            }
        } }
    public string 上上个场景 { get => 上上个场景1; set {
            if (上上个场景1 != value)
            {
                上上个场景1 = value;
            }
        } }

    public string 当当前场景 { get => 当当前场景a; set {
            if (当当前场景a != value)
            {
                新场景加载了();
                当当前场景a = value;
            }
        } }



    void 新场景加载了()
    {
        新加载了一个场景?.Invoke();
    }
    private void FixedUpdate()
    {
        //一次 = 0;
        //一次_ =0;
        当当前场景 = SceneManager.GetActiveScene().name;
    }
    private void Start()
    {
#if   UNITY_EDITOR
        EditorBuildSettingsScene[] 所有场景列表 = EditorBuildSettings.scenes; 
        foreach (var item in 所有场景列表)
        {
            var A = SceneManager.GetSceneByPath(item.path); 
        }
#endif
        if (I != null)
        {
            //已经有实例

            Destroy(gameObject);
            Debug.LogError(下个场景的出生点);
        }
        else
        {
            I = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }

        从传送门加载场景 += 这才是加载场景;
        //SceneManager.activeSceneChanged += 切换111;
        //SceneManager.sceneLoaded += 加载333;
    }
    private void 这才是加载场景(string obj)
    {
 
    }

    
    void Update()
    {
        当当前场景 = SceneManager.GetActiveScene().name;
    }


    #region MyRegion
    static int 一次_;
    static int 一次;
    private void 切换111_(Scene arg0, Scene arg1)
    {
        //var a = arg1.GetRootGameObjects();
        //foreach (var item in a)
        //{
        //    if (item.name == "传送门")
        //    {
        //        var Q = item.GetComponent<切换场景>();
        //        if (Q != null)
        //        {
        //            Debug.LogError("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA " + 跨场景.I.上一个场景 + Q.出生的玩家点);
        //            if (上一个场景 == null)
        //            {
        //                Debug.LogError("你UNITY的妈妈炸了                   " + 上一个场景);
        //            }
        //            else if (Q.Next_name == 跨场景.I.上一个场景)
        //            {


        //                下个场景的出生点 = Q.出生的玩家点.position;

        //                Debug.LogError("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBAAAAAA " + Q.出生的玩家点.position + "!!" + 下个场景的出生点);
        //            }
        //            else
        //            {
        //                Debug.LogError("CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC  " + 跨场景.I.上一个场景 + Q.Next_name);
        //            }
        //        }
        //    }
        //}

        //if (场景切换消息)
        //{
        //    Debug.Log("    改变         " + arg0.name + "                         " + arg1.name + 一次);
        //}
    }
    private void 切换111(Scene arg0, Scene arg1)
    {//会被调用两次
        SceneManager.activeSceneChanged -= 切换111;
        if (一次 == 0)
        {
            一次++;
            切换111_(arg0, arg1);
        }





    }


    GameObject Player_;
    //private void 加载333_(Scene arg0, LoadSceneMode arg1)
    //{
    //    var a = arg0.GetRootGameObjects();

    //    foreach (var item in a)
    //    {
    //        if (item.tag == "Player")
    //        {
    //            if (item.GetComponent<真假Player>() == null)
    //            {
    //                Destroy(item);
    //            }
    //        }

    //        //if (item.GetComponent<摄像机>() != null)
    //        //{ 
    //        //    var C = item.GetComponent<摄像机>(); 
    //        //    C.找到玩家();
    //        //}
    //    }

    //    if (场景切换消息)
    //    {
    //        Debug.Log(arg0.name + "  加载       " + arg1.ToString() + 一次_);
    //    }
    //}
    //private void 加载333(Scene arg0, LoadSceneMode arg1)
    //{

    //    SceneManager.sceneLoaded -= 加载333;
    //    Debug.LogError(arg0.path);
    //    var a = SceneManager.GetSceneByPath("Assets / Scenes / a.unity");
    //    Debug.LogError(a.name + "操操操");
    //    if (一次_ == 0)
    //    {
    //        一次_++;

    //        加载333_(arg0, arg1);
    //    }

    //}
    #endregion
}
