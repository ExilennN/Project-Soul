using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerDetectedStateData", menuName = "Data/State Data/Charge Leap State")]
public class D_ChargeLeapStateData : ScriptableObject
{
    public float chargeSpeed = 8f;
    public float leapDistance = 3f;
    public float leapForce = 20f;

    public float leapTime = 0.5f;
    public float chargeCooldown = 4f;
}
