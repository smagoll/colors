using System;
using UnityEngine;
using YG;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Action restartGame;

    public Transform dragTransform;
    public Transform listLetters;
    
    private void Awake()
    {
        if (instance == null) instance = this;
    }
}
