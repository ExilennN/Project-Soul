using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1E4_ChaseState : ChaseState
{
    private L1E4_Enemy enemy;
    public L1E4_ChaseState(Entity entity, StateController stateController, string animBoolName, D_ChaseState stateData, L1E4_Enemy enemy) : base(entity, stateController, animBoolName, stateData)
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

        if (Time.time >= enemy.meleeSpecialAttackState.startTime + enemy.GetSpecialAttackData().attackCooldown && isDetectingGround)
        {
            stateController.ChangeState(enemy.meleeSpecialAttackState);
        }

        else if (performCloseRangeAction && isDetectingGround)
        {
            if (Time.time >= enemy.meleeAttackState.startTime + enemy.GetMeleeAttackData().attackDuration + enemy.GetMeleeAttackData().attackCooldown)
            {
                stateController.ChangeState(enemy.meleeAttackState);
            }
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

    protected override bool StopChase()
    {
        return (performCloseRangeAction && isDetectingGround) || (Time.time >= enemy.meleeSpecialAttackState.startTime + enemy.GetSpecialAttackData().attackCooldown && isDetectingGround);
    }
}
