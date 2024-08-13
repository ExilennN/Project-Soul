using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalFollowState : State
{
    protected D_DirectionalFollowState stateData;

    protected bool isPlayerInCloseRange;
    protected bool isPlayerInMidRange;
    protected bool isPlayerInLongRange;

    public DirectionalFollowState(Entity entity, StateController stateController, string animBoolName, D_DirectionalFollowState stateData) : base(entity, stateController, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInCloseRange = entity.CheckPlayerInCloseRangeAction();
        isPlayerInMidRange = entity.CheckPlayerInMidRangeAction();
        isPlayerInLongRange = entity.CheckPlayerInLongRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(stateData.movementSpeed);
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

        int facingDirection = 0;
        if (entity.aliveGO.transform.position.x < entity.GetPlayerPosition().x) { facingDirection = 1; }
        else if (entity.aliveGO.transform.position.x > entity.GetPlayerPosition().x) { facingDirection = -1; }

        if (facingDirection != entity.facingDirection && facingDirection != 0) 
        { 
            entity.Flip();
            entity.SetVelocity(stateData.movementSpeed);
        }

    }
}
