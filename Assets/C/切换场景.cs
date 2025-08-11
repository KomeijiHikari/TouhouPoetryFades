using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement; 
using Trisibo;
 
public partial class 切换场景
{//小地图相关

    //召唤小弟，小弟是固定的
 
    protected  void 召唤小弟()
    { 
        //小弟sp.gameObject.layer = LayerMask.NameToLayer(在小地图上另外显示.小地图layer名字);

        //小弟sp.sortingLayerID = 0;
        //小弟sp.sortingOrder = 1;
        //if ((Mathf.Abs(transform.localScale.x)) >= 8)
        //{
        //    //如果是横向的
        //    小弟sp.gameObject.transform.Rotate(new Vector3 (0,0,90));
        //}
    }
}
public partial class 切换场景 :MonoBehaviour 
{
    public float 检测玩家的距离;
    public SceneField 废弃的;
    Collider2D po;
    //[DisplayOnly]
    //[SerializeField]
    //Vector2 门的朝向; 
    //public Transform 上点, 下点, 左点, 右点;
    [DisplayOnly]
    [SerializeField]
    Transform 已经废弃;
    public bool 上, 下, 左, 右;
    //public Transform 出生的玩家点; 
    public Action<GameObject> 丢玩家;
    protected void 前后和头(float 距离)
    {
        var down =
                   Physics2D.BoxCast(
        new Vector2(po.bounds.center.x, po.bounds.min.y),
         new Vector2(po.bounds.size.x - 0.5f, 0.1f),
         0f,
         Vector2.down,
          距离,
        1 << LayerMask.NameToLayer("Ground")
         )
         .collider;

        下 = down != null;


        var up =
        Physics2D.BoxCast(
       new Vector2(po.bounds.center.x, po.bounds.max.y),
        new Vector2(po.bounds.size.x - 0.5f, 1),
        0f,
        Vector2.up,
         距离,
       1 << LayerMask.NameToLayer("Ground")
        )
        .collider;
        上 = up != null;

        var left =
      Physics2D.BoxCast(
      new Vector2(po.bounds.min.x, po.bounds.center.y),
      new Vector2(1, po.bounds.size.y - 0.4f),
      0f,
    Vector2.left,
       距离,
     1 << LayerMask.NameToLayer("Ground")
      )
      .collider;
        左 = left != null;

        var right =
 Physics2D.BoxCast(
 new Vector2(po.bounds.max.x, po.bounds.center.y),
 new Vector2(1, po.bounds.size.y - 0.4f),
 0f,
       Vector2.right,
距离,
 1 << LayerMask.NameToLayer("Ground")
 )
 .collider;
        右 = right != null;

    }
    private   void Awake()
    {
 
        po = GetComponent<Collider2D>();
        //Initialize.RefreshAllScene();
    }

    public float 相对的角度;
    public bool Ground { get; private set; }
    public Transform 当前出生位置1 { get => 已经废弃; set => 已经废弃 = value; }
    //public PolygonCollider2D 相机碰撞框1 { get => 相机碰撞框; private set => 相机碰撞框 = value; }
    public bool 发现玩家了嘛1 { get => 发现玩家了嘛; set => 发现玩家了嘛 = value; }

    //[DisplayOnly]
    //[SerializeField]
    //PolygonCollider2D 相机碰撞框;
    private void Start()
    {
        if (检测玩家的距离 == 0)
        {
            检测玩家的距离 = 10;
        }

        ////获取当前场景的碰撞框引用 
        //相机碰撞框1 = Initialize.获取场景中的相机碰撞箱子(gameObject);

        Event_M.I.Add(Event_M.切换场景触发_obj ,活动场景切换 );
    }
    [DisplayOnly]
    [SerializeField]
    bool 发现玩家了嘛;
    bool 检测玩家(Vector2 方向, float 距离)
    {
        var a = Physics2D.BoxCastAll(
     po.bounds.center,
     /*new Vector2(po.bounds.size.x, 0.1f)*/po.bounds.size,
     0f,
   方向,
    距离,
    1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Ground"));
        Debug.DrawRay(transform.position, new Vector3(方向.x, 方向.y, 0) * 距离, Color.yellow);

 
        foreach (var item in a)
        {
            if (item.collider.gameObject.transform.tag=="Player" )
            {
                return true;
            }
        }
        return false;
    }

    public float 差;
    private void FixedUpdate()
    {
        差 = Mathf.Abs(transform.localScale.x);
        前后和头(0.5f);
        if ((Mathf.Abs(transform.localScale.x)) >= 8)
        {
            发现玩家了嘛1 = 检测玩家(Vector2.up, 检测玩家的距离)
          ||
          检测玩家(Vector2.down, 检测玩家的距离);
        }
        else
        {
            if (上 || 下)
            {
                发现玩家了嘛1 = 检测玩家(Vector2.right, 检测玩家的距离) ||

                   检测玩家(Vector2.left, 检测玩家的距离);
            }
            if (左 || 右)
            {
                // 上下有墙， 左右检测
                发现玩家了嘛1 = 检测玩家(Vector2.up, 检测玩家的距离)
                    ||
                    检测玩家(Vector2.down, 检测玩家的距离);
            }
        }

        if (发现玩家了嘛1)
        {

            unload();
            load();
        }
    }

    [SerializeField]
    List<SceneField> _loadS = new List<SceneField>();
    [SerializeField]
    List<SceneField> _UnloadS = new List<SceneField>();
 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            传送导点.I.ADD(this);
        }


        }
 
    public void 活动场景切换(GameObject  obj)
    {
        if (obj.scene != gameObject.scene) return;
        //if (gameObject.scene == SceneManager.GetActiveScene()) return;
        //var a = player.GetComponent<Rigidbody2D>();
        //if (a.velocity.y>1)
        //{
        //    a.velocity = (new Vector2(a.velocity.x * 1.5f,20));
        //}
        //else
        //{
        //    a.velocity = (new Vector2(a.velocity.x * 1.5f, a.velocity.y * 1.5f));
        //}

 
            //当进入的时候就把活动场景设定为当前场景 
            Initialize.设置和当前活动场景为这个obj的场景(gameObject);
 



        //当进入的时候就把相机设定为当前场景的碰撞箱
        //摄像机.I.设置相机碰撞体(相机碰撞框);
    }
    private void OnTriggerExit2D(Collider2D collision)
    { 
        if (collision .gameObject .tag=="Player")
        {
            传送导点.I.UnLoad (this);

        }
    } 
    void load()
    {//加载 
            for (int J= 0; J < _loadS.Count; J++)
        {
            bool ISLOAD = false;
            for (int i = 0; i <SceneManager .sceneCount ; i++)
            {
                Scene ASDASD= SceneManager.GetSceneAt(i);//已经加载的才会返回
                if (_loadS[J].Name== ASDASD.name )
                {
                    //如果已经被加载
                   ISLOAD = true;
                    break;
                }
            } 
            if (!ISLOAD )
            {
                SceneManager.LoadSceneAsync(_loadS[J].Name,LoadSceneMode.Additive);
            } 
        }
    }
    void unload()
    {//删除
        for (int J = 0; J < _UnloadS.Count; J++)
        { 
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene ASDASD = SceneManager.GetSceneAt(i);//已经加载的才会返回
                if (_UnloadS[J].Name == ASDASD.name)
                {
                    //如果已经被加载
                    SceneManager.UnloadSceneAsync(_UnloadS[J].Name ); 
                }
            }  
        }
    }


}
