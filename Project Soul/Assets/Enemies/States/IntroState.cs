using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroState : State
{
    D_IntroState stateData;
    protected bool isIntroOver;
    public IntroState(Entity entity, StateController stateController, string animBoolName, D_IntroState stateData) : base(entity, stateController, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isIntroOver = Time.time >= startTime + stateData.durationTime;
    }

    public override void Enter()
    {
        base.Enter();
        entity.ResetVelocity();
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
