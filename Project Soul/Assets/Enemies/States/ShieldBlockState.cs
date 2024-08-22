using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBlockState : State
{
    protected D_ShieldBlockState stateData;

    protected bool isShieldUpTimeOver;
    protected bool performCloseRangeAction;

    protected float shieldTime;
    public ShieldBlockState(Entity entity, StateController stateController, string animBoolName, D_ShieldBlockState stateData) : base(entity, stateController, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        entity.ResetVelocity();
        isShieldUpTimeOver = false;
        SetShieldTime();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + shieldTime) { isShieldUpTimeOver = true; }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected void SetShieldTime()
    {
        shieldTime = Random.Range(stateData.minShieldUpTime, stateData.maxShieldUpTime);
    }
}
