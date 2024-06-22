using System;
using System.Linq;
using UnityEngine;

public class LevelMaster : MonoBehaviour
{
    public static LevelMaster instance;
    
    public static Action DeckComplete;
    public static Action<Level> StartLevel;
    public static Action CompleteLevel;
    public static Action LoseLevel;
    
    private Level currentLevel;
    [SerializeField]
    private LetterSpawner letterSpawner;
    [SerializeField]
    private CellSpawner cellSpawner;

    [Header("Test")]
    [SerializeField]
    private bool isTest;
    [SerializeField]
    private string testString;

    public Level Level => currentLevel;
    
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        if (isTest)
        {
            currentLevel = new Level(testString, Mode.Single, Difficult.Easy, GameType.Rating);
        }
        else
        {
            currentLevel = new Level(Mode.Duo, Difficult.Easy, GameType.Rating);
        }
        
        StartLevel?.Invoke(currentLevel);
    }

    private void Launch(Level level)
    {
        letterSpawner.Spawn(level);
        cellSpawner.Launch(level);

        foreach (var word in level.Words)
        {
            Debug.Log(word);
        }
    }

    private void CheckEndGame()
    {
        if (cellSpawner.Decks.Count(x => x.IsCompleted) == cellSpawner.Decks.Length)
        {
            CompleteLevel?.Invoke();
            Debug.Log("Level completed!!!");
        }
    }

    private void OnEnable()
    {
        DeckComplete += CheckEndGame;
        StartLevel += Launch;
    }

    private void OnDisable()
    {
        DeckComplete -= CheckEndGame;
        StartLevel -= Launch;
    }
}
