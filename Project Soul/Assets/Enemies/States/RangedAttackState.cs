using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : AttackState
{
    protected D_RangedAttackState stateData;
    protected GameObject projectile;
    protected Projectile projectileScript;

    public RangedAttackState(Entity entity, StateController stateController, string animBoolName, Transform attackPosition, D_RangedAttackState stateData) : base(entity, stateController, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        entity.ResetVelocity();
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        projectile = GameObject.Instantiate(stateData.projectile, attackPosition.position, attackPosition.rotation);
    }
}
