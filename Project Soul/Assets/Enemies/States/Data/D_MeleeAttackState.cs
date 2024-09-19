using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMoveStateData", menuName = "Data/State Data/Melee Attack State")]
public class D_MeleeAttackState : ScriptableObject
{
    public float attackRadius = 0.5f;
    public int attackDamage = 1;
    public float attackCooldown = 1f;
    public float attackDuration = 1.5f;

    public Vector3 attackZone = new Vector3(1, 1, 0);
    public LayerMask whatIsPlayer;
}
