using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1B_DirectionalFollowState : DirectionalFollowState
{
    private L1B_Boss enemy;
    public L1B_DirectionalFollowState(Entity entity, StateController stateController, string animBoolName, D_DirectionalFollowState stateData, L1B_Boss enemy) : base(entity, stateController, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInCloseRange = enemy.CheckPlayerInCloseRangeAction();
        isPlayerInMidRange = enemy.CheckPlayerInMidRangeAction();
        isPlayerInLongRange = enemy.CheckPlayerInLongRangeAction();
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

        int facingDirection = 0;
        if (entity.aliveGO.transform.position.x < entity.GetPlayerPosition().x) { facingDirection = 1; }
        else if (entity.aliveGO.transform.position.x > entity.GetPlayerPosition().x) { facingDirection = -1; }

        if (Time.time >= enemy.GetWhirlWindAttackData().attackCooldown + enemy.whirlwindAttackState.startTime)
        {
            stateController.ChangeState(enemy.whirlwindAttackState);
        }

        else if (isPlayerInCloseRange)
        {
            if (facingDirection != entity.facingDirection && facingDirection != 0) { entity.Flip(); }
            stateController.ChangeState(enemy.meleeAttackState);
        }

        else if (isPlayerInLongRange && Time.time >= enemy.chainPullAttackState.startTime + enemy.GetChainPullAttackData().attackCooldown)
        {
            if (facingDirection != entity.facingDirection && facingDirection != 0) { entity.Flip(); }
            stateController.ChangeState(enemy.chainPullAttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
