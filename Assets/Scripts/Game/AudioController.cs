using System;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    [SerializeField]
    private AudioSource music;
    [SerializeField]
    private AudioSource sfx;
    [SerializeField]
    private AudioSource sfxSmall;

    [Header("SFX")]
    public AudioClip button;
    public AudioClip done;
    public AudioClip firework;
    public AudioClip lose;
    public AudioClip victory;
    
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        
        if (instance == null) instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(AudioClip audioClip)
    {
        sfx.PlayOneShot(audioClip);
    }
    
    public void PlaySFXSmall(AudioClip audioClip)
    {
        sfxSmall.PlayOneShot(audioClip);
    }

    private void OnEnable()
    {
        LevelMaster.CompleteLevel += () => PlaySFX(victory);
        LevelMaster.LoseLevel += () => PlaySFX(lose);
    }
    
    private void OnDisable()
    {
        LevelMaster.CompleteLevel -= () => PlaySFX(victory);
        LevelMaster.LoseLevel -= () => PlaySFX(lose);
    }
}
