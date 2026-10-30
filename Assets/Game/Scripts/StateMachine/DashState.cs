using UnityEngine;

public class DashState : IState
{
    private readonly Movement movement;

    private float dashSpeed;
    private Vector2 dashDirection;

    private float dashTimer;
    private float cooldownEndTime;

    private readonly float dashTurnSpeed = 5f;
    private readonly float dashMultiplier = 1.5f;
    private readonly float dashDuration = 0.5f;
    private readonly float dashCooldown = 0.85f;

    public Vector2 Direction => dashDirection;

    public bool CanDash => Time.time >= cooldownEndTime;

    public DashState(Movement movement)
    {
        this.movement = movement;
    }

    public void Enter()
    {
        Debug.Log("Enter Dash State");

        dashTimer = dashDuration;
        cooldownEndTime = Time.time + dashCooldown;

        dashSpeed = movement.CalculateMovementSpeed();
        dashDirection = movement.CalculateMovementVector();
    }

    public void Execute()
    {
        dashTimer -= Time.deltaTime;

        UpdateDashDirection();

        movement.MoveInDirection(
            dashDirection,
            dashSpeed * dashMultiplier
        );

        if (dashTimer <= 0f)
        {
            if (!movement.IsMoving)
                movement.ToIdle();
            else if (movement.IsMovingFast)
                movement.ToRun();
            else
                movement.ToWalk();
        }
    }

    private void UpdateDashDirection()
    {
        Vector2 inputDirection = movement.CalculateMovementVector();

        if (inputDirection == Vector2.zero)
            return;

        dashDirection = Vector2.Lerp(
            dashDirection,
            inputDirection,
            dashTurnSpeed * Time.deltaTime
        ).normalized;
    }

    public void FixedExecute() { }

    public void Exit()
    {
        Debug.Log("Exit Dash State");
    }
}