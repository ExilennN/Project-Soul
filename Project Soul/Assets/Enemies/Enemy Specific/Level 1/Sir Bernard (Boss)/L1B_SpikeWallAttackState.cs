using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1B_SpikeWallAttackState : AttackState
{
    private L1B_Boss enemy;
    private D_L1BSpikeWallAttackState stateData;

    private Transform attackPosition2;
    protected GameObject attackObject;
    protected SpikeWallAttack attackScript;

    private Transform currentAttackPosition;

    private Color initialColor;

    public L1B_SpikeWallAttackState(Entity entity, StateController stateController, string animBoolName, Transform attackPosition1, Transform attackPosition2, D_L1BSpikeWallAttackState stateData, L1B_Boss enemy) : base(entity, stateController, animBoolName, attackPosition1)
    {
        this.enemy = enemy;
        this.stateData = stateData;
        this.attackPosition2 = attackPosition2;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        initialColor = entity.aliveGO.GetComponent<SpriteRenderer>().color;
        entity.aliveGO.GetComponent<SpriteRenderer>().color = Color.red;
    }

    public override void Exit()
    {
        base.Exit();

        entity.aliveGO.GetComponent<SpriteRenderer>().color = initialColor;
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

        int direction = Random.Range(0, 2) == 0 ? -1 : 1;
        if (direction == 1) { currentAttackPosition = attackPosition; }
        else { currentAttackPosition = attackPosition2; }

        attackObject = GameObject.Instantiate(stateData.attackPrefab, currentAttackPosition.position, attackPosition.rotation);
        attackScript = attackObject.GetComponent<SpikeWallAttack>();
        attackScript.StartAttack(direction);

    }

    public override void FinishAtack()
    {
        base.FinishAtack();
    }
}
