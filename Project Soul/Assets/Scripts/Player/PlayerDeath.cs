using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private PlayerAnimation playerAnimation;
    private bool isDead = false;
    private HealthBar healthBar;
    private bool canRespawn = false;

    private void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        GameObject health = GameObject.FindWithTag("Health");
        healthBar = health.GetComponent<HealthBar>();

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
        if (isDead) return;

        isDead = true;
        playerAnimation.SetDeathAnimation();
        Debug.Log("Player is dead");

        GetComponent<PlayerMovement>().enabled = false;
        // DisableOtherComponents();  
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
        // EnableOtherComponents();
    }

    // private void DisableOtherComponents() {}
    // private void EnableOtherComponents() {}
}
