using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LetterSpawner : MonoBehaviour
{
    [SerializeField]
    private Letter prefabLetter;
    [SerializeField]
    private Transform listLetters;

    public List<Letter> Letters { get; } = new();
    
    public void Spawn(Level level)
    {
        Clear();

        switch (level.Mode)
        {
            case Mode.Single:
                Spawn(level.Words);
                break;
            case Mode.Duo:
                Spawn(level.Words);
                break;
        }
    }

    private void Spawn(string[] words)
    {
        StringBuilder sb = new StringBuilder();

        foreach (var word in words)
        {
            sb.AppendFormat(word);
        }
        
        var shuffleWord = Shuffle(sb.ToString());
        
        foreach (var symbol in shuffleWord)
        {
            var letter = Instantiate(prefabLetter, listLetters);
            letter.Init(symbol);
            Letters.Add(letter);
        }
    }

    private void Clear()
    {
        foreach (var letter in Letters) Destroy(letter.gameObject);
        Letters.Clear();
    }
    
    private string Shuffle(string str)
    {
        StringBuilder sb = new StringBuilder(str);
 
        for (int i = 0; i < sb.Length - 1; ++i)
        {
            int r = Random.Range(i + 1, sb.Length);
            (sb[i], sb[r]) = (sb[r], sb[i]);
        }
 
        return sb.ToString();
    }
}
