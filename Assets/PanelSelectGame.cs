using UnityEngine;
using UnityEngine.UI;

public class PanelSelectGame : Panel
{
    [SerializeField]
    private GameObject buttons;

    [SerializeField]
    private Button buttonNormal;
    [SerializeField]
    private Button buttonRating;
    
    public override void Show()
    {
        buttons.SetActive(true);
    }

    public override void Hide()
    {
        //анимация
        nextPanel.Show();
        
        buttons.SetActive(false);
        
    }

    private void OnEnable()
    {
        buttonNormal.onClick.AddListener(() => LevelCollector.GameType = GameType.Normal);
        buttonRating.onClick.AddListener(() => LevelCollector.GameType = GameType.Rating);
    }
    
    private void OnDisable()
    {
        buttonNormal.onClick.RemoveAllListeners();
        buttonRating.onClick.RemoveAllListeners();
    }
}
