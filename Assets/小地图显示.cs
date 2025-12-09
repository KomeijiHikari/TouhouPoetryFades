using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleFSM;
using DG.Tweening;
using Ink.Parsed;
public class 小地图显示 : MonoBehaviour
{
    
   public  bool Deb = false;
    public state 当前1 { get => 当前; private set => 当前 = value; }

   [DisplayOnly ] [SerializeField]state 当前;
    public state 不显示 { get => 不显示1; private set => 不显示1 = value; }
    public state 半透明 { get => 半透明1; private set => 半透明1 = value; }
    public state 全显示 { get => 全显示1; private set => 全显示1 = value; }
    public state 闪烁 { get => 闪烁1; private set => 闪烁1 = value; }
    //相机框 K;
    //在小地图上另外显示  m;
    [SerializeField]
    [DisplayOnly]
    int 编号;

    SpriteRenderer 小弟sp;
    public bool 显示否
    {
        get
        {
            return 小弟sp.enabled;
        }
        set
        {
            小弟sp.enabled = value;
        }
    }
    static Color 全 = Color.white;
    static Color 半 = new Color(1, 1, 1, 0.5f);

    //[DisplayOnly]
    public bool 有东西;

    DG.Tweening.Sequence quence;
    private void OnDestroy()
    {
         I = null;
    }
    public void 初始化(int i)
    {
        编号 = i;
    }
    public static string    机关刷新小地图 { get => "机关刷新小地图"; } 
    private void Awake()
    {


        小弟sp = GetComponent<SpriteRenderer>();
        Initialize_Mono.I.小地图刷新 += 机关刷新;
        Initialize_Mono.I.重制触发 += 进入框框;
        Event_M.I.Add(机关刷新小地图, 机关刷新);

        当前1 = new state("Null");
        全显示 = new state("全显示");
        半透明 = new state("半透明");
        不显示 = new state("不显示");
        闪烁 = new state("闪烁", 半透明);

        不显示.Enter += () =>
        {
            State_Action?.Invoke(不显示.StateName);
            显示否 = false;
        };

        闪烁.Enter += () =>
        {
            quence.Restart();
        };

        闪烁.Exite += () =>
        {
            quence.Pause();
        };

        半透明.Stay += () =>
        {
            if (有东西) 当前 = 当前.to_state(闪烁);
        };

        半透明.Enter += () =>
        {
 
            State_Action?.Invoke(半透明.StateName);

            显示否 = true;
            小弟sp.color = 半;
        };

        半透明.Exite += () =>
        {
            //if (有东西) quence.Pause();
            小弟sp.material.SetColor(材质管理._SpriteColor, 透明);
        };

        全显示.Enter += () =>
        { 
            State_Action?.Invoke(全显示.StateName);
            显示否 = true;
            小弟sp.color = 全;
            if (!激活过没有) I.add(my);
        };

        当前1 = 当前1.to_state(不显示);
    }
    public Action<string> State_Action;
    Color 透明;
    private void Update()
    {
        if (当前 != null) 当前.Stay?.Invoke();
    }
    private void Start()
    {
        透明 = new Color(Initialize_Mono.I.搜集物品指示颜色.r, Initialize_Mono.I.搜集物品指示颜色.g, Initialize_Mono.I.搜集物品指示颜色.b, 0f);
        quence = DOTween.Sequence();
        quence.Append(小弟sp.material.DOColor(Initialize_Mono.I.搜集物品指示颜色, 材质管理._SpriteColor, 0.5f));
        quence.AppendInterval(0.2f);
        quence.Append(小弟sp.material.DOColor(透明, 材质管理._SpriteColor, 0.5f));
        quence.SetLoops(-1, LoopType.Yoyo);
        quence.Pause().SetAutoKill(false);


        if (I.have(my))
        {
            激活过没有 = true;
        }


        当前?.Enter();
    }
 
    void 机关刷新()
    {
        if (Deb)
        {
            Debug.LogError("机关刷新 小地图显示 小地图显示 小地图显示 小地图显示 ");
            Debug.LogError("机关刷新 小地图显示 小地图显示 小地图显示 小地图显示 ");
        }

        bool 有我 = 激活过没有 || I.have(my);
        if (当前!=全显示) 
        if (有我)/// 全显示表示我也有  那我也半透明
        {
            激活过没有 = true;
            当前1 = 当前1.to_state(半透明);
            //状态_ = 显示状态.半透明;
        }
    } 
    private void 进入框框(int arg1, int arg2)
    {

        Vector2Int value = new Vector2Int(arg1, arg2);

        //Debug.LogError(value + "  " + my);
        bool 是我 = my == value;
        bool 有我 = 激活过没有 || I.have(my);
        if (是我)
        {
            当前1 = 当前1.to_state(全显示);
            //状态_ = 显示状态.全显示;
        }
        else
        {
            if (有我)
            {
                激活过没有 = true;
                当前1 = 当前1.to_state(半透明);
                //状态_ = 显示状态.半透明;
            }
            else
            {
                当前1 = 当前1.to_state(不显示);
                //状态_ = 显示状态.不显示;
            } 
        } 
    }
    Vector2Int my
    {
        get
        {
            return new Vector2Int(gameObject.scene.buildIndex, 编号);
        }
    }

    public static 小地图数据 I
    {
        get
        {
            if (小地图数据.I_ == null)
            {
                小地图数据.I_ = new 小地图数据();
            }
            return 小地图数据.I_;
        }
        set
        {
            小地图数据.I_ = value;
        }
    }



    public bool 激活过没有 = false;
    [DisplayOnly] state 不显示1;
    [DisplayOnly] state 半透明1;
    [DisplayOnly] state 闪烁1;
    [DisplayOnly] state 全显示1;

    public class 小地图数据 : I_Save
    {
        No_Re N { get; set; } = new No_Re();
        public static 小地图数据 I_ = new 小地图数据();
        public void add(Vector2Int v)
        {
            if (!N.Note_Re()) return;
            if (List.Contains(v)) return;
            else List.Add(v);
            保存();
        }
        public bool have(Vector2Int v)
        {
            for (int i = 0; i < I.List.Count; i++)
            {
                var V = I.List[i];
                var value = v;
                if (value == V)
                {
                    return true;
                }
            }
            return false;
        }
        [SerializeField]
        public List<Vector2Int> List = new List<Vector2Int>();
        public string Name => "小地图激活";
        public void 保存()
        {
            Save_static.SaveinText(Name, this);
        }
        public void 读取()
        {
            var a = Save_static.LoadinText<小地图数据>(Name);
            if (a == null)
            {
                I_ = new 小地图数据();
                保存();
            }
            else
            {
                I_ = a;
            }
        }
    }
    public enum 宝贝显示状态
    {
  不显示,
  显示
    }
    public enum 显示状态
    {
    不知道, ///不显示
    未探索,/// 显示，不显示通道出入口
        探索过,///  显示通道出入口  不明显
        是我,///   明显
    }
}



//switch (状态_)
//{
//    case 显示状态.半透明:
//         显示否 = true;
//         小弟sp.color = 半;
//        break;
//    case 显示状态.全显示:
//        显示否 = true;
//         小弟sp.color = 全;
//        if (!激活过没有) I.add(my); 
//        break; 
//    case 显示状态.不显示:
//        显示否 = false ;
//        break;
//}

//public  显示状态 状态_;
