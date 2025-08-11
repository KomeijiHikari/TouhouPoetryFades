using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 剧情执行 : MonoBehaviour
{
    BiologyBase e;
    public List<点> lu;
    [DisplayOnly]
    [SerializeField]
    bool 接管;
    public void Start_寄生(List<点> Lu)
    {
        e = GetComponent<BiologyBase>();
        lu = Lu; 
        e.关闭灵魂(); 
        接管 = true;
    }

    public void End_寄生()
    {
        if (接管)
        {
            e.开启灵魂();
            接管 = false;
        }
    }
    public int 下标;
    private void Update()
    {
        if (接管)
        {

        }

    }
}
