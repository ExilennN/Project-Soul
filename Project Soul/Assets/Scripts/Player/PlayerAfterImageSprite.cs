using System.Collections;
using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // Компонент SpriteRenderer
    private Color startColor; // Початковий колір
    [SerializeField] private float activeTime = 0.1f; // Час активності післяобразу
    [SerializeField] private float fadeSpeed = 10f; // Швидкість зникання

    private void OnEnable()
    {
        // Ініціалізація компонента SpriteRenderer та початкового кольору
        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        // Запуск корутини для зникання
        StartCoroutine(FadeOut());
    }

    private void OnDisable()
    {
        // Скидання кольору до повністю непрозорого при деактивації
        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, startColor.a);
    }

    private IEnumerator FadeOut()
    {
        float alpha = startColor.a; // Початкове значення альфа
        float fadeAmount = alpha / (activeTime * fadeSpeed); // Розрахунок швидкості зникання за кадр

        while (alpha > 0)
        {
            alpha -= fadeAmount * Time.deltaTime; // Зменшення значення альфа
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, alpha); // Застосування нового кольору
            yield return null; // Очікування наступного кадру
        }

        // Повернення післяобразу в пул
        PlayerAfterImagePool.Instance.ReturnToPool(gameObject);
    }
}
