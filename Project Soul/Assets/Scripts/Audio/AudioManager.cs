using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---- Audio Source ----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] public AudioSource SFXSource;

    [Header("---- Audio Clip ----")]
    #region PLAYER
    public AudioClip death;
    public AudioClip jump;
    public AudioClip run;
    public AudioClip hit;
    #endregion

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
