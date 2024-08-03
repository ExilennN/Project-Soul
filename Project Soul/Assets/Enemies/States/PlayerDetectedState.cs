using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetectedState stateData;

    protected bool isDetectingGround;
    protected bool isPlayerInMinAggroRange;
    protected bool isPlayerInBaseAggroArea;
    protected bool performCloseRangeAction;
    protected bool performMidRangeAction;
    protected bool performLongRangeAction;
    protected bool isPlayerInLOS;
    public PlayerDetectedState(Entity entity, StateController stateController, string animBoolName, D_PlayerDetectedState stateData) : base(entity, stateController, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        isPlayerInBaseAggroArea = entity.CheckPlayerInBaseAggroAreaRange();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        performMidRangeAction = entity.CheckPlayerInMidRangeAction();
        performLongRangeAction = entity.CheckPlayerInLongRangeAction();
        isDetectingGround = entity.CheckGround();
        isPlayerInLOS = entity.CheckIfPlayerInLineOfSight();
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }

}
