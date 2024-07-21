using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_MoveState : MoveState
{
    private Enemy2 enemy;

    public E2_MoveState(Entity entity, StateController stateController, string animBoolName, D_MoveState stateData, Enemy2 enemy) : base(entity, stateController, animBoolName, stateData)
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

            if (Vector2.Distance(enemy.aliveGO.transform.position, enemy.GetCurrectPatrollPoint().transform.position) <= 0.5f)
            {
                stateController.ChangeState(enemy.idleState);
            }

            else if (isDetectingWall || !isDetectingGround)
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
