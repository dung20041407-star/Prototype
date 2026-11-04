using UnityEngine;

public class AttackState4 : IState
{
    private readonly Movement movement;

    public AttackState4(Movement movement)
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
                if (movement.EquipmentManager.currentWeaponState == WeaponHandState.Blunt)
                    movement.ToAttack();
                else
                    movement.ToAttack2();

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
        movement.SetAttackAnimationFinished(false);
        movement.SetAttackPhaseFinished(false);
    }

    public void FixedExecute() { }

    public void Exit()
    {
        movement.ResetDamageScale();
    }
}
