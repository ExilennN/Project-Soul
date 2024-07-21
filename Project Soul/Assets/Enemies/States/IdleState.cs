using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected D_IdleState stateData;

    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;
    protected bool isPlayerInMinAggroRange;

    protected float idleTime;

    protected int desiredFacingDirection;
    public IdleState(Entity entity, StateController stateController, string animBoolName, D_IdleState stateData) : base(entity, stateController, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(0f);

        desiredFacingDirection = 1;
        isIdleTimeOver = false;
        
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();
        if (flipAfterIdle) { entity.Flip(); }
        NextPatrolPointHandle();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime+idleTime) { isIdleTimeOver = true; }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected virtual void NextPatrolPointHandle()
    {
        entity.NextPatrollPoint();
        if (entity.aliveGO.transform.position.x > entity.GetCurrectPatrollPoint().position.x) { desiredFacingDirection = -1; }
        else { desiredFacingDirection = 1; }
        if (entity.facingDirection != desiredFacingDirection) { entity.Flip(); }
    }

    public void SetFlipAfterIdle(bool flipAfterIdle)
    {
        this.flipAfterIdle = flipAfterIdle;
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }

 
}
