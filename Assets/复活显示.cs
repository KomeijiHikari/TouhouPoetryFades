using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class 复活显示 : MonoBehaviour
{
    public bool 粒子效果; 
    [SerializeField] 生命周期管理 s;
    [SerializeField] SpriteRenderer sp;

    Bounds BB; 

   [SerializeField][DisplayOnly] SpriteRenderer 白块;

    [DisplayOnly]
    [SerializeField]
    bool 开;
    void baikkk(bool b)
    {
        if (b)
        {
            Play();
            if (白块 == null)
            {
                白块 = Surp_Pool.I.GetPool(Surp_Pool.白块).GetComponent<SpriteRenderer>();
                白块.transform.position = transform.position;
                白块.transform.localScale= BB.size;
            }
            else
            {
                白块.gameObject.SetActive(true);
            }
        }
        else
        {
            if (白块 != null)
            {
                白块.gameObject.SetActive(false);
            } 
        }
        开 = b;
    }
    private void FixedUpdate()
    {
 
        if (开)
        {
            float lerp = Mathf.Pow(s.复活进度,1/2.2f);

            白块.color=new Color(1, 1, 1, lerp);

        }
    }
    private void Start()
    {
        if (s==null)   s = GetComponent<生命周期管理>(); 
        if (sp==null)    sp = GetComponent<SpriteRenderer>();
 
        if (s.R.Re_Time==0&&s.R.Re)
        {
            Debug.LogError("  啊？？？  " + gameObject.name+"    "+transform .position);
            return;
        }
 
        BB = s.R.盒子;

        s.效果_死亡Enter += () => {
       //if(Deb)     Debug.LogError("  s.效果_死亡Enter ");
            baikkk(true);
        };
        s.效果_活动Enter += () => {
            //if (Deb) Debug.LogError("  s.效果_活动Enter ");
            baikkk(false); 
        };

        if (粒子效果)
            Initialize_Mono.I.Waite(() => {
                var v = BB.阵列盒子();
                for (int i = 0; i < v.Count; i++)
                {
                    var a = v[i];
                    var obj = Surp_Pool.I.GetPool("机关重生粒子");
                    //obj.transform.SetParent(transform);
                    obj.transform.position = a;

                    var P = obj.GetComponent<ParticleSystem>();
                    LIzijiguan.Add(P);

                }

            });  
    } 
    List<ParticleSystem> LIzijiguan=new List<ParticleSystem>();

    public bool 真实时间 = false;
    public bool Deb;

    void Play()
    {
        for (int i = 0; i < LIzijiguan.Count; i++)
        {
            var a = LIzijiguan[i];
            var m = a.main;
            m.startLifetime = Player3.Public_Const_Speed * s.R.Re_Time ;
            m.startSpeed=3*(1/ Player3.Public_Const_Speed);
            if (真实时间)
            {
                m.startLifetime=  s.R.Re_Time;
                m.startSpeed = 3 ;
            }
            a.Play();
        }
    }
}
