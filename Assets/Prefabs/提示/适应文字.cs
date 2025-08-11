using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 竖向 顶喝下间距可以用 ho 达成   
/// 每行平均间距 用这里的解决
/// </summary>

 
public class 适应文字 : MonoBehaviour
{
public     GameObject 开关_;

    [TextArea (3,10)][SerializeField ]
     string Text;
     Text t;
    private void Awake()
    { 
        t = GetComponent<Text>();
    }
   
    public void 开关(bool b)
    {
        开关_.SetActive(b);
    }
    private void Start()
    {
        开关(false );
    }
    public string   GetText( )
    {
        return Text;
    }
    public void SetText(string s)
    {
        Text = s;
        t.text = "\n"+"  "+ s +"  " + "\n";
    }
}
