using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField]
    private Cell prefabCell;
    
    public List<Cell> Cells { get; set; } = new();
    private string Word { get; set; }
    public bool IsCompleted { get; set; }
    
    public void Init(string word)
    {
        Word = word;
        Spawn(word, transform);
    }
    
    private void Spawn(string word, Transform listTransform)
    {
        for (int i = 0; i < word.Length; i++)
        {
            var cell = Instantiate(prefabCell, listTransform);
            cell.deck = this;
            Cells.Add(cell);
        }
    }

    public void Clear()
    {
        foreach (Transform cell in transform) Destroy(cell.gameObject);
    }

    public void Check()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var cell in Cells)
        {
            if (cell.InstalledLetter == null)
            {
                return;
            }
        }
        
        foreach (var cell in Cells)
        {
            sb.AppendFormat(cell.InstalledLetter.Symbol.ToString());
        }

        if (Word == sb.ToString())
        {
            Debug.Log($"{Word} is completed!");
            IsCompleted = true;
            LevelMaster.DeckComplete?.Invoke();
        }
    }
}
