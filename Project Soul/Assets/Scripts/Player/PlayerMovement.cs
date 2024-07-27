using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Компоненти Rigidbody2D, PlayerAnimation та PlayerCollision
    private Rigidbody2D rb;
    private PlayerAnimation playerAnimation;
    private PlayerCollision playerCollision;
    
    private float xAxis; // Значення вводу по горизонталі
    private float currentVelocity; // Поточна швидкість гравця
    private bool facingRight = true; // Чи дивиться гравець вправо
    private Vector3 originalScale; // Оригінальний масштаб об'єкта

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 17f; // Швидкість руху
    [SerializeField] private float deceleration = 65f; // Уповільнення

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 30f; // Швидкість ривка
    [SerializeField] private float dashTime = 0.2f; // Час ривка
    [SerializeField] private float dashCooldown = 1f; // Час перезарядки ривка
    private bool isDashing = false; // Чи робить гравець ривок
    private bool canDash = true; // Чи може гравець робити ривок

    // Викликається перед першим оновленням кадру
    void Start()
    {
        // Ініціалізація компонентів
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerCollision = GetComponent<PlayerCollision>();
        originalScale = transform.localScale;
    }

    // Викликається кожного кадру
    void Update()
    {
        // Перевірка вводу
        CheckInput();
        // Перевірка напрямку руху
        CheckMovingDirection();
    }

    // Викликається з фіксованою частотою для фізичних обчислень
    void FixedUpdate()
    {
        if (isDashing)
        {
            // Під час ривка рухатися зі швидкістю ривка
            rb.velocity = new Vector2((facingRight ? 1 : -1) * dashSpeed, rb.velocity.y);
        }
        else
        {
            // Виконання руху
            Move();
        }
    }

    // Перевірка вводу
    private void CheckInput()
    {
        // Отримання значення по горизонтальній осі (A/D або стрілки)
        xAxis = Input.GetAxisRaw("Horizontal");

        // Перевірка вводу для ривка (ліва клавіша Shift)
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    // Рух гравця
    private void Move()
    {
        if (xAxis != 0)
        {
            // Розрахунок нової швидкості без урахування прискорення
            currentVelocity = xAxis * moveSpeed;
            if (playerCollision.IsGrounded)
            {
                // Встановлення анімації бігу
                playerAnimation.SetRunAnimation();
            }
        }
        else
        {
            // Уповільнення до повної зупинки
            currentVelocity = Mathf.MoveTowards(currentVelocity, 0, deceleration * Time.fixedDeltaTime);
            if (playerCollision.IsGrounded)
            {
                // Встановлення анімації очікування
                playerAnimation.SetIdleAnimation();
            }
        }

        // Оновлення швидкості Rigidbody2D
        rb.velocity = new Vector2(currentVelocity, rb.velocity.y);
    }

    // Перевірка напрямку руху
    private void CheckMovingDirection()
    {
        if (xAxis > 0 && !facingRight)
        {
            // Якщо рухається вправо, а гравець дивиться вліво, перевернути
            Flip(true);
        }
        else if (xAxis < 0 && facingRight)
        {
            // Якщо рухається вліво, а гравець дивиться вправо, перевернути
            Flip(false);
        }
    }

    // Перевернути гравця
    private void Flip(bool faceRight)
    {
        // Змінити напрямок, куди дивиться гравець
        facingRight = faceRight;
        // Оновити масштаб об'єкта для відображення напрямку
        transform.localScale = new Vector3(faceRight ? originalScale.x : -originalScale.x, originalScale.y, originalScale.z);
    }

    // Виконання ривка
    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        yield return new WaitForSeconds(dashTime);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
