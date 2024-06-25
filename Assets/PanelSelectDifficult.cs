using UnityEngine.SceneManagement;

public class PanelSelectDifficult : Panel
{
    public void SetDifficult(int difficult)
    {
        LevelCollector.Difficult = (Difficult)difficult;
        SceneManager.LoadScene("Game");
    }
}
