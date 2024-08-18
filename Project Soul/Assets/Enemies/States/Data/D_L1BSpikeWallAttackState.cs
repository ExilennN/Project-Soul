using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newL1BSpikeWallStateData", menuName = "Data/State Data/L1B Data/Spike Wall Attack State")]
public class D_L1BSpikeWallAttackState : ScriptableObject
{
    public float attackCooldown = 15f;
    public GameObject attackPrefab;
}
