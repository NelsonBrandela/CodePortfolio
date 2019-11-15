using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    static SoundManager _instance = null;

    public AudioSource sfxSource;
    public AudioSource musicSource;

    // Use this for initialization
    void Start()
    {

        if (instance)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlaySound(AudioClip clip, float volume = 1.0f)
    {
        if (sfxSource)
        {
            sfxSource.clip = clip;
            sfxSource.volume = volume;
            sfxSource.Play();
        }
    }

    public void PlayMusic(AudioClip clip, float volume = 1.0f, bool loop = true)
    {
        if (musicSource)
        {
            musicSource.clip = clip;
            musicSource.volume = volume;
            musicSource.loop = loop;
            musicSource.Play();
        }
    }

    public static SoundManager instance
    {
        set { _instance = value; }
        get { return _instance; }
    }
}
