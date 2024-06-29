using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class WindowComplete : MonoBehaviour, IWindow
{
    [SerializeField]
    private GameObject background;
    [SerializeField]
    private GameObject windowInfo;
    [SerializeField]
    private TextMeshProUGUI textRating;
    
    public void Show()
    {
        ParticleManager.instance.FireworksComplete().Forget();
        
        background.SetActive(true);
        windowInfo.SetActive(true);

        if (LevelMaster.instance.Level.GameType == GameType.Rating)
        {
            textRating.text = "+ " + LevelMaster.instance.Rating;
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


}