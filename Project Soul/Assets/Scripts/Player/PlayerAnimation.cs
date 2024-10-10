using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private string currentState;
    private bool isDead = false;
    private bool isAttacking = false;
    private bool isHealing = false;
    public bool isAtCheckpoint = false;
    private Checkpoint currentCheckpoint;

    private const string PLAYER_IDLE = "Player_idle";
    private const string PLAYER_RUN = "Player_run";
    private const string PLAYER_JUMP = "Jump_up";
    private const string PLAYER_FALL = "Jump_down";
    private const string PLAYER_DOUBLE_JUMP = "Double_jump";
    private const string PLAYER_DEFAULT_SLIDE = "Default_slide";
    private const string PLAYER_DEATH = "Player_death";
    private const string PLAYER_HEAL = "Player_heal";
    private const string PLAYER_CHECKPOINT = "Player_checkpoint";

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
        if (isAtCheckpoint && newState != PLAYER_CHECKPOINT) return;
        if (currentState == newState) return;

        animator.Play(newState);
        currentState = newState;
    }

    public void SetIdleAnimation()
    {
        if (!isDead && !isAttacking && !isHealing && !isAtCheckpoint)
        {
            ChangeAnimationState(PLAYER_IDLE);
        }
    }

    public void SetRunAnimation()
    {
        if (!isDead && !isAttacking && !isHealing && !isAtCheckpoint)
        {
            ChangeAnimationState(PLAYER_RUN);
        }
    }

    public void SetJumpAnimation()
    {
        if (!isDead && !isAttacking && !isHealing && !isAtCheckpoint)
        {
            ChangeAnimationState(PLAYER_JUMP);
        }
    }

    public void SetFallAnimation()
    {
        if (!isDead && !isAttacking && !isHealing && !isAtCheckpoint)
        {
            ChangeAnimationState(PLAYER_FALL);
        }
    }

    public void SetDoubleJumpAnimation()
    {
        if (!isDead && !isAttacking && !isHealing && !isAtCheckpoint)
        {
            ChangeAnimationState(PLAYER_DOUBLE_JUMP);
        }
    }

    public void SetSlideAnimation()
    {
        if (!isDead && !isAttacking && !isHealing && !isAtCheckpoint)
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
        isAtCheckpoint = false;
        SetIdleAnimation();
    }

    public void SetHealAnimation()
    {
        if (!isDead && !isHealing && !isAtCheckpoint)
        {
            isHealing = true;
            ChangeAnimationState(PLAYER_HEAL);
        }
    }

    public void OnHealAnimationEvent()
    {
        GameObject hud = GameObject.Find("HUD");
        if (hud != null)
        {
            Transform healthTransform = hud.transform.Find("Health");
            if (healthTransform != null)
            {
                PlayerHealthBar healthBar = healthTransform.GetComponent<PlayerHealthBar>();
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
                Debug.LogError("Health object not found in HUD!");
            }
        }
        else
        {
            Debug.LogError("HUD not found in the scene!");
        }
    }

    public void SetCheckpointAnimation(Checkpoint checkpoint)
    {
        if (!isDead && !isAttacking && !isHealing && !isAtCheckpoint)
        {
            isAtCheckpoint = true;
            currentCheckpoint = checkpoint; // Сохраняем ссылку на чекпоинт
            ChangeAnimationState(PLAYER_CHECKPOINT);
        }
    }

    // Этот метод вызывается через Animation Event для активации чекпоинта
    public void OnCheckpointAnimationEvent()
    {
        if (currentCheckpoint != null)
        {
            currentCheckpoint.ActivateCheckpoint();
        }
        else
        {
            Debug.LogError("Checkpoint component not found!");
        }

        EndCheckpointAnimation();
    }

    public void EndCheckpointAnimation()
    {
        isAtCheckpoint = false;
        SetIdleAnimation();
    }

    public void EndHealing()
    {
        isHealing = false;
    }

    // ATTACKS

    public void SetSwordAttackAnimation()
    {
        if (!isDead && !isAttacking && !isHealing && !isAtCheckpoint)
        {
            isAttacking = true;
            ChangeAnimationState(PLAYER_SWORD_ATTACK);
        }
    }

    public void SetSwordStabAnimation()
    {
        if (!isDead && !isAttacking && !isHealing && !isAtCheckpoint)
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
