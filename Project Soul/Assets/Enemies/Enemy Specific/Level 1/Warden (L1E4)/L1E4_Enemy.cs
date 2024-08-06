using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1E4_Enemy : Entity
{
    public L1E4_IdleState idleState { get; private set; }
    public L1E4_MoveState moveState { get; private set; }
    public L1E4_PlayerDetectedState playerDetectedState { get; private set; }
    public L1E4_ChaseState chaseState { get; private set; }
    public L1E4_MeleeAttackState meleeAttackState { get; private set; }
    public L1E4_MeleeSpecialAttackState meleeSpecialAttackState { get; private set; }

    [Header("Data References")]
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] private D_ChaseState chaseStateData;
    [SerializeField] private D_MeleeAttackState meleeAttackStateData;
    [SerializeField] private D_L1E4MeleeSpecialAttackState meleeSpecialAttackStateData;

    [Header("Attack Position Checks")]
    [SerializeField] private Transform meleeAttackPosition;
    [SerializeField] private Transform specialAttackPosition;
    [SerializeField] private Transform specialAttackPRJ1;
    [SerializeField] private Transform specialAttackPRJ2;


    public override void Start()
    {
        base.Start();

        moveState = new L1E4_MoveState(this, stateController, "move", moveStateData, this);
        idleState = new L1E4_IdleState(this, stateController, "idle", idleStateData, this);
        playerDetectedState = new L1E4_PlayerDetectedState(this, stateController, "playerDetected", playerDetectedStateData, this);
        chaseState = new L1E4_ChaseState(this, stateController, "playerDetected", chaseStateData, this);
        meleeAttackState = new L1E4_MeleeAttackState(this, stateController, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        meleeSpecialAttackState = new L1E4_MeleeSpecialAttackState(this, stateController, "specialMeleeAttack", specialAttackPosition, specialAttackPRJ1, specialAttackPRJ2, meleeSpecialAttackStateData, this);

        stateController.Initialize(moveState);
    }

    public D_MeleeAttackState GetMeleeAttackData() { return meleeAttackStateData; }
    public D_L1E4MeleeSpecialAttackState GetSpecialAttackData() { return meleeSpecialAttackStateData; }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(specialAttackPosition.position, meleeSpecialAttackStateData.attackRadius);
        Gizmos.DrawWireSphere(specialAttackPRJ1.position, 0.2f);
        Gizmos.DrawWireSphere(specialAttackPRJ2.position, 0.2f);
    }
}
