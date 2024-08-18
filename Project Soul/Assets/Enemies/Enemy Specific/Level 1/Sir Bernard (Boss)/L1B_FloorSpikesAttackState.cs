using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1B_FloorSpikesAttackState : AttackState
{
    private L1B_Boss enemy;
    private D_L1BFloorSpikesAttackState stateData;
    private SpikesFromFloor attackScript;

    private Color initialColor;
    public L1B_FloorSpikesAttackState(Entity entity, StateController stateController, string animBoolName, Transform attackPosition, D_L1BFloorSpikesAttackState stateData, L1B_Boss enemy) : base(entity, stateController, animBoolName, attackPosition)
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
        entity.ResetVelocity();
        initialColor = entity.aliveGO.GetComponent<SpriteRenderer>().color;

        entity.aliveGO.GetComponent<SpriteRenderer>().color = Color.cyan;

        attackScript = attackPosition.Find("SpikesFromFloor").gameObject.GetComponent<SpikesFromFloor>();
        attackScript.StartAttack();
    }

    public override void Exit()
    {
        base.Exit();
        entity.aliveGO.GetComponent<SpriteRenderer>().color = initialColor;
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
    }
}
