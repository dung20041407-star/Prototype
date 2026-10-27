using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private TouchController touchController;

    [Header("Movement")]
    [SerializeField] private bool isMoving;
    public bool IsMoving => isMoving;
    [SerializeField] private bool isMovingFast;
    public bool IsMovingFast => isMovingFast;
    [SerializeField] private float number = 30.0f;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float vectorMagnitudeClamp = 200.0f;

    [Header("Facing")]
    [SerializeField] private bool isFacingRight = true;

    [Header("Dash")]
    private float dashSpeed;
    [SerializeField] private float dashTurnSpeed = 3f;

    private Vector2 dashDirection;

    [SerializeField] private float dashMultiplier = 4.0f;
    [SerializeField] private float dashDuration = 0.15f;
    [SerializeField] private float dashCooldown = 0.8f;

    public bool IsDashing { get; private set; }
    public bool IsFacingRight => isFacingRight;

    private float dashTimer;
    private float dashCooldownTimer;

    private void Update()
    {
        UpdateDash();
        HandleDashInput();
        UpdateMovementState();
        Move();
        UpdateFacingDirection();
        FlipPlayer();
    }

    private void HandleDashInput()
    {
        if (Keyboard.current == null)
            return;

        if (!Keyboard.current.spaceKey.wasPressedThisFrame)
            return;
        TryDash();
    }

    public void TryDash()
    {
        if (!isMovingFast)
            return;

        if (IsDashing)
            return;

        if (dashCooldownTimer > 0f)
            return;

        StartDash();
    }

    private void StartDash()
    {
        IsDashing = true;

        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown;

        dashSpeed = CalculateMovementSpeed();
        dashDirection = CalculateMovementVector();
    }

    private void UpdateDash()
    {
        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;

            if (dashCooldownTimer < 0f)
                dashCooldownTimer = 0f;
        }

        if (!IsDashing)
            return;

        dashTimer -= Time.deltaTime;

        if (dashTimer <= 0f)
        {
            IsDashing = false;
            dashTimer = 0f;
        }
    }

    private void UpdateFacingDirection()
    {
        Vector2 movementVector;

        if (IsDashing)
        {
            movementVector = dashDirection;
        }else
        {
            movementVector = CalculateMovementVector();
        }

        if (movementVector.x > 0f)
        {
            isFacingRight = true;
        }else if (movementVector.x < 0f)
        {
            isFacingRight = false;
        }
    }

    private Vector2 CalculateMovementVector()
    {
        if (!touchController.IsMoving)
            return Vector2.zero;
        return (touchController.CurrentTouchPosition - touchController.MovementStartPosition).normalized;
    }

    private float CalculateMovementSpeed()
    {
        if (!touchController.IsMoving)
            return 0f;

        float vectorMagnitude =(touchController.CurrentTouchPosition - touchController.MovementStartPosition).magnitude;

        if (vectorMagnitude > vectorMagnitudeClamp)
            vectorMagnitude = vectorMagnitudeClamp;

        return vectorMagnitude;
    }
    private float CalculateNormalizedMovementSpeed()
    {
        float magnitude = CalculateMovementSpeed();

        return Mathf.InverseLerp(0f, vectorMagnitudeClamp, magnitude) * 100f;
    }

    private void Move()
    {
        Vector2 inputDirection = CalculateMovementVector();
        Vector2 movementDirection;
        float currentSpeed;

        if (IsDashing)
        {
            if (inputDirection != Vector2.zero)
            {
                dashDirection = Vector2.Lerp(dashDirection, inputDirection, dashTurnSpeed * Time.deltaTime).normalized;
            }
            movementDirection = dashDirection;
            currentSpeed = dashSpeed * dashMultiplier;
        }
        else
        {
            movementDirection = inputDirection;
            currentSpeed = CalculateMovementSpeed();
        }

        transform.Translate(movementDirection * currentSpeed * Time.deltaTime * (speed / 60f)
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
}