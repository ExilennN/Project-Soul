using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---- Audio Source ----")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource shortSfxSource;

    [Header("---- Audio Clip ----")]
    [SerializeField] public AudioClip track;
    [SerializeField] public AudioClip death;
    [SerializeField] public AudioClip jump;
    [SerializeField] public AudioClip run;

    private void Start()
    {
        PlayMusic(track);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip != clip || !musicSource.isPlaying)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
            musicSource.loop = false;
        }
    }

    
    public void PlaySFX(AudioClip clip)
    {
        shortSfxSource.PlayOneShot(clip);
    }

    public void PlayLoopedSFX(AudioClip clip)
    {
        if (sfxSource.clip != clip || !sfxSource.isPlaying)
        {
            sfxSource.clip = clip;
            sfxSource.loop = true;
            sfxSource.Play();
        }
    }

    public void StopLoopedSFX()
    {
        if (sfxSource.isPlaying)
        {
            sfxSource.Stop();
            sfxSource.loop = false;
        }
    }

    public AudioSource GetSFXSource()
    {
        return sfxSource;
    }
}
