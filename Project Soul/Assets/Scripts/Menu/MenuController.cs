using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt = null;

    [Header("Video")]
    public VideoManager videoManager;

    [Header("Audio")]
    public AudioManager audioManager;

    public void ApplyVideoSettings()
    {
        videoManager.ApplySettings();
        StartCoroutine(ConfirmationBox());
    }

    public void ApplyVolumeSettings()
    {
        audioManager.ApplySettings();
        StartCoroutine(ConfirmationBox());
    }

    private IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        confirmationPrompt.SetActive(false);
    }

    public void NewGameButton()
    {
        // Удаляем сохранённые данные позиций чекпоинтов
        PlayerPrefs.DeleteKey("CheckpointX");
        PlayerPrefs.DeleteKey("CheckpointY");
        PlayerPrefs.DeleteKey("CheckpointZ");

        // Удаляем все возможные данные активированных чекпоинтов
        string[] checkpointNames = {
            "lamppole_checkpoint_1",
            "lamppole_checkpoint_2",
            "lamppole_checkpoint_3",
            "lamppole_checkpoint_4",
            "lamppole_checkpoint_5",
            "lamppole_checkpoint_6",
            "lamppole_checkpoint_7",
            "lamppole_checkpoint_8",
            "lamppole_checkpoint_9"
        };

        foreach (string checkpointName in checkpointNames)
        {
            string checkpointKey = checkpointName + "_Activated";
            Debug.Log("Attempting to delete: " + checkpointKey); // Лог перед удалением
            PlayerPrefs.DeleteKey(checkpointKey); // Удаляем ключ
        }

        PlayerPrefs.Save(); // Сохраняем изменения
        Debug.Log("Checkpoint data deleted.");

        // Загружаем сцену новой игры
        SceneManager.LoadScene("LevelOne");
    }


    public void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No more scenes in build settings.");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
