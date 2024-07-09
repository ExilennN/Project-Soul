using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetectedState stateData;

    protected bool isPlayerInMinAggroRange;
    protected bool isPlayerInBaseAggroArea;
    public PlayerDetectedState(Entity entity, StateController stateController, string animBoolName, D_PlayerDetectedState stateData) : base(entity, stateController, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(0f);
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        isPlayerInBaseAggroArea = entity.CheckPlayerInBaseAggroAreaRange();
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
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        isPlayerInBaseAggroArea = entity.CheckPlayerInBaseAggroAreaRange();
    }
}
