using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class 对其网格 : MonoBehaviour
{ 
  public   Vector3Int 瓦片坐标;
    public Vector3Int 瓦片坐标2;
    SpriteRenderer sp;
    Bounds  B
    {
        get
        {
            return sp.bounds;
        }
    }
    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }
    void Start()
    { 
        transform.localScale = 
       new Vector3Int(
           (int)Mathf.Round(transform.localScale.x)
       , (int)Mathf.Round(transform.localScale.y)
       ,0);

        瓦片坐标 = 网格.I.T.WorldToCell(B.min);
        transform.position = 瓦片坐标 + B.size / 2;
    } 
    void Update()
    { 

    }
}
