using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

 
public class 残影 : MonoBehaviour
{
    public ObjectPool<GameObject> pool { get; set; }
    public static 残影 I { get; private set; }
 GameObject Player;
    public bool B;
    [SerializeField]
    Color Color;
    [SerializeField ]
    public float 残留时间_ = 0.2f;
 public   float 残留时间 { get => 残留时间_; }
 
    private void Awake()
    {
        var a= GameObject.FindGameObjectsWithTag("Player");
        if (a.Length>=1)
        {
            Player = a[0];
        }

        if (I != null && I != this)
        {
            Destroy(this);
        }
        else
        {
            I = this;
        }

        pool = new ObjectPool<GameObject>(createFunc, actionOnGet, actionOnRelease, actionOnDestroy, true, 10, 1000);
    }
    private void Start()
    {
        Int = 0;
    }
    private void FixedUpdate()
    {
        if (B)
        {
            pool.Get();
            //StartCoroutine(asdasd());
        }
    }
    IEnumerator asdasd()
    {
        for (int i = 0;; i++)
        {
            yield return new WaitForSeconds(0.02f);
            pool.Get();
            if (!B)
            {
                break;
            }
        }

    }

    public  void 开启残影(bool b)
    {
        Debug.LogError(b+"开启残影 DASH                                                      AAAAAAAAAAAAAAAAAAA");
        B = b;
    }
    public void 开启残影(bool b,float time)
    {
        if (b)
        {
            B = b;
            Initialize_Mono.I.Waite(() => B = false ,time );
        } 
    }
 
    GameObject createFunc()
    {
        GameObject obj;
        obj = new GameObject("残影");
        obj.transform.parent = transform ;
        obj.AddComponent<SpriteRenderer>();
        obj.AddComponent<分身消失>();
        return obj;
    }
    private void actionOnGet(GameObject obj)
    {
        obj. gameObject . SetActive(true);

        obj.transform.position  = Player.transform.position;
        obj.transform.localScale = Player.transform.localScale;
        obj.transform.rotation = Player.transform.rotation;
        //obj.GetComponent<SpriteRenderer>().sprite = Player.GetComponent<SpriteRenderer>().sprite;
      var a=  obj.GetComponent<SpriteRenderer>();
        a.sprite= Player.GetComponent<SpriteRenderer>().sprite;
        a.color = Color;
        a.sortingLayerName = "残影";
        a.sortingOrder = Int++;
        //StartCoroutine(杀自己(obj));
    }
    public int Int;
        private void actionOnRelease(GameObject obj)
    {
        obj.gameObject.SetActive(false );
    }
    private void actionOnDestroy(GameObject obj)
    {
        Destroy(obj);
        Debug.LogWarning("删除了删除了");
    }

   
}
