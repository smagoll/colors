using UnityEngine;

public class Cell : MonoBehaviour
{
    public Deck deck;
    public Letter InstalledLetter { get; set; }
    
    public void Set(Letter letter)
    {
        if (InstalledLetter != null)
        {
            InstalledLetter.transform.SetParent(GameManager.instance.listLetters);
            InstalledLetter.Cell = null;
        }
        
        InstalledLetter = letter;
        
        if(letter.Cell != null) letter.Cell.Clear();
        letter.Cell = this;
        letter.transform.SetParent(transform);
        letter.transform.position = transform.position;

        deck.Check();
    }

    public void Clear()
    {
        InstalledLetter = null;
    }
}
