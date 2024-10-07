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

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is missing on the checkpoint");
        }
        spriteRenderer.sprite = lampOffSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            PlayerDeath playerDeath = other.GetComponent<PlayerDeath>();
            if (playerDeath != null)
            {
                playerDeath.SetRespawnPoint(transform);
                light.enabled = true;
                isActivated = true;
                spriteRenderer.sprite = lampOnSprite;
                Debug.Log("Checkpoint activated");
            }
        }
    }
}
