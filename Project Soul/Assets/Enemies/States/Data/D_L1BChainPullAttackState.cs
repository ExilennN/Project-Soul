using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "newL1BChainPullStateData", menuName = "Data/State Data/L1B Data/Chain Pull Attack State")]
public class D_L1BChainPullAttackState : ScriptableObject
{
    public int damage = 1;
    public float attackCooldown = 16f;

    public GameObject chainPullAttack;
}
