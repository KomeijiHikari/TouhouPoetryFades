using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 只有一个是I_speed_is : MonoBehaviour, I_Speed_Is
{
    [SerializeField]
    private float speed_Lv;

    public float Speed_Lv { get => speed_Lv; set => speed_Lv = value; }
}
