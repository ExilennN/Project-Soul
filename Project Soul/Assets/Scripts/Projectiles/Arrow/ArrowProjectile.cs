using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : Projectile
{
    private Vector3 direction;
    private float angle;
    public override void Start()
    {
        base.Start();

        rb.gravityScale = 0f;

        rb.velocity = new Vector2(direction.x, direction.y).normalized * baseData.projectileSpeed * Time.deltaTime * 10;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);

    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        Collider2D damageHit = Physics2D.OverlapCircle(damagePoint.position, baseData.damagePointRadius, whatIsPlayer);
        Collider2D groundHit = Physics2D.OverlapCircle(damagePoint.position, baseData.damagePointRadius, whatIsGround);

        if (damageHit)
        {
            //TODO player revies damage

            Debug.Log("player hitted by prj for " + attackDetails.damageAmout);
            Destroy(gameObject);
        }

        if (groundHit) { Destroy(gameObject); }
    }

    public override void FireProjectile(Vector3 playerPos)
    {
        base.FireProjectile(playerPos);
        direction = playerPos - transform.position;
        angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;

    }
}
