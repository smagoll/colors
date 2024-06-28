using UnityEngine;

public class Panel : MonoBehaviour
{
    public Panel nextPanel;
    
    [SerializeField]
    private GameObject buttons;
    
    public void Show()
    {
        buttons.SetActive(true);
    }

    public void Hide()
    {
        //анимация исчезновения
        
        nextPanel.Show();
        
        buttons.SetActive(false);
    }
}
