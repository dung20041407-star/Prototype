using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class TouchController : MonoBehaviour
{
    [SerializeField] private Movement movement;

    private int movementTouchId = -1;

    [SerializeField] private bool isMoving;
    public bool IsMoving => isMoving;

    private Vector2 movementStartPosition;
    private Vector2 currentMovementPosition;

    public Vector2 MovementStartPosition => movementStartPosition;
    public Vector2 CurrentTouchPosition => currentMovementPosition;

    private void Update()
    {
        ReadTouches();

        if (isMoving && !IsMovementTouchStillPressed())
        {
            EndMovementSession();
        }

        if (Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (isMoving)
            {
                TriggerDash();
            }
        }
    }

    private void ReadTouches()
    {
        if (Touchscreen.current == null)
            return;

        var touches = Touchscreen.current.touches;

        for (int i = 0; i < touches.Count; i++)
        {
            TouchControl touch = touches[i];

            if (touch.press.wasPressedThisFrame)
            {
                HandleTouchBegan(touch);
            }

            if (touch.press.isPressed)
            {
                HandleTouchHeld(touch);
            }

            if (touch.press.wasReleasedThisFrame)
            {
                HandleTouchReleased(touch);
            }
        }
    }

    private void HandleTouchBegan(TouchControl touch)
    {
        int touchId = touch.touchId.ReadValue();
        Vector2 position = touch.position.ReadValue();

        if (!isMoving)
        {
            if (IsTouchBlocked(position))
                return;

            movementTouchId = touchId;

            movementStartPosition = position;
            currentMovementPosition = position;

            isMoving = true;

            return;
        }

        if (touchId != movementTouchId)
        {
            TriggerDash();
        }
    }

    private void HandleTouchHeld(TouchControl touch)
    {
        int touchId = touch.touchId.ReadValue();

        if (touchId != movementTouchId)
            return;

        currentMovementPosition = touch.position.ReadValue();
    }

    private void HandleTouchReleased(TouchControl touch)
    {
        int touchId = touch.touchId.ReadValue();

        if (touchId != movementTouchId)
            return;

        EndMovementSession();
    }

    private void EndMovementSession()
    {
        movementTouchId = -1;

        isMoving = false;

        movementStartPosition = Vector2.zero;
        currentMovementPosition = Vector2.zero;
    }

    private void TriggerDash()
    {
        movement.TryDash();
    }

    private bool IsTouchBlocked(Vector2 position)
    {
        if (EventSystem.current == null)
            return false;

        PointerEventData pointerData =
            new PointerEventData(EventSystem.current);

        pointerData.position = position;

        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponentInParent<InputBlocker>() != null)
                return true;
        }

        return false;
    }

    private bool IsMovementTouchStillPressed()
    {
        if (Touchscreen.current == null)
            return false;

        var touches = Touchscreen.current.touches;

        for (int i = 0; i < touches.Count; i++)
        {
            TouchControl touch = touches[i];

            if (touch.touchId.ReadValue() == movementTouchId)
            {
                return touch.press.isPressed;
            }
        }

        return false;
    }
}