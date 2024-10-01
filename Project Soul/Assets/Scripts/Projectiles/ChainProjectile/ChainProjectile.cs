using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainProjectile : MonoBehaviour
{
    public GameObject chainSegmentPrefab; // Reference to the chain segment prefab
    public GameObject firstChainSegmentPrefab;
    public float segmentWidth = 0.5f; // Width of each segment
    public float flightSpeed = 40f; // Speed at which the first segment flies
    public float retractSpeed = 40f; // Speed at which the chain retracts
    public int maxSegments = 10; // Maximum number of segments in the chain
    public float timeBeforeRetraction = 1f; // Time before the chain starts retracting

    private Transform damagePoint;
    [SerializeField] private LayerMask whatIsPlayer;

    private List<GameObject> chainSegments = new List<GameObject>();
    private Vector3 lastSegmentPosition;
    private bool isRetracting = false;
    private Vector3 direction;
    private float angle;


    void Start()
    {
        StartCoroutine(FireChain());
    }

    private IEnumerator FireChain()
    {
        // Spawn the first segment and make it fly
        GameObject firstSegment = Instantiate(firstChainSegmentPrefab, transform.position,  Quaternion.Euler(0, 0, angle + 90));
        damagePoint = firstSegment.transform.GetChild(0);
        Rigidbody2D rb = firstSegment.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(direction.x, direction.y).normalized * flightSpeed;

        chainSegments.Add(firstSegment);
        lastSegmentPosition = firstSegment.transform.position;

        while (chainSegments.Count < maxSegments && !isRetracting)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePoint.position, 0.2f, whatIsPlayer);
            if (damageHit)
            {
                damageHit.GetComponent<HealthContoller>().SendMessage("Damage", new AttackDetails() { damageAmout = 15, position = rb.position });
                break;
            }
            // Check if enough distance has been covered to spawn a new segment
            if (Vector3.Distance(lastSegmentPosition, firstSegment.transform.position) >= segmentWidth)
            {
                // Calculate the position for the new segment
                Vector3 newPosition = lastSegmentPosition + new Vector3(direction.x, direction.y).normalized * segmentWidth;

                // Instantiate the new segment
                GameObject newSegment = Instantiate(chainSegmentPrefab, newPosition, Quaternion.Euler(0, 0, angle + 90));
                chainSegments.Add(newSegment);

                // Update the last segment's position
                lastSegmentPosition = newPosition;
            }

            yield return null; // Wait until the next frame
        }

        // Stop the first segment after the chain is fully extended
        rb.velocity = Vector2.zero;

        // Wait for a short time before retracting
        yield return new WaitForSeconds(timeBeforeRetraction);

        // Move the first segment to the end of the list
        chainSegments.Add(chainSegments[0]);
        chainSegments.RemoveAt(0);

        // Start the retraction phase
        StartCoroutine(RetractChain());
    }

    private IEnumerator RetractChain()
    {
        isRetracting = true;

        while (chainSegments.Count > 0)
        {
            // Get the last segment (which was the first segment that was spawned)
            GameObject lastSegment = chainSegments[chainSegments.Count - 1];

            // Move the last segment back to the origin (or towards the previous segment's position)
            while (chainSegments.Count > 1 && Vector3.Distance(lastSegment.transform.position, chainSegments[chainSegments.Count - 2].transform.position) > 0.1f)
            {
                lastSegment.transform.position = Vector3.MoveTowards(lastSegment.transform.position, chainSegments[chainSegments.Count - 2].transform.position, retractSpeed * Time.deltaTime);
                yield return null; // Wait until the next frame
            }

            // Destroy the last segment when it reaches the previous one
            Destroy(lastSegment);
            chainSegments.RemoveAt(chainSegments.Count - 1);
        }
    }

    public void FireProjectile(Vector3 playerPosition)
    {

        direction = playerPosition - transform.position;
        angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
    }

}

