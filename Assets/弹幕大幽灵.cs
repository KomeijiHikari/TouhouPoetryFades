using Boss;
using Ink.Parsed;
using SampleFSM;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using 发射器空间;


namespace Enemmy
{
    public class 弹幕大幽灵 : 泛用状态机
    {
        监控激活碰撞框 s;
        [SerializeField]
        List<发射器> Fs;

        [SerializeField]
        float 攻击范围 = 4;

        Enemy_base e;
        戒备 j;

        float 和发射点距离 = 3;
        float 间距 = 1;
        state idle = new state("idle");
        state atk = new state("atk");
        private void Awake()
        {
            gameObject.组件(ref s);
            e = GetComponent<Enemy_base>();
            j = GetComponent<戒备>();
        }

        float time;
    readonly    float    cooltimeMax = 1;
        private void Start()
        {
            当前 = idle;

            e.被打 += () => {
                to_state(idle);
            };
            s.是我 += (bool b) => {
                to_state(idle);
            };
             atk.Enter += () =>
            {
                time = cooltimeMax;
                e.an.Play(atk.StateName);
            };
            atk.FixStay += () =>
            {

                if (e.is_Dead)
                {
                    time = 0;
                    to_state(idle);
                    return;
                }    ;
                if (!j.发现玩家了吗_)
                {
                    to_state(idle);
                }
                Vector3 aaa = Player3.I.Bounds.九个点(E_方向.下);
                Debug.DrawLine(e.Bounds.center, aaa, Color.yellow);
                time -= Time.fixedDeltaTime*e.I_S.固定等级差;
                if (time < 0)
                {
                    time = cooltimeMax;
                    发射();
                }
            }; 
            idle.Enter += () =>
            {
                e.an.Play(idle.StateName);
            };
            idle.Stay += () =>
            {
                if (Time.time-  idle.time>0.6f)
                if (j.发现玩家了吗_)
                {
                    to_state(atk);
                } 
            }; 
        }
        public float F;
        public static void 旋转(Transform Targg, Vector3 MMY, float 目标角度)
        {
            if (Targg == null ) return;

            // 1. 计算当前My相对于Target的方向角度
            Vector2 当前相对向量 = Targg.position - MMY ;
            float 当前角度 = Mathf.Atan2(当前相对向量.y, 当前相对向量.x) * Mathf.Rad2Deg; 
            // 2. 如果当前角度已经等于目标角度，不需要旋转
            if (Mathf.Abs(Mathf.DeltaAngle(当前角度, 目标角度)) < 0.001f)
            {
                return; // 已经处于目标角度
            }

 

            // 5. 计算旋转后的位置
            // 获取当前距离（保持距离不变）
            float 当前距离 = 当前相对向量.magnitude;

            // 计算目标方向向量
            Vector2 目标方向 = new Vector2(
                Mathf.Cos(目标角度 * Mathf.Deg2Rad),
                Mathf.Sin(目标角度 * Mathf.Deg2Rad)
            );

            // 计算新的位置
            Vector3 新位置 = MMY  + (Vector3)(目标方向 * 当前距离);

            // 6. 应用新位置
            Targg.position = 新位置;
             
            // 7. 旋转My自身的rotation
            // 注意：这里绕世界坐标Z轴旋转，而不是局部旋转
            Targg.rotation  = Quaternion.Euler(new Vector3(0,0, 目标角度));
        }
        public Transform 旋转t;
        private void 发射()
        { 
            Vector3 aaa = Player3.I.Bounds.九个点(E_方向.下); 
            var Angle = Initialize.To_方向到角度(aaa - e.Bounds.center);
              //Angle =0;
            旋转(旋转t, e.Bounds.center, Angle);
            for (int i = 0; i < Fs.Count; i++)
            {  
                Fs[i].初始化 += (Bullet_base) =>
                { 
                    Bullet_base.A角速度 = Angle / Time.fixedDeltaTime;
                };

                Fs[i].发射一下();
            }
            return;
        var a=    Get(transform.position, Player3.I.transform.position, 和发射点距离, 间距, Fs.Count);
            for (int i = 0; i < a.Count; i++)
            {
                Fs[i].transform.position = a[i];
            }
    
                return; 
        }
        public static List<Vector2> GetA(Vector2 Start, Vector2 End,  float 距离,float 角度, int 数量)
        {
            List<Vector2> Out = new List<Vector2>(数量);
            return Out;
        }
        /// <summary> 
        /// </summary>
        /// <param name="Start"> 法线开始点</param>
        /// <param name="End">法线终点</param>
        /// <param name="juli">中心点距离</param>
        /// <param name="jianju"> 间距</param>
        /// <param name="数量"></param>
        /// <returns></returns>
        public static List<Vector2> Get(Vector2 Start, Vector2 End, float juli, float jianju, int 数量)
        {
            List<Vector2> Out = new List<Vector2>(数量);

            // 计算方向向量
            Vector2 方向 = End - Start;
            if (方向.sqrMagnitude < 0.0001f) 方向 = Vector2.right;
            else 方向.Normalize();

            // 计算法向量
            Vector2 法向量 = new Vector2(方向.y, -方向.x);

            // 基础位置
            Vector2 基础位置 = Start + 方向 * juli;

            // 根据数量调用不同的排列方法
            if (数量 <= 0) return Out;

            switch (数量)
            {
                case 1:
                    Out.Add(基础位置);
                    break;

                case 2:
                    Out.Add(基础位置 + 法向量 * jianju * 0.5f);
                    Out.Add(基础位置 - 法向量 * jianju * 0.5f);
                    break;

                default:
                    if (数量 % 2 == 1) // 奇数
                    {
                        生成奇数个点(Out, 基础位置, 法向量, jianju, 数量);
                    }
                    else // 偶数
                    {
                        生成偶数个点(Out, 基础位置, 法向量, jianju, 数量);
                    }
                    break;
            }

            return Out;
        }

        private static void 生成奇数个点(List<Vector2> 点列表, Vector2 基础位置, Vector2 法向量, float 间距, int 数量)
        {
            int 中心索引 = 数量 / 2;

            for (int i = 0; i < 数量; i++)
            {
                int 偏移索引 = i - 中心索引;
                Vector2 点位置 = 基础位置 + 法向量 * (偏移索引 * 间距);
                点列表.Add(点位置);
            }
        }

        private static void 生成偶数个点(List<Vector2> 点列表, Vector2 基础位置, Vector2 法向量, float 间距, int 数量)
        {
            int 半数量 = 数量 / 2;

            for (int i = 0; i < 数量; i++)
            {
                // 关键：使用 +0.5f 来确保点对称分布在中心两侧
                float 偏移量 = (i - 半数量 + 0.5f) * 间距;
                Vector2 点位置 = 基础位置 + 法向量 * 偏移量;
                点列表.Add(点位置);
            }
        }
    } 
}
