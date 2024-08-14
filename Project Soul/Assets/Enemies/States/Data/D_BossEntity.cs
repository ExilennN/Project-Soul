using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntiteData", menuName = "Data/Entity Data/Boss Base Data")]
public class D_BossEntity : ScriptableObject
{
    public float maxHealth = 50;
    public float jumpForce = 10f;

    public float wallCheckDistance = 0.2f;
    public float groundCheckDistance = 0.4f;

    public float closeRangeActionDistance = 3f;
    public float midRangeActionDistance = 6f;
    public float longRangeActionDistance = 10f;

    public float LOSdistance = 10f;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}
