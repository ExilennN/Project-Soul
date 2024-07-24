using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerDetectedStateData", menuName = "Data/State Data/Chase State")]
public class D_ChaseState : ScriptableObject
{
    public float chaseSpeed = 6f;
    public int jumpHeightInCells = 4;
}
