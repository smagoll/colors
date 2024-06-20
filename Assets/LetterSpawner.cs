using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LetterSpawner : MonoBehaviour
{
    [SerializeField]
    private Letter prefabLetter;
    [SerializeField]
    private Transform listLetters;

    private List<Letter> Letters { get; } = new();
    
    public void Spawn(Level level)
    {
        Clear();

        switch (level.Mode)
        {
            case Mode.Single:
                Spawn(level.Words[0]);
                break;
            case Mode.Duo:
                Spawn(level.Words[0]);
                Spawn(level.Words[1]);
                break;
        }
    }

    private void Spawn(string word)
    {
        var shuffleWord = Shuffle(word);
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
