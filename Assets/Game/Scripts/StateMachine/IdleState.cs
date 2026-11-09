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
        movement.StopMovement();
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
            switch (movement.EquipmentManager.currentWeaponState)
            {
                case WeaponHandState.Blunt:
                    movement.ToAttack();
                    break;

                case WeaponHandState.Sharp:
                    movement.ToAttack2();
                    break;
            }

            return;
        }
    }

    public void FixedExecute() { }

    public void Exit()
    {
        Debug.Log("Exit Idle State");
    }
}
