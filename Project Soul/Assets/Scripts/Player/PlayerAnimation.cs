using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private string currentState;
    private bool isDead = false;
    private bool isAttacking = false;
    private bool isHealing = false;

    private const string PLAYER_IDLE = "Player_idle";
    private const string PLAYER_RUN = "Player_run";
    private const string PLAYER_JUMP = "Jump_up";
    private const string PLAYER_FALL = "Jump_down";
    private const string PLAYER_DOUBLE_JUMP = "Double_jump";
    private const string PLAYER_DEFAULT_SLIDE = "Default_slide";
    private const string PLAYER_DEATH = "Player_death";
    private const string PLAYER_HEAL = "Player_heal";

    // ATTACK ANIMATIONS
    private const string PLAYER_SWORD_ATTACK = "Player_sword_attack";
    private const string PLAYER_SWORD_STAB = "Player_sword_stab";

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newState)
    {
        if (isDead && newState != PLAYER_DEATH) return;
        if (isHealing && newState != PLAYER_HEAL) return;
        if (currentState == newState) return;

        animator.Play(newState);
        currentState = newState;
    }

    public void SetIdleAnimation()
    {
        if (!isDead && !isAttacking && !isHealing)
        {
            ChangeAnimationState(PLAYER_IDLE);
        }
    }

    public void SetRunAnimation()
    {
        if (!isDead && !isAttacking && !isHealing)
        {
            ChangeAnimationState(PLAYER_RUN);
        }
    }

    public void SetJumpAnimation()
    {
        if (!isDead && !isAttacking && !isHealing)
        {
            ChangeAnimationState(PLAYER_JUMP);
        }
    }

    public void SetFallAnimation()
    {
        if (!isDead && !isAttacking && !isHealing)
        {
            ChangeAnimationState(PLAYER_FALL);
        }
    }

    public void SetDoubleJumpAnimation()
    {
        if (!isDead && !isAttacking && !isHealing)
        {
            ChangeAnimationState(PLAYER_DOUBLE_JUMP);
        }
    }

    public void SetSlideAnimation()
    {
        if (!isDead && !isAttacking && !isHealing)
        {
            ChangeAnimationState(PLAYER_DEFAULT_SLIDE);
        }
    }

    public void SetDeathAnimation()
    {
        if (isDead) return;

        isDead = true;
        ChangeAnimationState(PLAYER_DEATH);
    }

    public void Respawn()
    {
        isDead = false;
        SetIdleAnimation();
    }

    public void SetHealAnimation()
    {
        if (!isDead && !isHealing)
        {
            isHealing = true;
            ChangeAnimationState(PLAYER_HEAL);
        }
    }

    public void OnHealAnimationEvent()
    {
        GameObject healthObj = GameObject.FindWithTag("Health");
        if (healthObj != null)
        {
            PlayerHealthBar healthBar = healthObj.GetComponentInChildren<PlayerHealthBar>();
            if (healthBar != null)
            {
                healthBar.RegenerateHealth();
            }
            else
            {
                Debug.LogError("PlayerHealthBar component not found on Health object!");
            }
        }
        else
        {
            Debug.LogError("Health object not found!");
        }
    }

    public void EndHealing()
    {
        isHealing = false;
    }

    // ATTACKS

    public void SetSwordAttackAnimation()
    {
        if (!isDead && !isAttacking && !isHealing)
        {
            isAttacking = true;
            ChangeAnimationState(PLAYER_SWORD_ATTACK);
        }
    }

    public void SetSwordStabAnimation()
    {
        if (!isDead && !isAttacking && !isHealing)
        {
            isAttacking = true;
            ChangeAnimationState(PLAYER_SWORD_STAB);
        }
    }

    public void ReturnToIdle()
    {
        isAttacking = false;
        SetIdleAnimation();
    }
}
