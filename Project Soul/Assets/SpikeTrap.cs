using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HealthContoller>().SendMessage("Damage", new AttackDetails() { damageAmout = 10, position = transform.position });
        }
    }
}
