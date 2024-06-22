using UnityEngine;

public class WindowLose : MonoBehaviour, IWindow
{
    [SerializeField]
    private GameObject background;
    [SerializeField]
    private GameObject windowInfo;
    
    public void Show()
    {
        background.SetActive(true);
        windowInfo.SetActive(true);
    }

    public void Hide()
    {
        background.SetActive(false);
        windowInfo.SetActive(false);
    }
}
