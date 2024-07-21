using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_PlayerDetectedState : PlayerDetectedState
{
    private Enemy1 enemy;
    public E1_PlayerDetectedState(Entity entity, StateController stateController, string animBoolName, D_PlayerDetectedState stateData, Enemy1 enemy) : base(entity, stateController, animBoolName, stateData)
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

        if (performCloseRangeAction)
        {
            stateController.ChangeState(enemy.meleeAttackState);
        }

        else if (!isPlayerInBaseAggroArea)
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
