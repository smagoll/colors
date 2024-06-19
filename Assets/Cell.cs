using UnityEngine;

public class Cell : MonoBehaviour
{
    public Letter InstalledLetter { get; set; }
    
    public void Set(Letter letter)
    {
        if (InstalledLetter != null) InstalledLetter.transform.SetParent(GameManager.instance.listLetters);
        
        InstalledLetter = letter;
        
        letter.Cell?.Clear();
        letter.Cell = this;
        letter.transform.SetParent(transform);
        letter.transform.position = transform.position;
        
        LevelMaster.Check?.Invoke();
    }

    public void Clear()
    {
        InstalledLetter = null;
    }
}
