using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using 发射器空间;

namespace ItemMager
{


public class 货物子单元 : MonoBehaviour
{
     [SerializeField]GameObject 售罄;
    [SerializeField] Text 名字 ;
    [SerializeField] Text 价格;
    [SerializeField] Image 图标;

        CompleteItem 物品;

    Text_button 按钮;
    private void Awake()
    { 
        按钮 = GetComponent<Text_button>();
        按钮.Enter .AddListener(Enter);
    } 
    void Enter()
        {
            if (物品.商店持有<=0)
            {
                消息.I.Come_on_Meesge(物品.key + "已经卖完了", true);
                return;
            }

            if (Player3.I.玩家数值.钱 < 物品.物品价值)
            {
                消息.I.Come_on_Meesge( "你的钱不够", true);
                return;
            }

            //Surp_Pool.Get_Gameobject("")
            Player3.I.玩家数值.钱 -= 物品.物品价值;
                消息.I.Come_on_Meesge("已购买" + 物品.key, true);
                物品.方法?.Invoke();

                所有物品管理.I.PlayerAdd(物品);
                物品.商店持有--; 
 
 

            if (售罄 != null)
            {
                售罄.SetActive(物品.商店持有 <= 0);
            }
        }


    [Button]
    public void 刷新(CompleteItem 物品)
    {
        this.物品 = 物品;
        // 1. 先检查传入的物品是否为空
        if (物品 == null)
        {
            Debug.LogError("AAAAAAAAAAAAAAAAA：传入的物品对象为空");
            return;
        }

        // 2. 检查物品的核心属性是否为空
        if (物品.itemImage == null)
        {
            Debug.LogError("RRRRRRRRRRRRR：物品的图片为空");

            return;
        }

        // 3. 逐个检查UI组件并赋值，每个组件都做非空校验
        // 图标赋值（非空校验）
        if (图标 != null)
        { 
            图标.sprite = 物品.itemImage;
        }
        else
        {
            Debug.LogError("TTTTTTTTTTT：图标UI组件未赋值");
        }

        // 名字赋值（非空校验）
        if (名字 != null)
        {
            名字.text = 物品.key;
        }
        else
        {
            Debug.LogError("名字UI组件未赋值，无法设置物品名称");
        }

        // 价格赋值（非空校验）
        if (价格 != null)
        {
            价格.text = 物品.物品价值.ToString();
        }
        else
        {
            Debug.LogError("价格UI组件未赋值，无法设置物品价格");
        }

        if(售罄!=null)
        {
            售罄.SetActive(物品.商店持有<=0);
        }
    } 
}
}