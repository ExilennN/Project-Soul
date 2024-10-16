using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class MoveState : State
{
    protected D_MoveState stateData;

    protected bool isDetectingWall;
    protected bool isDetectingGround;
    protected bool isPlayerMinAggroRange;

    protected PathAgent agent;
    PathNode currentNode;

    protected bool startIdle;
    public MoveState(Entity entity, StateController stateController, string animBoolName, D_MoveState stateData) : base(entity, stateController, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isDetectingGround = entity.CheckGround();
        isDetectingWall = entity.CheckWall();
        isPlayerMinAggroRange = entity.CheckPlayerInMinAggroRange();
    }

    public override void Enter()
    {
        base.Enter();
        startIdle = false;
        entity.SetVelocity(stateData.movementSpeed);

        if (entity.isTrackingBack)
        {
            entity.seeker.GetGrid().GetXY(entity.GetEntityPositionOnGrid().position, out int xO, out int yO);
            entity.seeker.GetGrid().GetXY(entity.GetBasePosition().position, out int xT, out int yT);

            List<PathNode> localPath = entity.seeker.FindPath(new PathNode(xO, yO), new PathNode(xT, yT));
            agent = new PathAgent(localPath);
            TrackToBase();
        }
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
        if (entity.isTrackingBack)
        {
            entity.seeker.GetGrid().GetXY(entity.GetEntityPositionOnGrid().position, out int xO, out int yO);
            entity.seeker.GetGrid().GetXY(entity.GetBasePosition().position, out int xT, out int yT);

            List<PathNode> localPath = entity.seeker.FindPath(new PathNode(xO, yO), new PathNode(xT, yT));
            agent = new PathAgent(localPath);
            TrackToBase();
        }
    }
    protected void TrackToBase()
    {
        DrawDebugPath();

        //Check if path is finised
        if (agent.isPathFinished || agent.GetNextNode() == agent.path[agent.path.Count - 1]) 
        { 
            entity.SetTrakingBack(false); 
            startIdle = true; 
            return; 
        }
        //Set current node
        if (currentNode == null || currentNode != agent.currentNode) { currentNode = agent.GetNode(); }

        entity.seeker.GetGrid().GetXY(entity.GetEntityPositionOnGrid().position, out int xEnemy, out int yEnemy);

        //if we reached next node change current node to next
        if (xEnemy == agent.GetNextNode().x && yEnemy == agent.GetNextNode().y) { currentNode = agent.GetNode(); }

        //change facing direction if character if needed
        int facingDirection = 0;
        if (xEnemy < agent.GetNextNode().x) { facingDirection = 1; }
        else if (xEnemy > agent.GetNextNode().x) { facingDirection = -1; }

        if (facingDirection != entity.facingDirection && facingDirection != 0) { entity.Flip(); }

        //Handle behavior depending on on which node entity currently is
        if (currentNode.isWalkable)
        {
            entity.SetVelocity(stateData.movementSpeed);
        }
        if (agent.GetNode(2).isAir)
        {
            if (currentNode.y < agent.GetNode(2).y && entity.CheckGround())
            {
                entity.rb.velocity = new Vector2(entity.rb.velocity.x, entity.entityData.jumpForce);
            }
        }
        if (currentNode.isAir && !agent.GetNextNode().isWalkable)
        {
            entity.rb.velocity = new Vector2(0f, entity.rb.velocity.y);
        }
    }

    private void DrawDebugPath()
    {
        for (int i = 0; i < agent.path.Count - 1; i++)
        {
            Debug.DrawLine(entity.seeker.GetGrid().GetCenterOfNode(entity.seeker.GetGrid().GetWorldPosition(agent.path[i].x, agent.path[i].y)), entity.seeker.GetGrid().GetCenterOfNode(entity.seeker.GetGrid().GetWorldPosition(agent.path[i + 1].x, agent.path[i + 1].y)), Color.red);
        }
    }
}
