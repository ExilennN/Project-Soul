using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb; // Компонент Rigidbody2D
    private PlayerAnimation playerAnimation; // Анімації гравця
    private PlayerCollision playerCollision; // Взаємодії зіткнень гравця

    private float xAxis; // Вісь X для руху
    private float currentVelocity; // Поточна швидкість
    private bool facingRight = true; // Напрямок обличчям вправо
    private Vector3 originalScale; // Початковий масштаб

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 17f; // Швидкість руху
    [SerializeField] private float deceleration = 65f; // Сповільнення

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 30f; // Швидкість ривка
    [SerializeField] private float dashTime = 0.2f; // Час ривка
    [SerializeField] private float dashCooldown = 1f; // Час перезарядки ривка
    private bool isDashing = false; // Статус ривка
    private bool canDash = true; // Можливість зробити ривок

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Ініціалізація Rigidbody2D
        playerAnimation = GetComponent<PlayerAnimation>(); // Ініціалізація анімацій
        playerCollision = GetComponent<PlayerCollision>(); // Ініціалізація взаємодій зіткнень
        originalScale = transform.localScale; // Збереження початкового масштабу
    }

    void Update()
    {
        CheckInput(); // Перевірка вводу
        CheckMovingDirection(); // Перевірка напрямку руху
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            rb.velocity = new Vector2((facingRight ? 1 : -1) * dashSpeed, rb.velocity.y); // Рух під час ривка
        }
        else
        {
            Move(); // Звичайний рух
        }
    }

    private void CheckInput()
    {
        xAxis = Input.GetAxisRaw("Horizontal"); // Отримання вводу осі X

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash()); // Початок ривка
        }
    }

    private void Move()
    {
        if (xAxis != 0)
        {
            currentVelocity = xAxis * moveSpeed; // Розрахунок швидкості
            if (playerCollision.IsGrounded)
            {
                playerAnimation.SetRunAnimation(); // Установка анімації бігу
            }
        }
        else
        {
            currentVelocity = Mathf.MoveTowards(currentVelocity, 0, deceleration * Time.fixedDeltaTime); // Сповільнення до зупинки
            if (playerCollision.IsGrounded)
            {
                playerAnimation.SetIdleAnimation(); // Установка анімації спокою
            }
        }

        rb.velocity = new Vector2(currentVelocity, rb.velocity.y); // Застосування швидкості
    }

    private void CheckMovingDirection()
    {
        if (xAxis > 0 && !facingRight)
        {
            Flip(true); // Поворот вправо
        }
        else if (xAxis < 0 && facingRight)
        {
            Flip(false); // Поворот вліво
        }
    }

    private void Flip(bool faceRight)
    {
        facingRight = faceRight;
        transform.localScale = new Vector3(faceRight ? originalScale.x : -originalScale.x, originalScale.y, originalScale.z); // Зміна масштабу для відображення напрямку
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        StartCoroutine(SpawnAfterImages()); // Початок створення післяобразів

        yield return new WaitForSeconds(dashTime);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true; // Перезарядка ривка завершена
    }

    private IEnumerator SpawnAfterImages()
    {
        while (isDashing)
        {
            GameObject afterImage = PlayerAfterImagePool.Instance.GetFromPool();
            afterImage.transform.position = transform.position;
            afterImage.transform.localScale = transform.localScale;
            afterImage.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;

            PlayerAfterImageSprite afterImageScript = afterImage.GetComponent<PlayerAfterImageSprite>();
            afterImageScript.enabled = true; // Увімкнення скрипта для запуску процесу зникнення

            yield return new WaitForSeconds(0.05f); // Інтервал між післяобразами
        }
    }
}
