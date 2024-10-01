using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Image frontHealthBar;
    [SerializeField] private Image backHealthBar;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float chipSpeed = 2f;
    private float health;
    private float lerpTimer = 0.05f;

    public bool isInvunurable = false;

    private SpriteRenderer spriteRenderer;
    private bool isFlashing = false;

    private PlayerDeath playerDeath;

    private void Start()
    {
        health = maxHealth;
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            spriteRenderer = player.GetComponent<SpriteRenderer>();
            playerDeath = player.GetComponent<PlayerDeath>();

            if (playerDeath == null)
            {
                Debug.LogError("PlayerDeath component not found on player!");
            }
        }
        else
        {
            Debug.LogError("Player with tag 'Player' not found in the scene!");
        }
    }

    private void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetHealth();
        }
    }

    private void UpdateHealthUI()
    {
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

    public void TakeDamage(AttackDetails attackDetails)
    {
        if (isInvunurable) { return; }

        health -= attackDetails.damageAmout;
        lerpTimer = 0f;

        if (health <= 0)
        {
            if (playerDeath != null)
            {
                playerDeath.Die();
            }
            else
            {
                Debug.LogError("Cannot call Die() because playerDeath is null!");
            }
        }
        else
        {
            if (!isFlashing)
            {
                StartCoroutine(FlashDamage());
            }
        }
    }

    public void ResetHealth()
    {
        health = maxHealth;
        lerpTimer = 0f;

        frontHealthBar.fillAmount = 1f;
        backHealthBar.fillAmount = 1f;
        backHealthBar.color = Color.white;
    }

    private IEnumerator FlashDamage()
    {
        isInvunurable = true;
        isFlashing = true;
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        spriteRenderer.color = Color.white;
        isFlashing = false;
        isInvunurable = false;
    }
}