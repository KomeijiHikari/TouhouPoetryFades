using Enemmy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using 发射器空间;

public class 魔炮机关 : MonoBehaviour, I_暂停, I_Speed_Is
{
    发射器 f;
    监控激活碰撞框 J;
    MonoMager M;
    public bool Deb;

    public float 生命周期 = 0;
    [SerializeField]
    SpriteRenderer sp;
    public bool 暂停 { get => 暂停1; set => 暂停1 = value; }
    public float Speed_Lv { get => speed_Lv; set => speed_Lv = value; }

    float Angle;
    private void Awake()
    {

        Angle = Initialize.To_方向到角度(Initialize.方向To_v2(方向));
        f = GetComponent<发射器>();
        gameObject.组件(ref sp);
        gameObject.组件(ref J);
        gameObject.组件(ref M);

        J.是我 += (b) =>
        {
            time = 间隔 - 0.01f; 
        };
    } 

    /// <summary>
    /// 运行前  所有的发射机关都一样
    /// 间隔（默认3）    之后速度应该是 发射物体的 自身速度*上   比如之后是21
    /// </summary>
    public float 间隔 = 3;
    float time;
    public float 弹道speed = 1;
    [SerializeField]
    private float speed_Lv = 1;
    I_Speed_Is I_s => this;
 
    public E_方向 方向;

    public   bool 开开开开开开开开开;
  [SerializeField][DisplayOnly]  private bool 暂停1;

    private void FixedUpdate()
    {
        if (暂停) return;

        if (f.LastBullet==null)
        {
            触发();
        }
        else
        { 
 
                time += Time.fixedDeltaTime * f.LastBullet.真实移动速度;
 
        if (time > 间隔)
        {
            time = 0;

            触发();
        }
        float ff = time / 间隔;
        ff = Mathf.Clamp(ff, 0.2f, 1) * 2f;
        transform.localScale = new Vector3(transform.localScale.x, ff);
        }
    } 
    public void 触发()
    {
        f.初始化 +=  (Bullet_base b) => {
            if (生命周期!=0)
            {
            b.生命周期 = 生命周期; 
            }
            b.speed_Lv = Speed_Lv;
            b.A角速度 = Angle / Time.fixedDeltaTime;
        };
        f.发射一下();
    }
}
