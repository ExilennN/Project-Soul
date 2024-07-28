using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColideDamage : MonoBehaviour
{
    public int colideDamge = 1;
    private AttackDetails attackDetails;

    private void Start()
    {
        attackDetails = new AttackDetails() { damageAmout = colideDamge, position = transform.position };
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //TODO damage player
            Debug.Log("Player collide and recieve " + attackDetails.damageAmout);
        }
    }
}
