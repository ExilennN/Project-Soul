using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1B_ChainPullAttackState : AttackState
{
    private L1B_Boss enemy;
    private D_L1BChainPullAttackState stateData;

    private AttackDetails attackDetails;
    private GameObject chainAttackObject;
    private ChainProjectile chainAttackScript;
    public L1B_ChainPullAttackState(Entity entity, StateController stateController, string animBoolName, Transform attackPosition, D_L1BChainPullAttackState stateData, L1B_Boss enemy) : base(entity, stateController, animBoolName, attackPosition)
    {
        this.enemy = enemy;
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        attackDetails.damageAmout = stateData.damage;
        attackDetails.position = attackPosition.position;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            stateController.ChangeState(enemy.followState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        chainAttackObject = GameObject.Instantiate(stateData.chainPullAttack, attackPosition.position, attackPosition.rotation);
        chainAttackScript = chainAttackObject.GetComponent<ChainProjectile>();
        chainAttackScript.FireProjectile(enemy.GetPlayerPosition());
    }
    public override void FinishAtack()
    {
        base.FinishAtack();
    }
}
