
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInBuild : Editor
{
    private static readonly string scenePath = "Scenes";

    [MenuItem("Tools/BuildMainScene")]
    static void RefreshAllScene()
    {
        string path = Path.Combine(Application.dataPath, scenePath);
        string[] files = Directory.GetFiles(path, "*.unity", SearchOption.AllDirectories);
        EditorBuildSettingsScene[] scenes = new EditorBuildSettingsScene[files.Length];
        for (int i = 0; i < files.Length; ++i)
        {
            int index = files[i].IndexOf("Assets");
            string _path = files[i].Remove(0, index);
            //Debug.LogError(_path);
            scenes[i] = new EditorBuildSettingsScene(_path, true);
        }
        EditorBuildSettings.scenes = scenes;
    }

    static string 反替换(string a)
    {
        var c = a.Replace( "/","\\");
        return c;
    }
    static string   替换(string   a)
    {
        var c = a.Replace("\\","/");
        return c;
    }

    [MenuItem("Tools/LoadScene")]
    static void LoadScene()
    {
        
        string path = Path.Combine(Application.dataPath, scenePath); 
        string[] files = Directory.GetFiles(path, "*.unity", SearchOption.AllDirectories);

        string 加载 = "_";
        foreach (var item in files)
        {
            int index = item.IndexOf("Assets");
            string _path = item.Remove(0, index); 
 
  if (EditorSceneManager.GetSceneByPath(替换(_path)).name != null) continue; //表示这个场景已经加载了   就跳转
            if (!_path.Contains(加载)) continue;//没有_    就跳转

                EditorSceneManager.OpenScene(_path, OpenSceneMode.Additive);
          
        } 
    }
    [MenuItem("Tools/UnLoadScene")]
    static void UnLoadScene()
    {
        Scene b = EditorSceneManager.GetActiveScene();
 
        string path = Path.Combine(Application.dataPath, scenePath);
        string[] files = Directory.GetFiles(path, "*.unity", SearchOption.AllDirectories);

        string 卸载= "_";
        foreach (var item in files)
        {
            int index = item.IndexOf("Assets");
            string _path = item.Remove(0, index); 
            //到这里的字符串是所有可以被加载的路径

            if (!_path.Contains(卸载)) continue;//没有_    就跳转
            //Debug .LogError(反替换(EditorSceneManager.GetActiveScene().path));
            if (_path.Contains(反替换(EditorSceneManager.GetActiveScene().path))) continue;//包含激活 场景的名字的也跳转

            Scene a = EditorSceneManager.GetSceneByPath(替换(_path));
            //Debug.LogError(a.name+"当前已经加载的场景");
            EditorSceneManager.SaveScene (a);
            EditorSceneManager.CloseScene(a, true); 
        }



    }

    [MenuItem("Tools/New")] 
    static void NewGameobj( )
    {
        new GameObject("a");
    }
    const string text = "text.ala";
    [ MenuItem("Tools/Delets   Player Save")]
    public static void Dele( )
    {
        PlayerPrefs.DeleteKey(text);
    }
}
 