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
