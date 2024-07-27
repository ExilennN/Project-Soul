using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1E1_ChaseState : ChaseState
{
    private L1E1_Enemy enemy;
    public L1E1_ChaseState(Entity entity, StateController stateController, string animBoolName, D_ChaseState stateData, L1E1_Enemy enemy) : base(entity, stateController, animBoolName, stateData)
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
        if (performCloseRangeAction)
        {
            stateController.ChangeState(enemy.clawAttackState);
        }
        if (performMidRangeAction)
        {
            stateController.ChangeState(enemy.chargeLeapState);
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
