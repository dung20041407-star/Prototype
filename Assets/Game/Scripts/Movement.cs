using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private TouchController touchController;
    [SerializeField] private StateManager stateManager;
    public DamageZone damageZone;

    private IdleState idleState;
    private WalkState walkState;
    private RunState runState;
    private DashState dashState;
    private AttackState attackState;

    public DashState DashState => dashState;

    public void ToIdle() => stateManager.ChangeState(idleState);
    public void ToWalk() => stateManager.ChangeState(walkState);
    public void ToRun() => stateManager.ChangeState(runState);
    public void ToDash() => stateManager.ChangeState(dashState);
    public void ToAttack() => stateManager.ChangeState(attackState);

    [SerializeField] private bool isMoving;
    public bool IsMoving => isMoving;

    [SerializeField] private bool isMovingFast;
    public bool IsMovingFast => isMovingFast;

    [SerializeField] private float number = 30f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float vectorMagnitudeClamp = 200f;

    [Header("Facing")]
    [SerializeField] private bool isFacingRight = true;

    [Header("Attack")]
    public bool IsAttacking { get; private set; }

    public bool IsDashing => stateManager.CurrentState == dashState;
    public bool IsFacingRight => isFacingRight;

    public bool IsAttackAnimationFinished { get; private set; }

    public Vector2 DashDirection => dashState.Direction;

    public bool DashRequested { get; private set; }

    private void Awake()
    {
        if (stateManager == null)
            stateManager = GetComponent<StateManager>();

        idleState = new IdleState(this);
        walkState = new WalkState(this);
        runState = new RunState(this);
        dashState = new DashState(this);
        attackState = new AttackState(this);

        stateManager.ChangeState(idleState);
    }

    private void Update()
    {
        UpdateMovementState();
        UpdateFacingDirection();
        FlipPlayer();
    }

    public void RequestDash()
    {
        DashRequested = true;
    }

    public void ClearDashRequest()
    {
        DashRequested = false;
    }

    private void UpdateFacingDirection()
    {
        if (IsAttacking)
            return;

        Vector2 movementVector;

        if (IsDashing)
            movementVector = DashDirection;
        else
            movementVector = CalculateMovementVector();

        if (movementVector.x > 0f)
            isFacingRight = true;
        else if (movementVector.x < 0f)
            isFacingRight = false;
    }

    public Vector2 CalculateMovementVector()
    {
        if (!touchController.IsMoving)
            return Vector2.zero;

        return (
            touchController.CurrentTouchPosition -
            touchController.MovementStartPosition
        ).normalized;
    }

    public float CalculateMovementSpeed()
    {
        if (!touchController.IsMoving)
            return 0f;

        float vectorMagnitude = (
            touchController.CurrentTouchPosition -
            touchController.MovementStartPosition
        ).magnitude;

        if (vectorMagnitude > vectorMagnitudeClamp)
            vectorMagnitude = vectorMagnitudeClamp;

        return vectorMagnitude;
    }

    private float CalculateNormalizedMovementSpeed()
    {
        float magnitude = CalculateMovementSpeed();

        return Mathf.InverseLerp(
            0f,
            vectorMagnitudeClamp,
            magnitude
        ) * 100f;
    }

    public void Move(float movementSpeed)
    {
        if (IsAttacking)
            return;

        MoveInDirection(CalculateMovementVector(), movementSpeed);
    }

    public void MoveInDirection(Vector2 direction, float movementSpeed)
    {
        if (IsAttacking)
            return;

        transform.Translate(
            direction * movementSpeed * Time.deltaTime * (speed / 60f)
        );
    }

    private void FlipPlayer()
    {
        float direction = isFacingRight ? 1f : -1f;

        transform.localScale = new Vector3(direction, 1f, 1f);
    }

    private void UpdateMovementState()
    {
        isMoving = CalculateMovementSpeed() > 0f;
        isMovingFast = CalculateNormalizedMovementSpeed() >= number;
    }
    public void SetAttacking(bool value)
    {
        IsAttacking = value;
    }
    public void SetAttackAnimationFinished(bool value)
    {
        IsAttackAnimationFinished = value;
    }
}