using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_Flow : MonoBehaviour
{
    [SerializeField]
    Transform Target;
    private void Update()
    {
        transform.position =new Vector3(Target.position.x, Target.position.y, transform.position.z) ;
    }
}
