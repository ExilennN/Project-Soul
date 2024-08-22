using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2E1_IdleState : IdleState
{
    private L2E1_Enemy enemy;
    public L2E1_IdleState(Entity entity, StateController stateController, string animBoolName, D_IdleState stateData, L2E1_Enemy enemy) : base(entity, stateController, animBoolName, stateData)
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

        if (isPlayerInMinAggroRange)
        {
            stateController.ChangeState(enemy.playerDetectedState);
        }

        if (isIdleTimeOver)
        {
            stateController.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected override void NextPatrolPointHandle()
    {
        base.NextPatrolPointHandle();
    }
}
