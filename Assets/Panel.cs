using UnityEngine;

public abstract class Panel : MonoBehaviour
{
    public Panel nextPanel;
    
    public abstract void Show();

    public abstract void Hide();
}
