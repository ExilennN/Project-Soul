using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2E1_Enemy : Entity
{
    public L2E1_IdleState idleState { get; private set; }
    public L2E1_MoveState moveState { get; private set; }
    public L2E1_PlayerDetectedState playerDetectedState { get; private set; }
    public L2E1_ChaseState chaseState { get; private set; }
    public L2E1_MeleeAttackState meleeAttackState { get; private set; }
    public L2E1_ShieldBlockState shieldBlockState { get; private set; }


    [Header("Data References")]
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] private D_ChaseState chaseStateData;
    [SerializeField] private D_MeleeAttackState meleeAttackStateData;
    [SerializeField] private D_ShieldBlockState shieldBlockStateData;


    [Header("Attack Positions Checks")]
    [SerializeField] private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();

        idleState = new L2E1_IdleState(this, stateController, "idle", idleStateData, this);
        moveState = new L2E1_MoveState(this, stateController, "move", moveStateData, this);
        playerDetectedState = new L2E1_PlayerDetectedState(this, stateController, "playerDetected", playerDetectedStateData, this);
        chaseState = new L2E1_ChaseState(this, stateController, "playerDetected", chaseStateData, this);
        meleeAttackState = new L2E1_MeleeAttackState(this, stateController, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        shieldBlockState = new L2E1_ShieldBlockState(this, stateController, "shieldBlock", shieldBlockStateData, this);

        stateController.Initialize(moveState);
    }

    public D_MeleeAttackState GetMeleeAttackData() { return meleeAttackStateData; }
    public D_ShieldBlockState GetShieldBlockData() { return shieldBlockStateData; }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);

    }
}
