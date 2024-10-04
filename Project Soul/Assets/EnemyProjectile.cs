using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    public void ActivateProjectile()
    {
        lifetime = 0;
        gameObject.SetActive(true);
    }
    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);
        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
           gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Trigger2" && collision.gameObject.name != "Trigger1" && collision.gameObject.name != "Trigger3" && collision.gameObject.name != "Trigger4" && collision.gameObject.name != "Trigger5" && collision.gameObject.name != "TriggerTrap1")
        {
            if (collision.CompareTag("Player"))
            {
               collision.GetComponent<HealthContoller>().SendMessage("Damage", new AttackDetails() { damageAmout = 15, position = transform.position });
            }
            gameObject.SetActive(false);
        }
    }

}
