using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    AudioManager audioManager;
    private PlayerAnimation playerAnimation;
    private bool isDead = false;
    private PlayerHealthBar healthBar;
    private bool canRespawn = false;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        GameObject health = GameObject.FindWithTag("Health");
        healthBar = health.GetComponent<PlayerHealthBar>();

        if (playerAnimation == null)
        {
            Debug.LogError("PlayerAnimation component is missing on the player!");
        }

        if (healthBar == null)
        {
            Debug.LogError("HealthBar component is missing on the player!");
        }
    }

    private void Update()
    {
        if (isDead && canRespawn && Input.GetKeyDown(KeyCode.O))
        {
            Respawn();
        }
    }

    public void Die()
    {
        // Перевіряємо, чи вже мертвий гравець, якщо так — виходимо з методу.
        if (isDead) return;

        isDead = true;
        audioManager.PlaySFX(audioManager.death);

        playerAnimation.SetDeathAnimation();
        Debug.Log("Player is dead");

        GetComponent<PlayerMovement>().enabled = false;
        canRespawn = true;
    }

    private void Respawn()
    {
        isDead = false;

        canRespawn = false;
        healthBar.ResetHealth();

        playerAnimation.Respawn();
        Debug.Log("Player respawned");
        GetComponent<PlayerMovement>().enabled = true;
    }

    // private void DisableOtherComponents() {}
    // private void EnableOtherComponents() {}
}
