using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    
    private DataWords Data { get; set; }
    
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        Load();
    }

    private void Load()
    {
            var textAssetJson = Resources.Load<TextAsset>("dictionary_words");
            Data = JsonUtility.FromJson<DataWords>(textAssetJson.text);
    }

    public string RandomWord(Difficult difficult)
    {
        string[] sortedWords = new string[] { };
        
        switch (difficult)
        {
            case Difficult.Easy:
                sortedWords = Data.words.Where(x => x.Length <= 5).ToArray();
                break;
            case Difficult.Medium:
                sortedWords = Data.words.Where(x => x.Length is <= 7 and > 5).ToArray();
                break;
            case Difficult.Hard:
                sortedWords = Data.words.Where(x => x.Length > 7).ToArray();
                break;
        }
        var rnd = Random.Range(0, sortedWords.Length);
        Debug.Log(sortedWords.Length);
        return sortedWords[rnd];
    }
}
