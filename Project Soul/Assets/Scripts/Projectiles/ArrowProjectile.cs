using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    AttackDetails attackDetails;
    private float speed;

    private Rigidbody2D rb;
    private float startTime;
    private float livingTime = 10f;

    [SerializeField] private float damageRadius;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform damagePoint;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startTime = Time.time;
        rb.gravityScale = 0f;
        rb.velocity = transform.right * speed;
    }

    private void FixedUpdate()
    {
        if (Time.time >= startTime+livingTime) { Destroy(gameObject); }

        Collider2D damageHit = Physics2D.OverlapCircle(damagePoint.position, damageRadius, whatIsPlayer);
        Collider2D groundHit = Physics2D.OverlapCircle(damagePoint.position, damageRadius, whatIsGround);

        if (damageHit)
        {
            //TODO player revies damage

            Debug.Log("player hitted by prj for " + attackDetails.damageAmout);
            Destroy(gameObject);
        }

        if (groundHit) { Destroy(gameObject); }
    }

    public void FireProjectile(float speed, int damage)
    {
        this.speed = speed;
        attackDetails.damageAmout = damage;
    }
}
