using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class L1B_WhirlwindAttackState : AttackState
{
    private L1B_Boss enemy;
    private D_L1BWhirlwindAttackState stateData;

    private bool isDetectingWall;
    private int attackCount = 0;

    private bool isKnockbackAactive;
    private float knockbackStartTime;
    public L1B_WhirlwindAttackState(Entity entity, StateController stateController, string animBoolName, Transform attackPosition, D_L1BWhirlwindAttackState stateData, L1B_Boss enemy) : base(entity, stateController, animBoolName, attackPosition)
    {
        this.enemy = enemy;
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isDetectingWall = entity.CheckWall();
    }

    public override void Enter()
    {
        base.Enter();

        isKnockbackAactive = false;

        entity.ResetVelocity();

        int facingDirection = 0;
        if (entity.aliveGO.transform.position.x < entity.GetPlayerPosition().x) { facingDirection = 1; }
        else if (entity.aliveGO.transform.position.x > entity.GetPlayerPosition().x) { facingDirection = -1; }
        if (facingDirection != entity.facingDirection && facingDirection != 0) { entity.Flip(); }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isKnockbackAactive) 
        { 
            if (Time.time >= knockbackStartTime + stateData.knockbackDuration)
            {
                entity.ResetVelocity();
                isKnockbackAactive = false;
            }
        }

        else if (isAnimationFinished)
        {
            if (attackCount >= 2)
            {
                attackCount = 0;
                stateController.ChangeState(enemy.followState);
            }
            else
            {
                stateController.ChangeState(enemy.whirlwindAttackState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (isDetectingWall)
        {
            entity.ResetVelocity();
            HandleKnockback();
        }
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        entity.SetVelocity(stateData.movementSpeed);
    }
    public override void FinishAtack()
    {
        base.FinishAtack();
    }

    private void HandleKnockback()
    {

        entity.ResetVelocity();
        entity.rb.AddForce(-entity.aliveGO.transform.right * stateData.knockbackForce, ForceMode2D.Impulse);
        attackCount++;

        knockbackStartTime = Time.time;
        isKnockbackAactive = true;

        entity.anim.SetBool(animBoolName, false);
    }

}
