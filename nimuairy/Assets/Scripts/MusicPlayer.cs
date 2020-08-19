using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;

    private static MusicPlayer instance;

    [SerializeField] float startVolume = 0.5f;

    [SerializeField] AudioClip[] backgroundMusic;
    [SerializeField] AudioClip[] battleMusic;

    private void Awake()
    {
        if (instance == null)
        {
            instance = gameObject.GetComponent<MusicPlayer>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static MusicPlayer GetInstance()
    {
        return instance;
    }


    void Start()
    {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.volume = startVolume;
        PlayBackgroundMusic();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void PlayBackgroundMusic()
    {
        audioSource.Stop();
        int clipIndex = Random.Range(0, backgroundMusic.Length);
        audioSource.clip = backgroundMusic[clipIndex];
        audioSource.Play();
    }

    public void PlayBattleMusic()
    {
        audioSource.Stop();
        int clipIndex = Random.Range(0, battleMusic.Length);
        audioSource.clip = battleMusic[clipIndex];
        audioSource.Play();
    }
}
