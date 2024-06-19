using System.Collections.Generic;
using UnityEngine;

public class CellSpawner : MonoBehaviour
{
    [SerializeField]
    private Cell prefabCell;
    [SerializeField]
    private Transform listCells;

    public List<Cell> Cells { get; } = new();
    
    public void Spawn(Level level)
    {
        Clear();
        
        for (int i = 0; i < level.Word.Length; i++)
        {
            var cell = Instantiate(prefabCell, listCells);
            Cells.Add(cell);
        }
    }

    private void Clear()
    {
        foreach (Transform cell in listCells) Destroy(cell.gameObject);
    }
}
