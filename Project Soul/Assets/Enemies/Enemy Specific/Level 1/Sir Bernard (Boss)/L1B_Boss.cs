using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1B_Boss : Entity
{
    public L1B_IntroState introState { get; private set; }
    public L1B_DirectionalFollowState followState { get; private set; }  
    public L1B_MeleeAttackState meleeAttackState { get; private set; }
    public L1B_ChainPullAttackState chainPullAttackState { get; private set;}
    public L1B_WhirlwindAttackState whirlwindAttackState { get; private set; }
    public L1B_FloorSpikesAttackState floorSpikesAttackState { get; private set; }

    [Header("Data References")]
    [SerializeField] private D_IntroState introStateData;
    [SerializeField] private D_DirectionalFollowState followStateData;
    [SerializeField] private D_MeleeAttackState meleeAttackStateData;
    [SerializeField] private D_L1BChainPullAttackState chainPullAttackStateData;
    [SerializeField] private D_L1BWhirlwindAttackState whirlwindAttackStateData;
    [SerializeField] private D_L1BFloorSpikesAttackState floorSpikesAttackStateData;

    [Header("Attack Positions")]
    [SerializeField] private Transform attackRadiusCheckPosition;
    [SerializeField] private Transform meleeAttackPosition;
    [SerializeField] private Transform chainPullAttackPosition;
    [SerializeField] private Transform floorSpikesAttackPosition;

    public override void Start()
    {
        base.Start();
        Flip();

        introState = new L1B_IntroState(this, stateController, "intro", introStateData, this);
        followState = new L1B_DirectionalFollowState(this, stateController, "move", followStateData, this);
        meleeAttackState = new L1B_MeleeAttackState(this, stateController, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        chainPullAttackState = new L1B_ChainPullAttackState(this, stateController, "chainPullAttack", chainPullAttackPosition, chainPullAttackStateData, this);
        whirlwindAttackState = new L1B_WhirlwindAttackState(this, stateController, "whirlwindAttack", attackRadiusCheckPosition, whirlwindAttackStateData, this);
        floorSpikesAttackState = new L1B_FloorSpikesAttackState(this, stateController, "floorSpikesAttack", floorSpikesAttackPosition, floorSpikesAttackStateData, this);

        stateController.Initialize(introState);
    }

    public override bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.CircleCast(attackRadiusCheckPosition.position, entityData.closeRangeActionDistance, Vector2.up, 0, entityData.whatIsPlayer);
    }
    public override bool CheckPlayerInMidRangeAction()
    {
        return Physics2D.CircleCast(attackRadiusCheckPosition.position, entityData.midRangeActionDistance, Vector2.up, 0, entityData.whatIsPlayer);
    }
    public override bool CheckPlayerInLongRangeAction()
    {
        return Physics2D.CircleCast(attackRadiusCheckPosition.position, entityData.longRangeActionDistance, Vector2.up, 0, entityData.whatIsPlayer);
    }

    public D_L1BWhirlwindAttackState GetWhirlWindAttackData() { return whirlwindAttackStateData; }
    public D_L1BChainPullAttackState GetChainPullAttackData() { return chainPullAttackStateData; }
    public D_L1BFloorSpikesAttackState GetFloorSpikesAttackData() { return floorSpikesAttackStateData; }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackRadiusCheckPosition.position, entityData.closeRangeActionDistance);
        Gizmos.DrawWireSphere(attackRadiusCheckPosition.position, entityData.midRangeActionDistance);
        Gizmos.DrawWireSphere(attackRadiusCheckPosition.position, entityData.longRangeActionDistance);


        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(chainPullAttackPosition.position, 0.2f);
    }
}
