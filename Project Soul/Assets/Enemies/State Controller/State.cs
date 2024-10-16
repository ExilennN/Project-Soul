using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class State 
{
    protected StateController stateController;
    protected Entity entity;

    public float startTime { get; private set; } = 0;
    public float endTime { get; private set; } = 0;

    protected string animBoolName;

    public State(Entity entity, StateController stateController, string animBoolName)
    {
        this.stateController = stateController;
        this.entity = entity;
        this.animBoolName = animBoolName;
    }

    public virtual void DoChecks() { }

    public virtual void Enter() 
    {
        startTime = Time.time;
        entity.anim.SetBool(animBoolName, true);
        DoChecks();
    }

    public virtual void Exit() 
    {
        endTime = Time.time;
        entity.anim.SetBool(animBoolName, false);
    }

    //Update
    public virtual void LogicUpdate() { }

    //FixedUpdate
    public virtual void PhysicsUpdate() 
    {
        DoChecks();
    }
}
