using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private float groundCheckDistance = 2.5f;
    [SerializeField] private float wallCheckDistance = 0.7f;
    [SerializeField] private LayerMask groundLayer;

    public bool IsGrounded { get; private set; }
    public bool IsTouchingWall { get; private set; }

    private bool facingRight = true;

    private void FixedUpdate()
    {
        IsGrounded = CheckGrounded();
        IsTouchingWall = CheckWall();
    }

    private bool CheckGrounded()
    {
        Vector2 groundCheckOrigin = new Vector2(transform.position.x, transform.position.y - 0.1f);
        return Physics2D.Raycast(groundCheckOrigin, Vector2.down, groundCheckDistance, groundLayer);
    }

    private bool CheckWall()
    {
        Vector2 wallCheckOrigin = new Vector2(transform.position.x, transform.position.y);
        Vector2 wallCheckDirection = facingRight ? Vector2.right : Vector2.left;
        return Physics2D.Raycast(wallCheckOrigin, wallCheckDirection, wallCheckDistance, groundLayer);
    }

    public void SetFacingDirection(bool isFacingRight)
    {
        facingRight = isFacingRight;
    }

    private void OnDrawGizmos()
    {
        Vector2 groundCheckOrigin = new Vector2(transform.position.x, transform.position.y - 0.1f);
        Gizmos.color = IsGrounded ? Color.green : Color.white;
        Gizmos.DrawLine(groundCheckOrigin, groundCheckOrigin + Vector2.down * groundCheckDistance);

        Vector2 wallCheckOrigin = new Vector2(transform.position.x, transform.position.y);
        Vector2 wallCheckDirection = facingRight ? Vector2.right : Vector2.left;
        Gizmos.color = IsTouchingWall ? Color.green : Color.white;
        Gizmos.DrawLine(wallCheckOrigin, wallCheckOrigin + wallCheckDirection * wallCheckDistance);
    }
}
