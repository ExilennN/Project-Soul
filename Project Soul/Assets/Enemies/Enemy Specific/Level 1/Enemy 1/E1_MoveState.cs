using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class E1_MoveState : MoveState
{
    private Enemy1 enemy;
    public E1_MoveState(Entity entity, StateController stateController, string animBoolName, D_MoveState stateData, Enemy1 enemy) : base(entity, stateController, animBoolName, stateData)
    {
        this.enemy = enemy;
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

        if (Vector2.Distance(enemy.aliveGO.transform.position, enemy.GetCurrectPatrollPoint().transform.position) <= 0.5f)
        {
            enemy.idleState.SetFlipAfterIdle(true);
            stateController.ChangeState(enemy.idleState);
        }


        else if (isDetectingWall || !isDetectingGround)
        {
            enemy.idleState.SetFlipAfterIdle(true);
            stateController.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
