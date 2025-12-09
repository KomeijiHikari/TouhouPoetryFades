using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class 戒备 : MonoBehaviour, I_暂停
{
    public bool Deb;
    public enum 戒备类型
    {
        一长一短,
        单面,
        扇形, 
        距离,
    }
    public 戒备类型 当前戒备类型;
    [SerializeField]
    [DisplayOnly]
    private List<GameObject> 检测到的列表1;
    public float 主检测距离;
    public float 次检测距离;
    Enemy_base b;


    public LayerMask 检测层 => Initialize_Mono.I.戒备检测层;

    RaycastHit2D[] 主要s = new RaycastHit2D[0];
    RaycastHit2D[] 次要s = new RaycastHit2D[0];

    public Action<bool> 发现玩家了嘛;
    public bool 发现玩家了吗_;
    public bool 发现玩家了吗
    {
        get => 发现玩家了吗_;
        set
        {
            if (!发现玩家了吗_ && value)
            {
                发现玩家了嘛?.Invoke(true);
            }
            else if (发现玩家了吗_ && !value)
            {
                发现玩家了嘛?.Invoke(false);
            }
            发现玩家了吗_ = value;
        }

    }

    public List<GameObject> 检测到的列表 { get => 检测到的列表1; set => 检测到的列表1 = value; }
    private void Awake()
    {
        b = GetComponent<Enemy_base>();
        if (主检测距离 <= 0)
        {
            Debug.LogWarning("这个" + gameObject + "的正面检测范围未设置   已使用默认值");
            主检测距离 = 5;
        }
        if (次检测距离 <= 0)
        {
            Debug.LogWarning("这个" + gameObject + "的反面检测范围未设置  已使用默认值");
            次检测距离 = 5;
        }

    }


    private void Update()
    {

        检测到的列表.Clear();
        if (暂停) return;
        switch (当前戒备类型)
        {
            case 戒备类型.单面:
                主要s = Physics2D.RaycastAll(b.正面中间, b.正向, 主检测距离, 检测层);

                if (Deb)
                    Debug.DrawRay(b.正面中间, new Vector3(transform.localScale.x * 主检测距离, 0, 0), Color.green, 0.1f);
                录入(主要s);
                break;
            case 戒备类型.一长一短:

                主要s = Physics2D.RaycastAll(b.正面中间, b.正向, 主检测距离, 检测层);
                次要s = Physics2D.RaycastAll(b.反面中间, b.反向, 次检测距离, 检测层);
                if (Deb)
                {
                    Debug.DrawRay(b.正面中间, new Vector3(transform.localScale.x * 主检测距离, 0, 0), Color.green, 0.1f);
                    Debug.DrawRay(b.反面中间, new Vector3(-transform.localScale.x * 次检测距离, 0, 0), Color.red, 0.1f);
                }
                录入(主要s);
                录入(次要s);
                break;

            case 戒备类型.扇形:

                int 朝向 = (int)transform.localScale.x;
                检测到的列表 = Initialize.扇形检测(b.Bounds.center, 主检测距离, 朝向 * 角度, 扇形角度, 精度, 检测层, 穿透否, Deb);
                break; 
                case 戒备类型.距离:
                var a = Player3.I.transform.position - transform.position;

       
                if (Deb) Debug.DrawRay(transform.position, a.normalized* 主检测距离, Color.red, 0.1f);
                if (a.sqrMagnitude<主检测距离*主检测距离)
                {
                    主要s = Physics2D.RaycastAll(transform.position, a.normalized, 主检测距离, 检测层);
                    for (int i = 0; i < 主要s.Length; i++)
                    {
                        var R = 主要s[i];
                        if (R.collider != null)
                        {
                            if (Deb)
                            {
                                R.point.DraClirl();
                            }
                            if (R.collider.gameObject == gameObject) continue;
                            if (!检测到的列表.Contains(R.collider.gameObject))
                            {
                                检测到的列表.Add(R.collider.gameObject);
                            }
                            if (!穿透否) break;
                        }
                    }
                  //bool b=    Physics2D.Raycast(transform.position, a.normalized, 主检测距离, 1 << Initialize.L_Player).collider==null;
                    //if (!检测到的列表.Contains(Player3.I.gameObject))
                    //{
                    //    检测到的列表.Add(Player3.I.gameObject);
                    //}
                } 
                break;
        } 
        发现玩家了吗 = 检测到的列表.Exists(t => t.layer == Initialize.L_Player);

    } 
    [SerializeField] bool 穿透否 = false;
    [Tooltip("扇形的方向角度  头上为0，顺时针")]
    /// <summary>
    ///  头上为0，顺时针
    /// </summary>
    [SerializeField] float 角度 = 90;
    [SerializeField] float 扇形角度 = 80;
    [SerializeField] int 精度 = 10;
    [SerializeField] float 丢失time_Max = 3;

    [DisableOnPlay]
    [SerializeField] float 丢失time;
    GameObject 最后姆布埃;

    [SerializeField ][DisplayOnly ]
    private bool 关闭1;

    public GameObject 返回一个玩家///帧运行
    {
        get
        {
            foreach (var item in 检测到的列表)
            {
                if (item.layer == Initialize.L_Player)
                {  ///有 
                    丢失time = Time.time;
                    最后姆布埃 = item;
                    return item;
                }
            }
            ///没有
            ///
            if (Time.time - 丢失time < 丢失time_Max * (1 / b.I_S.固定等级差))
            {
                return 最后姆布埃;
            }
            else
            {
                return null;
            }

        }
    }

    public bool 暂停 { get => 关闭1; set => 关闭1 = value; }

    void 录入(RaycastHit2D[] S)
    {
        for (int i = 0; i < S.Length; i++)
        {
            RaycastHit2D hit = S[i];
            GameObject a = hit.collider.gameObject;

            if (a != this.gameObject)
            {
                if (a.layer == Initialize.L_Ground)
                {
                    检测到的列表.Add(a);
                    break;  // 这里会在检测到第一个满足条件的物体后退出循环  
                }
                检测到的列表.Add(a);  // 注意：这里应该有一个else，否则即使物体不在地面上也会被添加到列表中   
            }
        }
    }
}
