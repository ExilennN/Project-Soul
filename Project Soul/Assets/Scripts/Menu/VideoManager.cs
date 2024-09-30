using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoManager : MonoBehaviour
{
    public Toggle fullscreenTog, vsyncTog;

    private const string FullscreenPrefKey = "Fullscreen";
    private const string VSyncPrefKey = "VSync";

    void Start()
    {
        LoadSettings();
    }

    public void ApplySettings()
    {
        Screen.fullScreen = fullscreenTog.isOn;
        QualitySettings.vSyncCount = vsyncTog.isOn ? 1 : 0;

        PlayerPrefs.SetInt(FullscreenPrefKey, fullscreenTog.isOn ? 1 : 0);
        PlayerPrefs.SetInt(VSyncPrefKey, vsyncTog.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey(FullscreenPrefKey))
        {
            fullscreenTog.isOn = PlayerPrefs.GetInt(FullscreenPrefKey) == 1;
            Screen.fullScreen = fullscreenTog.isOn;
        }
        else
        {
            fullscreenTog.isOn = Screen.fullScreen;
        }

        if (PlayerPrefs.HasKey(VSyncPrefKey))
        {
            vsyncTog.isOn = PlayerPrefs.GetInt(VSyncPrefKey) == 1;
            QualitySettings.vSyncCount = vsyncTog.isOn ? 1 : 0;
        }
        else
        {
            vsyncTog.isOn = QualitySettings.vSyncCount != 0;
        }
    }
}
