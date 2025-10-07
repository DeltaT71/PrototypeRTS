

using UnityEngine;

public class UnitChaseState : UnitBaseState
{
    public override void EnterState(UnitController unit)
    {

    }

    public override void UpdateState(UnitController unit)
    {
        if (SelectionManager.Instance.SelectedUnits.Contains(unit.gameObject) && Input.GetMouseButtonDown(1))
        {
            unit.isCommandedToMove = true;
            unit.SwitchState(unit.movementState);
            return;
        }
        else if (unit.targetToAttack != null && unit.isCommandedToMove == false)
        {
            unit.movementCmp.ChaseTarget(unit.targetToAttack.position);
            return;
        }
    }

}
