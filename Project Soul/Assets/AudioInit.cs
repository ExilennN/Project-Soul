using UnityEngine;
using UnityEngine.Audio;

public class AudioInit : MonoBehaviour
{
    public AudioMixer mixer;

    // Массив строк для всех параметров громкости
    public string[] volumeParameters = { "MasterVol", "EffectsVol", "MusicVol" };

    // Стандартные значения для громкости, если параметр не сохранён в PlayerPrefs
    private const float defaultVolumeMaster = 0f;
    private const float defaultVolumeOther = -80f;

    private void Start()
    {
        foreach (string volumeParameter in volumeParameters)
        {
            // Определяем стандартное значение в зависимости от параметра
            float defaultVolume = (volumeParameter == "MasterVol") ? defaultVolumeMaster : defaultVolumeOther;

            // Загружаем значение из PlayerPrefs или используем стандартное
            float savedVolume = PlayerPrefs.GetFloat(volumeParameter, defaultVolume);

            // Применяем сохранённое значение громкости к микшеру
            mixer.SetFloat(volumeParameter, savedVolume);

            // Debug для проверки, что значение было загружено и применено
            Debug.Log($"Loaded {volumeParameter} with volume: {savedVolume}");
        }
    }
}
