using System;
using UnityEngine;
using Sirenix.OdinInspector;
public class Phy_检测 : MonoBehaviour, I_暂停
{
   [SerializeField][DisplayOnly] public int 数量;
    public void Find(string s)
    {

    }
    public Vector2 尺寸加成;
    public bool 暂停 { get => 暂停1; set => 暂停1 = value; }
    [SerializeField]
    SpriteRenderer sp;

    public Action Exite;
    public Action Enter;
    public Action AliveStay;
    [SerializeField]
    public RaycastHit2D[] Rs;
    [SerializeField]
    LayerMask L;
    //List<Collider2D> CL;
    [SerializeField]
    bool 遇见了_;
    public bool PoinDeb;
    public Action Stay;
    public bool 遇见了
    {
        get => 遇见了_; set
        {
            if (遇见了_ != value)
            {
                if (value)
                {
                    if (Deb)
                    {
                        Debug.LogError(gameObject.name + "触发              触发");
                    }
                    Enter?.Invoke();
                }
                else
                {
                    Exite?.Invoke();
                }
            }
            遇见了_ = value;
        }
    }

    public float EnableTime;
    private void OnEnable()
    {
        EnableTime = Time.time;
    }
    private void OnDisable()
    { 
        Enter = null;
        Exite = null;
        Stay = null;
        遇见了 = false;
    }
    MonoMager mo;
    private void Awake()
    {
        gameObject.组件(ref mo);
    }
    public bool Deb;
    [SerializeField]
    [DisplayOnly]
    private bool 暂停1;

    public bool ActionDeb;
    //public float 角度;
    private void Update()
    {
        if (暂停) return;

        if(ActionDeb)
        {

            if (AliveStay != null) Debu.LogError(AliveStay.GetInvocationList().Length);
            if (Stay != null) Debu.LogError( Stay.GetInvocationList().Length);
            if (Enter != null) Debu.LogError(Enter.GetInvocationList().Length);
            if (Exite != null) Debu.LogError(Exite.GetInvocationList().Length);
        } 

        AliveStay?.Invoke();
        if (Deb)
        {
            Debug.LogError("AAAAAAAAAAA" + transform.lossyScale + sp.bounds.center); 
        }
        ///原先Bouns 会跟着变换改变Bouns大小
        ///之后 的话 保持transform   比例正常
        if (遇见了)
        {
            Stay?.Invoke();
        }
        ///size 值不能有负数
        Rs = Physics2D.BoxCastAll(sp.bounds.center,
             new Vector2(MathF.Abs(transform.lossyScale.x) + 尺寸加成.x, transform.lossyScale.y + 尺寸加成.x),
              transform.rotation.eulerAngles.z,
               Vector2.zero,
              0,
              L);
        遇见了 = (Rs != null) && Rs.Length > 0;

        if (Deb) Debug.LogError(Rs.Length);

        if (PoinDeb)
        {
            foreach (var item in Rs)
            {
                item.point.DraClirl(1, Color.blue);
            }
        }
    }

}
