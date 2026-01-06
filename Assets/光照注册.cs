using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 光照注册 : MonoBehaviour
{
    public Light L;

    [SerializeField]
    private float @in=1;


    float Start_intensity;

    public float In { get { 
            return @in;
        }
         set {
            L.intensity = @in * Start_intensity;
            @in = value; }  }

    private void Start()
    { 
        L = GetComponent<Light>();
        我的光照2.I.LisTT.Add(this);
        Start_intensity = L.intensity;
        In = 1;
    }
}
