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
        }
    }
}
