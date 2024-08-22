using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public GameObject DoorIn;
    private bool roomLocked = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !roomLocked)
        {
            DoorIn.SetActive(true);
            roomLocked = true;
        }
    }
}
