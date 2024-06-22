using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Timer
{
    private bool isRunning;
    private readonly Action onFinish;
    private readonly Action<float> onTick;
    private int interval;
    private float currentTime;

    public Timer(float time, int interval, Action onFinish = null, Action<float> onTick = null)
    {
        this.onFinish = onFinish;
        isRunning = false;
        this.interval = interval;
        currentTime = time;
        this.onTick = onTick;
    }

    public void Invoke(CancellationToken cancellationToken)
    {
        if (!isRunning)
        {
            isRunning = true;
            Runner(cancellationToken).Forget();
        }
    }

    public async UniTask Runner(CancellationToken cancellationToken)
    {
            while (currentTime > 0 && isRunning)
            {
                await UniTask.Delay(interval * 1000, cancellationToken: cancellationToken);
                currentTime -= interval;
                onTick?.Invoke(currentTime);

                if (cancellationToken.IsCancellationRequested)
                {
                    Stop();
                }
            }
            Stop();
    }

    public void  Stop()
    { 
        isRunning = false;
        onFinish?.Invoke();
    }
}
