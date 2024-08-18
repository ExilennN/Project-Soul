using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class SpikesFromFloor : MonoBehaviour
{
    public float timeUntilFullUnleash = 1f;
    public float timeUntilRetraction = 1.5f;
    public float speed = 10f;

    private float startTime;
    private float firstDistance;
    private float lastDistance;

    private bool isUnleashing;
    private bool isRetracting;

    private float destinationYPosition;
    private float initialYPosition;

    private BoxCollider2D damageCollider;

    [SerializeField] private Transform[] platformPositions;
    private List<Transform> activePlatforms;
    private void Start()
    {
        activePlatforms = new List<Transform>();
        initialYPosition = transform.position.y;
        isRetracting = false;
        firstDistance = GetComponent<SpriteRenderer>().bounds.size.y / 4;
        lastDistance = GetComponent<SpriteRenderer>().bounds.size.y - firstDistance;

        damageCollider = transform.Find("ColideZone").GetComponent<BoxCollider2D>();

        destinationYPosition = initialYPosition + firstDistance + lastDistance;
    }
    private void FixedUpdate()
    {
        if (!isRetracting && !isUnleashing)
        {
            activePlatforms.ForEach(platform => platform.gameObject.active = false);
        }

        if (isRetracting && Time.time >= startTime + timeUntilRetraction) 
        {
            if (Mathf.Abs(transform.position.y) >= Mathf.Abs(initialYPosition)) 
            {
                damageCollider.enabled = false;
                isRetracting = false;
            }
            else { transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, initialYPosition), speed * Time.deltaTime); }
        }

        else if (isUnleashing && Time.time >= startTime + timeUntilFullUnleash)
        {
            if (Mathf.Abs(transform.position.y) <= Mathf.Abs(destinationYPosition))
            {
                damageCollider.enabled = true;
                isRetracting = true;
                isUnleashing = false;
                startTime = Time.time;
            }
            else 
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, destinationYPosition), speed * Time.deltaTime);
            }
        }
    }

    public void StartAttack() 
    {
        activePlatforms = new List<Transform>();
        while(activePlatforms.Count != 2)
        {
            int randomIndex = Random.Range(0, platformPositions.Length);
            if (!activePlatforms.Contains(platformPositions[randomIndex]))
            {
                activePlatforms.Add(platformPositions[randomIndex]);
            }
        }
        transform.position = new Vector3(transform.position.x, transform.position.y + firstDistance);
        startTime = Time.time;
        isUnleashing = true;
        activePlatforms.ForEach(platform => platform.gameObject.active = true);
    }


}
