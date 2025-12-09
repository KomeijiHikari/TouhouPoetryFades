using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class 在小地图上另外显示  : MonoBehaviour 
{
    [DisplayOnly]
    [SerializeField]
    protected SpriteRenderer sp;
    public Sprite 小地图显示的啥样;
    [NoSaveDuringPlay]
    [DisplayOnly]
      SpriteRenderer 小弟sp;
    //[DisplayOnly]
    //public int 编号 = -9999;
    [SerializeField]
    Material m;
    public SpriteRenderer Get_小弟()
    {
        return 小弟sp;
    }
    protected virtual void 召唤小弟()
    {
        小弟sp.sortingLayerID = sp.sortingLayerID;
        小弟sp.sortingOrder = sp.sortingOrder;
        小弟sp.drawMode = SpriteDrawMode.Sliced;
        小弟sp.size = new(transform.localScale.x, transform.localScale.y);
  
        小弟sp.material = Instantiate(m);



        if (sp.enabled)
        {
            sp.enabled = false;
        }
    }
    public static readonly string 小地图layer名字 = "小地图";

    private void Start()
    {
        var 编号 = GetComponent<相机框>().编号;
        小弟sp.gameObject.AddComponent<小地图显示>().初始化(编号);
    }

    public GameObject 我的小地图 ;
    protected void Awake()
    {
        sp = GetComponent<SpriteRenderer>();

        GameObject a = new GameObject("小地图显示");
        a.gameObject.transform.SetParent(gameObject .transform);
        a.gameObject.transform.SetParent( null );
        //Debug.LogError(a.scene.buildIndex +"        "+ gameObject.scene.buildIndex);
        a.layer = gameObject.layer;
        小弟sp = a.AddComponent<SpriteRenderer>();


        小弟sp.sprite = 小地图显示的啥样;

        小弟sp.material = sp.material;
        小弟sp.transform.position = transform.position;

#if UNITY_EDITOR
        UnityEditor.SceneVisibilityManager.instance.Hide(小弟sp.gameObject, false);
#endif

        召唤小弟();
        我的小地图 = a;
    }
}
