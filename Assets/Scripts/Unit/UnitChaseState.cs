using UnityEngine;

public class UnitChaseState : UnitBaseState
{
    public override void EnterState(UnitController unit)
    {

    }

    public override void UpdateState(UnitController unit)
    {
        if (IsUnitSelected(unit) && Input.GetMouseButtonDown(1))
        {
            unit.isCommandedToMove = true;
            unit.movementCmp.chaseTarget = null;
            unit.SwitchState(unit.movementState);
            return;
        }

        if (unit.movementCmp.chaseTarget != null)
        {
            unit.movementCmp.ChaseTarget(unit.movementCmp.chaseTarget.transform.position);

            if (unit.CalculateDistanceFromEnemy(unit.movementCmp.chaseTarget.transform.position) < unit.attackRange)
            {
                unit.SwitchState(unit.attackState);
                return;
            }

            return;
        }

        if (unit.targetToAttack != null && unit.isCommandedToMove == false)
        {
            unit.movementCmp.ChaseTarget(unit.targetToAttack.position);

            if (unit.CalculateDistanceFromEnemy(unit.targetToAttack.position) < unit.attackRange)
            {
                unit.SwitchState(unit.attackState);
                return;
            }

            return;
        }

        if (unit.targetToAttack == null)
        {
            unit.SwitchState(unit.idleState);
            return;
        }
    }
    private bool IsUnitSelected(UnitController unit)
    {
        return SelectionManager.Instance.SelectedUnits.Contains(unit.gameObject);
    }
}
