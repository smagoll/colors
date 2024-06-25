using TMPro;
using UnityEngine;

public class Letter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textLetter;
    
    public bool IsDone { get; set; }

    public Cell Cell { get; set; }
    
    public char Symbol { get; set; }
    
    public void Init(char symbol)
    {
        Symbol = symbol;
        textLetter.text = symbol.ToString();
    }
}
