using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

public class ScreenFader : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;
    private bool isFading = false;
    public async Task FadeOut()
    {
        if (!isFading)
        {
            await FadeCoroutine(1);
        }
    }

    public async Task FadeIn()
    {
        if (!isFading)
        {
            await FadeCoroutine(0);
        }
    }

    private async Task FadeCoroutine(float targetAlpha)
    {
        isFading = true;
        float startAlpha = fadeCanvasGroup.alpha;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            await Task.Yield();
        }
        fadeCanvasGroup.alpha = targetAlpha;
        isFading = false;
    }
}
