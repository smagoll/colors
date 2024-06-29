using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField]
    private Color colorDone;
    [SerializeField]
    private SVGImage image;
    
    public Deck deck;
    public Letter InstalledLetter { get; set; }
    public bool IsDone { get; set; }
    
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

    public void Done()
    {
        IsDone = true;
        image.color = colorDone;
        InstalledLetter.IsDone = true;
        ParticleManager.instance.CellDone(transform);
    }
}
