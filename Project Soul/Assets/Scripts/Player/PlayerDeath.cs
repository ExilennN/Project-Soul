using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    AudioManager audioManager;
    private PlayerAnimation playerAnimation;
    private bool isDead = false;
    private PlayerHealthBar healthBar;
    private bool canRespawn = false;
    private Rigidbody2D rb;
    [SerializeField] private Transform respawnPoint;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
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

        if (respawnPoint == null)
        {
            Debug.LogError("Respawn point is not set!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Die();
        }

        if (isDead && canRespawn && Input.GetKeyDown(KeyCode.O))
        {
            Respawn();
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        // audioManager.PlaySFX(audioManager.death);
        playerAnimation.SetDeathAnimation();
        Debug.Log("Player is dead");
        healthBar.SetHealth(0);
        GetComponent<PlayerMovement>().enabled = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
        canRespawn = true;
        Invoke("Respawn", 1f); // respawn cherez 1 secundu
    }

    public void SetRespawnPoint(Transform newRespawnPoint)
    {
        respawnPoint = newRespawnPoint;
        Debug.Log("New respawn point set at: " + respawnPoint.position);
    }

    private void Respawn()
    {
        isDead = false;
        canRespawn = false;

        healthBar.ResetHealth();

        playerAnimation.Respawn();
        Debug.Log("Player respawned at: " + respawnPoint.position);

        GetComponent<PlayerMovement>().enabled = true;
        transform.position = respawnPoint.position;
    }

}
