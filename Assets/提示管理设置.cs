using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class 提示管理设置 : MonoBehaviour
{

    public List<I_BOOL> BBs;

    //private void Awake()
    //{
    //    Initialize_Mono.I.TsL.Add(this);
    //    BBs = new List<I_BOOL>(GetComponents<I_BOOL>());
    //}
    private void Start()
    {
        Initialize_Mono.I.TsL.Add(this);
        BBs = new List<I_BOOL>(GetComponents<I_BOOL>());
    }
    [Tooltip("且")]
    public bool 开关;
    public void 刷新()
    {
        开关 = true;
        for (int i = 0; i < BBs.Count; i++)
        {

            var a = BBs[i];
            bool 好 = a.好嘛();
            if (a.反转) 好 = !a.好嘛();
            if (!好)
            {
                ///不好就关掉
                ///好就不做处理不把开关打开
                ///所以是且
                开关 = false;
            }

        } 
          gameObject.SetActive(开关);

    }
}
