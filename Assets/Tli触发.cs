using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Sirenix.OdinInspector;
public class Tli触发 : MonoBehaviour
{
    [SerializeField]
    TileBase Ti;


    [SerializeField]
    Tilemap T;

    Phy_检测 P;
    SpriteRenderer sp;
    private void Awake()
    {
        P = GetComponent<Phy_检测>();
        sp = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        触发();
    }

 
    [Button("Play_", ButtonSizes.Large)]
    public void 触发()
    {
        for (int i = 0; i < P.Rs.Length; i++)
        {
            var B = Initialize.Get_v2(P.Rs[i].point, 0.2f);
            foreach (var item in B)
            {
                item .DraClirl(0.1f,Color.blue);
                Vector3Int poi = T.WorldToCell(item);
                T.CellToWorld(poi);
         
                消除砖块(poi); 
            }
      
        }
 bool  消除砖块(Vector3Int poi)
        {
            if (T.HasTile(poi))
            { 
                T.SetTile(poi, Ti);
                特效_pool_2.I.GetPool(T.CellToWorld(poi), T_N.特效砖块爆炸);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
