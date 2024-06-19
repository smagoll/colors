using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform dragTransform;
    public Transform listLetters;
    
    private void Awake()
    {
        if (instance == null) instance = this;
    }
}
