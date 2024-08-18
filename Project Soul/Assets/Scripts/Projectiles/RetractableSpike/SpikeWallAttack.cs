using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWallAttack : MonoBehaviour
{
    [SerializeField] private GameObject spikePrefab;
    public int spikeCount = 10;
    public float timeToNextSpike = 0.8f;
    public float distanceBetweenSpikes = 1f;

    private int direction = 1;

    private void Start()
    {
        StartCoroutine(SpawnSpikes());
    }

    private IEnumerator SpawnSpikes()
    {
        for (int i = 0; i <= spikeCount; i++)
        {
            // Calculate the position for the new spike
            Vector3 spawnPosition = transform.position + (new Vector3(i * distanceBetweenSpikes, 0, 0)) * direction;

            // Instantiate the spike at the calculated position
            Instantiate(spikePrefab, spawnPosition, Quaternion.identity);

            // Wait for the specified time before spawning the next spike
            yield return new WaitForSeconds(timeToNextSpike);
        }
    }

    public void StartAttack(int direction)
    {
        this.direction = direction;
    }
}
