using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyChaseState : State
{
    D_FlyChaseState stateData;


    public FlyChaseState(Entity entity, StateController stateController, string animBoolName, D_FlyChaseState stateData) : base(entity, stateController, animBoolName)
    {
        this.stateData = stateData;
    }

    protected bool isPlayerInMinAggroRange;
    protected bool isPlayerInBaseAggroArea;
    protected bool performCloseRangeAction;
    protected bool performMidRangeAction;
    protected bool performLongRangeAction;

    protected PathAgent agent;
    PathNode currentNode;

    private int xEnemy = 0, yEnemy = 0;

    private int frameInterval = 10;
    private int frameCount = 10;

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        isPlayerInBaseAggroArea = entity.CheckPlayerInBaseAggroAreaRange();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        performMidRangeAction = entity.CheckPlayerInMidRangeAction();
        performLongRangeAction = entity.CheckPlayerInLongRangeAction();

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

        CustommUpdate();

        FollowPath();
    }

    protected virtual void CustommUpdate()
    {
        frameCount++;
        if (frameCount >= frameInterval)
        {
            entity.seeker.GetGrid().GetXY(entity.GetPlayerPosition(), out int xT, out int yT);
            entity.seeker.GetGrid().GetXYFlyGrid(entity.GetEntityPositionOnGrid().position, out int xO, out int yO);
            if (xO != 999999) { xEnemy = xO; }
            if (yO != 999999) { yEnemy = yO; }
            List<PathNode> localPath = entity.seeker.FindPath(new PathNode(xEnemy, yEnemy), new PathNode(xT, yT));
            if (localPath != null) { agent = new PathAgent(localPath); }
            frameCount = 0;
        }

    }

    protected void FollowPath()
    {
        DrawDebugPath();

        //Check if path is finised
        if (agent.isPathFinished) { entity.SetVelocity(0f); return; }

        //Set current node
        if (currentNode == null || currentNode != agent.currentNode) 
        {
            currentNode = agent.GetNode(); 
        }
        entity.seeker.GetGrid().GetXYFlyGrid(entity.GetEntityPositionOnGrid().position, out int xEnemyC, out int yEnemyC);
        if (xEnemyC != 999999) { xEnemy = xEnemyC; }
        if (yEnemyC != 999999) { yEnemy = yEnemyC; }

        //if we reached next node change current node to next
        if (xEnemy == agent.GetNextNode().x && yEnemy == agent.GetNextNode().y) 
        {
            currentNode = agent.GetNode(); 
        }

        //change facing direction if character if needed
        int facingDirection = 0;
        if (xEnemy < agent.GetNextNode().x) { facingDirection = 1; }
        else if (xEnemy > agent.GetNextNode().x) { facingDirection = -1; }

        if (facingDirection != entity.facingDirection && facingDirection != 0) { entity.Flip(); }

        //Handle behavior depending on on which node entity currently is
        if (currentNode.isWalkable)
        {
            Vector2 distance = new Vector2(agent.GetNextNode().x, agent.GetNextNode().y) - new Vector2(currentNode.x, currentNode.y);
            Vector2 direction = new Vector2(distance.x, distance.y).normalized;

            entity.SetVelocity(stateData.chaseSpeed, direction);

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
