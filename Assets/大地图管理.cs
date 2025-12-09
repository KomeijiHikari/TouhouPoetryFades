using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
public class 大地图管理 : MonoBehaviour
{
 
    [SerializeField]
    Camera a;

    Key Map;

    public float Big_=20;
    public    float Small_=10;
    private void Start()
    {
        Map = Player_input.I.Get_key(Player_input.I.k.地图);
    }

    bool 大;
    private void Update()
    {
       if (Player_input.I.Now_Time - Map.KeytimeUp<0.5f)
        {
            大 = false;

            if (Map.被按下了吗)
            {
                大 = true; 
            }
        }

        if (大)    a.orthographicSize = Big_; 
        else    a.orthographicSize = Small_; 

    }
}
