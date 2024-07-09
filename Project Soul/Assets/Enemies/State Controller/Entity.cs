using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public StateController stateController;

    public D_Entity entityData;

    public int facingDirection { get; private set; }

    public Rigidbody2D rb {  get; private set; }
    public Animator anim { get; private set; }  
    public GameObject aliveGO { get;private set; }

    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform[] patrollPoints;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private Transform homePoint;
    protected int currentPatrollPoint { get; private set; }
    private int pointStep;

    private Vector2 velocityWorkspace;

    public virtual void Start()
    {
        facingDirection = 1;

        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();

        currentPatrollPoint = 0;
        pointStep = 1;
        stateController = new StateController();
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

    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0, 180, 0);
    }

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

    public virtual bool CheckPlayerInMinAggroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.minAggroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInBaseAggroAreaRange()
    {
        return Physics2D.CircleCast(homePoint.position, entityData.maxAggroDistance, Vector2.up, entityData.maxAggroDistance, entityData.whatIsPlayer);
    }

    public virtual void OnDrawGizmos()
    {
        //  Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        //  Gizmos.DrawLine(groundCheck.position, groundCheck.position + (Vector3)(Vector2.down * entityData.groundCheckDistance));
        // Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.minAggroDistance));
        // Gizmos.DrawWireSphere(homePoint.position, entityData.maxAggroDistance);

    }
}
