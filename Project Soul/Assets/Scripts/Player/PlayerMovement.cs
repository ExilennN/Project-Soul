using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerAnimation playerAnimation;
    private PlayerCollision playerCollision;

    AudioManager audioManager;

    private float xAxis;
    private float currentVelocity;
    private bool facingRight = true;
    private Vector3 originalScale;

    [Header("---- Movement ----")]
    [SerializeField] private float moveSpeed = 17f;
    [SerializeField] private float deceleration = 65f;

    #region DashParametersRegion
    [Header("---- Dash ----")]
    [SerializeField] private float dashSpeed = 30f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    private bool isDashing = false;
    private bool canDash = true;
    #endregion

    #region JumpParametersRegion
    [Header("---- Jumping ----")]
    [SerializeField] private float jumpForce = 17f;
    [SerializeField] private float jumpTimeToMaxHeight = 0.15f;
    [SerializeField] private float maxJumpSpeed = 20f;
    [SerializeField] private float maxFallSpeed = -20f;
    [SerializeField] private bool canDoubleJump = true;

    private bool isJumping = false;
    private bool hasDoubleJumped = false;
    private float jumpTimeCounter;
    #endregion

    #region WallMovementParametersRegion
    [Header("---- Wall Movement ----")]
    [SerializeField] private float wallSlideSpeed;
    [SerializeField] private float wallJumpForce = 20f;
    [SerializeField] private Vector2 wallJumpDirection = new Vector2(1, 1);

    private bool isWallSliding;
    private bool isWallJumping;
    #endregion

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerCollision = GetComponent<PlayerCollision>();
        originalScale = transform.localScale;
    }

    private void Update()
    {
        CheckInput();
        CheckMovingDirection();
        HandleAirborneAnimation();
        ApplyFalling();
        CheckIfWallSliding();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            rb.velocity = new Vector2((facingRight ? 1 : -1) * dashSpeed, rb.velocity.y);
        }
        else
        {
            ApplyMovement();
        }
    }

    private void CheckIfWallSliding()
    {
        bool isTouchingWall = playerCollision.IsTouchingWall;
        bool isFalling = rb.velocity.y < 0;
        bool isPressingHorizontal = (facingRight && xAxis > 0) || (!facingRight && xAxis < 0);

        isWallSliding = isTouchingWall && !playerCollision.IsGrounded && isFalling && isPressingHorizontal;
    }

    private void CheckInput()
    {
        xAxis = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        if (isDashing) return;

        if (Input.GetButtonDown("Jump"))
        {
            if (playerCollision.IsGrounded)
            {
                isJumping = true;
                hasDoubleJumped = false;
                jumpTimeCounter = jumpTimeToMaxHeight;
                Jump();
                audioManager.PlaySFX(audioManager.jump);
            }
            else if (isWallSliding)
            {
                WallJump();
                audioManager.PlaySFX(audioManager.jump);
            }
            else if (canDoubleJump && !hasDoubleJumped)
            {
                isJumping = true;
                hasDoubleJumped = true;
                jumpTimeCounter = jumpTimeToMaxHeight;
                Jump();
                audioManager.PlaySFX(audioManager.jump);
            }
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                float adjustedJumpForce = Mathf.Clamp(jumpForce, 0, maxJumpSpeed);
                Jump(adjustedJumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }

    private void ApplyMovement()
    {
        if (isWallJumping)
        {
            return;
        }

        if (xAxis != 0)
        {
            currentVelocity = xAxis * moveSpeed;
            if (playerCollision.IsGrounded && !isJumping)
            {
                audioManager.PlayLoopedSFX(audioManager.run);
                playerAnimation.SetRunAnimation();
            }
            else
            {
                audioManager.StopLoopedSFX();
            }
        }
        else
        {
            currentVelocity = Mathf.MoveTowards(currentVelocity, 0, deceleration * Time.fixedDeltaTime);
            if (playerCollision.IsGrounded)
            {
                audioManager.StopLoopedSFX();
                playerAnimation.SetIdleAnimation();
            }
        }

        if (isWallSliding)
        {
            playerAnimation.SetSlideAnimation();
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }

        rb.velocity = new Vector2(currentVelocity, rb.velocity.y);
    }


    private void CheckMovingDirection()
    {
        if (xAxis > 0 && !facingRight)
        {
            Flip(true);
        }
        else if (xAxis < 0 && facingRight)
        {
            Flip(false);
        }
    }

    private void Flip(bool faceRight)
    {
        facingRight = faceRight;
        playerCollision.SetFacingDirection(facingRight);
        transform.localScale = new Vector3(faceRight ? originalScale.x : -originalScale.x, originalScale.y, originalScale.z);
    }

    public bool IsFacingRight()
    {
        return facingRight;
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        float originalGravityScale = rb.gravityScale;

        rb.gravityScale = 0f;
        rb.velocity = new Vector2((facingRight ? 1 : -1) * dashSpeed, 0);

        StartCoroutine(SpawnAfterImages());

        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = originalGravityScale;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
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
            afterImageScript.enabled = true;

            yield return new WaitForSeconds(0.05f);
        }
    }

    private void Jump(float force = 0)
    {
        if (force == 0)
        {
            force = jumpForce;
        }
        rb.velocity = new Vector2(rb.velocity.x, force);
    }

    private void ApplyFalling()
    {
        if (isDashing)
        {
            return;
        }

        rb.gravityScale = 8f;
        if (rb.velocity.y < maxFallSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
        }
    }

    private void HandleAirborneAnimation()
    {
        if (!playerCollision.IsGrounded)
        {
            if (rb.velocity.y > 0f)
            {
                playerAnimation.SetJumpAnimation();
            }
            else if (rb.velocity.y < 0f && !playerCollision.IsTouchingWall)
            {
                playerAnimation.SetFallAnimation();
            }
        }
    }

    private void WallJump()
    {
        isWallJumping = true;

        float xForce = wallJumpDirection.x * wallJumpForce * (facingRight ? -1 : 1);
        float yForce = wallJumpDirection.y * wallJumpForce;

        rb.velocity = new Vector2(xForce, yForce);

        Flip(facingRight);

        isWallSliding = false;
        isJumping = true;
        hasDoubleJumped = false;

        StartCoroutine(RestoreControlAfterWallJump());
    }

    private IEnumerator RestoreControlAfterWallJump()
    {
        yield return new WaitForSeconds(0.15f);
        isWallJumping = false;
    }

}
