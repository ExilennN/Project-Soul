using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2E2_Enemy : Entity
{
    public L2E2_IdleState idleState { get; private set; }
    public L2E2_MoveState moveState { get; private set; }
    public L2E2_PlayerDetectedState playerDetectedState { get; private set; }
    public L2E2_ChaseState chaseState { get; private set; }
    public L2E2_MeleeAttackState meleeAttackState { get; private set; }


    [Header("Data References")]
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] private D_ChaseState chaseStateData;
    [SerializeField] private D_MeleeAttackState meleeAttackStateData;


    [Header("Attack Positions Checks")]
    [SerializeField] private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();

        idleState = new L2E2_IdleState(this, stateController, "idle", idleStateData, this);
        moveState = new L2E2_MoveState(this, stateController, "move", moveStateData, this);
        playerDetectedState = new L2E2_PlayerDetectedState(this, stateController, "playerDetected", playerDetectedStateData, this);
        chaseState = new L2E2_ChaseState(this, stateController, "playerDetected", chaseStateData, this);
        meleeAttackState = new L2E2_MeleeAttackState(this, stateController, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        
        stateController.Initialize(moveState);
    }


    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(meleeAttackPosition.position, meleeAttackStateData.attackZone);

    }
}
