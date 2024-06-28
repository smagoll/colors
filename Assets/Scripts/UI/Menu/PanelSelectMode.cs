public class PanelSelectMode : Panel
{
    public void SetMode(int mode)
    {
        LevelCollector.Mode = (Mode)mode;
        PanelController.switchPanel?.Invoke();
    }
}
