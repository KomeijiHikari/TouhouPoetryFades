using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SampleFSM
{
    public  partial    class 加速时间装置 : MonoBehaviour 
    {

        机关 机关_;
 [SerializeField ][DisplayOnly ]
        state 当前;


        state Null;
        state Up;

        public float 间隔_Enter = 3f;
    float 间隔 ;
        public float 进入最大持续时间;
        public float 目标Speed;

        float Target;
        float 原Speed_; 
        float Keeptime_;


 
        public float Fov缩放比例;

        BoxCollider2D bc;
        private void Awake()
        {
            bc = GetComponent<BoxCollider2D >();
            机关_ = GetComponent<机关>();
            Null = new state("Null"); 
            Up = new state("Up");
            当前 = Null;

            Up.Exite += Exite;
      Up.Enter += () =>
      {
          机关_.enabled = false;
          原Speed_ = Player3.Public_Const_Speed;

          Player3.I.HPROCK = true;
          变速(目标Speed , true);
          FSM.f.To_State(E_State.interaction);
          Keeptime_ = 0;
      };
            Up.Stay += () =>
            {
                if (Player_input.I.按键检测_松开(Player_input.I.k.交互))
                {
                    当前 = 当前.to_state(Null);
                } 
            };
            //Up.FixStay += () =>
            //{ 
            //    if (Keeptime_< 进入最大持续时间)
            //    {
            //        Keeptime_ += Time.fixedDeltaTime;
            //        float 比例 = Keeptime_ / 进入最大持续时间;
            //        Target = Mathf.Lerp(原Speed_, 目标Speed , 比例);
            //        变速(Target);

            //        if (Keeptime_ > 进入最大持续时间)
            //        {
            //            脉冲.I.End_File();
            //            TTime = 0;
            //            Keeptime_ = 进入最大持续时间;
            //            变速(Target);
            //            Player3.Public_Const_Speed = 目标Speed ;
            //        }
            //    } 
            //}; 
        }
        float TTime;
        void 变速(float f,bool Exite=true)
        {
            if (TTime==0)
            {
                间隔=0;
            }
            if (Time .time - TTime > 间隔)
            { 
                间隔+=  间隔_Enter ;
                  TTime = Time.time;

                if (Exite)    摄像机.I.FOV_缓动至(摄像机.I.Fov * Fov缩放比例, 间隔 * 4 / 5); 
                else    摄像机.I.FOV_还原(); 

                    脉冲.I.File(Player3.I.transform.position, 0.08f, false, 间隔  ,0.3f);
                Player3.I.SetSpeed(f);
                //Player3.Public_Const_Speed = f;
            } 
        }

        private void FixedUpdate()
        {
            当前?.FixStay?.Invoke();
        }
        [SerializeField ][DisplayOnly ]
        float Speed显示;
      void Update()
        {
            Speed显示 = Player3.Public_Const_Speed;
            当前?. Stay?.Invoke();
        }

        void  Exite ()
        {

            Keeptime_ = 0;
            Target = 原Speed_;
            脉冲.I.End_File();
            TTime = 0;
            变速(Target, false);
            机关_.enabled = true;
            Player3.I.HPROCK = false;
            机关_.适应.gameObject.SetActive(true);
            FSM.f.To_State(E_State.idle);
        }
        public void Event_进入()
        {
            if (FSM.f.I_State_C .state ==E_State.idle)
            {
                机关_.适应.gameObject.SetActive(false );
               当前= 当前.to_state(Up);
            }
        
        }

    }

    public partial class 加速时间装置
    {
 
        [Space(3)]
        public Animator 分;
        public Animator 时;

        private void Start()
        {
            时.Play("时");
            分.Play("分");
        }
        private void LateUpdate()
        {
            bc.enabled = Player3.I.N_.时缓;

            分.speed = 1 / Player3.Public_Const_Speed;
            时.speed =(1 / Player3.Public_Const_Speed)  / 60;
        }
    }
}
