using UnityEngine;

public class WalkState : IState
{
    private readonly Movement movement;

    public WalkState(Movement movement)
    {
        this.movement = movement;
    }
    public void Enter()
    {
        Debug.Log("Enter Walk State");
    }

    public void Execute()
    {
        if (!movement.IsMoving)
        {
            movement.ToIdle();
            return;
        }

        if (movement.IsMovingFast)
            movement.ToRun();

        movement.Move(movement.CalculateMovementSpeed());
    }

    public void Exit()
    {
        Debug.Log("Exit Walk State");
    }

    public void FixedExecute()
    {
    }
}
