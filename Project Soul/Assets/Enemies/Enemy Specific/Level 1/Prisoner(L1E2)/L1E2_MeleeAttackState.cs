using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1E2_MeleeAttackState : MeleeAttackState
{
    private L1E2_Enemy enemy;
    public L1E2_MeleeAttackState(Entity entity, StateController stateController, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData, L1E2_Enemy enemy) : base(entity, stateController, animBoolName, attackPosition, stateData)
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

    public override void FinishAtack()
    {
        base.FinishAtack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            stateController.ChangeState(enemy.playerDetectedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
