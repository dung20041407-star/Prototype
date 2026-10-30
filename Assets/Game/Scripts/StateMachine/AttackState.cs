using UnityEngine;

public class AttackState : IState
{
    private readonly Movement movement;

    public AttackState(Movement movement)
    {
        this.movement = movement;
    }

    public void Enter()
    {
        movement.SetAttacking(true);
        StartAttackCycle();
    }

    public void Execute()
    {
        if (!movement.IsAttackAnimationFinished)
            return;

        if (movement.IsMoving)
        {
            movement.SetAttacking(false);

            if (movement.IsMovingFast)
                movement.ToRun();
            else
                movement.ToWalk();

            return;
        }

        if (movement.damageZone.HasEnemy)
        {
            StartAttackCycle();
            return;
        }

        movement.SetAttacking(false);
        movement.ToIdle();
    }

    private void StartAttackCycle()
    {
        movement.SetAttackAnimationFinished(false);
    }

    public void FixedExecute() { }

    public void Exit()
    {
        movement.SetAttacking(false);
    }
}
