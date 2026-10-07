using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float vectorMagnitudeClamp = 200.0f;
    private Vector2 startPos;
    private Vector2 continuousPos;
    private bool isPressed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleStartPos();
        HandleContinuousPos();
        Move();
    }
    void HandleStartPos()
    {
        if (Touchscreen.current == null) return;
        if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            isPressed = true;
            startPos = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        if (Touchscreen.current.primaryTouch.press.wasReleasedThisFrame)
        {
            isPressed = false;
            startPos = Vector2.zero;
        }
    }
    void HandleContinuousPos()
    {
        if (isPressed)
        {
            continuousPos = Touchscreen.current.primaryTouch.position.ReadValue();
        }
    }
    Vector2 CalculateMovementVector()
    {
        if (!isPressed)
        return Vector2.zero;
        
        return (continuousPos - startPos).normalized;
    }
    float CalculateMovementSpeed()
    {
        float vectorMagnitude = (continuousPos - startPos).magnitude;
        if (vectorMagnitude > vectorMagnitudeClamp)
            return vectorMagnitudeClamp;
        return vectorMagnitude;
    }
    void Move()
    {
        transform.Translate(CalculateMovementVector() * CalculateMovementSpeed() * Time.deltaTime * (speed/60f));
    }
}
