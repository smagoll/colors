using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    public string RandomWord()
    {
        var rnd = Random.Range(0, Data.words.Length);
        return Data.words[rnd];
    }
}
