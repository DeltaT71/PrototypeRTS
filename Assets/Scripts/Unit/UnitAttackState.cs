using UnityEngine;

public class UnitAttackState : UnitBaseState
{
    public override void EnterState(UnitController unit)
    {
    }

    public override void UpdateState(UnitController unit)
    {
        if (unit.targetToAttack == null)
        {
            unit.SwitchState(unit.idleState);
            return;
        }

        if (SelectionManager.Instance.SelectedUnits.Contains(unit.gameObject) && Input.GetMouseButtonDown(1))
        {
            unit.SwitchState(unit.movementState);
            return;
        }

        if (unit.CalculateDistanceFromEnemy(unit.targetToAttack.position) > unit.attackRange)
        {
            unit.SwitchState(unit.chaseState);
            return;
        }

        Debug.Log("Attacking!");
    }
}
