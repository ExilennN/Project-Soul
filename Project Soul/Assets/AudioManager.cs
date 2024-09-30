using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider masterSlider;
    public Slider effectsSlider;
    public Slider musicSlider;

    private const float _multiplier = 20f;

    private const string MasterVolumeKey = "MasterVol";
    private const string EffectsVolumeKey = "EffectsVol";
    private const string MusicVolumeKey = "MusicVol";

    private void Start()
    {
        masterSlider.onValueChanged.AddListener(HandleMasterSliderChanged);
        effectsSlider.onValueChanged.AddListener(HandleEffectsSliderChanged);
        musicSlider.onValueChanged.AddListener(HandleMusicSliderChanged);

        LoadVolumeSettings();
    }

    private void HandleMasterSliderChanged(float value)
    {
        SetVolume("MasterVol", value);
    }

    private void HandleEffectsSliderChanged(float value)
    {
        SetVolume("EffectsVol", value);
    }

    private void HandleMusicSliderChanged(float value)
    {
        SetVolume("MusicVol", value);
    }

    private void SetVolume(string mixerParameter, float value)
    {
        float volumeValue = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * _multiplier;
        mixer.SetFloat(mixerParameter, volumeValue);
    }

    private void LoadVolume(string prefKey, string mixerParameter, Slider slider)
    {
        float savedVolume = PlayerPrefs.HasKey(prefKey) ? PlayerPrefs.GetFloat(prefKey) : slider.value;

        slider.value = savedVolume;
        mixer.SetFloat(mixerParameter, Mathf.Log10(Mathf.Clamp(savedVolume, 0.0001f, 1f)) * _multiplier);
    }

    private void LoadVolumeSettings()
    {
        LoadVolume(MasterVolumeKey, "MasterVol", masterSlider);
        LoadVolume(EffectsVolumeKey, "EffectsVol", effectsSlider);
        LoadVolume(MusicVolumeKey, "MusicVol", musicSlider);
    }

    public void ApplySettings()
    {
        PlayerPrefs.SetFloat(MasterVolumeKey, masterSlider.value);
        PlayerPrefs.SetFloat(EffectsVolumeKey, effectsSlider.value);
        PlayerPrefs.SetFloat(MusicVolumeKey, musicSlider.value);
        PlayerPrefs.Save();
    }
}
