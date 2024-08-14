using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private string currentState;

    const string PLAYER_IDLE = "Player_idle";
    const string PLAYER_RUN = "Player_run";

    const string PLAYER_JUMP = "Jump_up";
    const string PLAYER_FALL = "Jump_down";

    const string PLAYER_DEFAULT_SLIDE = "Default_slide";

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }

    public void SetIdleAnimation()
    {
        ChangeAnimationState(PLAYER_IDLE);
    }

    public void SetRunAnimation()
    {
        ChangeAnimationState(PLAYER_RUN);
    }

    public void SetJumpAnimation()
    {
        ChangeAnimationState(PLAYER_JUMP);
    }

    public void SetFallAnimation()
    {
        ChangeAnimationState(PLAYER_FALL);
    }

    public void SetSlideAnimation()
    {
        ChangeAnimationState(PLAYER_DEFAULT_SLIDE);
    }
}
