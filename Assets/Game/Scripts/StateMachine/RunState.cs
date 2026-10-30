using UnityEngine;

public class RunState : IState
{
    private readonly Movement movement;
    public RunState(Movement movement)
    {
        this.movement = movement;
    }
    public void Enter()
    {
        Debug.Log("Enter Run State");
    }

    public void Execute()
    {
        if (!movement.IsMoving)
        {
            movement.ToIdle();
            return;
        }

        if (!movement.IsMovingFast)
        {
            movement.ToWalk();
            return;
        }

        if (movement.DashRequested && !movement.IsAttacking && movement.DashState.CanDash)
        {
            movement.ClearDashRequest();
            movement.ToDash();
            return;
        }

        movement.Move(movement.CalculateMovementSpeed());
    }

    public void Exit()
    {
        Debug.Log("Exit Run State");
    }

    public void FixedExecute()
    {
        
    }

}
