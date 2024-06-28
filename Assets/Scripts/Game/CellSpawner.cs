using System.Linq;
using UnityEngine;

public class CellSpawner : MonoBehaviour
{
    [SerializeField]
    private Deck[] decks;

    public Deck[] Decks => decks;
    
    public void Launch(Level level)
    {
        foreach (var deck in decks) deck.Clear();

        switch (level.Mode)
        {
            case Mode.Single:
                decks.First().Init(level.Words[0].Length);
                break;
            case Mode.Duo:
                decks[0].Init(level.Words[0].Length);
                decks[1].Init(level.Words[1].Length);
                break;
        }
    }

}
