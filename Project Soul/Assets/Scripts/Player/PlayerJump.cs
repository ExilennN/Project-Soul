using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    // Компоненти Rigidbody2D, PlayerCollision та PlayerAnimation
    private Rigidbody2D rb;
    private PlayerCollision playerCollision;
    private PlayerAnimation playerAnimation;

    [Header("Jumping")]
    [SerializeField] private float minJumpForce = 10f; // Мінімальна сила стрибка
    [SerializeField] private float maxJumpForce = 25f; // Максимальна сила стрибка
    [SerializeField] private float jumpTimeToMaxHeight = 0.15f; // Час досягнення максимальної висоти стрибка
    [SerializeField] private float maxJumpSpeed = 20f; // Максимальна швидкість стрибка
    [SerializeField] private float maxFallSpeed = -20f; // Максимальна швидкість падіння
    [SerializeField] private float fallSpeed = -20f; // Швидкість падіння
    [SerializeField] private float gravityMultiplier = 7f; // Множник гравітації
    [SerializeField] private bool canDoubleJump = true; // Чи можливо подвійний стрибок

    // Внутрішні змінні для керування стрибком
    private bool isJumping = false;
    private bool hasDoubleJumped = false;
    private float jumpTimeCounter;
    private float currentJumpForce;

    // Викликається перед першим оновленням кадру
    void Start()
    {
        // Ініціалізація компонентів
        rb = GetComponent<Rigidbody2D>();
        playerCollision = GetComponent<PlayerCollision>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    // Викликається кожного кадру
    void Update()
    {
        // Перевірка вводу для стрибка
        CheckJumpInput();
        // Обробка анімацій у повітрі
        HandleAirborneAnimation();
        // Застосування логіки падіння
        ApplyFalling();
    }

    // Перевірка вводу для стрибка
    private void CheckJumpInput()
    {
        // Коли гравець натискає кнопку стрибка
        if (Input.GetButtonDown("Jump"))
        {
            if (playerCollision.IsGrounded)
            {
                // Початок стрибка
                isJumping = true;
                hasDoubleJumped = false;
                jumpTimeCounter = jumpTimeToMaxHeight;
                currentJumpForce = minJumpForce;
                Jump();
            }
            else if (canDoubleJump && !hasDoubleJumped)
            {
                // Подвійний стрибок
                isJumping = true;
                hasDoubleJumped = true;
                jumpTimeCounter = jumpTimeToMaxHeight;
                currentJumpForce = minJumpForce;
                Jump();
            }
        }

        // Підтримка стрибка, коли гравець утримує кнопку
        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                // Плавне збільшення сили стрибка
                currentJumpForce = Mathf.Lerp(minJumpForce, maxJumpForce, (jumpTimeToMaxHeight - jumpTimeCounter) / jumpTimeToMaxHeight);
                currentJumpForce = Mathf.Clamp(currentJumpForce, minJumpForce, maxJumpSpeed);
                Jump();
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        // Коли гравець відпускає кнопку стрибка
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }

    // Застосування логіки падіння
    private void ApplyFalling()
    {
        if (!isJumping && rb.velocity.y < fallSpeed)
        {
            // Збільшення гравітації при падінні
            rb.gravityScale = gravityMultiplier;
        }
        else
        {
            rb.gravityScale = 7f;
        }

        // Обмеження максимальної швидкості падіння
        if (rb.velocity.y < maxFallSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
        }
    }

    // Обробка анімацій у повітрі
    private void HandleAirborneAnimation()
    {
        if (!playerCollision.IsGrounded)
        {
            if (rb.velocity.y > 0f)
            {
                // Анімація стрибка
                playerAnimation.SetJumpAnimation();
            }
            else if (rb.velocity.y < 0f)
            {
                // Анімація падіння
                playerAnimation.SetFallAnimation();
            }
        }
    }

    // Виконання стрибка
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, currentJumpForce);
    }
}
