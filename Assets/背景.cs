using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 背景 : MonoBehaviour
{/// <summary>
/// 初始横轴和摄像机同步，数轴和初始点
/// </summary>
    public Transform 地平线;
    [Header("近距离为0  远距离为1")]
    [SerializeField]
   List<Transform> ts=new List<Transform> ();
    public Vector2 速度 = Vector2.zero;
    Vector2 Speed
    {
        get { return Vector2.one - 速度; }
    }
    public  Vector2 Flow
    {
        get
        {
            return Camera.main.transform.position ;
        }
    }
    Vector2 MyPo
    {
        get
        {
            return transform.position;
        }
        set
        {
            transform.position = value;
        }
    }
    Vector2 Last;
    SpriteRenderer sp;
    public Vector2 差值 { get; set; }
 
public    bool 不同步;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        var sps = GetComponentsInChildren<SpriteRenderer>();
 
        for (int i = 0; i < sps.Length; i++)
        {
            var s = sps[i];
            s.sortingLayerID = sp.sortingLayerID;
            s.sortingOrder = sp.sortingOrder + i;
            //s.sprite = sps[0].sprite;
        }
        var zi = GetComponentsInChildren<背景子>();
        for (int i = 0; i < zi.Length; i++)
        {
            zi[i].Start_();
        }
 

        //不同步 = true;
        Initialize_Mono.I.Waite(() => { 
            不同步 = true;
            Last = MyPo;
        }, 0.1f);


    }
 
    private void Update()
    {

        if (不同步)
        {
            差值 = Flow - Last;


            MyPo = Flow;

            Last = MyPo;

            for (int i = 0; i <ts.Count ; i++)
            {
                var t = ts[i];
                t.localPosition -= new Vector3( 差值.x*Speed.x,差值.y * Speed.y);
            }
        }
        else
        {
            MyPo = new Vector2(Flow.x, 地平线.position.y);
        } 
    }

}
