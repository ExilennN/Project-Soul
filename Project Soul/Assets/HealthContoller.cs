using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthContoller : MonoBehaviour
{
    public int health = 100;
    
    public void Damage(AttackDetails attackDetails)
    {
        health -= attackDetails.damageAmout;
    }
}
