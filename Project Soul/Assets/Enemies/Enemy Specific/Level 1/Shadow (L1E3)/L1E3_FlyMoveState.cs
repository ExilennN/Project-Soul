using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1E3_FlyMoveState : FlyMoveState
{
    private L1E3_Enemy enemy;
    public L1E3_FlyMoveState(Entity entity, StateController stateController, string animBoolName, D_FlyMoveState stateData, L1E3_Enemy enemy) : base(entity, stateController, animBoolName, stateData)
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

        if (isPlayerMinAggroRange)
        {
            stateController.ChangeState(enemy.playerDetectedState);
        }
        if (!enemy.isTrackingBack)
        {
            if (startIdle)
            {
                stateController.ChangeState(enemy.idleState);
            }

            if (Vector2.Distance(enemy.GetEntityPositionOnGrid().position, enemy.GetCurrectPatrollPoint().transform.position) <= 1f)
            {
                stateController.ChangeState(enemy.idleState);
            }

            else if (isDetectingWall)
            {
                enemy.idleState.SetFlipAfterIdle(true);
                stateController.ChangeState(enemy.idleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
