using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public D_BaseProjectile baseData;

    public AttackDetails attackDetails;
    public float spawnTime { get; private set; }

    public Rigidbody2D rb { get; private set; }


    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected Transform damagePoint;



    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spawnTime = Time.time;

        attackDetails.damageAmout = baseData.damage;
        attackDetails.position = rb.position;
    }

    public virtual void FixedUpdate()
    {
        if (Time.time >= spawnTime + baseData.livingTime) { Destroy(gameObject); }
    }

    public virtual void FireProjectile(Vector3 playerPos)
    {

    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePoint.position, baseData.damagePointRadius);
    }

}
