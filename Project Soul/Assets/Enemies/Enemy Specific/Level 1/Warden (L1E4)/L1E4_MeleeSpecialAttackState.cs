using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1E4_MeleeSpecialAttackState : AttackState
{
    private D_L1E4MeleeSpecialAttackState stateData;
    private L1E4_Enemy enemy;

    private Transform projectileSpawnPosition1;
    private Transform projectileSpawnPosition2;
    private AttackDetails attackDetails;

    protected GameObject projectile;
    protected ImpactWave projectileScript;
    public L1E4_MeleeSpecialAttackState(Entity entity, StateController stateController, string animBoolName, Transform attackPosition, Transform projectileSpawnPosition1, Transform projectileSpawnPosition2, D_L1E4MeleeSpecialAttackState stateData, L1E4_Enemy enemy) : base(entity, stateController, animBoolName, attackPosition)
    {
        this.stateData = stateData;
        this.enemy = enemy;
        this.projectileSpawnPosition1 = projectileSpawnPosition1;
        this.projectileSpawnPosition2 = projectileSpawnPosition2;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        entity.ResetVelocity();

        attackDetails.damageAmout = stateData.attackImpactDamage;
        attackDetails.position = entity.aliveGO.transform.position;

        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAtack()
    {
        base.FinishAtack();

        if (isAnimationFinished)
        {
            stateController.ChangeState(enemy.playerDetectedState);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        foreach (Collider2D collider in detectedObjects)
        {
            Debug.Log(collider.gameObject.name + " damaged for " + attackDetails.damageAmout);
        }


        projectile = GameObject.Instantiate(stateData.projectile, projectileSpawnPosition1.position, attackPosition.rotation);
        projectileScript = projectile.GetComponent<ImpactWave>();
        projectileScript.FireProjectile(1);

        projectile = GameObject.Instantiate(stateData.projectile, projectileSpawnPosition2.position, attackPosition.rotation);
        projectileScript = projectile.GetComponent<ImpactWave>();
        projectileScript.FireProjectile(-1);
    }
}
