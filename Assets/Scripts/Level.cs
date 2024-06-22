public class Level
{
    public string[] Words { get; set; }
    public GameType GameType { get; set; }
    public Mode Mode { get; set; }
    public Difficult Difficult { get; set; }

    public Level(string word, Mode mode, Difficult difficult, GameType gameType)
    {
        Mode = mode;
        Words = mode == Mode.Single ? new[] { word } : new[] { word, word };
        Difficult = difficult;
        GameType = gameType;
    }
    
    public Level(Mode mode, Difficult difficult, GameType gameType)
    {
        Mode = mode;
        GameType = gameType;
        Words = mode == Mode.Single ? new[] { DataManager.instance.RandomWord(difficult) } : new[] { DataManager.instance.RandomWord(difficult), DataManager.instance.RandomWord(difficult) };
    }
}
