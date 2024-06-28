using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField]
    private Cell prefabCell;
    
    public List<Cell> Cells { get; } = new();
    public bool IsCompleted { get; set; }
    
    public void Init(int count)
    {
        Spawn(count, transform);
    }
    
    private void Spawn(int count, Transform listTransform)
    {
        for (int i = 0; i < count; i++)
        {
            var cell = Instantiate(prefabCell, listTransform);
            cell.deck = this;
            Cells.Add(cell);
        }
    }

    public void Clear()
    {
        foreach (Transform cell in transform) Destroy(cell.gameObject);
        Cells.Clear();
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

        foreach (var word in LevelMaster.instance.Level.Words)
        {
            if (word == sb.ToString())
            {
                Debug.Log($"{word} is completed!");
                IsCompleted = true;
                foreach (var cell in Cells)
                {
                    cell.Done();
                } // deactivate drag
                LevelMaster.DeckComplete?.Invoke();
            }
        }
    }

    private void OnEnable()
    {
        GameManager.restartGame += () => IsCompleted = false;
    }
    
    private void OnDisable()
    {
        GameManager.restartGame -= () => IsCompleted = false;
    }
}
