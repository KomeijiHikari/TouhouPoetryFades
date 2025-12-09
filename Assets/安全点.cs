using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 安全点 : MonoBehaviour 
{
    Phy_检测 p;  
    private void Awake()
    { 
        p = GetComponent<Phy_检测>();
        p.Enter+=() => Player3.I.安全地点(true);
    } 
}
