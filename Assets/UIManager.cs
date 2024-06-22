using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TimerUI timer;

    private void LaunchTimer(Level level)
    {
        var time = level.Difficult switch
        {
            Difficult.Easy => 60,
            Difficult.Medium => 120,
            Difficult.Hard => 180,
            _ => 60
        };

        if (level.Mode == Mode.Duo) time *= 2;
        
        timer.Launch(time, () => LevelMaster.LoseLevel?.Invoke());
    }

    private void OnEnable()
    {
        LevelMaster.StartLevel += LaunchTimer;
    }
}
