using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class 按键方向 : SerializedMonoBehaviour
{
    [DisplayOnly] 
  public   按键 最后;
   public static 按键方向 I;
    static string N底 { get => "底"; }
    static string N图 { get => "图"; } 
    [System.Serializable]
  public   class 按键
    {
        public E_方向 我的 => v.v2_To方向();
        public bool 不响应;
        public bool 激活;
        public Vector2 v;
        public int i=99;
    public     Transform T;
        [SerializeField ]
   public   SpriteRenderer 底, 图;
     public   按键 (Transform t)
        { 
            T = t;
            foreach (Transform cl in T)
            {
                if (cl.name== N底)
                {
                    底 = cl.GetComponent<SpriteRenderer >();
                }
           else      if (cl.name == N图)
                {
                    图 = cl.GetComponent<SpriteRenderer>();
                }
            }
            if (底 == null && 图 == null) Debug.LogError("失败"); 
        }
    }
 
    SpriteRenderer sp;
    Bounds B
    {
        get
        {
            return sp.bounds;
        }
    }
    [SerializeField]
    Sprite 底, 上, 下, 左, 右, 左上, 左下, 右上, 右下;
    [SerializeField]
    Transform    上_, 下_, 左_, 右_, 左上_, 左下_, 右上_, 右下_;

    [SerializeField ][DisplayOnly]
  Dictionary<Transform , 按键>  字典=new Dictionary<Transform, 按键> ();

    //private void OnDisable()
    //{
    //    最后 = null;
    //} 
    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(this);
        }
        else
        {
            I = this;
        }

        sp = GetComponent<SpriteRenderer>();

        位置();
    }
   void 位置()
    {
        上_.position = B.九个点(E_方向.上);
        下_.position = B.九个点(E_方向.下);
        左_.position = B.九个点(E_方向.左);
        右_.position = B.九个点(E_方向.右);
        左上_.position = B.九个点(E_方向.左上);
        左下_.position = B.九个点(E_方向.左下);
        右上_.position = B.九个点(E_方向.右上);
        右下_.position = B.九个点(E_方向.右下);
    }
    public void ASD(Transform t ,int i)
    {
        var a = 字典[t];
            if(a==null )
        {
            Debug.LogError("空");
            return;
        }
        if (a.i == i)
        {
            if  (i ==1) 最后 = a;
            return;
        }
          
        if (i==-1)
        { 
                ///不显示
                t.gameObject.SetActive(false);
        }
        else if (i == 0)
        {
            ///没有选中
            if (a.激活&& !a.不响应 )
            {
                t.gameObject.SetActive(true);
                a.底.material.SetColor(材质管理._SpriteColor, Color.black * 0.5f);
                //a.底.material.SetColor(材质管理._EdgeColor, Color.white * 0.5f);
                //a.图.material.SetColor(材质管理._SpriteColor, Color.white * 0.5f);
            }
              

        }
        else  if (i==1)
        {
            ///按住
            ////////松开之后的
            if (a.激活 && !a.不响应)
            {
                t.gameObject.SetActive(true);
                a.底.material.SetColor(材质管理._SpriteColor, Color.black * 0);
                //a.底.material.SetColor(材质管理._EdgeColor, Color.black * 0);
                //a.图.material.SetColor(材质管理._SpriteColor, Color.black * 0);
                //StartCoroutine(Initialize_Mono.I.Q弹(Initialize_Mono.I.defaul_Curve, t, -1, 0.4f, false, true));
                var B = t.Q弹(-1,0.4f, false, true);
                最后 = a;
 
                StartCoroutine(B);
            } 
        }

        a.i = i;
    }
    SpriteRenderer 初始化(Transform t, Vector2 v, bool 封禁 = false )
    { 
        t.SetParent(transform );  
        字典.Add(t, new 按键(t));  
        字典[t].底.sprite = 底; 
        字典[t].v = v; 

        t.gameObject.SetActive(false ); 
        return 字典[t].图;
    }
    private void Start()
    {
        初始化(上_,Vector2 .up).sprite =上;
        初始化(下_, Vector2.down).sprite = 下;
        初始化(左_, Vector2.left ).sprite = 左;
        初始化(右_, Vector2.right).sprite = 右;
        初始化(左上_, new Vector2 (-1,1)  ).sprite = 左上;
        初始化(左下_, Vector2.one*-1 ).sprite = 左下;
        初始化(右上_, Vector2.one ).sprite = 右上;
        初始化(右下_, new Vector2( 1, -1) ).sprite = 右下;

        字典[左下_].不响应 = true;
        字典[左上_].不响应 = true;
        字典[右上_].不响应 = true;
        字典[右下_].不响应 = true;

        gameObject.SetActive(false);
    }
    [SerializeField ][DisplayOnly]
   Vector2 输入_;
 public Vector2 输入 { get => Player_input.I.输入; }

    private void OnEnable()
    {

        输入_ = Vector2.zero;
        最后 = null;
        III = 0;
        位置();
    }
    int III;
    private void Update()
    {
        输入_ = 输入;
        transform.position = Player3.I.transform.position;
        //if (III < 2) return;
        foreach (var item in 字典)
        {
            //if (item.Value.v.x == -Player3.I.LocalScaleX_Set) item.Value.激活 = false;
            //else item.Value.激活 = true;
            item.Value.激活 = true;
            int AS = 0;
            if (item.Value.v == 输入) AS = 1;
            if (item.Value.v.x==-Player3.I.LocalScaleX_Set) AS = -1;
            ASD(item.Key, AS);

        }
           

        //按到那个那个那个(); 
    }
    //void 按到那个那个那个()
    //{ 
    //    foreach (var item in 字典)
    //    {
    //        if (item.Value.v.x == -Player3.I.LocalScaleX_Set) item.Value.激活 = false;
    //        else item.Value.激活 = true;
    //        if (item.Value.v == 输入)
    //        {
    //            ASD(item.Key, 1);
    //        }
    //        else
    //        {
    //            if (item.Value.v.x == 输入.x && 输入.x != 0)
    //            {
    //                ASD(item.Key, 0);
    //            }
    //            else if (item.Value.v.y == 输入.y && 输入.y != 0)
    //            {
    //                ASD(item.Key, 0);
    //            }
    //            else
    //            {
    //                ASD(item.Key, -1);
    //            }
    //        }
    //    }
    //}
}
