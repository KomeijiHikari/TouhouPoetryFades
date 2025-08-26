using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleFSM;
using BehaviorDesigner.Runtime;
using UnityEditor.Timeline;
using UnityEngine.Playables;
using System;
using Sirenix.OdinInspector;
public class Timeline管理 : MonoBehaviour
{
    public List<GameObject> Objs;
    [SerializeField]
    List<PlayableAsset> PA;
 public   PlayableDirector PD;

    [Button("播放速度", ButtonSizes.Large)]
    public void 播放速度(float f)
    {
        PD.playableGraph.GetRootPlayable(0).SetSpeed(f);
    }
    [Button("Play", ButtonSizes.Large)]
    public void Play(string  name)
    {
        Debug.LogError(name);
        foreach (var item in PA)
        {
            if (item .name ==name)
            {
                PD.Play(item);
                return;
            }
        }
        Debug.LogError("没找到");
    }
    private void Awake()
    {
        PD = GetComponent<PlayableDirector>();
        PD.paused += paused;
        PD.stopped  += stopped;
        PD.played+= played;

        //PD.playableAsset=
        //PD .playableGraph.GetRootPlayable(0).SetSpeed(2.5f);
    }
    public Action stopp;
    public Action pause;
    private void played(PlayableDirector obj)
    {
        //Debug.LogError("    private void played(PlayableDirector obj)");
    }

    private void stopped(PlayableDirector obj)
    {
        foreach (var item in Objs)
        {
            item.SetActive(false );
        }
        stopp?.Invoke();
    }

    private void paused(PlayableDirector obj)
    {
        pause?.Invoke();
    }

public  void 停止播放()
    { 
        PD.Stop();
    }
}
