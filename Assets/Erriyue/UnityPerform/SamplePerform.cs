using Cysharp.Threading.Tasks;
using RenaissanceRestart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SamplePerform : PerformBase
{
    public override string PerformName => "演示脚本";

    public Transform Player { get; }

    public SamplePerform(Transform likeplayer)
    {
        this.Player = likeplayer;
    }



    protected override async UniTask Run()
    {
        Debug.Log("演出开始, 等待3秒");
        //等待3秒
        await WaitTimes_Skipable(3, false);
        Debug.Log("等待3秒结束");

        int i = 0;
        await Skipable(() =>
        {
            Debug.Log("等待 i=" + i);
            i++;
            return i > 100;
        }, () =>
        {
            Debug.Log("提前跳过了, 当 i=" + i);
        }, false, false);


        //等待玩家到目的地
        var to = GetPosByOffset(10, 0, Player.position.z);
        await Waitable(() =>
        {
            var old = this.Player.position;
            this.Player.position = Vector3.MoveTowards(this.Player.position, to, 2 * Time.deltaTime);
            Debug.DrawLine(old, this.Player.position, Color.red, 2f);
            Debug.DrawRay(this.Player.position, Vector3.up, Color.red, 2f);

            if (Vector2.Distance(old, this.Player.position) <= 0.01f)
                return true;
            return false;
        });


        Debug.Log("演出结束");
    }
}
