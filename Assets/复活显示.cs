using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class 复活显示 : MonoBehaviour
{
    [SerializeField] 生命周期管理 s;
    [SerializeField] SpriteRenderer sp;

    Bounds BB;
    ParticleSystem lizi;

    SpriteRenderer 白块;

    [DisplayOnly]
    [SerializeField]
    bool 开;
    void baikkk(bool b)
    {
        if (b)
        {
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
            白块.color=new Color(1, 1, 1, s.复活进度);

        }
    }
    private void Start()
    {
        if (s.R.Re_Time==0)
        {
            Debug.LogError("  啊？？？  " + gameObject.name+"    "+transform .position);
            return;
        }
 
        BB = s.R.盒子;
        s.效果_死亡Enter += () => {
            baikkk(true);
        };
        s.效果_活动Enter += () => {
            baikkk(false); 
        };


        //var a = lizi.shape;
        //a.shapeType= ParticleSystemShapeType.Box;
        //a.position = b.center;
        //a.scale= b.size;
    }
}
