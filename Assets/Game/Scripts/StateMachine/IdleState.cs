using UnityEngine;
public class IdleState : IState
{
    private readonly Movement movement;

    public IdleState(Movement movement)
    {
        this.movement = movement;
    }

    public void Enter()
    {
        Debug.Log("Enter Idle State");
    }

    public void Execute()
    {
        if (movement.IsMoving)
        {
            if (movement.IsMovingFast)
                movement.ToRun();
            else
                movement.ToWalk();

            return;
        }

        if (movement.damageZone.HasEnemy)
        {
            movement.ToAttack();
            return;
        }
    }

    public void FixedExecute() { }

    public void Exit()
    {
        Debug.Log("Exit Idle State");
    }
}
