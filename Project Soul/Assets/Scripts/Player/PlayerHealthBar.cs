using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Image frontHealthBar;
    [SerializeField] private Image backHealthBar;
    [SerializeField] private Image manaBar;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float maxMana = 3f;
    [SerializeField] private float chipSpeed = 2f;
    [SerializeField] private float healthRegenAmount = 20f;
    [SerializeField] private float manaCost = 1f;

    private float health;
    private float mana;
    private float lerpTimer = 0.05f;

    public bool isInvunurable = false;

    private SpriteRenderer spriteRenderer;
    private bool isFlashing = false;

    private PlayerDeath playerDeath;

    private void Start()
    {
        health = maxHealth;
        mana = maxMana;

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
        mana = Mathf.Clamp(mana, 0, maxMana);

        UpdateHealthUI();
        UpdateManaUI();

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (health < maxHealth && mana >= manaCost)
            {
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    PlayerAnimation playerAnimation = player.GetComponent<PlayerAnimation>();
                    if (playerAnimation != null)
                    {
                        RegenerateHealth();
                        playerAnimation.SetHealAnimation();
                    }
                }
                else
                {
                    Debug.LogError("Player object not found!");
                }
            }
            else
            {
                Debug.Log("Cannot regenerate: Health is full or not enough mana.");
            }
        }
    }

    private void UpdateHealthUI()
    {
        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;

        if (fillFront < hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.fillAmount = hFraction;
            lerpTimer = 0f;
        }
        else if (fillBack > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            backHealthBar.fillAmount = Mathf.Lerp(fillBack, hFraction, percentComplete);
        }
    }

    private void UpdateManaUI()
    {
        manaBar.fillAmount = mana / maxMana;
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

    public void SetHealth(float value)
    {
        health = Mathf.Clamp(value, 0, maxHealth);
        lerpTimer = 0f;
        UpdateHealthUI();
    }

    public void ResetHealth()
    {
        health = maxHealth;
        mana = maxMana;
        lerpTimer = 0f;

        frontHealthBar.fillAmount = 1f;
        backHealthBar.fillAmount = 1f;
        backHealthBar.color = Color.white;
        UpdateManaUI();
    }

    public void GainMana(float manaAmount)
    {
        mana += manaAmount;
        mana = Mathf.Clamp(mana, 0, maxMana);
        Debug.Log("Mana gained: " + manaAmount + ". Current mana: " + mana);
        UpdateManaUI();
    }

    public void RegenerateHealth()
    {
        if (mana >= manaCost && health < maxHealth)
        {
            mana -= manaCost;
            health += healthRegenAmount;
            health = Mathf.Clamp(health, 0, maxHealth);

            Debug.Log("Health regenerated by " + healthRegenAmount + " HP. Mana used: " + manaCost);

            UpdateHealthUI();
            UpdateManaUI();
        }
        else
        {
            Debug.Log("Cannot regenerate health: Not enough mana or health is full.");
        }
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
