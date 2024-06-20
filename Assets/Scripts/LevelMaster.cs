using System;
using System.Linq;
using UnityEngine;

public class LevelMaster : MonoBehaviour
{
    public static Action DeckComplete;
    
    private Level currentLevel;
    [SerializeField]
    private LetterSpawner letterSpawner;
    [SerializeField]
    private CellSpawner cellSpawner;
    
    private void Start()
    {
        currentLevel = new Level("соломинка", Mode.Duo);
        
        Launch();
    }

    private void Launch()
    {
        letterSpawner.Spawn(currentLevel);
        cellSpawner.Launch(currentLevel);
    }

    private void CheckEndGame()
    {
        if (cellSpawner.Decks.Count(x => x.IsCompleted) == cellSpawner.Decks.Length)
        {
            Debug.Log("Level completed!!!");
        }
    }

    private void OnEnable()
    {
        DeckComplete += CheckEndGame;
    }

    private void OnDisable()
    {
        DeckComplete -= CheckEndGame;
    }
}
