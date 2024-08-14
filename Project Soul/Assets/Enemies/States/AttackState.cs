using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected Transform attackPosition;
    protected bool isAnimationFinished;

    public AttackState(Entity entity, StateController stateController, string animBoolName, Transform attackPosition) : base(entity, stateController, animBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
    public override void Enter()
    {
        base.Enter();

        entity.atsc.attackState = this;
        isAnimationFinished = false;
        entity.ResetVelocity();
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
    }

    public virtual void TriggerAttack() 
    {
    
    }
    public virtual void FinishAtack() 
    {
        isAnimationFinished = true;
    }
    
}
