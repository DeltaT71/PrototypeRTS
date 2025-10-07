using UnityEngine;

public class UnitMoveState : UnitBaseState
{
    public override void EnterState(UnitController unit)
    {
        if (SelectionManager.Instance.SelectedUnits.Contains(unit.gameObject) && Input.GetMouseButtonDown(1))
        {
            unit.movementCmp.MoveToPosition();
        }
    }

    public override void UpdateState(UnitController unit)
    {
        if (SelectionManager.Instance.SelectedUnits.Contains(unit.gameObject) && Input.GetMouseButtonDown(1))
        {
            unit.movementCmp.MoveToPosition();
            return;
        }

        if (unit.movementCmp.ReachedDestination())
        {
            unit.SwitchState(unit.idleState);
            return;
        }
    }
}


