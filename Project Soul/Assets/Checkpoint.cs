using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Sprite lampOnSprite;
    [SerializeField] private Sprite lampOffSprite;
    [SerializeField] private Light2D light;
    private SpriteRenderer spriteRenderer;
    private bool isActivated = false;
    private bool playerInRange = false;
    private PlayerAnimation playerAnimation;

    private void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is missing on the checkpoint");
        }
        spriteRenderer.sprite = lampOffSprite;
        light.enabled = false;
    }

    private void Update()
    {
        if (playerInRange && !isActivated && Input.GetKeyDown(KeyCode.E))
        {
            ActivateCheckpoint();

            if (playerAnimation != null)
            {
                playerAnimation.SetCheckpointAnimation();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            playerAnimation = other.GetComponent<PlayerAnimation>();
            if (playerAnimation == null)
            {
                Debug.LogError("PlayerAnimation component is missing on the player!");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerAnimation = null;
        }
    }

    private void ActivateCheckpoint()
    {
        isActivated = true;
        spriteRenderer.sprite = lampOnSprite;
        light.enabled = true;
        Debug.Log("Checkpoint activated");

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerDeath playerDeath = player.GetComponent<PlayerDeath>();
            if (playerDeath != null)
            {
                playerDeath.SetRespawnPoint(transform);
            }
        }
    }
}
