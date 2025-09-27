using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using UnityEngine.Events;
using Cinemachine;
namespace 发射器空间
{ 
    public enum 播放类型
    {
        启动短暂播放,
        启动持续播放,
        持续待播放,
        持续播放,
        不播放
    }
    /// <summary>
    ///  只释放形状
    /// </summary>
    public class 发射器 : MonoBehaviour
    {
        CinemachineDollyCart C;
        CinemachineSmoothPath P;
        public 播放类型 当前播放类型_; 
        public 播放类型 原播放类型_; 
        public bool 监控子弹=false; 
        public bool 按照方向来 = true;
        [SerializeField]
        [DisplayOnly]
        float 朝向_; 
        public Bullet数据 B; 
        float 朝向X
        {
            get
            {
                return transform.lossyScale.x;
            }
        }
        [Button(" 刷新", ButtonSizes.Large)]
 
        [SerializeField]
        float currentAngle; //发射角度
        [SerializeField][DisplayOnly]
        float ResultAngle; //发射角度
        [SerializeField] float currentAngularVelocity; //容器

        public float 当前时间;

        public float Speed_Lv = 1f;
        float Spee => Speed_Lv / Player3.Public_Const_Speed;

        public float 玩家角度_;
        
        public float 玩家角度
        {
            get => Initialize.To_方向到角度(Player3.I.transform.position - transform.position);

        }
        public float ResultAngle1 { get => ResultAngle; set {
                if (Deb)           if (value != ResultAngle)     Debug.LogError(ResultAngle); 
                ResultAngle  = value;
            }  }
        public bool Deb;
        public int 次数;
        private void OnEnable()
        {
            次数 = 0;
            switch (原播放类型_)
            {
                case 播放类型.启动持续播放:
                    当前播放类型_ = 播放类型.持续播放;
                    StartCoroutine(Initialize .Waite(() => 当前时间 = B.发射间隔));
 
                    break;
                case 播放类型.启动短暂播放:
                    重制();
                    次数++;
                    发射?.Invoke();
                    break; 
            }
            //Debug.LogError("id OnEnable()id OnEnable()id OnEnable()id OnEnable()                                         AAAA"+子弹列表 .Count);
        }
        public void 试一啊下(SpriteRenderer  s  )
        {

        }
        private void Start()
        {
            if (B.pre.GetComponent<Bullet_base>() == null)
                弹幕形式发射重力子弹 = true;
                重制(); 
        }
        public float 发射时间;

        public void 重制()
        {
            发射时间 = B.发射时间;

            方向刷新();
            if (B != null)
            {
                currentAngle = B.InitRotation;
                currentAngularVelocity = B.SenderAngularVelocity;
            }

            当前时间 = 0f;
        }

        private void 方向刷新()
        {
            if (按照方向来)
            {

                朝向_ = 1;

                if (Initialize.返回正负号(朝向X) == 1)
                {
                    朝向_ = 0;
                }
                else
                {
                    朝向_ = 180;
                }
            }
        }

        public SpriteRenderer SP;
        private void FixedUpdate()
        {
            if (SP=null)
            {
                SP.transform.rotation = Quaternion.Euler(new Vector3 (0,0,1f)*Time.fixedDeltaTime*20f);
            }

            方向刷新();

            bool 可以 = false;
            switch (当前播放类型_)
            { 
                case 播放类型.持续播放:
                    可以 = true;
                    break; 
            }
            if (!可以) return;
 
                //currentAngle = 朝向_ - B.InitRotation;
                玩家角度_ = 玩家角度;
                currentAngularVelocity = Mathf.Clamp( currentAngularVelocity +(B.SenderAcceleration * Time.fixedDeltaTime * Spee) , -B.发射器Max角速度, B.发射器Max角速度);
                currentAngle += currentAngularVelocity * Time.fixedDeltaTime * Spee;
 
                if (Mathf.Abs(ResultAngle1) > 720)currentAngle -= Mathf.Sign(currentAngle) * 360f;
            ResultAngle1 = 朝向_ - currentAngle; 

            当前时间 += Time.fixedDeltaTime * Spee;
            if (当前时间 > B.发射间隔)
            {
                当前时间 -= B.发射间隔;

                次数++;
                 
                发射?.Invoke();
            } 
        }


        int TimeF;

        public UnityEvent 发射;
         
        [Button("Play_", ButtonSizes.Large)]
        public void 发射一下()
        {
            if (原播放类型_==播放类型.持续待播放)
            {
                当前播放类型_ = 播放类型.持续播放;
            }
            if (TimeF != Time.frameCount)
            {
                TimeF = Time.frameCount;
                发射组(B.Count, ResultAngle1);
            }
        }
        public void 轨迹发射(CinemachineSmoothPath c  )
        {
            var a = c.m_Waypoints;
            List<Vector2> 列表=new List<Vector2>();
            for (int i = 0; i < a.Length; i++)
            {

              var TT = transform .TransformPoint(a[i].position);
                Send().transform.position = TT;
                列表.Add(TT);
            }

            var R = Initialize.多线段均匀插点(列表, B.Count);
            foreach (var i in R)
            {
                Send().transform.position = i;
            } 
        } 
        public void 发射组_p_定位_()
        {
            var a = Initialize.中间并列点(Player3.I.transform.position, B.Count, B.LineAnle);
            for (int i = 0; i < B.Count; ++i)
            {
                a[i].DraClirl(1,Color .blue);
                if (Mathf.Abs(a[i].x - Boss.魔理沙.I.transform.position.x) < 5) continue;
                var VVV = a[i];
                Phy aa = SendP();
                aa.目标炮(VVV, B.生命周期); 
            }
        }
        [Button("重力跟踪", ButtonSizes.Large)]
      public   void 蘑菇孢子发射组_p_定位(  )
        {
            var a = Initialize . 中间并列点(Player3.I.transform.position, B.Count, B.LineAnle);
            for (int i = 0; i < B.Count; ++i)
            {
                if (Mathf.Abs(a[i].x - Boss.魔理沙.I.transform.position.x) < 5) continue;
                var VVV = a[i]; 
                Phy aa = SendP();
                aa.目标炮(VVV, B.生命周期);

               var P= aa.GetComponent<Phy_检测>();
                P.Enter += () =>
                {
                    if (P.Rs .Length >=1)
                    {
                        var ccc = P.Rs[0].collider.gameObject;
                        if (ccc.layer == Initialize.L_Ground)
                        {
                            var Targgget = P.Rs[0].point;
                            Boss.蘑菇管理.I.从这里升起蘑菇(Targgget);
                        }
                    }
            
                };
            } 
        } 

        void 发射组(int count, float angle)
        { 
            float temp = count % 2 == 0 ? angle + B.LineAnle / 2 : angle;
        
            for (int i = 0; i < count; ++i)
            {
                temp += Mathf.Pow(-1, i) * i * B.LineAnle;
                if (弹幕形式发射重力子弹) SendP(temp);

                else  Send(temp);
            } 
        }
      [SerializeField ][DisplayOnly]    bool 弹幕形式发射重力子弹;
        public static List<Vector2> 平均点(Bounds bounds, int count)
        {
            List<Vector2> points = new List<Vector2>(count);

            // 计算最佳的行列数（保持宽高比）
            float aspectRatio = bounds.size.x / bounds.size.y;
            int rows = Mathf.RoundToInt(Mathf.Sqrt(count / aspectRatio));
            int columns = Mathf.CeilToInt(count / (float)rows);

            // 计算单元格尺寸
            float cellWidth = bounds.size.x / columns;
            float cellHeight = bounds.size.y / rows;

            // 起始位置（左下角）
            Vector2 startPos = new Vector2(
                bounds.min.x + cellWidth / 2f,
                bounds.min.y + cellHeight / 2f
            );

            // 生成蛇形网格点
            for (int y = 0; y < rows; y++)
            {
                // 判断当前行是正向还是反向
                bool reverse = y % 2 == 1; // 奇数行反向

                for (int x = 0; x < columns; x++)
                {
                    // 计算实际列索引（蛇形顺序）
                    int actualX = reverse ? (columns - 1 - x) : x;

                    // 计算当前点位置
                    Vector2 point = new Vector2(
                        startPos.x + actualX * cellWidth,
                        startPos.y + y * cellHeight
                    );

                    points.Add(point);

                    // 达到所需点数时停止
                    if (points.Count >= count) break;
                }

                // 达到所需点数时停止
                if (points.Count >= count) break;
            }
        
            return points;
        }


        public static void 平均点Debug(List<Vector2> points, float pointSize = 0.1f)
        {
#if UNITY_EDITOR
            foreach (Vector2 point in points)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(new Vector3(point.x, point.y, 0), pointSize);
            }
#endif
        }
        float 返回不变形速度(Vector2 m,Vector2 y, float Tim=1)
        {
            var 距离 = Mathf.Abs(((Vector2)m - y).magnitude);
             return   距离 / Tim ;
        }
        public void 发射盒子平均点_重力(SpriteRenderer sp)
        {
            var a = 平均点(sp.bounds, B.Count);
            for (int i = 0; i < a.Count; i++)
            { 
                a[i].DraClirl();
                var S = SendP();
                S.transform.position = Boss.魔理沙.I.transform.position;
                var aaa = Initialize.抛物线_Get矢量(a[i] - (Vector2)S.transform.position, 2f, S.G);
                S.Set_RealVelo(aaa);
                Initialize_Mono.I.Waite_同速(() => { S.transform.position.DraClirl(); }, 2); 
            }
        }
        /// <summary>
        /// 盒子太小就不会发射
        /// </summary>
        /// <param name="sp"></param>
        public void 发射盒子平均点(SpriteRenderer sp)
        {
            var a =  平均点(sp.bounds, B.Count);  
            for (int i = 0; i < a.Count; i++)
            {  
                a[i].DraClirl();
                var Bb = Send();
                Bb.A角速度 = Bullet_base.方向转角度(a[i], Boss.魔理沙.I.transform.position);
                Bb.transform.position = Boss.魔理沙.I.transform.position; 

                Bb.L线速度 = 返回不变形速度(Bb.transform.position, a[i],  0.8f);

                Bb.Add(0.8f, () =>
              {
                  Bb.L线速度 = 0;
              });
            } 
        }
        public  void  发射盒子随机点(SpriteRenderer sp)
        {
            var a = 盒子随机点(sp.bounds ,B.Count,B .LineAnle);
            for (int i = 0; i < a.Count; i++)
            {
                var B = Send(ResultAngle);
                B.transform.position = a[i];
               
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
        /// 
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
                float randomX = UnityEngine . Random.Range(bounds.min.x, bounds.max.x);
                float randomY =  UnityEngine .Random.Range(bounds.min.y, bounds.max.y);
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
            var a = 星星弹幕_(初始距离, B.Count, ResultAngle1, 1, true ,false ,0.5f );
            foreach (var item in a)
            {
                var S = Send(Initialize.To_方向到角度(item - transform.position));
                S.transform.position = transform.position;
                S.L线速度 = 返回不变形速度(S.transform .position ,item, S.L线速度);
                //S.L线速度 += (item - transform.position).magnitude/2; 
            }
        }
        List<Vector3> 星星弹幕_(
   float 半径,
   int 均匀点数,
   float 第一个点角度 = 0,
   int N = 0,
   bool 连接非相邻内点 = false,
   bool 连接相邻内点 = false,
   float 内半径比例 = 0.5f)
        { 
            N += 4;  // 实际顶点数 = N + 4

            Vector3 center = transform.position;
            List<Vector3> points = new List<Vector3>();

            // 计算内圆半径（限制在10%-90%之间）
            float innerRadius = 半径 * Mathf.Clamp(内半径比例, 0.1f, 0.9f);

            // 总关键点数 = 2N (N个外点 + N个内点)
            int totalKeyPoints = 2 * N;

            // 生成所有关键点
            Vector3[] keyPoints = new Vector3[totalKeyPoints];
            float angleStep = 360f / totalKeyPoints;

            for (int i = 0; i < totalKeyPoints; i++)
            {
                float angleDeg = 第一个点角度 + (i * angleStep);
                bool isOuterPoint = (i % 2 == 0);
                float currentRadius = isOuterPoint ? 半径 : innerRadius;
                float angleRad = angleDeg * Mathf.Deg2Rad;

                keyPoints[i] = center + new Vector3(
                    Mathf.Cos(angleRad) * currentRadius,
                    Mathf.Sin(angleRad) * currentRadius,
                    0
                );
            }

            // 连接外点形成星形主体
            for (int i = 0; i < totalKeyPoints; i++)
            {
                int nextIndex = (i + 1) % totalKeyPoints;
                Vector3 startPoint = keyPoints[i];
                Vector3 endPoint = keyPoints[nextIndex];

                points.Add(startPoint);
                for (int j = 1; j < 均匀点数; j++)
                {
                    float t = (float)j / 均匀点数;
                    points.Add(Vector3.Lerp(startPoint, endPoint, t));
                }
            }

            // 收集所有内点（索引为奇数的点）
            List<Vector3> innerPoints = new List<Vector3>();
            for (int i = 1; i < totalKeyPoints; i += 2)
            {
                innerPoints.Add(keyPoints[i]);
            }
            // 连接非相邻内点（形成星形图案）[核心修改]
            if (连接非相邻内点)
            {    
                for (int i = 0; i < innerPoints.Count; i++)
                { 
                    List<Vector3> AA = new List<Vector3>();
                    List<int> UnI = new List<int>();
 
                    ///三个不相邻的点
                    
                    UnI.Add(i);
                    UnI.Add(Initialize.头尾(N,i,1));
                    UnI.Add(Initialize.头尾(N, i, -1)); 
                    if (N!=4&&!Initialize.是奇数(N)) UnI.Add(Initialize.头尾(N, i, N/2)); 

                    for (int iq = 0; iq < innerPoints.Count; iq++)
                    { 
                        if (UnI.Contains(iq)) continue;
                        AA.AddRange(Initialize.单线段插值(innerPoints[i], innerPoints[iq], 均匀点数)); 
                    }
                    Vector3 startPoint = innerPoints[i];
                    points.AddRange(AA); 
                }
            } 
            // 连接相邻内点（形成内多边形）
            if (连接相邻内点)
            { 
                for (int i = 0; i < innerPoints.Count; i++)
                {
                    int nextIndex = (i + 1) % innerPoints.Count;
                    Vector3 startPoint = innerPoints[i];
                    Vector3 endPoint = innerPoints[nextIndex];

                    points.Add(startPoint);
                    for (int j = 1; j < 均匀点数; j++)
                    {
                        float t = (float)j / 均匀点数;
                        points.Add(Vector3.Lerp(startPoint, endPoint, t));
                    }
                }
            } 
            // 闭合图形：添加第一个点
            points.Add(keyPoints[0]); 
            return points;
        } 
        public List<Bullet_base> 子弹列表; 
      void   子弹周期更新(Bullet_base a)
        {
            子弹列表.Remove (a);
            a.结束-= 子弹周期更新;
        }
        Phy SendP(float 角度 = 0)
        {
            var Bb = Surp_Pool.I.GetPool(B.pre.name).GetComponent<Phy>();
            var B_ = Bb.GetComponent<Phy_检测>();
            Bb.transform.position = transform.position;
            B_.Enter +=()=>{
                for (int i = 0; i < B_.Rs.Length; i++)
                {
                    var aa = B_.Rs[i];
                    if (aa.collider.gameObject.layer == Initialize.L_Ground)
                    {
                        特效_pool_2.I.GetPool(B_.transform.position, T_N.特效闪光爆炸);
                        Surp_Pool.I.ReturnPool(B_.gameObject, B.pre.name);
                    }
                    else if (aa.collider.gameObject.layer == Initialize.L_Player)
                    {
                        Initialize_Mono.I.Waite_同速(() =>
                        {
                            特效_pool_2.I.GetPool(B_.transform.position, T_N.特效闪光爆炸);
                            Surp_Pool.I.ReturnPool(B_.gameObject, B.pre.name);
                        }, 0.1f);
                    }
                } 
            };

            var 方向 = (Vector3)Initialize.To_角度到方向(角度);
            Bb.transform.position = transform.position + 方向 * 初始距离;



            Bb.Velocity = 方向 * B.LinearVelocity;
            return Bb;
        }
        Bullet_base Send(float 角度=0  )
        { 

            var Bb = Surp_Pool.I.GetPool( B.pre .name).GetComponent<Bullet_base>();

            初始化子弹(Bb);

            if (Bb.自身旋转)   Bb.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 角度)); 
            else  Bb.A角速度 += 角度 / (Time.fixedDeltaTime);

            Bb.transform.position = transform.position + (Vector3)Initialize.To_角度到方向(角度) * 初始距离;

            if (监控子弹)
            {
                子弹列表.Add(Bb);
                Bb.结束 += 子弹周期更新 ;
                if (子弹列表.Count==0)
                {
                    列表归零?.Invoke();
                }
            }

            初始化?.Invoke(Bb);
            return Bb;
 
        }
        public Action 列表归零;
        public Action<Bullet_base> 初始化;
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

