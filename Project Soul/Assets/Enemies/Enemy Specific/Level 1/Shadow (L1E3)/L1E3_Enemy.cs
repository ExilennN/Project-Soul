using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1E3_Enemy : Entity
{
    public L1E3_IdleState idleState { get; private set; }
    public L1E3_FlyMoveState moveState { get; private set; }
    public L1E3_PlayerDetectedState playerDetectedState { get; private set; }
    public L1E3_FlyChaseState chaseState { get; private set; }
    public L1E3_RangedMagicAttackState rangedMagicAttackState { get; private set; }


    [Header("Data References")]
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_FlyMoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] private D_FlyChaseState chaseStateData;
    [SerializeField] private D_RangedAttackState rangedAttackStateData;

    [Header("Attack Positions Checks")]
    [SerializeField] private Transform rangedAttackPosition;


    public override void Start()
    {
        base.Start();

        moveState = new L1E3_FlyMoveState(this, stateController, "move", moveStateData, this);
        idleState = new L1E3_IdleState(this, stateController, "idle", idleStateData, this);
        playerDetectedState = new L1E3_PlayerDetectedState(this, stateController, "playerDetected", playerDetectedStateData, this);
        chaseState = new L1E3_FlyChaseState(this, stateController, "playerDetected", chaseStateData, this);
        rangedMagicAttackState = new L1E3_RangedMagicAttackState(this, stateController, "rangedMagicAttack", rangedAttackPosition, rangedAttackStateData, this);

        stateController.Initialize(moveState);
    }

    public D_RangedAttackState GetRangedAttackData() { return rangedAttackStateData; }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(rangedAttackPosition.position, 0.2f);
    }
}
