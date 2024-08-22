using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2E1_ShieldBlockState : ShieldBlockState
{
    private L2E1_Enemy enemy;
    public L2E1_ShieldBlockState(Entity entity, StateController stateController, string animBoolName, D_ShieldBlockState stateData, L2E1_Enemy enemy) : base(entity, stateController, animBoolName, stateData)
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
            if (Random.Range(0,2) == 0 && Time.time >= enemy.meleeAttackState.endTime + enemy.GetMeleeAttackData().attackCooldown) 
            { 
                stateController.ChangeState(enemy.meleeAttackState); 
            }
        }
        else if (isShieldUpTimeOver && !performCloseRangeAction)
        {
            stateController.ChangeState(enemy.playerDetectedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
