public class Level
{
    public string[] Words { get; set; }
    public Mode Mode { get; set; }

    public Level(string word, Mode mode)
    {
        Mode = mode;
        Words = mode == Mode.Single ? new[] { word } : new[] { word, word };
    }
    
    public Level(Mode mode)
    {
        Mode = mode;
        Words = mode == Mode.Single ? new[] { DataManager.instance.RandomWord() } : new[] { DataManager.instance.RandomWord(), DataManager.instance.RandomWord() };
    }
}
