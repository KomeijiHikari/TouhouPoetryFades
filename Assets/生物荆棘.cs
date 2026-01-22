using Boss;
  
using UnityEngine;

public class 生物荆棘 : MonoBehaviour
{
    SpriteRenderer sp;
    BoxCollider2D bc;
    private void Awake()
    {
        sp=GetComponent<SpriteRenderer> ();
 bc=GetComponent<BoxCollider2D> ();
    }
    private void Start()
    {
        if (Surp_Pool.I != null)
        {
            var a = Surp_Pool.I.GetPool("危险");
            a.transform.position = transform.position;
            W= a.GetComponent<SpriteRenderer>() ;
            W.size = sp.size;
        }
    }
    SpriteRenderer W;

    float time;

    public void 关闭()
    { 
        真正开关(false);
    }
    void  真正开关(bool b)
    {
        sp.enabled = b;
        bc.enabled = b;
    }
    public void 开关(bool b)
    {
        if (b)
        {
            time = Time.time;
            W.enabled=true;
        }
        else
        {
            真正开关(false);
        }
    }
    private void Update()
    {
            if (W.enabled)
        {
            float 进度=(Time.time - time)/0.3f ;
            进度 =1- Mathf.Clamp(进度,0,1);
            W.color = Color.white * 进度;

            if ((Time.time - time)>=0.3f)
            {
                W.enabled = false;
                真正开关(true);
            }
        }
    }
    private void OnEnable()
    {
        if(W!=null)
        {
            W.size = sp.size;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(Initialize.Player))
        { 
            Player3.I.被扣血(20, gameObject, 0);
            Player3.I.To_SafeWay();
        }
    }
}
