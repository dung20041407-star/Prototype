using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private TouchController touchController;
    [SerializeField] private StateManager stateManager;
    [SerializeField] private EquipmentManager equipmentManager;
    public EquipmentManager EquipmentManager => equipmentManager;
    public DamageZone damageZone;

    private IdleState idleState;
    private WalkState walkState;
    private RunState runState;
    private DashState dashState;
    private AttackState attackState;
    private AttackState2 attackState2;
    private AttackState3 attackState3;
    private AttackState4 attackState4;
    private AttackState5 attackState5;

    public DashState DashState => dashState;

    public void ToIdle() => stateManager.ChangeState(idleState);
    public void ToWalk() => stateManager.ChangeState(walkState);
    public void ToRun() => stateManager.ChangeState(runState);
    public void ToDash() => stateManager.ChangeState(dashState);
    public void ToAttack() => stateManager.ChangeState(attackState);
    public void ToAttack2() => stateManager.ChangeState(attackState2);
    public void ToAttack3() => stateManager.ChangeState(attackState3);
    public void ToAttack4() => stateManager.ChangeState(attackState4);
    public void ToAttack5() => stateManager.ChangeState(attackState5);

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
    public bool IsAttacking1 => stateManager.CurrentState == attackState;
    public bool IsAttacking2 => stateManager.CurrentState == attackState2;

    public bool IsAttacking3 => stateManager.CurrentState == attackState3;
    public bool IsAttacking4 => stateManager.CurrentState == attackState4;
    public bool IsAttacking5 => stateManager.CurrentState == attackState5;

    public bool IsAttacking => IsAttacking1 || IsAttacking5 || IsAttacking2 || IsAttacking3 || IsAttacking4;

    public bool IsDashing => stateManager.CurrentState == dashState;
    public bool IsFacingRight => isFacingRight;

    public bool IsAttackAnimationFinished { get; private set; }
    public bool IsAttackPhaseFinished { get; private set; }

    public Vector2 DashDirection => dashState.Direction;

    public bool DashRequested { get; private set; }

    public float CurrentDamageScale { get; private set; } = 1f;

    private void Awake()
    {
        if (stateManager == null)
            stateManager = GetComponent<StateManager>();

        idleState = new IdleState(this);
        walkState = new WalkState(this);
        runState = new RunState(this);
        dashState = new DashState(this);
        attackState = new AttackState(this);
        attackState5 = new AttackState5(this);
        attackState4 = new AttackState4(this);
        attackState3 = new AttackState3(this);
        attackState2 = new AttackState2(this);

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

    public void SetAttackAnimationFinished(bool value)
    {
        IsAttackAnimationFinished = value;
    }
    public void SetAttackPhaseFinished(bool value)
    {
        IsAttackPhaseFinished = value;
    }
    public void SetDamageScale(float scale)
    {
        CurrentDamageScale = scale;
    }
    public void ResetDamageScale()
    {
        CurrentDamageScale = 1f;
    }
}