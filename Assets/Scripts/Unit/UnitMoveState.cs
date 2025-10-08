using UnityEngine;

public class UnitMoveState : UnitBaseState
{
    public override void EnterState(UnitController unit)
    {
        if (SelectionManager.Instance.SelectedUnits.Contains(unit.gameObject) && unit.CheckRightClickGround() && Input.GetMouseButtonDown(1))
        {
            unit.movementCmp.MoveToPosition();
        }
    }

    public override void UpdateState(UnitController unit)
    {
        if (SelectionManager.Instance.SelectedUnits.Contains(unit.gameObject) && unit.CheckRightClickGround() && Input.GetMouseButtonDown(1))
        {
            if (unit.CheckRightClickEnemy())
            {
                unit.movementCmp.SetTargetToChase();
                unit.SwitchState(unit.chaseState);
                return;
            }
            else
            {
                unit.movementCmp.MoveToPosition();
                return;
            }

        }

        if (unit.movementCmp.ReachedDestination())
        {
            unit.SwitchState(unit.idleState);
            return;
        }
    }
}


