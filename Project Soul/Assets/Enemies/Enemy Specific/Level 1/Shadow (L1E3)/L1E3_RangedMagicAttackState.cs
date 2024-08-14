using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1E3_RangedMagicAttackState : RangedAttackState
{
    private L1E3_Enemy enemy;
    public L1E3_RangedMagicAttackState(Entity entity, StateController stateController, string animBoolName, Transform attackPosition, D_RangedAttackState stateData, L1E3_Enemy enemy) : base(entity, stateController, animBoolName, attackPosition, stateData)
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

        projectileScript = projectile.GetComponent<EnergyballProjectile>();
        projectileScript.FireProjectile(entity.GetPlayerLosPosition());
    }
}
