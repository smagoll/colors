using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;
using Random = UnityEngine.Random;

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

    public Level Level => currentLevel;
    public int Rating => CalculateRating();

    public bool IsAdShow { get; set; }
    
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        StartGame();
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
        if (cellSpawner.Decks.Count(x => x.IsCompleted) == Level.Words.Length)
        {
            CompleteLevel?.Invoke();
            Debug.Log("Level completed!!!");
        }
    }

    private void StartGame()
    {
        YandexGame.FullscreenShow();
        IsAdShow = false;
        currentLevel = new Level(LevelCollector.Mode, LevelCollector.Difficult, LevelCollector.GameType);
        StartLevel?.Invoke(currentLevel);
    }
    
    private int CalculateRating()
    {
        int ratingPoints = 0;

        foreach (var word in instance.Level.Words)
        {
            ratingPoints += word.Length;
        }

        if (instance.Level.Mode == Mode.Duo)
        {
            ratingPoints *= 2;
        }

        return ratingPoints;
    }

    private void UpdateRating(bool result)
    {
        if (Level.GameType != GameType.Rating) return;
        
        if (result)
        {
            int newRating;
            if (IsAdShow)
            {
                newRating = YandexGame.savesData.rating + Rating * 2;
            }
            else
            {
                newRating = YandexGame.savesData.rating + Rating;
            }
            YandexGame.NewLeaderboardScores("rating", newRating);
            YandexGame.savesData.rating = newRating;
            Debug.Log(newRating);
        }
        else
        {
            var newRating = YandexGame.savesData.rating - Rating;
            newRating = Mathf.Clamp(newRating, 0, Int32.MaxValue);
            YandexGame.NewLeaderboardScores("rating", newRating);
            YandexGame.savesData.rating = newRating;
            Debug.Log(newRating);
        }
        
        YandexGame.SaveProgress();
    }

    public void Clue()
    {
        while (true)
        {
            var unusedLetters = letterSpawner.Letters.Where(x => x.IsDone == false).ToArray();
            var rndLetterNumber = Random.Range(0, unusedLetters.Length);
            var rndLetter = unusedLetters[rndLetterNumber];
            
            if (rndLetter == null) return;
        
            foreach (var word in Level.Words)
            {
                if (!word.Contains(rndLetter.Symbol)) continue;
                foreach (var deck in cellSpawner.Decks)
                {
                    if (deck.Cells.Count != word.Length) continue;
                    
                    List<int> indexes = new();
                    for (int i = 0; i < word.Length; i++)
                        if (word[i] == rndLetter.Symbol)
                            indexes.Add(i);
                    
                    foreach (var index in indexes)
                    {
                        if(deck.Cells[index].InstalledLetter != null)
                            if(deck.Cells[index].InstalledLetter.Symbol == rndLetter.Symbol) continue;
                        deck.Cells[index].Set(rndLetter);
                        rndLetter.IsDone = true;
                        return;
                    }
                }
            }
        }
    }
    
    private void LoseRating() => UpdateRating(false);
    private void WinRating() => UpdateRating(true);
    
    private void OnEnable()
    {
        LoseLevel += LoseRating;
        CompleteLevel += WinRating;
        DeckComplete += CheckEndGame;
        StartLevel += Launch;
        GameManager.restartGame += StartGame;
    }

    private void OnDisable()
    {
        LoseLevel -= LoseRating;
        CompleteLevel -= WinRating;
        DeckComplete -= CheckEndGame;
        StartLevel -= Launch;
        GameManager.restartGame -= StartGame;
    }
}
