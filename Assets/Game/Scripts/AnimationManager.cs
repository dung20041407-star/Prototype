using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Movement movement;

    private static readonly int IsIdle = Animator.StringToHash("isIdle");
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int IsDashing = Animator.StringToHash("isDashing");
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateMovementAnimation();
        UpdateAttackAnimation();
    }

    private void UpdateMovementAnimation()
    {
        if (movement.IsAttacking)
        {
            SetMovementAnimation();
            return;
        }

        if (movement.IsDashing)
        {
            SetMovementAnimation(dashing: true);
            return;
        }

        if (!movement.IsMoving)
        {
            SetMovementAnimation(idle: true);
            return;
        }

        if (movement.IsMovingFast)
        {
            SetMovementAnimation(running: true);
            return;
        }

        SetMovementAnimation(walking: true);
    }

    private void UpdateAttackAnimation()
    {
        animator.SetBool(IsAttacking, movement.IsAttacking);
    }

    private void SetMovementAnimation(
        bool idle = false,
        bool walking = false,
        bool running = false,
        bool dashing = false)
    {
        animator.SetBool(IsIdle, idle);
        animator.SetBool(IsWalking, walking);
        animator.SetBool(IsRunning, running);
        animator.SetBool(IsDashing, dashing);
    }
    public void OnAttackAnimationFinished()
    {
        movement.SetAttackAnimationFinished(true);
    }
}