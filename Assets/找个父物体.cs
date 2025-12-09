using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 找个父物体 : MonoBehaviour
{
    [SerializeField]
    Transform targrt;
    private void Start()
    {
        gameObject.transform.SetParent(targrt);
    }
}
