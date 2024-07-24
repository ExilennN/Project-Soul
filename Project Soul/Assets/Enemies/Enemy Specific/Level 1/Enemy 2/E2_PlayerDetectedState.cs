using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_PlayerDetectedState : PlayerDetectedState
{
    private Enemy2 enemy;
    public E2_PlayerDetectedState(Entity entity, StateController stateController, string animBoolName, D_PlayerDetectedState stateData, Enemy2 enemy) : base(entity, stateController, animBoolName, stateData)
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
        if (isPlayerInBaseAggroArea)
        {
            stateController.ChangeState(enemy.chaseState);
        }
        else if (isPlayerInMinAggroRange)
        {
            if (Time.time >= startTime + 2f) { stateController.ChangeState(enemy.rangedAttackState); }
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
