using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordAttack : MonoBehaviour
{
    private PlayerAnimation playerAnimation;

    void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();

        if (playerAnimation == null)
        {
            Debug.LogError("PlayerAnimation component not found on player!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            playerAnimation.SetSwordAttackAnimation();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            playerAnimation.SetSwordStabAnimation();
        }
    }
}
