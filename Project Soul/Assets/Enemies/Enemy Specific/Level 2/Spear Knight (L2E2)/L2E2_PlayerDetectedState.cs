using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2E2_PlayerDetectedState : PlayerDetectedState
{
    private L2E2_Enemy enemy;
    public L2E2_PlayerDetectedState(Entity entity, StateController stateController, string animBoolName, D_PlayerDetectedState stateData, L2E2_Enemy enemy) : base(entity, stateController, animBoolName, stateData)
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

        if (performCloseRangeAction && isDetectingGround)
        {
            stateController.ChangeState(enemy.meleeAttackState);
        }
        else if (isPlayerInBaseAggroArea || isPlayerInMinAggroRange)
        {
            stateController.ChangeState(enemy.chaseState);
        }

        if (!isPlayerInBaseAggroArea & !isPlayerInMinAggroRange)
        {
            enemy.SetTrakingBack(true);
            stateController.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
