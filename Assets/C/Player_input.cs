using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Key
{
    public KeyCode my_Key_;

    [SerializeField]
    [DisplayOnly ]
    float Keytime_;
    [SerializeField]
    [DisplayOnly]
    float KeytimeDown_;
    [SerializeField]
    [DisplayOnly]
    float KeytimeUp_;
    public float Keytime { get { return Keytime_; }set { Keytime_ = value; } }

    public float KeytimeDown { get { return KeytimeDown_; } set { KeytimeDown_ = value; } }
    public float KeytimeUp { get { return KeytimeUp_; } set { KeytimeUp_ = value; } }


    public float Keeptime;
    [SerializeField]
    bool 被按下了吗_;
    public bool 被按下了吗
    {
        get { return 被按下了吗_; }
        set
        {
            if (!被按下了吗_ && value)
            {
                down_State = 0;
            }
            被按下了吗_ = value;
        }
    }
    [SerializeField]
    public float yes_State;
    [SerializeField]
    public float no_State;
    [SerializeField]
    public float down_State;
    public KeyCode my_Key
    {
        get
        {

            return my_Key_;
        }
        set
        {
            my_Key_ = value;
        }
    }

    public Key(KeyCode K)
    {
        my_Key = K;
    }

    public void 更新时间(int i)
    {
        switch (i)
        {
            case 1:
                KeytimeDown =Player_input.I.Now_Time;
                break;
            case 2:
                Keytime = Player_input.I.Now_Time;
                break;
            case 3:
                KeytimeUp = Player_input.I.Now_Time;
                break;
        }
        Keeptime = Keytime - KeytimeDown;
    }
}
[DefaultExecutionOrder(-10)]



public class  Input_base: MonoBehaviour
{
public    bool 输入开关_ = true;
    public  virtual  bool 输入开关
    {
        get
        {
            return 输入开关_;
        }
        set
        {
          
            输入开关_ = value;
        }
    }

    public   KeyCode 上 = KeyCode.W;
    public KeyCode 下 = KeyCode.S;
    public KeyCode 左 = KeyCode.A;
    public KeyCode 右 = KeyCode.D;

    public Action<KeyCode> KeyUp { get; set; }
    public Action<KeyCode> KeyDown { get; set; }
    public Action<KeyCode> KeyState { get; set; }
public   Dictionary<KeyCode, Key> D_I = new Dictionary<KeyCode, Key>();
    public List<KeyCode> 玩家输入的按键顺序_显示 = new List<KeyCode>();
    public LinkedList<KeyCode> 玩家输入的按键存储栈 = new LinkedList<KeyCode>();
    public List<Key> 所有的按键列表;

    public Key Get_key(KeyCode k)
    {
        return D_I[k];
    }
    public List<KeyCode> 玩家输入的按键存储_按住 { get; set; } = new List<KeyCode>();
    public List<KeyCode> 玩家输入的按键存储_按下 = new List<KeyCode>();
    public List<KeyCode> 玩家输入的按键存储_松开 = new List<KeyCode>();
    public int 玩家输入的按键栈的长度 = 5;
    public float Now_Time_;
    public float Now_Time
    {
        get => Now_Time_;
        set => Now_Time_ = value;
    }

    public KeyCode getKeyDownCode()
    {

        Debug.Log(Input.GetAxisRaw("Horizontal"));
        Debug.Log(Input.GetAxisRaw("Vertical"));

        Debug.Log(Input.GetAxis("LT"));
        Debug.Log(Input.GetAxis("RT"));
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    Debug.Log(keyCode.ToString());
                    return keyCode;
                }
            }
        }
        return KeyCode.None;
    }
    public void New_(KeyCode K)
    {
        Key key;
        if (!D_I.TryGetValue(K, out key))
        {
            key = new Key(K);
            D_I.Add(K, key);
        }
        else
        {
            Debug.Log("键位重复");
        }
    }


    public bool 按键检测_按下(KeyCode K)
    {

        if ( 玩家输入的按键存储_按下.Contains(K))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool 按键检测_按住(KeyCode K)
    {

        if ( 玩家输入的按键存储_按住.Contains(K))
        {

            return true;
        }
        else
        {
            return false;
        }
    }
    public bool 按键检测_松开(KeyCode K)
    {

        if ( 玩家输入的按键存储_松开.Contains(K))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
public class Player_input : Input_base
{
    public override bool 输入开关
    {
        get
        {
            return 输入开关_;
        }
        set
        {
            if (!value && 输入开关_)
            {
                关掉();
            }
            //else if (value && !输入开关_)
            //{

            //}
            输入开关_ = value;
        }
    }

    public Key 跳;

    public  Action<int>方向变动 { get;  set; }

    public int 竖直正负零;

    public KeyCode 地图 = KeyCode.M;
    public KeyCode 攻击 = KeyCode.J;
    public KeyCode 跳跃 = KeyCode.Space;
    public KeyCode 冲刺 = KeyCode.LeftShift;
    public KeyCode 格挡 = KeyCode.K;
    public KeyCode 交互 = KeyCode.E ;
    public KeyCode 变速 = KeyCode.F;


    public  Vector2 输入
    {
        get
        {
            return new Vector2 (方向正零负 ,竖直正负零 );
        }
    }



    public  static Player_input I { get; private set; }

    [DisplayOnly]
    [SerializeField ]
    private int 方向正零负数_ = 0;
    [DisplayOnly]
    [SerializeField]
   int 方向正负_=1;
 
    public  int 方向正负
    {
        get
        {
            return Player_input.I.方向正负_;
        }
      private  set
        {
            if (value ==0)
            {
                Debug.LogError("我了个去");
            }
            if (Player_input.I.方向正负_ != value)
            {
                I.方向变动?.Invoke(value);
            }
            Player_input.I.方向正负_ = value;
        }
    }
    void 关掉()
    {
        foreach (var item in 玩家输入的按键存储_按住)
        {
            玩家输入的按键存储_松开.Add(item);
            KeyUp?.Invoke(item); 
            方向正零负 = 0;
 

           D_I[item].更新时间(3);
            D_I[item].被按下了吗 = false;
        }
    }
    //public void 检查朝向是否一致( int a)
    //{
    //    if (a!= 方向正负_)
    //    {
    //        方向变动?.Invoke(方向正负_);
    //    }
    //}
    //松开左右持续的时间

    /// <summary>
    /// 反向
    /// </summary>
    public static void 假装相反方向键( )
    {
        if (I.方向正负 == 1)
        {
            假装方向键(false);
        }
        else if (I.方向正负 == -1)
        {
            假装方向键(true);
        }
    }
    public static  void 假装方向键(bool b)
    {
        Debug.Log(" 假装方向键"+b);
        if (b)
        {
            I.方向正零负 = 1;
            I.方向正负_ = 1;
        }
        else
        {
            I.方向正零负 = -1;
            I.方向正负_ = -1;
        }
    }
    public float 方向正零负计时器=0;
    //按下左右持续的时间
    public float 方向正零负_非零计时器 = 0;

    public int 水平操作_ { get => 方向正零负数_; set => 方向正零负数_ = value; }
    public  int 方向正零负
    {
        get
        {
            return 水平操作_;
        }
       private  set
        {
            if (水平操作_!=0)
            {
                方向正零负计时器 = 0;
                方向正零负_非零计时器 += Time.fixedDeltaTime;
            }
            else
            {
                方向正零负计时器 += Time.fixedDeltaTime;
                方向正零负_非零计时器 = 0;

            }
            Player_input.I.水平操作_ = value;
        }
    }



    public  float 水平操作插值 = 0;


 

    private void Awake()
    {

        if (I != null/*&&I!=this*/)
        {
            Destroy(this);
        }
        else
        {
            I = this;
        }
        New_(上);
        New_(下);
        New_(右);
        New_(左);

        New_(地图);
        New_(攻击);
        New_(跳跃);
        New_(冲刺);
        New_(格挡);
        New_(交互);
        New_(变速);

        跳 = D_I[跳跃];
        foreach (Key D in D_I.Values)
        {
            所有的按键列表.Add(D);
        }
    }

    //public void ASDAD(KeyCode  code)
    //{
    //    CustomEvent.Trigger(Player2, "下", D.Key);
    //}
 
    void 水平方向更新()
    {
        if (按键检测_按住(左) && 按键检测_按住(右))
        {
            方向正零负 = 0;
            return;
        }
        else if (按键检测_按住(左))
        {

            方向正负 = -1;
            方向正零负 = -1;
        }
        else if (按键检测_按住(右))
        {

            方向正负 = 1;
            方向正零负 = 1;
        }
        else
        {
            方向正零负 = 0;
        }

        水平操作插值 = Mathf.Lerp(水平操作插值, 方向正零负, 0.2f * Time.deltaTime);
    }
    private void Update()
    {

        //if (true)
        //{
        //    getKeyDownCode();
        //}


        I.玩家输入的按键存储_按下.Clear();
        I.玩家输入的按键存储_松开.Clear();


        
            foreach (KeyValuePair<KeyCode, Key> D in D_I)
        {

      
                if (Input.GetKeyDown(D.Key))///玩家输入了范围中的按键
            {
                if (!输入开关) return;
                 
                KeyDown?.Invoke(D.Key);
             
                    D_I[D.Key].更新时间(1);
                    玩家输入的按键存储_按下.Add(D.Key);
                    玩家输入的按键存储栈.AddLast(D.Key);
                    玩家输入的按键顺序_显示.Clear();
                    foreach (KeyCode item in 玩家输入的按键存储栈)
                    {
                        玩家输入的按键顺序_显示.Add(item);

                    }
                }

                if (Input.GetKey(D.Key))
                {
                if (!输入开关) return;
                if (!玩家输入的按键存储_按住.Contains(D.Key))
                {
                    玩家输入的按键存储_按住.Add(D.Key);
                }
                KeyState?.Invoke(D.Key);
                    D_I[D.Key].更新时间(2);
                D_I[D.Key].被按下了吗 = true;
                }

                if (Input.GetKeyUp(D.Key))
                {

                    KeyUp?.Invoke(D.Key);
                    玩家输入的按键存储_按住.Remove(D.Key);
                    D_I[D.Key].更新时间(3);
                    玩家输入的按键存储_松开.Add(D.Key);

                D_I[D.Key].被按下了吗 = false;
            }

                if (玩家输入的按键存储栈.Count > 玩家输入的按键栈的长度 - 1)
                {
                    玩家输入的按键存储栈.RemoveFirst();
                }

            }

            if (!Input.anyKey)
            {
            玩家输入的按键存储_按下.Clear();
            玩家输入的按键存储_按住.Clear();
            }
        
        水平方向更新();

        竖直方向更新();


        Now_Time += Time.deltaTime;
        foreach (KeyValuePair<KeyCode, Key> D in D_I)
        {
            D_I[D.Key].down_State += Time.deltaTime;
            if (D_I[D.Key].被按下了吗)
            {
                D_I[D.Key].yes_State += Time.deltaTime;
                D_I[D.Key].no_State = 0;
            }
            else
            {
                D_I[D.Key].no_State += Time.deltaTime;
                D_I[D.Key].yes_State = 0;
            }
        }
    }
    /// <summary>
    /// 获取玩家输入的方法
    /// </summary>
    /// <returns></returns>
    void 竖直方向更新()
    {
        if (按键检测_按住(上)&& 按键检测_按住(下))
        {
            竖直正负零 = 0;
        }
        else  if(按键检测_按住(上))
        {
            竖直正负零 = 1;
        }
        else if (按键检测_按住(下))
        {
            竖直正负零 = -1;
        }
        else
        {
            竖直正负零 = 0;
        } 
    }
}




