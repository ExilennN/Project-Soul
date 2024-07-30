using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1E2_Enemy : Entity
{
    public L1E2_IdleState idleState { get; private set; }
    public L1E2_MoveState moveState { get; private set; }
    public L1E2_PlayerDetectedState playerDetectedState { get; private set; }
    public L1E2_ChaseState chaseState { get; private set; }
    public L1E2_MeleeAttackState meleeAttackState { get; private set; }


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

        moveState = new L1E2_MoveState(this, stateController, "move", moveStateData, this);
        idleState = new L1E2_IdleState(this, stateController, "idle", idleStateData, this);
        playerDetectedState = new L1E2_PlayerDetectedState(this, stateController, "playerDetected", playerDetectedStateData, this);
        chaseState = new L1E2_ChaseState(this, stateController, "playerDetected", chaseStateData, this);
        meleeAttackState = new L1E2_MeleeAttackState(this, stateController, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);

        stateController.Initialize(moveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
