using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newPlayerDetectedStateData", menuName = "Data/State Data/Player Detected State")]
public class D_PlayerDetectedState : ScriptableObject
{
    public float chaseSpeed = 6f;
    public float jumpForce = 10f;
    public int jumpHeightInCells = 4;
}
