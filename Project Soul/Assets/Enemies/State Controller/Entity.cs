using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.PlasticSCM.Editor.WebApi;
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

    [Header("Other Positions")]
    [SerializeField] private Transform[] patrollPoints;
    [SerializeField] private Transform homePoint;
    [SerializeField] private Transform playerPosition;

    [Header("Grid Controller")]
    [SerializeField] private GridController gridController;

    public StateController stateController;

    public int facingDirection { get; private set; }

    public Rigidbody2D rb {  get; private set; }
    public Animator anim { get; private set; }  
    public GameObject aliveGO { get;private set; }
    public Seeker seeker { get; private set; }
    public AnimationToStatecontroller atsc { get; private set; }

    private float currentHealth;

    protected int currentPatrollPoint { get; private set; }
    private int pointStep;

    private Vector2 velocityWorkspace;

    protected bool isDead;
    public bool isTrackingBack { get; private set; }

    public virtual void Start()
    {
        facingDirection = 1;
        currentHealth = entityData.maxHealth;

        isDead = false;
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
        stateController.currentState.LogicUpdate();

    }

    public virtual void FixedUpdate()
    {
        stateController.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
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
        currentHealth -= attackDetails.damageAmout;

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
        else { currentPatrollPoint += pointStep;  }
    }

    public virtual Transform GetCurrectPatrollPoint()
    {
        return patrollPoints[currentPatrollPoint];
    }
    public virtual Vector3 GetPlayerPosition()
    {
        return playerPosition.position;
    }
    public virtual Transform GetBasePosition()
    {
        return homePoint;
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

    public virtual bool CheckPlayerInBaseAggroAreaRange()
    {
        return Physics2D.CircleCast(homePoint.position, entityData.maxAggroDistance, Vector2.up, 0, entityData.whatIsPlayer);
    }   

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    } 

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance), 0.2f);
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + (Vector3)(Vector2.down * entityData.groundCheckDistance));
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.minAggroDistance), 0.2f);
        Gizmos.DrawWireSphere(homePoint.position, entityData.maxAggroDistance);
    }
}
