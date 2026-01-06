using Cysharp.Threading.Tasks;
using RenaissanceRestart;
using System.Threading;
using UnityEngine;




/// <summary>
/// 建议脚本播放器
/// </summary>
public class SamplePerformPlayer : MonoBehaviour, IPerformCore
{
    private CancellationTokenSource PerformToken;
    public CancellationToken AsyncToken => PerformToken.Token;

    SamplePerform Sp;
    private void Start()
    {
        //控制播放
        this.PerformToken = new CancellationTokenSource();

        //开始异步脚本
        Sp = new SamplePerform(this.transform);
        Sp.DoPerform(this, new Vector2Int(0,0)).Forget();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Sp.IsPressSkip = true;
        }
    }
    private void OnDestroy()
    {
        //如果播放没有完成,则销毁
        this.PerformToken?.Cancel();
        this.PerformToken?.Dispose();
        this.PerformToken = null;
    }
}
