using UnityEngine;

public class AttackState5 : IState
{
    private readonly Movement movement;

    public AttackState5(Movement movement)
    {
        this.movement = movement;
    }

    public void Enter()
    {
        StartAttackCycle();
        Debug.Log("Enter Attack5 State");
    }

    public void Execute()
    {
        if (!movement.IsAttackAnimationFinished)
            return;

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

        movement.ToIdle();
    }

    private void StartAttackCycle()
    {
        movement.SetAttackAnimationFinished(false);
    }

    public void FixedExecute() { }

    public void Exit()
    {
        Debug.Log("Exit Attack5 State");
    }
}
