using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class 假地面 : MonoBehaviour
{
    CompositeCollider2D co;
    TilemapRenderer s;

    Tilemap T ; 

 
    private void Awake()
    {
        s = GetComponent<TilemapRenderer>();
        co = GetComponent<CompositeCollider2D>();
        T = GetComponent<Tilemap >();

        s.material = 材质管理.Get_Material(材质管理 .Other);
    }
 
      List<Vector3Int> 已经空 = new List<Vector3Int>();
    List<Vector3Int> 应该空=new List<Vector3Int> ();

    int 次数;
    int Tim ;
    void 循环(List <Vector3Int>  PO)
    {
        if (Tim!=Time.frameCount)
        {
            次数 = 0;
               Tim = Time.frameCount;
        }
        else
        {
            次数++; 
        }
        if (次数 > 11000) return;
        foreach (var item in PO)
        {
            if (管(item))
            {
                var b = 九循环(item);
                循环(b);
            }
        }
    }
    public void 炸裂(Vector3Int ci)
    {
        if (Debu) Debug.LogError("AAAAAAAAAAAAAA普");
        if (!管(ci)) return;
        var a = 九循环(ci);

        循环(a); 
    }
    public List<Vector3Int> 九循环(Vector3Int c)
    {
        int X = c.x;
        int Y = c.y;
        List<Vector3Int> result = new List<Vector3Int>();
        result.Add(c);
 
        result.Add(new Vector3Int(X + 1, Y + 1));
        result.Add(new Vector3Int(X + 1, Y  ));
        result.Add(new Vector3Int(X + 1, Y - 1));

        result.Add(new Vector3Int(X, Y + 1));
        result.Add(new Vector3Int(X , Y));
        result.Add(new Vector3Int(X, Y - 1));

        result.Add(new Vector3Int(X - 1, Y + 1));
        result.Add(new Vector3Int(X  , Y));
        result.Add(new Vector3Int(X - 1, Y - 1));
        return result;
    }
    public bool 管(Vector3Int ci,bool Debbbb=false )
    {  
        if (T.HasTile(ci))
        {
            if (!应该空.Contains(ci))
            {
                应该空.Add(ci);
                return true;
            }
            else
            { 
                return false;
            }

        }
        else
        { 
            return false;
        }

    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{

    //    if (collision.gameObject.layer == Initialize.L_Player)
    //    {

    //        显示 = true;
    //        Target = 显示 ? 1 : 0; 
    //    }
    //}
    //private void OnTriggerEnter2D(Collider2D collision)
    //{ 
    //    if (collision.gameObject.layer == Initialize.L_Player)
    //    {
    //        显示 = false;
    //        Target = 显示 ? 1 : 0;

    //        var ccc = Player3.I.Bounds.碰撞(1 << Initialize.L_No_Player, 1.5f);
    //        if (ccc!=null&&ccc.Length>0)
    //        {
    //            var c = ccc[0].point;
    //            Vector3Int ci = T.WorldToCell(c);
    //            炸裂(ci);
    //            if (Debu) Debug.LogError("进");
    //        }

    //    }
    //}
    [SerializeField]
    [DisplayOnly]
    bool 接触;
    No_Re Nup=new No_Re (); 
    No_Re NR=new No_Re ();
    int 备用_Last;
    int 备用;
  
    private void FixedUpdate()
    { 
        if (备用_Last == 备用1)
        {
            接触1 = false;
        }
        else
        {
            接触1 = true;
            备用_Last = 备用1;
        } 
        渐变();
    }
    private void OnTriggerStay2D(Collider2D co )
    {

        if (co.gameObject .layer ==Initialize .L_Player  )
        {
            //if (NR.Note_Re())
            //Fix运行，帧数少的时候会在 同一个 frameCount   里运行
            //因此看起来是update里运行    然而   帧数多的时候     update连续会跳过FIx
            //stay   进行帧运行多次无妨    关键是停下来  来判断

            备用1++;
                if (备用1>1000)
                {
                    备用1 = 0;
                }
   
          }
    
    }
 void Changecolor(Vector3Int c,Color co)
    {
        T.RemoveTileFlags(c , TileFlags.LockColor);
        T.SetColor(c , co);
    }
    public bool Debu;
    void Changecolor (List<Vector3Int>   v3S, Color co)
    {
        if (Debu)  Debug.LogError("调用"+co);
        for (int i = 0; i < v3S .Count; i++)
        {
            var a = v3S[i];
            Changecolor(a, co);
        }
    }
 
    [SerializeField]
    [DisplayOnly]
    float Target_=1;
    float Target
    {
        get
        {
            return  Mathf.Clamp(Target_,0,1) ;
        }
        set
        { 
            if (Target_ != value)
            {
                //Debug.LogError("Target_ :"+value );
         
            Target_ = value;
            }
        }
    }

    public bool 接触1 { get => 接触; set
        {
            if (接触!=value )
            { 
                if (value )
                {
                    if (Debu) Debug.LogError("接触" + value);
                    var ccc = Player3.I.sp  .bounds .碰撞列表(1 << Initialize.L_No_Player, 1.1f);

                    bool 有碰撞点但是寄了=true;
                    if (ccc != null && ccc.Length > 0)
                    {
                        for (int i = 0; i < ccc.Length ; i++)
                        {
                            var c = ccc[0].point;
                            Vector3Int ci = T.WorldToCell(c);

                            var a = Initialize.Get_v2_IntArry((Vector2Int)ci);
                            for (int b = 0; b < a.Length ;b++)
                            {
                                Vector3Int ASDA = (Vector3Int) a[b];
                                if (T.HasTile(ASDA))
                                {
                                    有碰撞点但是寄了 = false;
                                    炸裂(ASDA);
                                    break;
                                } 
                            }
  
                        }
                        if (Debu) Debug.LogError("进" + ccc.Length);
                        if (有碰撞点但是寄了)
                        {

                            if (Debu) Debug.LogError("喷到了但是一个点都没对上" );
                        }
                    }
                    else
                    {
                        //for (int i = 0; i < ccc.Length ; i++)
                        //{
                        //    ccc[i].point 

                        //}
                        if (Debu) Debug.LogError("碰撞落空碰撞落空碰撞落空");
                    }
 
                }
                Target = value ? 0: 1;
 
                接触 = value;
            }
        } }

    public float A1 { get => A; set {
            if (A!=value)
            {
                A = value;
                //Debug.LogError("A  :"+A );
            }
        } }

    public int 备用1 { get => 备用; set {

            if (备用!=value )
            { 
                备用 = value;
            }

        } }

    [SerializeField ][DisplayOnly]
    float A=1;


    No_假Fix Fix = new No_假Fix(0.01f); 
    void 渐变()
    {
        if (!Fix.FixUpdate()) return;
        A1 = Mathf.Lerp(A1, Target, 0.1f);
        A1 = Mathf.Clamp(A1, 0, 1);
        A1 = (float)Math.Round(A1, 3);
        if (A1 < 0.05) A1 = 0;
        if (A1 > 0995) A1 = 1;
        //s.material.SetFloat(材质管理._Alpha, A);
        //应该空.Clear();
        if (应该空 != null && 应该空.Count > 0)
        { 
            Changecolor(应该空, new Color(1, 1, 1, A1));

            if (Target == 1 && A1 > 0.95f)
            {
                A1 = 1;
                Changecolor(应该空, Color .white );
                应该空.Clear();
                if (Debu) Debug.LogError("清空清空清空清空清空");
            }
            else if(Target == 0 && A1 <0.04f  )
            {
                Changecolor(应该空, Color.white*0f);
            }
        }
        else if(应该空 == null ||应该空.Count == 0    )
        {
            if (A1!=1)
            {
                Debug.LogError("空空空空空空空空\n空空空空空空空空\n空空空空空空空");
                s.material.SetFloat(材质管理._Alpha ,  A1 );
 
            }

        }
    } 
}
