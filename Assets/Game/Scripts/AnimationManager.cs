using UnityEngine;
using UnityEngine.InputSystem;

public enum CharacterState
{
    Idle,
    Walking,
    Running,
    Dashing,
    Attacking
}

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Movement movement;

    private CharacterState currentState;

    private static readonly int IsIdle = Animator.StringToHash("isIdle");

    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    private static readonly int IsRunning = Animator.StringToHash("isRunning");

    private static readonly int IsDashing = Animator.StringToHash("isDashing");

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void Start()
    {
        SetState(CharacterState.Idle);
    }

    public void SetState(CharacterState newState)
    {
        currentState = newState;

        animator.SetBool(IsIdle, currentState == CharacterState.Idle);

        animator.SetBool(IsWalking, currentState == CharacterState.Walking);

        animator.SetBool(IsRunning, currentState == CharacterState.Running);

        animator.SetBool(IsDashing, currentState == CharacterState.Dashing);
    }
    private void Update()
    {
        if (!movement.IsMoving)
        {
            SetState(CharacterState.Idle);
        }
        if (movement.IsMoving)
        {
            if ((movement.IsMovingFast))
            {
                SetState(CharacterState.Running);
            }
            else
            {
                SetState(CharacterState.Walking);
            }
        }
        if (movement.IsDashing)
        {
            SetState(CharacterState.Dashing);
        }
    }
}
