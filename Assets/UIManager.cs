using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TimerUI timer;

    public WindowComplete windowComplete;
    public WindowLose windowLose;
    
    private void LaunchTimer(Level level)
    {
        var time = level.Difficult switch
        {
            Difficult.Easy => 30,
            Difficult.Medium => 60,
            Difficult.Hard => 90,
            _ => 30
        };

        if (level.Mode == Mode.Duo) time *= 2;

        timer.Launch(time, () => LevelMaster.LoseLevel?.Invoke());
    }

    public void ButtonRestart()
    {
        windowLose.Hide();
        windowComplete.Hide();
        GameManager.restartGame?.Invoke();
    }

    public void ButtonAd(int id)
    {
        YandexGame.RewVideoShow(id);
    }

    public void ButtonMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void Reward(int id)
    {
        switch (id)
        {
            case 0:
                windowLose.HideButtonAd();
                windowLose.Hide();
                timer.Launch(20, () => LevelMaster.LoseLevel?.Invoke());
                LevelMaster.instance.IsAdShow = true;
                break;
            case 1:
                LevelMaster.instance.Clue();
                break;
        }
    }
    
    private void OnEnable()
    {
        LevelMaster.StartLevel += LaunchTimer;
        LevelMaster.LoseLevel += windowLose.Show;
        LevelMaster.CompleteLevel += windowComplete.Show;
        LevelMaster.CompleteLevel += timer.Stop;
        YandexGame.RewardVideoEvent += Reward;
    }
    
    private void OnDisable()
    {
        LevelMaster.StartLevel -= LaunchTimer;
        LevelMaster.LoseLevel -= windowLose.Show;
        LevelMaster.CompleteLevel -= windowComplete.Show;
        LevelMaster.CompleteLevel -= timer.Stop;
        YandexGame.RewardVideoEvent -= Reward;
    }
}
