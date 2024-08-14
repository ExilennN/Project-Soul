using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "newBaseProjectileData", menuName = "Data/Projectile Data/Base Data")]
public class D_BaseProjectile : ScriptableObject
{
    public float livingTime = 10f;
    public float projectileSpeed = 4f;
    public float damagePointRadius = 1f;
    public int damage = 1;
}
