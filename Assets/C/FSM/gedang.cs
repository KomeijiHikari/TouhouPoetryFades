using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// <summary>
/// 触发防御后   在规定时间内J防反
/// 防反动画时间范围内J   攻击
/// </summary>
/// 
public class gedang : State_Base
{
    public override bool 可以切换嘛()
    {
        return false;
        //return Player.N_.格挡; 
    }
    public Enemy_base E;
    public Fly_Ground Fly;

    //float 格挡时间 = 0.5f;
    float Enter_TTime;
    float TTime;
 
    public override void EnterState()
    {
      Fly = null;
        E = null;
        TTime = 0;
         
        Player.Velocity = Vector2.zero;
        A.Playanim(A_N .gedang_idle_to0);

        Player.防御状态 = Player3.防御.开始防御;
    }
    public override void 松开(KeyCode obj)
    {
        if (obj==IP.k.格挡)
        {
            if (IP.水平操作_==0)
            {
                Player.方向更新();
                f.To_State(E_State.idle); 
            }
            else
            {
                Player.方向更新();
                f.To_State(E_State.run);
            }
        }
    }
    public override void 方向改变(bool obj) { }
    public override bool Hit_Func(GameObject obj)
    {
        bool 朝向正确 = Mathf.Sign((obj.transform.position - Player.transform.position).x) ==Player. LocalScaleX_Set;
        if (!朝向正确) return true;

      var a = obj.GetComponent<Fly_Ground >();
        if (a!=null)
        {
             Fly = a;
            TTime = a.TTime1;
            Enter_TTime = Time.time;
       
        }
    var e=   obj.GetComponent<Enemy_base>();
        if (e!=null)
        { 
            E = e;  
        }

 
        f.教学模式();
        A.Playanim(A_N.gedang_A_to0);
        var tt = 特效_pool_2.I.GetPool(Player.gameObject, T_N.特效防御);
        tt.同步玩家 = true;
        return false;
    }
    public override void 按下(KeyCode obj)
    {
        if (obj==IP.k.攻击)
        {
            if ( Fly != null)
            {
 
                if (Time.time < Enter_TTime + TTime)
                {
 
                    if ( Fly.gameObject.activeInHierarchy)
                    {
 
                        f.To_State(E_State.counter );
                        return;
                    }
                } 
            }  
        }
    }
}
 
