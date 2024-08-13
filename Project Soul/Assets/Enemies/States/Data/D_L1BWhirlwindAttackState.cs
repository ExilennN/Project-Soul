using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newL1BWhirlwindAttackStateData", menuName = "Data/State Data/L1B Data/Whirlwind Attack State")]
public class D_L1BWhirlwindAttackState : ScriptableObject
{
    public int damage = 1;
    public float movementSpeed = 40f;
    public float attackCooldown = 10f;
    public float knockbackForce = 15f;
    public float knockbackDuration = 0.2f;
}
