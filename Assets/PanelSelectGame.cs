using UnityEngine;
using UnityEngine.UI;

public class PanelSelectGame : Panel
{
    public void SetGameType(int gameType)
    {
        LevelCollector.GameType = (GameType)gameType;
        PanelController.switchPanel?.Invoke();
    }
}
