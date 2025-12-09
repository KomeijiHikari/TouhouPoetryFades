using Boss;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using static Player3;

/// <summary>
/// 剧情控制 一个由小到大排列的 从最小的开始 排列顺序可以不是连续的 ，可以在？？之前之后插入一个？？
///  一个？？可以有N个子  子特征和？？ 一样
///  未来，子可以是平行，多个子完成 后，父才完成
///  
/// 因此？？ 状态有平行和顺序两种 由父类规定
/// 顺序？？可以是列表
/// 
/// ？？唯一寻找的就是父类和索引 ？？ 可以没有名字
/// 
/// 嵌套关系储存在
/// </summary>
/// 

namespace 流程控制
{
    public class 流程控制 : MonoBehaviour
    {
 
     public    List<提示管理> 提示List;

        [SerializeField]
        流程 L;
        [System.Serializable]
        public   class Nodee
        {
           public  void Inite()
            {
                int depth = 0;
                Nodee p = Parent;
                while (p != null)
                {
                    depth++;
                    p = p.Parent;
                }
                ParentIndex = depth;
                Next = GetNext();

                foreach (var item in Children)
                {
                    item.Inite();
                }
            }
             public int ParentIndex=-999;
            public int Next=-999;
              int GetNext()
            {
                for (int i = 0; i < Brothers.Count; i++)
                {
                    if (Brothers[i] == this)
                    {
                        if (i + 1 < Brothers.Count)
                        {
                            return Brothers[i + 1].Index;
                        }
               
                    }
                }
                return -999;
            }
            public  int Index;
            public string name;
            public List<Nodee> Brothers => Parent.Children; // 兄弟节点
            public Nodee Parent; // 父流程节点
            public List< Nodee> Children = new List< Nodee>(); // 子流程节

            Nodee() { }
            public  Nodee(int i)
            {
                Index = i;
            }
            public Nodee(int i,string s)
            {
                Index = i;
            }
            public void AddChild(Nodee child)
            {
                child.Parent = this;
                Children.Add(child);
            }

        }

        [System.Serializable]
        public   class 流程
        {
 
            [SerializeField]
            Nodee Base=new Nodee(-99,"Base");
            public void Inite()
            {
                Base.AddChild(new Nodee(0,"啥都没有"));
                 
                初始化位置();
            }
            void 初始化位置()
            {
                 foreach (var item in Base.Children)
                {
                    item.Inite();
                }
            }
        }
        public static 流程控制 I { get; private set; }
        public int Int;

        [SerializeField]
        Vector2Int 位置 = new Vector2Int(0, 0);
        //public  流程 流程状态;
        private void Awake()
        {
            if (I != null && I != this) Destroy(this);
            else I = this;
            L=new 流程();
            L.Inite();


            if (Int==0) 
           Int= Save_D.Load_Value_D<int>(Name);
            else
                Debug.LogError("流程控制Awake时  流程值大于0 可能是测试状态下强制设置的");
        }
        [SerializeField] GameObject 啥都没有;
        [SerializeField] GameObject 啥都没有1;

        [SerializeField] GameObject 战败;
        [SerializeField] Transform 出生位置;
        void 回退前进()
        {
            主UI.I.强提示.Action强提示_回退 -= 回退前进;
            Int++;
        }
        void 死亡回退前进()
        {
            Player3.I.生命归零 -= 回退前进;
            Int++;
        }  
        private void Update()
        {
            //if(Time.frameCount>2)
            if (Int == 0)
            {
                主UI.I.强提示.开(啥都没有);
                Int++;
                主UI.I.强提示.Action强提示_回退 += 回退前进;

                Player3.I.N_.全解锁();
                Player3.I.N_.时缓 = false;
            }
            else if (Int == 2)
            {
                主UI.I.强提示.开(啥都没有1);
                Int++;
                主UI.I.强提示.Action强提示_回退 += 回退前进;
            }
            else if (Int == 4)
            {

                Int++;
                Player3.I.生命归零 += 死亡回退前进;
                //主UI.I.强提示.Action强提示_回退 += 回退前进;
            }
            else if (Int == 6)
            {
                魔理沙.I.退场();
                主UI.I.强提示.开(战败);
                Player3.I.transform.position = 出生位置.position;
                Int++;
                主UI.I.强提示.Action强提示_回退 += 回退前进;
                 
                Player3.I.N_.全解锁(false);

                Player3.I.N_.上升攻击 = true;
                Player3.I.N_.Dash=true;
 
                Save_D.Add(Name, Int);
            }
            else if (Int == 8)
            {
                
            }


            }
        string Name => "流程";
    }

    public enum 流程
    {
        啥都没有,
        开始,
    }
}
