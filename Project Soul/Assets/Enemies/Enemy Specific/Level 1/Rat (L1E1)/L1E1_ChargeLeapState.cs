using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1E1_ChargeLeapState : ChargeLeapState
{
    private L1E1_Enemy enemy;

    public L1E1_ChargeLeapState(Entity entity, StateController stateController, string animBoolName, D_ChargeLeapStateData stateData, L1E1_Enemy enemy) : base(entity, stateController, animBoolName, stateData)
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

        if (isChargeOver)
        {
            stateController.ChangeState(enemy.playerDetectedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
