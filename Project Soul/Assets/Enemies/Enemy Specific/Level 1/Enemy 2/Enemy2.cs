using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
    public E2_IdleState idleState {  get; private set; }
    public E2_MoveState moveState { get; private set; }
    public E2_PlayerDetectedState playerDetectedState { get; private set; }
    public E2_RangedAttackState rangedAttackState { get; private set; }

    [Header("Data References")]
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] private D_RangedAttackState rangedAttackStateData;

    [Header("Attack Positions Checks")]
    [SerializeField] private Transform rangedAttackPosition;

    public override void Start()
    {
        base.Start();

        moveState = new E2_MoveState(this, stateController, "move", moveStateData, this);
        idleState = new E2_IdleState(this, stateController, "idle", idleStateData, this);
        playerDetectedState = new E2_PlayerDetectedState(this, stateController, "playerDetected", playerDetectedStateData, this);
        rangedAttackState = new E2_RangedAttackState(this, stateController, "rangedAttack", rangedAttackPosition, rangedAttackStateData, this);

        stateController.Initialize(moveState);
    }
}
