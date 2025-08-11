using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 粒子系统 : MonoBehaviour
{[SerializeField ][DisplayOnly]
    ParticleSystem lizi;
    private void Awake()
    {

        lizi = GetComponent<ParticleSystem>();
      
        if (lizi==null)
        {
            Debug.LogError("没有粒子组件");
        }
 
    } 
    private void Update()
    {
        var a = lizi.main; 
    }
    private void Start()
    {

        var e=   lizi.emission;
        e.enabled = false;
        最初数量 = 数量;
        Initialize_Mono.I.Waite(() => e.enabled = true); 
    }

    Vector2 喷射方向_;
    float 数量_;
    float 最初数量 ;
    public float 数量 { get {
            var e = lizi.emission;
            var B = e.GetBurst(0);
            return B.count.constant ; }
        set
        {
            var e = lizi.emission.GetBurst(0); 
            e.count =  value;
     

            数量_ = value;
        }
    }
    /// <summary>
    /// 仅限四个方向
    /// </summary>
    public    Vector2 喷射方向
    {
        get { return 喷射方向_; }
        set
        {
            var sh = lizi.shape;
            sh.arc = 90;
            float ro=0;
           if (value == Vector2.up)
            {
                ro = 45;
            } 
            else if (value == Vector2.left)
            {
                ro = 45+90;
            }
            else if (value == Vector2.down)
            {
                ro = 45 + 90 + 90;
            }
            else if (value == Vector2.right)
            {
                ro = 45 + 90 + 90 + 90;
            }
            else
            {
                sh.arc = 360;
            }
                sh.rotation = new Vector3(0, 0, ro);
            喷射方向_ = value;
        }
    } 
    public void restore()
    {
        数量 = 最初数量;
           喷射方向 = Vector2.zero;
    }
    public void Play()
    { 
        lizi.Play(); 
    } 
}


