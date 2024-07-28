using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1E1_Enemy : Entity
{
    public L1E1_IdleState idleState { get; private set; }
    public L1E1_MoveState moveState { get; private set; }
    public L1E1_PlayerDetectedState playerDetectedState { get; private set; }
    public L1E1_ChaseState chaseState { get; private set; }
    public L1E1_ChargeLeapState chargeLeapState { get; private set; }

    public L1E1_MeleeClawAttackState clawAttackState {  get; private set; }

    [Header("Data References")]
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] private D_ChaseState chaseStateData;
    [SerializeField] private D_MeleeAttackState clawAttackStateData;
    [SerializeField] private D_ChargeLeapStateData chargeLeapStateData;

    [Header("Attack Positions Checks")]
    [SerializeField] private Transform clawAttackPosition;



    public override void Start()
    {
        base.Start();

        moveState = new L1E1_MoveState(this, stateController, "move", moveStateData, this);
        idleState = new L1E1_IdleState(this, stateController, "idle", idleStateData, this);
        playerDetectedState = new L1E1_PlayerDetectedState(this, stateController, "playerDetected", playerDetectedStateData, this);
        chaseState = new L1E1_ChaseState(this, stateController, "playerDetected", chaseStateData, this);
        clawAttackState = new L1E1_MeleeClawAttackState(this, stateController, "clawAttack", clawAttackPosition, clawAttackStateData, this);
        chargeLeapState = new L1E1_ChargeLeapState(this, stateController, "playerDetected", chargeLeapStateData, this);

        stateController.Initialize(moveState);
    }

    public D_ChargeLeapStateData GetChargeLeapData() { return chargeLeapStateData; }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(clawAttackPosition.position, clawAttackStateData.attackRadius);
    }
}
