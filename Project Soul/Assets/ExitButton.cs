using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public GameObject DoorIn;
    public GameObject TriggerDoor1;
    public GameObject TriggerDoor2;
    private bool buttonPressed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !buttonPressed)
        {
            DoorIn.SetActive(false);
            TriggerDoor1.SetActive(false);
            TriggerDoor2.SetActive(false);
            buttonPressed = true;
        }
    }
}
