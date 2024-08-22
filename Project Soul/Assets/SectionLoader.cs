using UnityEngine;

public class SectionActivator : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    private int playerCount = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCount++;
            if (playerCount == 1)
            {
                ActivateSection();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCount--;
            if (playerCount == 0)
            {
                DeactivateSection();
            }
        }
    }

    void ActivateSection()
    {
        foreach (var obj in objectsToActivate)
        {
            obj.SetActive(true);
        }
    }

    void DeactivateSection()
    {
        foreach (var obj in objectsToActivate)
        {
            obj.SetActive(false);
        }
    }
}
