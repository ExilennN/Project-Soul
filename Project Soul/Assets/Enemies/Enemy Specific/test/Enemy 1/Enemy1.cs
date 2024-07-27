using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }
    public E1_PlayerDetectedState playerDetectedState { get; private set; }
    public E1_MeleeAttackState meleeAttackState { get; private set; }
    public E1_ChaseState chaseState { get; private set; }

    [Header("Data References")]
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] private D_MeleeAttackState meleeAttackStateData;
    [SerializeField] private D_ChaseState chaseStateData;

    [Header("Attack Positions Checks")]
    [SerializeField] private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();

        moveState = new E1_MoveState(this, stateController, "move", moveStateData, this);
        idleState = new E1_IdleState(this, stateController, "idle", idleStateData, this);
        playerDetectedState = new E1_PlayerDetectedState(this, stateController, "playerDetected", playerDetectedStateData, this);
        meleeAttackState = new E1_MeleeAttackState(this, stateController, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        chaseState = new E1_ChaseState(this, stateController, "playerDetected", chaseStateData, this);

        stateController.Initialize(moveState);
    }
}
