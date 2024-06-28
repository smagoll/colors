using System;
using System.Threading;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI textTime;
    [SerializeField]
    private CanvasGroup canvasGroup;

    private Timer timer;
    private CancellationTokenSource cancellationTokenSource;

    private void UpdateTime(float time) => textTime.text = TimeSpan.FromSeconds(time).ToString(@"mm\:ss");

    public void Launch(float time, Action onFinish)
    {
        onFinish += HideTimer;
        cancellationTokenSource = new();
        ShowTimer();
        UpdateTime(time);
        timer = new Timer(time, 1, onFinish, UpdateTime);
        timer.Invoke(cancellationTokenSource.Token);
    }

    public void Stop()
    {
        cancellationTokenSource.Cancel();
        HideTimer();
    }
    
    private void ShowTimer() => canvasGroup.DOFade(1f, .5f);
    private void HideTimer() => canvasGroup.DOFade(0f, .5f);
}
