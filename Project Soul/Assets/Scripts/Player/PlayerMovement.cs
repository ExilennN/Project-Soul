using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; // Посилання на Rigidbody2D гравця для контролю фізики
    [SerializeField] private Transform groundCheck; // Точка для перевірки, чи знаходиться гравець на землі
    [SerializeField] private LayerMask groundLayer; // Шар землі
    [SerializeField] private Transform wallCheck; // Точка для перевірки, чи знаходиться гравець біля стіни
    [SerializeField] private LayerMask wallLayer; // Шар стіни
    [SerializeField] private TrailRenderer tr; // Посилання на TrailRenderer для візуального ефекту при деші

    [Header("Movement")]
    [SerializeField] private float speed = 4f; // Швидкість руху гравця
    [SerializeField] private float jumpPower = 11f; // Сила стрибка гравця
    private float horizontal; // Зберігання горизонтального вводу
    private bool isFacingRight = true; // Відстеження, в якому напрямку дивиться гравець
    private float jumpCount; // Лічильник кількості стрибків
    private float maxJumpCount = 1; // Максимальна кількість стрибків

    [Header("Wall Slide/Jump Movement")]
    [SerializeField] private float wallSlidingSpeed = 2f; // Швидкість ковзання по стіні
    [SerializeField] private float wallJumpingTime = 0.2f; // Час, протягом якого можна зробити стрибок від стіни
    [SerializeField] private float wallJumpingDuration = 0.4f; // Тривалість стрибка від стіни
    [SerializeField] private Vector2 wallJumpingPower = new Vector2(12f, 20f); // Сила стрибка від стіни
    private float wallJumpingDirection; // Напрямок стрибка від стіни
    private float wallJumpingCounter; // Лічильник часу стрибка від стіни

    private bool isWallSliding; // Відстеження, чи ковзає гравець по стіні
    private bool isWallJumping; // Відстеження, чи стрибає гравець від стіни

    [Header("Dashing")]
    [SerializeField] private float dashingPower = 5f; // Сила деша
    [SerializeField] private float dashingTime = 0.2f; // Тривалість деша
    [SerializeField] private float dashingCooldown = 0.5f; // Час відновлення після деша

    private bool canDash = true; // Відстеження, чи може гравець зробити деша
    private bool isDashing; // Відстеження, чи робить гравець деша
    private Vector2 dashingDir; // Напрямок деша

    void Update()
    {
        HandleInput(); // Обробка вводу гравця
        HandleFlip(); // Перевертання персонажа, якщо змінюється напрямок руху
        HandleWallSlide(); // Обробка ковзання по стіні
        HandleWallJump(); // Обробка стрибків від стіни

        if (IsGrounded())
        {
            jumpCount = 0; // Скидання лічильника стрибків, коли гравець на землі
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            rb.velocity = dashingDir.normalized * dashingPower; // Виконання деша
        }
        else if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue)); // Виконання ковзання по стіні
        }
        else if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); // Звичайний рух гравця
        }
    }

    private void HandleInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); // Отримання горизонтального вводу
        if (Input.GetButtonDown("Jump"))
        {
            if (jumpCount < maxJumpCount)
            {
                Jump(); // Виконання стрибка
            }
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f); // Зменшення швидкості вертикального руху при відпусканні стрибка
        }
        if (Input.GetButtonDown("Dash") && canDash)
        {
            StartDash(); // Виконання деша
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpPower); // Виконання стрибка
        jumpCount++; // Збільшення лічильника стрибків
    }

    private void StartDash()
    {
        canDash = false; // Заборона виконання деша, поки він не завершиться
        isDashing = true; // Встановлення стану деша
        tr.emitting = true; // Включення візуального ефекту деша
        dashingDir = new Vector2(horizontal, Input.GetAxisRaw("Vertical")); // Встановлення напрямку деша
        if (dashingDir == Vector2.zero)
        {
            dashingDir = new Vector2(transform.localScale.x, 0); // Якщо немає напрямку, використовуємо напрямок персонажа
        }
        StartCoroutine(StopDashing()); // Початок корутини для завершення деша
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime); // Очікування завершення деша
        tr.emitting = false; // Вимкнення візуального ефекту деша
        isDashing = false; // Завершення деша
        yield return new WaitForSeconds(dashingCooldown); // Очікування відновлення деша
        canDash = true; // Дозвіл виконання деша
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer); // Перевірка, чи знаходиться гравець на землі
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapBox(wallCheck.position, new Vector2(0.3f, 5.5f), 0, wallLayer); // Перевірка, чи знаходиться гравець біля стіни
    }

    private void HandleWallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true; // Встановлення стану ковзання по стіні
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue)); // Обмеження швидкості ковзання по стіні
        }
        else
        {
            isWallSliding = false; // Вимкнення стану ковзання по стіні
        }
    }

    private void HandleWallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false; // Заборона стрибка під час ковзання по стіні
            wallJumpingDirection = -transform.localScale.x; // Встановлення напрямку стрибка від стіни
            wallJumpingCounter = wallJumpingTime; // Встановлення лічильника часу для стрибка від стіни

            CancelInvoke(nameof(StopWallJumping)); // Відміна попереднього виклику функції зупинки стрибка від стіни
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime; // Зменшення лічильника часу для стрибка від стіни
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true; // Встановлення стану стрибка від стіни
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y); // Виконання стрибка від стіни
            wallJumpingCounter = 0f; // Скидання лічильника часу для стрибка від стіни

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight; // Зміна напрямку погляду персонажа
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f; // Зміна масштабу персонажа для перевертання
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration); // Виклик функції для зупинки стрибка від стіни через певний час
        }

        // Дозволяє гравцеві контролювати напрямок стрибка
        if (isWallJumping && horizontal != 0f)
        {
            rb.velocity = new Vector2(horizontal * wallJumpingPower.x, rb.velocity.y); // Зміна напрямку стрибка відповідно до вводу гравця
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false; // Зупинка стрибка від стіни
    }

    private void HandleFlip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight; // Зміна напрямку погляду персонажа
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f; // Зміна масштабу персонажа для перевертання
            transform.localScale = localScale;
        }
    }
}
