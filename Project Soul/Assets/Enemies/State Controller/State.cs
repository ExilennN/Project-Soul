using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State 
{
    protected StateController stateController;
    protected Entity entity;

    public float startTime { get; private set; } = 0;

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
