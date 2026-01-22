using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;
public class 删除这个 : MonoBehaviour
{
    public SpriteRenderer 在不在摄像机里面;
    public Transform Phy预测;
    public Transform Ts;
    public Material M;
    public bool s设置材质=false;

    SpriteRenderer sp;

    Rigidbody2D rb;
    string DarkName = "_DarkColor";

   
    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();

        M= sp.sharedMaterial;
    }
 public   float Time;
    public Vector2 V;
    public Vector2 Way;
    void asd()
    {
        var a = V + (Vector2)transform.position;
        Initialize_Mono.I.Waite(() => {

          a.DraClirl(1, Color.red, 1);
            rb.bodyType = RigidbodyType2D.Static;
        }, Time);
    }
    [Button]
    public void aaaasd()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
    [Button]
    public void A()
    {
        rb.velocity = rb.Frist(Time, V);


        asd();
    }
    [Button]
    public void B()
    {

        rb.velocity = rb.Next(Time, V);
        asd();
    }
    [Button]
    public void C()
    {
        rb.velocity = rb.Last(Time, V);
        asd();
    }
    void v2ASD()
    {
        var a = V + (Vector2)transform.position; 
            a.DraClirl(1, Color.red, 12); 
    
    }
    [Button]
    public void Av()
    {
        rb.velocity = rb.V2Frist(Way, V); 
        v2ASD();
    }
    [Button]
    public void Ab()
    {
        rb.velocity = rb.V2Next(Way, V);
        v2ASD();
    }
    [Button]
    public void Ac()
    {
        rb.velocity = rb.V2Last(Way, V);
        v2ASD();
    }
    public Vector3 CameraWv;
    public Vector3 CameraWs;
    public Vector3 CameraV2zeroPo;
    private void Update()
    {
        if (在不在摄像机里面 != null)
        {
            var C = 在不在摄像机里面.transform.position;
            CameraWv = Camera.main.WorldToViewportPoint(C);
            CameraWs=Camera.main.WorldToScreenPoint(C);

            CameraV2zeroPo= Camera.main.ViewportToWorldPoint(Vector3.zero);
            //在不在摄像机里面.bounds.Intersects();
            //if (CameraWv.y < 0 || CameraWv.x < 0 || CameraWv.y > 1 || CameraWv.x > 1)
            //{
            //    在不在摄像机里面.color = Color.red;
            //}
            //else
            //{
            //    在不在摄像机里面.color = Color.white;
            //}
            var a = 摄像机.I.GetBouns().Intersects(在不在摄像机里面.bounds);
            if (a)
            {
                在不在摄像机里面.color = Color.red;
            }
            else
            {
                在不在摄像机里面.color = Color.white;
            }
 
        }
        if (Phy预测!=null)
        {
            Phy.碰撞预测2(Phy预测 .position- Player3.I.Bounds.center, Player3.I.Bounds,1<<Initialize.L_Ground,transform,true);
        }
        if (Ts!=null)
        {
            Vector3 V = transform.position - Ts.transform.position;
            Vector3 No = V;
            No.Normalize();
 
            Debug.DrawRay(transform.position, No * 3, Color.red);
            Debug.DrawRay(transform.position+Vector3.right*0.5f, V.normalized * 3, Color.yellow);
        }

        if (s设置材质)
        {
            sp.material.SetColor(DarkName, Color.red);
        }
        else
        {
            sp.material = M;
        }
    }

}
