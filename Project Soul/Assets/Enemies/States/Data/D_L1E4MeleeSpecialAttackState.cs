using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newL1E4SpecialAttackStateData", menuName = "Data/State Data/L1E4 State Data/Melee Special Attack State")]
public class D_L1E4MeleeSpecialAttackState : ScriptableObject
{
    public GameObject projectile;
    public float attackCooldown = 6f;
    public float attackRadius = 0.8f;
    public int attackImpactDamage = 1;

    public LayerMask whatIsPlayer;
}
