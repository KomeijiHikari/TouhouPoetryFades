using System;
using System.Collections;
using System.Collections.Generic;
using Trisibo;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class UI_Start : MonoBehaviour
{

    List<AsyncOperation> 等待 = new List<AsyncOperation>();
    public List<GameObject> 开始界面的所有=new List<GameObject> ();
    public List<SceneField> 要加载的场景=new List<SceneField> ();
    public Text_button 继续按钮;
    public GameObject 子菜单;


    private void Start()
    {

        if (继续按钮 != null)
        {
            if (Save_D .Load()==null )
            {
                继续按钮.interactable = false;
            }
        }    
    }
    public void Q()
    {
        Debug.LogError("1");
    }
 public void 删档()
    {
        Save_static.删除所有();
    }
    public void 重新开始()
    {
 
         

        for (int i = 0; i < 要加载的场景.Count; i++)
        {
            等待.Add(SceneManager.LoadSceneAsync(要加载的场景[i].BuildIndex, LoadSceneMode.Additive));
 
        } 
        StartCoroutine(加载());

    } 
    public void 退出游戏()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    public void 展开子菜单设置( )
    {
 
        子菜单.SetActive(true);
 
    }
 
    public void 继续()
    {
 

        //关闭当前场景();

        等待.Add(SceneManager.LoadSceneAsync(要加载的场景[0].Name, LoadSceneMode.Additive));

        var a = Save_static.LoadinText<SaveData.txt>(Save_static.text).最后保存的场景的名字;
        等待.Add(SceneManager.LoadSceneAsync(a, LoadSceneMode.Additive));

        StartCoroutine(加载());


    }

    private void 关闭当前场景()
    {
 
        foreach (var item in 开始界面的所有)
        {
            item.SetActive(false);
        }
    }

 IEnumerator 卸载()
    {
        yield return 3f;
        Debug.LogError("时间到");
        SceneManager.UnloadSceneAsync(gameObject.scene.buildIndex);
    }
    IEnumerator 加载()
    { 
        for (int i = 0; i < 等待.Count; i++)
        {
            等待[i].allowSceneActivation = false;
        } 
        for (int i = 0; i < 等待.Count; i++)
        { 
            yield return 等待[i].isDone; 
        } 
        foreach (var item in 等待)
        {
            item.allowSceneActivation = true;
        } 

        SceneManager.sceneLoaded += (Scene sc, LoadSceneMode loadSceneMode) =>
        {
            if (sc.buildIndex== 要加载的场景[1].BuildIndex)
            {
                //SceneManager.SetActiveScene(sc ); 
            } 
        };
        加载_ = true;
 

    }
    [SerializeField]
    [DisplayOnly]
    bool 加载_;

    private void Update()
    {
        if (加载_)
        { 
            //关闭当前场景();
            SceneManager .UnloadSceneAsync(gameObject.scene); 

        }


    }



}
