using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CunemachineTarget咕噜咕噜 : MonoBehaviour
{[SerializeField]
    CinemachineTargetGroup g; 
    public void SetPlayer()
    {
        g.AddMember(Player3.I.transform, 1, 7);
    }
    public void RemovePlayer()
    {
        g.RemoveMember(Player3.I.transform);
    }
}
