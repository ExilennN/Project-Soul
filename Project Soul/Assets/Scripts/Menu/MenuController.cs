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
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Start");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
