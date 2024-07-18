using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntiteData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity :ScriptableObject
{
    public float wallCheckDistance = 0.2f;
    public float groundCheckDistance = 0.4f;

    public float minAggroDistance = 7f;
    public float maxAggroDistance = 10f;

    public float closeRangeActionDistance = 2f;

    public LayerMask whatIsGround;
    public LayerMask whatIsWall;
    public LayerMask whatIsPlayer;
}
