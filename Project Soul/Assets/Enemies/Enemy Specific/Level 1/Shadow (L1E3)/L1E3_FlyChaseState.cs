using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1E3_FlyChaseState : FlyChaseState
{
    private L1E3_Enemy enemy;
    public L1E3_FlyChaseState(Entity entity, StateController stateController, string animBoolName, D_FlyChaseState stateData, L1E3_Enemy enemy) : base(entity, stateController, animBoolName, stateData)
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

        if (!isPlayerInBaseAggroArea && !isPlayerInMinAggroRange)
        {
            enemy.SetTrakingBack(true);
            stateController.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected override bool StopChase()
    {
        return isPlayerInLOS;
    }
}
