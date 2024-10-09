using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Sprite lampOnSprite;
    [SerializeField] private Sprite lampOffSprite;
    [SerializeField] private Light2D light2D;
    private SpriteRenderer spriteRenderer;
    private bool isActivated = false;
    private bool playerInRange = false;

    private string checkpointKey;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is missing on the checkpoint");
        }

        checkpointKey = gameObject.name + "_Activated";

        if (PlayerPrefs.HasKey(checkpointKey) && PlayerPrefs.GetInt(checkpointKey) == 1)
        {
            isActivated = true;
            spriteRenderer.sprite = lampOnSprite;
            light2D.enabled = true;
        }
        else
        {
            spriteRenderer.sprite = lampOffSprite;
            light2D.enabled = false;
        }
    }

    private void Update()
    {
        if (playerInRange && !isActivated && Input.GetKeyDown(KeyCode.E))
        {
            ActivateCheckpoint();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void ActivateCheckpoint()
    {
        isActivated = true;
        spriteRenderer.sprite = lampOnSprite;
        light2D.enabled = true;

        PlayerPrefs.SetInt(checkpointKey, 1);

        PlayerPrefs.SetFloat("CheckpointX", transform.position.x);
        PlayerPrefs.SetFloat("CheckpointY", transform.position.y);
        PlayerPrefs.SetFloat("CheckpointZ", transform.position.z);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerDeath playerDeath = player.GetComponent<PlayerDeath>();
            if (playerDeath != null)
            {
                playerDeath.SetRespawnPoint(transform);
            }
        }

        PlayerPrefs.Save();
    }
}
