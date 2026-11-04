using UnityEngine;

public class AttackState2 : IState
{
    private readonly Movement movement;

    public AttackState2(Movement movement)
    {
        this.movement = movement;
    }

    public void Enter()
    {
        StartAttackCycle();
        movement.SetDamageScale(0.2f);
    }

    public void Execute()
    {
        if (movement.IsAttackPhaseFinished)
        {
            if (movement.damageZone.HasEnemy && !movement.IsMoving)
            {
                movement.ToAttack3();
                return;
            }
        }

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

        movement.ToIdle();
    }

    private void StartAttackCycle()
    {
        movement.SetAttackPhaseFinished(false);
        movement.SetAttackAnimationFinished(false);
    }

    public void FixedExecute() { }

    public void Exit()
    {
        movement.ResetDamageScale();
    }
}