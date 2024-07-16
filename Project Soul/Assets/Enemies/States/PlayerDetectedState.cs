using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.Image;

public class PlayerDetectedState : State
{
    protected D_PlayerDetectedState stateData;

    protected bool isPlayerInMinAggroRange;
    protected bool isPlayerInBaseAggroArea;
    protected PathAgent agent;
    protected float jumpTime;
    protected bool isJumping;
    PathNode currentNode;
    public PlayerDetectedState(Entity entity, StateController stateController, string animBoolName, D_PlayerDetectedState stateData) : base(entity, stateController, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(0f);
        isJumping = false;
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        isPlayerInBaseAggroArea = entity.CheckPlayerInBaseAggroAreaRange();
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
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        isPlayerInBaseAggroArea = entity.CheckPlayerInBaseAggroAreaRange();

        entity.seeker.GetGrid().GetXY(entity.GetPlayerPosition(), out int xT, out int yT);
        entity.seeker.GetGrid().GetXY(entity.aliveGO.transform.position, out int xO, out int yO);
        List<PathNode> localPath = entity.seeker.FindPath(new PathNode(xO, yO), new PathNode(xT, yT));
        if (localPath != null) { agent = new PathAgent(localPath); }
        FollowPath();
    }

    protected virtual void FollowPath()
    {
        DrawDebugPath();
        
        //Check if path is finised
        if (agent.isPathFinished) { Debug.Log("Path is finished"); entity.SetVelocity(0f); return; }

        //Set current node
        if (currentNode == null || currentNode != agent.currentNode) { currentNode = agent.GetNode();  }

        entity.seeker.GetGrid().GetXY(entity.aliveGO.transform.position, out int xEnemy, out int yEnemy);

        //if we reached next node change current node to next
        if (xEnemy == agent.GetNextNode().x && yEnemy == agent.GetNextNode().y) {currentNode = agent.GetNode(); }
        
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
        if (agent.GetNextNode().isAir)
        {
            if (currentNode.y < agent.GetNode(2).y && entity.CheckGround())
            {
                entity.rb.velocity = new Vector2(entity.rb.velocity.x, stateData.jumpForce);
            }
        }

    }
    public void DrawDebugPath()
    {
        for (int i = 0; i < agent.path.Count - 1; i++)
        {
            Debug.DrawLine(entity.seeker.GetGrid().GetCenterOfNode(entity.seeker.GetGrid().GetWorldPosition(agent.path[i].x, agent.path[i].y)), entity.seeker.GetGrid().GetCenterOfNode(entity.seeker.GetGrid().GetWorldPosition(agent.path[i + 1].x, agent.path[i + 1].y)), Color.red);
        }
    }
}
