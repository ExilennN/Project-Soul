using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1B_IntroState : IntroState
{
    private L1B_Boss enemy;
    public L1B_IntroState(Entity entity, StateController stateController, string animBoolName, D_IntroState stateData, L1B_Boss enemy) : base(entity, stateController, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isIntroOver)
        {
            stateController.ChangeState(enemy.followState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
