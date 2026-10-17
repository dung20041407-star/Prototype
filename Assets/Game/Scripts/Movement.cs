using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private TouchController touchController;

    [Header("Movement")]
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float vectorMagnitudeClamp = 200.0f;

    [Header("Dash")]
    [SerializeField] private float dashMultiplier = 4.0f;
    [SerializeField] private float dashDuration = 0.15f;
    [SerializeField] private float dashCooldown = 0.8f;

    public bool IsDashing { get; private set; }
    private float dashTimer;
    private float dashCooldownTimer;

    private void Update()
    {
        UpdateDash();
        HandleDashInput();
        Move();
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
        if (!touchController.IsMoving)
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
        Debug.Log("Start Dashing");
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
            Debug.Log("Stop dashing.");
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

        float vectorMagnitude = (touchController.CurrentTouchPosition - touchController.MovementStartPosition).magnitude;

        if (vectorMagnitude > vectorMagnitudeClamp)
            vectorMagnitude = vectorMagnitudeClamp;
        return vectorMagnitude;
    }

    private void Move()
    {
        float currentDashMultiplier = IsDashing ? dashMultiplier : 1f;

        transform.Translate(CalculateMovementVector() * CalculateMovementSpeed() * currentDashMultiplier * Time.deltaTime * (speed / 60f));
    }
}