using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleFSM;
using UnityEngine.Events;
namespace Enemmy
{
    public class 垃圾桶无头投掷 : 垃圾桶无头
    {
        [SerializeField]
        UnityEvent EVENt; 
        [SerializeField]
        发射器空间.发射器 f; 
        protected  override void Update()
        {
            base.Update();
            f.Speed_Lv = e.Speed_Lv;
        }
        protected override void ATKENTER()
        {
            idle.Enter += () =>
            {

                Debug.Log("从攻击切换到待机");
                if (e != null && e.an != null) e.an.Play( idle.StateName );

            };

            atk.Enter += () =>
            {
                Debug.Log("进入攻击状态");
                if (e != null && e.an != null) e.an.Play(  atk.StateName);
            };
        }
 override       protected void atkk()
        {
            EVENt?.Invoke();
        }
    }
}