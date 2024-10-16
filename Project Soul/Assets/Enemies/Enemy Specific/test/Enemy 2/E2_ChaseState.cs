using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_ChaseState : ChaseState
{
    private Enemy2 enemy;

    public E2_ChaseState(Entity entity, StateController stateController, string animBoolName, D_ChaseState stateData, Enemy2 enemy) : base(entity, stateController, animBoolName, stateData)
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

        if (!isPlayerInBaseAggroArea && !isPlayerInMinAggroRange)
        {
            enemy.SetTrakingBack(true);
            stateController.ChangeState(enemy.moveState);
        }

        if (isPlayerInMinAggroRange)
        {
           stateController.ChangeState(enemy.rangedAttackState); 
        }


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
