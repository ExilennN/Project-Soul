using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image frontHealthBar;
    [SerializeField] private Image backHealthBar;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float chipSpeed = 2f;
    private float health;
    private float lerpTimer = 0.05f;

    private SpriteRenderer spriteRenderer;
    private bool isFlashing = false;


    private void Start()
    {
        health = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();

        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(Random.Range(5, 10));
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetHealth();
        }
    }

    private void UpdateHealthUI()
    {
        Debug.Log("Health" + health);
        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;

        if (fillBack > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            backHealthBar.fillAmount = Mathf.Lerp(fillBack, hFraction, percentComplete);
        }
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;

        if (!isFlashing)
        {
            StartCoroutine(FlashDamage());
        }
    }

    private void ResetHealth()
    {
        health = maxHealth;
        lerpTimer = 0f;

        frontHealthBar.fillAmount = 1f;
        backHealthBar.fillAmount = 1f;
        backHealthBar.color = Color.white;
    }


    private IEnumerator FlashDamage()
    {
        isFlashing = true;
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.color = new Color(1f, 0f, 0f, 0.5f);
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        isFlashing = false;
    }
}
