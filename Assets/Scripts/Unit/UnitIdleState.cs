using UnityEngine;

public class UnitIdleState : UnitBaseState
{
    public override void EnterState(UnitController unit)
    {
    }

    public override void UpdateState(UnitController unit)
    {
        if (SelectionManager.Instance.SelectedUnits.Contains(unit.gameObject) && Input.GetMouseButtonDown(1))
        {
            unit.SwitchState(unit.movementState);
            return;
        }
        if (unit.targetToAttack != null && unit.isCommandedToMove == false)
        {
            unit.SwitchState(unit.chaseState);
            return;
        }
        if (unit.movementCmp.ReachedDestination())
        {
            unit.isCommandedToMove = false;
            return;
        }
    }
}
