using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerDetectedStateData", menuName = "Data/State Data/Ranged Attack State")]
public class D_RangedAttackState :  ScriptableObject
{
    public GameObject projectile;
    public int projectileDamage = 1;
    public float projectileSpeed = 12f;
    
}
