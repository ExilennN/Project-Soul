using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthContoller : MonoBehaviour
{
    [SerializeField] private PlayerHealthBar healthBar;
    public void Damage(AttackDetails attackDetails)
    {
        healthBar.TakeDamage(attackDetails);
    }
}
