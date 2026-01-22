using RenaissanceRestart;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
///
namespace ItemMager
{
    [Serializable]
    public class Itemvalue
    {
        public string key;
        public int 商店持有;
        public int 玩家持有;

        public Itemvalue(string key, int 商店持有, int 玩家持有)
        {
            this.key = key;
            this.商店持有 = 商店持有;
            this.玩家持有 = 玩家持有;
        }


    }
    [Serializable]
    public class CompleteItem
    {
        public string key;
        public int 商店持有
        {
            get { return Save_value.商店持有;   }
            set {    Save_value.商店持有=value;  }
        } 
        public int 玩家持有
        {
            get { return   Save_value.玩家持有; }
            set { Save_value.玩家持有 = value; }
        }
        public Sprite itemImage;//物体图片
        public int 物品价值; //物品价值 
        public string itemInfo;//物体描述 

        /// <summary>
        /// 理论上应该有两个，
        /// 被使用时候调用的方法
        /// 获取后调用的方法
        /// 
        /// 目前只有被使用时候调用的方法
        /// </summary>
        public Action 方法;

        public item item { get; private set; }
        public Itemvalue Save_value { get; private set; } 
        public void     SetSave_value(Itemvalue v)
        {
            Save_value = v;
        }
        public CompleteItem(item it, Itemvalue v, Action A)
        {
            item = it;
            Save_value = v;

            key = it.itemName;
            //商店持有 = v.商店持有;
            //玩家持有 = v.玩家持有;
            itemImage = it.itemImage;
            物品价值 = it.物品价值;
            itemInfo = it.itemInfo;
            方法 = A;
        }
    }

    public class 所有物品管理 : MonoBehaviour
{
    public bool Deb;
    public static 所有物品管理 I;
    /// <summary>
    /// 所有物品，分成玩家有的和商店的    价值0的不在商店卖
    /// 开始之后之后读取存档 修改各个数量（动态数据）
    /// 遍历之后分发成两个,
    /// 
    /// 外部获取只是引用不能复制
    /// 
    /// 道具增减之后 同时修改List,不要遍历
    /// 
    /// 
    /// 
    /// 某个类，名称 动态数据
    /// 该类生成到存档字典里面储存
    /// </summary>
    public invontory 所有;
    public Dictionary<String, CompleteItem> D_CompleteItem=new Dictionary<String, CompleteItem>();
    public List<Itemvalue> itemSave=new List<Itemvalue>();

 
 
        public CompleteItem GetCompleteItem(string name)
        {
            D_CompleteItem.TryGetValue(name, out var 完整物品);
            if (完整物品 != null)
            {
                return 完整物品;
            }
            else
            {
                Debug.LogError("字典找不到"+name);
                return null;
            }
        }
        public void PlayerAdd(CompleteItem item)
        {
            if (item.玩家持有 == 0)
            {
                ///假设卖空了
                /// 是不显示还是售罄
                /// 如果是不显示，那么卖完之后就消失在商店了
                /// 如果是售罄 那么标记价值的东西0后都售罄 √√√√√ 
                玩家的.Add(item);
            }
            item.玩家持有++;
        } 
        void Loaditem()
        {
            
            ///不知道啥原因加载不了
            var 在 = Resources.Load <item>("Gameobject/ScriptableObject");
            Debug.LogError(在.itemName + "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            var a = Resources.LoadAll<item>("Gameobject/ScriptableObject/物品/能力");
            for (int i = 0; i < a.Length; i++)
            {
                //var c = (item)a[i];
                Debug.LogError(a + "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            }
        }
        private void Awake()
        {
            //Loaditem();
            if (I != null && I != this) Destroy(this);
        else I = this;

        if (所有==null)   return;

            ///读取 存档
            itemSave = Load();
            //Debug.LogError(itemSave.Count);
        var 空存档 = itemSave == null;
            if (空存档) itemSave = new List<Itemvalue>();

            for (int i = 0; i < 所有.itemList.Count; i++)
            {
                var a = 所有.itemList[i];
            string key= a.itemName;
            ///如果为空，根据所有item创建 空数据 
            Itemvalue 动态数据;
            if (空存档)  动态数据 = new Itemvalue(key, a.物品数量, 0); 
            else
            {
                动态数据 = GetItemvalue(key);
                if (动态数据==null||动态数据.key==null)
                {
                    Debug.LogError(key+"从存档里面找不到");
                }
            }
                ///这边初始自定义
                if (动态数据.key == "原劈") 动态数据.玩家持有 = 1;

                itemSave.Add(动态数据);

            var 完整物品=new CompleteItem(a, 动态数据, item.返回(key)  );
            D_CompleteItem.Add(key, 完整物品);


                刷新商店和玩家UI(完整物品);
          }
            //刷新商店和玩家UI();
        ///存字典一个初始
        if (空存档)
            {///存到字典
                save();
            }
    
    }

     void 刷新商店和玩家UI(CompleteItem a )
         {
                if (a.物品价值>0)
                {
                if (!商店的.Contains(a))
                {
                    商店的.Add(a);
                } 
                }
                if (a.Save_value.玩家持有>0)
                {
                if (!玩家的.Contains(a))
                {

                    玩家的.Add(a);
                }
                } 
        }
        [Button]
        public void 从存档刷新()
        {
             
      
            var a = Load();
            if (a != null)
            {   
                ///因为 执行顺序      存到了字典所以第一次运行   有可能 读不出来
                玩家的.Clear();
                商店的.Clear();


               ///存档不看 ref 只看列表的数据  导致列表数据和实际数据不对齐 因此  需要覆盖列表
               //覆盖列表等于存档读取列表  所以必须要覆盖
                itemSave = a;
                for (int i = 0; i< a.Count ; i++)
                {
 
                    var II = a[i].key;
                    D_CompleteItem[II].SetSave_value(a[i]);
                    刷新商店和玩家UI(D_CompleteItem[II]);
                }
            }
        }
        class itemSaveClass
        {
            public List<Itemvalue> itemSave;
           public  itemSaveClass(List<Itemvalue> s)
            {
                itemSave = s;
            }
        }
        List<Itemvalue>  Load()
        {
            Debug.LogError("LoadLoadLoadLoadLoadLoadLoadLoadLoadLoadLoadLoadLoadLoadLoadLoadLoadLoadLoad");
            string s = Save_D.Load_Value_D<string>(SaveName, false); 
            var data = JsonUtility.FromJson<itemSaveClass>(s);
            if (data==null)
            {
                return null;
            } 
            return data.itemSave;
        }
     public    void save()
        {
            Debug.LogError("savesavesavesavesavesavesavesavesavesavesavesavesavesavesavesavesavesavesavesavesave");

            string c="";
            for (int i = 0; i < itemSave.Count; i++)
            {
                var aa  = itemSave[i];
                string ite=aa.key+"商店"+aa.商店持有+"玩家"+aa.玩家持有+"\n";
                c += ite;
            }
            Debug.LogError(itemSave.Count+c);
            itemSaveClass a = new itemSaveClass(itemSave);
            string s = JsonUtility.ToJson(a, true);
            Debug.LogError(s);
            Save_D.Add(SaveName, JsonUtility.ToJson(a, true));
        }
 

    public List<CompleteItem> 商店的=new List<CompleteItem>();
    public List<CompleteItem> 玩家的 = new List<CompleteItem>();
        string SaveName => "itemSave";

      Itemvalue GetItemvalue(String s)
    { 
        for (int i = 0;i<itemSave.Count; i++)
        {
            if (itemSave[i].key == s)
            {
                return itemSave[i];
            }
        }
        return default;
    } 
}
}
//// 不动的资产 item    存档数据Itemvalue  物品方法Item.返回();   围绕不动的资产 item.key