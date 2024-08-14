using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ImpactWave : MonoBehaviour
{
    private AttackDetails attackDetails;
    
    [Header("Speed Settings")]
    public float initialSpeed = 2f; 
    public float maxSpeed = 20f;      
    public float acceleration = 5f;
    private float currentSpeed;

    [Header("Damage Amount")] 
    public int damage = 1;

    [Header("Size scale Settings")]
    public Vector4 currentScale;
    public Vector3 initialScale = new Vector3(0.8f, 0.6f, 1f);
    public Vector3 maxScale = new Vector3(1.8f, 1.6f, 1f);    
    public float scalingSpeed = 0.2f;

    private float initialYPosition;
    private float initialHeight;

    public float spawnTime { get; private set; }
    private float livingTime = 10f;

    public Rigidbody2D rb { get; private set; }

    [Header("Other")]  
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected Transform wallCheck;

    private void Start()
    {
        attackDetails.damageAmout = damage;
        spawnTime = Time.time;

        currentSpeed = initialSpeed;
        currentScale = initialScale; 
        transform.localScale = currentScale;

        initialHeight = GetComponent<SpriteRenderer>().bounds.size.y; 
        initialYPosition = transform.position.y - initialHeight / 2;
    }

    private void Update()
    {
       if (IsDetectingWall())
       {
            Destroy(gameObject);
       }
       if (Time.time >= spawnTime + livingTime) { Destroy(gameObject); }
    }

    private void FixedUpdate()
    {

        if (currentSpeed < maxSpeed)
        {
            currentSpeed += acceleration * Time.deltaTime;
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
        }
        if (currentSpeed >= maxSpeed) { currentSpeed = maxSpeed; }

        if (currentScale.x < maxScale.x || currentScale.y < maxScale.y)
        {
            currentScale.x += scalingSpeed * Time.deltaTime;
            currentScale.y += scalingSpeed * Time.deltaTime;

            currentScale.x = Mathf.Min(currentScale.x, maxScale.x);
            currentScale.y = Mathf.Min(currentScale.y, maxScale.y);

            if (currentScale.x >= maxScale.x) { currentScale.x = maxScale.x; }
            if (currentScale.y >= maxScale.y) { currentScale.y = maxScale.y; }

            float newHeight = GetComponent<SpriteRenderer>().bounds.size.y;
            transform.position = new Vector3(transform.position.x, initialYPosition + newHeight / 2, transform.position.z);

            transform.localScale = currentScale;
        }
        transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Recieves damage
            Debug.Log("Player damaged by wave for " + attackDetails.damageAmout);
        }
    }

    private bool IsDetectingWall() 
    {
        return Physics2D.Raycast(wallCheck.position, transform.right, 0.1f, whatIsGround);
    }
    public virtual void Flip()
    {
        transform.Rotate(0, 180, 0);
    }

    public void FireProjectile(int direction)
    {
        if (direction == -1) { Flip(); }
    }
}
