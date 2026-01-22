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

    [SerializeField]
    SpriteRenderer sp;
    public bool 暂停 { get; set; }
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
    public float 间隔 = 3;
    float time;
    public float 弹道speed = 1;
    [SerializeField]
    private float speed_Lv = 1;
    I_Speed_Is I => this;
 
    public E_方向 方向;
    private void FixedUpdate()
    {
        if (暂停) return; 
        time += Time.fixedDeltaTime * I.固定等级差;

        if (time > 间隔)
        {
            time = 0;

            触发();
        }
        float ff = time / 间隔;
        ff = Mathf.Clamp(ff, 0.2f, 1) * 2f;
        transform.localScale = new Vector3(transform.localScale.x, ff);
    } 
    public void 触发()
    {
        f.初始化 +=  (Bullet_base b) => {
            b.speed_Lv = Speed_Lv;
            b.A角速度 = Angle / Time.fixedDeltaTime;
        };
        f.发射一下();
    }
}
