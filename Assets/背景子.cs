using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 背景子 : MonoBehaviour
{
 
    [Header("完全同步，不为0后无视差,滚动速度")]
    public Vector2  滚动速度;
    背景 b;
    [Header("同步速度")]
    public Vector2 视差速度=Vector2 .zero;
    Vector2 视差速度_
    {
        get { return Vector2.one - 视差速度; }
    }
    Vector2 My
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

  int 数量 { get; } = 3;
    SpriteRenderer spR;
    [SerializeField]
List <SpriteRenderer>      sps=new List<SpriteRenderer>();
    float Wide
    {
        get => spR.bounds.size.x-1/32f;
    } 
  public  void Start_()
    {
        spR = GetComponent<SpriteRenderer>();
        b = GetComponentInParent<背景>();
        var sp = GetComponentInParent<SpriteRenderer>();
       
        spR.enabled = false;
        int Z = 0;
        while (transform.childCount <  数量)
        {
            Z++;
            if (Z > 6) break;
            var a = new GameObject();
            a.transform.SetParent(transform);
            a.transform .localPosition=Vector3 .zero;
            var s = a.AddComponent<SpriteRenderer>();
            sps.Add(s);
            s.sortingLayerID = sp.sortingLayerID;
            s.sortingOrder = sp.sortingOrder;
            s.sprite = spR.sprite;

        }
        for (int i = 0; i < sps.Count; i++)
        {
            sps[i].transform.localPosition += new Vector3(i* Wide-         Wide*(数量  - 1)  / 2 , 0); 
        }
        //sps[0].transform.localPosition = new Vector3(-Wide, 0);
        //sps[1].transform.localPosition = new Vector3(0, 0);
        //sps[2].transform.localPosition = new Vector3(Wide, 0);
    }
   
    private void LateUpdate()
    {
        if (滚动速度!=Vector2 .zero)
        {
            transform.localPosition -= new Vector3(Time.deltaTime * 滚动速度.x * Player3.Public_Const_Speed, Time.deltaTime*滚动速度.y * Player3.Public_Const_Speed );
        }
        else
        {
            if (b.不同步&& 视差速度!=Vector2 .zero) transform.localPosition -= new Vector3(b.差值.x * 视差速度_.x, b.差值.y * 视差速度_.y); 
        }
        循环图();
    }


        void 循环图()
        {
            for (int i = 0; i < sps.Count; i++)
            {
                var s = sps[i];
                float 差值 = b.Flow.x - s.transform.position.x;
                if (Mathf.Abs(差值) > Wide * 1.5f)
                {
                    s.transform.localPosition += new Vector3(Mathf.Sign(差值) * Wide * 数量, 0);
                }
            }
        }

}
