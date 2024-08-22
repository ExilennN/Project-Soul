using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "newShieldBlockStateData", menuName = "Data/State Data/Shield Block State")]
public class D_ShieldBlockState : ScriptableObject
{
    public float minShieldUpTime = 2f;
    public float maxShieldUpTime = 4f;

    public float actionCooldown = 2f;
}
