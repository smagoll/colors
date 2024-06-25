using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WindowLose : MonoBehaviour, IWindow
{
    [SerializeField]
    private GameObject background;
    [SerializeField]
    private GameObject windowInfo;
    [SerializeField]
    private Button buttonAd;
    [SerializeField]
    private TextMeshProUGUI textRating;
    
    public void Show()
    {
        background.SetActive(true);
        windowInfo.SetActive(true);
        
        if (LevelMaster.instance.Level.GameType == GameType.Rating)
        {
            textRating.text = "- " + LevelMaster.instance.Rating;
            textRating.gameObject.SetActive(true);
        }
        else
        {
            textRating.gameObject.SetActive(false);
        }
    }

    public void Hide()
    {
        background.SetActive(false);
        windowInfo.SetActive(false);
    }

    public void HideButtonAd()
    {
        buttonAd.interactable = false;
    }
}
