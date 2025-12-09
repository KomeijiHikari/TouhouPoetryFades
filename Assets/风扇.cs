using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 风扇 : MonoBehaviour,I_Speed_Is
{
    SpriteRenderer sp;
    Phy_检测 p;
    [SerializeField]
    private float speed_Lv=1;
    I_Speed_Is I=>this;
    [SerializeField]
    [DisableOnPlay]
    float 间隔=1;
    [SerializeField]
    [DisplayOnly]
    float 时间;
    public float Speed_Lv { get => speed_Lv; set => speed_Lv = value; }

    [SerializeField]
    public Vector2 力=Vector2.up;
    private void Awake()
    {
        p = GetComponent<Phy_检测>();
 sp=GetComponent<SpriteRenderer>(); 
    }
    public bool Deb;

    RaycastHit2D r;
    RaycastHit2D r2;

    bool  cc(RaycastHit2D r)
    {

        if (r.collider!=null)
        {
 
            if (r.collider.CompareTag(Initialize.Player))
            {
 
                return true;
            }
        }
        return false;
    }
    private void FixedUpdate()
    {
        if (p.遇见了)
        {
            Vector2 o2= new Vector2(Player3.I.Bounds.min.y-0.001f, sp.bounds.min.y);
            Vector2  o= new Vector2(  Player3.I.Bounds.min.x+0.001f, sp.bounds.min.y);
            r =    Physics2D.Raycast(o,Vector2.up,100f, 1 << Initialize.L_Player | 1 << Initialize.L_Ground | 1 << Initialize.L_M_Ground);
            r2 = Physics2D.Raycast(o2, Vector2.up, 100f, 1 << Initialize.L_Player|1<<Initialize.L_Ground|1<<Initialize.L_M_Ground);

            if (Deb)
            {
                if (r != default)    r.point.DraClirl(2, Color.red );
                if (r2 != default) r2.point.DraClirl(2, Color.red );
            }
            if (cc(r) || cc(r2))
            {
                Debug.LogError("AAWWWWWWWWWWWWWWWWWWWWWWWWWAAAAAA");
                if (Deb)
                {
                    if (r != default)
                    {
                        r.point.DraClirl(2, Color.blue, 2f);
                    }
                }
                时间 += Time.fixedDeltaTime * I.固定等级差;

                if (时间> 间隔)
                {
                    Player3.I.Velocity += 力;
                    时间 = 0;
                }
            }

        }
    }

}
