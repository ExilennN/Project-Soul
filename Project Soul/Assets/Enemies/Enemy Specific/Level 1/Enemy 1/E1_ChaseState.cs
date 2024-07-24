using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_ChaseState : ChaseState
{
    private Enemy1 enemy;

    public E1_ChaseState(Entity entity, StateController stateController, string animBoolName, D_ChaseState stateData, Enemy1 enemy) : base(entity, stateController, animBoolName, stateData)
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

        if (performCloseRangeAction)
        {
            stateController.ChangeState(enemy.meleeAttackState);
        }
        if (!isPlayerInBaseAggroArea & !isPlayerInMinAggroRange)
        {
            enemy.SetTrakingBack(true);
            stateController.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
