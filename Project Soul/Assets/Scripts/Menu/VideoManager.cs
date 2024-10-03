using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VideoManager : MonoBehaviour
{
    public Toggle fullscreenTog, vsyncTog;
    public List<ResItem> resolutions = new List<ResItem>();
    private int selectedResolution;
    public TMP_Text resolutionLabel;

    private const string FullscreenPrefKey = "Fullscreen";
    private const string VSyncPrefKey = "VSync";
    bool foundRes;

    void Start()
    {
        LoadSettings();
        fullscreenTog.onValueChanged.AddListener(delegate { ToggleFullscreen(); });
        foundRes = false;

        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;
                selectedResolution = i;
                break;
            }
        }

        UpdateResLabel();
    }

    public void ResLeft()
    {
        selectedResolution--;
        if (selectedResolution < 0)
        {
            selectedResolution = resolutions.Count - 1;
        }
        UpdateResLabel();
    }

    public void ResRight()
    {
        selectedResolution++;
        if (selectedResolution > resolutions.Count - 1)
        {
            selectedResolution = 0;
        }
        UpdateResLabel();
    }

    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[selectedResolution].horizontal + " x " + resolutions[selectedResolution].vertical;
    }

    public void ApplySettings()
    {
        if (Screen.width != resolutions[selectedResolution].horizontal || Screen.height != resolutions[selectedResolution].vertical)
        {
            Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, fullscreenTog.isOn);
        }

        QualitySettings.vSyncCount = vsyncTog.isOn ? 1 : 0;

        PlayerPrefs.SetInt("SelectedResolution", selectedResolution);
        PlayerPrefs.SetInt(FullscreenPrefKey, fullscreenTog.isOn ? 1 : 0);
        PlayerPrefs.SetInt(VSyncPrefKey, vsyncTog.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = fullscreenTog.isOn;
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

        if (PlayerPrefs.HasKey("SelectedResolution"))
        {
            selectedResolution = PlayerPrefs.GetInt("SelectedResolution");
        }
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}
