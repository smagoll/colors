using System;
using TMPro;
using UnityEngine;

public class WindowComplete : MonoBehaviour, IWindow
{
    [SerializeField]
    private GameObject background;
    [SerializeField]
    private GameObject windowInfo;
    [SerializeField]
    private TextMeshProUGUI rating;
    
    public void Show()
    {
        background.SetActive(true);
        windowInfo.SetActive(true);

        if (LevelMaster.instance.Level.GameType == GameType.Rating)
        {
            rating.gameObject.SetActive(true);
        }
        else
        {
            rating.gameObject.SetActive(false);
        }
    }

    public void Hide()
    {
        background.SetActive(false);
        windowInfo.SetActive(false);
    }
}