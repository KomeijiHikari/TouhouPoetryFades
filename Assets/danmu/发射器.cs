using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace 发射器空间
{ 
    public enum 播放类型
    {
        启动持续播放,
        启动短暂播放,
        持续待播放,
        持续播放,
        不播放
    }
    public class 发射器 : MonoBehaviour
    {
        public 播放类型 播放类型_;
        [SerializeField ][DisplayOnly]
         播放类型 原播放类型_; 
        public bool 监控子弹=false; 
        public bool 按照方向来 = true;
        [SerializeField]
        [DisplayOnly]
        float 朝向_; 
        public Bullet数据 B; 
        float 朝向
        {
            get
            {
                return transform.lossyScale.x;
            }
        }
        [Button(" 刷新", ButtonSizes.Large)]
        public void 刷新()
        {
            重制();
        }
        float currentAngle; //发射角度
        float currentAngularVelocity; //容器

        public float 当前时间;

        public float Speed_Lv = 1f;
        float Spee => Speed_Lv / Player3.Public_Const_Speed;

        public float 玩家角度_;
        
        public float 玩家角度
        {
            get => Initialize.To_方向到角度(Player3.I.transform.position - transform.position);

        }
        private void OnEnable()
        { 
            if (启动就)
            {
                if (Time.time > 0.2f)
                {
                    重制();
                    if (!持续发射) 发射一下();
                }
            }  
        }
        private void Start()
        {
            重制(); 
        }
        public float 发射时间;

        public void 重制()
        {
            发射时间 = B.发射时间;

            if (按照方向来) 朝向_ = 朝向 == 1 ? 0 : 180;
            currentAngle = 朝向_ - B.InitRotation;
            currentAngularVelocity = B.SenderAngularVelocity;
            当前时间 = 0f; 
        }

        private void FixedUpdate()
        {

            if (按照方向来) 朝向_ = 朝向 == 1 ? 0 : 180;
            //if (!持续发射) return;

            bool 可以 = false;
            switch (播放类型_)
            {
                case 播放类型.启动持续播放:
                    可以 = true;
                    break;
                case 播放类型.启动短暂播放:
                    break;
                case 播放类型.持续播放:
                    break;
                case 播放类型.不播放:
                    break; 
            }
            if (!可以) return;
            玩家角度_ = 玩家角度;
            currentAngularVelocity = Mathf.Clamp(currentAngularVelocity + B.SenderAcceleration * Time.fixedDeltaTime * Spee, -B.发射器Max角速度, B.发射器Max角速度);
            currentAngle += currentAngularVelocity * Time.fixedDeltaTime * Spee;

            if (Mathf.Abs(currentAngle) > 720) currentAngle -= Mathf.Sign(currentAngle) * 360f;


            当前时间 += Time.fixedDeltaTime * Spee;
            if (当前时间 > B.发射间隔)
            {
                当前时间 -= B.发射间隔;

                发射组(B.Count, currentAngle);
            }
        }
        [Button("Play_", ButtonSizes.Large)]

        int TimeF;
        public void 发射一下()
        {
            if (TimeF != Time.frameCount)
            {
                TimeF = Time.frameCount;
                发射组(B.Count, currentAngle);
            }
        }
        void 发射组(int count, float angle)
        { 
            float temp = count % 2 == 0 ? angle + B.LineAnle / 2 : angle;

            for (int i = 0; i < count; ++i)
            {
                temp += Mathf.Pow(-1, i) * i * B.LineAnle;
                Send(temp);
            } 
        }
        /// <summary>
        /// 在指定区域内生成随机点（带最小间距）
        /// </summary>
        /// <param name="bounds">生成区域</param>
        /// <param name="count">生成点数</param>
        /// <param name="minDistance">点间最小距离</param>
        /// <param name="maxAttempts">最大尝试次数</param>
        /// <returns>随机点列表</returns>
        public static List<Vector2>盒子随机点(
            Bounds bounds,
            int count,
            float minDistance=1,
            int maxAttempts = 100)
        {
            List<Vector2> points = new List<Vector2>(count);
            int attempts = 0;

            while (points.Count < count && attempts < maxAttempts)
            {
                // 生成候选点
                float randomX = Random.Range(bounds.min.x, bounds.max.x);
                float randomY = Random.Range(bounds.min.y, bounds.max.y);
                Vector2 candidate = new Vector2(randomX, randomY);

                // 检查是否满足最小间距
                bool isValid = true;
                foreach (Vector2 existingPoint in points)
                {
                    if (Vector2.Distance(candidate, existingPoint) < minDistance)
                    {
                        isValid = false;
                        break;
                    }
                }

                // 如果有效则添加
                if (isValid)
                {
                    points.Add(candidate);
                    attempts = 0; // 重置尝试计数器
                }
                else
                {
                    attempts++;
                }
            }

            return points;
        }

        public float 初始距离 = 3f;

        [SerializeField ]
        float 试一下;
        [Button("星星弹幕_", ButtonSizes.Large)]
        public void 发射星星弹幕()
        {
            var a = 星星弹幕_(2, 1, 试一下);
            foreach (var item in a)
            {
                var S = Send(Initialize.To_方向到角度(item - transform.position));
                S.transform.position = item;
                S.L线速度 += (item - transform.position).magnitude/2;
                //GameObject obj = Surp_Pool.I.GetPool(Surp_Pool.能量子弹);
                //var Bb = obj.GetComponent<Bullet_base>();
                //初始化子弹(Bb);
                //obj.transform.position = item;

                //Bb.方向 = (item - transform.position).normalized; 
            }
        }
       
        List<Vector3> 星星弹幕_(float 半径, int 顶点内点数量,float 第一个点角度=0)
        {
            Vector3 center = transform.position;
            List<Vector3> points = new List<Vector3>();

            // 计算内圆半径（黄金分割比例）
            float innerRadius = 半径 * 0.382f;

            // 五角星有5个顶点和5个内点
            int totalPoints = 10;

            // 生成所有关键点（5个外点 + 5个内点）
            Vector3[] keyPoints = new Vector3[totalPoints];

            for (int i = 0; i < totalPoints; i++)
            {
                // 计算当前角度（五角星需要每72度一个顶点，但交错放置）
                float angleDeg = 第一个点角度+(i * 36f); // 36度一个点（10个点 * 36 = 360度）

                // 区分外点和内点（奇数索引为内点，偶数索引为外点）
                bool isOuterPoint = (i % 2 == 0);
                float currentRadius = isOuterPoint ? 半径 : innerRadius;

                // 转换为弧度
                float angleRad = angleDeg * Mathf.Deg2Rad;

                // 计算点位置（在XY平面）
                Vector3 point = center + new Vector3(
                    Mathf.Cos(angleRad) * currentRadius,
                    Mathf.Sin(angleRad) * currentRadius,
                    0
                );

                keyPoints[i] = point;
            }

            // 连接点形成五角星
            for (int i = 0; i < totalPoints; i++)
            {
                int nextIndex = (i + 1) % totalPoints;
                Vector3 startPoint = keyPoints[i];
                Vector3 endPoint = keyPoints[nextIndex];

                // 添加起点
                points.Add(startPoint);

                // 在两点之间插值生成中间点
                for (int j = 1; j < 顶点内点数量; j++)
                {
                    float t = (float)j / 顶点内点数量;
                    Vector3 midPoint = Vector3.Lerp(startPoint, endPoint, t);
                    points.Add(midPoint);
                }
            }

            return points;
        }
        public List<Bullet_base> 子弹列表; 
      void   子弹周期更新(Bullet_base a)
        {
            子弹列表.Remove (a);
            a.结束-= 子弹周期更新;
        }
        Bullet_base Send(float 角度  )
        { 
            var Bb = Surp_Pool.I.GetPool(Surp_Pool.能量子弹).GetComponent<Bullet_base>();

            初始化子弹(Bb);

            if (Bb.自身旋转)   Bb.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 角度)); 
            else  Bb.A角速度 += 角度 / (Time.fixedDeltaTime);

            Bb.transform.position = transform.position + (Vector3)Initialize.To_角度到方向(角度) * 初始距离;

            if (监控子弹)
            {
                子弹列表.Add(Bb);
                Bb.结束 += 子弹周期更新 ;
            }
            return Bb;
            //((Vector2)obj.transform.position).DraClirl(1, Color.cyan, 0.1f);

            //StartCoroutine(Initialize .Waite( ()=>{
            //    Debug.LogError("我出生了喵我出生了喵我        出生了喵我出生了喵  出生了喵我出生了喵  出生了喵我死了喵    " + obj.activeInHierarchy + obj.transform.position);
            //}));
            //obj.transform.position += transform.position + (Vector3)Initialize.To_角度到方向(角度) * 初始距离;
        }
        void 初始化子弹(Bullet_base Bb)
        {
            if (随机发射无法消弹子弹)
            {
                var a = ((Bullet)Bb);
                if (a != null)
                {
                    a.可以被消灭 = Initialize.RandomInt(1, 5) != 1;
                    if (a.可以被消灭)
                    {
                        a.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                    else
                    {
                        a.GetComponent<SpriteRenderer>().color = Color.red;
                    } 
                }
            }
            Bb.追踪玩家 = B.追踪玩家;

            Bb.Speed_Lv = Speed_Lv;

            Bb.L线速度 = B.LinearVelocity;
            Bb.A_Acc角加速度 = B.AngularAcceleration;
            Bb.A角速度 = B.AngularVelocity;


            Bb.生命周期 = B.生命周期;

            Bb.L_Acc线加速度 = B.Acceleration;

            Bb.Max速度 = B.Max速度;
            Bb.自身旋转 = B.自身旋转;
        }
        public bool 随机发射无法消弹子弹;

    }
}

