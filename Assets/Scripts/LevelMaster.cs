using System;
using System.Text;
using UnityEngine;

public class LevelMaster : MonoBehaviour
{
    public static Action Check;
    
    private Level currentLevel;
    [SerializeField]
    private LetterSpawner letterSpawner;
    [SerializeField]
    private CellSpawner cellSpawner;
    
    private void Start()
    {
        currentLevel = new Level("солома");
        
        Launch();
    }

    private void Launch()
    {
        letterSpawner.Spawn(currentLevel);
        cellSpawner.Spawn(currentLevel);
    }

    private void CheckWord()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var cell in cellSpawner.Cells)
        {
            if (cell.InstalledLetter == null)
            {
                return;
            }
        }
        
        foreach (var cell in cellSpawner.Cells)
        {
            sb.AppendFormat(cell.InstalledLetter.Symbol.ToString());
        }

        if (currentLevel.Word == sb.ToString())
        {
            Debug.Log("end game///");
        }
    }

    private void OnEnable()
    {
        Check += CheckWord;
    }

    private void OnDisable()
    {
        Check -= CheckWord;
    }
}
