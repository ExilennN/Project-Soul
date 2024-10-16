using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Entity : MonoBehaviour
{
    [Header("Entity Data")]
    public D_Entity entityData;

    [Header("Checks Positions")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private Transform gridPosition;


    [Header("Other Positions")]
    [SerializeField] private Transform[] patrollPoints;
    [SerializeField] private Transform homePoint;
    [SerializeField] private Transform playerPosition;

    [Header("Grid Controller")]
    [SerializeField] private GridController gridController;

    public StateController stateController;

    public int facingDirection { get; private set; }

    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject aliveGO { get; private set; }
    public Seeker seeker { get; private set; }
    public AnimationToStatecontroller atsc { get; private set; }

    private float currentHealth;

    protected int currentPatrollPoint { get; private set; }
    private int pointStep;

    private Vector2 velocityWorkspace;

    protected bool isDead;
    public bool isDamaged;
    public bool isTrackingBack { get; private set; }

    public virtual void Start()
    {
        facingDirection = 1;
        currentHealth = entityData.maxHealth;

        isDead = false;
        isDamaged = false;
        isTrackingBack = false;

        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();
        atsc = aliveGO.GetComponent<AnimationToStatecontroller>();

        currentPatrollPoint = 0;
        pointStep = 1;
        stateController = new StateController();
        seeker = new Seeker(gridController.pathGrid);
    }



    public virtual void Update()
    {
        if (isDead) { Destroy(gameObject); }

        stateController.currentState.LogicUpdate();

    }

    public virtual void FixedUpdate()
    {
        if (isDead) { Destroy(gameObject); }
        stateController.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkspace;
    }
    public virtual void SetVelocity(float velocity, Vector2 direction)
    {
        velocityWorkspace = direction * velocity;
        rb.velocity = velocityWorkspace;
    }

    public virtual void ResetVelocity()
    {
        velocityWorkspace.Set(0f, 0f);
        rb.velocity = velocityWorkspace;
    }
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance, entityData.whatIsWall);
    }

    public virtual bool CheckGround()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, entityData.groundCheckDistance, entityData.whatIsGround);
    }

    public virtual void Damage(AttackDetails attackDetails)
    {
        isDamaged = true;
        currentHealth -= attackDetails.damageAmout;
        rb.AddForce(attackDetails.position.normalized * 2f * -1, ForceMode2D.Impulse);
        isDamaged = false;
        if (currentHealth <= 0) { isDead = true; }
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0, 180, 0);
    }

    public void SetTrakingBack(bool isTrackingBack) { this.isTrackingBack = isTrackingBack; }

    public virtual void NextPatrollPoint()
    {
        if (currentPatrollPoint == patrollPoints.Length - 1) { pointStep = -1; currentPatrollPoint += pointStep; }
        else if (currentPatrollPoint == 0) { pointStep = 1; currentPatrollPoint += pointStep; }
        else { currentPatrollPoint += pointStep; }
    }

    public virtual Transform GetCurrectPatrollPoint()
    {
        return patrollPoints[currentPatrollPoint];
    }
    public virtual Vector3 GetPlayerPosition()
    {
        return playerPosition.position;
    }
    public virtual Transform GetPlayerPositionTransform()
    {
        return playerPosition;
    }
    public virtual Transform GetBasePosition()
    {
        return homePoint;
    }
    public virtual Transform GetEntityPositionOnGrid()
    {
        return gridPosition;
    }
    public virtual bool CheckPlayerInMinAggroRange()
    {
        bool isPlayerInRange = false;
        RaycastHit2D hit = Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.minAggroDistance, entityData.whatIsGround | entityData.whatIsPlayer);
        if (hit.collider != null)
        {
            if ((entityData.whatIsPlayer.value & (1 << hit.collider.gameObject.layer)) != 0) { isPlayerInRange = true; }
        }
        return isPlayerInRange;
    }
    public virtual Vector3 GetPlayerLosPosition()
    {
        return GameObject.Find("LOS").gameObject.transform.position;
    }
    public virtual bool CheckIfPlayerInLineOfSight()
    {
        bool isPlayerInLOS = false;
        Vector3 direction = GetPlayerLosPosition() - transform.position;
        RaycastHit2D hit = Physics2D.CircleCast(playerCheck.position, 0.5f, new Vector2(direction.x, direction.y).normalized, entityData.longRangeActionDistance, entityData.whatIsGround | entityData.whatIsPlayer);
        if (hit.collider != null)
        {
            if ((entityData.whatIsPlayer.value & (1 << hit.collider.gameObject.layer)) != 0) { isPlayerInLOS = true; }
        }
        return isPlayerInLOS;
    }
    public virtual bool CheckPlayerInBaseAggroAreaRange()
    {
        return Physics2D.CircleCast(homePoint.position, entityData.baseRadius, Vector2.up, 0, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMidRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.midRangeActionDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInLongRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.longRangeActionDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckDistanceFromHorizontalPointToPlayer(float distance)
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, distance, entityData.whatIsPlayer);
    }
    public virtual bool CheckDistanceFromHorizontalPointToWall(float distance)
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, distance, entityData.whatIsGround);
    }
    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + (Vector3)(Vector2.down * entityData.groundCheckDistance));
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.minAggroDistance), 0.2f);
        Gizmos.DrawWireSphere(homePoint.position, entityData.baseRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.closeRangeActionDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.midRangeActionDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.longRangeActionDistance), 0.2f);
    }
}
