using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

public class ScreenFader : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;
    private bool isFading = false;

    private void Start()
    {
        fadeCanvasGroup.alpha = 0;
        isFading = false;
        Debug.Log("ScreenFader: Initialized with alpha = 0");
    }

    public async Task FadeOut()
    {
        if (!isFading)
        {
            Debug.Log("ScreenFader: Starting FadeOut");
            await FadeCoroutine(1);
        }
    }

    public async Task FadeIn()
    {
        if (!isFading)
        {
            Debug.Log("ScreenFader: Starting FadeIn");
            await FadeCoroutine(0);
        }
    }

    private async Task FadeCoroutine(float targetAlpha)
    {
        isFading = true;
        Debug.Log("ScreenFader: Fading to " + targetAlpha);
        float startAlpha = fadeCanvasGroup.alpha;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            await Task.Yield();
        }

        fadeCanvasGroup.alpha = targetAlpha;
        Debug.Log("ScreenFader: Fade completed, alpha = " + fadeCanvasGroup.alpha);
        isFading = false;
    }
}
