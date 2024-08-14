using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1E3_PlayerDetectedState : PlayerDetectedState
{
    private L1E3_Enemy enemy;
    public L1E3_PlayerDetectedState(Entity entity, StateController stateController, string animBoolName, D_PlayerDetectedState stateData, L1E3_Enemy enemy) : base(entity, stateController, animBoolName, stateData)
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

        if (isPlayerInLOS)
        {
            if (Time.time >= enemy.rangedMagicAttackState.startTime + enemy.GetRangedAttackData().attackCooldowm)
            {
                stateController.ChangeState(enemy.rangedMagicAttackState);
            }
            else
            {
                entity.ResetVelocity();
            }
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
