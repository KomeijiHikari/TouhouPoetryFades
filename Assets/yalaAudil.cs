using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Audio;



public class yalaAudil : MonoBehaviour
{
    public bool 静音;
    public bool 引入外部;

    public bool Deb;
    public static    yalaAudil I { get; private set; }
    [SerializeField] AudioSource BGM;
    
    /// <summary>
    /// 多个通道 比如攻击和受击 是不同 通道
    /// </summary>
    public List<AudioSource>    音效;
    public List<int> 通道;
    public int 通道数=2;
 
    public void BGMPlay(string s )
    {
 
        //var ss = "Audio/BGM" + s;
        //BGM.clip = Resources.Load<AudioClip>(ss);


        BGM.clip = GetAudio(s);
        BGM.loop = true;
        if (!静音)     BGM.Play();
    }
    AudioSource get(int 通道i)
    {
        for (int i = 0; i < 通道.Count; i++)
        {
            var assss = 通道[i];
            if (assss == 通道i)
            {
                return 音效[i];
            }
        }
        var ass = gameObject.AddComponent<AudioSource>();
        ass.playOnAwake = false;
        ass.loop = false;
        音效.Add(ass);
        通道.Add(通道i);
        return ass;
    }
    [SerializeField]
    音乐数据 As;

    [SerializeField]
    List<AudioClip> Ass;
    [SerializeField] List<string> Strs;
    public  int    SetAudio(AudioClip Ac,string name)
    {
 
        if (Ac == null)
        {
            Debug.LogError("空");
            return -99;
        }
        for (int i = 0; i < Ass.Count; i++)
        {
            var a = Ass[i];
            if (a.name == name) 
            {
                Ass[i]= Ac;
                return i;
            }
        }
   Debug.LogError("没找到"+name);
        return -99;
    }
     
    public AudioClip GetAudio(string name)
    { 
        for (int i = 0; i < Strs.Count; i++)
        {
            var a = Strs[i];
            if (a== name)
            {
                return Ass[i];
            }
        }
        Debug.LogError("没找到  返回空"+name);
        return null;
    }
    public void EffectsPlay(string s   , int 通道i)
    { 
        AudioClip a = GetAudio(s);

        if (Deb) Debug.LogError(a.name+s);
        
        var ass = get(通道i);
        ass.clip = a;
        ass.playOnAwake = false;
        ass.loop = false;
        if (!静音)   ass.Play();
    }
 
    void Awake()
    {
        if (I != null && I != this) Destroy(this);
        else I = this;
        
      //var a= GetComponents<AudioSource>();
        for (int i = 0; i < 通道数; i++)
        {
          var ass=  gameObject.AddComponent<AudioSource>();
            音效.Add(ass );
            通道.Add(i);
        }
        Ass = new List<AudioClip>(As.As);
        for (int i = 0; i < Ass.Count; i++) Strs.Add(Ass[i].name);

        for (int i = 0; i < Strs.Count; i++)
        {
            var a = Strs[i];
            Lod(a);
        }

    }
    private void Start()
    {
        BGMPlay("BGMUP");
    }
    void Lod( string name)
    {
        string realPath = Get完整Name("E:\\BGM/"+name);
        StartCoroutine(LoadAudio(realPath, name));
    }
    string Get完整Name(string name)
    {
        // 获取目录
        string dir = Path.GetDirectoryName(name);
        string fileNameNoExt = Path.GetFileNameWithoutExtension(name);

        if (!Directory.Exists(dir))
        {
            Debug.LogError("目录不存在: " + dir);
            return name;
        }

        // 搜索所有文件
        var files = Directory.GetFiles(dir);
        foreach (var file in files)
        {
            string candidateName = Path.GetFileNameWithoutExtension(file);
            // 忽略大小写、只要包含或相等即可
            if (candidateName.Equals(fileNameNoExt, System.StringComparison.OrdinalIgnoreCase) ||
                candidateName.Contains(fileNameNoExt))
            {
                return file;
            }
        }
        Debug.LogError("未找到相似文件: " + name);
        return name;
    }
    [SerializeField]
    AudioType typpe;
    IEnumerator LoadAudio(string path,string name)
    {
        // 使用 file:/// 前缀指定本地文件路径
        UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file:///" + path, typpe);
        yield return www.SendWebRequest(); // 等待文件加载完成

        if (www.result == UnityWebRequest.Result.Success)
        {
            // 加载成功，获取AudioClipS
            AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
            if (clip!=null)
            {
                var a = SetAudio(clip, name);


                //Debug.LogError("音频加载 : " + a);
                //BGM.clip = GetAudio(name);
                //BGM.Play(); // 播放加载的音频
            }

        }
        else
        {
            Debug.LogError("音频加载失败: " + www.error);
        }
    }
    [SerializeField]
    AudioClip 接收;
}
