using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2E1_PlayerDetectedState : PlayerDetectedState
{
    private L2E1_Enemy enemy;
    public L2E1_PlayerDetectedState(Entity entity, StateController stateController, string animBoolName, D_PlayerDetectedState stateData, L2E1_Enemy enemy) : base(entity, stateController, animBoolName, stateData)
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

        if (isDetectingGround && performLongRangeAction && Time.time >= enemy.shieldBlockState.endTime + enemy.GetShieldBlockData().actionCooldown)
        {
            if (performMidRangeAction || performCloseRangeAction) 
            {
                stateController.ChangeState(enemy.shieldBlockState);
            }
            else
            {
                if (Random.Range(0, 3) == 0) { stateController.ChangeState(enemy.shieldBlockState); }
            }
        }
        else if (isDetectingGround && performCloseRangeAction)
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
