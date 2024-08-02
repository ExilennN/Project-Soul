using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMoveState : State
{
    protected D_FlyMoveState stateData;

    protected bool isDetectingWall;
    protected bool isPlayerMinAggroRange;

    protected bool startIdle;

    protected PathAgent agent;
    PathNode currentNode;

    private int xEnemy = 0, yEnemy = 0;
    public FlyMoveState(Entity entity, StateController stateController, string animBoolName, D_FlyMoveState stateData) : base(entity, stateController, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

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
            entity.seeker.GetGrid().GetXY(entity.GetBasePosition().position, out int xT, out int yT);
            entity.seeker.GetGrid().GetXY(entity.GetEntityPositionOnGrid().position, out int xO, out int yO);
            if (xO != 999999) { xEnemy = xO; }
            if (yO != 999999) { yEnemy = yO; }
            List<PathNode> localPath = entity.seeker.FindPath(new PathNode(xEnemy, yEnemy), new PathNode(xT, yT));
            if (localPath != null) { agent = new PathAgent(localPath); }
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
            entity.seeker.GetGrid().GetXY(entity.GetBasePosition().position, out int xT, out int yT);
            entity.seeker.GetGrid().GetXYFlyGrid(entity.GetEntityPositionOnGrid().position, out int xO, out int yO);
            if (xO != 999999) { xEnemy = xO; }
            if (yO != 999999) { yEnemy = yO; }
            List<PathNode> localPath = entity.seeker.FindPath(new PathNode(xEnemy, yEnemy), new PathNode(xT, yT));
            if (localPath != null) { agent = new PathAgent(localPath); }
            TrackToBase();
        }
    }


    protected void TrackToBase()
    {
        DrawDebugPath();
        //Check if path is finised
        if (agent.isPathFinished || agent.currentNode == agent.path[agent.path.Count - 1]) 
        {
            entity.SetTrakingBack(false);
            entity.ResetVelocity();
            startIdle = true;
            return; 
        }

        //Set current node
        if (currentNode == null || currentNode != agent.currentNode) { currentNode = agent.GetNode(); }

        entity.seeker.GetGrid().GetXYFlyGrid(entity.GetEntityPositionOnGrid().position, out int xEnemyC, out int yEnemyC);
        if (xEnemyC != 999999) { xEnemy = xEnemyC; }
        if (yEnemyC != 999999) { yEnemy = yEnemyC; }

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
            Vector2 distance = new Vector2(agent.GetNextNode().x, agent.GetNextNode().y) - new Vector2(currentNode.x, currentNode.y);
            Vector2 direction = new Vector2(distance.x, distance.y).normalized;

            entity.SetVelocity(stateData.movementSpeed, direction);

        }
    }

    private void DrawDebugPath()
    {
        for (int i = 0; i < agent.path.Count - 1; i++)
        {
            Debug.DrawLine(entity.seeker.GetGrid().GetCenterOfNode(entity.seeker.GetGrid().GetWorldPosition(agent.path[i].x, agent.path[i].y)), entity.seeker.GetGrid().GetCenterOfNode(entity.seeker.GetGrid().GetWorldPosition(agent.path[i + 1].x, agent.path[i + 1].y)), Color.blue);
        }
    }
}
