using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Налаштування перевірки зіткнення із землею та стінами
    [SerializeField] private float groundCheckDistance = 2.5f; // Відстань для перевірки зіткнення із землею
    [SerializeField] private float wallCheckDistance = 0.7f; // Відстань для перевірки зіткнення зі стінами
    [SerializeField] private LayerMask groundLayer; // Шар, який представляє землю

    // Властивості для перевірки зіткнень
    public bool IsGrounded { get; private set; } // Чи гравець на землі
    public bool IsTouchingWall { get; private set; } // Чи торкається гравець стіни

    // Викликається з фіксованою частотою для фізичних обчислень
    private void FixedUpdate()
    {
        // Оновлення стану зіткнення із землею та стінами
        IsGrounded = CheckGrounded();
        IsTouchingWall = CheckWall();
    }

    // Перевірка, чи гравець на землі
    private bool CheckGrounded()
    {
        // Початкова точка для перевірки зіткнення із землею
        Vector2 groundCheckOrigin = new Vector2(transform.position.x, transform.position.y - 0.1f);
        // Виконання променевої перевірки вниз на певну відстань для визначення зіткнення із землею
        return Physics2D.Raycast(groundCheckOrigin, Vector2.down, groundCheckDistance, groundLayer);
    }

    // Перевірка, чи гравець торкається стіни
    private bool CheckWall()
    {
        // Початкові точки для перевірки зіткнення зі стінами з обох боків гравця
        Vector2 wallCheckOriginRight = new Vector2(transform.position.x + 0.1f, transform.position.y);

        // Виконання променевої перевірки вправо та вліво на певну відстань для визначення зіткнення зі стінами
        bool isTouchingWall = Physics2D.Raycast(wallCheckOriginRight, Vector2.right, wallCheckDistance, groundLayer);

        // Повернення результату, чи гравець торкається будь-якої зі стін
        return isTouchingWall;
    }

    // Додатково: відображення променевих перевірок для налагодження
    private void OnDrawGizmos()
    {
        // Відображення променевої перевірки для зіткнення із землею
        Vector2 groundCheckOrigin = new Vector2(transform.position.x, transform.position.y - 0.1f);
        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawLine(groundCheckOrigin, groundCheckOrigin + Vector2.down * groundCheckDistance);

        // Відображення променевих перевірок для зіткнення зі стінами
        Vector2 wallCheckOrigin = new Vector2(transform.position.x + 0.1f, transform.position.y);
        Gizmos.color = IsTouchingWall ? Color.green : Color.red;
        Gizmos.DrawLine(wallCheckOrigin, wallCheckOrigin + Vector2.right * wallCheckDistance);
    }
}
