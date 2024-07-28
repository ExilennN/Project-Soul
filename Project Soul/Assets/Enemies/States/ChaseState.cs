using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChaseState : State
{
    D_ChaseState stateData;

    protected bool isPlayerInMinAggroRange;
    protected bool isPlayerInBaseAggroArea;
    protected bool performCloseRangeAction;
    protected bool performMidRangeAction;
    protected bool isDetectingGround;

    protected PathAgent agent;
    PathNode currentNode;

    public ChaseState(Entity entity, StateController stateController, string animBoolName, D_ChaseState stateData) : base(entity, stateController, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        isPlayerInBaseAggroArea = entity.CheckPlayerInBaseAggroAreaRange();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        performMidRangeAction = entity.CheckPlayerInMidRangeAction();
        isDetectingGround = entity.CheckGround();
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        entity.seeker.GetGrid().GetXY(entity.GetPlayerPosition(), out int xT, out int yT);
        yT--;
        entity.seeker.GetGrid().GetXY(entity.GetEntityPositionOnGrid().position, out int xO, out int yO);
        List<PathNode> localPath = entity.seeker.FindPath(new PathNode(xO, yO), new PathNode(xT, yT));
        if (localPath != null) { agent = new PathAgent(localPath); }
        FollowPath();
    }

    protected void FollowPath()
    {
        DrawDebugPath();

        //Check if path is finised
        if (agent.isPathFinished) { entity.SetVelocity(0f); return; }


        //Set current node
        if (currentNode == null || currentNode != agent.currentNode) { currentNode = agent.GetNode(); }

        entity.seeker.GetGrid().GetXY(entity.aliveGO.transform.position, out int xEnemy, out int yEnemy);

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
            entity.SetVelocity(stateData.chaseSpeed);
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
