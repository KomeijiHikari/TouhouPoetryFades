using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ATK_Clip
{
  public   bool Atk { get; set; }
  
    public enum 攻击类型
    {
        脚,
        头,
        全身,
        球

    }
    public AnimationClip my;
  public   string    真实name;
    public string   播放name;
    public string 假的name;
    public float time;

    [Header("参数")]
    [Space] 

    public float Start_time;
    public float End_time;
    public bool  双向攻击;
    public 攻击类型 当前攻击位置;
    public float 攻击距离;
    /// <summary>
    /// 如果零就是保持静止
    /// </summary>
    public  Vector2  移动速度;
    public bool 友伤;



    public  string[] 旧=new string[7] ;
    public string  新;
    public bool 更新否;
    public string  返回真名(string s)
    {
        if (s== 假的name)
        {
            return 播放name;
        }
        Debug.LogError("名称错误");
        return null;
    }
    public void 更新()

    {
        if (更新否)
        {
            旧[0] = Start_time.ToString();
            旧[1] = End_time.ToString();
            旧[2] = 双向攻击 ? "1" : "0";
            旧[3] = ((int)当前攻击位置).ToString();
            旧[4] = 攻击距离.ToString();
            旧[5] = v2转字符串(移动速度);
            旧[6] = 友伤 ? "1" : "0";



            //for (int i = 0; i < 旧.Length; i++)
            //{
            //    if (i + 1 == 旧.Length) break;
            //    旧[i] = 旧[i] + ";";
            //}

             新 =假的name + string.Join(";",旧);

        }
    }
    
    public  void  初始化()
    {
     
        time = my.length;
        真实name = my.name;
        string[] a = 真实name.Split("_");
        假的name = a[0]+"_";
        string[] b = a[1].Split(";");


        Start_time = float.Parse(b[0]);
        End_time = float.Parse(b[1] );
        双向攻击 = b[2] == "0" ? false : true;
        当前攻击位置 = (攻击类型)int.Parse(b[3]);
        攻击距离 = float.Parse(b[4] );
        移动速度 = 字符串转v2(b[5]);
        友伤 = b[6] == "0" ? false : true;


        播放name = 真实name.Replace(".","_");

        旧 = new string[7]
        { "null", "null", "null", "null", "null", "null",  "null" };
    }

    /// <summary>
    /// 两个数字，用"-"分割
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
 public static   Vector2 字符串转v2(string s)
    { float n, m;
       var a= s.Split("@");
      n = float.Parse(a[0]);
        m = float.Parse(a[1]);
        return new Vector2(n,m);
    }
    public static string  v2转字符串( Vector2 v2)
    {
        string a = v2.x + "@" + v2.y;

        return a;
    }

}

public class Enemy_atk : MonoBehaviour, I_攻击
{ 
    public List<ATK_Clip> Clip列表; 
    Animator an;
    enemy e;
     
    public ATK_Clip 当前Clip;

   Collider2D c;
    [DisplayOnly]
    public bool 释放;

    Vector2 脚底 = Vector2.zero;
    [DisplayOnly]
    [SerializeField]
    public Vector2 脚的原点 ;
    [DisplayOnly]
    [SerializeField]
    public Vector2 战立的原点 ;
    [DisplayOnly ]
    [SerializeField]
    float 刚体厚度_一半;

    public Action<Vector2> 攻击时候的速度;
    [DisplayOnly]
    [SerializeField]
    Vector2 朝向_;
    Vector2 朝向 { get => 朝向_; set => 朝向_ = value; }
    [DisplayOnly]
    [SerializeField]
    Vector2 反向_;
    Vector2 反向 { get => 反向_; set => 反向_ = value; }
    LayerMask 检测层;
    [DisplayOnly]
    [SerializeField]
    private float 攻击距离1;
    public float 攻击距离 { get => 攻击距离1; set => 攻击距离1 = value; }
    public Action 攻击结束;
    public bool 绘制; 

    public float 攻击检测范围_;
    public float 攻击检测范围 { get => 攻击检测范围_; set => 攻击检测范围_ = value; }
    public bool 攻击范围绘制; 
    public Action 进入攻击范围 { get; set; }

    public float atkvalue_;
  public   float  atkvalue { get => atkvalue_; set=> atkvalue_=value ; }
    public bool 进入攻击检测范围了吗 
    { get => 进入攻击检测范围了吗1; set => 进入攻击检测范围了吗1 = value; }


    [DisplayOnly ]
    [SerializeField]
    private bool 进入攻击检测范围了吗1;

    Vector2 原点;
    private void Awake()
    {
        e = GetComponent<enemy>();
           an = GetComponent<Animator>();
           c = GetComponent<Collider2D>();
        刚体厚度_一半 = (c.bounds.max.x - c.bounds.min.x) / 2;


        if (攻击检测范围 == 0)
        {

            Initialize_Mono.I.Debug_(this.GetType(), "这个" + gameObject + "的玩家检测范围未设置"); 
            攻击检测范围 = 4;
        }
        if (atkvalue_==0)
        {
            atkvalue_ = 10;
        }
    }


    private void Start()
    {
        if (Clip列表.Count==0)
        {
            Debug.LogError("没有攻击动画放进来");
        }
        else
        {
            foreach (var item in Clip列表)
            {
                item.初始化();
            }
        }
        当前Clip = Clip列表[0];
    }


    [DisplayOnly ]
    [SerializeField]
    float time;



    private void FixedUpdate()
    {
        原点 = c.bounds.center;
        if (e.攻击目标!=null
            && Mathf.Abs(e.攻击目标.transform.position.x - c.bounds.center.x)  <= 攻击检测范围)
        { 
                进入攻击检测范围了吗 = true;
            if (进入攻击检测范围了吗)
            {
                进入攻击范围.Invoke();
            }
        }

        else 
        {
            进入攻击检测范围了吗 = false;
        }
        更新();
    }
    void 更新()
    {
        攻击距离 = 当前Clip.攻击距离 + 刚体厚度_一半;
        脚底 = new Vector2(c.bounds.center.x, c.bounds.min.y);
        脚的原点 = new Vector2(脚底.x, 脚底.y + Initialize.打腿);
        战立的原点 = new Vector2(脚底.x, 脚底.y + Initialize.打胸);
        朝向 = new Vector2(transform.localScale.x, 0);
        反向 = new Vector2(-transform.localScale.x, 0);
        if (e.攻击目标!=null)
        {
            transform.localScale = Initialize.朝向对象(this.gameObject, e.攻击目标);
        }
        foreach (var item in Clip列表)
        {
            item.更新();
        }
    }
  public   void  攻击() 
    { 
        if (time == 0)
        {
            Debug.LogWarning("开播");
            Debug.LogWarning(当前Clip.假的name);
            Debug.LogWarning(当前Clip.返回真名(当前Clip.假的name));
            an.Play(当前Clip.返回真名(当前Clip.假的name));
        }

        time += Time.fixedDeltaTime;
        if (time>=当前Clip .time )
        {
            Debug.LogWarning("结束");
            time = 0;
            攻击结束.Invoke();
        }
      else   if (time>= 当前Clip.End_time)
        {
            Debug.LogWarning("END");
            释放 =false;
        }
        else  if(time >= 当前Clip.Start_time)
        {
            Debug.LogWarning("Start");
            释放 =true ;  
        }



        if (当前Clip.友伤)
        {
            检测层 = 1 << LayerMask.NameToLayer("Player")| 1 << LayerMask.NameToLayer("Enemy");
        }
        else
        {
            检测层 = 1 << LayerMask.NameToLayer("Player");

        }




        if (释放)
        {

            switch (当前Clip.当前攻击位置)
            {
                case ATK_Clip.攻击类型.脚:
                    发射(脚的原点);
                    break;
                case ATK_Clip.攻击类型.头:
                    发射(战立的原点);
                    break;
                case ATK_Clip.攻击类型.全身:
                    发射(战立的原点);
                    发射(脚的原点);
                    break;

                case ATK_Clip.攻击类型.球:
                    爆炸();
                    break;
                default:
                    发射(战立的原点);
                    发射(脚的原点);
                    break;
            }
        }

    }
    private void OnDrawGizmos()
    {
        if (c != null)
        {
            if (e.攻击目标 != null)
            {
                if (当前Clip.当前攻击位置 == ATK_Clip.攻击类型.球
    && 释放)
                {
                    Gizmos.DrawWireSphere(原点, 攻击距离);
                }
                else
                {
                    Gizmos.DrawWireSphere(原点, 攻击检测范围);
                }



            }
        }
    }
    void 爆炸()
    {
        if (e.攻击目标 == null) return;
        var a = Physics2D.OverlapCircle(new Vector3(原点.x, 原点.y, e.攻击目标.transform.position.z), 攻击距离, 检测层);
        if (a != null)
        {

                触发(a.gameObject);
            

        }
    }
    void 发射(Vector2  v2)
    {
        攻击时候的速度?.Invoke(当前Clip.移动速度);
        var a = Physics2D.Raycast(v2, 朝向, 攻击距离, 检测层);
        //if (a == null) return;
        if (a.collider == null) return;
        if (a.collider.gameObject == null) return;
        触发(a.collider.gameObject);
        Debug.DrawRay(v2, new Vector3(攻击距离 * 朝向.x, 0, 0), Color.blue, 0.05f);
        if (当前Clip.双向攻击)
        {

            var b = Physics2D.Raycast(v2, 反向, 攻击距离, 检测层);
            Debug.DrawRay(v2, new Vector3(攻击距离 * 反向.x, 0, 0), Color.black, 0.05f);
            //if (b== null) return;
            if (b.collider == null) return;
            if (b.collider.gameObject == null) return;
            触发(b.collider.gameObject);

        }

    }
    int I;
    void 触发(GameObject 玩家)
    {
        
        if (玩家 == null) return;
        if (Time.frameCount - I < 8) return;
        Debug.LogError("调用");
          

            I = Time.frameCount; 
        玩家.GetComponent<I_生命>()?.被扣血(atkvalue,this .gameObject,0 );
    }

    void I_攻击.扣攻击(float i)
    {
    }
}
