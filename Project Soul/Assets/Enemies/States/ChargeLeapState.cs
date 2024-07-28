using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChargeLeapState : State
{
    D_ChargeLeapStateData stateData;

    protected bool performMidRangeAction;
    protected bool isChargeOver;
    private bool isCharging;
    private float leapStart;
    public ChargeLeapState(Entity entity, StateController stateController, string animBoolName, D_ChargeLeapStateData stateData) : base(entity, stateController, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        performMidRangeAction = entity.CheckPlayerInMidRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(stateData.chargeSpeed);
        isChargeOver = false;
        isCharging = false; 
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (isCharging)
        {
            if (Time.time >= leapStart + stateData.leapTime) { isChargeOver = true; isCharging = false; }
        }

        if (entity.CheckDistanceFromHorizontalPointToPlayer(stateData.leapDistance) && !isCharging)
        {
            isCharging = true; leapStart = Time.time;
            entity.SetVelocity(0f);
            entity.rb.AddForce(new Vector2(stateData.leapForce*entity.facingDirection, 2f), ForceMode2D.Impulse);
        }
    }


}
